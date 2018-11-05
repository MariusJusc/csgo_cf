using System.Collections.Generic;

namespace CoinFlip.Main.Models
{
    public class TradesViewModel
    {
        public TradesViewModel()
        {
            Coinflips = new HashSet<CoinflipViewModel>();
        }

        public ICollection<CoinflipViewModel> Coinflips { get; set; }
    }
}