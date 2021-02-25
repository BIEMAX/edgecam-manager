using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Infragistics.Win;
using Infragistics.Win.UltraWinSchedule;

namespace Edgecam_Manager
{
    internal partial class FrmFabrica : Form
    {

        #region Variáveis Globais

        /// <summary>
        ///     Contém a data selecionada pelo usuário.
        /// </summary>
        private DateTime mDtSelecionado;

        #endregion


        public FrmFabrica()
        {
            InitializeComponent();

            InicializaControlesInterface();
        }

        #region Methods

        /// <summary>
        ///     Método que inicializa os controles da interface via código.
        /// </summary>
        private void InicializaControlesInterface()
        {
            CarregaCentrosTrabalhos();

            //Define o item da combo box como o primeiro.
            cbMaquinas.SelectedIndex = 0;

            //Sempre irá receber o dia de hoje
            dtDataSelecao.Value = DateTime.Today;

            //Reseto sempre os owners do calendar info por causa da base legada (vulgo cache)
            ultraCalendarInfo1.ResetOwners();
            ultraCalendarInfo1.Owners.UnassignedOwner.Visible = false;
        }

        /// <summary>
        ///     Carrega os centros de trabalhos estáticos na classe 'Objects'.
        /// </summary>
        private void CarregaCentrosTrabalhos()
        {
            //Parâmetros iniciais
            cbMaquinas.Items.Add("(Selecione)");

            if (Objects.LstMaquinas != null)
            {
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            }
        }

        private DataSet ObtemApontamentos_Existentes()
        {
            //Link para ajuda
            //http://help.infragistics.com/Help/Doc/WinForms/2011.2/CLR2.0/html/WinTimelineView_Appointments_DataBinding_for_WinTimelineView.html

            DataSet theDataSet = new DataSet();

            // Create Appointments DataTable
            DataTable theAppointments = theDataSet.Tables.Add("Appointments");
            theAppointments.Columns.Add("AppointmentID", typeof(int));
            theAppointments.Columns.Add("StartTime", typeof(DateTime));
            theAppointments.Columns.Add("EndTime", typeof(DateTime));
            theAppointments.Columns.Add("Subject", typeof(string));
            theAppointments.Columns.Add("OwnerKey", typeof(string));
            theAppointments.Columns.Add("Description", typeof(string));

            //// Add Appointments Sample Data
            //theAppointments.Rows.Add(new object[] { 0, FrmNewAppointment.App.DtInicio, FrmNewAppointment.App.DtFim, FrmNewAppointment.App.Titulo, cbMaquinas.SelectedItem.ToString(), FrmNewAppointment.App.Descricao });
            //theAppointments.Rows.Add(new object[] { "1", "7/28/2009 8:00:00 AM", "7/28/2009 08:30:00 AM", "Scrum Meet", "Jamie" });
            //theAppointments.Rows.Add(new object[] { "1", "7/28/2009 9:00:00 AM", "7/28/2009 09:30:00 AM", "Status Meeting", "Lukas" });
            //theAppointments.Rows.Add(new object[] { "1", "7/28/2009 8:45:00 AM", "7/28/2009 09:00:00 AM", "Feature Review", "Lopez" });

            // Create Owners DataTable
            DataTable theOwners = theDataSet.Tables.Add("Owners");
            theOwners.Columns.Add("OwnerID", typeof(int));
            theOwners.Columns.Add("Name", typeof(string));
            theOwners.Columns.Add("OwnerKey", typeof(string));

            // Add Owners Sample Data

            //Obtem o ID da maquina
            int idMqn = Objects.LstMaquinas.Where(x => x.NomeMqn.ToUpper() == cbMaquinas.SelectedItem.ToString().ToUpper()).Select(y => y.IdMqn).FirstOrDefault();

            theOwners.Rows.Add(new object[] { idMqn.ToString(), cbMaquinas.SelectedItem.ToString(), cbMaquinas.SelectedItem.ToString() });

            return theDataSet;
        }

        /// <summary>
        ///     Método que adiciona um compromisso na interface.
        /// </summary>
        /// <param name="dsAppt">Dataset que irá receber os dados do novo compromisso</param>
        private void AdicionaNovoApontamento(ref DataSet dsAppt)
        {
            //DataTable theAppointments = dsAppt.Tables["Appointments"];
            //theAppointments.Rows.Add(new object[] { 0, FrmNewAppointment.App.DtInicio, FrmNewAppointment.App.DtFim, FrmNewAppointment.App.Titulo, cbMaquinas.SelectedItem.ToString(), FrmNewAppointment.App.Descricao });
        }
            
