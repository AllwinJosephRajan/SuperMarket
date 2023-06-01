using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Models
{
    public class SMarketModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Categories { get; set; }
        [Required]
        public int PricingAndDiscounts { get; set; }
        public string AvailableOffers { get; set; }
        [Required]
        [MaxLength(100)]
        public string ReviewAndRatings { get; set; }

    }
}
