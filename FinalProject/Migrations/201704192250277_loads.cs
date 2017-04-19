namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loads : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Accounts_BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "Accounts_BankAccountID" });
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "State", c => c.String(nullable: false));
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        type = c.Int(nullable: false),
                        Description = c.String(),
                        Comments = c.String(),
                        Accounts_BankAccountID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            CreateIndex("dbo.Transactions", "Accounts_BankAccountID");
            AddForeignKey("dbo.Transactions", "Accounts_BankAccountID", "dbo.BankAccounts", "BankAccountID");
        }
    }
}
