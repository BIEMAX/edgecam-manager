using Infragistics.Win;
using Infragistics.Win.UltraWinTree;
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
    internal partial class FrmOrcamentos_RotasSeleciona : Form
    {
        #region Variáveis globais

        private String mRotaSelecionada;

        #endregion

        #region Enumeradores

        private enum e_SkaColunas
        {
            Selecionar,
            NomeRota,
            Operacao,
            OrdemExecucao,
            TempoEstimado,
            Custo
        }

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém a tabela selecionada pelo usuário.
        /// </summary>
        public String _RotaSelecionada
        {
            get
            {
                return mRotaSelecionada;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Permite consultar as rotas de produção.
        /// </summary>
        public FrmOrcamentos_RotasSeleciona()
        {
            InitializeComponent();
            ConsultaRotasProducao();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método responsável por consultar 
        /// </summary>
        /// <param name="PesquisarPorCodigo">True para utilizar o valor do campo txtDescricao</param>
        private void ConsultaRotasProducao(Boolean PesquisarPorCodigo = false)
        {
            DataTable dt;

            if (!PesquisarPorCodigo)
                dt = SQLQueries.Consulta_RotasProducao(true);
            else dt = SQLQueries.Consulta_RotasProducao(true, txtDescricao.Text);

            if (dt.Rows.Count > 0)
            {
                //Aqui contém o nome das rotas de produção (pais)
                DataTable lstNomesRotas = dt.AsDataView().ToTable(true, "NomeRota");

                for (int y = 0; y < lstNomesRotas.Rows.Count; y++)
                {
                    //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                    UltraTreeNode n = utv.Nodes.Add();

                    n.Cells[(int)e_SkaColunas.Selecionar].Value = "Selecionar";
                    n.Cells[(int)e_SkaColunas.NomeRota].Value = lstNomesRotas.Rows[y]["NomeRota"].ToString();

                    DataTable lstFilhos = dt.Select(String.Format("NomeRota = '{0}'", lstNomesRotas.Rows[y]["NomeRota"].ToString())).CopyToDataTable();

                    for (int z = 0; z < lstFilhos.Rows.Count; z++)
                    {
                        //Recebe os nós já adicionados no pai.
                        UltraTreeNode noFilho = n.Nodes.Add();

                        noFilho.Cells[(int)e_SkaColunas.Operacao].Value      = lstFilhos.Rows[z]["Operacao"].ToString();
                        noFilho.Cells[(int)e_SkaColunas.OrdemExecucao].Value = lstFilhos.Rows[z]["OrdemExecucao"].ToString();
                        noFilho.Cells[(int)e_SkaColunas.TempoEstimado].Value = lstFilhos.Rows[z]["TempoEstimado"].ToString();
                        noFilho.Cells[(int)e_SkaColunas.Custo].Value         = lstFilhos.Rows[z]["Custo"].ToString();

                        n.Nodes.Add(noFilho);
                    }

                    utv.Nodes.Add(n);
                }

                utv.ExpandAll();
            }
        }

        #endregion

        #region Eventos

        private void utv_MouseDown(object sender, MouseEventArgs e)
        {
            //Aqui eu verifico se o click é com o direito ou esquerdo do mouse.
            if (e.Button == MouseButtons.Left)
            {
                UltraTree tree = sender as UltraTree;
                UIElement controlElement = tree != null ? tree.UIElement : null;
                UIElement elementAtPoint = controlElement != null ? controlElement.ElementFromPoint(e.Location) : null;
                NodeSelectableAreaUIElement noSelecionado = null;

                while (elementAtPoint != null)
                {
                    noSelecionado = elementAtPoint as NodeSelectableAreaUIElement;
                    if (noSelecionado != null)
                        break;

                    elementAtPoint = elementAtPoint.Parent;
                }

                //  Se o usuário clicou na célula SELECIONAR, a condição abaixo é verdadeira. Se ele
                //clicar em qualquer outra coluna, o texto abaixo fica "Selecionar\<Próximas colunas>".
                if (noSelecionado != null && noSelecionado.Node.FullPath.ToUpper().Trim() == "SELECIONAR")
                {
                    mRotaSelecionada = noSelecionado.Node.Cells[(int)e_SkaColunas.NomeRota].Value.ToString();
                    btnVoltar_Click(new object(), new EventArgs());
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaRotasProducao(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar custos adicionais", "FrmOrcamentos_CustosSeleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa as peças.
        /// </summary>
        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion
    }
}