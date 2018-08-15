namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedparktable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Park", "HasPin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Park", "HasPhoto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Park", "HasPhoto");
            DropColumn("dbo.Park", "HasPin");
        }
    }
}
