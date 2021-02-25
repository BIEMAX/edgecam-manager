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
using System.IO;
using ImagedComboBox;
using Edgecam_Manager.Idiomas;
using Infragistics.Win.UltraWinGrid;

namespace Edgecam_Manager
{
    internal partial class FrmOrdens : Form
    {

        #region Variáveis da classe

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados da grade dos dados.
        /// </summary>
        private Exporter mExporter;

        #endregion

        public FrmOrdens()
        {
            InitializeComponent();
            //DefineLanguage();
            InicializaValoresDefault();
            CarregaEstruturaOrdens();
        }

        #region Métodos

        /// <summary>
        ///     Método que incializa 
        /// </summary>
        private void InicializaValoresDefault()
        {
            //Carrega as listas nas combo boxes
            icbxEstado.Items.Add(new ComboBoxItem("(Todos)"));
            icbxEstado.Items.Add(new ComboBoxItem("Pendente de planejamento", Properties.Resources.White));
            icbxEstado.Items.Add(new ComboBoxItem("Em andamento (Iniciado)", Properties.Resources.Green));
            icbxEstado.Items.Add(new ComboBoxItem("Aguardando aprovação (Sob-revisão)", Properties.Resources.PartiallyReleased));
            icbxEstado.Items.Add(new ComboBoxItem("Não aprovado (Em revisão)", Properties.Resources.Orange)); 
            icbxEstado.Items.Add(new ComboBoxItem("Liberado (Concluído)", Properties.Resources.Global));
            icbxEstado.Items.Add(new ComboBoxItem("Cancelado", Properties.Resources.Cancel));
            icbxEstado.Items.Add(new ComboBoxItem("Atrasado", Properties.Resources.Red));
            icbxEstado.SelectedIndex = 1;//Só trago as ordens pendentes de planejamento quando o usuário for consultar.

            //Adiciona as máquinas na lista.
            cbMaquinas.Items.Add("(Todos)");
            if (Objects.LstMaquinas != null)
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            else
            {
                Objects.LstMaquinas = new ListaMachines();
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            }
            cbMaquinas.SelectedIndex = 0;

            //Desabilita os botões da interface
            btnOpen.Enabled         = false;
            btnEdit.Enabled         = false;
            btnComplete.Enabled     = false;
            btnCancelar.Enabled     = false;
            btnViewDetails.Enabled  = false;
            btnDefineUser.Enabled   = false;
            btnView3D.Enabled       = false;
            btnPdfView.Enabled      = false;

            //Controles para seleção por data.
            dtEntrega.Enabled   = false;
            cbxUsarData.Checked  = false;

            //Carrega lista de clientes
            cbClientes.Items.Add("(Todos)");
            if (Objects.LstClientes != null)
            {
                cbClientes.Items.AddRange(Objects.LstClientes.Where(x => x.NomeEmpresa != "").Select(x => x.NomeEmpresa).ToArray());
            }
            cbClientes.SelectedIndex = 0;

            //Remove a ordenação anterior que o usuário pode ter feito (fica salvo internamente no sistema).
            UltraGridOptions udgv_op = new UltraGridOptions(udgv, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                              Imagens_NewLookInterface.ordenar_crescente_16,
                                                                              Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                              Imagens_NewLookInterface.remover_deletar,
                                                                              Imagens_NewLookInterface.agrupamento_16);

            //Objects.DefineColorThemeInterface(this);

            mExporter = new Exporter(udgv, btnExportar, "Ordens");

            BloqueiaAcoesDireitoMouse();
        }

        /// <summary>
        ///     Método que bloqueia algumas ações do grid (Editar, visualizar, etc), tudo
        ///  o que precisar de dados para trabalhar, ele bloqueia.
        /// </summary>
        private void BloqueiaAcoesDireitoMouse()
        {
            btnOpen.Enabled = false;
            btnEdit.Enabled = false;
            btnComplete.Enabled = false;
            btnCancelar.Enabled = false;
            btnViewDetails.Enabled = false;
            btnDefineUser.Enabled = false;
            btnView3D.Enabled = false;
            btnPdfView.Enabled = false;
        }

