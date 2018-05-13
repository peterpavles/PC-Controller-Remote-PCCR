using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class EcranController : ControllerBase
    {
        public const string Name = "Ecran";

        public void Ecran(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            string url = Router.url("get_file") + data["media_id"] + "?token=" + Parametre.API_KEY;
            formPanel.pictureBox1.Invoke((MethodInvoker)(() =>
            {
                formPanel.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                formPanel.pictureBox1.ImageLocation = url;
            }));
            setStatus("Capture d'ecran reçus");
        }

        public void ReqEcran()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Ecran");

            List<string> parameters = new List<string> { };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }

    }
}
