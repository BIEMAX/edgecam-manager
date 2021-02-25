using Infragistics.Win.UltraWinSchedule;
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
    public partial class FrmCalendario : Form
    {

        public FrmCalendario()
        {
            InitializeComponent();

            //Infragistics.Win.AppStyling.StyleManager.Load(@"C:\Users\Public\Documents\Infragistics\2017.2\Windows Forms\AppStylist for Windows Forms\Styles\Office2013 - White.isl");

            InicializaValoresDefault();
        }

        #region Métodos

        private void InicializaValoresDefault()
        {
            ConsultaEventos();
            AtualizaTexto_Interface();
        }

        private void ConsultaEventos()
        {
            //Aqui consulta todos os eventos do calendário criados pelo admin do sistema
            //e pelo próprio usuário.
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_EVENTOS_CALENDARIO, new Dictionary<string, object>() { { "@USR", Objects.UsuarioAtual.Login } });

            if (dt != null && dt.Rows.Count > 0)
            {
                //Aqui eu obtenho os usuários.
                AdicionaOwners_CalendarInfo(dt);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if (Convert.ToBoolean(dt.Rows[x]["VisivelATodos"].ToString()))
                    {
                        Appointment a = new Appointment(Convert.ToDateTime(dt.Rows[x]["DtInicio"].ToString()), Convert.ToDateTime(dt.Rows[x]["DtFim"].ToString()));
                        a.Subject = dt.Rows[x]["NomeEvento"].ToString();
                        a.Description = dt.Rows[x]["DescricaoEvento"].ToString();
                        a.BarColor = Color.LightGray;
                        //a.Owner = ultraCalendarInfo1.Owners[dt.Rows[x]["Proprietario"].ToString()];

                        ultraCalendarInfo1.Appointments.Add(a);
                    }
                }
            }
            //else MessageBox.Show("Não há eventos à serem exibidos", "Calendário vazio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void AdicionaOwners_CalendarInfo(DataTable Dados)
        {
            DataTable users = Dados.AsDataView().ToTable(true, "Proprietario");

            if (users.Rows.Count > 0)
            {
                for (int x = 0; x < users.Rows.Count; x++)
                {
                    ultraCalendarInfo1.Owners.Add(users.Rows[x]["Proprietario"].ToString());
                }
            }
        }

        private void AtualizaTexto_Interface()
        {
            ultraTile1.Caption = String.Format("Dia {0}, {1}", ultraCalendarInfo1.ActiveDay.Date.ToString("dd/MM/yyyy"), ultraCalendarInfo1.ActiveDay.Date.ToString("dddd"));
            ultraTile2.Caption = String.Format("Semana {0}", ultraCalendarInfo1.GetWeekNumberForDate(ultraCalendarInfo1.ActiveDay.Date));
            ultraTile3.Caption = String.Format("Mês de {0} de {1}", ultraCalendarInfo1.ActiveDay.Date.ToString("MMMM"), ultraCalendarInfo1.ActiveDay.Date.ToString("yyyy"));
        }

        #endregion

        #region Eventos

        private void ultraCalendarInfo1_AfterActiveDayChanged(object sender, AfterActiveDayChangedEventArgs e)
        {
            try
            {
                AtualizaTexto_Interface();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao atualizar o texto do calendário na interface", "FrmCalendario", "ultraCalendarInfo1_AfterActiveDayChanged", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

    }
}
