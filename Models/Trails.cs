using System.ComponentModel.DataAnnotations;
namespace connectingToDBTESTING.Models
{
    public class Trail : BaseEntity
    {   
        [Key]
        public long id { get; set; }
        
        [Required(ErrorMessage = "Please enter the trail's name.")]
        [StringLength(16, ErrorMessage = "Trail name must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Trail:")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Trail name")]
        public string name { get; set; }

        [Range(0, 999.99, ErrorMessage = "Trail's length is too long")]
        [Display(Name = "Length:")]
        [Required(ErrorMessage = "Please enter the trail's length.")]
        public decimal? length { get; set; }

        [Range(0.1, 99999.9, ErrorMessage = "Trail's elevation is too high")]
        [Display(Name = "Elevation:")]
        [Required(ErrorMessage = "Please enter the trail's elevation.")]
        public decimal? elevation { get; set; }
        
        [Required(ErrorMessage = "latitude is required.")]
        [Display(Name = "Lat")]
        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Input decimal numbers for latitude.")]
         public float? latitude { get; set; }

        [Range(float.MinValue, float.MaxValue, ErrorMessage = "Input decimal numbers for longitude.")]
        [Display(Name = "Long")]
        [Required(ErrorMessage = "longitude is required.")]
        public float? longitude { get; set; }
        
        [Required(ErrorMessage = "Please enter the trail's description.")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Trail's description must be at least 10 characters")]
        [Display(Name = "Description:")]
        public string description { get; set; }
    }
}