using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USDAFundedProjects.Models
{
    [Table("FundingTypes")]
    public class FundingType
    {
        [Key, Column("id")]
        public int FundingTypeID { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}