namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedparkcounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Park", "VisitCount", c => c.Int(nullable: false));
            AddColumn("dbo.Park", "PinCount", c => c.Int(nullable: false));
            AddColumn("dbo.Park", "PhotoCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Park", "PhotoCount");
            DropColumn("dbo.Park", "PinCount");
            DropColumn("dbo.Park", "VisitCount");
        }
    }
}
