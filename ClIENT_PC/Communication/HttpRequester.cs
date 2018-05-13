using ClIENT_PC.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClIENT_PC
{
    class HttpRequester
    {
        public static string useragent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; pt-PT; rv:1.9.1.2) Gecko/20090729 Firefox/3.5.2 (.NET CLR 3.5.30729)";
        private int tantativeMax = 6;
        public string get(string url)
        {
            var requete = (HttpWebRequest)WebRequest.Create(url);
            requete.UserAgent = useragent;
            requete.CookieContainer = new CookieContainer();
            requete.AllowAutoRedirect = true;
            requete.KeepAlive = true;
            requete.Headers.Add("Authorization", Parametre.API_KEY);

            var reponseString = "";

            for (int t = 0; t < tantativeMax; t++)
            {
                try
                {
                    using (var response = (HttpWebResponse)requete.GetResponse())
                    {
                        reponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        break;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.UnknownError ||
                        ex.Status == WebExceptionStatus.TrustFailure ||
                        ex.Status == WebExceptionStatus.ServerProtocolViolation ||
                        ex.Status == WebExceptionStatus.SendFailure ||
                        ex.Status == WebExceptionStatus.SecureChannelFailure ||
                        ex.Status == WebExceptionStatus.RequestProhibitedByCachePolicy ||
                        ex.Status == WebExceptionStatus.RequestCanceled ||
                        ex.Status == WebExceptionStatus.ReceiveFailure ||
                        ex.Status == WebExceptionStatus.ProtocolError ||
                        ex.Status == WebExceptionStatus.PipelineFailure ||
                        ex.Status == WebExceptionStatus.CacheEntryNotFound)
                    {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream))
                        {
                            reponseString = reader.ReadToEnd();
                        }
                    }
                }
                catch
                {

                }
            }
            return reponseString;
        }
		public string post(string url, string donne)
		{
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(donne);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.KeepAlive = true;
            request.Headers.Add("Authorization", Parametre.API_KEY);

            var reponseString = "";

            for (int t = 0; t < tantativeMax; t++)
            {
                try
                {
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    reponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    break;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.UnknownError ||
                        ex.Status == WebExceptionStatus.TrustFailure ||
                        ex.Status == WebExceptionStatus.ServerProtocolViolation ||
                        ex.Status == WebExceptionStatus.SendFailure ||
                        ex.Status == WebExceptionStatus.SecureChannelFailure ||
                        ex.Status == WebExceptionStatus.RequestProhibitedByCachePolicy ||
                        ex.Status == WebExceptionStatus.RequestCanceled ||
                        ex.Status == WebExceptionStatus.PipelineFailure ||
                        ex.Status == WebExceptionStatus.CacheEntryNotFound)
                    {
                        using (var stream = ex.Response.GetResponseStream())
                        using (var reader = new StreamReader(stream))
                        {
                            reponseString = reader.ReadToEnd();
                        }
                        Console.WriteLine(ex.ToString());
                        Console.WriteLine(reponseString);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return reponseString;
		}
        public string postFile(string url, string pathFile, string context)
        {
            string data = string.Empty;
            try
            {
                using (WebClient cl = new WebClient())
                {
                    cl.Headers.Add("Authorization", Parametre.API_KEY);
                    cl.Headers.Add("context", context);
                    data = Encoding.ASCII.GetString(cl.UploadFile(url, "POST", pathFile));
                }
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    data = reader.ReadToEnd();
                }
            }
            var serializer = new JavaScriptSerializer();
            dynamic result = serializer.DeserializeObject(data);
            string reponse = Encryption.Decrypt(Parametre.encryptionKey, result["d"]);
            return reponse;
        }
    }
}
