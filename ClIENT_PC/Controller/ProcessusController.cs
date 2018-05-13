using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC.Controller
{
    class ProcessusController : ControllerBase
    {
        public string Processus()
        {
            List<object> ListeProcessus = new List<object> { };
            string prcsDescription = null;
            try
            {
                Process[] listePrcs = Process.GetProcesses();
                foreach (Process prcs in listePrcs)
                {
                    try
                    {
                        prcsDescription = FileVersionInfo.GetVersionInfo(prcs.MainModule.FileName).FileDescription;
                    }
                    catch { prcsDescription = ""; }

                    ListeProcessus.Add(new { name = prcs.ProcessName, memory = prcs.PrivateMemorySize64, description = prcsDescription });
                }
            }
            catch { };
            return serializer.Serialize(new { process = ListeProcessus });
        }

        public void Demmarer(string process_name)
        {
            try
            {
                Process.Start(process_name);
            }
            catch { }
        }

        public void Tuer(string process_name)
        {
            try
            {
                var prcs = Process.GetProcesses().Where(pr => pr.ProcessName == process_name);
                foreach (var process in prcs)
                {
                    process.Kill();
                }
            }
            catch { }
        }
    }
}
