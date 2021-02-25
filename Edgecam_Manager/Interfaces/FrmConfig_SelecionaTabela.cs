using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Infragistics.Win;

namespace Edgecam_Manager
{
    internal partial class FrmConfig_SelecionaTabela : Form
    {

        #region Variáveis globais

        private String mTabelaSelecionada = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém a tabela selecionada pelo usuário.
        /// </summary>
        public String _TabelaSelecionada
        {
            get
            {
                return mTabelaSelecionada;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmConfig_SelecionaTabela()
        {
            InitializeComponent();

            ConsultaTabelas();
        }

        #endregion

        #region Métodos

        private void ConsultaTabelas()
        {
            udgv.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_NOMES_TABELAS_PERSONALIZAR);
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
                mTabelaSelecionada = udgv.Rows[e.Cell.Row.Index].Cells["Tabela"].OriginalValue.ToString();

                Close();
                GC.Collect();
            }
        }

        #endregion

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            mTabelaSelecionada = "";
            Close();
            GC.Collect();
        }

    }
}