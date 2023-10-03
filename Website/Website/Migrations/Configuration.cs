namespace Website.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Website.DB.AppDBContexts_>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Website.DB.AppDBContexts_";
        }

        protected override void Seed(Website.DB.AppDBContexts_ context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
