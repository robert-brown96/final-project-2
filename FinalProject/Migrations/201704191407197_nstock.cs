namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nstock : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deposits", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            
            
            DropForeignKey("dbo.Withdraws", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            
            DropIndex("dbo.Deposits", new[] { "Portfolio_StockPortfolioID" });
           
            DropIndex("dbo.Withdraws", new[] { "Portfolio_StockPortfolioID" });
            DropColumn("dbo.Deposits", "Portfolio_StockPortfolioID");
            DropColumn("dbo.Withdraws", "Portfolio_StockPortfolioID");
            
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Ticker = c.String(),
                        Name = c.String(),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockID);
            
            CreateTable(
                "dbo.StockTransactions",
                c => new
                    {
                        StockTransactionID = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_StockPortfolioID = c.Int(),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockTransactionID);
            
            
            
            AddColumn("dbo.Withdraws", "Portfolio_StockPortfolioID", c => c.Int());
            AddColumn("dbo.Deposits", "Portfolio_StockPortfolioID", c => c.Int());
            CreateIndex("dbo.Withdraws", "Portfolio_StockPortfolioID");
            CreateIndex("dbo.StockTransactions", "Stock_StockID");
            CreateIndex("dbo.StockTransactions", "Portfolio_StockPortfolioID");
            CreateIndex("dbo.Deposits", "Portfolio_StockPortfolioID");
            
            AddForeignKey("dbo.Withdraws", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
            
            AddForeignKey("dbo.StockTransactions", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.StockTransactions", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
            AddForeignKey("dbo.Deposits", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
        }
    }
}
