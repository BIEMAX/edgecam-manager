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
    public partial class FrmArquivos_Seleciona : Form
    {
        #region Variáveis globais

        private String mIdArqSelecionado = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém a tabela selecionada pelo usuário.
        /// </summary>
        public String _IdArqSelecionado
        {
            get
            {
                return mIdArqSelecionado;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmArquivos_Seleciona()
        {
            InitializeComponent();

            ConsultaArquivosAuxiliares();
        }

        #endregion

        #region Métodos

        private void ConsultaArquivosAuxiliares(Boolean PesquisarPorCodigo = false)
        {
            if (!PesquisarPorCodigo)
            {
                udgv.DataSource = SQLQueries.Consulta_ArquivosAuxiliares("", "", SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
                udgv.DisplayLayout.Bands[0].Columns["Arquivo"].PerformAutoResize();
            }
            else
            {
                udgv.DataSource = SQLQueries.Consulta_ArquivosAuxiliares("", txtDescricao.Text, SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
                udgv.DisplayLayout.Bands[0].Columns["Arquivo"].PerformAutoResize();
            }
        }

        private void SelecionaMultiplosArquivos()
        {
            if (udgv.Selected.Rows.Count > 0)
            {
                for (int x = 0; x < udgv.Selected.Rows.Count; x++)
                {
                    if (!String.IsNullOrEmpty(mIdArqSelecionado)) mIdArqSelecionado += ",";
                    mIdArqSelecionado += String.Format("'{0}'", udgv.Selected.Rows[x].Cells["id"].OriginalValue.ToString());
                }

                btnVoltar_Click(new object(), new EventArgs());
            }
            else MessageBox.Show("Você deve selecionar ao menos um arquivo para utilizar essa opção", "Arquivo não selecionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                mIdArqSelecionado = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();

                btnVoltar_Click(new object(), new EventArgs());
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaArquivosAuxiliares(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar dados auxiliares", "FrmDadosAuxiliares_Seleciona", "btnPesquisar_Click", "", "Consultas_EcMgr.CONSULTA_TODOS_DADOS_AUXILIARES", e_TipoErroEx.Erro, ex);
            }
        }

        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        private void tsmSelecionaMarcados_Click(object sender, EventArgs e)
        {
            SelecionaMultiplosArquivos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion

    }
}