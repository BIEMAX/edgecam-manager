﻿namespace Edgecam_Manager
 {
     partial class FrmConfig_AdicionaColuna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig_AdicionaColuna));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtValorPadrao = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTipos = new System.Windows.Forms.ComboBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNomeColuna = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNomeTabela = new System.Windows.Forms.TextBox();
            this.alert = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alert)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtValorPadrao);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbTipos);
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.btnVoltar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtNomeColuna);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNomeTabela);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 166);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 118;
            this.label5.Text = "Valor padrão";
            // 
            // txtValorPadrao
            // 
            this.txtValorPadrao.Location = new System.Drawing.Point(339, 92);
            this.txtValorPadrao.Name = "txtValorPadrao";
            this.txtValorPadrao.Size = new System.Drawing.Size(264, 20);
            this.txtValorPadrao.TabIndex = 117;
            this.txtValorPadrao.Enter += new System.EventHandler(this.txtValorPadrao_Enter);
            this.txtValorPadrao.Leave += new System.EventHandler(this.txtValorPadrao_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 116;
            this.label4.Text = "Tipo de dado";
            // 
            // cbTipos
            // 
            this.cbTipos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipos.Items.AddRange(new object[] {
            "<Selecione>",
            "Inteiro",
            "Texto",
            "Caractere",
            "Data",
            "Data e hora",
            "Real",
            "Imagem",
            "Verdadeiro ou falso"});
            this.cbTipos.Location = new System.Drawing.Point(339, 39);
            this.cbTipos.Name = "cbTipos";
            this.cbTipos.Size = new System.Drawing.Size(264, 21);
            this.cbTipos.TabIndex = 115;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSalvar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(558, 132);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(66, 31);
            this.btnSalvar.TabIndex = 114;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoltar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnVoltar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnVoltar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoltar.Location = new System.Drawing.Point(479, 132);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(73, 31);
            this.btnVoltar.TabIndex = 113;
            this.btnVoltar.Text = "Retornar";
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nome da coluna";
            // 
            // txtNomeColuna
            // 
            this.txtNomeColuna.Location = new System.Drawing.Point(70, 91);
            this.txtNomeColuna.Name = "txtNomeColuna";
            this.txtNomeColuna.Size = new System.Drawing.Size(220, 20);
            this.txtNomeColuna.TabIndex = 3;
            this.txtNomeColuna.Enter += new System.EventHandler(this.txtNomeColuna_Enter);
            this.txtNomeColuna.Leave += new System.EventHandler(this.txtNomeColuna_Leave);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(28, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "USR_";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tabela";
            // 
            // txtNomeTabela
            // 
            this.txtNomeTabela.Enabled = false;
            this.txtNomeTabela.Location = new System.Drawing.Point(26, 40);
            this.txtNomeTabela.Name = "txtNomeTabela";
            this.txtNomeTabela.Size = new System.Drawing.Size(264, 20);
            this.txtNomeTabela.TabIndex = 0;
            // 
            // alert
            // 
            this.alert.ContainerControl = this;
            // 
            // FrmConfig_AdicionaColuna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(651, 190);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmConfig_AdicionaColuna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de nova coluna";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alert)).EndInit();
            this.ResumeLayout(false);

         }

         #endregion

         private System.Windows.Forms.Panel panel1;
         private System.Windows.Forms.Label label1;
         private System.Windows.Forms.TextBox txtNomeTabela;
         private System.Windows.Forms.Label label3;
         private System.Windows.Forms.TextBox txtNomeColuna;
         private System.Windows.Forms.Label label2;
         private System.Windows.Forms.Button btnVoltar;
         private System.Windows.Forms.Button btnSalvar;
         private System.Windows.Forms.Label label5;
         private System.Windows.Forms.TextBox txtValorPadrao;
         private System.Windows.Forms.Label label4;
         private System.Windows.Forms.ComboBox cbTipos;
         private System.Windows.Forms.ErrorProvider alert;
     }
 }