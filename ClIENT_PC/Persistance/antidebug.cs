using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Persistance
{
    class antidebug
    {
        public static bool CheckParentProcess()
        {
            if (Parametre.AntiDeBug)
            {
                using (ManagementObject mO = new ManagementObject("win32_process.handle='" + Process.GetCurrentProcess().Id.ToString() + "'"))
                {
                    mO.Get();
                    if (Process.GetProcessById(Convert.ToInt32(mO["ParentProcessId"])).ProcessName.ToLower() != "explorer")
                        return false;
                    return true;
                }
            }
            return true;
        }
    }
}
