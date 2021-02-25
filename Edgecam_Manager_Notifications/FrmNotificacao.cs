using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tulpep.NotificationWindow;

namespace Edgecam_Manager_Notifications
{
    public partial class FrmNotificacao : Form
    {
        //Help: https://www.c-sharpcorner.com/article/working-with-popup-notification-in-windows-forms/

        #region Variáveis globais

        protected e_SkaVersao mVersaoMgr;

        #endregion

        #region Propriedades

        #endregion

        #region Enumeradores

        public enum e_SkaVersao
        {
            V2017R1,
            V2017R2,
            V2018R1,
            V2018R2,
            V2019R1
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmNotificacao()
        {
            InitializeComponent();

            EscondeForm();

            timer.Interval = 10000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        /// <summary>
        ///     Instância do objeto que carrega as notificações para o usuário.
        /// </summary>
        /// <remarks>Essa classe instância um Form, e eu escondo ele nessa chamada.</remarks>
        public FrmNotificacao(e_SkaVersao Versao)
        {
            InitializeComponent();

            EscondeForm();

            timer.Interval = 10000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Esconde o formulário do usuário.
        /// </summary>
        private void EscondeForm()
        {
            this.Visible = false;
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void MostraNotificacao()
        {
            //Tive de invocar esse popup, 
            this.Invoke((MethodInvoker)delegate 
            {
                PopupNotifier popup = new PopupNotifier();

                popup.Image = Properties.Resources.Edgecam_Manager.ToBitmap();

                popup.BorderColor = Color.LightGreen;
                popup.HeaderColor = Color.LightGreen;

                popup.AnimationDuration = 5;//Em segundos

                popup.TitleFont = new System.Drawing.Font("Arial", 18.0f, FontStyle.Bold);
                popup.TitleText = "Edgecam Manager - 2018 R2";

                popup.ContentFont = new System.Drawing.Font("Arial", 12.0f, FontStyle.Regular);
                popup.ContentText = "Você possuí novas tarefas";

                popup.Popup();// show
            });
        }

        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            MostraNotificacao();
        }

        #region Eventos

        #endregion

    }
}
