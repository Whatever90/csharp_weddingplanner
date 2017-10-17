using System.ComponentModel.DataAnnotations;
namespace connectingToDBTESTING.Models
{
    public class Message : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [DataType(DataType.Date)]
        public long created_at { get; set; }

        [Required(ErrorMessage = "Please enter anything.")]
        [StringLength(225, ErrorMessage = "Message must be between 3 and 225 characters", MinimumLength = 3)]
        [Display(Name = "Message:")]
        public string text { get; set; }

        [Required(ErrorMessage = "Where is you goddam id?")]
        [Display(Name = "user_id:")]
        public int user_id { get; set; }

    }
}