        /// <summary>
        ///     Método que desbloqueia algumas ações do grid (Editar, visualizar, etc), tudo
        ///  o que precisar de dados para trabalhar, ele desbloqueia.
        /// </summary>
        private void DesbloqueiaAcoesDireitoMouse()
        {
            btnOpen.Enabled = true;
            btnEdit.Enabled = true;
            btnComplete.Enabled = true;
            btnCancelar.Enabled = true;
            btnViewDetails.Enabled = true;
            btnDefineUser.Enabled = true;
            btnView3D.Enabled = true;
            btnPdfView.Enabled = true;
        }

        /// <summary>
        ///     Método responsável por carregar os centros de trabalhos e materiais das ordens de produção.
        /// </summary>
        private void CarregaEstruturaOrdens()
        {
            //Lista de imagens da árvore.
            ImageList imgLst = new ImageList();
            imgLst.Images.Add(Edgecam_Manager.Imagens_NewLookInterface.maquinas);
            imgLst.Images.Add(Edgecam_Manager.Imagens_NewLookInterface.materiais_camadas);
            tv.ImageList = imgLst;

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_CTS_MATERIAIS_DAS_ORDENS);

            if (dt != null && dt.Rows.Count > 0)
            {
                //Aqui eu faço uma consulta para obter os os centros de trabalho
                DataTable lstCts = dt.DefaultView.ToTable(true, "CentroTrabalho");

                if (lstCts != null && lstCts.Rows.Count > 0)
                {
                    //Aqui adiciona os centros de trabalho
                    for (int x = 0; x < lstCts.Rows.Count; x++)
                    {
                        //Adiciona o nó pai na árvore, ou seja, nome do trabalho
                        TreeNode noInicial = tv.Nodes.Add(String.Format("{0}", lstCts.Rows[x]["CentroTrabalho"].ToString()));
                        noInicial.ImageIndex = 0;

                        //Aqui adiciona os materiais
                        DataTable lstRaw = dt.Select(String.Format("CentroTrabalho = '{0}'", lstCts.Rows[x]["CentroTrabalho"].ToString())).CopyToDataTable();

                        if (lstRaw != null && lstRaw.Rows.Count > 0)
                        {
                            for (int y = 0; y < lstRaw.Rows.Count; y++)
                            {
                                if (!String.IsNullOrEmpty(lstRaw.Rows[y]["Material"].ToString()))
                                    AdicionaItensNaArvore(noInicial, lstRaw.Rows[y]["Material"].ToString(), 1);
                            }
                        }
                    }
                }
                //Expande a árvore.
                tv.ExpandAll();
            }
        }

        /// <summary>
        ///     Adiciona a árvore, um novo nó (seja ele pai ou filho)
        /// </summary>
        /// <param name="NoPai">Nó pai que vai receber o filho</param>
        /// <param name="NomeItemNaArvore">Nome do elemento à ser exibido na árvore</param>
        /// <param name="IndiceImagem">Indice da ImageList à ser atribuído</param>
        private void AdicionaItensNaArvore(TreeNode NoPai, String NomeItemNaArvore, int IndiceImagem)
        {
            TreeNode n = NoPai.Nodes.Add(NomeItemNaArvore);
            n.ImageIndex = IndiceImagem;
        }

