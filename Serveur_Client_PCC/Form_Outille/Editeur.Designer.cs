namespace Serveur_Client_PCC_Outille
{
    partial class Editeur
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_texte = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sauvegardeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conteunuInitialeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_texte
            // 
            this.txt_texte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_texte.Location = new System.Drawing.Point(0, 28);
            this.txt_texte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_texte.Name = "txt_texte";
            this.txt_texte.Size = new System.Drawing.Size(932, 559);
            this.txt_texte.TabIndex = 0;
            this.txt_texte.Text = "";
            this.txt_texte.ZoomFactor = 2F;
            this.txt_texte.TextChanged += new System.EventHandler(this.txt_texte_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sauvegardeToolStripMenuItem,
            this.viderToolStripMenuItem,
            this.conteunuInitialeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(932, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sauvegardeToolStripMenuItem
            // 
            this.sauvegardeToolStripMenuItem.Name = "sauvegardeToolStripMenuItem";
            this.sauvegardeToolStripMenuItem.Size = new System.Drawing.Size(99, 24);
            this.sauvegardeToolStripMenuItem.Text = "Sauvegarde";
            this.sauvegardeToolStripMenuItem.Click += new System.EventHandler(this.sauvegardeToolStripMenuItem_Click);
            // 
            // viderToolStripMenuItem
            // 
            this.viderToolStripMenuItem.Name = "viderToolStripMenuItem";
            this.viderToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.viderToolStripMenuItem.Text = "Vider";
            this.viderToolStripMenuItem.Click += new System.EventHandler(this.viderToolStripMenuItem_Click);
            // 
            // conteunuInitialeToolStripMenuItem
            // 
            this.conteunuInitialeToolStripMenuItem.Name = "conteunuInitialeToolStripMenuItem";
            this.conteunuInitialeToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.conteunuInitialeToolStripMenuItem.Text = "Conteunu Initiale";
            this.conteunuInitialeToolStripMenuItem.Click += new System.EventHandler(this.conteunuInitialeToolStripMenuItem_Click);
            // 
            // Editeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 587);
            this.Controls.Add(this.txt_texte);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Editeur";
            this.Text = "Editeur";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editeur_FormClosing);
            this.Load += new System.EventHandler(this.Editeur_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sauvegardeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conteunuInitialeToolStripMenuItem;
        public System.Windows.Forms.RichTextBox txt_texte;
    }
}