namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wstock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockPortfolios",
                c => new
                    {
                        StockPortfolioID = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        CashBalance = c.Double(nullable: false),
                        Name = c.String(),
                        InitialDeposit = c.Double(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.StockPortfolioID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
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
                        Symbol = c.String(),
                        Name = c.String(),
                        Fee = c.Double(nullable: false),
                        StockType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockID);
            
            AddColumn("dbo.Deposits", "StockPortfolio_StockPortfolioID", c => c.Int());
            AddColumn("dbo.Withdraws", "StockPortfolio_StockPortfolioID", c => c.Int());
            CreateIndex("dbo.Deposits", "StockPortfolio_StockPortfolioID");
            CreateIndex("dbo.Withdraws", "StockPortfolio_StockPortfolioID");
            AddForeignKey("dbo.Deposits", "StockPortfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
            AddForeignKey("dbo.Withdraws", "StockPortfolio_StockPortfolioID", "dbo.StockPortfolios", "StockPortfolioID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Withdraws", "StockPortfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropForeignKey("dbo.StockPortfolios", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StockTransactions", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockTransactions", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropForeignKey("dbo.Deposits", "StockPortfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropIndex("dbo.Withdraws", new[] { "StockPortfolio_StockPortfolioID" });
            DropIndex("dbo.StockTransactions", new[] { "Stock_StockID" });
            DropIndex("dbo.StockTransactions", new[] { "Portfolio_StockPortfolioID" });
            DropIndex("dbo.Deposits", new[] { "StockPortfolio_StockPortfolioID" });
            DropIndex("dbo.StockPortfolios", new[] { "User_Id" });
            DropColumn("dbo.Withdraws", "StockPortfolio_StockPortfolioID");
            DropColumn("dbo.Deposits", "StockPortfolio_StockPortfolioID");
            DropTable("dbo.Stocks");
            DropTable("dbo.StockTransactions");
            DropTable("dbo.StockPortfolios");
        }
    }
}