        /// <summary>
        ///     Método que consulta as ordens de produção de acordo com os filtros do usuário.
        /// </summary>
        private void ConsultaOrdens()
        {
            Cursor = Cursors.WaitCursor;

            //ECMGR-250
            Objects.LimpaOrdenacaoColunasGrid(udgv);

            udgv.DataSource = SQLQueries.Consulta_OrdensProducao(txtOrdem.Text, txtTrabalho.Text, cbClientes.Text, cbxUsarData.Checked == true ? dtEntrega.DateTime.ToString("yyyy-MM-dd") : "", icbxEstado.SelectedIndex, cbMaquinas.SelectedItem.ToString());

            //Habilita os botões de editar e excluir.
            if (udgv.Rows.Count > 0)
            {
                DesbloqueiaAcoesDireitoMouse();
                udgv.ActiveRow = udgv.ActiveRowScrollRegion.FirstRow;
                DestacaItensAtrasados();
            }
            else BloqueiaAcoesDireitoMouse();

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///     Método que verifica se o usuário possuí alguma tarefa atrasada e muda a cor da linha para vermelho.
        /// </summary>
        private void DestacaItensAtrasados()
        {
            // Essa variável vai armazenar todos os ids das ordens de produção
            //que estão atrasadas.
            String ids = "";

            for (int x = 0; x < udgv.Rows.Count; x++)
            {
                if (udgv.Rows[x].Cells["Estado_Db"].Value.ToString() == "6")
                    continue;

                // 0 = mesma data  |  -1 = A segunda data é a maior |  1 = A primeira data é a menor
                if (DateTime.Compare(Convert.ToDateTime(udgv.Rows[x].Cells["Data de entrega"].Value.ToString()), Convert.ToDateTime(DateTime.Now.ToShortDateString())) == -1)
                {
                    //Adiciona a vírgula caso a variável já tenha algum ID salvo.
                    if (ids != "") ids += " , ";

                    udgv.Rows[x].Appearance.ForeColor = Color.Red;
                    udgv.Rows[x].Cells["Estado"].Value = Properties.Resources.Red;
                    ids += String.Format("'{0}'", udgv.Rows[x].Cells["id"].OriginalValue.ToString());
                }
            }

            if (!String.IsNullOrEmpty(ids))
            {
                Objects.CnnBancoEcMgr.ExecutaSql(String.Format(Consultas_EcMgr.ATUALIZA_ORDENS_COMO_ATRASADAS, ids));
            }

        }

        /// <summary>
        ///     Método que consulta as ordens de produção de acordo com o click
        /// na treview, onde contém os centros de trabalhos e materiais.
        /// </summary>
        private void ConsultaOrdensFromArvoreClick(String ItemSelecionado, Boolean IsWorkCenter)
        {
            Cursor = Cursors.WaitCursor;

            if (IsWorkCenter)
                udgv.DataSource = SQLQueries.Consulta_OrdensProducao("", "", "", "", 0, ItemSelecionado);
            else udgv.DataSource = SQLQueries.Consulta_OrdensProducao("", "", "", "", 0, "", ItemSelecionado);

            //Habilita os botões de editar e excluir.
            if (udgv.Rows.Count > 0)
            {
                DesbloqueiaAcoesDireitoMouse();
                udgv.ActiveRow = udgv.ActiveRowScrollRegion.FirstRow;
            }
            else BloqueiaAcoesDireitoMouse();

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///     Método que abre a interface para se criar uma nova ordem de produção.
        /// </summary>
        private void CriaNovaOrdem()
        {
            FrmOrdens_New frm;
            Objects.ImplementaNovoFormTela(frm = new FrmOrdens_New(), true);

            ////  Fico dentro do loop enquanto o formulário principal ainda tiver
            ////uma instância do formulário de nova interação.
            //do
            //{
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();
            //}
            //while (Objects.FormularioPrincipal.Controls.Contains(frm));

            //Consulta as ordens de produção novamente para trazer a ordem recém criada.
            btnPesquisar_Click(new object(), new EventArgs());

            Objects.SetaUltimaTuplaSelecionada(udgv);
        }

        /// <summary>
        ///     Abre a peça no Edgecam.
        /// </summary>
        private void AbreOrdemEdgecam()
        {
            Edgecam ec = new Edgecam(true);

            //TODO: COLOCAR A VERSÃO DO EDGECAM FIXA AQUI
            ec.LstUltimosArquivosAbertos("2018.20");

            FrmWaiting frm = null;

            //Monitora o tempo que o usuário está gastando para programar a peça.
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            try
            {
                Cursor = Cursors.WaitCursor;
                String dirPeca = "";

                if (udgv.Rows.Count > 0)
                {
                    //Obtém a linha selecionada pelo usuário.
                    var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                    /*
                     *  OK 1- Se esta ordem está aberta já
                     *  OK 2- Se está ordem já foi concluída a sua programação (Está sob revisão)
                     *  3- Se está ordem que está determinada como "iniciado" gerou PPF ou não.
                     *      3.1 - Se gerou PPF, abrir o mesmo, caso contrário, abrir o CAD
                     *  4- Se está ordem gerou PPF, verificar se é o mesmo centro de trabalho
                     *      4.1 - Caso não for o mesmo CT, informar o usuário e trocar na ordem
                     */

                    //O usuário só poderá abrir ordens de produção com o estado 1 e 2 (não iniciado e iniciado).
                    if (Convert.ToInt16(q.Cells["Estado_Db"].OriginalValue.ToString()) > 2)
                        MessageBox.Show("Você só pode abrir ordens de produção não iniciadas e iniciadas", "Estado inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //Se a ordem estiver aberta, dentro do método abaixo apresenta uma mensagem para o usuário.
                    else if (!IsOrdemAberta(q.Cells["id"].OriginalValue.ToString()))
                    {
                        //Se a ordem já estiver aberta com o mesmo usuário (ou por algum movito ficou presa), permito abrir a mesma.
                        if (IsOrdemAbertaMesmoUsuario(q.Cells["id"].OriginalValue.ToString()))
                        {
                            //Aqui contém o caminho do PPF.
                            DataTable dtPpf = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_Ec.BUSCA_ARQUIVO_PPF_ORDEM, new Dictionary<string, object>() { { "@JOB", q.Cells["Trabalho"].OriginalValue.ToString() } });

                            if (dtPpf.Rows.Count > 0)
                            {
                                dirPeca = String.IsNullOrEmpty(dtPpf.Rows[0]["JOB_CAM_FILE"].ToString()) == true ? dtPpf.Rows[0]["JOB_CAD_FILE"].ToString() : dtPpf.Rows[0]["JOB_CAM_FILE"].ToString();

                                //Só inicia o tempo aqui, pois irei iniciar a abertura da peça.
                                sw.Start();

                                //String dirPeca = Path.Combine(q.Cells["CaminhoPeca"].OriginalValue.ToString(), q.Cells["Peça"].OriginalValue.ToString());

                                //Congela a interface para o usuário, ou seja, ele vai ter que ficar aguardando.
                                frm = new FrmWaiting(dirPeca, q.Cells["id"].OriginalValue.ToString());
                                frm.ShowDialog();

                                sw.Stop();

                                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TEMPO_GASTO_ORDEM, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                                Double tempoJaGasto = 0.0;

                                if (dt.Rows.Count > 0)
                                    tempoJaGasto = Convert.ToDouble(dt.Rows[0][0].ToString());

                                tempoJaGasto += sw.Elapsed.Seconds;

                                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_ORDEM_USUARIO_NAO_ESTA_TRABALHANDO,
                                                                    new Dictionary<string, object>() 
                                                        {
                                                            { "@ID", q.Cells["id"].OriginalValue.ToString() },
                                                            { "@TMP", tempoJaGasto } 
                                                        });

                                frm.Close();
                            }
                        }
                        else return;
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                frm.Close();
                GC.Collect();

                Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        ///     Verifica se a ordem está aberta por outro usuário apenas, desconsiderando
        /// usuários.
        /// </summary>
        /// <param name="IdOrdem">id da ordem à ser verificada.</param>
        /// <returns>True caso a ordem estiver aberta, false para o contrário.</returns>
        private Boolean IsOrdemAberta(String IdOrdem)
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.VERIFICA_SE_ORDEM_ESTA_ABERTA, new Dictionary<string, object>() { { "@ID", IdOrdem } });

            if (dt.Rows.Count > 0)
            {
                // Retorno false caso o usuário que está trabalhando com a ordem de produção seja o mesmo usuário ativo
                //no nomento (isso permite que o usuário abra a ordem novamente).
                if (Objects.UsuarioAtual.Login.ToUpper() == dt.Rows[0]["UsuarioTrabalhando"].ToString().ToUpper())
                    return false;
                //else return Convert.ToBoolean(dt.Rows[0]["IsOpened"].ToString());
                else return true;
            }

            return true;
        }

        /// <summary>
        ///     Verifica se a ordem está aberta pelo menos usuário corrente do sistema.
        /// </summary>
        /// <param name="IdOrdem">id da ordem à ser verificada.</param>
        /// <returns>True caso a ordem estiver aberta com o mesmo usuário, false para o contrário.</returns>
        private Boolean IsOrdemAbertaMesmoUsuario(String IdOrdem)
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.VERIFICA_SE_ORDEM_ESTA_ABERTA, new Dictionary<string, object>() { { "@ID", IdOrdem } });

            if (dt.Rows.Count > 0)
            {
                //  Se for o mesmo usuário que estava com a peça aberta, eu permito ele abrir novamente,
                //pois o EC pode parar de funcionar, ou o computador desligar e a ordem pode ficar presa.
                if (Objects.UsuarioAtual.Login.ToUpper() == dt.Rows[0]["UsuarioTrabalhando"].ToString().ToUpper()) return true;
                else
                {
                    MessageBox.Show(String.Format("A peça está aberta pelo usuário '{0}' e não pode ser aberta novamente.", dt.Rows[0]["UsuarioTrabalhando"].ToString()), "Peça já está aberta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        ///     Método responsável por editar uma ordem de produção.
        /// </summary>
        private void EditaOrdem()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                Ordem o = new Ordem();
                o.IdOrdem           = q.Cells["id"].OriginalValue.ToString();
                o.OrdemProducao     = q.Cells["Ordem de produção"].OriginalValue.ToString();
                o.Pedido            = q.Cells["Pedido"].OriginalValue.ToString();
                o.Cliente           = q.Cells["Cliente"].OriginalValue.ToString();
                o.Material          = q.Cells["Material"].OriginalValue.ToString();
                o.UsuarioResp       = q.Cells["Usuário responsável"].OriginalValue.ToString();
                o.Ambiente          = q.Cells["Ambiente_Db"].OriginalValue.ToString();
                o.Artigo            = q.Cells["Peça"].OriginalValue.ToString();
                o.CaminhoArtigo     = q.Cells["CaminhoPeca"].OriginalValue.ToString();
                o.EstadoOperacao    = q.Cells["Estado_Db"].OriginalValue.ToString();
                o.CentroTrabalho    = q.Cells["Centro de trabalho"].OriginalValue.ToString();
                o.Trabalho          = q.Cells["Trabalho"].OriginalValue.ToString();
                o.QtdeSolicitada    = q.Cells["Solicitado"].OriginalValue.ToString();
                o.QtdeRealizada     = q.Cells["Realizado"].OriginalValue.ToString();
                o.DataRequerida     = q.Cells["Data de solicitação"].OriginalValue.ToString();
                o.DataEntrega       = q.Cells["Data de entrega"].OriginalValue.ToString();
                o.Descricao         = q.Cells["Descrição"].OriginalValue.ToString();
                o.DadosAux1         = q.Cells["DadosAux1"].OriginalValue.ToString();
                o.DadosAux2         = q.Cells["DadosAux2"].OriginalValue.ToString();
                o.DadosAux3         = q.Cells["DadosAux3"].OriginalValue.ToString();
                o.DadosAux4         = q.Cells["DadosAux4"].OriginalValue.ToString();
                o.DadosAux5         = q.Cells["DadosAux5"].OriginalValue.ToString();
                o.DadosAux6         = q.Cells["DadosAux6"].OriginalValue.ToString();
                o.DadosAux7         = q.Cells["DadosAux7"].OriginalValue.ToString();
                o.DadosAux8         = q.Cells["DadosAux8"].OriginalValue.ToString();

                FrmOrdens_New frm;
                Objects.ImplementaNovoFormTela(frm = new FrmOrdens_New(o, false));

                //  Fico dentro do loop enquanto o formulário principal ainda tiver
                //uma instância do formulário de nova interação.
                do
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
                while (Objects.FormularioPrincipal.Controls.Contains(frm));

                //Consulta as ordens de produção novamente para trazer a ordem recém criada.
                btnPesquisar_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Abre a visualização dos detalhes da ordem de produção.
        /// </summary>
        private void VisualizaDetalhesOrdem()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                Ordem o = new Ordem();
                o.IdOrdem = q.Cells["id"].OriginalValue.ToString();
                o.OrdemProducao = q.Cells["Ordem de produção"].OriginalValue.ToString();
                o.Pedido = q.Cells["Pedido"].OriginalValue.ToString();
                o.Cliente = q.Cells["Cliente"].OriginalValue.ToString();
                o.Material = q.Cells["Material"].OriginalValue.ToString();
                o.UsuarioResp = q.Cells["Usuário responsável"].OriginalValue.ToString();
                o.Ambiente = q.Cells["Ambiente_Db"].OriginalValue.ToString();
                o.Artigo = q.Cells["Peça"].OriginalValue.ToString();
                o.EstadoOperacao = q.Cells["Estado_Db"].OriginalValue.ToString();
                o.CentroTrabalho = q.Cells["Centro de trabalho"].OriginalValue.ToString();
                o.Trabalho = q.Cells["Trabalho"].OriginalValue.ToString();
                o.QtdeSolicitada = q.Cells["Solicitado"].OriginalValue.ToString();
                o.QtdeRealizada = q.Cells["Realizado"].OriginalValue.ToString();
                o.DataRequerida = q.Cells["Data de solicitação"].OriginalValue.ToString();
                o.DataEntrega = q.Cells["Data de entrega"].OriginalValue.ToString();
                o.Descricao = q.Cells["Descrição"].OriginalValue.ToString();
                o.DadosAux1 = q.Cells["DadosAux1"].OriginalValue.ToString();
                o.DadosAux2 = q.Cells["DadosAux2"].OriginalValue.ToString();
                o.DadosAux3 = q.Cells["DadosAux3"].OriginalValue.ToString();
                o.DadosAux4 = q.Cells["DadosAux4"].OriginalValue.ToString();
                o.DadosAux5 = q.Cells["DadosAux5"].OriginalValue.ToString();
                o.DadosAux6 = q.Cells["DadosAux6"].OriginalValue.ToString();
                o.DadosAux7 = q.Cells["DadosAux7"].OriginalValue.ToString();
                o.DadosAux8 = q.Cells["DadosAux8"].OriginalValue.ToString();

                Objects.ImplementaNovoFormTela(new FrmOrdens_New(o, true));
            }
        }

        /// <summary>
        ///     Concluí a programação de uma peça e envia para o administrador do sistema aprovar
        /// a mesma ou recusar.
        /// </summary>
        private void CompletaProgramacao()
        {
            if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                if (MessageBox.Show("Deseja concluir a programação? Uma vez feita, só poderá ser desfeita pelo administrador do sistema!", "Concluir programação", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_ESTADO_ORDEM, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                    MessageBox.Show("Programação concluída. A ordem ficará aguardando revisão do administrador do sistema", "Programação concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPesquisar_Click(new object(), new EventArgs());
                }
                else return;
            }
        }

        /// <summary>
        ///     Cancela uma ordem de produção.
        /// </summary>
        private void CancelaOrdem()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                if (MessageBox.Show("Deseja realmente cancelar a ordem de produção?", "Cancelar ordem de produção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    FrmCancelar f = new FrmCancelar("ordem de produção", q.Cells["Ordem de produção"].OriginalValue.ToString());
                    f.ShowDialog();

                    if (!String.IsNullOrEmpty(f._Razao.Trim()))
                    {
                        Dictionary<String, Object> d = new Dictionary<string, object>();
                        d.Add("@IDORDEM", q.Cells["id"].OriginalValue.ToString());
                        d.Add("@INFO", $"Usuário cancelou a ordem de produção pela seguinte razão/motivo: '{f._Razao}'");
                        d.Add("@USR", Objects.UsuarioAtual.Login);
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ADICIONA_HISTORICO_ORDENS, d);
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CANCELA_ORDEM_DE_PRODUCAO_POR_ID, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });

                        MessageBox.Show("Ordem de produção cancelada com êxito", "Ordem cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Objects.CadastraNovoLog(false, $"Usuário '{Objects.UsuarioAtual.Login}' cancelou a OP '{q.Cells["id"].OriginalValue.ToString()}'", "FrmOrdens", "btnCancelar_Click", "<None>", "Consultas_EcMgr.CANCELA_ORDEM_DE_PRODUCAO", e_TipoErroEx.Informacao);

                        btnPesquisar_Click(new object(), new EventArgs());
                    }
                }
                else return;
            }
        }

        /// <summary>
        ///     Troca o usuário da ordem de produção (redesignação por algum motivo).
        /// </summary>
        private void TrocaUsuarioOrdem()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                //Ele só pode trocar o usuáruio em ordens de produção ainda não planejadas e iniciadas.
                if (Convert.ToInt16(q.Cells["Estado_Db"].OriginalValue.ToString()) <= 2)
                {
                    FrmSelecionaUsuario frm = new FrmSelecionaUsuario();
                    frm.ShowDialog();

                    if (!String.IsNullOrEmpty(frm._UsuarioSelecionado))
                    {
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.TROCA_USUARIO_ORDEM, new Dictionary<string, object>() { { "@USR", frm._UsuarioSelecionado }, { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                        MessageBox.Show("Usuário redesignado com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnPesquisar_Click(new object(), new EventArgs());
                    }
                }
                else MessageBox.Show("Você só pode trocar o usuário enquanto a ordem de produção em estado pendente de planejamento e iniciado", "Ação inválida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        ///     Abre a janela de visualização de modelos 3D.
        /// </summary>
        private void VisualizaModelo3D()
        {
            if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                String dirPeca = Path.Combine(q.Cells["CaminhoPeca"].OriginalValue.ToString(), q.Cells["Peça"].OriginalValue.ToString());

                //FrmOrdens_ViewModel frm = new FrmOrdens_ViewModel(@"C:\temp\Solidos\box.sldprt");
                FrmOrdens_ViewModel frm = new FrmOrdens_ViewModel(dirPeca);
                frm.ShowDialog();
            }
        }

        #endregion

        #region Eventos

        private void txtOrdem_Click(object sender, EventArgs e)
        {
            txtOrdem.Text = "";
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaOrdens();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao tentar contular as ordens", "FrmOrdens", "ConsultaOrdens", "<None>", "Consultas_EcMgr.CONSULTA_ORDENS", e_TipoErroEx.Erro, ex);
            }
        }
        private void cms_Opening(object sender, CancelEventArgs e)
        {
            //Iriei verificar se existem linhas na grade
            if(udgv.Rows != null && udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                switch(q.Cells["Estado_db"].Value.ToString())
                {
                    case "1":
                        btnOpen.Enabled = false;
                        btnEdit.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnComplete.Enabled = true;
                        btnDefineUser.Enabled = true;
                        btnViewDetails.Enabled = true;
                        btnView3D.Enabled = true;
                        btnPdfView.Enabled = true;
                        break;
                    case "2":
                        btnOpen.Enabled = true;
                        btnEdit.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnComplete.Enabled = true;
                        btnDefineUser.Enabled = true;
                        btnViewDetails.Enabled = true;
                        btnView3D.Enabled = true;
                        btnPdfView.Enabled = true;
                        break;
                    case "3":
                        btnOpen.Enabled = false;
                        btnEdit.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnComplete.Enabled = false;
                        btnDefineUser.Enabled = false;
                        btnViewDetails.Enabled = false;
                        btnView3D.Enabled = false;
                        btnPdfView.Enabled = false;
                        break;
                    case "4":
                        btnOpen.Enabled = false;
                        btnEdit.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnComplete.Enabled = false;
                        btnDefineUser.Enabled = false;
                        btnViewDetails.Enabled = false;
                        btnView3D.Enabled = false;
                        btnPdfView.Enabled = false;
                        break;
                    case "5":
                        btnOpen.Enabled = false;
                        btnEdit.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnComplete.Enabled = false;
                        btnDefineUser.Enabled = false;
                        btnViewDetails.Enabled = false;
                        btnView3D.Enabled = false;
                        btnPdfView.Enabled = false;
                        break;
                    case "6":
                        btnOpen.Enabled = false;
                        btnEdit.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnComplete.Enabled = false;
                        btnDefineUser.Enabled = false;
                        btnViewDetails.Enabled = false;
                        btnView3D.Enabled = false;
                        btnPdfView.Enabled = false;
                        break;

                    case "7":
                        btnOpen.Enabled = true;
                        btnEdit.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnComplete.Enabled = true;
                        btnDefineUser.Enabled = true;
                        btnViewDetails.Enabled = true;
                        btnView3D.Enabled = true;
                        btnPdfView.Enabled = true;
                        break;
                    default: break;
                }
            }
            //Desabilita os controles.
            else
            {                
                btnOpen.Enabled = false;
                btnEdit.Enabled = false;
                btnCancelar.Enabled = false;
                btnComplete.Enabled = false;
                btnDefineUser.Enabled = false;
                btnViewDetails.Enabled = false;
                btnView3D.Enabled = false;
                btnPdfView.Enabled = false;
            }
        }

        /// <summary>
        ///     Cria uma nova ordem de produção.
        /// </summary>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                CriaNovaOrdem();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar criar uma nova ordem", "FrmOrdens", "btnNew_Click", "<None>", "Consultas_EcMgr.CONSULTA_ORDENS", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Abre a peça no Edgecam (PPF caso já tenha sido programada/aberta ou o CAD caso contrário).
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                AbreOrdemEdgecam();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar abrir uma ordem", "FrmOrdens", "btnOpen_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EditaOrdem();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar editar uma ordem", "FrmOrdens", "EditaOrdem", "<None>", "Consultas_EcMgr.CONSULTA_ORDENS", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                CompletaProgramacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar completar uma ordem", "FrmOrdens", "btnComplete_Click", "<None>", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelaOrdem();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar cancelar uma ordem", "FrmOrdens", "btnDelete_Click", "<None>", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                VisualizaDetalhesOrdem();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar visualizar os detalhes da ordem", "FrmOrdens", "btnViewDetails_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDefineUser_Click(object sender, EventArgs e)
        {
            try
            {
                TrocaUsuarioOrdem();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao redesignar a ordem de produção", "FrmOrdens", "btnDefineUser_Click", "<None>", "Consultas_EcMgr.TROCA_USUARIO_ORDEM", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnView3D_Click(object sender, EventArgs e)
        {
            try
            {
                VisualizaModelo3D();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao visualizar modelo 3D da peça", "FrmOrdens", "btnView3D_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnPdfView_Click(object sender, EventArgs e)
        {

        }

        private void cbxUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarData.Checked)
                dtEntrega.Enabled = true;
            else dtEntrega.Enabled = false;
        }

        private void btnExpandePainel_Click(object sender, EventArgs e)
        {
            //False siginifica que ele está visível, true para invisível.
            if (!splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = true;
                btnExpandePainel.Image = Properties.Resources.arrow_direita;
            }
            //Se estiver invisível, mostra ele na inteface.
            else if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
                btnExpandePainel.Image = Properties.Resources.arrow_esquerda;
            }
        }

        /// <summary>
        ///     Evento para manter a mesma imagem do index (quando você clicava
        /// sobre um node, ele trocava a imagem).
        /// </summary>
        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //Ações que eu desconsidero.
                if (e.Action == TreeViewAction.ByKeyboard) return;
                if (e.Action == TreeViewAction.Collapse) return;
                if (e.Action == TreeViewAction.Expand) return;
                if (e.Action == TreeViewAction.Unknown) return;

                e.Node.SelectedImageIndex = e.Node.ImageIndex;

                if (e.Node.ImageIndex == 0)
                    ConsultaOrdensFromArvoreClick(e.Node.Text, true);
                else ConsultaOrdensFromArvoreClick(e.Node.Text, false);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar ordem de produção pela árvore", "FrmOrdens", "tv_AfterSelect", "<None>", "<None", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Abre o 'Context menu strip' e permite o usuário exportar os dados
        /// da grade de dados em arquivos definidos pelo mesmo.
        /// </summary>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            mExporter.MostrarCms();
        }

        private void udgv_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            VisualizaDetalhesOrdem();
        }

#endregion
    }
}