namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _double : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "Symbol", c => c.String());
            AlterColumn("dbo.StockPortfolios", "CashBalance", c => c.Double(nullable: false));
            AlterColumn("dbo.StockPortfolios", "InitialDeposit", c => c.Double(nullable: false));
            AlterColumn("dbo.Stocks", "Fee", c => c.Double(nullable: false));
            DropColumn("dbo.StockPortfolios", "StockBalance");
            DropColumn("dbo.Stocks", "Ticker");
            DropColumn("dbo.Stocks", "CurrentPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "Ticker", c => c.String());
            AddColumn("dbo.StockPortfolios", "StockBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Stocks", "Fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockPortfolios", "InitialDeposit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockPortfolios", "CashBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Stocks", "Symbol");
        }
    }
}
