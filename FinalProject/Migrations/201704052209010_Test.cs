namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Transactions");
            AddColumn("dbo.Transactions", "TransactionID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Transactions", "TransactionID");
            DropColumn("dbo.Transactions", "SavingTransactionsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "SavingTransactionsID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Transactions");
            DropColumn("dbo.Transactions", "TransactionID");
            AddPrimaryKey("dbo.Transactions", "SavingTransactionsID");
        }
    }
}
