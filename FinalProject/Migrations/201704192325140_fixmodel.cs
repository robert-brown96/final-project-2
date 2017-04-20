namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payees", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Deposits", "TransactionNumber");
            DropColumn("dbo.Withdraws", "TransactionNumber");
            DropColumn("dbo.Transfers", "TransactionNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transfers", "TransactionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Withdraws", "TransactionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Deposits", "TransactionNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Payees", "Date");
        }
    }
}
