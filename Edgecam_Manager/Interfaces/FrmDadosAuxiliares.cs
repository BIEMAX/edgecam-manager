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
    internal partial class FrmDadosAuxiliares : Form
    {
        public FrmDadosAuxiliares()
        {
            InitializeComponent();
        }

        private void ConsultaDadosAuxilirares()
        {
            udgv.DataSource = SQLQueries.Consulta_DadosAuxiliares("", "", SQLQueries.e_SkaTipoConsultaDados.ConsultarParaSelecionar, true);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            //TODO: Terminar essa interface que ainda não está pronta
        }

        
    }
}
