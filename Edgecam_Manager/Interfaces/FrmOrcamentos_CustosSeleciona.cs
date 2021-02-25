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
    public partial class FrmOrcamentos_CustosSeleciona : Form
    {
        #region Variáveis globais

        private String mCustoAdicionalSelecionado = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém a tabela selecionada pelo usuário.
        /// </summary>
        public String _IdCustoAdicionalSelecionado
        {
            get
            {
                return mCustoAdicionalSelecionado;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmOrcamentos_CustosSeleciona()
        {
            InitializeComponent();
            ConsultaCustosAdicionais();
        }

        #endregion

        #region Métodos

        private void ConsultaCustosAdicionais(Boolean PesquisarPorCodigo = false)
        {
            if (!PesquisarPorCodigo)
                udgv.DataSource = SQLQueries.Consulta_CustosAdicionais("", "", SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
            else udgv.DataSource = SQLQueries.Consulta_CustosAdicionais("", txtDescricao.Text, SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
        }

        private void SelecionaMultiplosCustos()
        {
            if (udgv.Selected.Rows.Count > 0)
            {
                for (int x = 0; x < udgv.Selected.Rows.Count; x++)
                {
                    if (!String.IsNullOrEmpty(mCustoAdicionalSelecionado)) mCustoAdicionalSelecionado += ",";
                    mCustoAdicionalSelecionado += String.Format("'{0}'", udgv.Selected.Rows[x].Cells["id"].OriginalValue.ToString());
                }

                btnVoltar_Click(new object(), new EventArgs());
            }
            else MessageBox.Show("Você deve selecionar ao menos um custo adicional para utilizar essa opção", "Custo não selecionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                mCustoAdicionalSelecionado = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();

                Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaCustosAdicionais(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar custos adicionais", "FrmOrcamentos_CustosSeleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa as peças.
        /// </summary>
        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void tsmSelecionaMarcados_Click(object sender, EventArgs e)
        {
            SelecionaMultiplosCustos();
        }

        #endregion
    }
}