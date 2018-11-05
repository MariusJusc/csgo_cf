using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinFlip.Main.Models
{
    public class CoinflipViewModel
    {
        public int Id { get; set; }

        public string CoinflipGameId { get; set; }

        public string User1_SteamId { get; set; }

        public string User1_Username { get; set; }

        public string User1_IconUrl { get; set; }

        public double User1_Value { get; set; }

        public string User2_SteamId { get; set; }

        public string User2_Username { get; set; }

        public string User2_IconUrl { get; set; }

        public double User2_Value { get; set; }

        public double TotalValue { get; set; }

        public int Status { get; set; }

        public string Created { get; set; }

        public double Tax { get; set; }

        public List<WeaponWebViewModel> Player1Assets { get; set; }

        public List<WeaponWebViewModel> Player2Assets { get; set; }
    }
}