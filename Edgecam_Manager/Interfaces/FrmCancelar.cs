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
    public partial class FrmCancelar : Form
    {
        #region Variáveis globais

        /// <summary>
        ///     Contém a razão do cancelamento do item.
        /// </summary>
        private String mRazao;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém a razão do cancelamento do item.
        /// </summary>
        public String _Razao
        {
            get { return mRazao; }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmCancelar(String Modulo, String Referencia)
        {
            this.InitializeComponent();
            this.Text += $" da(o) {Modulo} de número/código/nome '{Referencia}";
        }

        #endregion

        #region Métodos

        private void SalvaRazaoCancelamento()
        {
            if (!String.IsNullOrEmpty(txtMotivo.Text.Trim()))
            {
                mRazao = txtMotivo.Text;
                btnVoltar_Click(new object(), new EventArgs());
            }
            else
            {
                MessageBox.Show("Você deve obrigatóriamente informar um motivo para cancelamento do item.", "Motivo/razão não pode ser vazio", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        #endregion

        #region Eventos

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void ubtnSalvar_Click(object sender, EventArgs e)
        {
            this.SalvaRazaoCancelamento();
        }

        #endregion
    }
}