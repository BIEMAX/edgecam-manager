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
    internal partial class FrmTarifas_Seleciona : Form
    {
        #region Variáveis globais

        private String mIdTarifa;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public String _TarifaSelecionada
        {
            get
            {
                return mIdTarifa;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmTarifas_Seleciona()
        {
            InitializeComponent();

            cbTarifasVencidas.SelectedIndex = 0;

            ConsultaTarifas();
        }

        #endregion

        #region Métodos

        private void ConsultaTarifas()
        {
            Nullable<Boolean> vencidas = null;

            if (cbTarifasVencidas.SelectedIndex == 0) vencidas = null;
            else if (cbTarifasVencidas.SelectedIndex == 1) vencidas = true;
            else vencidas = false;

            udgv.DataSource = SQLQueries.Consulta_Tarifas("", txtNome.Text, vencidas, SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
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
                mIdTarifa = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaTarifas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar tarifas", "FrmTarifas_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa.
        /// </summary>
        private void txtNome_KeyDown(object sender, KeyEventArgs e)
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
