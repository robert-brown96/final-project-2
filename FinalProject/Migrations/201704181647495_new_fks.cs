namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_fks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deposits", "Portfolio_StockPortfolioID", c => c.Int());
            AddColumn("dbo.Withdraws", "Portfolio_StockPortfolioID", c => c.Int());
            CreateIndex("dbo.Deposits", "Portfolio_StockPortfolioID");
            CreateIndex("dbo.Withdraws", "Portfolio_StockPortfolioID");
            AddForeignKey("dbo.Deposits", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
            AddForeignKey("dbo.Withdraws", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Withdraws", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropForeignKey("dbo.Deposits", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropIndex("dbo.Withdraws", new[] { "Portfolio_StockPortfolioID" });
            DropIndex("dbo.Deposits", new[] { "Portfolio_StockPortfolioID" });
            DropColumn("dbo.Withdraws", "Portfolio_StockPortfolioID");
            DropColumn("dbo.Deposits", "Portfolio_StockPortfolioID");
        }
    }
}
