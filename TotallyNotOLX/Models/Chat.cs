using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        public string FirstUserId { get; set; }
        public ApplicationUser FirstUser { get; set; }

        public string SecondUserId { get; set; }
        public ApplicationUser SecondUser { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

        public IEnumerable<Messages> Messages { get;set; }
    }
}
