namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Identity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "State", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false));
        }
    }
}
