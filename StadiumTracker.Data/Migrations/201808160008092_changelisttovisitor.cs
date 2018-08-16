namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changelisttovisitor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visitor", "Visit_VisitId", "dbo.Visit");
            DropIndex("dbo.Visitor", new[] { "Visit_VisitId" });
            AddColumn("dbo.Visit", "Visitor_VisitorId", c => c.Int());
            CreateIndex("dbo.Visit", "Visitor_VisitorId");
            AddForeignKey("dbo.Visit", "Visitor_VisitorId", "dbo.Visitor", "VisitorId");
            DropColumn("dbo.Visitor", "Visit_VisitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visitor", "Visit_VisitId", c => c.Int());
            DropForeignKey("dbo.Visit", "Visitor_VisitorId", "dbo.Visitor");
            DropIndex("dbo.Visit", new[] { "Visitor_VisitorId" });
            DropColumn("dbo.Visit", "Visitor_VisitorId");
            CreateIndex("dbo.Visitor", "Visit_VisitId");
            AddForeignKey("dbo.Visitor", "Visit_VisitId", "dbo.Visit", "VisitId");
        }
    }
}
