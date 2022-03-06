using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Models;
namespace TotallyNotOLX.ViewModels.Products
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }
        public string SearchType { get; set; }
        public int Page { get; set; }
    }
}
