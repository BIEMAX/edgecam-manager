using Infragistics.Win;
using Infragistics.Win.UltraWinTree;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using interop.EmodelView;
//using eDrawingHostControl;

namespace Edgecam_Manager
{
    internal partial class FrmTrabalhos_New : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     Contém um objeto do trabalho atual.
        /// </summary>
        private TrabalhoEdgecam mJob;

        /// <summary>
        ///     Representa as colunas do 'UltraTreeView'.
        /// </summary>
        private enum e_Colunas
        {
            Img_Expandir = 0,
            Ordem = 1,
            /// <summary>
            ///     Contém o ícone da instrução
            /// </summary>
            Img_Tipo_Instrucao = 2,
            /// <summary>
            ///     Contém o tipo de instrução advindo do banco de dados.
            /// </summary>
            Tipo_Instrucao_Db = 3,
            Descricao = 4,
            Speed = 5,
            Feed = 6,
            Plunge = 7,
            Tempo_Usinagem = 8
        }

        /// <summary>
        ///     Contém o controle do EDrawings
        /// </summary>
        //private EModelViewControl mEmvControl = null;

        /// <summary>
        ///     Contém a base hexadecimal do eDrawings
        /// </summary>
        private eDrawingControl mHostContainer = null;

        #endregion

        #region Propriedades

        #endregion

        #region Instância dos objetos da classe

        public FrmTrabalhos_New()
        {
            InitializeComponent();
            InicializaValoresDefault();
        }

        /// <summary>
        ///     Instância o objeto para criar/visualizar/editar o trabalho.
        /// </summary>
        /// <param name="Trabalho">Objeto contendo um novo trabalho</param>
        /// /// <param name="DesabilitarControles">True para desabilitar os controles (apenas visualização)</param>
        public FrmTrabalhos_New(TrabalhoEdgecam Trabalho, Boolean DesabilitarControles = false)
        {
            InitializeComponent();
            mJob = Trabalho;

            InicializaValoresDefault();

            if (DesabilitarControles)
            {
                DesabilitaControlesInterface();
            }
            //else btnEdit.Visible = false;

            //Objects.DefineColorThemeInterface(this);
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que inicializa os valores default da classe/interface.
        /// </summary>
        private void InicializaValoresDefault()
        {
            Cursor = Cursors.WaitCursor;

            //  Se o objeto for diferente de null, significa que o usuário
            //está editando ou visualizando um trabalho.
            if (mJob != null)
            {
                CarregaTrabalho();
                ConsultaHistoricoTrabalho();
                ConsultaInstrucoes_Job();
                ConsultaFerramentas_Job();
                ConsultaImagens_Job();

                mJob.CaminhoArqPpf = @"C:\Program Files\Vero Software\EDGECAM 2020.0\cam\Examples\Machined Parts\2.5D Milling Support Bracket.ppf";

                if (File.Exists(Edgecam.ConvertAscII_ToString(mJob.CaminhoArqCad))) CarregaPreview_ArquivoCAD();
                else
                {
                    ultraTabControl1.Tabs[5].Visible = false;
                }


                if (File.Exists(Edgecam.ConvertAscII_ToString(mJob.CaminhoArqPpf))) CarregaPreview_ArquivoCAM();
                else
                {
                    ultraTabControl1.Tabs[6].Visible = false;
                }

                if (File.Exists(Edgecam.ConvertAscII_ToString(mJob.CaminhoArqCnc))) CarregaPreview_ArquivoCNC();
                else
                {
                    ultraTabControl1.Tabs[7].Visible = false;
                }
            }
            //  Significa que o usuário está criando um novo trabalho,
            //então eu escondo essas abas (que contém dados de trabalho
            //existente).
            else
            {
                ultraTabControl1.Tabs[2].Visible = false;
                ultraTabControl1.Tabs[3].Visible = false;
                ultraTabControl1.Tabs[4].Visible = false;
                ultraTabControl1.Tabs[5].Visible = false;
                ultraTabControl1.Tabs[6].Visible = false;
                ultraTabControl1.Tabs[7].Visible = false;
                ultraTabControl1.Tabs[8].Visible = false;
            }

            UltraGridOptions uop1 = new UltraGridOptions(udgv_Tools, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                 Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                 Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                 Imagens_NewLookInterface.remover_deletar,
                                                                                 Imagens_NewLookInterface.agrupamento_16);
        }

        private void CarregaTrabalho()
        {
            txtId.Text = mJob.IdJob.ToString();
            txtNome.Text = mJob.Descricao;
            txtFamilia.Text = mJob.Familia;
            txtSequencia.Text = mJob.Sequencia;
            txtPrg1.Text = mJob.ProgramId1;
            txtPrg2.Text = mJob.ProgramId2;
            rtxtComentario.Text = mJob.Comentario;
            txtCliente.Text = mJob.Cliente;
            txtProgramador.Text = mJob.Usuario;
            txtMaterial.Text = mJob.Material;
            txtTempo.Text = mJob.TempoDeCiclo;
            txtRev.Text = mJob.RevisaoJob;
            txtMqn.Text = mJob.PostoTrabalho;

            txtCAD.Text = Edgecam.ConvertAscII_ToString(mJob.CaminhoArqCad);
            txtCNC.Text = Edgecam.ConvertAscII_ToString(mJob.CaminhoArqCnc);
            txtCAM.Text = Edgecam.ConvertAscII_ToString(mJob.CaminhoArqPpf);

            txtGeral_Title.Text = mJob.JobNotesSubject;
            rtbGeral_Desc.Text = mJob.JobNotes;
            txtGeral_Arq.Text = mJob.JobNotesFile;
        }

        /// <summary>
        ///     Método que consulta o histórico 
        /// </summary>
        private void ConsultaHistoricoTrabalho()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_HISTORICO_TRABALHOS, new Dictionary<string, object>() { { "@IDJOB", mJob.IdJob } });

