namespace Cape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Transactions", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Transactions", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Transactions", "Date");
            DropColumn("dbo.Transactions", "Description");
        }
    }
}
