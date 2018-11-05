using System;
using System.ComponentModel.DataAnnotations;


namespace CoinFlip.Models
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TaxPercentage { get; set; }
    }
}