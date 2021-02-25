namespace Edgecam_Manager
{
    partial class FrmPeca_Seleciona_Db
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
            Infragistics.Win.UltraWinTree.UltraTreeColumnSet ultraTreeColumnSet1 = new Infragistics.Win.UltraWinTree.UltraTreeColumnSet();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn1 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn2 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn3 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn4 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn5 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn6 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPeca_Seleciona_Db));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.utv = new Infragistics.Win.UltraWinTree.UltraTree();
            this.cms_Comandos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_expandirTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_recolherTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_marcarTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_desmarcaTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utv)).BeginInit();
            this.cms_Comandos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.utv);
            this.groupBox1.Controls.Add(this.btnSelecionar);
            this.groupBox1.Controls.Add(this.btnReturn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(822, 564);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peças processadas anteriormente pelo sistema";
            // 
            // utv
            // 
            this.utv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            ultraTreeColumnSet1.AllowCellSizing = Infragistics.Win.UltraWinTree.LayoutSizing.Horizontal;
            ultraTreeNodeColumn1.AllowCellEdit = Infragistics.Win.UltraWinTree.AllowCellEdit.Full;
            ultraTreeNodeColumn1.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance1.TextHAlignAsString = "Center";
            appearance1.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn1.CellAppearance = appearance1;
            ultraTreeNodeColumn1.DataType = typeof(bool);
            ultraTreeNodeColumn1.Key = "Column 10";
            ultraTreeNodeColumn1.Text = "Selecionar";
            appearance2.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance2.ImageVAlign = Infragistics.Win.VAlign.Middle;
            ultraTreeNodeColumn2.ActiveCellAppearance = appearance2;
            ultraTreeNodeColumn2.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance3.ImageHAlign = Infragistics.Win.HAlign.Center;
            appearance3.ImageVAlign = Infragistics.Win.VAlign.Middle;
            appearance3.TextHAlignAsString = "Center";
            appearance3.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn2.CellAppearance = appearance3;
            ultraTreeNodeColumn2.DataType = typeof(System.Drawing.Bitmap);
            ultraTreeNodeColumn2.Key = "Column 1";
            ultraTreeNodeColumn2.Text = "Tipo";
            ultraTreeNodeColumn3.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance4.TextHAlignAsString = "Left";
            appearance4.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn3.CellAppearance = appearance4;
            ultraTreeNodeColumn3.DataType = typeof(string);
            ultraTreeNodeColumn3.Key = "Column 9";
            ultraTreeNodeColumn3.LayoutInfo.PreferredLabelSize = new System.Drawing.Size(300, 0);
            ultraTreeNodeColumn3.SortType = Infragistics.Win.UltraWinTree.SortType.Descending;
            ultraTreeNodeColumn3.Text = "Nome do item";
            ultraTreeNodeColumn4.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance5.TextHAlignAsString = "Center";
            appearance5.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn4.CellAppearance = appearance5;
            ultraTreeNodeColumn4.DataType = typeof(string);
            ultraTreeNodeColumn4.Key = "Column 4";
            ultraTreeNodeColumn4.Text = "Nível";
            ultraTreeNodeColumn5.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance6.TextHAlignAsString = "Center";
            appearance6.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn5.CellAppearance = appearance6;
            ultraTreeNodeColumn5.DataType = typeof(bool);
            ultraTreeNodeColumn5.Key = "Column 2";
            ultraTreeNodeColumn5.Text = "Ativo";
            ultraTreeNodeColumn6.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance7.TextHAlignAsString = "Center";
            appearance7.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn6.CellAppearance = appearance7;
            ultraTreeNodeColumn6.DataType = typeof(string);
            ultraTreeNodeColumn6.Key = "Column 3";
            ultraTreeNodeColumn6.Text = "Revisão";
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn1);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn2);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn3);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn4);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn5);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn6);
            this.utv.ColumnSettings.RootColumnSet = ultraTreeColumnSet1;
            this.utv.ContextMenuStrip = this.cms_Comandos;
            this.utv.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.utv.Location = new System.Drawing.Point(6, 19);
            this.utv.Name = "utv";
            this.utv.NodeConnectorColor = System.Drawing.SystemColors.ControlDark;
            _override1.CellClickAction = Infragistics.Win.UltraWinTree.CellClickAction.EditCell;
            this.utv.Override = _override1;
            this.utv.Size = new System.Drawing.Size(810, 502);
            this.utv.TabIndex = 85;
            this.utv.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.OutlookExpress;
            // 
            // cms_Comandos
            // 
            this.cms_Comandos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_expandirTodos,
            this.tsmi_recolherTodos,
            this.tsmi_marcarTodos,
            this.tsmi_desmarcaTodos});
            this.cms_Comandos.Name = "contextMenuStrip1";
            this.cms_Comandos.Size = new System.Drawing.Size(164, 92);
            // 
            // tsmi_expandirTodos
            // 
            this.tsmi_expandirTodos.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_expandirTodos.Image")));
            this.tsmi_expandirTodos.Name = "tsmi_expandirTodos";
            this.tsmi_expandirTodos.Size = new System.Drawing.Size(163, 22);
            this.tsmi_expandirTodos.Text = "Expandir todos";
            this.tsmi_expandirTodos.Click += new System.EventHandler(this.tsmi_expandirTodos_Click);
            // 
            // tsmi_recolherTodos
            // 
            this.tsmi_recolherTodos.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_recolherTodos.Image")));
            this.tsmi_recolherTodos.Name = "tsmi_recolherTodos";
            this.tsmi_recolherTodos.Size = new System.Drawing.Size(163, 22);
            this.tsmi_recolherTodos.Text = "Recolher todos";
            this.tsmi_recolherTodos.Click += new System.EventHandler(this.tsmi_recolherTodos_Click);
            // 
            // tsmi_marcarTodos
            // 
            this.tsmi_marcarTodos.Image = global::Edgecam_Manager.Properties.Resources.check;
            this.tsmi_marcarTodos.Name = "tsmi_marcarTodos";
            this.tsmi_marcarTodos.Size = new System.Drawing.Size(163, 22);
            this.tsmi_marcarTodos.Text = "Marcar todos";
            this.tsmi_marcarTodos.Click += new System.EventHandler(this.tsmi_marcarTodos_Click);
            // 
            // tsmi_desmarcaTodos
            // 
            this.tsmi_desmarcaTodos.Image = global::Edgecam_Manager.Properties.Resources.uncheck;
            this.tsmi_desmarcaTodos.Name = "tsmi_desmarcaTodos";
            this.tsmi_desmarcaTodos.Size = new System.Drawing.Size(163, 22);
            this.tsmi_desmarcaTodos.Text = "Desmarcar todos";
            this.tsmi_desmarcaTodos.Click += new System.EventHandler(this.tsmi_desmarcaTodos_Click);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelecionar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSelecionar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.selecionar_selecionado_16;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(704, 527);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(112, 31);
            this.btnSelecionar.TabIndex = 84;
            this.btnSelecionar.Text = "Selecionar peças";
            this.btnSelecionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnReturn.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(625, 527);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 31);
            this.btnReturn.TabIndex = 83;
            this.btnReturn.Text = "Retornar";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // FrmPeca_Seleciona_Db
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(846, 588);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPeca_Seleciona_Db";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escolha de artigos";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.utv)).EndInit();
            this.cms_Comandos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnReturn;
        private Infragistics.Win.UltraWinTree.UltraTree utv;
        private System.Windows.Forms.ContextMenuStrip cms_Comandos;
        private System.Windows.Forms.ToolStripMenuItem tsmi_expandirTodos;
        private System.Windows.Forms.ToolStripMenuItem tsmi_recolherTodos;
        private System.Windows.Forms.ToolStripMenuItem tsmi_marcarTodos;
        private System.Windows.Forms.ToolStripMenuItem tsmi_desmarcaTodos;
    }
}