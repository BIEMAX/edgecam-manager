using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;

namespace Edgecam_Manager
{
    public partial class FrmOrcamentos_HistoricoCotacoes : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     Contém a moeda da cotação à ser buscada no banco de daos.
        /// </summary>
        private String mMoedaCotacao;

        /// <summary>
        ///     Contém uma lista dos meses do ano.
        /// </summary>
        private List<String> mMeses = new List<string>() {  
                                                            "Janeiro", 
                                                            "Fevereiro",
                                                            "Março",
                                                            "Abril",
                                                            "Maio",
                                                            "Junho",
                                                            "Julho",
                                                            "Agosto",
                                                            "Setembro",
                                                            "Outubro",
                                                            "Novembro",
                                                            "Dezembro"
                                                        };

        #endregion

        #region Instância dos objetos da classe

        public FrmOrcamentos_HistoricoCotacoes(String Cotacao)
        {
            InitializeComponent();
            mMoedaCotacao = Cotacao;
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        private void InicializaValoresDefault()
        {
            this.Text = String.Format("Histórico da variação cambial da moeda '{0}'", mMoedaCotacao);

            //Desativa as janelas para editar/adicionar apontamentos manuais.
            ultraMonthViewSingle1.ShowClickToAddIndicator = Infragistics.Win.DefaultableBoolean.False;
            ultraMonthViewSingle1.AutoAppointmentDialog = false;
            ultraCalendarInfo1.SelectTypeActivity = Infragistics.Win.UltraWinSchedule.SelectType.Single;

            //na data de inicio, sempre começo pelo último dia do mês anterior do atual.
            DateTime dtInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, CustomTimes.GetLastDayMonth());

            ConsultaCotacoes(dtInicio, dtFim);
        }

        #region Obsoleto (utilizado apenas para testes)

        private void AlimentaGraficos()
        {
            //http://help.infragistics.com/Help/Doc/WinForms/2012.1/CLR2.0/html/Chart_Creating_a_Composite_Chart_in_Code_Part_1_of_2.html
            //http://help.infragistics.com/Help/Doc/WinForms/2012.1/CLR2.0/html/Chart_Creating_a_Composite_Chart_in_Code_Part_2_of_2.html

            //Aqui define o tipo de gráfico que será realizado.
            //https://www.infragistics.com/help/winforms/chart-2d-charts
            this.ultraChart1.ChartType = ChartType.Composite;

            ChartArea myChartArea = new ChartArea();
            this.ultraChart1.CompositeChart.ChartAreas.Add(myChartArea);

            AxisItem axisX = new AxisItem();
            axisX.OrientationType = AxisNumber.X_Axis;
            axisX.DataType = AxisDataType.String;
            axisX.SetLabelAxisType = Infragistics.UltraChart.Core.Layers.SetLabelAxisType.GroupBySeries;
            axisX.Labels.ItemFormatString = "<ITEM_LABEL>";
            axisX.Labels.Orientation = TextOrientation.VerticalLeftFacing;

            AxisItem axisY = new AxisItem();
            axisY.OrientationType = AxisNumber.Y_Axis;
            axisY.DataType = AxisDataType.Numeric;
            axisY.Labels.ItemFormatString = "<DATA_VALUE:0.#>";

            //Adiciona os gráficos
            myChartArea.Axes.Add(axisX);
            myChartArea.Axes.Add(axisY);

            //Adiciona as séries na interface
            NumericSeries seriesA = GetNumericSeriesBound();
            NumericSeries seriesB = GetNumericSeriesUnBound();
            this.ultraChart1.CompositeChart.Series.Add(seriesA);
            this.ultraChart1.CompositeChart.Series.Add(seriesB);

            //Cria a aparência das colunas
            ChartLayerAppearance myColumnLayer = new ChartLayerAppearance();
            myColumnLayer.ChartType = ChartType.ColumnChart;
            myColumnLayer.ChartArea = myChartArea;
            myColumnLayer.AxisX = axisX;
            myColumnLayer.AxisY = axisY;
            myColumnLayer.Series.Add(seriesA);
            myColumnLayer.Series.Add(seriesB);
            this.ultraChart1.CompositeChart.ChartLayers.Add(myColumnLayer);

            //Cria a legenda
            CompositeLegend myLegend = new CompositeLegend();
            myLegend.ChartLayers.Add(myColumnLayer);
            myLegend.Bounds = new Rectangle(0, 75, 20, 25);
            myLegend.BoundsMeasureType = MeasureType.Percentage;
            myLegend.PE.ElementType = PaintElementType.Gradient;
            myLegend.PE.FillGradientStyle = GradientStyle.ForwardDiagonal;
            myLegend.PE.Fill = Color.CornflowerBlue;
            myLegend.PE.FillStopColor = Color.Transparent;
            myLegend.Border.CornerRadius = 10;
            myLegend.Border.Thickness = 0;
            this.ultraChart1.CompositeChart.Legends.Add(myLegend);
        }

