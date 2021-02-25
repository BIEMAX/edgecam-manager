namespace Edgecam_Manager
{
    partial class CustomReportControl
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
            this.ubtnShowReport = new Infragistics.Win.Misc.UltraButton();
            this.cbxReports = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.tp = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cbxReports)).BeginInit();
            this.SuspendLayout();
            // 
            // ubtnShowReport
            // 
            this.ubtnShowReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.Image = global::Edgecam_Manager.Imagens_NewLookInterface.report_google_16;
            this.ubtnShowReport.Appearance = appearance1;
            this.ubtnShowReport.Location = new System.Drawing.Point(174, 2);
            this.ubtnShowReport.Name = "ubtnShowReport";
            this.ubtnShowReport.Size = new System.Drawing.Size(25, 25);
            this.ubtnShowReport.TabIndex = 5;
            this.tp.SetToolTip(this.ubtnShowReport, "Salvar os filtros da interface");
            this.ubtnShowReport.Click += new System.EventHandler(this.ubtnShowReport_Click);
            // 
            // cbxReports
            // 
            this.cbxReports.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxReports.Location = new System.Drawing.Point(3, 4);
            this.cbxReports.Name = "cbxReports";
            this.cbxReports.Size = new System.Drawing.Size(168, 21);
            this.cbxReports.TabIndex = 4;
            this.tp.SetToolTip(this.cbxReports, "Filtros criados anteriormente");
            this.cbxReports.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.cbxFilters_BeforeDropDown);
            this.cbxReports.ValueChanged += new System.EventHandler(this.cbxReports_ValueChanged);
            // 
            // CustomReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ubtnShowReport);
            this.Controls.Add(this.cbxReports);
            this.Name = "CustomReportControl";
            this.Size = new System.Drawing.Size(202, 28);
            ((System.ComponentModel.ISupportInitialize)(this.cbxReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Infragistics.Win.Misc.UltraButton ubtnShowReport;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cbxReports;
        private System.Windows.Forms.ToolTip tp;
    }
}
