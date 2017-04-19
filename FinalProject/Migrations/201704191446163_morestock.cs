namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morestock : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.StockSellID)
                .ForeignKey("dbo.StockPortfolios", t => t.Portfolio_StockPortfolioID)
                .ForeignKey("dbo.Stocks", t => t.Stock_StockID)
                .Index(t => t.Portfolio_StockPortfolioID)
                .Index(t => t.Stock_StockID);
            
            AddColumn("dbo.StockPortfolios", "StockBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockSells", "Stock_StockID", "dbo.Stocks");
            DropForeignKey("dbo.StockSells", "Portfolio_StockPortfolioID", "dbo.StockPortfolios");
            DropIndex("dbo.StockSells", new[] { "Stock_StockID" });
            DropIndex("dbo.StockSells", new[] { "Portfolio_StockPortfolioID" });
            DropColumn("dbo.StockPortfolios", "StockBalance");
            DropTable("dbo.StockSells");
        }
    }
}
