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
    public partial class FrmLoadingScreen : Form
    {
        /// <summary>
        ///     Variável que obtém o valor atual da barra de progresso do sistema principal.
        /// </summary>
        private int mValorAtual = 0;

        public FrmLoadingScreen()
        {
            InitializeComponent();
            ShowInTaskbar = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.Focus();

            //if (Objects.FormularioPrincipal._IsShowed)
            //{
            //    System.Threading.Thread.Sleep(400);
            //    FechaInterface();
            //}
            //else 
            if (Objects.FormularioPrincipal._AtualValorBarraProgresso == 100)
            {
                pb.ForeColor = Color.FromArgb(100, 200, 100);
                timer.Enabled = false;

                FechaInterface();
            }
            else if (Objects.FormularioPrincipal._AtualValorBarraProgresso <= 100)
            {
                if(mValorAtual == Objects.FormularioPrincipal._AtualValorBarraProgresso)
                {
                    pb.Value += 1;
                }
                else
                {
                    mValorAtual = Objects.FormularioPrincipal._AtualValorBarraProgresso;
                    pb.Value = mValorAtual;
                }

                lblTexto.Text = Objects.FormularioPrincipal._TextoBarraProgresso;
            }
        }

        private void FechaInterface()
        {
            Close();
            GC.Collect();
        }
    }
}