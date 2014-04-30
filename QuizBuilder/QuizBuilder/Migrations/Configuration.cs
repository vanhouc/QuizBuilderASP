namespace QuizBuilder.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QuizBuilder.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizBuilder.DataContexts.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(QuizBuilder.DataContexts.IdentityDb context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!roleManager.RoleExists("admin"))
            {
                roleManager.Create(new IdentityRole { Name = "admin" });
            }
            if (userManager.FindByName("vanhouc") == null)
            {
                var user = new ApplicationUser { UserName = "vanhouc" };
                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "admin");
            }
        }
    }
}
