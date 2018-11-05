using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using CoinFlip.Models;

namespace Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Coinflip> Coinflips { get; set; }
        public IDbSet<Tax> Taxes { get; set; }
        public IDbSet<Asset> Assets { get; set; }

        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}