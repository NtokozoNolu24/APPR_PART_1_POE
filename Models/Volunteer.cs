using System.ComponentModel.DataAnnotations;

namespace APPR_PART_1_POE.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required] //this ensures that the Name field is required and cannot be left empty  to be ably i identify 
        [Display(Name = "Full Names")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Skills { get; set; } = string.Empty;

        [Required]
        public string Availability { get; set; } = string.Empty;
    }
}