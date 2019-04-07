using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SteamAPI;
using Microsoft.AspNet.Identity;
using CoinFlip.Main.Models;
using System.Threading.Tasks;
using SteamAPI.SteamModels;
using CoinFlip.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Routing;
using RazorEngine;
using System.IO;
using RazorEngine.Templating;
using System.Timers;

namespace CoinFlip.Main.Controllers
{
    [RequireHttps]
    [RoutePrefix("coinflip")]
    public class CoinflipController : BaseController
    {
        private SteamWeb steamWeb = new SteamWeb();
        private UserRetriever userRetriever;
        private ItemRootObject inventory = new ItemRootObject();
        private ApplicationUserManager _userManager;

        bool tradeComlpete = true;

        public CoinflipController()
        {
            userRetriever = new UserRetriever(steamWeb);
        }

        public CoinflipController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // Inserts new Coinflip Game and Updates existing game on join.
        [HttpPost]
        [Authorize]
        [Route("begin-coinflip")]
        public ActionResult InsertNewCoinflip(List<string> assetIds, string cfGameId, bool joinGame)
        {
            if (inventory != null)
            {
                if (tradeComlpete == true)
                {
                    var userId = User.Identity.GetUserId();

                    if (joinGame == false)
                    {
                        var cf = new Coinflip()
                        {
                            User1_SteamId = userId,
                            User1Value = 250.5,
                            Status = Status.Pending
                        };

                        if (cf != null)
                        {
                            this.Data.Coinflips.Add(cf);
                            this.Data.SaveChanges();
                            cfGameId = cf.CoinflipGuidId.ToString();
                            InsertAssets(cf.Id, assetIds);
                        }
                    }
                    else
                    { 
                        var cf = this.Data.Coinflips.GetById(GetCoinflipGameByGuid(cfGameId).Id);

                        if(cf != null)
                        {
                            cf.User2_SteamId = userId;
                            cf.Status = Status.Joined;
                            this.Data.SaveChanges();

                            InsertAssets(GetCoinflipGameByGuid(cfGameId).Id, assetIds);
                        }
                    }

                    return Json(new { GameId = cfGameId });
                }
            }

            return new EmptyResult();
        }

        // <summary>
        // Returns all currently active games Pending & Joined.
        // </summary>21
        [HttpGet]
        [Route("coinflip-table")]
        public ActionResult RefreshCoinflipTableGames()
        {
            var coinflips = this.Data.Coinflips.All().Where(x => x.Status == Status.Pending || x.Status == Status.Joined).ToList();
            var result = new List<CoinflipViewModel>();

            if (coinflips != null)
            {
                foreach(var x in coinflips)
                {
                    var steamUser = userRetriever.SteamUser(x.User1_SteamId);

                    if (steamUser != null)
                    {
                        if (String.IsNullOrEmpty(x.User2_SteamId))
                        {
                            var cf = new CoinflipViewModel()
                            {
                                CoinflipGameId = x.CoinflipGuidId.ToString(),
                                User1_SteamId = x.User1_SteamId,
                                User1_IconUrl = steamUser.Avatar,
                                TotalValue = TotalValueCounter(x.User1Value, x.User2Value),
                                Status = (int)x.Status,
                                Player1Assets = ReturnAssetListByCfId(x.Id, x.User1_SteamId).ToList()
                            };

                            result.Add(cf);
                        }
                        else
                        {
                            var oppSteamUser = userRetriever.SteamUser(x.User1_SteamId);

                            var cf = new CoinflipViewModel()
                            {
                                CoinflipGameId = x.CoinflipGuidId.ToString(),
                                User1_SteamId = x.User1_SteamId,
                                User1_IconUrl = oppSteamUser.Avatar,
                                User2_SteamId = x.User2_SteamId,
                                User2_Username = oppSteamUser.PersonaName,
                                User2_IconUrl = oppSteamUser.Avatar,
                                User2_Value = x.User2Value,
                                TotalValue = TotalValueCounter(x.User1Value, x.User2Value),
                                Status = (int)x.Status,
                                Player1Assets = ReturnAssetListByCfId(x.Id, x.User1_SteamId).ToList()
                            };

                            result.Add(cf);
                        }
                    }
                }

                result.Reverse();
            }
            return PartialView("CoinflipTable", new TradesViewModel { Coinflips = result });
        }

        //<summary>
        // Loads the users steam inventory on Create Game.
        //</summary>
        [Route("load-inventory")]
        [Authorize]
        public ActionResult InventoryFetcher()
        {
            var inventory = InitializeInventory(User.Identity.GetUserId());

            if (inventory != null)
            {
                var playersInventoryDescr = inventory.Descriptions.Where(a => a.ClassId != "1923037342").ToList();

                ViewBag.Weapons = playersInventoryDescr.Select(x => new WeaponWebViewModel
                {
                    ClassId = x.ClassId,
                    Name = x.Name,
                    ImageUrl = x.IconUrl,
                    AssetId = inventory.Assets.Find(y => y.ClassId == x.ClassId).Assetid
                });
            }
            return PartialView("_UserInventory");
        }

