namespace Edgecam_Manager
{
    partial class FrmLogin_UserCnns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin_UserCnns));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_Cn_EcUser = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Cn_EcPass = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Cn_DbEc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Cn_SqlEc = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Cn_EcMgrUser = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Cn_EcMgrPass = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_Cn_DbEcMgr = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Cn_SqlEcMgr = new System.Windows.Forms.TextBox();
            this.alert = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Cn_EcMgrLogUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Cn_EcMgrLogPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Cn_DbEcMgrLog = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Cn_SqlEcMgrLog = new System.Windows.Forms.TextBox();
            this.btn_Test_EcMgrLog = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btn_Test_EcMgr = new System.Windows.Forms.Button();
            this.btn_Test_Ec = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alert)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btn_Test_Ec);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txt_Cn_EcUser);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_Cn_EcPass);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_Cn_DbEc);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_Cn_SqlEc);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 155);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servidor do banco de dados do Edgecam:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 13);
            this.label13.TabIndex = 43;
            this.label13.Text = "Usuário para conexão";
            // 
            // txt_Cn_EcUser
            // 
            this.txt_Cn_EcUser.Location = new System.Drawing.Point(6, 90);
            this.txt_Cn_EcUser.Name = "txt_Cn_EcUser";
            this.txt_Cn_EcUser.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_EcUser.TabIndex = 3;
            this.txt_Cn_EcUser.Enter += new System.EventHandler(this.txt_Cn_EcUser_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(290, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 41;
            this.label12.Text = "Senha do usuário";
            // 
            // txt_Cn_EcPass
            // 
            this.txt_Cn_EcPass.Location = new System.Drawing.Point(293, 90);
            this.txt_Cn_EcPass.Name = "txt_Cn_EcPass";
            this.txt_Cn_EcPass.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_EcPass.TabIndex = 4;
            this.txt_Cn_EcPass.Enter += new System.EventHandler(this.txt_Cn_EcPass_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(290, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Banco de dados do Edgecam";
            // 
            // txt_Cn_DbEc
            // 
            this.txt_Cn_DbEc.Location = new System.Drawing.Point(293, 35);
            this.txt_Cn_DbEc.Name = "txt_Cn_DbEc";
            this.txt_Cn_DbEc.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_DbEc.TabIndex = 2;
            this.txt_Cn_DbEc.Enter += new System.EventHandler(this.txt_Cn_DbEc_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "Servidor do Edgecam";
            // 
            // txt_Cn_SqlEc
            // 
            this.txt_Cn_SqlEc.Location = new System.Drawing.Point(6, 35);
            this.txt_Cn_SqlEc.Name = "txt_Cn_SqlEc";
            this.txt_Cn_SqlEc.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_SqlEc.TabIndex = 1;
            this.txt_Cn_SqlEc.Enter += new System.EventHandler(this.txt_Cn_SqlEc_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.btn_Test_EcMgr);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txt_Cn_EcMgrUser);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txt_Cn_EcMgrPass);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txt_Cn_DbEcMgr);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txt_Cn_SqlEcMgr);
            this.groupBox2.Location = new System.Drawing.Point(12, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 151);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Servidor do banco de dados do Edgecam Manager:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 42;
            this.label10.Text = "Usuário para conexão";
            // 
            // txt_Cn_EcMgrUser
            // 
            this.txt_Cn_EcMgrUser.Location = new System.Drawing.Point(6, 88);
            this.txt_Cn_EcMgrUser.Name = "txt_Cn_EcMgrUser";
            this.txt_Cn_EcMgrUser.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_EcMgrUser.TabIndex = 8;
            this.txt_Cn_EcMgrUser.Enter += new System.EventHandler(this.txt_Cn_EcMgrUser_Enter);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(290, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 13);
            this.label14.TabIndex = 40;
            this.label14.Text = "Senha do usuário";
            // 
            // txt_Cn_EcMgrPass
            // 
            this.txt_Cn_EcMgrPass.Location = new System.Drawing.Point(293, 88);
            this.txt_Cn_EcMgrPass.Name = "txt_Cn_EcMgrPass";
            this.txt_Cn_EcMgrPass.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_EcMgrPass.TabIndex = 9;
            this.txt_Cn_EcMgrPass.Enter += new System.EventHandler(this.txt_Cn_EcMgrPass_Enter);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(290, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(193, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Banco de dados do Edgecam Manager";
            // 
            // txt_Cn_DbEcMgr
            // 
            this.txt_Cn_DbEcMgr.Location = new System.Drawing.Point(293, 36);
            this.txt_Cn_DbEcMgr.Name = "txt_Cn_DbEcMgr";
            this.txt_Cn_DbEcMgr.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_DbEcMgr.TabIndex = 7;
            this.txt_Cn_DbEcMgr.Enter += new System.EventHandler(this.txt_Cn_DbEcMgr_Enter);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(154, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Servidor do Edgecam Manager";
            // 
            // txt_Cn_SqlEcMgr
            // 
            this.txt_Cn_SqlEcMgr.Location = new System.Drawing.Point(6, 36);
            this.txt_Cn_SqlEcMgr.Name = "txt_Cn_SqlEcMgr";
            this.txt_Cn_SqlEcMgr.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_SqlEcMgr.TabIndex = 6;
            this.txt_Cn_SqlEcMgr.Enter += new System.EventHandler(this.txt_Cn_SqlEcMgr_Enter);
            // 
            // alert
            // 
            this.alert.ContainerControl = this;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.btn_Test_EcMgrLog);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnReturn);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txt_Cn_EcMgrLogUser);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txt_Cn_EcMgrLogPass);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txt_Cn_DbEcMgrLog);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_Cn_SqlEcMgrLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 333);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(531, 188);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Servidor do banco de dados de logs do Edgecam Manager:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Usuário para conexão";
            // 
            // txt_Cn_EcMgrLogUser
            // 
            this.txt_Cn_EcMgrLogUser.Location = new System.Drawing.Point(6, 88);
            this.txt_Cn_EcMgrLogUser.Name = "txt_Cn_EcMgrLogUser";
            this.txt_Cn_EcMgrLogUser.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_EcMgrLogUser.TabIndex = 8;
            this.txt_Cn_EcMgrLogUser.Enter += new System.EventHandler(this.txt_Cn_EcMgrLogUser_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Senha do usuário";
            // 
            // txt_Cn_EcMgrLogPass
            // 
            this.txt_Cn_EcMgrLogPass.Location = new System.Drawing.Point(293, 88);
            this.txt_Cn_EcMgrLogPass.Name = "txt_Cn_EcMgrLogPass";
            this.txt_Cn_EcMgrLogPass.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_EcMgrLogPass.TabIndex = 9;
            this.txt_Cn_EcMgrLogPass.Enter += new System.EventHandler(this.txt_Cn_EcMgrLogPass_Enter);
            this.txt_Cn_EcMgrLogPass.Leave += new System.EventHandler(this.txt_Cn_EcMgrLogPass_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Banco de dados de Log";
            // 
            // txt_Cn_DbEcMgrLog
            // 
            this.txt_Cn_DbEcMgrLog.Location = new System.Drawing.Point(293, 36);
            this.txt_Cn_DbEcMgrLog.Name = "txt_Cn_DbEcMgrLog";
            this.txt_Cn_DbEcMgrLog.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_DbEcMgrLog.TabIndex = 7;
            this.txt_Cn_DbEcMgrLog.Enter += new System.EventHandler(this.txt_Cn_DbEcMgrLog_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Servidor de Log";
            // 
            // txt_Cn_SqlEcMgrLog
            // 
            this.txt_Cn_SqlEcMgrLog.Location = new System.Drawing.Point(6, 36);
            this.txt_Cn_SqlEcMgrLog.Name = "txt_Cn_SqlEcMgrLog";
            this.txt_Cn_SqlEcMgrLog.Size = new System.Drawing.Size(221, 20);
            this.txt_Cn_SqlEcMgrLog.TabIndex = 6;
            this.txt_Cn_SqlEcMgrLog.Enter += new System.EventHandler(this.txt_Cn_SqlEcMgrLog_Enter);
            // 
            // btn_Test_EcMgrLog
            // 
            this.btn_Test_EcMgrLog.ForeColor = System.Drawing.Color.SteelBlue;
            this.btn_Test_EcMgrLog.Image = global::Edgecam_Manager.Imagens_NewLookInterface.backup_banco_de_dados_16;
            this.btn_Test_EcMgrLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Test_EcMgrLog.Location = new System.Drawing.Point(6, 114);
            this.btn_Test_EcMgrLog.Name = "btn_Test_EcMgrLog";
            this.btn_Test_EcMgrLog.Size = new System.Drawing.Size(108, 31);
            this.btn_Test_EcMgrLog.TabIndex = 10;
            this.btn_Test_EcMgrLog.Text = "Testar conexão";
            this.btn_Test_EcMgrLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Test_EcMgrLog.UseVisualStyleBackColor = true;
            this.btn_Test_EcMgrLog.Click += new System.EventHandler(this.btn_Test_EcMgrLog_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(459, 151);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 31);
            this.btnSave.TabIndex = 11;
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
            this.btnReturn.Location = new System.Drawing.Point(375, 151);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 31);
            this.btnReturn.TabIndex = 12;
            this.btnReturn.Text = "Retornar";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btn_Test_EcMgr
            // 
            this.btn_Test_EcMgr.ForeColor = System.Drawing.Color.SteelBlue;
            this.btn_Test_EcMgr.Image = global::Edgecam_Manager.Imagens_NewLookInterface.backup_banco_de_dados_16;
            this.btn_Test_EcMgr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Test_EcMgr.Location = new System.Drawing.Point(6, 114);
            this.btn_Test_EcMgr.Name = "btn_Test_EcMgr";
            this.btn_Test_EcMgr.Size = new System.Drawing.Size(108, 31);
            this.btn_Test_EcMgr.TabIndex = 10;
            this.btn_Test_EcMgr.Text = "Testar conexão";
            this.btn_Test_EcMgr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Test_EcMgr.UseVisualStyleBackColor = true;
            this.btn_Test_EcMgr.Click += new System.EventHandler(this.btn_Test_EcMgr_Click);
            // 
            // btn_Test_Ec
            // 
            this.btn_Test_Ec.ForeColor = System.Drawing.Color.SteelBlue;
            this.btn_Test_Ec.Image = global::Edgecam_Manager.Imagens_NewLookInterface.backup_banco_de_dados_16;
            this.btn_Test_Ec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Test_Ec.Location = new System.Drawing.Point(6, 116);
            this.btn_Test_Ec.Name = "btn_Test_Ec";
            this.btn_Test_Ec.Size = new System.Drawing.Size(108, 31);
            this.btn_Test_Ec.TabIndex = 5;
            this.btn_Test_Ec.Text = "Testar conexão";
            this.btn_Test_Ec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Test_Ec.UseVisualStyleBackColor = true;
            this.btn_Test_Ec.Click += new System.EventHandler(this.btn_Test_Ec_Click);
            // 
            // FrmLogin_UserCnns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(555, 533);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(571, 624);
            this.MinimizeBox = false;
            this.Name = "FrmLogin_UserCnns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração do servidor";
            this.Load += new System.EventHandler(this.FrmUserConfigCnns_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alert)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btn_Test_Ec;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_Cn_EcUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Cn_EcPass;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Cn_DbEc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Cn_SqlEc;
        private System.Windows.Forms.Button btn_Test_EcMgr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Cn_EcMgrUser;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Cn_EcMgrPass;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_Cn_DbEcMgr;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Cn_SqlEcMgr;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider alert;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Test_EcMgrLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Cn_EcMgrLogUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Cn_EcMgrLogPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Cn_DbEcMgrLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Cn_SqlEcMgrLog;
    }
}