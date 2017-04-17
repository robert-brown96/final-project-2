namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtransactionmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        DepositID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Comments = c.String(),
                        Account_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.DepositID)
                .ForeignKey("dbo.BankAccounts", t => t.Account_BankAccountID)
                .Index(t => t.Account_BankAccountID);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        TransferID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Comments = c.String(),
                        transferFrom_BankAccountID = c.Int(),
                        transferTo_BankAccountID = c.Int(),
                        BankAccount_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.TransferID)
                .ForeignKey("dbo.BankAccounts", t => t.transferFrom_BankAccountID)
                .ForeignKey("dbo.BankAccounts", t => t.transferTo_BankAccountID)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_BankAccountID)
                .Index(t => t.transferFrom_BankAccountID)
                .Index(t => t.transferTo_BankAccountID)
                .Index(t => t.BankAccount_BankAccountID);
            
            CreateTable(
                "dbo.Withdraws",
                c => new
                    {
                        WithdrawID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Comments = c.String(),
                        Account_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.WithdrawID)
                .ForeignKey("dbo.BankAccounts", t => t.Account_BankAccountID)
                .Index(t => t.Account_BankAccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Withdraws", "Account_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transfers", "BankAccount_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transfers", "transferTo_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Transfers", "transferFrom_BankAccountID", "dbo.BankAccounts");
            DropForeignKey("dbo.Deposits", "Account_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Withdraws", new[] { "Account_BankAccountID" });
            DropIndex("dbo.Transfers", new[] { "BankAccount_BankAccountID" });
            DropIndex("dbo.Transfers", new[] { "transferTo_BankAccountID" });
            DropIndex("dbo.Transfers", new[] { "transferFrom_BankAccountID" });
            DropIndex("dbo.Deposits", new[] { "Account_BankAccountID" });
            DropTable("dbo.Withdraws");
            DropTable("dbo.Transfers");
            DropTable("dbo.Deposits");
        }
    }
}
