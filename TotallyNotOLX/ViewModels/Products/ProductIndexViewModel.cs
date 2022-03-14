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
        public List<string> Categories { get; set; }
        public string SearchType { get; set; }
        public int? Page { get; set; }
        public string CategoryChosen { get; set; }
        public string Searched { get; set; }
    }
}
