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
    internal partial class FrmCotacoesDiarias_Seleciona : Form
    {
        #region Variáveis globais

        private String mCotacao;
        private String mMoeda;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public String _CotacaoSelecionada
        {
            get
            {
                return mCotacao;
            }
        }

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public String _MoedaSelecionada
        {
            get
            {
                return mMoeda;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto que permite selecionar o valor de uma cotação
        /// diária pesquisada pelo sistema.
        /// </summary>
        /// <param name="NomeMoeda">Nome da moeda caso o usuário já tenha a selecionado previamente.</param>
        public FrmCotacoesDiarias_Seleciona(String NomeMoeda)
        {
            InitializeComponent();

            udt.Enabled = false;
            txtMoeda.Text = NomeMoeda;

            ConsultaCotacoesDiarias();
        }

        #endregion

        #region Métodos

        private void ConsultaCotacoesDiarias()
        {
            udgv.DataSource = SQLQueries.Consulta_CotacoesDiarias(txtMoeda.Text, cbxUsarData.Checked == true ? udt.DateTime.Date.ToString("yyyy-MM-dd") : "");
        }

        #endregion

        #region Eventos

        private void udgv_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR")
            {
                return;
            }
            else
            {
                mCotacao = udgv.Rows[e.Cell.Row.Index].Cells["Valor da cotação"].OriginalValue.ToString().Split(new char[] { '=' })[1].Replace(" Real brasileiro", "").Trim();
                mMoeda = udgv.Rows[e.Cell.Row.Index].Cells["Moeda"].OriginalValue.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaCotacoesDiarias();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar moedas", "FrmMoedas_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa.
        /// </summary>
        private void txtMoeda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        private void cbxUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarData.Checked) udt.Enabled = true;
            else udt.Enabled = false;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
            GC.Collect();
        }

        #endregion
    }
}
