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
    internal partial class FrmSobre : Form
    {
        public FrmSobre()
        {
            InitializeComponent();
            label3.Text += $"\nVersão {Objects.Versao.VersaoSistema}";

            lblEmpresa.Text = $"Licenciado para {Objects.License.GetSingleConfigValue(Edgecam_Manager_License.LicenseInfo.e_h01x.h02xfffS4A)}";
            lblSerial.Text = $"Serial: {Objects.License.GetSingleConfigValue(Edgecam_Manager_License.LicenseInfo.e_h01x.h01xfffS4A)}";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }
    }
}
