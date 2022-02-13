
using GHIApplication.ChangeManagement.Models;
using GHIApplication.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace GHIApplication.Models
{
    public class GHIDBContext : DbContext
    {
        public GHIDBContext() : base("Name=GHIDB") {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
        public class DbConfiguration : System.Data.Entity.DbConfiguration
        {
            public DbConfiguration()
            {
                SetExecutionStrategy("System.Data.SqlClient",
                    () => new SqlAzureExecutionStrategy(5, TimeSpan.Parse("00:00:03")));
            }
        }

        public DbSet<EmployeeInfo> EmployeeInfo { get; set; }
        public DbSet<ChangeTypeInfo> ChangeTypeInfo { get; set; }
        public DbSet<RiskAuthorityInfo> RiskAuthorityInfo { get; set; }
        public DbSet<AssignAuthorityInfo> AssignAuthorityInfo { get; set; }
        public DbSet<VerificationAuthorityInfo> VerificationAuthorityInfo { get; set; }
        public DbSet<ChangeInfo> ChangeInfo { get; set; }
        public DbSet<RiskAssessmentInfo> RiskAssessmentInfo { get; set; }
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<IncidentInvestigation> IncidentInvestigation { get; set; }
        public DbSet<IncidentInvestigationReport> IncidentInvestigationReport { get; set; }
    }
}