namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixpayee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "PayeeName", c => c.String(nullable: false));
            AddColumn("dbo.StockPortfolios", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.StockPortfolios", "User_Id");
            AddForeignKey("dbo.StockPortfolios", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockPortfolios", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.StockPortfolios", new[] { "User_Id" });
            DropColumn("dbo.StockPortfolios", "User_Id");
            DropColumn("dbo.Payees", "PayeeName");
        }
    }
}
