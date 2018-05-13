using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC.Controller
{
    class RegistresController : ControllerBase
    {
        public string Registres(string path)
        {
            List<object> ListeRegsV = new List<object> { };
            List<object> ListeRegsSub = new List<object> { };

            if (path.StartsWith("HKEY_CURRENT_USER\\"))
                path = path.Replace("HKEY_CURRENT_USER\\", "");

                RegistryKey reg = Registry.CurrentUser;

                string[] sousCles = reg.OpenSubKey(path, true).GetSubKeyNames();
                foreach (string sousClesCle in sousCles)
                {
                    ListeRegsV.Add(new { key = sousClesCle });
                }
                string[] sousClesF = reg.OpenSubKey(path, true).GetValueNames();
                foreach (string sousClesCle in sousClesF)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
                    ListeRegsSub.Add(new { key = sousClesCle, value = key.GetValue(sousClesCle) });
                }
            return serializer.Serialize(new { registres = ListeRegsV, sousRegistres = ListeRegsSub });
        }

        public string AddKey(string path, string argv2)
        {
            dynamic argvs = serializer.DeserializeObject(argv2);
            bool ok = false;
            try
            {
                Registry.CurrentUser.OpenSubKey(path, true).SetValue(argvs["name"], argvs["value"]);
                ok = true;
            }
            catch
            {
                ok = false;
            }
            return serializer.Serialize(new { success = ok, message = "Erreur: echec ajout de la clé: " + argvs["name"] }); ;
        }

        public string DeleteKey(string path, string argv2)
        {
            dynamic argvs = serializer.DeserializeObject(argv2);
            bool ok = false;
            try
            {
                Registry.CurrentUser.OpenSubKey(path, true).DeleteSubKey(argvs["name"]);
                ok = true;
            }
            catch
            {
                ok = false;
            }
            return serializer.Serialize(new { success = ok, message = "Erreur: echec suppression de la clé: " + argvs["name"] }); ;
        }

    }
}
