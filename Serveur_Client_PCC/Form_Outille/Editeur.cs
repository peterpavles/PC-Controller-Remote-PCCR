using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur_Client_PCC_Outille
{
    public partial class Editeur : Form
    {
        public Editeur(String texte, string titre = null, string fihierChemin = null)
        {
            InitializeComponent();
            txt_texte.Text = conteunu_init = texte;
            fichier_chemin = titre;
            fichier_chemin = fihierChemin;
            if(titre != null)
                this.Text += " | " + titre;
        }
        public bool text_changer = false;
        public string fichier_chemin = string.Empty;
        private string conteunu_init = string.Empty;
        private void Editeur_Load(object sender, EventArgs e)
        {

        }

        private void txt_texte_TextChanged(object sender, EventArgs e)
        {
            text_changer = true;
        }

        private void Editeur_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void sauvegardeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Path.GetFileName(fichier_chemin); ;
            // set filters - this can be done in properties as well
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            Thread th = new Thread(() =>
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine(txt_texte);
                    }
                }
            });
            th.Start();
        }

        private void viderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (text_changer)
                txt_texte.Text = string.Empty;
        }

        private void conteunuInitialeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_texte.Text = conteunu_init;
        }
    }
}
