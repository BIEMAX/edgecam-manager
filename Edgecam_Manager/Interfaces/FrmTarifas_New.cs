using ImagedComboBox;
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
    internal partial class FrmTarifas_New : Form
    {

        #region Variáveis globais

        private String mIdNovaTarifa;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém o ID da tarifa recém criada.
        /// </summary>
        public String _IdNovaTarifa
        {
            get
            {
                return mIdNovaTarifa;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmTarifas_New()
        {
            InitializeComponent();

            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Inicializa os valores 'padrões' na interface (controles de filtros por exemplo).
        /// </summary>
        private void InicializaValoresDefault()
        {
            //ComboBox de prioridades
            cbPrioridade.Items.Add(new ComboBoxItem("Sem prioridade"));
            cbPrioridade.Items.Add(new ComboBoxItem("Baixa", Edgecam_Manager.Properties.Resources.p_baixa));
            cbPrioridade.Items.Add(new ComboBoxItem("Normal (Médio)", Edgecam_Manager.Properties.Resources.p_normal));
            cbPrioridade.Items.Add(new ComboBoxItem("Alta", Edgecam_Manager.Properties.Resources.p_alta));
            cbPrioridade.SelectedIndex = 0;//Sempre selecionado 'sem prioridade'

            udtDataValidade.Enabled = false;
            udtDataValidade.DateTime = DateTime.Now;

            txtValor.Text = "0.0";
            txtLucro.Text = "0.0";
            txtVersao.Enabled = false;
        }

        /// <summary>
        ///     Método que cadastra uma nova tarifa no banco de dados intermediário.
        /// </summary>
        private void CadastraNovaTarifa()
        {
            if (CamposObrigatoriosPreenchidos())
            {
                Dictionary<String, Object> dic = new Dictionary<string, object>();
                dic.Add("@NOME", txtNome.Text);
                dic.Add("@DESC", txtDescricao.Text);
                dic.Add("@PRIO", cbPrioridade.SelectedIndex);
                dic.Add("@VALOR", Convert.ToDouble(txtValor.Text));
                dic.Add("@LUCRO", Convert.ToDouble(txtLucro.Text));
                dic.Add("@HASVALIDITY", cbxUsarValidade.Checked);
                dic.Add("@DTEXPIRY", cbxUsarValidade.Checked ? udtDataValidade.DateTime.ToString("yyyy-MM-dd") : DBNull.Value.ToString());
                dic.Add("@CTRLVER", cbxControlarVersao.Checked);
                dic.Add("@VER", cbxControlarVersao.Checked ? txtVersao.Text : "");
                dic.Add("@USR", Objects.UsuarioAtual.Login);

                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVA_TARIFA, dic);

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    mIdNovaTarifa = dt.Rows[0]["id"].ToString();

                //    btnReturn_Click(new object(), new EventArgs());
                //}

                //deixei sem IF pois, na teoria, a situação acima nunca deverá ocorrer (dt.Rows.Count == 0).
                mIdNovaTarifa = dt.Rows[0]["id"].ToString();
                btnReturn_Click(new object(), new EventArgs());
            }
            else
            {
                MessageBox.Show("Campos obrigatórios não preenchidos, favor, revisá-los", "Campos não preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        ///     Método que valida se os campos obrigatórios foram preenchidos.
        /// </summary>
        /// <returns>True caso todos os campos obrigatórios tenham sido preenchido</returns>
        private Boolean CamposObrigatoriosPreenchidos()
        {
            int emptyFields = 0;

            try
            {
                emptyFields += String.IsNullOrEmpty(txtNome.Text) ? +1 : +0;
                emptyFields += String.IsNullOrEmpty(txtValor.Text) ? +1 : +0;
                emptyFields += String.IsNullOrEmpty(txtLucro.Text) ? +1 : +0;

                return emptyFields == 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Eventos

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            txtValor.Text = CustomStrings.DeixaSomenteDecimais(txtValor.Text);
        }

        private void txtLucro_TextChanged(object sender, EventArgs e)
        {
            txtLucro.Text = CustomStrings.DeixaSomenteDecimais(txtLucro.Text);
        }

        private void cbxUsarValidade_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarValidade.Checked)
                udtDataValidade.Enabled = true;
            else udtDataValidade.Enabled = false;
        }

        private void cbxControlarVersao_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxControlarVersao.Checked)
                txtVersao.Enabled = true;
            else txtVersao.Enabled = false;
            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovaTarifa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar nova tarifa", "FrmTarifas_New", "btnSalvar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

    }
}