using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ImagedComboBox;

namespace Edgecam_Manager
{
    internal partial class FrmMaquinas : Form
    {

        #region Variáveis globais

        #endregion

        #region Propriedades

        #endregion

        #region Instância dos objetos da classe

        public FrmMaquinas()
        {
            InitializeComponent();
            InicializaControles();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Inicializa os controles da interface (indices dos combo boxes, dentre outros)
        /// </summary>
        private void InicializaControles()
        {
            //Combo box de visibilidade
            cbxVisivel.Items.Add(new ComboBoxItem("(Todos)"));
            cbxVisivel.Items.Add(new ComboBoxItem("Inativo", Properties.Resources.Red));
            cbxVisivel.Items.Add(new ComboBoxItem("Ativo", Properties.Resources.Green));
            cbxVisivel.SelectedIndex = 0;

            //Combo box de ambientes
            cbxAmbiente.Items.Add("(Todos)");
            cbxAmbiente.Items.Add("Torneamento");
            cbxAmbiente.Items.Add("Fresamento");
            cbxAmbiente.Items.Add("Aditiva");
            cbxAmbiente.SelectedIndex = 0;

            //Desabilita controles
            btnEditar.Enabled = false;
            btnDeletar.Enabled = false;
            btnMagazine.Enabled = false;
        }

        /// <summary>
        ///     Consulta as máquinas previamente cadastradas pelo sistema.
        /// </summary>
        private void ConsultaMaquinas()
        {
            udgv.DataSource = SQLQueries.Consulta_Maquinas(txtNomeMqn.Text, cbxAmbiente.SelectedIndex - 1, cbxVisivel.SelectedIndex - 1);

            if (udgv.Rows.Count > 0)
            {
                //Habilita os controles
                btnEditar.Enabled = true;
                btnDeletar.Enabled = true;
                btnMagazine.Enabled = true;
            }
        }

        private void CadastraNovaMaquina()
        {
            FrmMqns_New frm = new FrmMqns_New();
            frm.ShowDialog();
        }

        /// <summary>
        ///     Método público utilizado, ou para esconder o formulário ou para limpar o cache da interface.
        /// </summary>
        /// <param name="Esconder"></param>
        /// <param name="LimparCache"></param>
        /// <remarks>Variáveis booleanas utilizadas para reaproveitar o método para apenas
        /// limpar o cache.</remarks>
        public void FechaInterface(Boolean Esconder, Boolean LimparCache)
        {
            if (Esconder)
                Hide();

            if (LimparCache)
            {
                //dgvMachines.DataSource = null;
            }
        }

        #endregion

        #region Eventos

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaMaquinas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os centros de trabalho", "FrmMqns", "btnPesquisar_Click", "Exceção não trattada",
                                           "Consultas_EcMgr.CONSULTA_MAQUINAS", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNewMachine_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovaMaquina();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar centros de trabalho", "FrmMqns", "btnNewMachine_Click", "Exceção não trattada",
                                           "Consultas_EcMgr.CADASTRA_NOVA_MAQUINA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEditMachine_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteMachine_Click(object sender, EventArgs e)
        {

        }

        private void btnViewTools_Click(object sender, EventArgs e)
        {
            //SkaMachine m = (SkaMachine)dgvMachines.SelectedRows[0].DataBoundItem;

            //FrmMqns_ViewMagazine frmViewTools = new FrmMqns_ViewMagazine(m);
            //frmViewTools.ShowDialog();
        }

        #endregion

    }
}
