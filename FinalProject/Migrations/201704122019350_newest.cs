namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Description");
        }
    }
}
