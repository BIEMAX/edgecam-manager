using CircularPictureBox;
namespace Edgecam_Manager
{
    partial class FrmUserData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserData));
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGestor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.linkTrocarFoto = new System.Windows.Forms.LinkLabel();
            this.linkRemoverFoto = new System.Windows.Forms.LinkLabel();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblDataExpirarSenha = new System.Windows.Forms.Label();
            this.cbxExpirarSenha = new System.Windows.Forms.CheckBox();
            this.btnTasks = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pic_ImgUser = new CircularPictureBox.CircularPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ImgUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Enabled = false;
            this.btnSalvar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSalvar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(199, 408);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(66, 31);
            this.btnSalvar.TabIndex = 56;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnReturn.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(120, 408);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(73, 31);
            this.btnReturn.TabIndex = 55;
            this.btnReturn.Text = "Retornar";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::Edgecam_Manager.Imagens_NewLookInterface.editar;
            this.btnEdit.Location = new System.Drawing.Point(10, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 24);
            this.btnEdit.TabIndex = 50;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtCargo
            // 
            this.txtCargo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCargo.Enabled = false;
            this.txtCargo.Location = new System.Drawing.Point(75, 232);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.Size = new System.Drawing.Size(194, 20);
            this.txtCargo.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 235);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Cargo";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new System.Drawing.Point(75, 202);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(194, 20);
            this.txtEmail.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "E-mail:";
            // 
            // txtGestor
            // 
            this.txtGestor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGestor.Enabled = false;
            this.txtGestor.Location = new System.Drawing.Point(75, 291);
            this.txtGestor.Name = "txtGestor";
            this.txtGestor.Size = new System.Drawing.Size(194, 20);
            this.txtGestor.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 294);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Gerente";
            // 
            // linkTrocarFoto
            // 
            this.linkTrocarFoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkTrocarFoto.AutoSize = true;
            this.linkTrocarFoto.Location = new System.Drawing.Point(214, 100);
            this.linkTrocarFoto.Name = "linkTrocarFoto";
            this.linkTrocarFoto.Size = new System.Drawing.Size(38, 13);
            this.linkTrocarFoto.TabIndex = 16;
            this.linkTrocarFoto.TabStop = true;
            this.linkTrocarFoto.Text = "Trocar";
            this.linkTrocarFoto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTrocarFoto_LinkClicked);
            // 
            // linkRemoverFoto
            // 
            this.linkRemoverFoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRemoverFoto.AutoSize = true;
            this.linkRemoverFoto.Location = new System.Drawing.Point(214, 76);
            this.linkRemoverFoto.Name = "linkRemoverFoto";
            this.linkRemoverFoto.Size = new System.Drawing.Size(50, 13);
            this.linkRemoverFoto.TabIndex = 15;
            this.linkRemoverFoto.TabStop = true;
            this.linkRemoverFoto.Text = "Remover";
            this.linkRemoverFoto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRemoverFoto_LinkClicked);
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Enabled = false;
            this.txtCompany.Location = new System.Drawing.Point(75, 260);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(194, 20);
            this.txtCompany.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Filial:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.remover_deletar;
            this.btnCancelar.Location = new System.Drawing.Point(42, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(24, 24);
            this.btnCancelar.TabIndex = 60;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblDataExpirarSenha
            // 
            this.lblDataExpirarSenha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataExpirarSenha.AutoSize = true;
            this.lblDataExpirarSenha.Location = new System.Drawing.Point(22, 387);
            this.lblDataExpirarSenha.Name = "lblDataExpirarSenha";
            this.lblDataExpirarSenha.Size = new System.Drawing.Size(35, 13);
            this.lblDataExpirarSenha.TabIndex = 59;
            this.lblDataExpirarSenha.Text = "label1";
            // 
            // cbxExpirarSenha
            // 
            this.cbxExpirarSenha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxExpirarSenha.AutoSize = true;
            this.cbxExpirarSenha.Enabled = false;
            this.cbxExpirarSenha.Location = new System.Drawing.Point(25, 367);
            this.cbxExpirarSenha.Name = "cbxExpirarSenha";
            this.cbxExpirarSenha.Size = new System.Drawing.Size(90, 17);
            this.cbxExpirarSenha.TabIndex = 58;
            this.cbxExpirarSenha.Text = "Expirar senha";
            this.cbxExpirarSenha.UseVisualStyleBackColor = true;
            // 
            // btnTasks
            // 
            this.btnTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTasks.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnTasks.Image = global::Edgecam_Manager.Properties.Resources.Email;
            this.btnTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTasks.Location = new System.Drawing.Point(75, 323);
            this.btnTasks.Name = "btnTasks";
            this.btnTasks.Size = new System.Drawing.Size(133, 38);
            this.btnTasks.TabIndex = 57;
            this.btnTasks.Text = "Tarefas não lidas";
            this.btnTasks.UseVisualStyleBackColor = true;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(22, 158);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(243, 24);
            this.lblUsuario.TabIndex = 20;
            this.lblUsuario.Text = "Usuário";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pic_ImgUser
            // 
            this.pic_ImgUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_ImgUser.Location = new System.Drawing.Point(75, 36);
            this.pic_ImgUser.Name = "pic_ImgUser";
            this.pic_ImgUser.Size = new System.Drawing.Size(133, 119);
            this.pic_ImgUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_ImgUser.TabIndex = 19;
            this.pic_ImgUser.TabStop = false;
            // 
            // FrmUserData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(277, 451);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblDataExpirarSenha);
            this.Controls.Add(this.pic_ImgUser);
            this.Controls.Add(this.cbxExpirarSenha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTasks);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.txtCargo);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtGestor);
            this.Controls.Add(this.linkRemoverFoto);
            this.Controls.Add(this.linkTrocarFoto);
            this.Controls.Add(this.label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUserData";
            this.Text = "FrmUserData";
            ((System.ComponentModel.ISupportInitialize)(this.pic_ImgUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGestor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkTrocarFoto;
        private System.Windows.Forms.LinkLabel linkRemoverFoto;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSalvar;
        private CircularPictureBox.CircularPictureBox pic_ImgUser;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnTasks;
        private System.Windows.Forms.Label lblDataExpirarSenha;
        private System.Windows.Forms.CheckBox cbxExpirarSenha;
        private System.Windows.Forms.Button btnCancelar;
    }
}