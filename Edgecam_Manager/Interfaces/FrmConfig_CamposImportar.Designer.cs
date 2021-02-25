﻿namespace Edgecam_Manager
 {
     partial class FrmConfig_CamposImportar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig_CamposImportar));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSalva = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValorPadrao = new System.Windows.Forms.TextBox();
            this.cbxAceitarNull = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtElementoXml = new System.Windows.Forms.TextBox();
            this.cbColunas = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
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
            this.panel1.Controls.Add(this.btnSalva);
            this.panel1.Controls.Add(this.btnVoltar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtValorPadrao);
            this.panel1.Controls.Add(this.cbxAceitarNull);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtElementoXml);
            this.panel1.Controls.Add(this.cbColunas);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 206);
            this.panel1.TabIndex = 0;
            // 
            // btnSalva
            // 
            this.btnSalva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalva.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSalva.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            this.btnSalva.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalva.Location = new System.Drawing.Point(387, 163);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(66, 31);
            this.btnSalva.TabIndex = 111;
            this.btnSalva.Text = "Salvar";
            this.btnSalva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoltar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnVoltar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnVoltar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoltar.Location = new System.Drawing.Point(308, 163);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(73, 31);
            this.btnVoltar.TabIndex = 110;
            this.btnVoltar.Text = "Retornar";
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 80;
            this.label2.Text = "Valor padrão opcional (caso vier vazio)";
            // 
            // txtValorPadrao
            // 
            this.txtValorPadrao.Location = new System.Drawing.Point(21, 119);
            this.txtValorPadrao.Name = "txtValorPadrao";
            this.txtValorPadrao.Size = new System.Drawing.Size(188, 20);
            this.txtValorPadrao.TabIndex = 79;
            this.txtValorPadrao.Enter += new System.EventHandler(this.txtValorPadrao_Enter);
            this.txtValorPadrao.Leave += new System.EventHandler(this.txtValorPadrao_Leave);
            // 
            // cbxAceitarNull
            // 
            this.cbxAceitarNull.AutoSize = true;
            this.cbxAceitarNull.Location = new System.Drawing.Point(21, 82);
            this.cbxAceitarNull.Name = "cbxAceitarNull";
            this.cbxAceitarNull.Size = new System.Drawing.Size(129, 17);
            this.cbxAceitarNull.TabIndex = 78;
            this.cbxAceitarNull.Text = "Aceitar valores vazios";
            this.cbxAceitarNull.UseVisualStyleBackColor = true;
            this.cbxAceitarNull.CheckedChanged += new System.EventHandler(this.cbxAceitarNull_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 77;
            this.label1.Text = "Nome do elemento no xml";
            // 
            // txtElementoXml
            // 
            this.txtElementoXml.Location = new System.Drawing.Point(254, 38);
            this.txtElementoXml.Name = "txtElementoXml";
            this.txtElementoXml.Size = new System.Drawing.Size(188, 20);
            this.txtElementoXml.TabIndex = 76;
            this.txtElementoXml.Enter += new System.EventHandler(this.txtElementoXml_Enter);
            this.txtElementoXml.Leave += new System.EventHandler(this.txtElementoXml_Leave);
            // 
            // cbColunas
            // 
            this.cbColunas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColunas.FormattingEnabled = true;
            this.cbColunas.Location = new System.Drawing.Point(21, 38);
            this.cbColunas.Name = "cbColunas";
            this.cbColunas.Size = new System.Drawing.Size(188, 21);
            this.cbColunas.TabIndex = 75;
            this.cbColunas.Enter += new System.EventHandler(this.cbColunas_Enter);
            this.cbColunas.Leave += new System.EventHandler(this.cbColunas_Leave);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 24);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 74;
            this.label19.Text = "Coluna";
            // 
            // alert
            // 
            this.alert.ContainerControl = this;
            // 
            // FrmConfig_CamposImportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(489, 230);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(505, 269);
            this.Name = "FrmConfig_CamposImportar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adição de campo para importação de ordens de produção";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alert)).EndInit();
            this.ResumeLayout(false);

         }

         #endregion

         private System.Windows.Forms.Panel panel1;
         private System.Windows.Forms.ComboBox cbColunas;
         private System.Windows.Forms.Label label19;
         private System.Windows.Forms.Label label2;
         private System.Windows.Forms.TextBox txtValorPadrao;
         private System.Windows.Forms.CheckBox cbxAceitarNull;
         private System.Windows.Forms.Label label1;
         private System.Windows.Forms.TextBox txtElementoXml;
         private System.Windows.Forms.Button btnVoltar;
         private System.Windows.Forms.Button btnSalva;
         private System.Windows.Forms.ErrorProvider alert;
     }
 }