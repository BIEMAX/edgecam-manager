using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Infragistics.Win.UltraWinTree;
using Infragistics.Win;

namespace Edgecam_Manager
{
    internal partial class FrmPeca_Seleciona_Db : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     Representa as colunas do 'UltraTreeView'.
        /// </summary>
        private enum e_SkaColunas
        {
            /// <summary>
            ///     Coluna de checkbox.
            /// </summary>
            Selecionar = 0,
            /// <summary>
            ///     Coluna que define se o item é peça ou montagem (conjunto).
            /// </summary>
            Tipo = 1,
            /// <summary>
            ///     Coluna que contém o nome do artigo
            /// </summary>
            NomeItem = 2,
            /// <summary>
            ///     Nível da estrutura (ordem dos itens na estrutura).
            /// </summary>
            Nivel  =3,
            /// <summary>
            ///     Coluna que contém se o item está ou não ativo.
            /// </summary>
            Ativo = 4,
            /// <summary>
            ///     Coluna que contém a revisão do artigo (peça/montagem)
            /// </summary>
            Revisao = 5,
        }

        /// <summary>
        ///     Contém uma coleção dos nós selecionados pelo usuário na interface.
        /// </summary>
        private List<UltraTreeNode> mNosSelecionados = new List<UltraTreeNode>();

        #region Propriedades

        /// <summary>
        ///     Propriedade somente leitura que contém a lista dos nós selecionados.
        /// </summary>
        public List<UltraTreeNode> _NosSelecionados
        {
            get
            {
                return mNosSelecionados;
            }
            //set
            //{
            //    mNosSelecionados = value;
            //}
        }

        #endregion

        #endregion

        #region Métodos

        public FrmPeca_Seleciona_Db()
        {
            InitializeComponent();

            // melhorar desempenho e não mostrar itens carregando, no final eu torno visível.
            utv.Visible = false;

            ConsultaEstruturaArtigos();

            utv.Visible = true;
        }

        /// <summary>
        ///     Método que consulta a estrutura de artigos do banco de dados do Edgecam Manage.
        /// </summary>
        private void ConsultaEstruturaArtigos()
        {
            ListaArtigos lst = new ListaArtigos();
            MontaEstruturaArvore(lst);
        }

        /// <summary>
        ///     Método que monta a estrutura de pai e filho na grade de dados em forma de árvore.
        /// </summary>
        /// <param name="ListaArtigos">Lista de peças do banco de dados SQL</param>
        private void MontaEstruturaArvore(ListaArtigos ListaArtigos)
        {
            List<String> processados = new List<string>();

            if (ListaArtigos != null && ListaArtigos.Count > 0)
            {
                foreach (Artigo a in ListaArtigos)
                {
                    //Se já foi processado, ignora o item.
                    if (processados.Where(x => x.ToUpper() == a.id.ToString().ToUpper()).Count() > 0)
                        continue;

                    //Adiciona o item na lista de processados.
                    processados.Add(a.id.ToString());

                    //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                    UltraTreeNode n = utv.Nodes.Add();

                    //Primeira adiciona o no pai
                    n.Cells[(int)e_SkaColunas.Selecionar].Value = false;

                    //Adiciona a imagem ao tipo de componente (peça ou conjunto)
                    EmbeddableImageRenderer embeddableImageRenderer = new EmbeddableImageRenderer();
                    embeddableImageRenderer.DrawBorderShadow = false;
                    n.Cells[(int)e_SkaColunas.Tipo].Editor = embeddableImageRenderer;
                    n.Cells[(int)e_SkaColunas.Tipo].Value = DefineImagemArtigo(a.IsPai);

                    n.Cells[(int)e_SkaColunas.NomeItem].Value = a.NomePeca;
                    n.Cells[(int)e_SkaColunas.Nivel].Value = a.Nivel;
                    n.Cells[(int)e_SkaColunas.Ativo].Value = a.Ativo;
                    n.Cells[(int)e_SkaColunas.Revisao].Value = a.Revisao;

                    //Procuro os filhos
                    List<Artigo> lstFilhos = ListaArtigos.Where(x => x.NomePai.ToUpper() == a.NomePeca.ToUpper()).AsEnumerable().OrderBy(y => a.NomePeca).ToList();

                    //Possui filhos e os adiciono no grid.
                    if (lstFilhos.Count > 0)
                    {
                        foreach (Artigo filho in lstFilhos)
                        {
                            //Recebe os nós já adicionados no pai.
                            UltraTreeNode noFilho = n.Nodes.Add();

                            //Adiciona os elementos filhos à árvore
                            noFilho.Cells[(int)e_SkaColunas.Selecionar].Value = false;

                            //Adiciona a imagem ao tipo de componente (peça ou conjunto)
                            EmbeddableImageRenderer embeddableImageRenderer_f = new EmbeddableImageRenderer();
                            embeddableImageRenderer_f.DrawBorderShadow = false;
                            noFilho.Cells[(int)e_SkaColunas.Tipo].Editor = embeddableImageRenderer_f;
                            noFilho.Cells[(int)e_SkaColunas.Tipo].Value = DefineImagemArtigo(filho.IsPai);

                            noFilho.Cells[(int)e_SkaColunas.NomeItem].Value = filho.NomePeca;
                            noFilho.Cells[(int)e_SkaColunas.Nivel].Value = filho.Nivel;
                            noFilho.Cells[(int)e_SkaColunas.Ativo].Value = filho.Ativo;
                            noFilho.Cells[(int)e_SkaColunas.Revisao].Value = filho.Revisao;

                            n.Nodes.Add(noFilho);

                            //Adiciona o item na lista de processados.
                            processados.Add(filho.id.ToString());
                        }
                    }
                    utv.Nodes.Add(n);
                }

                //  Após o 'foreach', expande a árvore.
                utv.ExpandAll();
            }
        }

