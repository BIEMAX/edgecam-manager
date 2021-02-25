using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace Edgecam_Manager
{
    public partial class CustomReportControl : UserControl
    {

        #region Global variables

        private String mModule;
        private String mInterface;
        /// <summary>
        ///     UltraGrid that contains all data.
        /// </summary>
        private UltraGrid mUltraGrid;

        /// <summary>
        ///     Object that contains the value to query in SQL.
        /// </summary>
        private Object mValue;

        #endregion

        #region Properties
        public String _Module { set { mModule = value; } }
        public String _Interface { set { mInterface = value; } }

        public UltraGrid _UltraGrid { set { mUltraGrid = value; } }

        #endregion

        #region Class instances

        public CustomReportControl()
        {
            InitializeComponent();
            this.cbxReports.Items.Add("<Selecione>");
            this.cbxReports.SelectedIndex = 0;
            this.cbxReports.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        }

        private CustomReportControl(Object Module, Object Interface)
        {
            this.InitializeComponent();
            this.mModule = (String)Module;
            this.mInterface = (String)Module;

            this.cbxReports.Items.Add("<Selecione>");
            this.cbxReports.SelectedIndex = 0;
            this.cbxReports.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        }

        #endregion

        #region Methods

        #endregion

        #region Events

        private void cbxReports_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cbxReports.SelectedIndex == 0) this.ubtnResetFilter_Click(new object(), new EventArgs());
                //else if (cbxReports.SelectedItem != null) Objects.LoadFilterValuesInControls(mGroupBox, cbxReports.SelectedItem.ToString());
                if (cbxReports.SelectedIndex > 0)
                {
                    //Contém um objeto do relatório.
                    //var r = Objects.LstReports.Where(x => x.NomeRelatorio == cbxReports.SelectedItem.ToString());
                    Relatorio r = Objects.LstReports.Single(x => x.NomeRelatorio == cbxReports.SelectedItem.ToString().Split(new char[] { '-' })[1].ToString().Trim());

                    //Get the active row
                    mValue = mUltraGrid.ActiveRow.Cells["id"].OriginalValue.ToString();

                    //Geta a random file path to export report XML (extension is MDC)
                    String reportPath = Path.GetTempFileName() + ".mdc";

                    //Save
                    File.WriteAllText(reportPath, r.ConteudoRelatorio);

                    //If is to see only one record, get the active row.
                    if (r.ViewOneRecord)
                    {
                        //Params to query
                        Dictionary<String, Object> d = new Dictionary<string, object>();
                        d.Add("@PARAM", mValue);

                        //Get the data from database
                        DataSet ds = Objects.CnnBancoEcMgr.ExecutaMultiSql(r.SQL, d, true);

                        //ds.Tables[0].TableName = "Orcamentos";
                        //ds.Tables[1].TableName = "OrcamentosDetaCustos";
                        //ds.Tables[2].TableName = "OrcamentosItens";
                        //ds.Tables[3].TableName = "UnidadePrincipal";

                        //Call report center.
                        //Edgecam_Manager_Reports.FrmViewer f = new Edgecam_Manager_Reports.FrmViewer(reportPath, dt);
                        //f.ShowDialog();

                        //Stimulsoft.Report.StiReport stir = new Stimulsoft.Report.StiReport();
                        ////stir.Load(@"C:\Users\ANAKIN\Desktop\SamplesQuotes\Report_Quote_2.mrt");
                        //stir.Load(reportPath);
                        //stir.RegData("TEST", Objects.CnnBancoEcMgr);
                        //stir["ID"] = mValue;
                        //stir.CacheAllData = false;
                        //stir.Show();


                        //Stimulsoft.Report.Design.StiDesigner designer = new Stimulsoft.Report.Design.StiDesigner(stir);
                        //designer.MdiParent = this;
                        //designer.Report = stir;
                        //designer.Show();
                        //Clear the cache.
                        //File.Delete(reportPath);
                    }
                    else
                    {
                        //TODO: ISSO NÃO VAI FUNCIONA, PQ O DATASOURCE DA GRID É DIFERENTE DO BANCO DE DADOS (OS NOMES DE EXIBIÇÃO SÃO DIFERENTES)
                        //Get the data from database
                        DataTable dt = (DataTable)mUltraGrid.DataSource;

                        //Call report center.
                        //Edgecam_Manager_Reports.FrmViewer f = new Edgecam_Manager_Reports.FrmViewer(reportPath, dt);
                        //f.ShowDialog();

                        //Clear the cache.
                        File.Delete(reportPath);
                    }
                }

                //Reset users report choice.
                cbxReports.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                cbxReports.SelectedIndex = 0;
                Objects.CadastraNovoLog(true, "Erro ao carregar o relatório desejado", "CustomReportControl", "cbxReports_ValueChanged", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Before dropdown event, we load all filtes (olds and news) in the list
        /// to user choice.
        /// </summary>
        private void cbxFilters_BeforeDropDown(object sender, CancelEventArgs e)
        {
            try
            {
                Objects.LoadReportNameListInComboBox(ref cbxReports, mInterface, mModule);
                //cbxFilters.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao carregar os filtros das tarefas", "CustomFilterControl", "cbFilters_BeforeDropDown", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void ubtnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmFiltros_New f = new FrmFiltros_New(mInterface, mModule, Objects.CreateFilterFromControls(mGroupBox));
                //f.ShowDialog();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao salvar o filtro", "CustomFilterControl", "ubtnSaveFilter_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}