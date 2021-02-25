using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Edgecam_Manager;

namespace Edgecam_Manager
{
    internal partial class FrmFerramentas_Rastreio : Form
    {

        #region Variáveis globals/da classe

        /// <summary>
        ///     Contém o ID da ferramenta.
        /// </summary>
        private String mIdTool;

        /// <summary>
        ///     Contém o nome da ferramenta.
        /// </summary>
        private String mNomeTool;

        /// <summary>
        ///     True = Ferramenta foi localizada em trabalhos do edgecam
        ///     False = Ferramenta não foi loalizada em trabalhos do edgecam
        /// </summary>
        public Boolean mFerramentaUsada;
        
        #endregion

        #region Propriedades

        /// <summary>
        ///     True = Ferramenta foi localizada em trabalhos do edgecam
        ///     False = Ferramenta não foi loalizada em trabalhos do edgecam
        /// </summary>
        public Boolean _FerramentaUsada
        {
            get
            {
                return mFerramentaUsada;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto que permite rastrear o uso das ferramentas dentro do edgecam.
        /// </summary>
        /// <param name="IdTool">Id da ferramenta</param>
        /// <param name="NomeTool">Nome da ferramenta</param>
        public FrmFerramentas_Rastreio(String IdTool, String NomeTool)
        {
            InitializeComponent();
            mIdTool = IdTool;
            mNomeTool = NomeTool;
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que faz a carga dos ícones na interface.
        /// </summary>
        private void InicializaValoresDefault()
        {
            //Objects.DefineColorThemeInterface(this);

            lblTotalTime.Visible = false;

            RastreiaTrabalhos();
        }

        /// <summary>
        ///     Método responsável por consultar quais trabalhos a ferramenta foi
        /// utilizada e a quantidade de vezes de utilizada.
        /// </summary>
        private void RastreiaTrabalhos()
        {
            udgv_Jobs.DataSource = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_USABILIDADE_TOOLS_IN_JOBS, new Dictionary<string, object>() { { "@TOOL_ID", mIdTool } });

            if (udgv_Jobs.Rows.Count <= 0)
            {
                mFerramentaUsada = false;
                Objects.CadastraNovoLog(false, String.Format("Não foi possível localizar trabalhos da ferramenta de id '{0}'", mIdTool), "FrmFerramentas_Rastreio", "RastreiaTrabalhos", "", "", e_TipoErroEx.Informacao);
                MessageBox.Show(String.Format("Não foi localizados trabalhos em que a ferramenta '{0}' é utilizada", mNomeTool), "Ferramenta não utilizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                GC.Collect();
            }
            //Condição adicionada para pesquisar os dados em seguida, caso tenha apenas um trabalho
            else if (udgv_Jobs.Rows.Count == 1)
            {
                mFerramentaUsada = true;

                ConsultaTempoUsinagemTrabalho();
            }
            else mFerramentaUsada = true;
        }

        /// <summary>
        ///     Método que consulta os tempos de usinagem de acordo com o trabalho selecionado.
        /// </summary>
        private void ConsultaTempoUsinagemTrabalho()
        {
            var q = udgv_Jobs.Selected.Rows.Count == 0 ? udgv_Jobs.Rows[0] : udgv_Jobs.Selected.Rows[0];

            if (q != null)
            {
                String trabalho = q.Cells["Nome do trabalho"].OriginalValue.ToString();
                ultraExpandableGroupBox2.Text = String.Format("Tempo do trabalho '{0}'", trabalho);

                udgv_Times.DataSource = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_TEMPOS_FERRAMENTA_POR_JOB, new Dictionary<string, object>() { { "@JOB", trabalho }, { "@TOOL_ID", mIdTool } });

                SomaTempos();
            }
        }

        /// <summary>
        ///     Soma o tempo de todas as estratégias no trabalho e apresenta para o usuário o tempo total.
        /// </summary>
        private void SomaTempos()
        {
            if (udgv_Times.Rows.Count > 0)
            {
                List<String> lstTempos = new List<string>();
                for (int x = 0; x < udgv_Times.Rows.Count; x++)
                {
                    lstTempos.Add(udgv_Times.Rows[x].Cells["Tempo total de usinagem"].OriginalValue.ToString());
                }

                lblTotalTime.Text = String.Format("Tempo total de uso da ferramenta: '{0}'", CustomTimes.SumTimes(lstTempos.ToArray()));
                lblTotalTime.Visible = true;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Fecha a interface.
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        /// <summary>
        ///     Evento de duplo clique na linha, que faz o sistema consultar os tempos de usinagem.
        /// </summary>
        private void udgv_Jobs_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                ConsultaTempoUsinagemTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os tempos das ferramentas", "FrmFerramentas_Rastreio", "udgv_Jobs_DoubleClickRow", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}