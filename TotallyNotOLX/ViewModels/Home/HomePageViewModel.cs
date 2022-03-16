using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.ViewModels.Home
{
    public class HomePageViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> PopularSearches { get; set; }
        public int ProductsCount { get; set; }
    }
}
