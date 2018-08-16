namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforeignkeystovisit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visit", "Visitor_VisitorId", "dbo.Visitor");
            DropIndex("dbo.Visit", new[] { "Visitor_VisitorId" });
            RenameColumn(table: "dbo.Visit", name: "Visitor_VisitorId", newName: "VisitorId");
            AlterColumn("dbo.Visit", "VisitorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Visit", "VisitorId");
            AddForeignKey("dbo.Visit", "VisitorId", "dbo.Visitor", "VisitorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visit", "VisitorId", "dbo.Visitor");
            DropIndex("dbo.Visit", new[] { "VisitorId" });
            AlterColumn("dbo.Visit", "VisitorId", c => c.Int());
            RenameColumn(table: "dbo.Visit", name: "VisitorId", newName: "Visitor_VisitorId");
            CreateIndex("dbo.Visit", "Visitor_VisitorId");
            AddForeignKey("dbo.Visit", "Visitor_VisitorId", "dbo.Visitor", "VisitorId");
        }
    }
}
