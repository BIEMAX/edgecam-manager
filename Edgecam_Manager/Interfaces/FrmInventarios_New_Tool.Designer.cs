namespace Edgecam_Manager
{
    partial class FrmInventarios_New_Tool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventarios_New_Tool));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUnidadeMedida = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAmbiente = new System.Windows.Forms.ComboBox();
            this.icbxSubTipo = new ImagedComboBox.ImagedComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNomeFerramenta = new System.Windows.Forms.TextBox();
            this.txtIdTool = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTempoEntrega = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbFornecedores = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCusto = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxTemValidade = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtValidade = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.txtTempo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxGerenciar = new System.Windows.Forms.CheckBox();
            this.cbTipoGestao = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbArmazem = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbUnidadeEmpresa = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtEstoqueMinimo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxForIntern = new System.Windows.Forms.CheckBox();
            this.cbxForProduction = new System.Windows.Forms.CheckBox();
            this.cbxForSale = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidade)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cbUnidadeMedida);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbAmbiente);
            this.groupBox1.Controls.Add(this.icbxSubTipo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNomeFerramenta);
            this.groupBox1.Controls.Add(this.txtIdTool);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 145);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações gerais";
            // 
            // cbUnidadeMedida
            // 
            this.cbUnidadeMedida.FormattingEnabled = true;
            this.cbUnidadeMedida.Location = new System.Drawing.Point(390, 106);
            this.cbUnidadeMedida.Name = "cbUnidadeMedida";
            this.cbUnidadeMedida.Size = new System.Drawing.Size(129, 21);
            this.cbUnidadeMedida.TabIndex = 97;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(387, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 96;
            this.label5.Text = "Unidade de medida";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 95;
            this.label4.Text = "Tipo de ferramenta";
            // 
            // cbAmbiente
            // 
            this.cbAmbiente.FormattingEnabled = true;
            this.cbAmbiente.Location = new System.Drawing.Point(20, 105);
            this.cbAmbiente.Name = "cbAmbiente";
            this.cbAmbiente.Size = new System.Drawing.Size(121, 21);
            this.cbAmbiente.TabIndex = 94;
            // 
            // icbxSubTipo
            // 
            this.icbxSubTipo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.icbxSubTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.icbxSubTipo.FormattingEnabled = true;
            this.icbxSubTipo.Location = new System.Drawing.Point(178, 105);
            this.icbxSubTipo.Name = "icbxSubTipo";
            this.icbxSubTipo.Size = new System.Drawing.Size(175, 21);
            this.icbxSubTipo.TabIndex = 93;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Ambiente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 90;
            this.label2.Text = "Nome da ferramenta";
            // 
            // txtNomeFerramenta
            // 
            this.txtNomeFerramenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeFerramenta.Location = new System.Drawing.Point(139, 46);
            this.txtNomeFerramenta.Name = "txtNomeFerramenta";
            this.txtNomeFerramenta.Size = new System.Drawing.Size(425, 20);
            this.txtNomeFerramenta.TabIndex = 89;
            // 
            // txtIdTool
            // 
            this.txtIdTool.Location = new System.Drawing.Point(20, 46);
            this.txtIdTool.Name = "txtIdTool";
            this.txtIdTool.Size = new System.Drawing.Size(81, 20);
            this.txtIdTool.TabIndex = 88;
            this.txtIdTool.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Id da ferramenta";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(511, 174);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 31);
            this.btnSave.TabIndex = 86;
            this.btnSave.Text = "Salvar";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnReturn.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(432, 174);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 31);
            this.btnReturn.TabIndex = 85;
            this.btnReturn.Text = "Retornar";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.txtTempoEntrega);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbFornecedores);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCusto);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbxTemValidade);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dtValidade);
            this.groupBox2.Controls.Add(this.txtTempo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbxGerenciar);
            this.groupBox2.Controls.Add(this.cbTipoGestao);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 179);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gestão de custos";
            // 
            // txtTempoEntrega
            // 
            this.txtTempoEntrega.Location = new System.Drawing.Point(381, 136);
            this.txtTempoEntrega.Name = "txtTempoEntrega";
            this.txtTempoEntrega.Size = new System.Drawing.Size(129, 20);
            this.txtTempoEntrega.TabIndex = 112;
            this.txtTempoEntrega.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTempoEntrega.TextChanged += new System.EventHandler(this.txtTempoEntrega_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(378, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 13);
            this.label11.TabIndex = 111;
            this.label11.Text = "Tempo para entrega (dias)";
            // 
            // cbFornecedores
            // 
            this.cbFornecedores.FormattingEnabled = true;
            this.cbFornecedores.Location = new System.Drawing.Point(209, 135);
            this.cbFornecedores.Name = "cbFornecedores";
            this.cbFornecedores.Size = new System.Drawing.Size(128, 21);
            this.cbFornecedores.TabIndex = 110;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(206, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 109;
            this.label10.Text = "Fornecedor";
            // 
            // txtCusto
            // 
            this.txtCusto.Location = new System.Drawing.Point(20, 136);
            this.txtCusto.Name = "txtCusto";
            this.txtCusto.Size = new System.Drawing.Size(131, 20);
            this.txtCusto.TabIndex = 108;
            this.txtCusto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 13);
            this.label9.TabIndex = 107;
            this.label9.Text = "Custo da ferramenta (unidade)";
            // 
            // cbxTemValidade
            // 
            this.cbxTemValidade.AutoSize = true;
            this.cbxTemValidade.Location = new System.Drawing.Point(379, 73);
            this.cbxTemValidade.Name = "cbxTemValidade";
            this.cbxTemValidade.Size = new System.Drawing.Size(15, 14);
            this.cbxTemValidade.TabIndex = 106;
            this.cbxTemValidade.UseVisualStyleBackColor = true;
            this.cbxTemValidade.CheckedChanged += new System.EventHandler(this.cbxTemValidade_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(378, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 105;
            this.label8.Text = "Data de validade";
            // 
            // dtValidade
            // 
            this.dtValidade.DateTime = new System.DateTime(2019, 3, 15, 0, 0, 0, 0);
            this.dtValidade.Location = new System.Drawing.Point(400, 70);
            this.dtValidade.Name = "dtValidade";
            this.dtValidade.Size = new System.Drawing.Size(110, 21);
            this.dtValidade.TabIndex = 104;
            this.dtValidade.Value = new System.DateTime(2019, 3, 15, 0, 0, 0, 0);
            // 
            // txtTempo
            // 
            this.txtTempo.Location = new System.Drawing.Point(209, 71);
            this.txtTempo.Name = "txtTempo";
            this.txtTempo.Size = new System.Drawing.Size(128, 20);
            this.txtTempo.TabIndex = 102;
            this.txtTempo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTempo.TextChanged += new System.EventHandler(this.txtTempo_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(206, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 101;
            this.label7.Text = "Valor da vida útil";
            // 
            // cbxGerenciar
            // 
            this.cbxGerenciar.AutoSize = true;
            this.cbxGerenciar.Location = new System.Drawing.Point(20, 29);
            this.cbxGerenciar.Name = "cbxGerenciar";
            this.cbxGerenciar.Size = new System.Drawing.Size(179, 17);
            this.cbxGerenciar.TabIndex = 100;
            this.cbxGerenciar.Text = "Gerenciar vida útil da ferramenta";
            this.cbxGerenciar.UseVisualStyleBackColor = true;
            this.cbxGerenciar.CheckedChanged += new System.EventHandler(this.cbxGerenciar_CheckedChanged);
            // 
            // cbTipoGestao
            // 
            this.cbTipoGestao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoGestao.FormattingEnabled = true;
            this.cbTipoGestao.Items.AddRange(new object[] {
            "(Selecione)",
            "Horas",
            "Dias de usinagem",
            "Metros percorridos"});
            this.cbTipoGestao.Location = new System.Drawing.Point(20, 71);
            this.cbTipoGestao.Name = "cbTipoGestao";
            this.cbTipoGestao.Size = new System.Drawing.Size(131, 21);
            this.cbTipoGestao.TabIndex = 99;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 98;
            this.label6.Text = "Tipo de gestão de vida últil";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(169, 47);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(111, 20);
            this.txtQuantidade.TabIndex = 114;
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantidade.TextChanged += new System.EventHandler(this.txtQuantidade_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(166, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 13);
            this.label12.TabIndex = 113;
            this.label12.Text = "Quantidade disponível";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.cbArmazem);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cbUnidadeEmpresa);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txtEstoqueMinimo);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cbxForIntern);
            this.groupBox3.Controls.Add(this.cbxForProduction);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnReturn);
            this.groupBox3.Controls.Add(this.cbxForSale);
            this.groupBox3.Controls.Add(this.txtQuantidade);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(12, 348);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(583, 211);
            this.groupBox3.TabIndex = 113;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Estoque";
            // 
            // cbArmazem
            // 
            this.cbArmazem.FormattingEnabled = true;
            this.cbArmazem.Location = new System.Drawing.Point(222, 104);
            this.cbArmazem.Name = "cbArmazem";
            this.cbArmazem.Size = new System.Drawing.Size(179, 21);
            this.cbArmazem.TabIndex = 123;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(219, 88);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 122;
            this.label15.Text = "Armazém";
            // 
            // cbUnidadeEmpresa
            // 
            this.cbUnidadeEmpresa.FormattingEnabled = true;
            this.cbUnidadeEmpresa.Location = new System.Drawing.Point(20, 104);
            this.cbUnidadeEmpresa.Name = "cbUnidadeEmpresa";
            this.cbUnidadeEmpresa.Size = new System.Drawing.Size(179, 21);
            this.cbUnidadeEmpresa.TabIndex = 121;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(105, 13);
            this.label14.TabIndex = 120;
            this.label14.Text = "Unidade da empresa";
            // 
            // txtEstoqueMinimo
            // 
            this.txtEstoqueMinimo.Location = new System.Drawing.Point(20, 47);
            this.txtEstoqueMinimo.Name = "txtEstoqueMinimo";
            this.txtEstoqueMinimo.Size = new System.Drawing.Size(111, 20);
            this.txtEstoqueMinimo.TabIndex = 119;
            this.txtEstoqueMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstoqueMinimo.TextChanged += new System.EventHandler(this.txtEstoqueMinimo_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 13);
            this.label13.TabIndex = 118;
            this.label13.Text = "Estoque minimo";
            // 
            // cbxForIntern
            // 
            this.cbxForIntern.AutoSize = true;
            this.cbxForIntern.Location = new System.Drawing.Point(467, 30);
            this.cbxForIntern.Name = "cbxForIntern";
            this.cbxForIntern.Size = new System.Drawing.Size(103, 17);
            this.cbxForIntern.TabIndex = 117;
            this.cbxForIntern.Text = "Para uso interno";
            this.cbxForIntern.UseVisualStyleBackColor = true;
            // 
            // cbxForProduction
            // 
            this.cbxForProduction.AutoSize = true;
            this.cbxForProduction.Location = new System.Drawing.Point(325, 49);
            this.cbxForProduction.Name = "cbxForProduction";
            this.cbxForProduction.Size = new System.Drawing.Size(145, 17);
            this.cbxForProduction.TabIndex = 116;
            this.cbxForProduction.Text = "Permitir uso em produção";
            this.cbxForProduction.UseVisualStyleBackColor = true;
            // 
            // cbxForSale
            // 
            this.cbxForSale.AutoSize = true;
            this.cbxForSale.Location = new System.Drawing.Point(325, 30);
            this.cbxForSale.Name = "cbxForSale";
            this.cbxForSale.Size = new System.Drawing.Size(93, 17);
            this.cbxForSale.TabIndex = 115;
            this.cbxForSale.Text = "Permitir venda";
            this.cbxForSale.UseVisualStyleBackColor = true;
            // 
            // FrmInventarios_New_Tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(607, 571);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(623, 610);
            this.Name = "FrmInventarios_New_Tool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtValidade)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.ComboBox cbUnidadeMedida;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAmbiente;
        private ImagedComboBox.ImagedComboBox icbxSubTipo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNomeFerramenta;
        private System.Windows.Forms.TextBox txtIdTool;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTempo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbxGerenciar;
        private System.Windows.Forms.ComboBox cbTipoGestao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTempoEntrega;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbFornecedores;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCusto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbxTemValidade;
        private System.Windows.Forms.Label label8;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dtValidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbArmazem;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbUnidadeEmpresa;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtEstoqueMinimo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbxForIntern;
        private System.Windows.Forms.CheckBox cbxForProduction;
        private System.Windows.Forms.CheckBox cbxForSale;
    }
}