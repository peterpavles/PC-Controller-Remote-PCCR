using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClIENT_PC.Controller
{
    class MsgboxController : ControllerBase
    {
        private string[] Objet;
        public string Msgbox(string type, string texte, string titre)
        {
            this.Objet = new string[] { type, texte, titre };
            ThreadPool.QueueUserWorkItem(new WaitCallback(MsgBoxThread));
            return Encode(new { success = true });
        }

        private void MsgBoxThread(object obj)
        {
            switch (Objet[0])
            {
                case "erreur":
                    MessageBox.Show(Objet[1], Objet[2], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "attention":
                    MessageBox.Show(Objet[1], Objet[2], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(Objet[1], Objet[2]);
                    break;
            }
        }
    }
}
