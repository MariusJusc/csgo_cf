using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinFlip.Main.Models
{
    public class SteamAccountViewModel
    {
        public string SteamId { get; set; }

        public string PersonaName { get; set; }

        public string ProfileUrl { get; set; }

        public string Avatar { get; set; }

        public string AvatarMedium { get; set; }

        public string AvatarFull { get; set; }

        public string TradeUrl { get; set; }
    }
}