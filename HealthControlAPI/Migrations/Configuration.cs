namespace HealthControlAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HealthControlAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HealthControlAPI.Models.HealthControlAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HealthControlAPI.Models.HealthControlAPIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Users.AddOrUpdate(x => x.Id,
        new User()
        {
            Id = 1,
            Nome = "João",
            Senha = "1234"
        },
        new User()
        {
            Id = 2,
            Nome = "Maria",
            Senha = "1234"

        },
        new User()
        {
            Id = 3,
            Nome = "Carlos",
            Senha = "1234"

        },
        new User()
        {
            Id = 4,
            Nome = "Diego",
            Senha = "1234"

        }
        );
        }
    }
}
