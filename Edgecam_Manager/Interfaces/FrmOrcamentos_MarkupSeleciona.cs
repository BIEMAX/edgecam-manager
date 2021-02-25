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
    internal partial class FrmOrcamentos_MarkupSeleciona : Form
    {
        #region Variáveis globais

        private Markup mMarkupSelecionado;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém o markup selecionada pelo usuário.
        /// </summary>
        public Markup _MarkupSelecionado
        {
            get
            {
                return mMarkupSelecionado;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmOrcamentos_MarkupSeleciona()
        {
            this.InitializeComponent();
            ConsultaMarkups();
        }

        #endregion

        #region Métodos

        private void ConsultaMarkups(Boolean PesquisarPorCodigo = false)
        {
            if (!PesquisarPorCodigo)
                udgv.DataSource = SQLQueries.Consulta_Markup("", true);
            else udgv.DataSource = SQLQueries.Consulta_Markup(txtDescricao.Text, true);
        }

        //private void SelecionaMultiplosCustos()
        //{
        //    if (udgv.Selected.Rows.Count > 0)
        //    {
        //        for (int x = 0; x < udgv.Selected.Rows.Count; x++)
        //        {
        //            if (!String.IsNullOrEmpty(mMarkupSelecionado)) mMarkupSelecionado += ",";
        //            mMarkupSelecionado += String.Format("'{0}'", udgv.Selected.Rows[x].Cells["id"].OriginalValue.ToString());
        //        }

        //        btnVoltar_Click(new object(), new EventArgs());
        //    }
        //    else MessageBox.Show("Você deve selecionar ao menos um custo adicional para utilizar essa opção", "Custo não selecionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //}

        #endregion

        #region Eventos

        private void udgv_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR") return;
            else
            {
                mMarkupSelecionado = new Markup()
                {
                    Id = Convert.ToInt16(udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString()),
                    Nome = udgv.Rows[e.Cell.Row.Index].Cells["Nome"].OriginalValue.ToString(),
                    MargemLucro = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["Margem de lucro (%)"].OriginalValue.ToString()),
                    MarkupUp = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["Markup"].OriginalValue.ToString()),
                    MarkupDown = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["MarkDown"].OriginalValue.ToString()),
                    Mult = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["MultiplicadorValor"].OriginalValue.ToString()),
                    MultPer = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["MultiplicadorPercentual"].OriginalValue.ToString()),
                    Ativo = Convert.ToBoolean(udgv.Rows[e.Cell.Row.Index].Cells["Ativo_db"].OriginalValue.ToString()),
                    Visivel = Convert.ToBoolean(udgv.Rows[e.Cell.Row.Index].Cells["Visível"].OriginalValue.ToString()),
                    DtCrt = Convert.ToDateTime(udgv.Rows[e.Cell.Row.Index].Cells["Data de cadastro"].OriginalValue.ToString()),
                    UsrCrty = udgv.Rows[e.Cell.Row.Index].Cells["Cadastrado por"].OriginalValue.ToString(),
                };
                //mMarkupSelecionado = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ConsultaMarkups(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os markups", "FrmOrcamentos_MarkupSeleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa as peças.
        /// </summary>
        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.btnPesquisar_Click(new object(), new EventArgs());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void tsmSelecionaMarcados_Click(object sender, EventArgs e)
        {
            //SelecionaMultiplosCustos();
        }

        #endregion
    }
}