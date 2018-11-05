using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System;

namespace CoinFlip.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Created = DateTime.Now;
        }

        public string TradeUrl { get; set; }

        public double LuckPercentage { get; set; }

        public DateTime Created { get; set; }

        public string AvatarLink { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        
            return userIdentity;
        }
    }
}
