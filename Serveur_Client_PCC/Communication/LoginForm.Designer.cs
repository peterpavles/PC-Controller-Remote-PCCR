namespace Serveur_Client_PCC
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.txt_encryptionKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_connexion = new System.Windows.Forms.Button();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_auto_connexion = new System.Windows.Forms.CheckBox();
            this.txt_mdp = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_encryptionKey
            // 
            this.txt_encryptionKey.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_encryptionKey.ForeColor = System.Drawing.Color.Red;
            this.txt_encryptionKey.Location = new System.Drawing.Point(48, 120);
            this.txt_encryptionKey.Name = "txt_encryptionKey";
            this.txt_encryptionKey.Size = new System.Drawing.Size(326, 22);
            this.txt_encryptionKey.TabIndex = 24;
            this.txt_encryptionKey.Text = "piWUCi7DsWLUTwOLou9QbiBP33NVk3yAyWRrAIXrdtg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(48, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 14);
            this.label6.TabIndex = 23;
            this.label6.Text = "Clé Cryptage:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(401, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "X";
            this.label5.Click += new System.EventHandler(this.btn_quitter_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(113, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 35);
            this.label4.TabIndex = 21;
            this.label4.Text = "CONNEXION";
            // 
            // btn_connexion
            // 
            this.btn_connexion.BackColor = System.Drawing.Color.Lavender;
            this.btn_connexion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_connexion.ForeColor = System.Drawing.Color.Red;
            this.btn_connexion.Location = new System.Drawing.Point(237, 232);
            this.btn_connexion.Name = "btn_connexion";
            this.btn_connexion.Size = new System.Drawing.Size(137, 31);
            this.btn_connexion.TabIndex = 19;
            this.btn_connexion.Text = "Conexion";
            this.btn_connexion.UseVisualStyleBackColor = false;
            this.btn_connexion.Click += new System.EventHandler(this.btn_connexion_Click);
            // 
            // txt_url
            // 
            this.txt_url.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_url.ForeColor = System.Drawing.Color.Red;
            this.txt_url.Location = new System.Drawing.Point(48, 79);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(326, 22);
            this.txt_url.TabIndex = 16;
            this.txt_url.Text = "http://127.0.0.1/pcc_gateway/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(48, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "Mot de passe:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(48, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "Email:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(48, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Serveur URL:";
            // 
            // checkBox_auto_connexion
            // 
            this.checkBox_auto_connexion.AutoSize = true;
            this.checkBox_auto_connexion.Checked = global::Serveur_Client_PCC.Properties.Settings.Default.AutoLogin;
            this.checkBox_auto_connexion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_auto_connexion.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Serveur_Client_PCC.Properties.Settings.Default, "AutoLogin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_auto_connexion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_auto_connexion.ForeColor = System.Drawing.Color.Red;
            this.checkBox_auto_connexion.Location = new System.Drawing.Point(48, 232);
            this.checkBox_auto_connexion.Name = "checkBox_auto_connexion";
            this.checkBox_auto_connexion.Size = new System.Drawing.Size(114, 18);
            this.checkBox_auto_connexion.TabIndex = 20;
            this.checkBox_auto_connexion.Text = "Auto Connexion";
            this.checkBox_auto_connexion.UseVisualStyleBackColor = true;
            // 
            // txt_mdp
            // 
            this.txt_mdp.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Serveur_Client_PCC.Properties.Settings.Default, "Motdepasse", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txt_mdp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mdp.ForeColor = System.Drawing.Color.Red;
            this.txt_mdp.Location = new System.Drawing.Point(48, 206);
            this.txt_mdp.Name = "txt_mdp";
            this.txt_mdp.Size = new System.Drawing.Size(326, 22);
            this.txt_mdp.TabIndex = 18;
            this.txt_mdp.Text = global::Serveur_Client_PCC.Properties.Settings.Default.Motdepasse;
            this.txt_mdp.UseSystemPasswordChar = true;
            // 
            // txt_email
            // 
            this.txt_email.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Serveur_Client_PCC.Properties.Settings.Default, "Email", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txt_email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_email.ForeColor = System.Drawing.Color.Red;
            this.txt_email.Location = new System.Drawing.Point(48, 165);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(326, 22);
            this.txt_email.TabIndex = 17;
            this.txt_email.Text = global::Serveur_Client_PCC.Properties.Settings.Default.Email;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(431, 293);
            this.Controls.Add(this.txt_encryptionKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox_auto_connexion);
            this.Controls.Add(this.btn_connexion);
            this.Controls.Add(this.txt_mdp);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion Serveur";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_encryptionKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_auto_connexion;
        private System.Windows.Forms.Button btn_connexion;
        private System.Windows.Forms.TextBox txt_mdp;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}