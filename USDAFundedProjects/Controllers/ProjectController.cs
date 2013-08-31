using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USDAFundedProjects.Models;

namespace USDAFundedProjects.Controllers
{
    public class ProjectController : Controller
    {
        private USDAFundedProjectsContext context = new USDAFundedProjectsContext();
        [HttpPost]
        public ActionResult Index()
        {
            List<Project> results = context.Projects
                .Include("Program")
                .Include("Agency")
                .Include("MissionArea")
                .Include("RecipientType")
                .Include("FundingType").ToList();

            if (Request.Form["Year"].ToString().Trim().Length > 0)
            {
                int year = int.Parse(Request.Form["Year"].ToString().Trim());
                results = results.Where(p => p.Year == year).ToList();
            }

            if (Request.Form["ProgramID"].ToString().Trim().Length > 0)
            {
                int programId = int.Parse(Request.Form["ProgramID"].ToString().Trim());
                results = results.Where(p => p.ProgramID == programId).ToList();
            }

            if (Request.Form["AgencyID"].ToString().Trim().Length > 0)
            {
                int agencyId = int.Parse(Request.Form["AgencyID"].ToString().Trim());
                results = results.Where(p => p.AgencyID == agencyId).ToList();
            }

            if (Request.Form["MissionAreaID"].ToString().Trim().Length > 0)
            {
                int missionAreaId = int.Parse(Request.Form["MissionAreaID"].ToString().Trim());
                results = results.Where(p => p.MissionAreaID == missionAreaId).ToList();
            }

            if (Request.Form["RecipientTypeID"].ToString().Trim().Length > 0)
            {
                int recipientTypeId = int.Parse(Request.Form["RecipientTypeID"].ToString().Trim());
                results = results.Where(p => p.RecipientTypeID == recipientTypeId).ToList();
            }

            if (Request.Form["FundingTypeID"].ToString().Trim().Length > 0)
            {
                int fundingTypeId = int.Parse(Request.Form["FundingTypeID"].ToString().Trim());
                results = results.Where(p => p.FundingTypeID == fundingTypeId).ToList();
            }

            if (Request.Form["TopicID"].ToString().Trim().Length > 0)
            {
                int topicId = int.Parse(Request.Form["TopicID"].ToString().Trim());
                results = results.Where(p => p.TopicAID == topicId || p.TopicBID == topicId || p.TopicCID == topicId).ToList();
            }

            ViewBag.Topics = context.Topics.ToList();
            ViewBag.Results = results;
            
            return View();
        }

        public ActionResult Details(int id)
        {
            Project project = context.Projects.Include("Program")
                .Include("Agency")
                .Include("MissionArea")
                .Include("RecipientType")
                .Include("FundingType").FirstOrDefault(p => p.ProjectID == id);

            if (project != null)
            {
                ViewBag.TopicA = context.Topics.FirstOrDefault(t => t.TopicID == project.TopicAID);
                ViewBag.TopicB = context.Topics.FirstOrDefault(t => t.TopicID == project.TopicBID);
                ViewBag.TopicC = context.Topics.FirstOrDefault(t => t.TopicID == project.TopicCID);
                return View(project);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
