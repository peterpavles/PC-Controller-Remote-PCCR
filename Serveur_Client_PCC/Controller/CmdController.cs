using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class CmdController : ControllerBase
    {
        public const string Name = "Cmd";

        public void Cmd(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            string result = Convert.ToString(data["output"]);
            result = result.Replace("\r", Environment.NewLine).Replace("\r\n", Environment.NewLine);

            formPanel.txt_outputShell.Invoke((MethodInvoker)(() =>
            {
                formPanel.txt_outputShell.AppendText("\r\n" + result);
            }));
            setStatus("Commande reponse reçus avec succés");
        }

        public void ReqCmd(string argv1, string argv2)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Cmd");

            List<string> parameters = new List<string> { argv1, argv2 };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }

    }
}
