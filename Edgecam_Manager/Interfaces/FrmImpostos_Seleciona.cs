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
    internal partial class FrmImpostos_Seleciona : Form
    {
        #region Variáveis globais

        private List<Imposto> mLstImpostos;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public List<Imposto> _LstImpostos
        {
            get
            {
                return mLstImpostos;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instanciar quando for somente obter o ID
        /// </summary>
        public FrmImpostos_Seleciona()
        {
            InitializeComponent();

            cbImpostosVencidos.SelectedIndex = 0;

            ConsultaImpostos();
        }

        #endregion

        #region Métodos

        private void ConsultaImpostos()
        {
            Nullable<Boolean> vencidas = null;

            if (cbImpostosVencidos.SelectedIndex == 0) vencidas = null;
            else if (cbImpostosVencidos.SelectedIndex == 1) vencidas = true;
            else vencidas = false;

            udgv.DataSource = SQLQueries.Consulta_Impostos("", txtNome.Text, vencidas, SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
        }

        private void SelecionaMultiplosDados()
        {
            if (udgv.Selected.Rows.Count > 0)
            {
                mLstImpostos = new List<Imposto>();

                for (int x = 0; x < udgv.Selected.Rows.Count; x++)
                {
                    mLstImpostos.Add(new Imposto()
                    {
                        Id              = Convert.ToInt16(udgv.Selected.Rows[x].Cells["id"].OriginalValue.ToString()),
                        Nome            = udgv.Selected.Rows[x].Cells["Imposto"].OriginalValue.ToString(),
                        TemValidade     = Convert.ToBoolean(udgv.Selected.Rows[x].Cells["TemValidade"].OriginalValue.ToString()),
                        ValidoAte       = Convert.ToDateTime(udgv.Selected.Rows[x].Cells["Válido até"].OriginalValue.ToString()),
                        TipoImposto     = Convert.ToInt16(udgv.Selected.Rows[x].Cells["TipoImposto"].OriginalValue.ToString()),
                        ValorImposto    = Convert.ToDouble(udgv.Selected.Rows[x].Cells["ValorImposto"].OriginalValue.ToString())
                    });
                }

                this.Close();
                GC.Collect();
            }
            else MessageBox.Show("Você deve selecionar ao menos um dado auxiliar para utilizar essa opção", "Dado auxiliar não selecionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Eventos

        private void tsmSelecionaMarcados_Click(object sender, EventArgs e)
        {
            SelecionaMultiplosDados();
        }

        private void udgv_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR") return;
            else
            {
                mLstImpostos = new List<Imposto>();
                mLstImpostos.Add(new Imposto()
                {
                    Id              = Convert.ToInt16(udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString()),
                    Nome            = udgv.Rows[e.Cell.Row.Index].Cells["Imposto"].OriginalValue.ToString(),
                    TemValidade     = Convert.ToBoolean(udgv.Rows[e.Cell.Row.Index].Cells["TemValidade"].OriginalValue.ToString()),
                    ValidoAte       = Convert.ToDateTime(udgv.Rows[e.Cell.Row.Index].Cells["Válido até"].OriginalValue.ToString()),
                    TipoImposto     = Convert.ToInt16(udgv.Rows[e.Cell.Row.Index].Cells["TipoImposto"].OriginalValue.ToString()),
                    ValorImposto    = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["ValorImposto"].OriginalValue.ToString())
                });

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaImpostos();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar impostos", "FrmImpostos_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa.
        /// </summary>
        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnPesquisar_Click(new object(), new EventArgs());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mLstImpostos = null;

            this.Close();
            GC.Collect();
        }

        #endregion
    }
}
