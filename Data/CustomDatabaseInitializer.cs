using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SteamAPI;
using CoinFlip.Models;
using System.Data.Entity;

namespace Data
{
    public class CustomDatabaseInitializer : DbMigrationsConfiguration<AppDbContext>
    {

        public CustomDatabaseInitializer()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;

            //Database.SetInitializer<AppDbContext>(new DropCreateDatabaseAlways<AppDbContext>());
            new DropCreateDatabaseAlways<AppDbContext>();
        }

        protected override void Seed(AppDbContext context)
        {

            //var genInventory = new GenericInventory();
           /* var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var userRetriever = new UserRetriever();
            var steamOptions = new SteamOptions();
            var steamUser = userRetriever.SteamUser(steamOptions.getApiKey(), defaultAdminSteamId);

            if(!roleManager.RoleExists("Admin"))
            {
                // Creating an Admin role
                var role = new IdentityRole() { Name = "Admin" };

                // Inserting Admin Role
                roleManager.Create(role);

                var user = new ApplicationUser { Id = steamUser.Result.SteamId, UserName = "Admin", Email = "123@123.com" };

                var chkUser = userManager.Create(user, "123");

                if(chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }*/

            // Create new Tax
            Tax tax = new Tax() { Id = 1, Name = "Casual", TaxPercentage = 30 };

            // Insert Tax
            context.Taxes.Add(tax);

            // Save incoming changes
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
