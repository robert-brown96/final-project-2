namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doubles : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transfers", name: "Account_BankAccountID", newName: "BankAccount_BankAccountID");
            RenameIndex(table: "dbo.Transfers", name: "IX_Account_BankAccountID", newName: "IX_BankAccount_BankAccountID");
            AddColumn("dbo.Stocks", "Symbol", c => c.String());
            AddColumn("dbo.Transfers", "transferFrom_BankAccountID", c => c.Int());
            AddColumn("dbo.Transfers", "transferTo_BankAccountID", c => c.Int());
            AlterColumn("dbo.StockPortfolios", "CashBalance", c => c.Double(nullable: false));
            AlterColumn("dbo.StockPortfolios", "InitialDeposit", c => c.Double(nullable: false));
            AlterColumn("dbo.Stocks", "Fee", c => c.Double(nullable: false));
            CreateIndex("dbo.Transfers", "transferFrom_BankAccountID");
            CreateIndex("dbo.Transfers", "transferTo_BankAccountID");
            AddForeignKey("dbo.Transfers", "transferFrom_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            AddForeignKey("dbo.Transfers", "transferTo_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            DropColumn("dbo.StockPortfolios", "StockBalance");
            DropColumn("dbo.Stocks", "Ticker");
            DropColumn("dbo.Stocks", "CurrentPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "Ticker", c => c.String());
            AddColumn("dbo.StockPortfolios", "StockBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Transfers", "transferTo_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transfers", "transferFrom_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transfers", new[] { "transferTo_BankAccountID" });
            DropIndex("dbo.Transfers", new[] { "transferFrom_BankAccountID" });
            AlterColumn("dbo.Stocks", "Fee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockPortfolios", "InitialDeposit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StockPortfolios", "CashBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Transfers", "transferTo_BankAccountID");
            DropColumn("dbo.Transfers", "transferFrom_BankAccountID");
            DropColumn("dbo.Stocks", "Symbol");
            RenameIndex(table: "dbo.Transfers", name: "IX_BankAccount_BankAccountID", newName: "IX_Account_BankAccountID");
            RenameColumn(table: "dbo.Transfers", name: "BankAccount_BankAccountID", newName: "Account_BankAccountID");
        }
    }
}
