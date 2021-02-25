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
    internal partial class FrmDadosAuxiliares_Seleciona : Form
    {
        #region Variáveis globais

        private String mDadoAuxSelecionado = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém a tabela selecionada pelo usuário.
        /// </summary>
        public String _IdDadoAuxSelecionado
        {
            get
            {
                return mDadoAuxSelecionado;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmDadosAuxiliares_Seleciona()
        {
            InitializeComponent();

            ConsultaDadosAuxilirares();
        }

        #endregion

        #region Métodos

        private void SelecionaMultiplosDados()
        {
            if (udgv.Selected.Rows.Count > 0)
            {
                for (int x = 0; x < udgv.Selected.Rows.Count; x++)
                {
                    if (!String.IsNullOrEmpty(mDadoAuxSelecionado)) mDadoAuxSelecionado += ",";
                    mDadoAuxSelecionado += String.Format("'{0}'", udgv.Selected.Rows[x].Cells["id"].OriginalValue.ToString());
                }

                btnVoltar_Click(new object(), new EventArgs());
            }
            else MessageBox.Show("Você deve selecionar ao menos um dado auxiliar para utilizar essa opção", "Dado auxiliar não selecionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ConsultaDadosAuxilirares(Boolean PesquisarPorCodigo = false)
        {
            if (!PesquisarPorCodigo)
                udgv.DataSource = SQLQueries.Consulta_DadosAuxiliares("", "", SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
            else udgv.DataSource = SQLQueries.Consulta_DadosAuxiliares("", txtDescricao.Text, SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
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
                mDadoAuxSelecionado = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();

                btnVoltar_Click(new object(), new EventArgs());
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaDadosAuxilirares(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar dados auxiliares", "FrmDadosAuxiliares_Seleciona", "btnPesquisar_Click", "", "Consultas_EcMgr.CONSULTA_TODOS_DADOS_AUXILIARES", e_TipoErroEx.Erro, ex);
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

        private void tsmSelecionaMarcados_Click(object sender, EventArgs e)
        {
            SelecionaMultiplosDados();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion
    }
}
