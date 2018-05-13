using Serveur_Client_PCC.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Serveur_Client_PCC
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            Router.load();
        }
        public JavaScriptSerializer serializer = new JavaScriptSerializer();

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Auto_connexion();
        }
        private void Auto_connexion()
        {
            this.Hide();
            string Email = txt_email.Text = Serveur_Client_PCC.Properties.Settings.Default.Email;
            string Mdp = txt_mdp.Text = Serveur_Client_PCC.Properties.Settings.Default.Motdepasse;
            checkBox_auto_connexion.Checked = Serveur_Client_PCC.Properties.Settings.Default.AutoLogin;
            if(Serveur_Client_PCC.Properties.Settings.Default.AutoLogin)
            {
                if (Email != string.Empty && Mdp != string.Empty)
                {
                    string reponse = hr.post(txt_url.Text + Router.url("login"), 
                        "email=" + txt_email.Text + 
                        "&password=" + txt_mdp.Text);
                    dynamic data = serializer.DeserializeObject(reponse);
                    Serveur_Client_PCC.Properties.Settings.Default.Save();
                    if (data["success"] == true)
                    {
                        Parametre.API_KEY = Convert.ToString(data["token"]);
                        Parametre.email = txt_email.Text;
                        Parametre.motdepasse = txt_mdp.Text;
                        Parametre.URLServeur = txt_url.Text;
                        Serveur_Client_PCC.Properties.Settings.Default.Save();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    } 
                }
            }
            this.Show();
        }
        private HttpRequester hr = new HttpRequester();
        private void btn_connexion_Click(object sender, EventArgs e)
        {
            if (txt_url.Text.Contains("http") && txt_email.Text.Contains("@") && txt_mdp.Text != string.Empty)
            {
                Parametre.URLServeur = txt_url.Text;
                Parametre.encryptionKey = txt_encryptionKey.Text;
                string reponse = hr.post(Router.url("login"), "email=" + txt_email.Text + "&password=" + txt_mdp.Text);
                dynamic data = serializer.DeserializeObject(reponse);

                if (!Convert.ToBoolean(data["error"]))
                {
                    Parametre.API_KEY = Convert.ToString(data["api_key"]);
                    Parametre.email = txt_email.Text;
                    Parametre.motdepasse = txt_mdp.Text;
                    Parametre.URLServeur = txt_url.Text;
                    Serveur_Client_PCC.Properties.Settings.Default.Save();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erreur Connexion", Convert.ToString(data["message"]), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Champs incorrectes", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_quitter_Click(object sender, EventArgs e)
        {
            Environment.Exit(666);
        }
    }
}
