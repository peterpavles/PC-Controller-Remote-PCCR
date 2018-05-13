using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class ProcessusController : ControllerBase
    {
        public const string Name = "Processus";

        public void Processus(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            formPanel.list_programmes.Invoke((MethodInvoker)(() => { formPanel.list_programmes.Items.Clear(); }));
            foreach (dynamic item in data["process"])
            {
                formPanel.list_programmes.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_processus.Items.Add(new ListViewItem(new string[] { Convert.ToString(item["name"]), Convert.ToString(item["memory"]), Convert.ToString(item["description"]) }));
                }));
            }
            setStatus("Processus actualiser avec succés");
        }

        public void ReqProcessus()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Processus");

            obj.Add("parameters", new List<string> {  });

            send(Encode(obj));
        }

        public void ReqTuer(string name)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Tuer");

            obj.Add("parameters", new List<string> { name });

            send(Encode(obj));
        }

        public void ReqDemmarer(string name)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Demmarer");

            obj.Add("parameters", new List<string> { name });

            send(Encode(obj));
        }

    }
}
