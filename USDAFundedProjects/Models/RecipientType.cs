using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USDAFundedProjects.Models
{
    [Table("RecipientTypes")]
    public class RecipientType
    {
        [Key, Column("id")]
        public int RecipientTypeID { get; set; }

        [Column("description")]
        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}