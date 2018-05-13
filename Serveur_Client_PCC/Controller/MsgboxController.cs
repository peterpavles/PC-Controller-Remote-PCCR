using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class MsgboxController : ControllerBase
    {
        public const string Name = "Msgbox";

        public void Msgbox(string status)
        {
            setStatus("Message Box afficher succés");
        }

        public void ReqMsgbox(string type, string msg, string title)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Msgbox");

            List<string> parameters = new List<string> { type, msg, title};
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }
    }
}
