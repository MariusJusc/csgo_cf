using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SteamAPI;
using SteamKit2;
using Microsoft.AspNet.Identity;
using CoinFlip.Main.Models;
using System.Threading.Tasks;
using SteamAPI.SteamModels;
using CoinFlip.Models;
using Microsoft.AspNet.Identity.Owin;

namespace CoinFlip.Main.Controllers
{
    public class HomeController : BaseController
    {
        private SteamWeb steamWeb = new SteamWeb();
        private UserRetriever userRetriever;

        public HomeController()
        {
            userRetriever = new UserRetriever(steamWeb);
        }

        public ActionResult Index()
        {
            LoadUserAvatar();
            return View();
        }

        public void LoadUserAvatar()
        {
            var steamId = User.Identity.GetUserId();

            if (!String.IsNullOrEmpty(steamId))
            {
                ViewBag.LoadUsetAvatar = userRetriever.SteamUser(steamId).Avatar;
            }
        }
    }
}