using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.Models
{
    public class ApplicationUsers_SavedProducts
    {
        [Key]
        public int UserID { get; set; }
        public ApplicationUser User { get; set; }

        [Key]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
