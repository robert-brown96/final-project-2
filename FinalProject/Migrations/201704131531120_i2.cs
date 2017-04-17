namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class i2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StockPortfolios", "Balance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockPortfolios", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