        //<summary>
        // Loads the Coinflip Game in depth view.
        //</summary>
        [Route("load-coinflip-game")]
        public ActionResult LoadCoinflipGameView(string cfId)
        {
            var model = new CoinflipViewModel();

            if (!String.IsNullOrEmpty(cfId))
            {
                var cfGame = this.Data.Coinflips.All().Where(x => x.CoinflipGuidId.ToString() == cfId).ToList();
                if (cfGame != null)
                {
                    foreach (var x in cfGame)
                    {
                        if (String.IsNullOrEmpty(x.User2_SteamId))
                        {
                            model = new CoinflipViewModel()
                            {
                                User1_SteamId = x.User1_SteamId,
                                User1_Username = userRetriever.SteamUser(x.User1_SteamId).PersonaName,
                                User1_IconUrl = userRetriever.SteamUser(x.User1_SteamId).AvatarFull,
                                TotalValue = TotalValueCounter(x.User1Value, x.User2Value),
                                Status = (int)x.Status,
                                CoinflipGameId = x.CoinflipGuidId.ToString(),
                                Player1Assets = ReturnAssetListByCfId(x.Id, x.User1_SteamId).ToList()
                            };
                        }
                        else
                        {
                            model = new CoinflipViewModel()
                            {
                                User1_SteamId = x.User1_SteamId,
                                User1_Username = userRetriever.SteamUser(x.User1_SteamId).PersonaName,
                                User1_IconUrl = userRetriever.SteamUser(x.User1_SteamId).AvatarFull,
                                User1_Value = x.User1Value,
                                Player1Assets = ReturnAssetListByCfId(x.Id, x.User1_SteamId).ToList(),
                                User2_SteamId = x.User2_SteamId,
                                User2_Username = userRetriever.SteamUser(x.User2_SteamId).PersonaName,
                                User2_IconUrl = userRetriever.SteamUser(x.User2_SteamId).AvatarFull,
                                User2_Value = x.User2Value,
                                Status = (int)x.Status,
                                Created = x.Created.ToString(),
                                Player2Assets = ReturnAssetListByCfId(x.Id, x.User2_SteamId).ToList()
                            };
                        }
                    }
                }
            }
            return PartialView("_CoinflipGame", model);
        }

        // <summary>
        // Gets Coinflip Game by GUID
        // </summary>
        private CoinflipViewModel GetCoinflipGameByGuid(string cfGuid)
        {
            var result = this.Data.Coinflips.All().Where(x => x.CoinflipGuidId.ToString() == cfGuid).ToList();

            if (result.Count != 0)
            {
                CoinflipViewModel model = new CoinflipViewModel();
                try
                {
                    foreach (var x in result)
                    {
                        model = new CoinflipViewModel()
                        {
                            Id = x.Id,
                            CoinflipGameId = x.CoinflipGuidId.ToString(),
                            User1_SteamId = x.User1_SteamId,
                            User1_Username = userRetriever.SteamUser(x.User1_SteamId).PersonaName,
                            User1_IconUrl = userRetriever.SteamUser(x.User1_SteamId).AvatarFull,
                            User1_Value = x.User1Value,
                            User2_SteamId = x.User2_SteamId,
                            User2_Username = userRetriever.SteamUser(x.User2_SteamId).PersonaName,
                            User2_IconUrl = userRetriever.SteamUser(x.User2_SteamId).AvatarFull,
                            User2_Value = x.User2Value,
                            TotalValue = TotalValueCounter(x.User1Value, x.User2Value),
                            Status = (int)x.Status,
                            Created = x.Created.ToString(),
                            //Tax = x.Tax.TaxPercentage,
                            Player1Assets = ReturnAssetListByCfId(x.Id, x.User1_SteamId).ToList()
                        };
                    }
                    return model;
                }
                catch (Exception)
                {
                    foreach (var x in result)
                    {
                        model = new CoinflipViewModel()
                        {
                            Id = x.Id,
                        };
                    }
                    return model;
                }
            }
            return null;
        }
        
        // <summary>
        // Converts Partial View HTML to String and Return for SignalR.
        // </summary>
        public string ReturnViewHTML(string userId, List<string> assetIds)
        {
            var coinflips = this.Data.Coinflips.All().Where(x => x.Status == Status.Pending || x.Status == Status.Joined && x.User1_SteamId == userId).ToList();

            var lastInsertedGuid = coinflips.LastOrDefault();

            var model = new CoinflipViewModel()
            {
                CoinflipGameId = lastInsertedGuid.CoinflipGuidId.ToString(),
                User1_IconUrl = userRetriever.SteamUser(userId).Avatar,
                TotalValue = 250,
                Status = (int)Status.Pending,
                Player1Assets = ReturnAssetObjectList(userId, assetIds).ToList()
            };

            var templateText = System.IO.File.ReadAllText(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"Views\Shared\_CoinflipRow.cshtml"));

