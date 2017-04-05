namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class error : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "AccountType", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccounts", "AccountType", c => c.Int(nullable: false));
        }
    }
}
