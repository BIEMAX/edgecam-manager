namespace Edgecam_Manager_MacroDev
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmArquivo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSalvar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSair = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAjuda = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbTexto = new SkaSyntaxHighlighterRichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmArquivo,
            this.tsmConfig,
            this.tsmAjuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(761, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmArquivo
            // 
            this.tsmArquivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAbrir,
            this.tsmSalvar,
            this.tsmSair});
            this.tsmArquivo.Image = global::Edgecam_Manager_MacroDev.Properties.Resources.file;
            this.tsmArquivo.Name = "tsmArquivo";
            this.tsmArquivo.Size = new System.Drawing.Size(77, 20);
            this.tsmArquivo.Text = "Arquivo";
            this.tsmArquivo.DropDownOpening += new System.EventHandler(this.tsmArquivo_DropDownOpening);
            // 
            // tsmAbrir
            // 
            this.tsmAbrir.Image = global::Edgecam_Manager_MacroDev.Properties.Resources.folder_open;
            this.tsmAbrir.Name = "tsmAbrir";
            this.tsmAbrir.Size = new System.Drawing.Size(152, 22);
            this.tsmAbrir.Text = "Abrir";
            this.tsmAbrir.Click += new System.EventHandler(this.tsmAbrir_Click);
            // 
            // tsmSalvar
            // 
            this.tsmSalvar.Image = global::Edgecam_Manager_MacroDev.Properties.Resources.Save;
            this.tsmSalvar.Name = "tsmSalvar";
            this.tsmSalvar.Size = new System.Drawing.Size(152, 22);
            this.tsmSalvar.Text = "Salvar";
            this.tsmSalvar.Click += new System.EventHandler(this.tsmSalvar_Click);
            // 
            // tsmSair
            // 
            this.tsmSair.Image = global::Edgecam_Manager_MacroDev.Properties.Resources.exit;
            this.tsmSair.Name = "tsmSair";
            this.tsmSair.Size = new System.Drawing.Size(152, 22);
            this.tsmSair.Text = "Sair";
            this.tsmSair.Click += new System.EventHandler(this.tsmSair_Click);
            // 
            // tsmConfig
            // 
            this.tsmConfig.Image = global::Edgecam_Manager_MacroDev.Properties.Resources.config;
            this.tsmConfig.Name = "tsmConfig";
            this.tsmConfig.Size = new System.Drawing.Size(112, 20);
            this.tsmConfig.Text = "Configurações";
            // 
            // tsmAjuda
            // 
            this.tsmAjuda.Image = global::Edgecam_Manager_MacroDev.Properties.Resources.help;
            this.tsmAjuda.Name = "tsmAjuda";
            this.tsmAjuda.Size = new System.Drawing.Size(65, 20);
            this.tsmAjuda.Text = "Sobre";
            // 
            // rtbTexto
            // 
            this.rtbTexto.AcceptsTab = true;
            this.rtbTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTexto.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.rtbTexto.Location = new System.Drawing.Point(0, 24);
            this.rtbTexto.Name = "rtbTexto";
            this.rtbTexto.Size = new System.Drawing.Size(761, 476);
            this.rtbTexto.TabIndex = 0;
            this.rtbTexto.Text = "";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 500);
            this.Controls.Add(this.rtbTexto);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ambiente para desenvolvimento de macros - Edgecam Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkaSyntaxHighlighterRichTextBox rtbTexto;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmArquivo;
        private System.Windows.Forms.ToolStripMenuItem tsmSalvar;
        private System.Windows.Forms.ToolStripMenuItem tsmSair;
        private System.Windows.Forms.ToolStripMenuItem tsmConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmAjuda;
        private System.Windows.Forms.ToolStripMenuItem tsmAbrir;
    }
}

