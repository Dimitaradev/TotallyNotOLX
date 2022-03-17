using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.ViewModels.Products
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ProductImage NewProductImage { get; set; }
    }
}
