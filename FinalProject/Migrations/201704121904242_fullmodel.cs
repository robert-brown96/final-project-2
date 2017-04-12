namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payee_PayeeID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Payees", t => t.Payee_PayeeID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Payee_PayeeID)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.StockPortfolios", "StockBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "StockType", c => c.Int(nullable: false));
            AlterColumn("dbo.Payees", "PayeeType", c => c.String(nullable: false));
            AlterColumn("dbo.Payees", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Payments", "Payee_PayeeID", "dbo.Payees");
            DropIndex("dbo.Payments", new[] { "User_Id" });
            DropIndex("dbo.Payments", new[] { "Payee_PayeeID" });
            AlterColumn("dbo.Payees", "Address", c => c.String());
            AlterColumn("dbo.Payees", "PayeeType", c => c.String());
            DropColumn("dbo.Stocks", "StockType");
            DropColumn("dbo.StockPortfolios", "StockBalance");
            DropTable("dbo.Payments");
        }
    }
}
