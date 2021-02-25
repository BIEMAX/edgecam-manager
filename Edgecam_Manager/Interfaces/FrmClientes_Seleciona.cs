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
    internal partial class FrmClientes_Seleciona : Form
    {
        #region Variáveis globais

        private Cliente mCliente;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public Cliente _ClienteSelecionado
        {
            get
            {
                return mCliente;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmClientes_Seleciona()
        {
            InitializeComponent();

            ConsultaClientes();
        }

        #endregion

        #region Métodos

        private void ConsultaClientes()
        {
            udgv.DataSource = SQLQueries.Consulta_Clientes("", txtDescricao.Text);
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
                mCliente = new Cliente();
                mCliente.Id = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();
                mCliente.NomeEmpresa = udgv.Rows[e.Cell.Row.Index].Cells["Nome do cliente"].OriginalValue.ToString();
                mCliente.Cidade = udgv.Rows[e.Cell.Row.Index].Cells["Cidade"].OriginalValue.ToString();
                mCliente.Estado = udgv.Rows[e.Cell.Row.Index].Cells["Estado"].OriginalValue.ToString();
                mCliente.Pais = udgv.Rows[e.Cell.Row.Index].Cells["País"].OriginalValue.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaClientes();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar clientes", "FrmClientes_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
            GC.Collect();
        }

        #endregion
    }
}
