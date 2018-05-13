using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC.Controller
{
    class ProgrammesController : ControllerBase
    {
        public string Programmes()
        {
            List<object> ListeProgramme = new List<object> { };
            string SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey RegCleA = Registry.LocalMachine.OpenSubKey(SoftwareKey))
            {
                foreach (string skName in RegCleA.GetSubKeyNames())
                {
                    using (RegistryKey RegCleB = RegCleA.OpenSubKey(skName))
                    {
                        try
                        {
                            if (RegCleB.GetValue("InstallLocation") != null && RegCleB.GetValue("DisplayName") != null)
                            {
                                ListeProgramme.Add(new
                                {
                                    name = RegCleB.GetValue("DisplayName"),
                                    version = RegCleB.GetValue("DisplayVersion"),
                                    publisher = RegCleB.GetValue("Publisher"),
                                    uninstall = RegCleB.GetValue("UninstallString")
                                });
                            }
                        }
                        catch { }
                    }
                }
            }
            return serializer.Serialize(new { programs = ListeProgramme });
        }

        private void Desinstaller(string name)
        {
            
        }
    }
}