            if (dt != null && dt.Rows.Count > 0)
            {
                udgv_Historico.DataSource = dt;
            }
            else
            {
                String info = String.Format("O trabalho de id '{0}' não foi criado pelo sistema, e sim diretamente pelo edgecam.\r\nO mesmo agora está sendo gerenciado pelo sistema.", mJob.IdJob);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@IDJOB", mJob.IdJob);
                dic.Add("@INFO", info);
                dic.Add("@USR", Objects.UsuarioAtual.Login);

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ADICIONA_HISTORICO_TRABALHOS, dic);

                udgv_Historico.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_HISTORICO_TRABALHOS, new Dictionary<string, object>() { { "@IDJOB", mJob.IdJob } });
            }
        }

        private void HabilitaControlesInterface()
        {
            txtId.Enabled = true;
            txtNome.Enabled = true;
            txtFamilia.Enabled = true;
            txtSequencia.Enabled = true;
            txtPrg1.Enabled = true;
            txtPrg2.Enabled = true;
            rtxtComentario.Enabled = true;
            txtCliente.Enabled = true;
            txtProgramador.Enabled = true;
            txtMaterial.Enabled = true;
            txtTempo.Enabled = true;
            txtRev.Enabled = true;
            txtMqn.Enabled = true;
            txtCAD.Enabled = true;
            txtCNC.Enabled = true;
            txtCAM.Enabled = true;

            txtGeral_Title.Enabled = true;
            rtbGeral_Desc.Enabled = true;
            txtGeral_Arq.Enabled = true; 
        }

        private void DesabilitaControlesInterface()
        {
            txtId.Enabled = false;
            txtNome.Enabled = false;
            txtFamilia.Enabled = false;
            txtSequencia.Enabled = false;
            txtPrg1.Enabled = false;
            txtPrg2.Enabled = false;
            rtxtComentario.Enabled = false;
            txtCliente.Enabled = false;
            txtProgramador.Enabled = false;
            txtMaterial.Enabled = false;
            txtTempo.Enabled = false;
            txtRev.Enabled = false;
            txtMqn.Enabled = false;
            txtCAD.Enabled = false;
            txtCNC.Enabled = false;
            txtCAM.Enabled = false;

            txtGeral_Title.Enabled = false;
            rtbGeral_Desc.Enabled = false;
            txtGeral_Arq.Enabled = false;
        }

        /// <summary>
        ///     Método que consulta as instruções do trabalho e joga na interface.
        /// </summary>
        private void ConsultaInstrucoes_Job()
        {
            DataTable dt = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_INSTRUCOES_TRABALHO, new Dictionary<string, object>() { { "@JOBNAME", mJob.Descricao } });

            if (dt != null && dt.Rows.Count > 0)
            {
                CarregaInstrucoes_Arvore(dt);
            }
        }

        /// <summary>
        ///     Método que recebe uma lista de objeto e popula o UltraDataGridView.
        /// </summary>
        /// <param name="Instrucoes">Lista de objetos contendo as instruções de um determinado trabalho</param>
        private void CarregaInstrucoes_Arvore(DataTable Instrucoes)
        {
            List<String> processados = new List<string>();

            for (int x = 0; x < Instrucoes.Rows.Count; x++)
            {
                //Se já foi processado, ignora o item.
                if (processados.Where(p => p.ToUpper() == Instrucoes.Rows[x]["Ordem"].ToString().ToUpper().Trim()).Count() > 0)
                    continue;

                //Adiciona o item na lista de processados.
                processados.Add(Instrucoes.Rows[x]["Ordem"].ToString().ToUpper().Trim());

                //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                UltraTreeNode n = utv.Nodes.Add();

                //Primeira adiciona o no pai
                n.Cells[(int)e_Colunas.Ordem].Value = Instrucoes.Rows[x]["Ordem"].ToString();
                n.Cells[(int)e_Colunas.Tipo_Instrucao_Db].Value = Instrucoes.Rows[x]["Tipo_Instru_Db"].ToString();


                EmbeddableImageRenderer embeddableImageRenderer = new EmbeddableImageRenderer();
                embeddableImageRenderer.DrawBorderShadow = false;
                n.Cells[(int)e_Colunas.Img_Tipo_Instrucao].Editor = embeddableImageRenderer;
                n.Cells[(int)e_Colunas.Img_Tipo_Instrucao].Value = DefineImagemInstrucao(Instrucoes.Rows[x]["Tipo_Instru_Db"].ToString());

                n.Cells[(int)e_Colunas.Descricao].Value = Instrucoes.Rows[x]["Descrição"].ToString();
                n.Cells[(int)e_Colunas.Speed].Value = Instrucoes.Rows[x]["Velocidade (RPM)"].ToString();
                n.Cells[(int)e_Colunas.Feed].Value = Instrucoes.Rows[x]["Avanço lateral"].ToString();
                n.Cells[(int)e_Colunas.Plunge].Value = Instrucoes.Rows[x]["Avanço vertical"].ToString();
                n.Cells[(int)e_Colunas.Tempo_Usinagem].Value = Instrucoes.Rows[x]["Tempo do processo"].ToString();

                //Procuro os filhos
                var filhos = Instrucoes.Select(String.Format("Ordem like '{0}.%'", Instrucoes.Rows[x]["Ordem"].ToString()));

                //Possui filhos e os adiciono no grid.
                if (filhos != null && filhos.Count() > 0)
                {
                    for (int y = 0; y < filhos.Count(); y++)
                    {
                        //Recebe os nós já adicionados no pai.
                        UltraTreeNode noFilho = n.Nodes.Add();

                        //Primeira adiciona o no pai
                        //noFilho.Cells[0].Value = null;
                        noFilho.Cells[(int)e_Colunas.Ordem].Value = InsereTabArvore(filhos[y]["Ordem"].ToString(), 1);
                        noFilho.Cells[(int)e_Colunas.Tipo_Instrucao_Db].Value = filhos[y]["Tipo_Instru_Db"].ToString();

                        EmbeddableImageRenderer embeddableImageRenderer_f = new EmbeddableImageRenderer();
                        embeddableImageRenderer_f.DrawBorderShadow = false;
                        noFilho.Cells[(int)e_Colunas.Img_Tipo_Instrucao].Editor = embeddableImageRenderer_f;
                        noFilho.Cells[(int)e_Colunas.Img_Tipo_Instrucao].Value = DefineImagemInstrucao(filhos[y]["Descrição"].ToString());

                        noFilho.Cells[(int)e_Colunas.Descricao].Value = filhos[y]["Descrição"].ToString();
                        noFilho.Cells[(int)e_Colunas.Speed].Value = filhos[y]["Velocidade (RPM)"].ToString();
                        noFilho.Cells[(int)e_Colunas.Feed].Value = filhos[y]["Avanço lateral"].ToString();
                        noFilho.Cells[(int)e_Colunas.Plunge].Value = filhos[y]["Avanço vertical"].ToString();
                        noFilho.Cells[(int)e_Colunas.Tempo_Usinagem].Value = filhos[y]["Tempo do processo"].ToString();

                        n.Nodes.Add(noFilho);

                        //Adiciona o item na lista de processados.
                        processados.Add(filhos[y]["Ordem"].ToString());
                    }
                }

                //A primeira célula irá conter uma imagem para expandir a árvore.
                //Caso o nó seja um filho, não irá conter imagem nenhuma
                if (n.HasNodes)
                {
                    //  Tive de adicionar esses controles abaixo para conseguir visualizar a imagem (ainda não sei o motivo
                    //do porque eu precisar fazer isso!)
                    EmbeddableImageRenderer embeddableImageRenderer_p = new EmbeddableImageRenderer();
                    embeddableImageRenderer_p.DrawBorderShadow = false;
                    n.Cells[(int)e_Colunas.Img_Expandir].Editor = embeddableImageRenderer_p;
                    //n.Cells[(int)e_Colunas.Img_Expandir].Value = Properties.Resources.arrow_expandir;
                    n.Cells[(int)e_Colunas.Img_Expandir].Value = Imagens_NewLookInterface.subtrair_menos_16;
                }
                else
                {
                    EmbeddableImageRenderer embeddableImageRenderer_p = new EmbeddableImageRenderer();
                    embeddableImageRenderer_p.DrawBorderShadow = false;
                    n.Cells[(int)e_Colunas.Img_Expandir].Editor = embeddableImageRenderer_p;
                    //n.Cells[(int)e_Colunas.Img_Expandir].Value = Properties.Resources.arrow_direita;
                    //n.Cells[(int)e_Colunas.Img_Expandir].Value = Imagens_NewLookInterface.subtrair_menos_16;
                }

                utv.Nodes.Add(n);
            }

            utv.ExpandAll();
        }

        /// <summary>
        ///     Método que retorna um bitmap a partir de uma descrição
        /// </summary>
        /// <param name="Descricao">Descrição da instrução.</param>
        /// <returns>Bitmap</returns>
        private Bitmap DefineImagemInstrucao(String Descricao)
        {
            //switch (Descricao.ToUpper())
            //{
            //    #region Cases em inglês

            //    case "CREATE SETUP":        return Properties.Resources.create_setup;
            //    case "UPDATE FIXTURES":     return Properties.Resources.update_fixture;
            //    case "UPDATE STOCK":        return Properties.Resources.update_stock;
            //    case "MOVE TO TOOLCHANGE":  return Properties.Resources.toolchange;
            //    case "MOVE TO HOME":        return Properties.Resources.toolhome;
            //    case "RAPID MOVE":          return Properties.Resources.rapid_move;
            //    case "FEED MOVE":           return Properties.Resources.feed_move;
            //    case "COMMENT":             return Properties.Resources.comment;


            //    //Ciclos de usinagem MILL
            //    case "CHAMFER OPERATION":   return Properties.Resources.chamfer_cycle;
            //    case "FACE MILL":           return Properties.Resources.face_mill;
            //    //case "FACE MILL OPERATION": return Properties.Resources.face_mill;
            //    //case "FACE MILLING":        return Properties.Resources.face_mill;
            //    case "PROFILING":           return Properties.Resources.profiling_cycle;
            //    case "PENCIL MILL":         return Properties.Resources.pencil_mill_cycle;
            //    case "REST FINISHING":      return Properties.Resources.rest_finishing_cycle;
            //    case "SLOT MILLING":        return Properties.Resources.rest_finishing_cycle;
            //    case "FLAT LAND FINISHING": return Properties.Resources.flat_and_finishing_cycle;
            //    case "ROUGHING":            return Properties.Resources.roughing_cycle;
            //    case "HOLE":                return Properties.Resources.hole;

            //    //Ciclos de usinagem TURN


            //    //Ciclos de usinagem HOLE

            //    #endregion

            //    default: return null;
            //}
            return null;
        }

        /// <summary>
        /// Insere os espaços necessários para o nome de arquivo ficar indentado na árvore.
        /// </summary>
        /// <param name="Texto">Texto a ser adicionado as tabulações</param>
        /// <param name="QtdeTabs">Quantidade de tabulações a serem inseridas.</param>
        /// <returns>String original formatada com as tabulações.</returns>
        private String InsereTabArvore(String Texto, int QtdeTabs)
        {
            for (int i = 1; i <= QtdeTabs; i++)
            {
                Texto = "    " + Texto;
            }

            return Texto;
        }

        /// <summary>
        ///     Método que consulta as ferramentas utilizadas no trabalho do edgecam.
        /// </summary>
        private void ConsultaFerramentas_Job()
        {
            DataTable dt = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_TOOLS_BY_JOB, new Dictionary<string, object>() { { "@JOBID" , mJob.IdJob } });

            if (dt != null && dt.Rows.Count > 0)
            {
                //Lista de ferramentas já processadas pelo sistema (evita duplicidade)
                List<String> toolsProcessed = new List<string>();

                //Esse é um datatable temporário, esses serão os dados que irão para a interface.
                DataTable dados = new DataTable();
                dados.Columns.Add(new DataColumn("Posição", typeof(String)));
                dados.Columns.Add(new DataColumn("Nome da ferramenta", typeof(String)));
                dados.Columns.Add(new DataColumn("Diâmetro", typeof(Double)));
                dados.Columns.Add(new DataColumn("Tipo da ferramenta", typeof(Bitmap)));
                dados.Columns.Add(new DataColumn("Tempo total de uso", typeof(String)));
                dados.Columns.Add(new DataColumn("Quantidade utilizada", typeof(int)));

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    //Obtenho o nome da ferramenta (facilitar a manipulação).
                    String nomeTool = dt.Rows[x]["Nome da ferramenta"].ToString();

                    //Verifica se a ferramenta já foi processada anteriormente.
                    if (toolsProcessed.Where(t => t.ToUpper().Trim() == nomeTool.ToUpper().Trim()).Count() > 0) continue;

                    var r = dt.Select(String.Format("[Nome da ferramenta] = '{0}'", nomeTool));

                    //Se for localizado mais de uma ferramenta, faço alguns cálculos
                    if (r.Count() > 1)
                    {
                        List<String> tempos = new List<string>();

                        for (int y = 0; y < r.Count(); y++)
                        {
                            tempos.Add(r[y]["Tempo_Db"].ToString().Split(new char[] { '(' })[1].Replace(")", ""));
                        }

                        toolsProcessed.Add(nomeTool);
                        String tempoTotal = CustomTimes.SumTimes(tempos.ToArray());

                        Bitmap iconeTool = DefineIcone_Ferramenta(dt.Rows[x]["TL_TOOL_TYPE_MILL_ID"].ToString(), dt.Rows[x]["TL_TOOL_TYPE_TURN_ID"].ToString(), dt.Rows[x]["TL_TOOL_TYPE_HOLE_ID"].ToString());
                        dados.Rows.Add(dt.Rows[x]["Posição"].ToString(), nomeTool, dt.Rows[x]["Diâmetro"].ToString(), iconeTool, tempoTotal, r.Count().ToString());
                    }
                    else
                    {
                        toolsProcessed.Add(nomeTool);

                        Bitmap iconeTool = DefineIcone_Ferramenta(dt.Rows[x]["TL_TOOL_TYPE_MILL_ID"].ToString(), dt.Rows[x]["TL_TOOL_TYPE_TURN_ID"].ToString(), dt.Rows[x]["TL_TOOL_TYPE_HOLE_ID"].ToString());
                        dados.Rows.Add(dt.Rows[x]["Posição"].ToString(), nomeTool, dt.Rows[x]["Diâmetro"].ToString(), iconeTool, dt.Rows[x]["Tempo_Db"].ToString(), 1);
                    }
                }

                udgv_Tools.DataSource = dados;
            }
        }

        /// <summary>
        ///     Método que define o ícone da ferramenta de acordo com o seu ID.
        /// </summary>
        /// <param name="MillId"></param>
        /// <param name="TurnId"></param>
        /// <param name="HoleId"></param>
        /// <returns>Imagem da ferramenta</returns>
        private Bitmap DefineIcone_Ferramenta(String MillId, String TurnId, String HoleId)
        {
            if (!String.IsNullOrEmpty(MillId) && MillId != "-1")
            {
                //Ícone da ferramenta de fresamento.
                switch (MillId)
                {
                    case "0": return Imagens_Edgecam.Mill_0;
                    case "1": return Imagens_Edgecam.Mill_1;
                    case "2": return Imagens_Edgecam.Mill_2;
                    case "3": return Imagens_Edgecam.Mill_3;
                    case "4": return Imagens_Edgecam.Mill_4;
                    case "5": return Imagens_Edgecam.Mill_5;
                    case "6": return Imagens_Edgecam.Mill_6;
                    case "7": return Imagens_Edgecam.Mill_7;
                    case "8": return Imagens_Edgecam.Mill_8;
                    case "9": return Imagens_Edgecam.Mill_9;
                    default: return null;
                }
            }
            else if (!String.IsNullOrEmpty(TurnId) && TurnId != "-1")
            {
                //Ícone da ferramenta de fresamento.
                switch (TurnId)
                {
                    case "0": return Imagens_Edgecam.Turn_0; 
                    case "1": return Imagens_Edgecam.Turn_1; 
                    case "2": return Imagens_Edgecam.Turn_2; 
                    case "3": return Imagens_Edgecam.Turn_3; 
                    case "4": return Imagens_Edgecam.Turn_4; 
                    case "5": return Imagens_Edgecam.Turn_5; 
                    case "6": return Imagens_Edgecam.Turn_6; 
                    case "7": return Imagens_Edgecam.Turn_7;
                    default: return null;
                }
            }
            else
            {
                //Ícone da ferramenta de fresamento.
                switch (HoleId)
                {
                    case "0": return Imagens_Edgecam.Hole_0; 
                    case "1": return Imagens_Edgecam.Hole_1; 
                    case "2": return Imagens_Edgecam.Hole_2; 
                    case "3": return Imagens_Edgecam.Hole_3; 
                    case "4": return Imagens_Edgecam.Hole_4; 
                    case "5": return Imagens_Edgecam.Hole_5; 
                    case "6": return Imagens_Edgecam.Hole_6; 
                    case "7": return Imagens_Edgecam.Hole_7; 
                    case "8": return Imagens_Edgecam.Hole_8; 
                    default: return null;
                }
            }
        }

        /// <summary>
        ///     Método que consultas as imagens atreladas ao trabalho atual.
        /// </summary>
        private void ConsultaImagens_Job()
        {
            DataTable dt = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.BUSCA_IMAGENS_POR_JOBID, new Dictionary<string, object>() { { "@JOBID", mJob.IdJob } });

            if (dt != null && dt.Rows.Count > 0)
            {
                udgv_Imagens.DataSource = dt;
            }
        }

        /// <summary>
        ///     Método responsável por converter o byte array em imagem e 
        /// apresentar para o usuário.
        /// </summary>
        /// <param name="Imagem">Byte Array da imagem</param>
        private void VisualizaImagemEdgecam(Byte[] Imagem)
        {
            if (Imagem.Count() > 0)
            {
                //Aqui contém um array da imagem.
                MemoryStream ms = new MemoryStream(Imagem);

                pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.Refresh();
            }
        }

        /// <summary>
        ///     Método que insere uma imagem no banco de dados do Edgecam.
        /// </summary>
        /// <param name="ImageImportarPath"></param>
        public void InsereImagensBancoEc(string ImageImportarPath)
        {
            if (ImageImportarPath != null)
            {

                //Dictionary<string, object> dic = new Dictionary<string, object>();

                //FileStream fStreamImgUnk = File.OpenRead(ImageImportarPath);
                //byte[] bImg = new byte[fStreamImgUnk.Length];
                //fStreamImgUnk.Read(bImg, 0, bImg.Length);

                //dic.Add("@JOBID", JobId);
                //dic.Add("@NOMEJOB", JobName);
                //dic.Add("@NOMEIMG", ImageImportarPath.ToUpper().Substring(ImageImportarPath.LastIndexOf("\\") + 1).Replace(".JPG", ""));
                //dic.Add("@DIRIMG", string.Format(@"JM {0}\{1}", JobName, ImageImportarPath.Substring(ImageImportarPath.LastIndexOf("\\") + 1)));
                //dic.Add("@IMGORDER", ImageOrderEc());
                //dic.Add("@DTCREATE", DateTime.Now);
                //dic.Add("@DTMODIFY", DateTime.Now);
                //dic.Add("@IMGBUFFER", bImg);

                //sql.ExecutaSql(SkaConsultas.SQL_INSERE_MAGEM, dic);

                //CopiaImagem(ImageImportarPath);
            }
        }

        private void CarregaPreview_ArquivoCAD()
        {
            //Instância o objeto
            mHostContainer = new eDrawingControl(false, false);
            //ultraTabPageControl6.Controls.Add(mHostContainer);
            panel1.Controls.Add(mHostContainer);

            //Configurações do controle
            //mEmvControl = (EModelViewControl)mHostContainer.GetOcx();

            //if (mEmvControl != null)
            //{
            //    //Abre o arquivo de controle de peça no eDrawings 
            //    mEmvControl.OpenDoc(SkaEdgecam.ConvertAscII_ToString(mJob.CaminhoArqCad), false, false, false, "");

            //    //  Tive que fazer dessa forma, pois, por algum motivo, eu não consigo 
            //    //adicionar o controle do edrawings diretamente no 'ultraTabPageControl6',
            //    //então, eu criei um panel que fica escondido, depois de instânciar o objeto,
            //    //eu o adiciono via código para ser apresentado na interface.
            //    ultraTabPageControl6.Controls.Add(panel1);
            //    panel1.Dock = DockStyle.Fill;
            //}

            Cursor = Cursors.Arrow;
        }

        private void CarregaPreview_ArquivoCAM()
        {
            //PreviewHandler pre = new PreviewHandler();
            skaPreviewHandler1.Open(Edgecam.ConvertAscII_ToString(mJob.CaminhoArqPpf));
        }

        private void CarregaPreview_ArquivoCNC()
        {
            String cncContent = File.ReadAllText(Edgecam.ConvertAscII_ToString(mJob.CaminhoArqCnc));
            rtxtCnc.Text = cncContent;
        }

        #endregion

        #region Eventos

        private void utv_AfterCollapse(object sender, NodeEventArgs e)
        {
            try
            {
                if (e.TreeNode.HasNodes)
                {
                    e.TreeNode.Cells[(int)e_Colunas.Img_Expandir].Value = Imagens_NewLookInterface.subtrair_menos_16;
                }
            }
            catch { }
        }

        private void utv_AfterExpand(object sender, NodeEventArgs e)
        {
            try
            {
                if (e.TreeNode.HasNodes)
                {
                    e.TreeNode.Cells[(int)e_Colunas.Img_Expandir].Value = Imagens_NewLookInterface.adicionar_mais;
                }
            }
            catch { }
        }

        private void ultraGrid1_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            try
            {
                VisualizaImagemEdgecam((byte[])udgv_Imagens.Rows[e.Cell.Row.Index].Cells["JBP_IMAGE_BLOB"].Value);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar visualizar a imagem do trabalho", "FrmTrabalhos_New", "ultraGrid1_ClickCell", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void FrmTrabalhos_New_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                skaPreviewHandler1.UnloadPreviewHandler();
                this.Close();
            }
            catch(Exception ex)
            {
                Objects.CadastraNovoLog(false, "Erro ao tentar fechar a visualização dos trabalhos do edgecam", "FrmTrabalhos_New", "btnReturn_Click", "", "", e_TipoErroEx.Informacao, ex);
            }
            
        }

        #endregion

    }
}