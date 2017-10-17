using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace connectingToDBTESTING.Models
{
    public class Dojo : BaseEntity
    {   
        public Dojo() {
            ninjas = new List<Ninja>();
        }
        [Key]
        public long id { get; set; }
        
        [Required(ErrorMessage = "Please enter the dojo's name.")]
        [StringLength(16, ErrorMessage = "Dojo name must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Name:")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Dojo name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter the dojo's location.")]
        [StringLength(16, ErrorMessage = "Dojo location must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Location:")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Dojo location")]
        public string location { get; set; }

        [Display(Name = "Description:")]
        public string description { get; set; }
        public ICollection<Ninja> ninjas { get; set; }
    }
}