using Serveur_Client_PCC.Communication;
using System.Windows.Forms;
namespace Serveur_Client_PCC
{
    class Parametre
    {
        public static string URLServeur = null;
        public static string encryptionKey = null;
        public static string email = null;
        public static string motdepasse = null;

        public static string API_KEY = null;
        public static int refresh_ms = 1000;

        public static Form form_principale;
    }
}
