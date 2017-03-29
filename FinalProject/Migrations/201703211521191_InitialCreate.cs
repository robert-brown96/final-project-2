namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Savings",
                c => new
                    {
                        SavingID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SavingID);
            
            CreateTable(
                "dbo.SavingTransactions",
                c => new
                    {
                        SavingTransactionsID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        Savings_SavingID = c.Int(),
                    })
                .PrimaryKey(t => t.SavingTransactionsID)
                .ForeignKey("dbo.Savings", t => t.Savings_SavingID)
                .Index(t => t.Savings_SavingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingTransactions", "Savings_SavingID", "dbo.Savings");
            DropIndex("dbo.SavingTransactions", new[] { "Savings_SavingID" });
            DropTable("dbo.SavingTransactions");
            DropTable("dbo.Savings");
        }
    }
}
