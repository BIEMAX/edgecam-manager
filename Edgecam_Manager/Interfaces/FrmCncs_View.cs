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
    internal partial class FrmCncs_View : Form
    {
        public FrmCncs_View()
        {
            InitializeComponent();
        }




        /// <summary>
        ///     Quando acionado, executa uma consulta no banco de dados.
        /// </summary>
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            //if (txtModify.Text == "")
            //    dgvCncs.DataSource = new SkaListaCncs(txtTrabalho.Text, txtNomeCnc.Text, txtModify.Text);

            //if (txtModify.Text != "")
            //    dgvCncs.DataSource = new SkaListaCncs(txtTrabalho.Text, txtNomeCnc.Text, Convert.ToDateTime(txtModify.Text).ToString("yyyy-MM-dd"));    

            //dgvCncs.AutoGenerateColumns = false;
        }

        /// <summary>
        ///     Quando é utilizado o DataPickerView, eu defino o valor selecionado na 'textbox'
        /// </summary>
        private void dtSelecionadaData_ValueChanged(object sender, EventArgs e)
        {
            txtModify.Text = dtSelecionadaData.Value.ToShortDateString();
        }

        /// <summary>
        ///     Obtém o 
        /// </summary>
        private void btnView_Click(object sender, EventArgs e)
        {

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
                dgvCncs.DataSource = null;
        }
    }
}
