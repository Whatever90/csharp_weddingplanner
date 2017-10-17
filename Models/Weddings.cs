using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace connectingToDBTESTING.Models
{
    public class Wedding: BaseEntity
    {   
        public Wedding()
        {
            Guests = new List<Guest>();
        }
        public class MyDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime d = Convert.ToDateTime(value);
                return d >= DateTime.Now;

            }
        }
        public List<Guest> Guests { get; set; }
        [Key]
        public int WeddingId { get; set; }
        
        [Required(ErrorMessage = "Please enter the first wedder.")]
        [StringLength(16, ErrorMessage = "The first wedder's name must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Wedder One: ")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid First Wedder's name")]
        public string WedderOne { get; set; }

        [Required(ErrorMessage = "Please enter the second wedder.")]
        [StringLength(16, ErrorMessage = "The second wedder's name must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Wedder Two: ")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Second Wedder's name")]
        public string WedderTwo { get; set; }
        
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "The address must be between 3 and 16 characters long", MinimumLength = 3)]
        [Display(Name = "Address: ")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [MyDate(ErrorMessage ="Can't set date in past")]
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
        public int GuestsAmount { get; set; }

        
    }
    
}
