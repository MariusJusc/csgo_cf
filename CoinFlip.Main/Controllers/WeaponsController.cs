using CoinFlip.Main.Models;
using SteamAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoinFlip.Main.Controllers
{
    [RoutePrefix("weapons")]
    public class WeaponsController : Controller
    {
        [Route("user/{id}")]
        // GET: Weapons
        public ActionResult Index(ulong id)
        {
            SteamWeb steamweb = new SteamWeb();

            Inventory inv = new Inventory(steamweb);

            var inven = inv.FetchInventory(id);

            ViewBag.Weapons = inven.Descriptions.Select(x => new WeaponWebViewModel
            {
                ClassId = x.ClassId,
                Name = x.Name
            });

            return View("Home");
        }
    }
}