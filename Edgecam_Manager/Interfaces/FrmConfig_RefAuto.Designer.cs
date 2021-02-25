namespace Edgecam_Manager
{
    partial class FrmConfig_RefAuto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig_RefAuto));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txtNumZeros = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.cbxZerosDireita = new System.Windows.Forms.CheckBox();
            this.cbxZerosEsquerda = new System.Windows.Forms.CheckBox();
            this.cbxCompletarZeros = new System.Windows.Forms.CheckBox();
            this.txtValorAtual = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txtIncremento = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.txtValorFinal = new System.Windows.Forms.TextBox();
            this.label71 = new System.Windows.Forms.Label();
            this.txtValorInicial = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.txtPrefixo = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.cbColunas = new System.Windows.Forms.ComboBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.cbTabelas = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnVoltar);
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.txtNumZeros);
            this.panel1.Controls.Add(this.label74);
            this.panel1.Controls.Add(this.cbxZerosDireita);
            this.panel1.Controls.Add(this.cbxZerosEsquerda);
            this.panel1.Controls.Add(this.cbxCompletarZeros);
            this.panel1.Controls.Add(this.txtValorAtual);
            this.panel1.Controls.Add(this.label73);
            this.panel1.Controls.Add(this.txtIncremento);
            this.panel1.Controls.Add(this.label72);
            this.panel1.Controls.Add(this.txtValorFinal);
            this.panel1.Controls.Add(this.label71);
            this.panel1.Controls.Add(this.txtValorInicial);
            this.panel1.Controls.Add(this.label70);
            this.panel1.Controls.Add(this.txtPrefixo);
            this.panel1.Controls.Add(this.label69);
            this.panel1.Controls.Add(this.cbColunas);
            this.panel1.Controls.Add(this.label68);
            this.panel1.Controls.Add(this.label62);
            this.panel1.Controls.Add(this.cbTabelas);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 252);
            this.panel1.TabIndex = 0;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoltar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnVoltar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.sair_blue_16;
            this.btnVoltar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoltar.Location = new System.Drawing.Point(582, 210);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(73, 31);
            this.btnVoltar.TabIndex = 108;
            this.btnVoltar.Text = "Retornar";
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnSalvar.Image = global::Edgecam_Manager.Imagens_NewLookInterface.salvar_16;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(661, 210);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(66, 31);
            this.btnSalvar.TabIndex = 107;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtNumZeros
            // 
            this.txtNumZeros.Location = new System.Drawing.Point(566, 145);
            this.txtNumZeros.Name = "txtNumZeros";
            this.txtNumZeros.Size = new System.Drawing.Size(152, 20);
            this.txtNumZeros.TabIndex = 106;
            this.txtNumZeros.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNumZeros.TextChanged += new System.EventHandler(this.txtNumZeros_TextChanged);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(563, 116);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(123, 26);
            this.label74.TabIndex = 105;
            this.label74.Text = "Quantidade de números \r\n(completa com zero)";
            // 
            // cbxZerosDireita
            // 
            this.cbxZerosDireita.AutoSize = true;
            this.cbxZerosDireita.Location = new System.Drawing.Point(384, 147);
            this.cbxZerosDireita.Name = "cbxZerosDireita";
            this.cbxZerosDireita.Size = new System.Drawing.Size(122, 17);
            this.cbxZerosDireita.TabIndex = 104;
            this.cbxZerosDireita.Text = "Inserir zeros à direita";
            this.cbxZerosDireita.UseVisualStyleBackColor = true;
            this.cbxZerosDireita.CheckedChanged += new System.EventHandler(this.cbxZerosDireita_CheckedChanged);
            // 
            // cbxZerosEsquerda
            // 
            this.cbxZerosEsquerda.AutoSize = true;
            this.cbxZerosEsquerda.Location = new System.Drawing.Point(202, 148);
            this.cbxZerosEsquerda.Name = "cbxZerosEsquerda";
            this.cbxZerosEsquerda.Size = new System.Drawing.Size(138, 17);
            this.cbxZerosEsquerda.TabIndex = 103;
            this.cbxZerosEsquerda.Text = "Inserir zeros à esquerda";
            this.cbxZerosEsquerda.UseVisualStyleBackColor = true;
            this.cbxZerosEsquerda.CheckedChanged += new System.EventHandler(this.cbxZerosEsquerda_CheckedChanged);
            // 
            // cbxCompletarZeros
            // 
            this.cbxCompletarZeros.AutoSize = true;
            this.cbxCompletarZeros.Location = new System.Drawing.Point(20, 148);
            this.cbxCompletarZeros.Name = "cbxCompletarZeros";
            this.cbxCompletarZeros.Size = new System.Drawing.Size(124, 17);
            this.cbxCompletarZeros.TabIndex = 102;
            this.cbxCompletarZeros.Text = "Completar com zeros";
            this.cbxCompletarZeros.UseVisualStyleBackColor = true;
            this.cbxCompletarZeros.CheckedChanged += new System.EventHandler(this.cbxCompletarZeros_CheckedChanged);
            // 
            // txtValorAtual
            // 
            this.txtValorAtual.Location = new System.Drawing.Point(384, 96);
            this.txtValorAtual.Name = "txtValorAtual";
            this.txtValorAtual.Size = new System.Drawing.Size(152, 20);
            this.txtValorAtual.TabIndex = 101;
            this.txtValorAtual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtValorAtual.TextChanged += new System.EventHandler(this.txtValorAtual_TextChanged);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(381, 80);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(57, 13);
            this.label73.TabIndex = 100;
            this.label73.Text = "Valor atual";
            // 
            // txtIncremento
            // 
            this.txtIncremento.Location = new System.Drawing.Point(566, 46);
            this.txtIncremento.Name = "txtIncremento";
            this.txtIncremento.Size = new System.Drawing.Size(152, 20);
            this.txtIncremento.TabIndex = 99;
            this.txtIncremento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIncremento.TextChanged += new System.EventHandler(this.txtIncremento_TextChanged);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(563, 31);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(60, 13);
            this.label72.TabIndex = 98;
            this.label72.Text = "Incremento";
            // 
            // txtValorFinal
            // 
            this.txtValorFinal.Location = new System.Drawing.Point(202, 96);
            this.txtValorFinal.Name = "txtValorFinal";
            this.txtValorFinal.Size = new System.Drawing.Size(152, 20);
            this.txtValorFinal.TabIndex = 97;
            this.txtValorFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtValorFinal.TextChanged += new System.EventHandler(this.txtValorFinal_TextChanged);
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(199, 80);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(53, 13);
            this.label71.TabIndex = 96;
            this.label71.Text = "Valor final";
            // 
            // txtValorInicial
            // 
            this.txtValorInicial.Location = new System.Drawing.Point(20, 96);
            this.txtValorInicial.Name = "txtValorInicial";
            this.txtValorInicial.Size = new System.Drawing.Size(152, 20);
            this.txtValorInicial.TabIndex = 95;
            this.txtValorInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtValorInicial.TextChanged += new System.EventHandler(this.txtValorInicial_TextChanged);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(17, 80);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(60, 13);
            this.label70.TabIndex = 94;
            this.label70.Text = "Valor inicial";
            // 
            // txtPrefixo
            // 
            this.txtPrefixo.Location = new System.Drawing.Point(384, 47);
            this.txtPrefixo.Name = "txtPrefixo";
            this.txtPrefixo.Size = new System.Drawing.Size(152, 20);
            this.txtPrefixo.TabIndex = 93;
            this.txtPrefixo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(381, 31);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(39, 13);
            this.label69.TabIndex = 92;
            this.label69.Text = "Prefixo";
            // 
            // cbColunas
            // 
            this.cbColunas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColunas.Location = new System.Drawing.Point(202, 46);
            this.cbColunas.Name = "cbColunas";
            this.cbColunas.Size = new System.Drawing.Size(152, 21);
            this.cbColunas.TabIndex = 91;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(199, 30);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(40, 13);
            this.label68.TabIndex = 90;
            this.label68.Text = "Coluna";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(17, 30);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(40, 13);
            this.label62.TabIndex = 89;
            this.label62.Text = "Tabela";
            // 
            // cbTabelas
            // 
            this.cbTabelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTabelas.FormattingEnabled = true;
            this.cbTabelas.Location = new System.Drawing.Point(20, 46);
            this.cbTabelas.Name = "cbTabelas";
            this.cbTabelas.Size = new System.Drawing.Size(152, 21);
            this.cbTabelas.TabIndex = 88;
            this.cbTabelas.SelectedIndexChanged += new System.EventHandler(this.cbTabelas_SelectedIndexChanged);
            // 
            // FrmConfig_RefAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(763, 276);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConfig_RefAuto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de nova referência automática";
            this.Shown += new System.EventHandler(this.FrmConfig_RefAuto_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNumZeros;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.CheckBox cbxZerosDireita;
        private System.Windows.Forms.CheckBox cbxZerosEsquerda;
        private System.Windows.Forms.CheckBox cbxCompletarZeros;
        private System.Windows.Forms.TextBox txtValorAtual;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox txtIncremento;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox txtValorFinal;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txtValorInicial;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.TextBox txtPrefixo;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.ComboBox cbColunas;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.ComboBox cbTabelas;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnVoltar;
    }
}