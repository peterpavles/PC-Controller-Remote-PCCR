using Serveur_Client_PCC.Configuration;
using Serveur_Client_PCC_Outille;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class FichiersController : ControllerBase
    {
        public const string Name = "Fichiers";

        public void Fichiers(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            formPanel.list_fichiers.Invoke((MethodInvoker)(() => { formPanel.list_fichiers.Items.Clear(); }));
            formPanel.treeView_fichiers.Invoke((MethodInvoker)(() => { formPanel.treeView_fichiers.SelectedNode.Nodes.Clear(); }));

            foreach (dynamic item in data["folders"])
            {
                formPanel.list_fichiers.Invoke((MethodInvoker)(() =>
                {
                    formPanel.treeView_fichiers.SelectedNode.Nodes.Add(Convert.ToString(item["name"]), Convert.ToString(item["name"]), 0);
                    formPanel.treeView_fichiers.SelectedNode.ExpandAll();
                }));
            }
            foreach (dynamic item in data["files"])
            {
                ListViewItem lvi = new ListViewItem(Convert.ToString(item["name"]));
                lvi.ImageIndex = 0;
                lvi.SubItems.Add(getLength(Convert.ToString(item["size"])));
                lvi.SubItems.Add(Convert.ToString(item["date"]));
                formPanel.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_fichiers.Items.Add(lvi);
                }));
            }
            setStatus("Fichiers actualiser avec succés");
        }

        public static string getLength(string length)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = Convert.ToDouble(length);
            int order = 0;
            while (len >= 1024 && ++order < sizes.Length) {
                len = len/1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

        public void TelechargerFichier(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            try
            {
                if (!Directory.Exists("Downloads/"))
                {
                    Directory.CreateDirectory("Downloads/");
                }
                string pc_name = Convert.ToString(data["pc_name"]);
                string file_name = Convert.ToString(data["file_name"]);
                string dir = "Downloads/" + pc_name + "/";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

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

                string url = Router.url("get_file") + Convert.ToString(data["media_id"]) + "?token=" + Parametre.API_KEY;

                using (var cl = new WebClient())
                {
                    byte[] dataFile = cl.DownloadData(url);
                    File.WriteAllBytes(dir + file_name, dataFile);
                }

                setStatus("Fichier reçus de " + pc_name + ": " + file_name + " " + getLength(Convert.ToString(data["size"])));
            }
            catch (Exception)
            {
                setStatus("Erreur: téléchargement du fichier: " + data["file_name"]);
            }
        }

        public void GetFichier(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            setStatus(data["message"]);
        }
        public void Editer(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            Thread th = new Thread(() =>
            {
                DialogResult dr = new DialogResult();
                Editeur frm2 = new Editeur(data["path"], data["name"], data["content"]);
                dr = frm2.ShowDialog();
                if (dr == DialogResult.Cancel && frm2.text_changer)
                {
                    DialogResult dialogResult = MessageBox.Show("Voulez vous modifier le fichier ?", "Sauvegarder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ReqEditer(frm2.fichier_chemin, "1", frm2.txt_texte.Text);
                    }
                }
            });
            th.Start();
        }

        public void ReqFichiers(string argv1)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Fichiers");

            obj.Add("parameters", new List<string> { argv1 });

            send(Encode(obj));
        }
        public void ReqEditer(string path, string step, string content)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Editer");

            obj.Add("parameters", new List<string> { path, step, content });

            send(Encode(obj));
        
        }
        public void ReqAction(string argv1, string argv2)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Editer");

            obj.Add("parameters", new List<string> { argv1, argv2 });

            send(Encode(obj));
        }
        public void ReqGetFichier(string path_file, string file_name, string pc_id, string destination_dir)
        {
            if (File.Exists(path_file))
            {
                Thread th = new Thread(() =>
                {
                    string response = hr.postFile(Router.url("file_send"), path_file, "file", pc_id);
                    dynamic data = serializer.DeserializeObject(response);
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    obj.Add("controller", Name);
                    obj.Add("method", "GetFichier");

                    obj.Add("parameters", new List<string> { destination_dir, file_name, Convert.ToString(data["media_id"]) });

                    send(Encode(obj));
                    setStatus("Le fichier à été envoyer corréctement au serveur");
                });
                th.Start();
            }
            else
            {
                setStatus("Erreur: le fichier à envoyer n'existe pas");
            }
        }
        public void ReqTelechargerFichier(string path_file)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "TelechargerFichier");

            obj.Add("parameters", new List<string> { path_file });

            send(Encode(obj));
        }
    }
}
