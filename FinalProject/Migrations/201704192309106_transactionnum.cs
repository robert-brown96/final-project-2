namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deposits", "TransactionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Withdraws", "TransactionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Transfers", "TransactionNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transfers", "TransactionNumber");
            DropColumn("dbo.Withdraws", "TransactionNumber");
            DropColumn("dbo.Deposits", "TransactionNumber");
        }
    }
}
