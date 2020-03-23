namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.League",
                c => new
                    {
                        LeagueId = c.Int(nullable: false, identity: true),
                        LeagueName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LeagueId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        LeagueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.League", t => t.LeagueId, cascadeDelete: true)
                .Index(t => t.LeagueId);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        VisitId = c.Int(nullable: false, identity: true),
                        VisitorId = c.Int(nullable: false),
                        ParkId = c.Int(nullable: false),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        VisitDate = c.DateTime(nullable: false),
                        GotPin = c.Boolean(nullable: false),
                        GotPhoto = c.Boolean(nullable: false),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.VisitId)
                .ForeignKey("dbo.Team", t => t.AwayTeamId, cascadeDelete: false)
                .ForeignKey("dbo.Team", t => t.HomeTeamId, cascadeDelete: false)
                .ForeignKey("dbo.Park", t => t.ParkId, cascadeDelete: true)
                .ForeignKey("dbo.Visitor", t => t.VisitorId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.Team_TeamId)
                .Index(t => t.VisitorId)
                .Index(t => t.ParkId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
                .Index(t => t.Team_TeamId);
            
            CreateTable(
                "dbo.Park",
                c => new
                    {
                        ParkId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ParkName = c.String(nullable: false),
                        CityName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ParkId);
            
            CreateTable(
                "dbo.Visitor",
                c => new
                    {
                        VisitorId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VisitorId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Visit", "Team_TeamId", "dbo.Team");
            DropForeignKey("dbo.Visit", "VisitorId", "dbo.Visitor");
            DropForeignKey("dbo.Visit", "ParkId", "dbo.Park");
            DropForeignKey("dbo.Visit", "HomeTeamId", "dbo.Team");
            DropForeignKey("dbo.Visit", "AwayTeamId", "dbo.Team");
            DropForeignKey("dbo.Team", "LeagueId", "dbo.League");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Visit", new[] { "Team_TeamId" });
            DropIndex("dbo.Visit", new[] { "AwayTeamId" });
            DropIndex("dbo.Visit", new[] { "HomeTeamId" });
            DropIndex("dbo.Visit", new[] { "ParkId" });
            DropIndex("dbo.Visit", new[] { "VisitorId" });
            DropIndex("dbo.Team", new[] { "LeagueId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Visitor");
            DropTable("dbo.Park");
            DropTable("dbo.Visit");
            DropTable("dbo.Team");
            DropTable("dbo.League");
        }
    }
}
