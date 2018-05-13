using Serveur_Client_PCC.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Serveur_Client_PCC
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer3.FixedPanel = FixedPanel.Panel1;
            Serveur_Client_PCC.Properties.Settings.Default.Save();
        }
        private HttpRequester hr = new HttpRequester();
        public JavaScriptSerializer serializer = new JavaScriptSerializer();
        private void Form1_Load(object sender, EventArgs e)
        {
            Parametre.form_principale = this;
            chargePcListe();
            chargeFichiersListe();
            setIp();
        }

        #region PC
        static int pc_enligne = 0;
        private void chargePcListe()
        {
            this.UseWaitCursor = true;
            var parent = Task.Factory.StartNew(() =>
            {
                var child = Task.Factory.StartNew(() =>
                {
                    string reponse = hr.get(Router.url("pc_liste"));
                    dynamic data = serializer.DeserializeObject(reponse);
                    string json = Encryption.Decrypt(Parametre.encryptionKey, data["d"]);
                    data = serializer.DeserializeObject(json);

                    if (data["success"] == false)
                        MessageBox.Show(Convert.ToString(data["message"]), "Erreur Chargement des PC", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    try
                    {
                        Action DoCrossThreadUIWork = () =>
                        {
                            list_pc.Items.Clear();
                            upInfos();
                        };
                        this.BeginInvoke(DoCrossThreadUIWork);

                        pc_enligne = 0;
                        foreach (dynamic pc in data["machines"])
                        {
                            ListViewItem lvi = new ListViewItem(pc["id"]);

                            Int32 temp = 0;
                            Int32 unixTimestamp = 0;
                            try
                            {
                                temp = Convert.ToInt32(pc["last_time"]);
                                unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                            }
                            catch (Exception) { }
                            lvi.ImageIndex = (unixTimestamp - temp > 180) ? 0 : 1;

                            pc_enligne = (lvi.ImageIndex == 1) ? pc_enligne + 1 : pc_enligne;

                            lvi.SubItems.Add(pc["guid"]);
                            lvi.SubItems.Add(pc["name"]);
                            lvi.SubItems.Add(pc["country"]);
                            lvi.SubItems.Add(pc["os"]);
                            lvi.SubItems.Add(pc["ram"] + " MB");
                            lvi.SubItems.Add(pc["processor"]);
                            lvi.SubItems.Add(pc["ip"]);
                            lvi.SubItems.Add(pc["date_created"]);

                            DoCrossThreadUIWork = () =>
                            {
                                list_pc.Invoke((MethodInvoker)(() =>
                                {
                                    list_pc.Items.Add(lvi);
                                }));
                                upInfos();
                            };
                            this.BeginInvoke(DoCrossThreadUIWork);
                        }
                    }
                    catch (Exception Ec)
                    {
                        Action DoCrossThreadUIWork = () =>
                        {
                            MessageBox.Show(Ec.ToString());
                        };
                        MessageBox.Show(Ec.ToString());
                        if (data["success"] == false)
                        {
                            Auth auth = new Auth();
                            auth.reconnexion();
                        }
                        else
                        {
                            DoCrossThreadUIWork = () =>
                            {
                                try { MessageBox.Show(Convert.ToString(data["message"]), "Erreur Chargement PC", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                catch { MessageBox.Show("Erreur: lors du chargement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            };
                        }
                    }
                });
            },
               TaskCreationOptions.AttachedToParent
            );
            this.UseWaitCursor = false;
        }
        private void list_pc_DoubleClick(object sender, EventArgs e)
        {
            if (list_pc.SelectedIndices.Count > 0)
            {
                PanelControl f1 = new PanelControl(list_pc.SelectedItems[0].Text, list_pc.SelectedItems[0].SubItems[2].Text);
                f1.Show();
            }
        }
        private void panelControleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (list_pc.SelectedIndices.Count > 0)
            {
                PanelControl f1 = new PanelControl(list_pc.SelectedItems[0].Text, list_pc.SelectedItems[0].SubItems[2].Text);
                f1.ShowDialog();
            }
        }
        private void list_pc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txt_pc_enLigne.Text = list_pc.SelectedItems[0].Text;
        }
        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chargePcListe();
        }
        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_pc.SelectedItems)
            {
                string file_name = item.SubItems[3].Text;
                string media_id = item.Text;
                string url = Router.url("delete_pc") + Convert.ToString(media_id);
                hr.get(url);
                setEventFile("Supprimer: " + file_name);
            }
        } 
        #endregion

        #region Fichiers
        private void chargeFichiersListe()
        {
            this.UseWaitCursor = true;
            var parent = Task.Factory.StartNew(() =>
            {
                var child = Task.Factory.StartNew(() =>
                {
                    string reponse = hr.get(Router.url("file_liste"));
                    dynamic data = serializer.DeserializeObject(reponse);
                    string json = Encryption.Decrypt(Parametre.encryptionKey, data["d"]);
                    data = serializer.DeserializeObject(json);

                    if (data["success"] == false)
                        MessageBox.Show(Convert.ToString(data["message"]), "Erreur Chargement des Fichiers", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    try
                    {
                        Action DoCrossThreadUIWork = () =>
                        {
                            list_fichiers.Items.Clear();
                            upInfos();
                        };
                        this.BeginInvoke(DoCrossThreadUIWork);

                        foreach (dynamic item in data["medias"])
                        {
                            ListViewItem lvi = new ListViewItem(item["id"]);
                            lvi.ImageIndex = 0;
                            lvi.SubItems.Add(item["pc_id"]);
                            lvi.SubItems.Add(item["pc_name"]);
                            lvi.SubItems.Add(item["name"]);
                            lvi.SubItems.Add(item["context"]);
                            lvi.SubItems.Add(item["date_created"]);

                            DoCrossThreadUIWork = () =>
                            {
                                list_fichiers.Items.Add(lvi);
                                upInfos();
                            };
                            this.BeginInvoke(DoCrossThreadUIWork);
                        }
                    }
                    catch (Exception)
                    {
                        if (data["success"] == false)
                        {
                            Auth auth = new Auth();
                            auth.reconnexion();
                        }
                        else
                        {
                            try { MessageBox.Show(Convert.ToString(data["message"]), "Erreur Chargement Fichiers", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            catch { MessageBox.Show("Erreur: lors du chargement des Fichiers.", "Erreur Chargement Fichiers", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }
                });
            },
               TaskCreationOptions.AttachedToParent
            );
            this.UseWaitCursor = false;
        }
        private void actualiser_toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            chargeFichiersListe();
            setEventFile("Fichiers actualiser");
        }
        private void telechargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_fichiers.SelectedItems)
            {
                try
                {
                    if (!Directory.Exists("Downloads/"))
                    {
                        Directory.CreateDirectory("Downloads/");
                    }
                    string pc_name = item.SubItems[2].Text;
                    string file_name = item.SubItems[3].Text;
                    string media_id = item.Text;
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

                    string url = Router.url("get_file") + Convert.ToString(media_id) + "?token=" + Parametre.API_KEY;

                    setEventFile("Téléchargement démmarer:" + file_name);
                    Thread th = new Thread(() =>
                    {
                        using (var cl = new WebClient())
                        {
                            cl.Headers["token"] = Parametre.API_KEY;
                            byte[] dataFile = cl.DownloadData(url);
                            File.WriteAllBytes(dir + file_name, dataFile);
                        }

                        setEventFile("Terminer: " + pc_name + ": " + file_name);
                    });
                    th.Start();
                }
                catch (Exception)
                {
                    setEventFile("Erreur: téléchargement du fichier: " + item.SubItems[3].Text);
                }
            }
        }
        private void supprimer_MenuItem3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list_fichiers.SelectedItems)
            {
                string file_name = item.SubItems[3].Text;
                string media_id = item.Text;
                string url = Router.url("delete_file") + Convert.ToString(media_id);
                hr.get(url);
                setEventFile("Supprimer: " + file_name);
            }
        }
        #endregion

        private void setIp()
        {
            Thread th = new Thread(() =>
            {
                try
                {
                    string reponse = hr.get(Router.url("ip"));
                    dynamic data = serializer.DeserializeObject(reponse);
                    string json = Encryption.Decrypt(Parametre.encryptionKey, data["d"]);
                    data = serializer.DeserializeObject(json);
                    txt_mon_ip.Invoke((MethodInvoker)(() =>
                    {
                        txt_mon_ip.Text = data["your_ip"];
                    }));
                }
                catch
                {
                    txt_mon_ip.Invoke((MethodInvoker)(() =>
                    {
                        txt_mon_ip.Text = "N/A";
                    }));
                }
            });
            th.Start();
        }

        
        private void timer_active_Tick(object sender, EventArgs e)
        {
            //Mettre a jour le jeton
            var parent = Task.Factory.StartNew
            (() =>
            {
                var child = Task.Factory.StartNew(() =>
                {
                    hr.get(Router.url("default"));                    
                });
            },
               TaskCreationOptions.AttachedToParent
            );
        }
        private void upInfos()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                txt_nbr_pc.Text = list_pc.Items.Count.ToString();
                txt_pc_enLigne.Text = pc_enligne.ToString();
                txt_nbr_fichiers.Text = list_fichiers.Items.Count.ToString();
            }));
        }

        public void setEventFile(string text)
        {
            list_event_files.Invoke((MethodInvoker)(() =>
            {
                list_event_files.Items.Add(text);
                list_event_files.SelectedIndex = list_event_files.Items.Count - 1;
            }));
        }
    }
}
