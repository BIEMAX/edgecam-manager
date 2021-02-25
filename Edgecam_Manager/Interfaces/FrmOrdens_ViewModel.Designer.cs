namespace Edgecam_Manager
{
    partial class FrmOrdens_ViewModel
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
        /// the contents of method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdens_ViewModel));
            panel1 = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(792, 576);
            panel1.TabIndex = 0;
            // 
            // FrmOrdens_ViewModel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(792, 576);
            Controls.Add(panel1);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "FrmOrdens_ViewModel";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Visualização 3D";
            Load += new System.EventHandler(FrmOrdens_ViewModel_Load);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
    }
}