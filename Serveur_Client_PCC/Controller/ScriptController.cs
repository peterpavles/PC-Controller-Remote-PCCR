using Serveur_Client_PCC.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur_Client_PCC.Controller
{
    class ScriptController : ControllerBase
    {
        public const string Name = "Script";

        public void Script(string json)
        {
            //dynamic data = serializer.DeserializeObject(json);
            setStatus("Script éxecuter avec succés");
        }

        public void ReqScript(string extension, string content)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Script");

            List<string> parameters = new List<string> { extension, content };
            obj.Add("parameters", parameters);

            send(Encode(obj)); 
        }
    }
}
