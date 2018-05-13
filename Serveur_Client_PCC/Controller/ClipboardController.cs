using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class ClipboardController : ControllerBase
    {
        public const string Name = "Clipboard";

        public void Clipboard(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            formPanel.txt_sortie_clipboard.Invoke((MethodInvoker)(() =>
            {
                formPanel.txt_sortie_clipboard.Text = data["clipboard"];
            }));
            setStatus("Presse papier actualiser avec succés");
        }

        public void ReqClipboard(string choix, string text = "")
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Clipboard");

            List<string> parameters = new List<string> { choix, text };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }
    }
}
