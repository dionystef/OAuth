namespace oAuth.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<oAuth.Model.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(oAuth.Model.MyDbContext context)
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
            var userManager = new UserManager<MyUser>(new UserStore<MyUser>(context));
            var userRole = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!userRole.RoleExists("user"))
            {
                userRole.Create(new IdentityRole("users"));
            }

            var user = new MyUser { UserName = "toto" };
            var userResult = userManager.Create(user, "123456");

            if (userResult.Succeeded)
            {
                userManager.AddToRole<MyUser,string>(user.Id, "user");
            }

            context.Todoes.AddOrUpdate(
                new Todo { Description = "todo1", Status="todo", Title="todo1"},
                new Todo { Description = "todo2", Status = "todo", Title = "todo2" },
                new Todo { Description = "todo3", Status = "todo", Title = "todo3" },
                new Todo { Description = "todo4", Status = "todo", Title = "todo4" },
                new Todo { Description = "todo5", Status = "todo", Title = "todo5" }
            );
        }
    }
}
