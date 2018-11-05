using System;
using System.ComponentModel.DataAnnotations;
using SteamKit2;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinFlip.Models
{
    public class Coinflip
    {
        public Coinflip()
        {
            CoinflipGuidId = Guid.NewGuid();
            Created = DateTime.Today;
            User2Value = 0.0;
        }

        [Key]
        public int Id { get; set; }

        public Guid CoinflipGuidId { get; set; }

        public string User1_SteamId { get; set; }

        public string User2_SteamId { get; set; }

        public double User1Value { get; set; }

        public double User2Value { get; set; }

        public Status Status { get; set; }

        public DateTime Created { get; set; }

        public Tax Tax { get; set; }  
    }

    public enum Status
    {
        Pending = 0,
        Joined = 1,
        Complete = 2
    }
}
