namespace Qoveo.Impact.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SessionId = c.Int(nullable: false),
                        ClusterId = c.Int(),
                        UserId = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .ForeignKey("dbo.Clusters", t => t.ClusterId)
                .Index(t => t.SessionId)
                .Index(t => t.ClusterId);
            
            CreateTable(
                "dbo.Clusters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PdfUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tutors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FirstName = c.String(),
                        SessionId = c.Int(nullable: false),
                        ClusterId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .ForeignKey("dbo.Clusters", t => t.ClusterId, cascadeDelete: true)
                .Index(t => t.SessionId)
                .Index(t => t.ClusterId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        MissionId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Jauge1 = c.Single(nullable: false),
                        Jauge2 = c.Single(nullable: false),
                        Jauge3 = c.Single(nullable: false),
                        Jauge4 = c.Single(nullable: false),
                        Jauge5 = c.Single(nullable: false),
                        ScoreEpreuve1 = c.Int(nullable: false),
                        ScoreEpreuve2 = c.Int(nullable: false),
                        ScoreEpreuve3 = c.Int(nullable: false),
                        GlobalScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MissionId, t.TeamId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.MissionId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.MissionId);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        MissionUrl = c.String(),
                        FtaPdfUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        MissionId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Question = c.String(nullable: false, maxLength: 128),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => new { t.MissionId, t.TeamId, t.Question })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.MissionId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.MissionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Surveys", new[] { "MissionId" });
            DropIndex("dbo.Surveys", new[] { "TeamId" });
            DropIndex("dbo.Results", new[] { "MissionId" });
            DropIndex("dbo.Results", new[] { "TeamId" });
            DropIndex("dbo.Tutors", new[] { "ClusterId" });
            DropIndex("dbo.Tutors", new[] { "SessionId" });
            DropIndex("dbo.Teams", new[] { "ClusterId" });
            DropIndex("dbo.Teams", new[] { "SessionId" });
            DropForeignKey("dbo.Surveys", "MissionId", "dbo.Missions");
            DropForeignKey("dbo.Surveys", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Results", "MissionId", "dbo.Missions");
            DropForeignKey("dbo.Results", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Tutors", "ClusterId", "dbo.Clusters");
            DropForeignKey("dbo.Tutors", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.Teams", "ClusterId", "dbo.Clusters");
            DropForeignKey("dbo.Teams", "SessionId", "dbo.Sessions");
            DropTable("dbo.Surveys");
            DropTable("dbo.Missions");
            DropTable("dbo.Results");
            DropTable("dbo.Tutors");
            DropTable("dbo.Clusters");
            DropTable("dbo.Teams");
            DropTable("dbo.Sessions");
        }
    }
}
