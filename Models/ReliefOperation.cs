using System.ComponentModel.DataAnnotations;

namespace APPR_PART_1_POE.Models
{
    public class ReliefOperation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project / Operation Name")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Operation Update")]
        public string Update { get; set; } = string.Empty;

        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}