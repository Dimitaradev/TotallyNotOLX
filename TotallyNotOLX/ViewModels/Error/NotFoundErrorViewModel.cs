using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.ViewModels.Error
{
    public class NotFoundErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }
    }
}
