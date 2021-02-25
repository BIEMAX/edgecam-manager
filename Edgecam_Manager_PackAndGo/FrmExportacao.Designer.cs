namespace Edgecam_Manager_PackAndGo
{
    partial class FrmExportacao
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportacao));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udtFim = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.udtInicio = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rdExportarPeriodo = new System.Windows.Forms.RadioButton();
            this.rdExportarUltimo = new System.Windows.Forms.RadioButton();
            this.rdExportarTudo = new System.Windows.Forms.RadioButton();
            this.cbxTrabalhos = new System.Windows.Forms.CheckBox();
            this.cbxInventario = new System.Windows.Forms.CheckBox();
            this.cbxOrcamentos = new System.Windows.Forms.CheckBox();
            this.cbxOrdens = new System.Windows.Forms.CheckBox();
            this.cbxTarefas = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdExtSkaZip = new System.Windows.Forms.RadioButton();
            this.rdExtZip = new System.Windows.Forms.RadioButton();
            this.rdExt7Zip = new System.Windows.Forms.RadioButton();
            this.rdExtRar = new System.Windows.Forms.RadioButton();
            this.btnPesquisa_Pasta_Zip = new System.Windows.Forms.Button();
            this.txtPastaArquivo = new System.Windows.Forms.TextBox();
            this.btnPesquisa_Pasta = new System.Windows.Forms.Button();
            this.txtPasta = new System.Windows.Forms.TextBox();
            this.rdSalvarArquivo = new System.Windows.Forms.RadioButton();
            this.rdSalvarPasta = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstbxArquivos = new System.Windows.Forms.ListBox();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmRemoverArquivos = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAnexar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rtxtComentarios = new System.Windows.Forms.RichTextBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udtFim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udtInicio)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.udtFim);
            this.groupBox1.Controls.Add(this.udtInicio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdExportarPeriodo);
            this.groupBox1.Controls.Add(this.rdExportarUltimo);
            this.groupBox1.Controls.Add(this.rdExportarTudo);
            this.groupBox1.Controls.Add(this.cbxTrabalhos);
            this.groupBox1.Controls.Add(this.cbxInventario);
            this.groupBox1.Controls.Add(this.cbxOrcamentos);
            this.groupBox1.Controls.Add(this.cbxOrdens);
            this.groupBox1.Controls.Add(this.cbxTarefas);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 156);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Definições dos conteúdos a serem exportados";
            // 
            // udtFim
            // 
            this.udtFim.Location = new System.Drawing.Point(542, 120);
            this.udtFim.Name = "udtFim";
            this.udtFim.Size = new System.Drawing.Size(85, 21);
            this.udtFim.TabIndex = 11;
            // 
            // udtInicio
            // 
            this.udtInicio.Location = new System.Drawing.Point(451, 120);
            this.udtInicio.Name = "udtInicio";
            this.udtInicio.Size = new System.Drawing.Size(85, 21);
            this.udtInicio.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Até:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "De:";
            // 
            // rdExportarPeriodo
            // 
            this.rdExportarPeriodo.AutoSize = true;
            this.rdExportarPeriodo.Location = new System.Drawing.Point(321, 102);
            this.rdExportarPeriodo.Name = "rdExportarPeriodo";
            this.rdExportarPeriodo.Size = new System.Drawing.Size(122, 17);
            this.rdExportarPeriodo.TabIndex = 7;
            this.rdExportarPeriodo.TabStop = true;
            this.rdExportarPeriodo.Text = "Exportar por período";
            this.rdExportarPeriodo.UseVisualStyleBackColor = true;
            this.rdExportarPeriodo.CheckedChanged += new System.EventHandler(this.rdExportarPeriodo_CheckedChanged);
            // 
            // rdExportarUltimo
            // 
            this.rdExportarUltimo.AutoSize = true;
            this.rdExportarUltimo.Location = new System.Drawing.Point(127, 102);
            this.rdExportarUltimo.Name = "rdExportarUltimo";
            this.rdExportarUltimo.Size = new System.Drawing.Size(178, 17);
            this.rdExportarUltimo.TabIndex = 6;
            this.rdExportarUltimo.TabStop = true;
            this.rdExportarUltimo.Text = "Exportar apenas o útlimo registro";
            this.rdExportarUltimo.UseVisualStyleBackColor = true;
            // 
            // rdExportarTudo
            // 
            this.rdExportarTudo.AutoSize = true;
            this.rdExportarTudo.Location = new System.Drawing.Point(18, 102);
            this.rdExportarTudo.Name = "rdExportarTudo";
            this.rdExportarTudo.Size = new System.Drawing.Size(88, 17);
            this.rdExportarTudo.TabIndex = 5;
            this.rdExportarTudo.TabStop = true;
            this.rdExportarTudo.Text = "Exportar tudo";
            this.rdExportarTudo.UseVisualStyleBackColor = true;
            // 
            // cbxTrabalhos
            // 
            this.cbxTrabalhos.AutoSize = true;
            this.cbxTrabalhos.Location = new System.Drawing.Point(345, 30);
            this.cbxTrabalhos.Name = "cbxTrabalhos";
            this.cbxTrabalhos.Size = new System.Drawing.Size(111, 17);
            this.cbxTrabalhos.TabIndex = 4;
            this.cbxTrabalhos.Text = "Exportar trabalhos";
            this.cbxTrabalhos.UseVisualStyleBackColor = true;
            // 
            // cbxInventario
            // 
            this.cbxInventario.AutoSize = true;
            this.cbxInventario.Location = new System.Drawing.Point(186, 63);
            this.cbxInventario.Name = "cbxInventario";
            this.cbxInventario.Size = new System.Drawing.Size(119, 17);
            this.cbxInventario.TabIndex = 3;
            this.cbxInventario.Text = "Exportar inventários";
            this.cbxInventario.UseVisualStyleBackColor = true;
            // 
            // cbxOrcamentos
            // 
            this.cbxOrcamentos.AutoSize = true;
            this.cbxOrcamentos.Location = new System.Drawing.Point(186, 30);
            this.cbxOrcamentos.Name = "cbxOrcamentos";
            this.cbxOrcamentos.Size = new System.Drawing.Size(123, 17);
            this.cbxOrcamentos.TabIndex = 2;
            this.cbxOrcamentos.Text = "Exportar orçamentos";
            this.cbxOrcamentos.UseVisualStyleBackColor = true;
            // 
            // cbxOrdens
            // 
            this.cbxOrdens.AutoSize = true;
            this.cbxOrdens.Location = new System.Drawing.Point(18, 63);
            this.cbxOrdens.Name = "cbxOrdens";
            this.cbxOrdens.Size = new System.Drawing.Size(163, 17);
            this.cbxOrdens.TabIndex = 1;
            this.cbxOrdens.Text = "Exportar ordens de produção";
            this.cbxOrdens.UseVisualStyleBackColor = true;
            // 
            // cbxTarefas
            // 
            this.cbxTarefas.AutoSize = true;
            this.cbxTarefas.Location = new System.Drawing.Point(18, 30);
            this.cbxTarefas.Name = "cbxTarefas";
            this.cbxTarefas.Size = new System.Drawing.Size(100, 17);
            this.cbxTarefas.TabIndex = 0;
            this.cbxTarefas.Text = "Exportar tarefas";
            this.cbxTarefas.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.btnPesquisa_Pasta_Zip);
            this.groupBox2.Controls.Add(this.txtPastaArquivo);
            this.groupBox2.Controls.Add(this.btnPesquisa_Pasta);
            this.groupBox2.Controls.Add(this.txtPasta);
            this.groupBox2.Controls.Add(this.rdSalvarArquivo);
            this.groupBox2.Controls.Add(this.rdSalvarPasta);
            this.groupBox2.Location = new System.Drawing.Point(12, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(641, 156);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opções de exportação";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdExtSkaZip);
            this.groupBox4.Controls.Add(this.rdExtZip);
            this.groupBox4.Controls.Add(this.rdExt7Zip);
            this.groupBox4.Controls.Add(this.rdExtRar);
            this.groupBox4.Location = new System.Drawing.Point(18, 100);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(342, 50);
            this.groupBox4.TabIndex = 103;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Extensão do arquivo compactado:";
            // 
            // rdExtSkaZip
            // 
            this.rdExtSkaZip.AutoSize = true;
            this.rdExtSkaZip.Location = new System.Drawing.Point(195, 19);
            this.rdExtSkaZip.Name = "rdExtSkaZip";
            this.rdExtSkaZip.Size = new System.Drawing.Size(133, 17);
            this.rdExtSkaZip.TabIndex = 102;
            this.rdExtSkaZip.TabStop = true;
            this.rdExtSkaZip.Text = "SkaZip (recomendado)";
            this.rdExtSkaZip.UseVisualStyleBackColor = true;
            // 
            // rdExtZip
            // 
            this.rdExtZip.AutoSize = true;
            this.rdExtZip.Location = new System.Drawing.Point(6, 19);
            this.rdExtZip.Name = "rdExtZip";
            this.rdExtZip.Size = new System.Drawing.Size(42, 17);
            this.rdExtZip.TabIndex = 98;
            this.rdExtZip.TabStop = true;
            this.rdExtZip.Text = "ZIP";
            this.rdExtZip.UseVisualStyleBackColor = true;
            // 
            // rdExt7Zip
            // 
            this.rdExt7Zip.AutoSize = true;
            this.rdExt7Zip.Location = new System.Drawing.Point(130, 19);
            this.rdExt7Zip.Name = "rdExt7Zip";
            this.rdExt7Zip.Size = new System.Drawing.Size(48, 17);
            this.rdExt7Zip.TabIndex = 101;
            this.rdExt7Zip.TabStop = true;
            this.rdExt7Zip.Text = "7ZIP";
            this.rdExt7Zip.UseVisualStyleBackColor = true;
            // 
            // rdExtRar
            // 
            this.rdExtRar.AutoSize = true;
            this.rdExtRar.Location = new System.Drawing.Point(64, 19);
            this.rdExtRar.Name = "rdExtRar";
            this.rdExtRar.Size = new System.Drawing.Size(48, 17);
            this.rdExtRar.TabIndex = 100;
            this.rdExtRar.TabStop = true;
            this.rdExtRar.Text = "RAR";
            this.rdExtRar.UseVisualStyleBackColor = true;
            // 
            // btnPesquisa_Pasta_Zip
            // 
            this.btnPesquisa_Pasta_Zip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisa_Pasta_Zip.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisa_Pasta_Zip.Image = global::Edgecam_Manager_PackAndGo.Properties.Resources.Search;
            this.btnPesquisa_Pasta_Zip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa_Pasta_Zip.Location = new System.Drawing.Point(542, 61);
            this.btnPesquisa_Pasta_Zip.Name = "btnPesquisa_Pasta_Zip";
            this.btnPesquisa_Pasta_Zip.Size = new System.Drawing.Size(75, 24);
            this.btnPesquisa_Pasta_Zip.TabIndex = 97;
            this.btnPesquisa_Pasta_Zip.Text = "Procurar";
            this.btnPesquisa_Pasta_Zip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa_Pasta_Zip.UseVisualStyleBackColor = true;
            this.btnPesquisa_Pasta_Zip.Click += new System.EventHandler(this.btnPesquisa_Pasta_Zip_Click);
            // 
            // txtPastaArquivo
            // 
            this.txtPastaArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPastaArquivo.Location = new System.Drawing.Point(197, 64);
            this.txtPastaArquivo.Name = "txtPastaArquivo";
            this.txtPastaArquivo.Size = new System.Drawing.Size(326, 20);
            this.txtPastaArquivo.TabIndex = 96;
            // 
            // btnPesquisa_Pasta
            // 
            this.btnPesquisa_Pasta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisa_Pasta.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnPesquisa_Pasta.Image = global::Edgecam_Manager_PackAndGo.Properties.Resources.Search;
            this.btnPesquisa_Pasta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPesquisa_Pasta.Location = new System.Drawing.Point(542, 34);
            this.btnPesquisa_Pasta.Name = "btnPesquisa_Pasta";
            this.btnPesquisa_Pasta.Size = new System.Drawing.Size(75, 24);
            this.btnPesquisa_Pasta.TabIndex = 95;
            this.btnPesquisa_Pasta.Text = "Procurar";
            this.btnPesquisa_Pasta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisa_Pasta.UseVisualStyleBackColor = true;
            this.btnPesquisa_Pasta.Click += new System.EventHandler(this.btnPesquisa_Pasta_Click);
            // 
            // txtPasta
            // 
            this.txtPasta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasta.Location = new System.Drawing.Point(197, 38);
            this.txtPasta.Name = "txtPasta";
            this.txtPasta.Size = new System.Drawing.Size(326, 20);
            this.txtPasta.TabIndex = 2;
            // 
            // rdSalvarArquivo
            // 
            this.rdSalvarArquivo.AutoSize = true;
            this.rdSalvarArquivo.Location = new System.Drawing.Point(18, 65);
            this.rdSalvarArquivo.Name = "rdSalvarArquivo";
            this.rdSalvarArquivo.Size = new System.Drawing.Size(127, 17);
            this.rdSalvarArquivo.TabIndex = 1;
            this.rdSalvarArquivo.TabStop = true;
            this.rdSalvarArquivo.Text = "Salvar em um arquivo";
            this.rdSalvarArquivo.UseVisualStyleBackColor = true;
            this.rdSalvarArquivo.CheckedChanged += new System.EventHandler(this.rdSalvarArquivo_CheckedChanged);
            // 
            // rdSalvarPasta
            // 
            this.rdSalvarPasta.AutoSize = true;
            this.rdSalvarPasta.Location = new System.Drawing.Point(18, 38);
            this.rdSalvarPasta.Name = "rdSalvarPasta";
            this.rdSalvarPasta.Size = new System.Drawing.Size(124, 17);
            this.rdSalvarPasta.TabIndex = 0;
            this.rdSalvarPasta.TabStop = true;
            this.rdSalvarPasta.Text = "Salvar em uma pasta";
            this.rdSalvarPasta.UseVisualStyleBackColor = true;
            this.rdSalvarPasta.CheckedChanged += new System.EventHandler(this.rdSalvarPasta_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.lstbxArquivos);
            this.groupBox3.Controls.Add(this.btnAnexar);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rtxtComentarios);
            this.groupBox3.Controls.Add(this.btnExportar);
            this.groupBox3.Controls.Add(this.btnReturn);
            this.groupBox3.Location = new System.Drawing.Point(12, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(641, 260);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Considerações finais";
            // 
            // lstbxArquivos
            // 
            this.lstbxArquivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbxArquivos.ContextMenuStrip = this.cms;
            this.lstbxArquivos.FormattingEnabled = true;
            this.lstbxArquivos.Location = new System.Drawing.Point(349, 57);
            this.lstbxArquivos.Name = "lstbxArquivos";
            this.lstbxArquivos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstbxArquivos.Size = new System.Drawing.Size(278, 160);
            this.lstbxArquivos.TabIndex = 91;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRemoverArquivos});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(241, 26);
            this.cms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_Opening);
            // 
            // tsmRemoverArquivos
            // 
            this.tsmRemoverArquivos.Name = "tsmRemoverArquivos";
            this.tsmRemoverArquivos.Size = new System.Drawing.Size(240, 22);
            this.tsmRemoverArquivos.Text = "Remover arquivos selecionados";
            this.tsmRemoverArquivos.Click += new System.EventHandler(this.tsmRemoverArquivos_Click);
            // 
            // btnAnexar
            // 
            this.btnAnexar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnexar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnAnexar.Image = global::Edgecam_Manager_PackAndGo.Properties.Resources.Search;
            this.btnAnexar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnexar.Location = new System.Drawing.Point(349, 20);
            this.btnAnexar.Name = "btnAnexar";
            this.btnAnexar.Size = new System.Drawing.Size(107, 31);
            this.btnAnexar.TabIndex = 90;
            this.btnAnexar.Text = "Anexar arquivos";
            this.btnAnexar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnexar.UseVisualStyleBackColor = true;
            this.btnAnexar.Click += new System.EventHandler(this.btnAnexar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 88;
            this.label4.Text = "Comentários (opcional)";
            // 
            // rtxtComentarios
            // 
            this.rtxtComentarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtComentarios.Location = new System.Drawing.Point(6, 57);
            this.rtxtComentarios.Name = "rtxtComentarios";
            this.rtxtComentarios.Size = new System.Drawing.Size(318, 160);
            this.rtxtComentarios.TabIndex = 87;
            this.rtxtComentarios.Text = "";
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnExportar.Image = global::Edgecam_Manager_PackAndGo.Properties.Resources.ok;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(546, 223);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(81, 31);
            this.btnExportar.TabIndex = 86;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnReturn.Image = global::Edgecam_Manager_PackAndGo.Properties.Resources.exit;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(463, 223);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 31);
            this.btnReturn.TabIndex = 85;
            this.btnReturn.Text = "Retornar";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // FrmExportacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(665, 608);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(681, 647);
            this.Name = "FrmExportacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compactar e enviar";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udtFim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udtInicio)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPasta;
        private System.Windows.Forms.RadioButton rdSalvarArquivo;
        private System.Windows.Forms.RadioButton rdSalvarPasta;
        private System.Windows.Forms.CheckBox cbxInventario;
        private System.Windows.Forms.CheckBox cbxOrcamentos;
        private System.Windows.Forms.CheckBox cbxOrdens;
        private System.Windows.Forms.CheckBox cbxTarefas;
        private System.Windows.Forms.Button btnPesquisa_Pasta_Zip;
        private System.Windows.Forms.TextBox txtPastaArquivo;
        private System.Windows.Forms.Button btnPesquisa_Pasta;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbxTrabalhos;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor udtInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdExportarPeriodo;
        private System.Windows.Forms.RadioButton rdExportarUltimo;
        private System.Windows.Forms.RadioButton rdExportarTudo;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor udtFim;
        private System.Windows.Forms.RadioButton rdExtSkaZip;
        private System.Windows.Forms.RadioButton rdExt7Zip;
        private System.Windows.Forms.RadioButton rdExtRar;
        private System.Windows.Forms.RadioButton rdExtZip;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtxtComentarios;
        private System.Windows.Forms.ListBox lstbxArquivos;
        private System.Windows.Forms.Button btnAnexar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem tsmRemoverArquivos;
    }
}

