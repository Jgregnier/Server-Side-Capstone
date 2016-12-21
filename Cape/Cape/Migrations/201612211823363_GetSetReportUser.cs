namespace Cape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetSetReportUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reports", "User_Id");
            AddForeignKey("dbo.Reports", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reports", new[] { "User_Id" });
            DropColumn("dbo.Reports", "User_Id");
        }
    }
}
