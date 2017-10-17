using System;
using System.ComponentModel.DataAnnotations;
namespace connectingToDBTESTING.Models
{
    public class Review: BaseEntity
    {   
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the your name.")]
        [StringLength(16, ErrorMessage = "Name must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Reviewer Name:")]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Reviewer Name")]
        public string RevName { get; set; }

        [Required(ErrorMessage = "Please enter Restaurant Name.")]
        [StringLength(16, ErrorMessage = "Restaurant Name must be between 3 and 16 characters", MinimumLength = 3)]
        [Display(Name = "Restaurant Name:")]
        public string ResName { get; set; }

        [Required(ErrorMessage = "Please enter Restaurant review.")]
        [StringLength(255, ErrorMessage = "Restaurant review must be at least 11 characters", MinimumLength = 11)]
        [Display(Name = "Restaurant review:")]
        public string Rev { get; set; }

        [Required(ErrorMessage = "Please enter Restaurant stars.")]
        [Range(0, 6, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Restaurant stars:")]
        public int Stars { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [MyDate(ErrorMessage ="Invalid date")]
        public DateTime? Date { get; set; }
        // [Key]
        // public int UserId { get; set; }
        
        // [Required(ErrorMessage = "Please enter the user's first name.")]
        // [StringLength(16, ErrorMessage = "First name must be between 3 and 16 characters", MinimumLength = 3)]
        // [Display(Name = "First Name:")]
        // [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid First Name")]
        // public string Name { get; set; }

        // // [Required(ErrorMessage = "Please enter the user's last name.")]
        // // [StringLength(16, ErrorMessage = "Last Name Must be between 3 and 16 characters", MinimumLength = 3)]
        // // [Display(Name = "Last Name:")]
        // // [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Invalid Last Name")]
        // // public string LastName { get; set; }
        
        // [EmailAddress(ErrorMessage = "The Email Address is not valid")]
        // [Required(ErrorMessage = "Please enter an email address.")]
        // [Display(Name = "Email Address:")]
        // [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Wrong email format")]
        // public string Email { get; set; }

        // [Required(ErrorMessage = "Please enter a password.")]
        // [DataType(DataType.Password)]
        // [MinLength(8, ErrorMessage = "The Password must be at least 8 characters.")]
        // public string Password { get; set; }

        // // [Required(ErrorMessage = "Please enter a confimation password.")]
        // // [DataType(DataType.Password)]
        // // [MinLength(8, ErrorMessage = "The Confirm password must be at least 8 characters.")]
        // // [Compare("Password", ErrorMessage = "Password andx Confirmation don't match")]
        // // public string ConPassword { get; set; }
        // [Required(ErrorMessage = "Invalid age.")]
        // public int Age { get; set; }
    }
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now;

        }
}   
}