namespace Edgecam_Manager
{
    partial class FrmNewAppointment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewAppointment));
            groupBox1 = new System.Windows.Forms.GroupBox();
            cb_HoraFim = new System.Windows.Forms.ComboBox();
            cb_HoraInicio = new System.Windows.Forms.ComboBox();
            ultraDateTimeEditor2 = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            ultraDateTimeEditor1 = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            rtxt_Descricao = new System.Windows.Forms.RichTextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            txtTitle = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btnCadastrar = new System.Windows.Forms.Button();
            btnVoltar = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(ultraDateTimeEditor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ultraDateTimeEditor1)).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.BackColor = System.Drawing.SystemColors.Control;
            groupBox1.Controls.Add(cb_HoraFim);
            groupBox1.Controls.Add(cb_HoraInicio);
            groupBox1.Controls.Add(ultraDateTimeEditor2);
            groupBox1.Controls.Add(ultraDateTimeEditor1);
            groupBox1.Controls.Add(tabControl1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtTitle);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnCadastrar);
            groupBox1.Controls.Add(btnVoltar);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(583, 421);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // cb_HoraFim
            // 
            cb_HoraFim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cb_HoraFim.FormattingEnabled = true;
            cb_HoraFim.Items.AddRange(new object[] {
            "00:15",
            "00:30",
            "00:45",
            "01:00",
            "01:15",
            "01:30",
            "01:45",
            "02:00",
            "02:15",
            "02:30",
            "02:45",
            "03:00",
            "03:15",
            "03:30",
            "03:45",
            "04:00",
            "04:15",
            "04:30",
            "04:45",
            "05:00",
            "05:15",
            "05:30",
            "05:45",
            "06:00",
            "06:15",
            "06:30",
            "06:45",
            "07:00",
            "07:15",
            "07:30",
            "07:45",
            "08:00",
            "08:15",
            "08:30",
            "08:45",
            "09:00",
            "09:15",
            "09:30",
            "09:45",
            "10:00",
            "10:15",
            "10:30",
            "10:45",
            "11:00",
            "11:15",
            "11:30",
            "11:45",
            "12:00",
            "12:15",
            "12:30",
            "12:45",
            "13:00",
            "13:15",
            "13:30",
            "13:45",
            "14:00",
            "14:15",
            "14:30",
            "14:45",
            "15:00",
            "15:15",
            "15:30",
            "15:45",
            "16:00",
            "16:15",
            "16:30",
            "16:45",
            "17:00",
            "17:15",
            "17:30",
            "17:45",
            "18:00",
            "18:15",
            "18:30",
            "18:45",
            "19:00",
            "19:15",
            "19:30",
            "19:45",
            "20:00",
            "20:15",
            "20:30",
            "20:45",
            "21:00",
            "21:15",
            "21:30",
            "21:45",
            "22:00",
            "22:15",
            "22:30",
            "22:45",
            "23:00",
            "23:15",
            "23:30",
            "23:45",
            "00:00"});
            cb_HoraFim.Location = new System.Drawing.Point(262, 129);
            cb_HoraFim.Name = "cb_HoraFim";
            cb_HoraFim.Size = new System.Drawing.Size(125, 21);
            cb_HoraFim.TabIndex = 68;
            // 
            // cb_HoraInicio
            // 
            cb_HoraInicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cb_HoraInicio.FormattingEnabled = true;
            cb_HoraInicio.Items.AddRange(new object[] {
            "00:15",
            "00:30",
            "00:45",
            "01:00",
            "01:15",
            "01:30",
            "01:45",
            "02:00",
            "02:15",
            "02:30",
            "02:45",
            "03:00",
            "03:15",
            "03:30",
            "03:45",
            "04:00",
            "04:15",
            "04:30",
            "04:45",
            "05:00",
            "05:15",
            "05:30",
            "05:45",
            "06:00",
            "06:15",
            "06:30",
            "06:45",
            "07:00",
            "07:15",
            "07:30",
            "07:45",
            "08:00",
            "08:15",
            "08:30",
            "08:45",
            "09:00",
            "09:15",
            "09:30",
            "09:45",
            "10:00",
            "10:15",
            "10:30",
            "10:45",
            "11:00",
            "11:15",
            "11:30",
            "11:45",
            "12:00",
            "12:15",
            "12:30",
            "12:45",
            "13:00",
            "13:15",
            "13:30",
            "13:45",
            "14:00",
            "14:15",
            "14:30",
            "14:45",
            "15:00",
            "15:15",
            "15:30",
            "15:45",
            "16:00",
            "16:15",
            "16:30",
            "16:45",
            "17:00",
            "17:15",
            "17:30",
            "17:45",
            "18:00",
            "18:15",
            "18:30",
            "18:45",
            "19:00",
            "19:15",
            "19:30",
            "19:45",
            "20:00",
            "20:15",
            "20:30",
            "20:45",
            "21:00",
            "21:15",
            "21:30",
            "21:45",
            "22:00",
            "22:15",
            "22:30",
            "22:45",
            "23:00",
            "23:15",
            "23:30",
            "23:45",
            "00:00"});
            cb_HoraInicio.Location = new System.Drawing.Point(262, 90);
            cb_HoraInicio.Name = "cb_HoraInicio";
            cb_HoraInicio.Size = new System.Drawing.Size(125, 21);
            cb_HoraInicio.TabIndex = 67;
            // 
            // ultraDateTimeEditor2
            // 
            ultraDateTimeEditor2.Location = new System.Drawing.Point(107, 129);
            ultraDateTimeEditor2.Name = "ultraDateTimeEditor2";
            ultraDateTimeEditor2.Size = new System.Drawing.Size(117, 21);
            ultraDateTimeEditor2.TabIndex = 66;
            // 
            // ultraDateTimeEditor1
            // 
            ultraDateTimeEditor1.Location = new System.Drawing.Point(107, 90);
            ultraDateTimeEditor1.Name = "ultraDateTimeEditor1";
            ultraDateTimeEditor1.Size = new System.Drawing.Size(117, 21);
            ultraDateTimeEditor1.TabIndex = 65;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new System.Drawing.Point(9, 167);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(568, 200);
            tabControl1.TabIndex = 64;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(rtxt_Descricao);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(560, 174);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Descrição";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtxt_Descricao
            // 
            rtxt_Descricao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            rtxt_Descricao.Location = new System.Drawing.Point(3, 3);
            rtxt_Descricao.Name = "rtxt_Descricao";
            rtxt_Descricao.Size = new System.Drawing.Size(554, 168);
            rtxt_Descricao.TabIndex = 0;
            rtxt_Descricao.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 133);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(85, 13);
            label3.TabIndex = 62;
            label3.Text = "Horá de termino:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 94);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(77, 13);
            label2.TabIndex = 60;
            label2.Text = "Horá de início:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new System.Drawing.Point(107, 35);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new System.Drawing.Size(389, 20);
            txtTitle.TabIndex = 59;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 38);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 13);
            label1.TabIndex = 58;
            label1.Text = "Título:";
            // 
            // btnCadastrar
            // 
            btnCadastrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCadastrar.ForeColor = System.Drawing.Color.SteelBlue;
            btnCadastrar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            btnCadastrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCadastrar.Location = new System.Drawing.Point(475, 384);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new System.Drawing.Size(102, 31);
            btnCadastrar.TabIndex = 57;
            btnCadastrar.Text = "Salvar e fechar";
            btnCadastrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnCadastrar.UseVisualStyleBackColor = true;
            btnCadastrar.Click += new System.EventHandler(btnCadastrar_Click);
            // 
            // btnVoltar
            // 
            btnVoltar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnVoltar.ForeColor = System.Drawing.Color.SteelBlue;
            btnVoltar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            btnVoltar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnVoltar.Location = new System.Drawing.Point(407, 384);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.Size = new System.Drawing.Size(62, 31);
            btnVoltar.TabIndex = 56;
            btnVoltar.Text = "Voltar";
            btnVoltar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Click += new System.EventHandler(btnVoltar_Click);
            // 
            // FrmNewAppointment
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ActiveBorder;
            ClientSize = new System.Drawing.Size(607, 445);
            Controls.Add(groupBox1);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmNewAppointment";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Criar um novo agendamento";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(ultraDateTimeEditor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(ultraDateTimeEditor1)).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.ComboBox cb_HoraFim;
        private System.Windows.Forms.ComboBox cb_HoraInicio;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor ultraDateTimeEditor2;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor ultraDateTimeEditor1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxt_Descricao;
    }
}