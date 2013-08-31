using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace USDAFundedProjects.Models
{
    public class USDAFundedProjectsContext : DbContext
    {
        public USDAFundedProjectsContext() : base("USDAFundedProjects") { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<FundingType> FundingTypes { get; set; }
        public DbSet<MissionArea> MissionAreas { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<RecipientType> RecipientTypes { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}