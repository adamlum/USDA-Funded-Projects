using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace USDAFundedProjects.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key, Column("id")]
        public int TopicID { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}