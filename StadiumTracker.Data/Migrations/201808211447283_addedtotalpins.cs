namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtotalpins : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visitor", "TotalPins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visitor", "TotalPins");
        }
    }
}