        private DataTable GetData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Label Column", typeof(string));
            table.Columns.Add("Value Column", typeof(double));
            table.Columns.Add("Another Value Column", typeof(double));
            table.Rows.Add(new object[] { "Point A", 1.0, 3.0 });
            table.Rows.Add(new object[] { "Point B", 2.0, 2.0 });
            table.Rows.Add(new object[] { "Point C", 3.0, 1.0 });
            table.Rows.Add(new object[] { "Point D", 4.0, 2.0 });
            table.Rows.Add(new object[] { "Point E", 5.0, 3.0 });
            return table;
        }

        private NumericSeries GetNumericSeriesBound()
        {
            NumericSeries series = new NumericSeries();
            series.Label = "Series A";
            // this code populates the series from an external data source
            DataTable table = GetData();
            series.Data.DataSource = table;
            series.Data.LabelColumn = "Label Column";
            series.Data.ValueColumn = "Value Column";
            return series;
        }

        private NumericSeries GetNumericSeriesUnBound()
        {
            NumericSeries series = new NumericSeries();
            series.Label = "Series B";
            // this code populates the series using unbound data
            series.Points.Add(new NumericDataPoint(5.0, "Point A", false));
            series.Points.Add(new NumericDataPoint(4.0, "Point B", false));
            series.Points.Add(new NumericDataPoint(3.0, "Point C", false));
            series.Points.Add(new NumericDataPoint(2.0, "Point D", false));
            series.Points.Add(new NumericDataPoint(1.0, "Point E", false));
            return series;
        }

        #endregion

        /// <summary>
        ///     Método que consulta as cotações da moeda definida pelo usuário.
        /// </summary>
        private void ConsultaCotacoes(DateTime DtIni, DateTime DtFim)
        {
            //Ativa o dia do mês a ser inicializado na interface.
            ultraCalendarInfo1.ActivateDay(DtIni);

            String sql = Consultas_EcMgr.CONSULTA_TODAS_COTACOES_MES_CORRENTE.Replace("@MOEDA", mMoedaCotacao.ToLower()).Replace("@DTINICIO", DtIni.ToString("yyyy-MM-dd")).Replace("@DTFIM", DtFim.ToString("yyyy-MM-dd"));

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sql);

            if (ultraCalendarInfo1.Appointments.Count > 0)
            {
                ultraCalendarInfo1.Appointments.Clear();
                ultraChart1.CompositeChart.Series.Clear();
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    //Atualiza a coluna com o valor apenas numérico
                    dt.Rows[x]["ValorNum"] = dt.Rows[x]["ValorTexto"].ToString().ToLower().Replace(" real brasileiro", "").Split('=')[1].Replace(".", ",");
                    DateTime d = Convert.ToDateTime(dt.Rows[x]["DtConsulta"].ToString());
                    AdicionarCotacao_Mes(d, dt.Rows[x]["ValorNum"].ToString());
                }

                AdicionaCotacao_Grafico(dt);
                AtualizaTextoInterface(DtIni, DtFim);
            }
        }

        /// <summary>
        ///     Método responsável por adicionar a cotação na interface para o usuário.
        /// </summary>
        /// <param name="DataCotacao">Data da cotação</param>
        /// <param name="ValorCotacao">Valor da cotação.</param>
        private void AdicionarCotacao_Mes(DateTime DataCotacao, String ValorCotacao)
        {
            Infragistics.Win.UltraWinSchedule.Appointment a = new Infragistics.Win.UltraWinSchedule.Appointment(DataCotacao, DataCotacao);
            a.AllDayEvent = true;
            a.Subject = ValorCotacao;
            ultraCalendarInfo1.Appointments.Add(a);
        }

        /// <summary>
        ///     Método que adiciona uma datatable dentro do ultrachart e apresenta
        /// para o usuário.
        /// </summary>
        /// <param name="Dados">DataTable contendo os dados.</param>
        private void AdicionaCotacao_Grafico(DataTable Dados)
        {
            if (Dados != null && Dados.Rows.Count > 0)
            {
                NumericSeries series = new NumericSeries();

                for (int x = 0; x < Dados.Rows.Count; x++)
                {
                    Double valor = Convert.ToDouble(Dados.Rows[x]["ValorNum"].ToString());
                    String data = Convert.ToDateTime(Dados.Rows[x]["DtConsulta"].ToString()).ToString("dd/MM/yyyy");
                    series.Points.Add(new NumericDataPoint(valor, data, false));
                }

                ultraChart1.CompositeChart.Series.Add(series);
                
                //Comentei esse trecho, pois o gráfico fica estranho com o valor 0;
                //ultraChart1.Data.ZeroAligned = true;

                //Remove a legenda (não existe)
                //ultraChart1.CompositeChart.Legends.RemoveAt(0);

                //Não apresenta a legenda.
                ultraChart1.Legend.Visible = false;
            }
        }

        /// <summary>
        ///     Método que atualiza o texto na aba para o usuário saber de quando
        /// até quando foi realizado a pesquisa.
        /// </summary>
        /// <param name="DtIni">Data de inicio</param>
        /// <param name="DtFim">Data de fim</param>
        private void AtualizaTextoInterface(DateTime DtIni, DateTime DtFim)
        {
            ultraTile1.Caption = String.Format("Variação cambial de '{0}' até '{1}'", DtIni.ToString("dd/MM/yyyy"), DtFim.ToString("dd/MM/yyyy"));
        }

        #endregion

        #region Eventos

        private void tsmMesAtual_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DateTime dtInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime dtFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, CustomTimes.GetLastDayMonth());

                ConsultaCotacoes(dtInicio, dtFim);

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo mês atual", "FrmOrcamentos_HistoricoCotacoes", "tsmMesAtual_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmMesAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                DateTime dtInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
                DateTime dtFim = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, CustomTimes.GetLastDayMonth(0, DateTime.Now.Month - 1));

                ConsultaCotacoes(dtInicio, dtFim);

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo mês anterior", "FrmOrcamentos_HistoricoCotacoes", "tsmMesAnterior_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm1Tri_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 3, 31));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo 1 trimestre", "FrmOrcamentos_HistoricoCotacoes", "tsm1Tri_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm2Tri_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 4, 1), new DateTime(DateTime.Now.Year, 6, 30));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo 2 trimestre", "FrmOrcamentos_HistoricoCotacoes", "tsm2Tri_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm3Tri_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 7, 1), new DateTime(DateTime.Now.Year, 9, 30));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo 3 trimestre", "FrmOrcamentos_HistoricoCotacoes", "tsm3Tri_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm4Tri_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 10, 1), new DateTime(DateTime.Now.Year, 12, 31));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo 4 trimestre", "FrmOrcamentos_HistoricoCotacoes", "tsm4Tri_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm1Sem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 6, 30));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo 1 semestre", "FrmOrcamentos_HistoricoCotacoes", "tsm1Sem_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm2Sem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 7, 1), new DateTime(DateTime.Now.Year, 12, 31));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo 2 semestre", "FrmOrcamentos_HistoricoCotacoes", "tsm2Sem_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmAnoAtual_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                ConsultaCotacoes(new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 12, 31));

                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao consultar o histórico pelo ano atual", "FrmOrcamentos_HistoricoCotacoes", "tsmAnoAtual_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}