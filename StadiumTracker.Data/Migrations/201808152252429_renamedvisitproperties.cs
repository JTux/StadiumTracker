namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedvisitproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visit", "GotPin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Visit", "GotPhoto", c => c.Boolean(nullable: false));
            DropColumn("dbo.Visit", "HasPin");
            DropColumn("dbo.Visit", "HasPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visit", "HasPhoto", c => c.Boolean(nullable: false));
            AddColumn("dbo.Visit", "HasPin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Visit", "GotPhoto");
            DropColumn("dbo.Visit", "GotPin");
        }
    }
}
