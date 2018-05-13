using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serveur_Client_PCC
{
    public class Router
    {
        public static Dictionary<String, String> path = new Dictionary<String, String>();

        public static string url(string conn)
        {
            string param = string.Empty;
            return Parametre.URLServeur.TrimEnd('/') + "/" + path[conn] + param;
        }

        public static void load()
        {
            path.Add("login", "admin/login");
            path.Add("send_command", "admin/commands");
            path.Add("get_result", "admin/results/");
            path.Add("get_file", "admin/medias/get/");

            path.Add("pc_liste", "admin/machines");
            path.Add("pc_profile", "admin/machines/");
            path.Add("file_send", "admin/medias");
            path.Add("file_liste", "admin/medias/gets");
            path.Add("pc_file_liste", "admin/medias/gets/");

            path.Add("delete_pc", "admin/machines/delete/");
            path.Add("delete_file", "admin/medias/delete/");

            path.Add("default", "admin/default/");
            path.Add("ip", "admin/tools/ip");
        }
    }
}
