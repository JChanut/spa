namespace Qoveo.Impact.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMissionsStartDateInSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Mission1StartDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission1EndDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission2StartDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission2EndDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission3StartDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission3EndDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission4StartDate", c => c.DateTime());
            AddColumn("dbo.Sessions", "Mission4EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Mission4EndDate");
            DropColumn("dbo.Sessions", "Mission4StartDate");
            DropColumn("dbo.Sessions", "Mission3EndDate");
            DropColumn("dbo.Sessions", "Mission3StartDate");
            DropColumn("dbo.Sessions", "Mission2EndDate");
            DropColumn("dbo.Sessions", "Mission2StartDate");
            DropColumn("dbo.Sessions", "Mission1EndDate");
            DropColumn("dbo.Sessions", "Mission1StartDate");
        }
    }
}
