using Qoveo.Impact.Data.Configuration;
using Qoveo.Impact.Model;
using System.Data.Entity;

namespace Qoveo.Impact.Data
{
    public class ImpactDbContext : DbContext
    {
        /// <summary>
        /// Public Default Constructor
        /// </summary>
        public ImpactDbContext()
        : base(nameOrConnectionString: "Impact") { }

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="connectionString">Connection String to the Database</param>
        public ImpactDbContext(string connectionString)
            : base(nameOrConnectionString: connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Data Configuration
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new ResultConfiguration());
            modelBuilder.Configurations.Add(new SurveyConfiguration());
            modelBuilder.Configurations.Add(new TeamConfiguration());
        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}
