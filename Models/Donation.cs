using System.ComponentModel.DataAnnotations;

namespace APPR_PART_1_POE.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a valid donation amount to be able to donate.")]
        public decimal Amount { get; set; }

        [Required]
        public string DonationType { get; set; } = string.Empty;

        [Required]
        public string Currency { get; set; } = string.Empty;

        public bool IsAnonymous { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.Now;// date and time of the donation
    }
}