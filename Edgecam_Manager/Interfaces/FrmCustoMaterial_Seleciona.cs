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
    internal partial class FrmCustoMaterial_Seleciona : Form
    {
        #region Variáveis globais

        private String mMaterial;
        private String mDensidade;
        private String mPreco;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém o material selecionado pelo cliente.
        /// </summary>
        public String _MaterialSelecionado
        {
            get
            {
                return mMaterial;
            }
        }

        /// <summary>
        ///     Contém a densidade do material selecionado pelo cliente.
        /// </summary>
        public String _DensidadeSelecionado
        {
            get
            {
                return mDensidade;
            }
        }

        /// <summary>
        ///     Contém o preço por quilo selecionado pelo cliente.
        /// </summary>
        public string _PrecoSelecionado
        {
            get
            {
                return mPreco;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto que permite selecionar o valor de uma cotação
        /// diária pesquisada pelo sistema.
        /// </summary>
        /// <param name="NomeMoeda">Nome da moeda caso o usuário já tenha a selecionado previamente.</param>
        public FrmCustoMaterial_Seleciona()
        {
            InitializeComponent();

            ConsultaCustosMateriais();
            this.udt.Enabled = false;
        }

        #endregion

        #region Métodos

        private void ConsultaCustosMateriais()
        {
            udgv.DataSource = SQLQueries.Consulta_PrecosMateriais(txtMaterial.Text, txtFornecedor.Text, cbxUsarData.Checked == true ? udt.DateTime.Date.ToString("yyyy-MM-dd") : "", true);
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
                mMaterial = udgv.Rows[e.Cell.Row.Index].Cells["Material"].OriginalValue.ToString();
                mDensidade = udgv.Rows[e.Cell.Row.Index].Cells["Densidade"].OriginalValue.ToString();
                mPreco = udgv.Rows[e.Cell.Row.Index].Cells["Preço por quilo"].OriginalValue.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaCustosMateriais();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os custos dos materiais", "FrmCustoMaterial_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
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

        private void txtFornecedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        private void cbxUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarData.Checked) this.udt.Enabled = true;
            else this.udt.Enabled = false;
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