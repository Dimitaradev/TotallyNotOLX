using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TotallyNotOLX.Models
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The properties of the ApplicationUser table from our database.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ApplicationUsers_SavedProducts> Saved { get; set; }
    }
}