            return Engine.Razor.RunCompile(templateText, "templKey", null, model);
        }

        public string ReturnViewHTML(Guid gameId)
        {
            var coinflip = Data.Coinflips.All().Where(x => x.CoinflipGuidId == gameId).FirstOrDefault();

            var assetIds = Data.Assets.All()
                .Where(x => x.CoinflipId == coinflip.Id && x.UserSteamId == coinflip.User1_SteamId)
                .Select(x => x.AssetId)
                .ToList()
                .Select(x => x.ToString())
                .ToList();

            var model = new CoinflipViewModel()
            {
                CoinflipGameId = gameId.ToString(),
                User1_IconUrl = userRetriever.SteamUser(coinflip.User1_SteamId).Avatar,
                User2_IconUrl = userRetriever.SteamUser(coinflip.User2_SteamId).Avatar,
                TotalValue = 250,
                Status = coinflip.Status.GetHashCode(), // TODO: Fetch DB
                Player1Assets = ReturnAssetObjectList(coinflip.User1_SteamId, assetIds).ToList()
            };

            var templateText = System.IO.File.ReadAllText(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"Views\Shared\_CoinflipRow.cshtml"));

            return Engine.Razor.RunCompile(templateText, "templKey", null, model);
        }

        //
        // Inserts the gambling assets into database.
        private void InsertAssets(int cfId, List<string> assetIds)
        {
            if (assetIds.Count != 0)
            {
                var assetList = new List<Asset>();
                var userId = User.Identity.GetUserId();

                foreach (var id in assetIds)
                {
                    var assetObj = GetAssetObject(userId, id);

                    var asset = new Asset()
                    {
                        Id = Convert.ToInt64(id),
                        AssetId = id,
                        UserSteamId = User.Identity.GetUserId(),
                        ImageUrl = assetObj.ImageUrl,
                        Name = assetObj.Name,
                        CoinflipId = cfId
                    };

                    this.Data.Assets.Add(asset);
                    this.Data.SaveChanges();

                    assetList.Add(asset);

                    ViewBag.WeaponBet = assetList.Select(x => new WeaponWebViewModel()
                    {
                        Name = assetObj.Name,
                        ImageUrl = assetObj.ImageUrl
                    });
                }
            }
        }

        //
        // Gets the Asset Object.
        private WeaponWebViewModel GetAssetObject(string userId, string assetId)
        {
            var inventory = InitializeInventory(userId);

            WeaponWebViewModel model = new WeaponWebViewModel();

            if (inventory != null)
            {
                var classId = inventory.Assets.Find(x => x.Assetid == assetId).ClassId;
                var assetDescr = inventory.Descriptions.Find(y => y.ClassId == classId);

                model = new WeaponWebViewModel()
                {
                    ClassId = classId,
                    Name = assetDescr.Name,
                    AssetId = assetId,
                    ImageUrl = assetDescr.IconUrl
                };
            }
            return model;
        }

        //
        // Returns Coinflip asset list by Coinflip ID.
        private IEnumerable<WeaponWebViewModel> ReturnAssetListByCfId(int cfId, string steamId)
        {
            var assets = new List<WeaponWebViewModel>();
            var data = Data.Assets.All().Where(x => x.CoinflipId == cfId && x.UserSteamId == steamId).ToList();

            return assets = data.Select(y => new WeaponWebViewModel
            {
                Name = y.Name,
                ImageUrl = y.ImageUrl
            }).ToList();
        }

        //
        // Returns a list of Asset Object for SignalR View HTML.
        private IEnumerable<WeaponWebViewModel> ReturnAssetObjectList(string userId, List<string> assetIds)
        {
            if (assetIds.Count > 0)
            {
                var assetList = new List<WeaponWebViewModel>();

                foreach (var asset in assetIds)
                {
                    assetList.Add(GetAssetObject(userId, asset));
                }

                return assetList;
            }
            return null;
        }

        //
        // Gets users current inventory.
        private ItemRootObject InitializeInventory(string userId)
        {
            var inv = new Inventory(steamWeb);
            var currentUser = Convert.ToUInt64(userId);
            inventory = inv.FetchInventory(currentUser);

            return inventory;
        }

        public void CountDownTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 10000;
            timer.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            var i = 10;


        }

        private double TotalValueCounter(double u1, double u2)
        {
            return u1 + u2;
        }
    }
}