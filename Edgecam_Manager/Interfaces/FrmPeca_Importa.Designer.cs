namespace Edgecam_Manager
{
    partial class FrmPeca_Importa
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
            Infragistics.Win.UltraWinTree.UltraTreeColumnSet ultraTreeColumnSet1 = new Infragistics.Win.UltraWinTree.UltraTreeColumnSet();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn1 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn2 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn3 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn4 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn5 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn6 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn7 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn8 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.UltraTreeNodeColumn ultraTreeNodeColumn9 = new Infragistics.Win.UltraWinTree.UltraTreeNodeColumn();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTree.Override _override1 = new Infragistics.Win.UltraWinTree.Override();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPeca_Importa));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.utv = new Infragistics.Win.UltraWinTree.UltraTree();
            this.btnImportar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnSelecionar);
            this.panel1.Controls.Add(this.utv);
            this.panel1.Controls.Add(this.btnImportar);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(993, 520);
            this.panel1.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnReturn.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(674, 477);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 31);
            this.btnReturn.TabIndex = 88;
            this.btnReturn.Text = "Retornar";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelecionar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSelecionar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.mao_selecionar_16;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(753, 477);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(114, 31);
            this.btnSelecionar.TabIndex = 87;
            this.btnSelecionar.Text = "Selecionar peças";
            this.btnSelecionar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
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
            ultraTreeNodeColumn1.Text = "Importar";
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
            ultraTreeNodeColumn3.Text = "Nome do item";
            ultraTreeNodeColumn4.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            ultraTreeNodeColumn4.DataType = typeof(string);
            ultraTreeNodeColumn4.Key = "Column 5";
            ultraTreeNodeColumn4.Text = "Caminho do item";
            ultraTreeNodeColumn5.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            ultraTreeNodeColumn5.DataType = typeof(string);
            ultraTreeNodeColumn5.Key = "Column 6";
            ultraTreeNodeColumn5.Text = "Revisão";
            ultraTreeNodeColumn6.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            ultraTreeNodeColumn6.DataType = typeof(string);
            ultraTreeNodeColumn6.Key = "Column 8";
            ultraTreeNodeColumn6.Text = "Centro de trabalho";
            ultraTreeNodeColumn7.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            ultraTreeNodeColumn7.DataType = typeof(string);
            ultraTreeNodeColumn7.Key = "Column 7";
            ultraTreeNodeColumn7.Text = "Material";
            ultraTreeNodeColumn8.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance5.TextHAlignAsString = "Center";
            appearance5.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn8.CellAppearance = appearance5;
            ultraTreeNodeColumn8.DataType = typeof(string);
            ultraTreeNodeColumn8.Key = "Column 4";
            ultraTreeNodeColumn8.Text = "Nível";
            ultraTreeNodeColumn9.ButtonDisplayStyle = Infragistics.Win.UltraWinTree.ButtonDisplayStyle.Always;
            appearance6.TextHAlignAsString = "Center";
            appearance6.TextVAlignAsString = "Middle";
            ultraTreeNodeColumn9.CellAppearance = appearance6;
            ultraTreeNodeColumn9.DataType = typeof(bool);
            ultraTreeNodeColumn9.Key = "Column 2";
            ultraTreeNodeColumn9.Text = "Ativo";
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn1);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn2);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn3);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn4);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn5);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn6);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn7);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn8);
            ultraTreeColumnSet1.Columns.Add(ultraTreeNodeColumn9);
            this.utv.ColumnSettings.RootColumnSet = ultraTreeColumnSet1;
            this.utv.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.utv.Location = new System.Drawing.Point(17, 16);
            this.utv.Name = "utv";
            this.utv.NodeConnectorColor = System.Drawing.SystemColors.ControlDark;
            _override1.CellClickAction = Infragistics.Win.UltraWinTree.CellClickAction.EditCell;
            this.utv.Override = _override1;
            this.utv.Size = new System.Drawing.Size(964, 438);
            this.utv.TabIndex = 86;
            this.utv.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.OutlookExpress;
            // 
            // btnImportar
            // 
            this.btnImportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnImportar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.selecionar_selecionado_16;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(873, 477);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(108, 31);
            this.btnImportar.TabIndex = 85;
            this.btnImportar.Text = "Importar peças";
            this.btnImportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // FrmPeca_Importa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1017, 544);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPeca_Importa";
            this.Text = "FrmImportaPeca";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.utv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImportar;
        private Infragistics.Win.UltraWinTree.UltraTree utv;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.Button btnReturn;
    }
}