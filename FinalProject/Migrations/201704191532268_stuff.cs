namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stuff : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StockPortfolios", "StockBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockPortfolios", "StockBalance", c => c.Double(nullable: false));
        }
    }
}
