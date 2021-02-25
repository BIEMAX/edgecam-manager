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
    internal partial class FrmPeca_Importa : Form
    {
        #region Variáveis globais

        /// <summary>
        ///     Representa as colunas do 'UltraTreeView' em forma de índices (números
        /// inteiros), onde, zero (0) é a primeira coluna.
        /// </summary>
        private enum e_SkaColunas
        {
            /// <summary>
            ///     Coluna de checkbox que determina se deve ou não importar a peça
            /// </summary>
            Importar = 0,
            /// <summary>
            ///     Coluna que define se o item é peça ou montagem (conjunto).
            /// </summary>
            Tipo = 1,
            /// <summary>
            ///     Coluna que contém o nome do artigo
            /// </summary>
            NomeItem = 2,
            /// <summary>
            ///     Contém a pasta da peça.
            /// </summary>
            CaminhoItem = 3,
            /// <summary>
            ///     Contém a revisão do Item (pode ser letra ou número, pra mim é string).
            /// </summary>
            Revisao = 4,
            /// <summary>
            ///     Contém o centro de trabalho (máquina cadastrada)
            /// </summary>
            Maquina = 5,
            /// <summary>
            ///     Contém o material da peça.
            /// </summary>
            Material = 6,
            /// <summary>
            ///     Nível da estrutura (ordem dos itens na estrutura).
            /// </summary>
            Nivel = 7,
            /// <summary>
            ///     Coluna que contém se o item está ou não ativo.
            /// </summary>
            Ativo = 8            
        }

        #endregion

        #region Propriedades

        #endregion

        #region Instância dos objetos da classe

        public FrmPeca_Importa()
        {
            InitializeComponent();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que incializa os valores padrões do formulário.
        /// </summary>
        private void InicializaValoresDefault()
        {
            btnImportar.Enabled = false;
        }

        /// <summary>
        ///     Método responsável por realizar a seleção de peças nativas do SolidWorks.
        /// </summary>
        private void SelecionaPecas()
        {
            Utilities util = new Utilities();
            Dictionary<String, Object> dic = new Dictionary<String, Object>();
            dic.Add("Montagens", "sldasm");
            dic.Add("Peças", "sldprt");
            List<String> lst = util.BuscaArquivos("Selecione um ou mais arquivos nativos do SolidWorks", "Arquivos SOLIDWORKS (*.sldprt;*.sldasm)|*.sldprt;*.sldas", dic);

            CarregaListaPecasGrid(lst);
        }

        /// <summary>
        ///     Aa
        /// </summary>
        /// <param name="LstPecas"></param>
        private void CarregaListaPecasGrid(List<String> LstPecas)
        {
            if (LstPecas == null && LstPecas.Count == 0) return;

            List<String> processados = new List<string>();

            //Instância o objeto.
            //SkaSwDocMgr docMgr = new SkaSwDocMgr(SkaSwDocMgr.e_SkaVersaoDc.V2018);

            foreach (String s in LstPecas)
            {
                //Se estiver vazio a string, o arquivo não existir ou se já foi processado, continua.
                if (String.IsNullOrEmpty(s) || !System.IO.File.Exists(s) || processados.Where(x => x.ToUpper().Trim() == s.ToUpper().Trim()).Count() > 0)
                    continue;

                processados.Add(s);

                if (s.ToUpper().Trim().EndsWith(".SLDPRT"))
                {
                    //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                    UltraTreeNode tmp = utv.Nodes.Add();

                    //Primeira adiciona o no pai
                    tmp.Cells[(int)e_SkaColunas.Importar].Value = true;

                    //Adiciona a imagem ao tipo de componente (peça ou conjunto)
                    EmbeddableImageRenderer embeddableImageRenderer = new EmbeddableImageRenderer();
                    embeddableImageRenderer.DrawBorderShadow = false;
                    tmp.Cells[(int)e_SkaColunas.Tipo].Editor = embeddableImageRenderer;
                    tmp.Cells[(int)e_SkaColunas.Tipo].Value = Properties.Resources.sw_part;

                    tmp.Cells[(int)e_SkaColunas.NomeItem].Value = s.Substring(s.LastIndexOf("\\") + 1);
                    tmp.Cells[(int)e_SkaColunas.CaminhoItem].Value = s.Substring(s.LastIndexOf("\\"));
                    tmp.Cells[(int)e_SkaColunas.Revisao].Value = 1;//Sempre deixar como 1, pois ficaria inviável deixar zero.
                    tmp.Cells[(int)e_SkaColunas.Maquina].Value = "";
                    tmp.Cells[(int)e_SkaColunas.Material].Value = "";
                    tmp.Cells[(int)e_SkaColunas.Nivel].Value = 0;
                    tmp.Cells[(int)e_SkaColunas.Ativo].Value = 1;

                    utv.Nodes.Add(tmp);
                }
                else
                {

                }
            }
        }

        private void ImportaPeca()
        {

        }

        private void ImportaMontagem()
        {

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
                SelecionaPecas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar selecionar as peças", "FrmPeca_Importa", "btnSelecionar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}