namespace Serveur_Client_PCC
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.list_pc = new System.Windows.Forms.ListView();
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PCListe = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelControleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PcImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_mon_ip = new System.Windows.Forms.Label();
            this.txt_nbr_fichiers = new System.Windows.Forms.Label();
            this.txt_pc_enLigne = new System.Windows.Forms.Label();
            this.txt_nbr_pc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btn_recherche = new System.Windows.Forms.Button();
            this.txt_dans = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_recherche = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.list_fichiers = new System.Windows.Forms.ListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilesListe = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.telechargerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FichierImageList = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_valider = new System.Windows.Forms.Button();
            this.checkBox_auto_conn = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_mdp_2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_mdp = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.timer_active = new System.Windows.Forms.Timer(this.components);
            this.list_event_files = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.PCListe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.FilesListe.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImageList = this.PcImageList;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(935, 541);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ImageKey = "screen.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(927, 514);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Liste PC";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.list_pc);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(921, 508);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.TabIndex = 1;
            // 
            // list_pc
            // 
            this.list_pc.AllowColumnReorder = true;
            this.list_pc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_pc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader13});
            this.list_pc.ContextMenuStrip = this.PCListe;
            this.list_pc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_pc.FullRowSelect = true;
            this.list_pc.GridLines = true;
            this.list_pc.Location = new System.Drawing.Point(0, 0);
            this.list_pc.MultiSelect = false;
            this.list_pc.Name = "list_pc";
            this.list_pc.Size = new System.Drawing.Size(921, 383);
            this.list_pc.SmallImageList = this.PcImageList;
            this.list_pc.TabIndex = 0;
            this.list_pc.UseCompatibleStateImageBehavior = false;
            this.list_pc.View = System.Windows.Forms.View.Details;
            this.list_pc.SelectedIndexChanged += new System.EventHandler(this.list_pc_SelectedIndexChanged);
            this.list_pc.DoubleClick += new System.EventHandler(this.list_pc_DoubleClick);
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "ID";
            this.columnHeader15.Width = 50;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "GUID";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "PC Name";
            this.columnHeader2.Width = 190;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Pays";
            this.columnHeader3.Width = 162;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Os";
            this.columnHeader4.Width = 141;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "RAM";
            this.columnHeader5.Width = 81;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Processeur";
            this.columnHeader6.Width = 102;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "IP";
            this.columnHeader7.Width = 109;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Date";
            // 
            // PCListe
            // 
            this.PCListe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelControleToolStripMenuItem,
            this.actualiserToolStripMenuItem,
            this.supprimerToolStripMenuItem});
            this.PCListe.Name = "contextMenuStripPCListe";
            this.PCListe.Size = new System.Drawing.Size(130, 70);
            // 
            // panelControleToolStripMenuItem
            // 
            this.panelControleToolStripMenuItem.Name = "panelControleToolStripMenuItem";
            this.panelControleToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.panelControleToolStripMenuItem.Text = "Controller";
            this.panelControleToolStripMenuItem.Click += new System.EventHandler(this.panelControleToolStripMenuItem_Click);
            // 
            // actualiserToolStripMenuItem
            // 
            this.actualiserToolStripMenuItem.Name = "actualiserToolStripMenuItem";
            this.actualiserToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.actualiserToolStripMenuItem.Text = "Actualiser";
            this.actualiserToolStripMenuItem.Click += new System.EventHandler(this.actualiserToolStripMenuItem_Click);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // PcImageList
            // 
            this.PcImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PcImageList.ImageStream")));
            this.PcImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.PcImageList.Images.SetKeyName(0, "offline.png");
            this.PcImageList.Images.SetKeyName(1, "online.png");
            this.PcImageList.Images.SetKeyName(2, "files.png");
            this.PcImageList.Images.SetKeyName(3, "screen.png");
            this.PcImageList.Images.SetKeyName(4, "settings.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(921, 121);
            this.splitContainer2.SplitterDistance = 431;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_mon_ip);
            this.groupBox1.Controls.Add(this.txt_nbr_fichiers);
            this.groupBox1.Controls.Add(this.txt_pc_enLigne);
            this.groupBox1.Controls.Add(this.txt_nbr_pc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 121);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Infos";
            // 
            // txt_mon_ip
            // 
            this.txt_mon_ip.AutoSize = true;
            this.txt_mon_ip.Location = new System.Drawing.Point(68, 95);
            this.txt_mon_ip.Name = "txt_mon_ip";
            this.txt_mon_ip.Size = new System.Drawing.Size(34, 13);
            this.txt_mon_ip.TabIndex = 7;
            this.txt_mon_ip.Text = "         ";
            // 
            // txt_nbr_fichiers
            // 
            this.txt_nbr_fichiers.AutoSize = true;
            this.txt_nbr_fichiers.Location = new System.Drawing.Point(105, 69);
            this.txt_nbr_fichiers.Name = "txt_nbr_fichiers";
            this.txt_nbr_fichiers.Size = new System.Drawing.Size(34, 13);
            this.txt_nbr_fichiers.TabIndex = 6;
            this.txt_nbr_fichiers.Text = "         ";
            // 
            // txt_pc_enLigne
            // 
            this.txt_pc_enLigne.AutoSize = true;
            this.txt_pc_enLigne.Location = new System.Drawing.Point(82, 46);
            this.txt_pc_enLigne.Name = "txt_pc_enLigne";
            this.txt_pc_enLigne.Size = new System.Drawing.Size(34, 13);
            this.txt_pc_enLigne.TabIndex = 5;
            this.txt_pc_enLigne.Text = "         ";
            // 
            // txt_nbr_pc
            // 
            this.txt_nbr_pc.AutoSize = true;
            this.txt_nbr_pc.Location = new System.Drawing.Point(82, 23);
            this.txt_nbr_pc.Name = "txt_nbr_pc";
            this.txt_nbr_pc.Size = new System.Drawing.Size(34, 13);
            this.txt_nbr_pc.TabIndex = 4;
            this.txt_nbr_pc.Text = "         ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Votre IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre Fichiers:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PC en ligne:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre PC:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer3);
            this.tabPage4.ImageKey = "files.png";
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(927, 514);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Fichiers Serveur";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.list_event_files);
            this.splitContainer3.Panel1.Controls.Add(this.btn_recherche);
            this.splitContainer3.Panel1.Controls.Add(this.txt_dans);
            this.splitContainer3.Panel1.Controls.Add(this.label6);
            this.splitContainer3.Panel1.Controls.Add(this.txt_recherche);
            this.splitContainer3.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.list_fichiers);
            this.splitContainer3.Size = new System.Drawing.Size(927, 514);
            this.splitContainer3.SplitterDistance = 252;
            this.splitContainer3.TabIndex = 0;
            // 
            // btn_recherche
            // 
            this.btn_recherche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_recherche.Image = ((System.Drawing.Image)(resources.GetObject("btn_recherche.Image")));
            this.btn_recherche.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_recherche.Location = new System.Drawing.Point(8, 109);
            this.btn_recherche.Name = "btn_recherche";
            this.btn_recherche.Size = new System.Drawing.Size(238, 29);
            this.btn_recherche.TabIndex = 4;
            this.btn_recherche.Text = "Recherche";
            this.btn_recherche.UseVisualStyleBackColor = true;
            // 
            // txt_dans
            // 
            this.txt_dans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_dans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txt_dans.FormattingEnabled = true;
            this.txt_dans.Items.AddRange(new object[] {
            "Tout",
            "ID",
            "Nom PC",
            "Nom Fichier",
            "Type",
            "Date"});
            this.txt_dans.Location = new System.Drawing.Point(8, 66);
            this.txt_dans.Name = "txt_dans";
            this.txt_dans.Size = new System.Drawing.Size(238, 21);
            this.txt_dans.TabIndex = 3;
            this.txt_dans.Tag = "Recherche dans";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Dans:";
            // 
            // txt_recherche
            // 
            this.txt_recherche.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_recherche.Location = new System.Drawing.Point(5, 26);
            this.txt_recherche.Name = "txt_recherche";
            this.txt_recherche.Size = new System.Drawing.Size(241, 21);
            this.txt_recherche.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Recherche:";
            // 
            // list_fichiers
            // 
            this.list_fichiers.AllowColumnReorder = true;
            this.list_fichiers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_fichiers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.list_fichiers.ContextMenuStrip = this.FilesListe;
            this.list_fichiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_fichiers.FullRowSelect = true;
            this.list_fichiers.GridLines = true;
            this.list_fichiers.Location = new System.Drawing.Point(0, 0);
            this.list_fichiers.Name = "list_fichiers";
            this.list_fichiers.Size = new System.Drawing.Size(671, 514);
            this.list_fichiers.SmallImageList = this.FichierImageList;
            this.list_fichiers.TabIndex = 1;
            this.list_fichiers.UseCompatibleStateImageBehavior = false;
            this.list_fichiers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "id";
            this.columnHeader14.Width = 50;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "pc_id";
            this.columnHeader8.Width = 108;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Nom PC";
            this.columnHeader9.Width = 137;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Nom Fichier";
            this.columnHeader10.Width = 179;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Type";
            this.columnHeader11.Width = 72;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Date";
            this.columnHeader12.Width = 170;
            // 
            // FilesListe
            // 
            this.FilesListe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.telechargerToolStripMenuItem});
            this.FilesListe.Name = "contextMenuStripPCListe";
            this.FilesListe.Size = new System.Drawing.Size(136, 70);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem2.Text = "Actualiser";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.actualiser_toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem3.Text = "Supprimer";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.supprimer_MenuItem3_Click);
            // 
            // telechargerToolStripMenuItem
            // 
            this.telechargerToolStripMenuItem.Name = "telechargerToolStripMenuItem";
            this.telechargerToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.telechargerToolStripMenuItem.Text = "Telecharger";
            this.telechargerToolStripMenuItem.Click += new System.EventHandler(this.telechargerToolStripMenuItem_Click);
            // 
            // FichierImageList
            // 
            this.FichierImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FichierImageList.ImageStream")));
            this.FichierImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.FichierImageList.Images.SetKeyName(0, "file.png");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.ImageKey = "settings.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(927, 514);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parametre";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.btn_valider);
            this.groupBox2.Controls.Add(this.checkBox_auto_conn);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txt_mdp_2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_mdp);
            this.groupBox2.Controls.Add(this.txt_email);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(8, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 518);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mon Compte";
            // 
            // btn_valider
            // 
            this.btn_valider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_valider.Location = new System.Drawing.Point(259, 181);
            this.btn_valider.Name = "btn_valider";
            this.btn_valider.Size = new System.Drawing.Size(145, 24);
            this.btn_valider.TabIndex = 8;
            this.btn_valider.Text = "Valider";
            this.btn_valider.UseVisualStyleBackColor = true;
            // 
            // checkBox_auto_conn
            // 
            this.checkBox_auto_conn.AutoSize = true;
            this.checkBox_auto_conn.Checked = global::Serveur_Client_PCC.Properties.Settings.Default.AutoLogin;
            this.checkBox_auto_conn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_auto_conn.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Serveur_Client_PCC.Properties.Settings.Default, "AutoLogin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_auto_conn.Location = new System.Drawing.Point(6, 160);
            this.checkBox_auto_conn.Name = "checkBox_auto_conn";
            this.checkBox_auto_conn.Size = new System.Drawing.Size(103, 17);
            this.checkBox_auto_conn.TabIndex = 7;
            this.checkBox_auto_conn.Text = "Auto Connexion";
            this.checkBox_auto_conn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(9, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "Deconnexion";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txt_mdp_2
            // 
            this.txt_mdp_2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_mdp_2.Location = new System.Drawing.Point(6, 124);
            this.txt_mdp_2.Name = "txt_mdp_2";
            this.txt_mdp_2.Size = new System.Drawing.Size(398, 21);
            this.txt_mdp_2.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Nouveau mot de passe:";
            // 
            // txt_mdp
            // 
            this.txt_mdp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_mdp.Location = new System.Drawing.Point(6, 82);
            this.txt_mdp.Name = "txt_mdp";
            this.txt_mdp.Size = new System.Drawing.Size(398, 21);
            this.txt_mdp.TabIndex = 3;
            // 
            // txt_email
            // 
            this.txt_email.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_email.Location = new System.Drawing.Point(6, 38);
            this.txt_email.Name = "txt_email";
            this.txt_email.ReadOnly = true;
            this.txt_email.Size = new System.Drawing.Size(398, 21);
            this.txt_email.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Mot de passe actuelle";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Email:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.BackgroundImage = global::Serveur_Client_PCC.Properties.Resources.qd_illumination;
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(927, 514);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "A propos";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label10.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkRed;
            this.label10.Location = new System.Drawing.Point(236, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(485, 47);
            this.label10.TabIndex = 0;
            this.label10.Text = "By QuadCore ENGINEERING";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_active
            // 
            this.timer_active.Interval = 10000;
            this.timer_active.Tick += new System.EventHandler(this.timer_active_Tick);
            // 
            // list_event_files
            // 
            this.list_event_files.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_event_files.FormattingEnabled = true;
            this.list_event_files.Location = new System.Drawing.Point(0, 419);
            this.list_event_files.Name = "list_event_files";
            this.list_event_files.Size = new System.Drawing.Size(249, 95);
            this.list_event_files.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 541);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCC QuadCore";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.PCListe.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.FilesListe.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView list_pc;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label txt_mon_ip;
        private System.Windows.Forms.Label txt_nbr_fichiers;
        private System.Windows.Forms.Label txt_pc_enLigne;
        private System.Windows.Forms.Label txt_nbr_pc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView list_fichiers;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.TextBox txt_recherche;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txt_dans;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer_active;
        private System.Windows.Forms.ContextMenuStrip PCListe;
        private System.Windows.Forms.ToolStripMenuItem panelControleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualiserToolStripMenuItem;
        private System.Windows.Forms.ImageList PcImageList;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_mdp_2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_mdp;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox_auto_conn;
        private System.Windows.Forms.Button btn_valider;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_recherche;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.ImageList FichierImageList;
        private System.Windows.Forms.ContextMenuStrip FilesListe;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem telechargerToolStripMenuItem;
        public System.Windows.Forms.ListBox list_event_files;
    }
}

