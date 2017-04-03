namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "AccountNumber", c => c.Int(nullable: false));
            AddColumn("dbo.BankAccounts", "AccountType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "AccountType");
            DropColumn("dbo.BankAccounts", "AccountNumber");
        }
    }
}
