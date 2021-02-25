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
    internal partial class FrmNewAppointment : Form
    {

        public enum e_TipoAgendamento
        {
            Novo,
            Existente
        }




        /// <summary>
        ///     Método estático que representa um novo compromisso agendado pelo usuário.
        /// </summary>
        //public static SkaAppointment App;




        public FrmNewAppointment(e_TipoAgendamento Agendamento, String NomeMqn)
        {
            InitializeComponent();

            DefineTipoAgendamento(Agendamento, NomeMqn);

            //Reinicia sempre a variável estática.
            //App = null;
        }

        #region Methods

        /// <summary>
        ///     Método que define o tipo de agendamento e o texto à ser apresentado
        /// quando carregado a interface.
        /// </summary>
        private void DefineTipoAgendamento(e_TipoAgendamento Agendamento, String NomeMqn)
        {
            switch (Agendamento)
            {
                case e_TipoAgendamento.Novo:
                    Text = "Criar um novo agendamento para o centro de trabalho " + NomeMqn;
                    break;

                case e_TipoAgendamento.Existente:
                    Text = "Agendamento existente para o centro de trabalho " + NomeMqn;
                    break;
            }
        }

        #endregion

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        /// <summary>
        ///     Método que cadastra um novo compromisso na interface para o usuário.
        /// </summary>
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //App = new SkaAppointment
            //{
            //    Titulo = txtTitle.Text,
            //    DtInicio = ConcatenaDateTime(ultraDateTimeEditor1.Value.ToString(), cb_HoraInicio.SelectedItem.ToString()),
            //    DtFim = ConcatenaDateTime(ultraDateTimeEditor2.Value.ToString(), cb_HoraFim.SelectedItem.ToString()),
            //    Descricao = rtxt_Descricao.Text
            //};

            ////Se for diferente de nulo, significa que o objeto foi criado com êxito e fecho a interface.
            //if (App != null)
            //    btnVoltar_Click(new object(), new EventArgs());
        }

        /// <summary>
        ///     Método que recebe dois textos e 
        /// </summary>
        /// <returns></returns>
        private DateTime ConcatenaDateTime(String Data, String Hora)
        {
            String[] data = Data.Split(new char[] { '/' , ' '}).ToArray();//[0] - dia | [1] - mês | [2] - ano
            String[] hora = Hora.Split(':').ToArray();//[0] - hora | [1] - minutos

            DateTime dtRet = new DateTime(Convert.ToInt16(data[2]), Convert.ToInt16(data[1]), Convert.ToInt16(data[0]), Convert.ToInt16(hora[0]), Convert.ToInt16(hora[1]), 0);
            
            return dtRet;
        }
    }
}
