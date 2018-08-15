namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpark : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Visit", new[] { "Park_ParkID" });
            AddColumn("dbo.Park", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Park", "ParkName", c => c.String(nullable: false));
            CreateIndex("dbo.Visit", "Park_ParkId");
            DropColumn("dbo.Park", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Park", "Name", c => c.String(nullable: false));
            DropIndex("dbo.Visit", new[] { "Park_ParkId" });
            DropColumn("dbo.Park", "ParkName");
            DropColumn("dbo.Park", "OwnerId");
            CreateIndex("dbo.Visit", "Park_ParkID");
        }
    }
}
