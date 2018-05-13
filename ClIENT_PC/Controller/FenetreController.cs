using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller
{
    class FenetreController : ControllerBase
    {
        public FenetreController()
        {
            EnumWindows(new WindowEnumCallback(this.AddWnd), 0);
        }
        public string Fenetre(string action)
        {
            string encoder = serializer.Serialize(new { action = "fenetre-ouvert", windows = Liste_Windows });
            return encoder;
        }

        public delegate bool WindowEnumCallback(int hwnd, int lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(WindowEnumCallback lpEnumFunc, int lParam);

        [DllImport("user32.dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(int h);

        private List<string> Liste_Windows = new List<string>();
        private bool AddWnd(int hwnd, int lparam)
        {
            if (IsWindowVisible(hwnd))
            {
              StringBuilder sb = new StringBuilder(255);
              GetWindowText(hwnd, sb, sb.Capacity);
              Liste_Windows.Add(sb.ToString());          
            }
            return true;
        }
       

    }
}
