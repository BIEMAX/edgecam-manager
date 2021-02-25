namespace Edgecam_Manager
{
    partial class CustomFilterControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.ubtnFiltros = new Infragistics.Win.Misc.UltraButton();
            this.ubtnResetFilter = new Infragistics.Win.Misc.UltraButton();
            this.ubtnSaveFilter = new Infragistics.Win.Misc.UltraButton();
            this.cbxFilters = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.tp = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cbxFilters)).BeginInit();
            this.SuspendLayout();
            // 
            // ubtnFiltros
            // 
            this.ubtnFiltros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.Image = global::Edgecam_Manager.Imagens_NewLookInterface.filter_16;
            this.ubtnFiltros.Appearance = appearance1;
            this.ubtnFiltros.Location = new System.Drawing.Point(233, 0);
            this.ubtnFiltros.Name = "ubtnFiltros";
            this.ubtnFiltros.Size = new System.Drawing.Size(25, 25);
            this.ubtnFiltros.TabIndex = 7;
            this.tp.SetToolTip(this.ubtnFiltros, "Administrar filtros");
            this.ubtnFiltros.Click += new System.EventHandler(this.ubtnFiltros_Click);
            // 
            // ubtnResetFilter
            // 
            this.ubtnResetFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.Image = global::Edgecam_Manager.Imagens_NewLookInterface.filter_clear_16;
            this.ubtnResetFilter.Appearance = appearance2;
            this.ubtnResetFilter.Location = new System.Drawing.Point(202, 0);
            this.ubtnResetFilter.Name = "ubtnResetFilter";
            this.ubtnResetFilter.Size = new System.Drawing.Size(25, 25);
            this.ubtnResetFilter.TabIndex = 6;
            this.tp.SetToolTip(this.ubtnResetFilter, "Reiniciar o filtro");
            this.ubtnResetFilter.Click += new System.EventHandler(this.ubtnResetFilter_Click);
            // 
            // ubtnSaveFilter
            // 
            this.ubtnSaveFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.Image = global::Edgecam_Manager.Imagens_NewLookInterface.filtro_salvar_16;
            this.ubtnSaveFilter.Appearance = appearance3;
            this.ubtnSaveFilter.Location = new System.Drawing.Point(171, 0);
            this.ubtnSaveFilter.Name = "ubtnSaveFilter";
            this.ubtnSaveFilter.Size = new System.Drawing.Size(25, 25);
            this.ubtnSaveFilter.TabIndex = 5;
            this.tp.SetToolTip(this.ubtnSaveFilter, "Salvar os filtros da interface");
            this.ubtnSaveFilter.Click += new System.EventHandler(this.ubtnSaveFilter_Click);
            // 
            // cbxFilters
            // 
            this.cbxFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFilters.Location = new System.Drawing.Point(7, 4);
            this.cbxFilters.Name = "cbxFilters";
            this.cbxFilters.Size = new System.Drawing.Size(158, 21);
            this.cbxFilters.TabIndex = 4;
            this.tp.SetToolTip(this.cbxFilters, "Filtros criados anteriormente");
            this.cbxFilters.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.cbxFilters_BeforeDropDown);
            this.cbxFilters.ValueChanged += new System.EventHandler(this.cbxFilters_ValueChanged);
            // 
            // CustomFilterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ubtnFiltros);
            this.Controls.Add(this.ubtnResetFilter);
            this.Controls.Add(this.ubtnSaveFilter);
            this.Controls.Add(this.cbxFilters);
            this.Name = "CustomFilterControl";
            this.Size = new System.Drawing.Size(258, 28);
            ((System.ComponentModel.ISupportInitialize)(this.cbxFilters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton ubtnFiltros;
        private Infragistics.Win.Misc.UltraButton ubtnResetFilter;
        private Infragistics.Win.Misc.UltraButton ubtnSaveFilter;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cbxFilters;
        private System.Windows.Forms.ToolTip tp;
    }
}
