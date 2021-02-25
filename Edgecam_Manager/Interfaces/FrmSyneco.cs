using Microsoft.Win32;
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
    public partial class FrmSyneco : Form
    {
        public FrmSyneco()
        {
            InitializeComponent();
        }

        private void FrmSyneco_Shown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //TODO: NÃO ESQUECER DE CONFIGURAR O LINK DO SYNECO NO BANCO DE DADOS MGR
            //wb.Navigate("http://syneco.ska.com.br:3000");
            wb.Navigate("http://syneco.ska.com.br:4000");

            wb.ScriptErrorsSuppressed = true;

            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            Cursor = Cursors.Arrow;
        }
    }
}