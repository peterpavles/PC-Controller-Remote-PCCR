using Serveur_Client_PCC.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class ProgrammesController : ControllerBase
    {
        public const string Name = "Programmes";

        public void Programmes(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            formPanel.list_programmes.Invoke((MethodInvoker)(() => { formPanel.list_programmes.Items.Clear(); }));
            foreach (dynamic item in data["programs"])
            {
                formPanel.list_programmes.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_programmes.Items.Add(new ListViewItem(new string[] { item["name"], item["version"], item["publisher"], item["uninstall"] }));
                }));
            }
            setStatus("Programmes actualiser avec succés");
        }

        public void ReqProgrammes()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Programmes");

            obj.Add("parameters", new List<string> { });

            send(Encode(obj));
        }

        public void ReqDesinstaller(string name)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Desinstaller");

            obj.Add("parameters", new List<string> { name });

            send(Encode(obj));
        }

    }
}
