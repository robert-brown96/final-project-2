namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockPortfolios", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockPortfolios", "Name");
        }
    }
}
