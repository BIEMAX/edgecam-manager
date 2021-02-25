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
    internal partial class FrmCidades_Seleciona : Form
    {
        #region Variáveis globais

        private Cidade mCidade;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public Cidade _CidadeSelecionada
        {
            get
            {
                return mCidade;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmCidades_Seleciona()
        {
            InitializeComponent();

            ConsultaCidades();
        }

        #endregion

        #region Métodos

        private void ConsultaCidades()
        {
            udgv.DataSource = SQLQueries.Consulta_Cidades(txtNome.Text, txtCliente.Text);
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
                mCidade = new Cidade();
                mCidade.id      = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();
                mCidade.NomeCidade  = udgv.Rows[e.Cell.Row.Index].Cells["Cidade"].OriginalValue.ToString();
                mCidade.Estado  = udgv.Rows[e.Cell.Row.Index].Cells["Estado"].OriginalValue.ToString();
                mCidade.Pais    = udgv.Rows[e.Cell.Row.Index].Cells["País"].OriginalValue.ToString();
                mCidade.UF      = udgv.Rows[e.Cell.Row.Index].Cells["UF"].OriginalValue.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaCidades();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar cidades", "FrmCidades_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa.
        /// </summary>
        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa.
        /// </summary>
        private void txtCliente_KeyDown(object sender, KeyEventArgs e)
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
