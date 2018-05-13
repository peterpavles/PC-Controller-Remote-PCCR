using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Communication
{
    class Auth
    {
        private HttpRequester hr = new HttpRequester();
        public JavaScriptSerializer serializer = new JavaScriptSerializer();

        public bool reconnexion()
        {
            if (Parametre.URLServeur != string.Empty && Parametre.email != string.Empty && Parametre.motdepasse != string.Empty)
            {
                string reponse = hr.post(Router.url("login"),
                        "email=" + Parametre.motdepasse +
                        "&password=" + Parametre.motdepasse);
                dynamic data = serializer.DeserializeObject(reponse);
                if (data["success"] == false)
                {
                    Parametre.API_KEY = Convert.ToString(data["token"]);
                }
                else
                {
                    MessageBox.Show("Une erreur c'est produite", Convert.ToString(data["message"]), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return true;
            }
            else
            {
                LoginForm f1 = new LoginForm();
                DialogResult dr = f1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    Application.Exit();
                }
                return true;
            }
        }
    }
}
