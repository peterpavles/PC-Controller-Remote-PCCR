using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class TelechargementController : ControllerBase
    {
        public void Telechargement(string[] ligne)
        {
            Thread thread = new Thread(() =>
            {
                if (!Directory.Exists("telecharger"))
                    Directory.CreateDirectory("telecharger");

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(ligne[1]), @"telecharger\");
                }
                MessageBox.Show("Telechargement de fichier terminer" + Environment.NewLine +
                    "Dans le dossier: telecharger", "Telechargement terminer ID PC:" + formPanel.PC_ID, MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            thread.Start();
        }

        public void ReqTelechargement(string choix = "")
        {
            send(Encode(new string[] { choix }));
        }

    }
}
