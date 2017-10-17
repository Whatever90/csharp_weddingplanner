using System.ComponentModel.DataAnnotations;
namespace connectingToDBTESTING.Models
{
    public class Ninja : BaseEntity
    {   
        [Key]
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Please enter the ninja's name.")]
        [StringLength(16, ErrorMessage = "Name must be between 1 and 16 characters", MinimumLength = 1)]
        [Display(Name = "Name:")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter ninja's level.")]
        [Range(0, 12, ErrorMessage = "Invalid level")]
        [Display(Name = "Ninja:")]
        public int level { get; set; }

        public Dojo dojo { get; set; }
        public int dojo_id { get; set; }

        [Display(Name = "Description:")]
        public string description { get; set; }
    }
}