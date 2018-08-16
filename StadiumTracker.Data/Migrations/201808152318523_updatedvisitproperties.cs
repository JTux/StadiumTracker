namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedvisitproperties : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Visitor", new[] { "Visit_VisitID" });
            CreateIndex("dbo.Visitor", "Visit_VisitId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Visitor", new[] { "Visit_VisitId" });
            CreateIndex("dbo.Visitor", "Visit_VisitID");
        }
    }
}
