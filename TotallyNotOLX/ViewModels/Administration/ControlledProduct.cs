using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.ViewModels.Administration
{
    public class ControlledProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsSelected { get; set; }
    }
}
