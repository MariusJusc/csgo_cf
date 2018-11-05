using SteamKit2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinFlip.Models
{
    public class Asset
    {
        [Key]
        public long Id { get; set; }

        public string AssetId { get; set; }

        public string UserSteamId { get; set; }

        public string BotSteamId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CoinflipId { get; set; }
    }
}
