using CoinFlip.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace CoinFlip.Main.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string ProfilePictureUrl(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
        {
            if(user.Identity.IsAuthenticated)
            {
                var appUser = userManager.FindByIdAsync(user.Identity.GetUserId()).Result;
                return appUser.AvatarLink;
            }

            return "";
        }
    }
}