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
    public partial class FrmOrcamentos_Criptomoedas : Form
    {
        public FrmOrcamentos_Criptomoedas()
        {
            InitializeComponent();
        }

        private void FrmOrcamentos_Criptomoedas_Shown(object sender, EventArgs e)
        {
            /*
             *  https://igniteui.github.io/crypto-portfolio-app/#/block-list
             *  https://www.infragistics.com/downloads/request/complete
             *  https://www.infragistics.com/resources/sample-applications/angular-cryptocurrency-app?utm_source=IG_Marketing&utm_medium=Email&utm_campaign=19-04-02%20Newsletter&utm_term=Non-Customer&utm_content=SampleApp_Crypto&mkt_tok=eyJpIjoiWmpGak5HRmtabUl3WWpBMiIsInQiOiJHZE4rRmtWcXJDWmxCK3Fub2VPVlF4cEdrSlJpdXczNnhqS0h1cnhIOXR3TUo0aCtZN0RFK3JSd09lQ2J6SEtOTGMrbGtsbk9TajMzV2hXak1Vd01uSW5EWkpXZ200UW1qcWpScVZcL3ZUTjJublVlaUczXC9CZnBVTk1HcDZrYWphIn0%3D
             */

            Cursor = Cursors.WaitCursor;

            /*
             *  Esses links abaixos demoravam muito para carregar,
             * tendo em vista que eu os carrego durante a inicialização
             * do sistema.
             */

            //wb.Navigate("https://igniteui.github.io/crypto-portfolio-app/#/block-list");
            //wb.Navigate("https://coinmarketcap.com/pt-br/");
            wb.Navigate("https://www.cryptocompare.com/coins/list/USD/1");

            wb.ScriptErrorsSuppressed = true;

            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            Cursor = Cursors.Arrow;
        }
    }
}