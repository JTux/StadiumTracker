namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedownerids : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Team", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Visitor", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Visit", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visit", "OwnerId");
            DropColumn("dbo.Visitor", "OwnerId");
            DropColumn("dbo.Team", "OwnerId");
        }
    }
}
