using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClIENT_PC.Controller.Installation
{
    class Mutuelle
    {
        public static bool VerifierMutex()
        {
            if (Parametre.Mutex_Enable)
            {
                Mutex mutex;
                string MutexString = "";
                if (Parametre.Mutex_MUTEX.Length > 0)
                {
                    MutexString = Parametre.Mutex_MUTEX;
                }
                else
                {
                    byte[] buffer = new byte[20];
                    Random rnd = new Random();

                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = (byte)rnd.Next(70, 122);
                    }
                    MutexString = ASCIIEncoding.ASCII.GetString(buffer);
                }
                //MessageBox.Show(MutexString);
                try
                {
                    mutex = Mutex.OpenExisting(MutexString);
                    return false;
                }
                catch
                {
                    mutex = new Mutex(true, MutexString);
                    return true;
                }
            }
            return false;
        }
    }
}
