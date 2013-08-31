using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace USDAFundedProjects.Models
{
    [Table("Projects")]
    public class Project
    {
        [Key, Column("id")]
        public int ProjectID { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("program_id")]
        public int ProgramID { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("town")]
        public string Town { get; set; }

        [Column("zip")]
        public string Zip { get; set; }

        [Column("agency_id")]
        public int AgencyID { get; set; }

        [Column("mission_id")]
        public int MissionAreaID { get; set; }

        [Column("recipient")]
        public string Recipient { get; set; }

        [Column("recipient_type_id")]
        public int RecipientTypeID { get; set; }

        [Column("funding_amount")]
        public decimal FundingAmount { get; set; }

        [Column("funding_type_id")]
        public int FundingTypeID { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("topic_a_id")]
        public int TopicAID { get; set; }

        [Column("topic_b_id")]
        public int TopicBID { get; set; }

        [Column("topic_c_id")]
        public int TopicCID { get; set; }

        [Column("more_information_url")]
        public string MoreInfoURL { get; set; }

        [ForeignKey("ProgramID")]
        public Program Program { get; set; }

        [ForeignKey("AgencyID")]
        public Agency Agency { get; set; }

        [ForeignKey("MissionAreaID")]
        public MissionArea MissionArea { get; set; }

        [ForeignKey("RecipientTypeID")]
        public RecipientType RecipientType { get; set; }

        [ForeignKey("FundingTypeID")]
        public FundingType FundingType { get; set; }
    }
}