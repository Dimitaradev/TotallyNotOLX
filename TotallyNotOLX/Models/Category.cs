using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public int ItemsInCategory { get; set; }
        public IEnumerable<Product> Items { get; set; }
    }
}
