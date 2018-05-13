using Serveur_Client_PCC.Controller;
using System.Web;
using System.Web.Script.Serialization;

namespace Serveur_Client_PCC
{
    class ControllerBase
    {
        public PanelControl formPanel;
        protected static HttpRequester hr = new HttpRequester();
        public static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public void setThis(PanelControl formPanel)
        {
            this.formPanel = formPanel;
        }

        public void send(string data, int priority = 1)
        {
            data = "d=" + HttpUtility.UrlEncode(Encryption.Encrypt(Parametre.encryptionKey, serializer.Serialize(new { pc_id = formPanel.PC_ID, priority = priority, content = data })));
            string rps = hr.post(Router.url("send_command"), data);
            string msg = "Requete envoyer " + FichiersController.getLength(data.Length.ToString()) + " patienter svp... ";
            setStatus(msg);
            addEvent(msg);
        }

        public string Encode(dynamic obj)
        {
            return serializer.Serialize(obj);
        }

        public void addEvent(string text)
        {
            formPanel.setEvent(text);
        }

        public void setStatus(string text, bool status = true)
        {
            formPanel.setStatus(text, status);
        }
    }
}
