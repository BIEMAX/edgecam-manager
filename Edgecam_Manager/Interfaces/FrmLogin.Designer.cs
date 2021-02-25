namespace Edgecam_Manager
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel_ReqLogin = new System.Windows.Forms.LinkLabel();
            this.btnCfgDbs = new System.Windows.Forms.Button();
            this.txtLoginFailed = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.cbxManterConectado = new System.Windows.Forms.CheckBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.linkLabel_ForgotPassword = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.linkLabel_ReqLogin);
            this.groupBox1.Controls.Add(this.btnCfgDbs);
            this.groupBox1.Controls.Add(this.txtLoginFailed);
            this.groupBox1.Controls.Add(this.txtSenha);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.cbxManterConectado);
            this.groupBox1.Controls.Add(this.btnIniciar);
            this.groupBox1.Controls.Add(this.linkLabel_ForgotPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // linkLabel_ReqLogin
            // 
            resources.ApplyResources(this.linkLabel_ReqLogin, "linkLabel_ReqLogin");
            this.linkLabel_ReqLogin.Name = "linkLabel_ReqLogin";
            this.linkLabel_ReqLogin.TabStop = true;
            this.linkLabel_ReqLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ReqLogin_LinkClicked);
            // 
            // btnCfgDbs
            // 
            resources.ApplyResources(this.btnCfgDbs, "btnCfgDbs");
            this.btnCfgDbs.Image = global::Edgecam_Manager.Imagens_NewLookInterface.backup_banco_de_dados_16;
            this.btnCfgDbs.Name = "btnCfgDbs";
            this.btnCfgDbs.UseVisualStyleBackColor = true;
            this.btnCfgDbs.Click += new System.EventHandler(this.btnCfgDbs_Click);
            // 
            // txtLoginFailed
            // 
            this.txtLoginFailed.BackColor = System.Drawing.SystemColors.Control;
            this.txtLoginFailed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLoginFailed.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.txtLoginFailed, "txtLoginFailed");
            this.txtLoginFailed.Name = "txtLoginFailed";
            // 
            // txtSenha
            // 
            resources.ApplyResources(this.txtSenha, "txtSenha");
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.Name = "txtUser";
            // 
            // cbxManterConectado
            // 
            resources.ApplyResources(this.cbxManterConectado, "cbxManterConectado");
            this.cbxManterConectado.Name = "cbxManterConectado";
            this.cbxManterConectado.UseVisualStyleBackColor = true;
            // 
            // btnIniciar
            // 
            resources.ApplyResources(this.btnIniciar, "btnIniciar");
            this.btnIniciar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnIniciar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.selecionar_selecionado_16;
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // linkLabel_ForgotPassword
            // 
            resources.ApplyResources(this.linkLabel_ForgotPassword, "linkLabel_ForgotPassword");
            this.linkLabel_ForgotPassword.Name = "linkLabel_ForgotPassword";
            this.linkLabel_ForgotPassword.TabStop = true;
            this.linkLabel_ForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ForgotPassword_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel_ForgotPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtSenha;
        public System.Windows.Forms.TextBox txtUser;
        public System.Windows.Forms.CheckBox cbxManterConectado;
        public System.Windows.Forms.Button btnIniciar;
        public System.Windows.Forms.TextBox txtLoginFailed;
        private System.Windows.Forms.Button btnCfgDbs;
        private System.Windows.Forms.LinkLabel linkLabel_ReqLogin;
    }
}