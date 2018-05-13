using Serveur_Client_PCC.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class InfosPcController : ControllerBase
    {
        public const string Name = "InfosPc";

        public void InfosPc(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            formPanel.Invoke((MethodInvoker)(() =>
            {
                formPanel.txt_SystemeInfos.Text = data["pc"];
                formPanel.txt_CpuInfos.Text = data["processor"];
                formPanel.txt_localDriver.Text = data["local"];
                formPanel.txt_VideoInfos.Text = data["video"];
            }));
            setStatus("Informations PC actualiser avec succés");
        }

        public void ReqInfosPc(string argv1 = "")
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "InfosPc");

            List<string> parameters = new List<string> { argv1 };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }

    }
}
