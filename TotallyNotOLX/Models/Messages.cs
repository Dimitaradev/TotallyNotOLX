using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }
        public Chat ChatId { get; set; }

        public string SenderID { get; set; }
        public ApplicationUser Sender { get; set; }

        public string MessageContent { get; set; }
        public string TimeSent { get; set; }
    }
}