        /// <summary>
        ///     Método que define a imagem do artigo (peça), se é conjunto (montagem) ou peça.
        /// </summary>
        /// <param name="IsPai">True para caso for pai, false caso for filho</param>
        /// <returns>Bitmap contendo o ícone do componente</returns>
        private Bitmap DefineImagemArtigo(Boolean IsPai)
        {
            switch (IsPai)
            {
                case true: return Edgecam_Manager.Properties.Resources.sw_assembly;
                case false: return Edgecam_Manager.Properties.Resources.sw_part;
                default: return null;
            }
        }

        /// <summary>
        ///     Método responsável por trocar os valores das checkboxes na grade de dados
        /// em forma de ávore.
        /// </summary>
        /// <param name="Nos">Lista contendo os nós da árvore</param>
        /// <param name="Valor">True para marcar a checkbox, false para desmarcar.</param>
        private void TrocaValorColunaSelecionar(TreeNodesCollection Nos, Boolean Valor)
        {
            if (Nos.Count > 0)
            {
                foreach (UltraTreeNode n in Nos)
                {
                    n.Cells[(int)e_SkaColunas.Selecionar].Value = Valor;
                    TrocaValorColunaSelecionar(n.Nodes, Valor);
                }                
            }
        }

        /// <summary>
        ///     Método que salva os nós marcados pelo usuário em uma lista (propriedade)
        /// somente leitura.
        /// </summary>
        private void SalvaNosSelecionados()
        {
            if (ForamSelecionadosNos())
            {
                BuscaNosSelecionados(utv.Nodes);
                btnReturn_Click(new object(), new EventArgs());
            }
            else MessageBox.Show("Por favor, selecione uma ou mais peças para prosseguir", "Peças não selecionadas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        ///     Método que percorre os nós da estrutura da árvore e busca as peças marcadas.
        /// </summary>
        /// <param name="Nos">Coleção com os nós de um nó pai (no caso, os filhos)</param>
        private void BuscaNosSelecionados(TreeNodesCollection Nos)
        {
            if (Nos.Count > 0)
            {
                foreach (UltraTreeNode n in Nos)
                {
                    if ((bool)n.Cells[(int)e_SkaColunas.Selecionar].Value == true)
                    {
                        //Se tiver nós dentro do 'n', significa que ele é pai de alguém.
                        if (n.HasNodes)
                            BuscaNosSelecionados(n.Nodes);
                        //Caso contrário, significa que é uma peça.
                        else mNosSelecionados.Add(n);
                    }
                }
            }
        }

        /// <summary>
        ///     Método que percorre a árvore na interface e verifica se o usuário marcou
        /// ao menos uma peça para criar uma ordem de produção.
        /// </summary>
        /// <returns>True caso o usuário tenha selecionado ao menos uma peça, false para não ter selecionado nenhuma.</returns>
        private Boolean ForamSelecionadosNos()
        {
            for (int i = 0; i < utv.Nodes.Count; i++)
            {
                if ((bool)utv.Nodes[i].Cells[(int)e_SkaColunas.Selecionar].Value == true)
                    return true;
            }

            return false;
        }

        #endregion

        #region Eventos

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaNosSelecionados();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar peças para criar ordem de produção", "FrmSelectParts", "btnSave_Click", "Exceção não tratada em uma tentativa de selecionar peças",
                                           "Consultas_EcMgr.CADASTRA_NOVA_TAREFA/ATUALIZA_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Clique com o botão direito do mouse sobre o UltraTree que ativa a opção
        /// de expandir todos os itens da árvore.
        /// </summary>
        private void tsmi_expandirTodos_Click(object sender, EventArgs e)
        {
            utv.ExpandAll();
        }

        /// <summary>
        ///     Clique com o botão direito do mouse sobre o UltraTree que ativa a opção
        /// de recolher todos os itens da árvore.
        /// </summary>
        private void tsmi_recolherTodos_Click(object sender, EventArgs e)
        {
            utv.CollapseAll();
        }

        /// <summary>
        ///     Clique com o botão direito do mouse sobre o UltraTree que ativa a opção
        /// de marcar todos os itens da árvore.
        /// </summary>
        private void tsmi_marcarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                if (utv.Nodes.Count > 0)
                {
                    TrocaValorColunaSelecionar(utv.Nodes, true);
                }
            }
            catch { }
        }

        /// <summary>
        ///     Clique com o botão direito do mouse sobre o UltraTree que ativa a opção
        /// de desmarcar todos os itens da árvore.
        /// </summary>
        private void tsmi_desmarcaTodos_Click(object sender, EventArgs e)
        {
            if (utv.Nodes.Count > 0)
            {
                TrocaValorColunaSelecionar(utv.Nodes, false);
            }
        }

        #endregion
    }
}
