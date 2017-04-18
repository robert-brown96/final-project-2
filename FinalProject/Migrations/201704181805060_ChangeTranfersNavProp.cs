namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTranfersNavProp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transfers", "transferFrom_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transfers", "transferTo_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transfers", new[] { "transferFrom_BankAccountID" });
            DropIndex("dbo.Transfers", new[] { "transferTo_BankAccountID" });
            RenameColumn(table: "dbo.Transfers", name: "BankAccount_BankAccountID", newName: "Account_BankAccountID");
            RenameIndex(table: "dbo.Transfers", name: "IX_BankAccount_BankAccountID", newName: "IX_Account_BankAccountID");
            AddColumn("dbo.StockPortfolios", "InitialDeposit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Transfers", "transferFrom_BankAccountID");
            DropColumn("dbo.Transfers", "transferTo_BankAccountID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transfers", "transferTo_BankAccountID", c => c.Int());
            AddColumn("dbo.Transfers", "transferFrom_BankAccountID", c => c.Int());
            DropColumn("dbo.StockPortfolios", "InitialDeposit");
            RenameIndex(table: "dbo.Transfers", name: "IX_Account_BankAccountID", newName: "IX_BankAccount_BankAccountID");
            RenameColumn(table: "dbo.Transfers", name: "Account_BankAccountID", newName: "BankAccount_BankAccountID");
            CreateIndex("dbo.Transfers", "transferTo_BankAccountID");
            CreateIndex("dbo.Transfers", "transferFrom_BankAccountID");
            AddForeignKey("dbo.Transfers", "transferTo_BankAccountID", "dbo.BankAccounts", "BankAccountID");
            AddForeignKey("dbo.Transfers", "transferFrom_BankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
    }
}
