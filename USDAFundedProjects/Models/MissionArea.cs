using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USDAFundedProjects.Models
{
    [Table("MissionAreas")]
    public class MissionArea
    {
        [Key, Column("id")]
        public int MissionAreaID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}