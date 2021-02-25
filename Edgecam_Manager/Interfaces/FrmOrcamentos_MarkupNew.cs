using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    internal partial class FrmOrcamentos_MarkupNew : Form
    {

        #region Global variables

        /// <summary>
        ///     Impostos selecionados pelo usuário.
        /// </summary>
        public DataTable mDados;

        public Markup mMarkup;

        #endregion

        #region Propriedades

        public Markup _Markup
        {
            get { return mMarkup; }
        }

        #endregion

        #region Class instances

        public FrmOrcamentos_MarkupNew()
        {
            this.InitializeComponent();
            this.AddColunasDataTable();
        }

        #endregion

        #region Methods

        private void AddColunasDataTable()
        {
            mDados = new DataTable();

            mDados.Columns.Add(new DataColumn("id", typeof(int)));
            mDados.Columns.Add(new DataColumn("Imposto", typeof(string)));
            mDados.Columns.Add(new DataColumn("Valor do imposto", typeof(double)));
        }

        private void RecalculaMarkup()
        {
            double mk = 0, mkd = 0, mul = 0, mulp = 0;

            //Soma os markups.
            for(int x = 0; x < mDados.Rows.Count; x++)
            {
                mk += Convert.ToDouble(mDados.Rows[x]["Valor do imposto"].ToString());
            }

            //Soma a margem no markup;
            mk += Convert.ToDouble(txtMargem.Text.ToString());
            //Obtém o markup down.
            mkd = 100 - mk;
            //Obtém o fator multiplicador (valor)
            mul = (1 / mkd) * 100;
            //Obtém o fator multiplicador (valor em percentual)
            mulp = (mul - 1) * 100;

            this.txtMk.Text = mk.ToString();
            this.txtMkDown.Text = mkd.ToString();
            this.txtMul.Text = mul.ToString();
            this.txtMulPer.Text = mulp.ToString();
        }

        private void SalvaMarkup()
        {
            if (String.IsNullOrEmpty(txtNome.Text))
                MessageBox.Show("Você deve obrigatoriamente informar um nome de markup para salvar", 
                                "Campo não preenchido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (Objects.ExisteValorBanco("Markup", "Nome", txtNome.Text.ToString().Trim()))
                {
                    MessageBox.Show($"Já existe uma markup de nome '{txtNome.Text}'. Escolha um novo nome antes de salvar.", "Referência já existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add("@NOME", txtNome.Text);
                d.Add("@MARGEM", Convert.ToDouble(txtMargem.Text));
                d.Add("@MK", Convert.ToDouble(txtMk.Text));
                d.Add("@MKDOWN", Convert.ToDouble(txtMkDown.Text));
                d.Add("@MUL", Convert.ToDouble(txtMul.Text));
                d.Add("@MULPER", Convert.ToDouble(txtMulPer.Text));
                d.Add("@USR", Objects.UsuarioAtual.Login);

                //Preciso armazenar temporarimente para obter o ID do MARKUP
                DataTable t = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_MARKUP, d);

                for (int x = 0; x < mDados.Rows.Count; x++)
                {
                    //Reinicializo o dicionário para reutilizá-lo
                    d = new Dictionary<string, object>();
                    d.Add("@ID", t.Rows[0][0].ToString());
                    d.Add("@NOME", mDados.Rows[x]["Imposto"].ToString());
                    d.Add("@VALOR", Convert.ToDouble(mDados.Rows[x]["Valor do imposto"].ToString()));
                    d.Add("@USR", Objects.UsuarioAtual.Login);

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ADICIONA_IMPOSTOS_MARKUP, d);
                }

                mMarkup             = new Markup();
                mMarkup.Id          = Convert.ToInt16(t.Rows[0][0].ToString());
                mMarkup.Nome        = txtNome.Text;
                mMarkup.MargemLucro = Convert.ToDouble(txtMargem.Text);
                mMarkup.MarkupUp    = Convert.ToDouble(txtMk.Text);
                mMarkup.MarkupDown  = Convert.ToDouble(txtMkDown.Text);
                mMarkup.Mult        = Convert.ToDouble(txtMul.Text);
                mMarkup.MultPer     = Convert.ToDouble(txtMulPer.Text);

                MessageBox.Show("Markup cadastrado com êxito", "Sucesso ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                GC.Collect();
            }
        }

        #endregion

        #region Events

        private void btnAddIm_Click(object sender, EventArgs e)
        {
            try
            {
                FrmImpostos_Seleciona f = new FrmImpostos_Seleciona();
                f.ShowDialog();

                if (f._LstImpostos != null && f._LstImpostos.Count > 0)
                {
                    foreach(Imposto i in f._LstImpostos) mDados.Rows.Add(i.Id, i.Nome, i.ValorImposto);
                    udgv.DataSource = mDados;

                    RecalculaMarkup();
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar impostos para adicionar ao markup", "FrmOrcamentos_MarkupNew", "btnAddIm_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }
        private void txtMargem_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMargem.Text))
            {
                txtMargem.Text = CustomStrings.DeixaSomenteDecimais(this.txtMargem.Text);

                //Coloca um nome automático para facilitar a vida do usuário.
                txtNome.Text = $"Margem {txtMargem.Text.ToString().Replace(",", ".")}%";
                RecalculaMarkup();
            }
            else return;
        }

        private void cms_Comandos_Opening(object sender, CancelEventArgs e)
        {
            if(udgv.DataSource != null && udgv.Rows.Count > 0)
            {
                tsm_RemImp.Enabled = true;
                tsm_RemAllImp.Enabled = true;
            }
            else
            {
                tsm_RemImp.Enabled = false;
                tsm_RemAllImp.Enabled = false;
            }
        }

        private void tsm_RemImp_Click(object sender, EventArgs e)
        {
            if(udgv.Selected.Rows.Count > 0)
            {
                foreach(UltraGridRow r in udgv.Selected.Rows)
                {
                    r.Delete();
                }
            }
        }

        private void tsm_RemAllImp_Click(object sender, EventArgs e)
        {
            if (udgv.Rows.Count > 0)
            {
                while(udgv.Rows.Count > 0)
                {
                    udgv.Rows[0].Delete(false);
                }
            }
        }

        private void ubtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void ubtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaMarkup();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao salvar o markup", "FrmOrcamentos_MarkupNew", "ubtnSalvar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}