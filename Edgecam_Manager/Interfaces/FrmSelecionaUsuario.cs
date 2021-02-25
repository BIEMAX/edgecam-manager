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
    internal partial class FrmSelecionaUsuario : Form
    {
        #region Variáveis globais

        private String mUsuarioSelecionado;

        #endregion

        #region Propriedades

        public String _UsuarioSelecionado
        {
            get
            {
                return mUsuarioSelecionado;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmSelecionaUsuario()
        {
            InitializeComponent();
            CarregaListaUsuario();
        }

        #endregion

        /// <summary>
        ///     Carrega os usuários na grid de dados.
        /// </summary>
        private void CarregaListaUsuario()
        {
            udgv.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_USUARIOS_PARA_DESIGNAR_ORDENS);
        }

        private void udgv_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR")
            {
                return;
            }
            else
            {
                mUsuarioSelecionado = udgv.Rows[e.Cell.Row.Index].Cells["Usuário"].OriginalValue.ToString();

                this.Close();
                GC.Collect();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }
    }
}
