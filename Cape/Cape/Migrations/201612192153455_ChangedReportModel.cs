namespace Cape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedReportModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reports", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "Name", c => c.String(nullable: false));
        }
    }
}
