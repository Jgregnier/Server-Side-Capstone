namespace Cape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChaingReportModelAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reports", new[] { "User_Id" });
            AddColumn("dbo.Reports", "UserId", c => c.String());
            DropColumn("dbo.Reports", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Reports", "UserId");
            CreateIndex("dbo.Reports", "User_Id");
            AddForeignKey("dbo.Reports", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
