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
        /// <summary>
        /// The properties of the ApplicationUsers_SavedProducts table from our database.
        /// </summary>
        [Key]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Key]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
