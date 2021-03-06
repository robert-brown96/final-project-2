namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FinalProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalProject.DAL.AppDbContext>
    {
        public Configuration()
        {
            

            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalProject.DAL.AppDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddPayees.AddAllPayees(db);
            SeedIdentity.SeedUsers(db);

            // attempt to seed accounts with csv connection
            
        }
    }
}
