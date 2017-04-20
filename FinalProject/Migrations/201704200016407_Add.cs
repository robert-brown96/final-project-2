namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deposits", "TransactionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Withdraws", "TransactionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Transfers", "TransactionNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Payees", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payees", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Transfers", "TransactionNumber");
            DropColumn("dbo.Withdraws", "TransactionNumber");
            DropColumn("dbo.Deposits", "TransactionNumber");
        }
    }
}
