using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC.Controller
{
    class FunController : ControllerBase
    {
        public const string Name = "Fun";

        public void Fun(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            setStatus(data["message"]);
        }

        public void TeleExe(string json)
        {
            dynamic data = serializer.DeserializeObject(json);
            setStatus(data["message"]);
        }

        public void ReqFun(string argv1, string argv2)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("controller", Name);
            obj.Add("method", "Fun");

            List<string> parameters = new List<string> { argv1, argv2 };
            obj.Add("parameters", parameters);

            send(Encode(obj));
        }

        public void ReqTeleExe(string path_file, string file_name, string pc_id)
        {
            if (File.Exists(path_file))
            {
                Thread th = new Thread(() =>
                {
                    string response = hr.postFile(Router.url("file_send"), path_file, "file", pc_id);
                    dynamic data = serializer.DeserializeObject(response);
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    obj.Add("controller", Name);
                    obj.Add("method", "TeleExe");

                    string randomFileName = Path.GetRandomFileName() + Path.GetExtension(file_name);
                    obj.Add("parameters", new List<string> { randomFileName, Convert.ToString(data["media_id"]) });

                    send(Encode(obj));
                    setStatus("Le fichier: " + file_name + " à été envoyer corréctement au serveur");
                });
                th.Start();
            }
            else
            {
                setStatus("Erreur: le fichier à envoyer et éxecuter n'existe pas dans votre ordinateur");
            }
        }
    }
}
