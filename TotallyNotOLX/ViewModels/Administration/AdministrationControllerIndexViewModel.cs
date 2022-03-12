using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.ViewModels.Administration
{
    public class AdministrationControllerIndexViewModel
    {
        public List<ControlledUser> Users { get; set; }
        public List<ControlledProduct> Products { get; set; }
    }
}
