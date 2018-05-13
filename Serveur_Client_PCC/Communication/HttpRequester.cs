using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Serveur_Client_PCC
{
    class HttpRequester
    {
        public string get(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", Parametre.API_KEY);
            request.KeepAlive = true;
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
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

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        public string postFile(string url, string pathFile, string context, string pc_id = null)
        {
            string data = string.Empty;
            try
            {
                using (WebClient cl = new WebClient())
                {
                    cl.Headers.Add("Authorization", Parametre.API_KEY);
                    cl.Headers.Add("context", context);
                    if (!string.IsNullOrEmpty(pc_id))
                    {
                        cl.Headers.Add("pc_id", pc_id);
                    }
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
