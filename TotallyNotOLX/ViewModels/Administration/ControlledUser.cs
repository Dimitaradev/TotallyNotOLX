using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotallyNotOLX.ViewModels.Administration
{
    public class ControlledUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
