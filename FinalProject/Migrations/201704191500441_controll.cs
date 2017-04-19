namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class controll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockPurchases", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropForeignKey("dbo.StockPurchases", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockSells", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropForeignKey("dbo.StockSells", "Stock_StockID", "dbo.Stocks");
            DropIndex("dbo.StockPurchases", new[] { "Portfolio_StockPortfolioID" });
            DropIndex("dbo.StockPurchases", new[] { "Stock_StockID" });
            DropIndex("dbo.StockSells", new[] { "Portfolio_StockPortfolioID" });
            DropIndex("dbo.StockSells", new[] { "Stock_StockID" });
            CreateTable(
                "dbo.StockTransactions",
                c => new
                    {
                        StockTransactionID = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Portfolio_StockPortfolioID = c.Int(),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockTransactionID)
                .ForeignKey("dbo.StockPortfolios", t => t.Portfolio_StockPortfolioID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Portfolio_StockPortfolioID)
                .Index(t => t.Stock_StockID);
            
            DropTable("dbo.StockPurchases");
            DropTable("dbo.StockSells");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StockSells",
                c => new
                    {
                        StockSellID = c.Int(nullable: false, identity: true),
                        Shares = c.Int(nullable: false),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_StockPortfolioID = c.Int(),
                        Stock_StockID = c.Int(),
                    })
                .PrimaryKey(t => t.StockSellID);
            
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
                .PrimaryKey(t => t.StockPurchaseID);
            
            DropForeignKey("dbo.StockTransactions", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockTransactions", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropIndex("dbo.StockTransactions", new[] { "Stock_StockID" });
            DropIndex("dbo.StockTransactions", new[] { "Portfolio_StockPortfolioID" });
            DropTable("dbo.StockTransactions");
            CreateIndex("dbo.StockSells", "Stock_StockID");
            CreateIndex("dbo.StockSells", "Portfolio_StockPortfolioID");
            CreateIndex("dbo.StockPurchases", "Stock_StockID");
            CreateIndex("dbo.StockPurchases", "Portfolio_StockPortfolioID");
            AddForeignKey("dbo.StockSells", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.StockSells", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
            AddForeignKey("dbo.StockPurchases", "Stock_StockID", "dbo.Stocks", "StockID");
            AddForeignKey("dbo.StockPurchases", "Portfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
        }
    }
}
