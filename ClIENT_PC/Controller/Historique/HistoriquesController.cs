using ClIENT_PC.Controller.Communacation;
using ClIENT_PC.Controller.Historique;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller
{
    class HistoriquesController : ControllerBase
    {
        public string Historiques()
        {
            ChromeHistorique CH = new ChromeHistorique();
            FirefoxHistorique FH = new FirefoxHistorique();
            return Encode(new { chrome = CH.liste(), firefox = FH.liste() });
        }
    }
}