        /// <summary>
        ///     Após selecionado uma máquina, define o nome dela como Owner no DayView.
        /// </summary>
        private void DefineNomeMaquina_ControlerDayView()
        {
            #region Commented
            /*
            //  Add a new Owner to the Owners collection
            Owner myOwner = ultraCalendarInfo1.Owners.Add(cbMaquinas.SelectedItem.ToString());

            //  Create an OwnerDateSettings object for the first day of the current month
            OwnerDateSettings dateSettings = new OwnerDateSettings(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));

            //  Designate the day as a workday
            dateSettings.IsWorkDay = DefaultableBoolean.True;

            //  Add two sets of working hours, one for 9AM to 12PM,
            //  and another for 1PM to 5PM
            dateSettings.WorkingHours.Add(new TimeRange(TimeSpan.FromHours(9), TimeSpan.FromHours(12)));
            dateSettings.WorkingHours.Add(new TimeRange(TimeSpan.FromHours(13), TimeSpan.FromHours(17)));

            //  Create an appearance to indicate that the office is closed
            Appearance officeClosedAppearance = new Appearance();
            officeClosedAppearance.BackColor = Color.White;
            officeClosedAppearance.BackColor2 = Color.LightGray;
            officeClosedAppearance.BackGradientStyle = GradientStyle.Horizontal;
            officeClosedAppearance.BorderColor = Color.Transparent;

            //  Create TimeRanges for the hours during which the office is closed,
            //  which is 12AM to 6AM, and 8PM to 12AM
            TimeRange closed1 = new TimeRange(TimeSpan.FromHours(0), TimeSpan.FromHours(6));
            TimeRange closed2 = new TimeRange(TimeSpan.FromHours(20), TimeSpan.FromHours(24));

            //  Add two TimeRangeAppearance objects, one for each range during which
            //  the office is closed
            dateSettings.TimeRangeAppearances.Add(closed1, officeClosedAppearance);
            dateSettings.TimeRangeAppearances.Add(closed2, officeClosedAppearance);

            //  Add the OwnerDateSettings object to the Owner's DateSettings collection
            myOwner.DateSettings.Add(dateSettings);
            */
            #endregion

            //Reseto sempre os owners do calendar info por causa da base legada (vulgo cache)
            ultraCalendarInfo1.ResetOwners();

            ultraCalendarInfo1.Owners.Add(cbMaquinas.SelectedItem.ToString());
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

            if (LimparCache) { }
                //.DataSource = null;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Caso seja trocada a data, seto o valor ativo o DayVIew.
        /// </summary>
        private void dtDataSelecao_ValueChanged(object sender, EventArgs e)
        {
            //DateTime selectedDay = Convert.ToDateTime(dtDataSelecao.Value);
            ////   Get a Day object for the first day of the current month
            //Infragistics.Win.UltraWinSchedule.Day day;
            //day = ultraCalendarInfo1.GetDay(selectedDay, true);

            ////   Set the ActiveDay property to that day object we created above
            //ultraCalendarInfo1.ActiveDay = day;


            mDtSelecionado = Convert.ToDateTime(dtDataSelecao.Value);
            Infragistics.Win.UltraWinSchedule.Day day;
            day = ultraCalendarInfo1.GetDay(mDtSelecionado, true);
            ultraCalendarInfo1.ActiveDay = day;

            //TODO: Aqui eu preciso buscar os dados do banco
        }

        /// <summary>
        ///     A partir da seleção da máquina, é liberado os controles para o usuário iniciar
        /// o controle, ou seja, é obrigatório a seleção de uma máquina.
        /// </summary>
        private void cbMaquinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaquinas.SelectedIndex == 0)
            {
                dtDataSelecao.Enabled = false;
                ultraTabControl.Enabled = false;
            }
            else if (cbMaquinas.SelectedIndex != 0)
            {
                dtDataSelecao.Enabled = true;
                ultraTabControl.Enabled = true;

                DefineNomeMaquina_ControlerDayView();
            }
        }

        /// <summary>
        ///     Cria um novo formulário de compromisso na interface, modificando alguns controles como por exemplo,
        /// a localização do formulário, ícone, etc.
        /// </summary>
        private void ultraCalendarInfo1_BeforeDisplayAppointmentDialog(object sender, DisplayAppointmentDialogEventArgs e)
        {
            //Cancela o criador de 'compromisso' padrão.
            e.Cancel = true;

            //Cria o 'compromisso' personalizado
            FrmNewAppointment app = new FrmNewAppointment(FrmNewAppointment.e_TipoAgendamento.Novo, cbMaquinas.SelectedItem.ToString());
            app.ShowDialog();

            //if (FrmNewAppointment.App != null)
            //{
            //    #region Obsoleto - motivo : adicionava os dados na aba padrão.
            //    //Appointment appt = ultraDayView1.CalendarInfo.Appointments.Add(FrmNewAppointment.App.DtInicio, FrmNewAppointment.App.DtFim, FrmNewAppointment.App.Titulo);
            //    //appt.Description = FrmNewAppointment.App.Descricao;
            //    //appt.Location = "(None)";
            //    //ultraCalendarInfo1.Appointments.Add(appt);
            //    //ultraDayView1.Refresh();
            //    #endregion

            //    // Get the Sample Data required for Appointments and Owners data binding
            //    DataSet ds = ObtemApontamentos_Existentes();

            //    AdicionaNovoApontamento(ref ds);

            //    // Bind ultraCalendarInfo to Appointments Data Table
            //    ultraCalendarInfo1.DataBindingsForAppointments.DataSource = ds.Tables["Appointments"];

            //    // Bind ultraCalendarInfo to Owners Data Table
            //    ultraCalendarInfo1.DataBindingsForOwners.DataSource = ds.Tables["Owners"];

            //    // Set Appointments data binding properties to UltraCalendarInfo
            //    ultraCalendarInfo1.DataBindingsForAppointments.StartDateTimeMember = "StartTime";
            //    ultraCalendarInfo1.DataBindingsForAppointments.EndDateTimeMember = "EndTime";
            //    ultraCalendarInfo1.DataBindingsForAppointments.SubjectMember = "Subject";
            //    ultraCalendarInfo1.DataBindingsForAppointments.OwnerKeyMember = "OwnerKey";

            //    // Set Owners data binding properties to UltraCalendarInfo
            //    ultraCalendarInfo1.DataBindingsForOwners.KeyMember = "OwnerKey";
            //    ultraCalendarInfo1.DataBindingsForOwners.NameMember = "Name";

            //    Refresh();
            //}
            //TODO: Pensar se preciso apresentar uma mensagem para o usuário. (verificar se ele cancelou a ação)
            //else MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

    }
}
