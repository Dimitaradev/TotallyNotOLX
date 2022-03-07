using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotallyNotOLX.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string SellerId { get; set; }
        [Required]
        public bool Sold { get; set; }
        [Required]
        public string DatePosted { get; set; }
        public decimal Price { get; set; }
        public bool PriceNegotiable { get; set; }
        public int CategoryId { get; set; }
        public string Condition { get; set; }
        public string Notes { get; set; }
        public string ProductImage { get; set; }
        [NotMapped]
        public bool SavedByUser { get; set; }
        [NotMapped]
        public bool CreatedByUser { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public ApplicationUser Seller { get; set; }
        public IEnumerable<ApplicationUsers_SavedProducts> SavedBy { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public Category Category { get; set; }
    }
}
