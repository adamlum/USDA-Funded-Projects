using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using USDAFundedProjects.Models;

namespace USDAFundedProjects.Controllers
{
    public class HomeController : Controller
    {
        private USDAFundedProjectsContext context = new USDAFundedProjectsContext();

        public ActionResult Index()
        {
            ViewBag.Message = "This dataset reflects USDA funded projects to develop local and regional food systems. It includes data from virtually all USDA Agencies and 9 other Federal Departments.";
            ViewBag.Title = "USDA Funded Projects - Know Your Farmer, Know Your Food";

            ViewBag.Programs = context.Programs.ToList();
            ViewBag.Agencies = context.Agencies.ToList();
            ViewBag.Missions = context.MissionAreas.ToList();
            ViewBag.RecipientTypes = context.RecipientTypes.ToList();
            ViewBag.FundingTypes = context.FundingTypes.ToList();
            ViewBag.Topics = context.Topics.ToList();
            int[] Years = (from project in context.Projects select project.Year).Distinct().ToArray();
            Array.Sort(Years);
            ViewBag.Years = Years;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Import()
        {
            List<Project> projects = context.Projects.ToList();

            if (projects.Count == 0)
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\kyfprojects.xls";
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");

                int projectCount = 0;

                try
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Updated KYF Projects$]", conn);

                    OleDbDataReader rdr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(rdr);

                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["Program Name"].ToString().Trim();
                        Program program = context.Programs.FirstOrDefault(p => p.Name == name);
                        if (program == null)
                        {
                            program = new Program { Name = row["Program Name"].ToString().Trim(), Abbreviation = row["Program Abbreviation"].ToString().Trim() };
                            context.Programs.Add(program);
                        }

                        name = row["USDA Agency"].ToString().Trim();
                        Agency agency = context.Agencies.FirstOrDefault(a => a.Name == name);
                        if (agency == null)
                        {
                            agency = new Agency { Name = row["USDA Agency"].ToString().Trim() };
                            context.Agencies.Add(agency);
                        }

                        name = row["USDA Mission Area"].ToString().Trim();
                        MissionArea missionArea = context.MissionAreas.FirstOrDefault(m => m.Name == name);
                        if (missionArea == null)
                        {
                            missionArea = new MissionArea { Name = row["USDA Mission Area"].ToString().Trim() };
                            context.MissionAreas.Add(missionArea);
                        }

                        string description = row["Recipient Type"].ToString().Trim();
                        RecipientType recipientType = context.RecipientTypes.FirstOrDefault(r => r.Description == description);
                        if (recipientType == null)
                        {
                            recipientType = new RecipientType { Description = row["Recipient Type"].ToString().Trim() };
                            context.RecipientTypes.Add(recipientType);
                        }

                        description = row["Funding Type"].ToString().Trim();
                        FundingType fundingType = context.FundingTypes.FirstOrDefault(f => f.Description == description);
                        if (fundingType == null)
                        {
                            fundingType = new FundingType { Description = row["Funding Type"].ToString().Trim() };
                            context.FundingTypes.Add(fundingType);
                        }

                        Topic topicA = null;
                        if (row["Topic_A"].ToString().Trim().Length > 0)
                        {
                            description = row["Topic_A"].ToString().Trim();
                            topicA = context.Topics.FirstOrDefault(t => t.Description == description);
                            if (topicA == null)
                            {
                                topicA = new Topic { Description = row["Topic_A"].ToString().Trim() };
                                context.Topics.Add(topicA);
                            }
                        }

                        Topic topicB = null;
                        if (row["Topic_B"].ToString().Trim().Length > 0)
                        {
                            description = row["Topic_B"].ToString().Trim();
                            topicB = context.Topics.FirstOrDefault(t => t.Description == description);
                            if (topicB == null)
                            {
                                topicB = new Topic { Description = row["Topic_B"].ToString().Trim() };
                                context.Topics.Add(topicB);
                            }
                        }

                        Topic topicC = null;
                        if (row["Topic_C"].ToString().Trim().Length > 0)
                        {
                            description = row["Topic_C"].ToString().Trim();
                            topicC = context.Topics.FirstOrDefault(t => t.Description == description);
                            if (topicC == null)
                            {
                                topicC = new Topic { Description = row["Topic_C"].ToString().Trim() };
                                context.Topics.Add(topicC);
                            }
                        }

                        context.SaveChanges();

                        Project project = new Project();
                        project.Title = row["Project Title"].ToString().Trim();
                        project.Program = program;
                        project.Year = int.Parse(row["Year"].ToString().Trim());
                        project.State = row["State"].ToString().Trim();
                        project.Town = row["Town"].ToString().Trim();
                        project.Zip = row["Zip"].ToString().Trim();
                        project.Agency = agency;
                        project.MissionArea = missionArea;
                        project.Recipient = row["Recipient"].ToString().Trim();
                        project.RecipientType = recipientType;
                        project.FundingAmount = (row["Funding Amount ($)"].ToString().Trim().Length > 0) ? decimal.Parse(row["Funding Amount ($)"].ToString().Trim()) : 0;
                        project.FundingType = fundingType;
                        project.Description = row["Description"].ToString().Trim();
                        project.MoreInfoURL = row["More Information"].ToString().Trim();
                        if (topicA != null)
                        {
                            project.TopicAID = topicA.TopicID;
                        }
                        if (topicB != null)
                        {
                            project.TopicBID = topicB.TopicID;
                        }
                        if (topicC != null)
                        {
                            project.TopicCID = topicC.TopicID;
                        }

                        context.Projects.Add(project);
                        context.SaveChanges();

                        projectCount++;
                    }

                    ViewBag.Result = String.Format("{0} Projects imported successfully!", projectCount);
                }
                catch (Exception ex)
                {
                    ViewBag.Result = String.Format("There was an error importing the projects.<br /><br />{0} projects imported.<br /><br />{1}", projectCount, ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                ViewBag.Result = "Projects already imported.";
            }
            
            return View();
        }
    }
}
