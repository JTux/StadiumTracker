namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedteamfk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Team", name: "League_LeagueId", newName: "LeagueId");
            RenameIndex(table: "dbo.Team", name: "IX_League_LeagueId", newName: "IX_LeagueId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Team", name: "IX_LeagueId", newName: "IX_League_LeagueId");
            RenameColumn(table: "dbo.Team", name: "LeagueId", newName: "League_LeagueId");
        }
    }
}
