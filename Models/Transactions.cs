using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace connectingToDBTESTING.Models
{
    public class Transaction : BaseEntity
    {   

        [Key]
        public long TransactionId { get; set; }
        public int Activity { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}