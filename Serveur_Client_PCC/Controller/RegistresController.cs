using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class RegistresController : ControllerBase
    {
        public const string Name = "Registres";

        public void Registres(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            formPanel.list_registres.Invoke((MethodInvoker)(() => { formPanel.list_registres.Items.Clear(); }));
            formPanel.treeView_Registres.Invoke((MethodInvoker)(() => { formPanel.treeView_Registres.SelectedNode.Nodes.Clear(); }));

            foreach (dynamic item in data["registres"])
            {
                formPanel.list_registres.Invoke((MethodInvoker)(() =>
                {
                    formPanel.treeView_Registres.SelectedNode.Nodes.Add(item["key"]);
                    formPanel.treeView_Registres.SelectedNode.ExpandAll();
                }));
            }
            foreach (dynamic item in data["sousRegistres"])
            {
                formPanel.list_registres.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_registres.Items.Add(new ListViewItem(new string[] { Convert.ToString(item["key"]), Convert.ToString(item["value"]) }));
                }));
            }

            setStatus("Registres actualiser avec succés");
        }

        public void ReqAddKey(string path, string json)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "AddKey");

            List<string> parameters = new List<string> { path, json };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }
        public void ReqDeleteKey(string path, string json)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "DeleteKey");

            List<string> parameters = new List<string> { path, json };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }
        public void ReqRegistres(string argv1)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Registres");

            List<string> parameters = new List<string> { argv1 };
            obj.Add("parameters", parameters);

            send(Encode(obj)); 
        }
    }
}
