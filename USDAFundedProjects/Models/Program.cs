using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USDAFundedProjects.Models
{
    [Table("Programs")]
    public class Program
    {
        [Key, Column("id")]
        public int ProgramID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("abbreviation")]
        public string Abbreviation { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}