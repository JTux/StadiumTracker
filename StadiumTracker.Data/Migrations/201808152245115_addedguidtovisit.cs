namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedguidtovisit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visit", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visit", "OwnerId");
        }
    }
}