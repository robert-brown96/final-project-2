namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nw : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StockPortfolios", new[] { "User_Id" });
            AlterColumn("dbo.StockPortfolios", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.StockPortfolios", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StockPortfolios", new[] { "User_Id" });
            AlterColumn("dbo.StockPortfolios", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.StockPortfolios", "User_Id");
        }
    }
}
