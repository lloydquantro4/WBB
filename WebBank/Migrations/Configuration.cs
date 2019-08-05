namespace WebBank.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebBank.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebBank.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WebBank.Models.ApplicationDbContext";
        }

        protected override void Seed(WebBank.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
             if(!context.Users.Any(t => t.UserName == "admin@webbank.com"))
            {
                var user = new ApplicationUser {UserName = "admin@webbank.com", Email = "admin@webbank.com", Id = "myadmin" };
                userManager.Create(user, "Admin123!");
            
                //add user to checking accounts
                var db = new ApplicationDbContext();
                var accountNum = (123456 + db.CheckingAccounts.Count()).ToString().PadLeft(10, '0');
                var checkingAccountNew = new CheckingAccount { FirstName = "Admin", LastName = "User", AccountNumber = accountNum, Balance = 1000, ApplicationUserId = user.Id };
                db.CheckingAccounts.Add(checkingAccountNew);
                db.SaveChanges();

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
