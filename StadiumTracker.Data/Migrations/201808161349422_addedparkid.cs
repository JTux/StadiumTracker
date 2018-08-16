namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedparkid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Visit", name: "Park_ParkId", newName: "ParkId");
            RenameIndex(table: "dbo.Visit", name: "IX_Park_ParkId", newName: "IX_ParkId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Visit", name: "IX_ParkId", newName: "IX_Park_ParkId");
            RenameColumn(table: "dbo.Visit", name: "ParkId", newName: "Park_ParkId");
        }
    }
}
