namespace StadiumTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfullname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visitor", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visitor", "FullName");
        }
    }
}
