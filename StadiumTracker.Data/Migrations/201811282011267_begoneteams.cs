namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class begoneteams : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visit", "AwayTeam_TeamId", "dbo.Team");
            DropForeignKey("dbo.Visit", "HomeTeam_TeamId", "dbo.Team");
            DropIndex("dbo.Visit", new[] { "AwayTeam_TeamId" });
            DropIndex("dbo.Visit", new[] { "HomeTeam_TeamId" });
            DropColumn("dbo.Visit", "AwayTeam_TeamId");
            DropColumn("dbo.Visit", "HomeTeam_TeamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visit", "HomeTeam_TeamId", c => c.Int());
            AddColumn("dbo.Visit", "AwayTeam_TeamId", c => c.Int());
            CreateIndex("dbo.Visit", "HomeTeam_TeamId");
            CreateIndex("dbo.Visit", "AwayTeam_TeamId");
            AddForeignKey("dbo.Visit", "HomeTeam_TeamId", "dbo.Team", "TeamId");
            AddForeignKey("dbo.Visit", "AwayTeam_TeamId", "dbo.Team", "TeamId");
        }
    }
}
