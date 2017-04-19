namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockTransactions", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropForeignKey("dbo.StockTransactions", "Stock_StockID", "dbo.Stocks");
            DropIndex("dbo.StockTransactions", new[] { "Portfolio_StockPortfolioID" });
            DropIndex("dbo.StockTransactions", new[] { "Stock_StockID" });
            CreateTable(
                "dbo.StockPurchases",
                c => new
                    {
                        StockPurchaseID = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_StockPortfolioID = c.Int(),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockPurchaseID)
                .ForeignKey("dbo.StockPortfolios", t => t.Portfolio_StockPortfolioID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Portfolio_StockPortfolioID)
                .Index(t => t.Stock_StockID);
            
            DropTable("dbo.StockTransactions");
        }
        
        public override void Down()
        {
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
            
            DropForeignKey("dbo.StockPurchases", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockPurchases", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropIndex("dbo.StockPurchases", new[] { "Stock_StockID" });
            DropIndex("dbo.StockPurchases", new[] { "Portfolio_StockPortfolioID" });
            DropTable("dbo.StockPurchases");
            CreateIndex("dbo.StockTransactions", "Stock_StockID");
            CreateIndex("dbo.StockTransactions", "Portfolio_StockPortfolioID");
            AddForeignKey("dbo.StockTransactions", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.StockTransactions", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
        }
    }
}
