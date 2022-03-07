using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.ViewModels.Products
{
    public class CreateProductViewModel
    {
        public Product NewProduct { get; set; }
        public List<Category> Categories { get; set; }
    }
}
