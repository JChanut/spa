namespace Qoveo.Impact.Data.Migrations
{
    using Qoveo.Impact.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Qoveo.Impact.Data.ImpactDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Qoveo.Impact.Data.ImpactDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // Création de la session
            // Seulement en debug
            #if DEBUG
            var session = new Session { Id = 1, Name = "Session 1", StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddMonths(1) };
            context.Sessions.AddOrUpdate(session);
            #endif

            // Création des missions
            var missions = new List<Mission>()
            {
                new Mission{Id = 1, Name = "Mission 1", Order = 1, MissionUrl = "/content/Modules/IMPACT_Mission1/home.html"},
                new Mission{Id = 2, Name = "Mission 2", Order = 2, MissionUrl = "/content/Modules/IMPACT_Mission2/home.html"},
                new Mission{Id = 3, Name = "Mission 3", Order = 3, MissionUrl = "/content/Modules/IMPACT_Mission3/home.html"},
                new Mission{Id = 4, Name = "Mission 4", Order = 4, MissionUrl = "/content/Modules/IMPACT_Mission4/home.html"},
            };
            missions.ForEach(m => context.Missions.AddOrUpdate(m));

            // Création des clusters
            var clusters = new List<Cluster>()
            {
                new Cluster{Id = 1, Name = "Finance"},
                new Cluster{Id = 2, Name = "Outdoor Goods"},
                new Cluster{Id = 3, Name = "Construction Materials"},
                new Cluster{Id = 4, Name = "Food Industry"},
                new Cluster{Id = 5, Name = "Cultural Industries"},
                new Cluster{Id = 6, Name = "Retailing"},
                new Cluster{Id = 7, Name = "Nuclear Industry"},
                new Cluster{Id = 8, Name = "Automotive"},
                new Cluster{Id = 9, Name = "Luxury Goods"},
                new Cluster{Id = 10, Name = "Information Technology"},
            };
            clusters.ForEach(p => context.Clusters.AddOrUpdate(p));

            // Création des équipes
            // Seulement en DEBUG
            #if DEBUG
            var teams = new List<Team>()
            {
                new Team{Id = 1, Name = "Team 1", SessionId = 1, ClusterId = 1, Login="Team1Session1", Password = "test1"},
                new Team{Id = 2, Name = "Team 2", SessionId = 1, ClusterId = 2, Password = "test2"},
                new Team{Id = 3, Name = "Team 3", SessionId = 1, ClusterId = 3, Password = "test3"},
                new Team{Id = 4, Name = "Team 4", SessionId = 1, ClusterId = 4, Password = "test4"},
            };
            teams.ForEach(p => context.Teams.AddOrUpdate(p));
            #endif
        }
    }
}
