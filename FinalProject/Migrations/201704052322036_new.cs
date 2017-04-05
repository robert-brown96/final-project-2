namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payees",
                c => new
                    {
                        PayeeID = c.Int(nullable: false, identity: true),
                        PayeeType = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.PayeeID);
            
            CreateTable(
                "dbo.StockPortfolios",
                c => new
                    {
                        StockPortfolioID = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        CashBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StockPortfolioID);
            
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
                .PrimaryKey(t => t.StockTransactionID)
                .ForeignKey("dbo.StockPortfolios", t => t.Portfolio_StockPortfolioID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Portfolio_StockPortfolioID)
                .Index(t => t.Stock_StockID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Ticker = c.String(),
                        Name = c.String(),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StockID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockTransactions", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockTransactions", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropIndex("dbo.StockTransactions", new[] { "Stock_StockID" });
            DropIndex("dbo.StockTransactions", new[] { "Portfolio_StockPortfolioID" });
            DropTable("dbo.Stocks");
            DropTable("dbo.StockTransactions");
            DropTable("dbo.StockPortfolios");
            DropTable("dbo.Payees");
        }
    }
}
