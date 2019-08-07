namespace WebBank.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebBank.Models;
    using WebBank.Services;

    internal sealed class Configuration : DbMigrationsConfiguration<WebBank.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebBank.Models.ApplicationDbContext";
        }

        protected override void Seed(WebBank.Models.ApplicationDbContext context)
        {
            //called after every migration
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
             if(!context.Users.Any(t => t.UserName == "admin@webbank.com"))
            {
                var user = new ApplicationUser {UserName = "admin@webbank.com", Email = "admin@webbank.com" };
                userManager.Create(user, "Admin123!");

                //add user to checking accounts
                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin", "user", user.Id, 1000);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
