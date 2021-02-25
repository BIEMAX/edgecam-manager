using Edgecam_Manager.Idiomas;
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

namespace Edgecam_Manager
{
    internal partial class FrmOrdens_New : Form
    {
        #region Variáveis globais

        /// <summary>
        ///     Contém um objeto da ordem que está para editar/visualizar.
        /// </summary>
        private Ordem mOrdem;

        /// <summary>
        ///     Representa as colunas do 'UltraTreeView'.
        /// </summary>
        private enum e_Colunas
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
            CaminhoItem = 3,
            /// <summary>
            ///     Coluna que contém a revisão do artigo (peça/montagem)
            /// </summary>
            Revisao = 4,
            /// <summary>
            ///     Coluna que contém a quantidade de peças à serem fabricadas.
            /// </summary>
            QuantidadeFabricar = 5
        }

        /// <summary>
        ///     Variável que determina se posso ou não tornar visível o botão de referência
        /// automatica para trabalhos.
        /// </summary>
        private Boolean mPodeHabilitarRefAutoJob = false;

        /// <summary>
        ///     Contém uma lista de campos não preenchidos.
        /// </summary>
        private List<String> mLstCampos;

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instancia um novo objeto para criação de uma nova ordem de produção.
        /// </summary>
        public FrmOrdens_New()
        {
            InitializeComponent();
            InicializaValoresDefault();
            //Objects.DefineColorThemeInterface(this);
        }

        /// <summary>
        ///     Instancia um novo objeto para edição ou visualização de uma ordem
        /// de produção.
        /// </summary>
        /// <param name="Ordem">Objeto contendo os dados da ordem</param>
        /// <param name="DesabilitarControles">True caso seja visulização, false para edição.</param>
        public FrmOrdens_New(Ordem Ordem, Boolean DesabilitarControles)
        {
            InitializeComponent();

            mOrdem = Ordem;

            InicializaValoresDefault();
            CarregaOrdem();

            if (DesabilitarControles)
            {
                DesabilitaControles();
            }
            else
            {
                //btnEdit.Visible = false;
                btnAddParts.Enabled = false;
            }

            //Objects.DefineColorThemeInterface(this);
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Inicializa os valores 'padrões' na interface (controles de filtros por exemplo, listas
        /// em bomboboxes, valores padrões dentre outros).
        /// </summary>
        private void InicializaValoresDefault()
        {
            //Radio button 'Torneamento' habilitado como inicial.
            //radioButton2.Checked = true;
            groupBox4.Enabled = false;

            //  Métodos que carregam classes nas combo boxes (resolvi fazer assim para ficar
            //mais organizado.
            //  As listas estáticas são populadas no 'FrmMain', método 'CarregaObjetosEstaticos',
            //onde, dentro desses métodos, eu apenas verifico se as listas estão vazias.
            CarregaListaUsuarios();
            CarregaListaMaquinas();
            CarregaListaMateriais();
            CarregaListaClientes();
            CarregaListaFamilias();
            
            //TODO: 'FrmOrdens_New' - Implementar um método para carrega a lista de pedidos

            //Define os controles como invisíveis.
            label15.Visible         = false;
            txtNewJobName.Visible   = false;
            cbxAddInfo.Visible      = false;
            ultraExpandableGroupBox4.Visible = false;
            ultraExpandableGroupBox5.Visible = false;
            ultraExpandableGroupBox6.Visible = false;
            label27.Visible         = false;
            txtJobComment.Visible   = false;
            label28.Visible         = false;
            cbFamilias.Visible      = false;

            //Desabilita os controles
            dtEntrega.Enabled       = false;

            //Adicionei esse valor para sempre puxar a data de entrega do dia atual.
            dtEntrega.DateTime = DateTime.Now;

            //Habilita os controles para referências automáticas.
            ValidaReferenciaAutomatica();

            //Desabilita botoões do direito do mouse sobre o grid de peças
            tsm_removePeca.Enabled = false;
            tsm_removeTodasPecas.Enabled = false;

            //Desabilita os botões de adição de adicionar dados de auxiliares e arquivos.
            btnAddDadoAux.Enabled = false;
            btnNewDadoAux.Enabled = false;
            btnNewFile.Enabled = false;
            btnAddFile.Enabled = false;
        }

        /// <summary>
        ///     Método que carrega a ordem (atributos) na interface para o usuário.
        /// </summary>
        private void CarregaOrdem()
        {
            if (mOrdem != null)
            {
                txtOrdem.Text = mOrdem.OrdemProducao;
                cbPedidos.Text = mOrdem.Pedido;
                cbClientes.Text = mOrdem.Cliente;
                cbMateriais.Text = mOrdem.Material;
                cbUsuarios.Text = mOrdem.UsuarioResp;
                //Radio Button
                CarregaAmbienteDefinido();
                cbMaquinas.Text = mOrdem.CentroTrabalho;
                txtJobComment.Text = mOrdem.Trabalho;
                dtEntrega.DateTime = Convert.ToDateTime(mOrdem.DataEntrega);

                #region Adiciona a peça na lista de peças à fabricar

                UltraTreeNode tmp = utv.Nodes.Add();

                //Primeira adiciona o no pai
                tmp.Cells[(int)e_Colunas.Selecionar].Value = true;

                //Adiciona a imagem ao tipo de componente (peça ou conjunto)
                EmbeddableImageRenderer embeddableImageRenderer = new EmbeddableImageRenderer();
                embeddableImageRenderer.DrawBorderShadow = false;
                tmp.Cells[(int)e_Colunas.Tipo].Editor = embeddableImageRenderer;
                tmp.Cells[(int)e_Colunas.Tipo].Value = Properties.Resources.sw_part;

                tmp.Cells[(int)e_Colunas.NomeItem].Value = mOrdem.Artigo;
                tmp.Cells[(int)e_Colunas.CaminhoItem].Value = mOrdem.CaminhoArtigo;
                tmp.Cells[(int)e_Colunas.Revisao].Value = 0;
                tmp.Cells[(int)e_Colunas.QuantidadeFabricar].Value = mOrdem.QtdeSolicitada;

                utv.Nodes.Add(tmp);

                utv.Enabled = false;

                #endregion

                if (!String.IsNullOrEmpty(mOrdem.Trabalho))
                    cbxCriarNewJob.Checked = true;

                txtNewJobName.Text = mOrdem.Trabalho;
                BuscaDadosAuxiliaresOrdem();
                BuscaArquivosAuxiliaresOrdem();
                BuscaHistoricoOrdem();
            }
        }

        /// <summary>
        ///     Busca todos os dados auxiliares atribuídos à ordem em questão.
        /// </summary>
        private void BuscaDadosAuxiliaresOrdem()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_DADOS_AUXILIARES_POR_ID_ORDEM, new Dictionary<string, object>() { { "@ID", mOrdem.IdOrdem } });

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }

            udgv_Dados.DataSource = dt;
        }

        /// <summary>
        ///     Busca todos os arquivos auxiliares atribuídos à ordem em questão.
        /// </summary>
        private void BuscaArquivosAuxiliaresOrdem()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ARQUIVOS_POR_ID_ORDEM, new Dictionary<string, object>() { { "@ID", mOrdem.IdOrdem } });

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }

            udgv_Arquivos.DataSource = dt;
        }

        /// <summary>
        ///     Método que consulta o histórico das ordens de produção.
        /// </summary>
        private void BuscaHistoricoOrdem()
        {
            udgv_Historico.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_HISTORICO_ORDENS, new Dictionary<string, object>() { { "@IDORDEM", mOrdem.IdOrdem } });
        }

        /// <summary>
        ///     Desabilta os controles da interface.
        /// </summary>
        private void DesabilitaControles()
        {
            txtOrdem.Enabled = false;
            cbPedidos.Enabled = false;
            cbClientes.Enabled = false;
            cbMateriais.Enabled = false;
            cbUsuarios.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            cbMaquinas.Enabled = false;
            dtEntrega.Enabled = false;

            //Aba edgecam
            txtNewJobName.Enabled = false;
            txtJobComment.Enabled = false;
            cbFamilias.Enabled = false;
            cbxCriarNewJob.Enabled = false;

            //Controles extras (opcionais que tem ações)
            btnRefAuto_Op.Enabled = false;
            btnRefAuto_Job.Enabled = false;
            cbxAddInfo.Enabled = false;
            cbxUsarData.Enabled = false;

            //Controles dos dados/arquivos auxiliares à ordem.
            btnAddDadoAux.Enabled = false;
            btnNewDadoAux.Enabled = false;
            btnAddFile.Enabled = false;
            btnNewFile.Enabled = false;

            //Esconde/Desativa alguns botões da interface
            cbxOrdensInc.Visible = false;
            btnAddParts.Enabled = false;

            btnSave.Visible = false;
            btnCancelar.Visible = false;
            btnEdit.Enabled = true;
        }

        /// <summary>
        ///     Habilita os controles da interface.
        /// </summary>
        private void HabilitaControles()
        {
            txtOrdem.Enabled = true;
            cbPedidos.Enabled = true;
            cbClientes.Enabled = true;
            cbMateriais.Enabled = true;
            cbUsuarios.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            cbMaquinas.Enabled = true;
            dtEntrega.Enabled = true;

            //Aba edgecam
            txtNewJobName.Enabled = true;
            txtJobComment.Enabled = true;
            cbFamilias.Enabled = true;
            cbxCriarNewJob.Enabled = true;

            //Controles extras (opcionais que tem ações)
            btnRefAuto_Op.Enabled = true;
            btnRefAuto_Job.Enabled = true;
            cbxAddInfo.Enabled = true;
            cbxUsarData.Enabled = true;

            //Controles dos dados/arquivos auxiliares à ordem.
            btnAddDadoAux.Enabled = true;
            btnNewDadoAux.Enabled = true;
            btnAddFile.Enabled = true;
            btnNewFile.Enabled = true;

            btnSave.Visible = true;
            btnCancelar.Visible = true;
            btnEdit.Enabled = false;
        }

        /// <summary>
        ///     Método que carrega a lista de usuários estática na classe
        /// 'Objects' na combo box 'cbxUsuarios'
        /// </summary>
        private void CarregaListaUsuarios()
        {
            //Carrega a lista de usuários na combo box
            cbUsuarios.Items.Add("<Selecione>");
            if (Objects.LstUsuarios != null)
            {
                //  Caso esse lista já tenha sido carregada anteriormente, apenas jogo os itens na combobox.
                cbUsuarios.Items.AddRange(Objects.LstUsuarios.ToArray());
            }
            cbUsuarios.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que carrega a lista de máquinas estática na classe
        /// 'Objects' na combo box 'cbxMaquinas'
        /// </summary>
        private void CarregaListaMaquinas()
        {
            //Carrega a lista de máquinas na combo box
            cbMaquinas.Items.Add("<Selecione>");
            if (Objects.LstMaquinas != null)
            {
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            }
            else
            {
                Objects.LstMaquinas = new ListaMachines();
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            }
            cbMaquinas.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que carrega a lista de materiais estática na classe
        /// 'Objects' na combo box 'cbxMateriais'
        /// </summary>
        private void CarregaListaMateriais()
        {
            //Lista de materiais
            cbMateriais.Items.Add("<Selecione>");
            if (Objects.LstMaquinas != null)
            {
                cbMateriais.Items.AddRange(Objects.LstMateriais.Where(x => x.NomeMaterial != "").Select(x => x.NomeMaterial).ToArray());
            }
            else
            {
                Objects.LstMateriais = new ListaMaterais();
                cbMateriais.Items.AddRange(Objects.LstMateriais.Where(x => x.NomeMaterial != "").Select(x => x.NomeMaterial).ToArray());
            }
            cbMateriais.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que carrega a lista de clientes estática na classe
        /// 'Objects' na combo box 'cbxClientes'
        /// </summary>
        private void CarregaListaClientes()
        {
            //Ignora a carga de dados na lista de o usuário está visualizando dados.
            if (mOrdem != null) return;

            //Carrega lista de clientes
            cbClientes.Items.Add("<Selecione>");
            if (Objects.LstClientes != null)
            {
                cbClientes.Items.AddRange(Objects.LstClientes.Where(x => x.NomeEmpresa != "").Select(x => x.NomeEmpresa).ToArray());
            }
            else
            {
                Objects.LstClientes = new ListaClientes();
                cbClientes.Items.AddRange(Objects.LstClientes.Where(x => x.NomeEmpresa != "").Select(x => x.NomeEmpresa).ToArray());
            }
            cbClientes.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que carrega a lista de familias dos trabalhos do edgecam de forma
        /// estática na classe 'Objects' na combo box 'cbxFamilias'
        /// </summary>
        private void CarregaListaFamilias()
        {
            cbFamilias.Items.Add("<Selecione>");
            cbFamilias.Items.AddRange((Objects.LstFamilias = new ListaFamilia()).Select(x => x.NomeFamilia).ToArray());
            cbFamilias.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que verifica se existe referência automática para 'Ordem de produção'
        /// e 'trabalhos' do Edgecam. Caso não existir, esconde os botões para utilizar
        /// as referências automáticas do sistema.
        /// </summary>
        private void ValidaReferenciaAutomatica()
        {
            //Verifica se existe 'ref auto para OP'
            if (Objects.ExisteReferenciaAutomatica("Ordens", "OrdemProducao"))
            {
                btnRefAuto_Op.Visible = true;
                cbxOrdensInc.Visible  = true;
            }
            else
            {
                btnRefAuto_Op.Visible = false;
                cbxOrdensInc.Visible  = false;
            }

            //Verifica se existe 'ref auto para trabalhos'
            if (Objects.ExisteReferenciaAutomatica("Ordens", "Trabalho"))
            {
                mPodeHabilitarRefAutoJob = true;
            }
            else mPodeHabilitarRefAutoJob = false;

            if (!mPodeHabilitarRefAutoJob)
                btnRefAuto_Job.Visible = false;
        }

        /// <summary>
        ///     Método que valida se todos os campos 'obrigatórios' estão devidamente preenchidos.
        /// </summary>
        /// <returns>True caso estiver tudo OK e false para caso contenha erros.</returns>
        /// <remarks>Não faz nenhum teste de conexão para verificar a autenticidade das informações.</remarks>
        private Boolean ValidaDadosAntesSalvar()
        {
            mLstCampos = new List<string>();

            int ret = 0;

            if (String.IsNullOrEmpty(txtOrdem.Text))
            {
                ret++;
                mLstCampos.Add("Ordem de produção");
            }

            if (cbUsuarios.SelectedIndex == 0)
            {
                ret++;
                mLstCampos.Add("Usuário responsável");
            }

            if (utv.Nodes.Count == 0)
            {
                ret++;
                mLstCampos.Add("Lista de peças");
            }

            if (cbxUsarData.Checked && String.IsNullOrEmpty(dtEntrega.DateTime.ToString()))
            {
                ret++;
                mLstCampos.Add("Data de entrega");
            }

            if (cbxCriarNewJob.Checked && String.IsNullOrEmpty(txtNewJobName.Text))
            {
                ret++;
                mLstCampos.Add("Nome do trabalho");
            }

            return ret == 0;
        }

        /// <summary>
        ///     Identifica o ambiente para criar as ordens de produção.
        /// </summary>
        /// <returns>Inteiro </returns>
        private int IdentificaAmbienteDefinido()
        {
            //Fresamento
            if (radioButton2.Checked)
                return 1;
            //Torneamento
            if (radioButton1.Checked)
                return 2;
            //Aditiva
            if (radioButton3.Checked)
                return 3;
            //Wire (Eletroerosão)
            if (radioButton4.Checked)
                return 4;
            else return 0;
        }

        /// <summary>
        ///     A partir de uma ordem existente, ele carrega ela na interface para o usuário.
        /// </summary>
        private void CarregaAmbienteDefinido()
        {
            if (mOrdem.Ambiente == "1") radioButton2.Checked = true;
            if (mOrdem.Ambiente == "2") radioButton1.Checked = true;
            if (mOrdem.Ambiente == "3") radioButton3.Checked = true;
            if (mOrdem.Ambiente == "4") radioButton4.Checked = true;

        }

        /// <summary>
        ///     Método que salva uma nova ordem de produção no banco de dados.
        /// </summary>
        private void SalvaOrdem()
        {
            Cursor = Cursors.WaitCursor;

            Dictionary<String, Object> dic = new Dictionary<String, Object>();

            if (ValidaDadosAntesSalvar())
            {
                #region OP

                //O campo 'ordem' é populado dentro do foreach.
                dic.Add("@ORDEM", "");
                dic.Add("@PEDIDO", cbPedidos.Text.ToString());
                //dic.Add("@CLIENTE", cbClientes.SelectedIndex != -1 && cbClientes.SelectedIndex > 0 ? cbClientes.SelectedItem.ToString() : "");
                dic.Add("@CLIENTE", cbClientes.Text.ToUpper().Trim() != "<SELECIONE>" ? cbClientes.Text : "");
                dic.Add("@MAT", cbMateriais.SelectedIndex != -1 && cbMateriais.SelectedIndex > 0 ? cbMateriais.SelectedItem.ToString() : "");
                dic.Add("@USR", cbUsuarios.SelectedIndex != -1 && cbUsuarios.SelectedIndex > 0 ? cbUsuarios.SelectedItem.ToString() : "");
                dic.Add("@AMBIENTE", IdentificaAmbienteDefinido());
                dic.Add("@ESTADO", 1);//Pendente de planejamento
                //O campo 'peça' é populado dentro do foreach
                dic.Add("@PECA", "");
                dic.Add("@DIRPECA", "");
                dic.Add("@ESTOP", 2);//'Em andamento', pendente de planejamento só é definido quando é recebido uma OP do ERP.
                dic.Add("@CT", cbMaquinas.SelectedIndex != -1 && cbMaquinas.SelectedIndex > 0 ? cbMaquinas.SelectedItem.ToString() : "");
                //O Campo 'Trabalho' é populado dentro do foreach
                //dic.Add("@JOB", cbxCriarNewJob.Checked == true ? txtNewJobName.Text : "");
                dic.Add("@JOB", "");
                //O campo 'quantidade solicitada' é populado dentro do foreach.
                dic.Add("@QTDESOL", "");
                dic.Add("@QTDEREA", 0);
                dic.Add("@DTREQ", DateTime.Now.ToString("yyyy-MM-dd"));//2017-10-26
                dic.Add("@DTENT", cbxUsarData.Checked == true ? dtEntrega.DateTime.Date.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"));//2017-10-26
                dic.Add("@DESC", "");
                dic.Add("@DA1", "");
                dic.Add("@DA2", "");
                dic.Add("@DA3", "");
                dic.Add("@DA4", "");
                dic.Add("@DA5", "");
                dic.Add("@DA6", "");
                dic.Add("@DA7", "");
                dic.Add("@DA8", "");

                /*
                 *  Dionei Beilke dos Santos
                 *  ECMGR-276
                 *  07/03/2019, at 08:16 AM
                 */
                dic.Add("@USR_CRT", Objects.UsuarioAtual.Login);
                dic.Add("@DT_CRT", DateTime.Now);
                dic.Add("@USR_ULT_MOD", Objects.UsuarioAtual.Login);
                dic.Add("@DT_ULT_MOD", DateTime.Now);

                #endregion

                #region Trabalho

                Dictionary<String, Object> dic2 = new Dictionary<String, Object>();
                //Essa chave abaixo contém o nome do trabalho e tambem é populada dentro do loop foreach.
                dic2.Add("@DESC", txtNewJobName.Text);
                dic2.Add("@COMMENT", txtJobComment.Text);
                dic2.Add("@FAMILY", cbFamilias.Text.ToUpper().Trim() != "<SELECIONE>" ? cbFamilias.Text : "");
                dic2.Add("@LOCATION", cbMaquinas.SelectedItem.ToString().ToUpper().Trim() != "<SELECIONE>" ? cbMaquinas.SelectedItem.ToString() : "");
                dic2.Add("@CUSTOMER", cbClientes.SelectedItem.ToString().ToUpper().Trim() != "<SELECIONE>" ? cbClientes.SelectedItem.ToString() : "");
                dic2.Add("@PROGRAMMER", Objects.UsuarioAtual.Login);
                dic2.Add("@MATERIAL", cbMateriais.SelectedIndex != -1 && cbMateriais.SelectedIndex > 0 ? cbMateriais.SelectedItem.ToString() : "");
                //@CAD é populado dentro do foreach
                dic2.Add("@CAD", "");
                dic2.Add("@REV", "1");//Revisão da ordem de produção.
                dic2.Add("@JOB_NOTES_SUBJECT", txtGeral_Title.Text);
                dic2.Add("@JOB_NOTES_FILE", txtGeral_Arq.Text);
                dic2.Add("@JOB_NOTES", rtbGeral_Desc.Text);
                dic2.Add("@FIX_NOTES_SUBJECT", txtFixacao_Title.Text);
                dic2.Add("@FIX_NOTES_FILE", txtFixacao_Arq.Text);
                dic2.Add("@FIX_NOTES", rtbFixacao_Desc.Text);
                dic2.Add("@STOCK_NOTES_SUBJECT", txtBruto_Title.Text);
                dic2.Add("@STOCK_NOTES_FILE", txtBruto_Arq.Text);
                dic2.Add("@STOCK_NOTES", rtbBruto_Desc.Text);
                dic2.Add("@DT_CRT", DateTime.Now);
                dic2.Add("@DT_MOD", DateTime.Now);
                //PARENT_JOB é populado dentro do foreach
                //dic2.Add("@PARENT_JOB", "");

                #endregion

                //Contém o pai de todas as ordens de produção, que nesse caso é o pai.
                //String parentJobId = "";

                /*
                 *  Dionei Beilke dos Santos
                 *  19/10/2018, at 07:44 Am
                 *  Precisei implementar esse método, por cause se der bosta,
                 * eu consigo voltar atrás.
                 */
                Sql sqlTmpMg = new Sql(Objects.CfgAtual._AuxStringConnectionSql, true);
                Sql sqlTmpEc = new Sql(Objects.CfgAtual._EcStringConnectionSql, true);

                int contador = 0;

                // Esse loop apenas atualiza o dicionário com os dados das peças
                foreach (UltraTreeNode n in utv.Nodes)
                {
                    try
                    {
                        dic["@ORDEM"]   = txtOrdem.Text;
                        dic["@PECA"]    = n.Cells[(int)e_Colunas.NomeItem].Value;
                        dic["@DIRPECA"] = n.Cells[(int)e_Colunas.CaminhoItem].Value;
                        dic["@JOB"]     = txtNewJobName.Text;
                        dic["@QTDESOL"] = n.Cells[(int)e_Colunas.QuantidadeFabricar].Value;

                        //Abaixo, executa uma tran
                        //Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVA_ORDEM_PRODUCAO, dic);
                        DataTable tmp = sqlTmpMg.ExecutaTransacaoComRetorno(Consultas_EcMgr.CADASTRA_NOVA_ORDEM_PRODUCAO, dic);
                        sqlTmpMg.ComitaTransacao();

                        //TOOD: Preciso verificar se o cliente já não existe, se não, eu vou criá-lo dentro do banco de dados.

                        /*
                         *  Dionei Beilke dos Santos
                         *  10/12/2018, at 05:52 PM
                         *  Adicionei a condição abaixo para vincular
                         * os dados e arquivos auxiliares da ordem.
                         */

                        AtribuiHistoricoOrdem(tmp.Rows[0]["id"].ToString());

                        if (tmp != null && tmp.Rows.Count > 0 && udgv_Dados.Rows.Count > 0)
                        {
                            AtribuiDadosAuxiliaresOrdem(tmp.Rows[0]["id"].ToString(), n.Cells[(int)e_Colunas.NomeItem].Value.ToString());
                            AtribuiArquivosAuxiliaresOrdem(tmp.Rows[0]["id"].ToString(), n.Cells[(int)e_Colunas.NomeItem].Value.ToString());
                        }

                        //Cria o novo trabalho no Edgecam (pai, por ser o primeiro trabalho à ser criado, será ele o PARENT ID dos demais).
                        //Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CADASTRA_NOVO_TRABALHO, dic2);
                        if (cbxCriarNewJob.Checked)
                        {
                            //Adiciona ao trabalho do Edgecam, o caminho do arquivo CAD.
                            dic2["@CAD"] = Path.Combine(n.Cells[(int)e_Colunas.NomeItem].Value.ToString(), n.Cells[(int)e_Colunas.CaminhoItem].Value.ToString());

                            sqlTmpEc.ExecutaTransacaoSemRetorno(Consultas_Ec.CADASTRA_NOVO_TRABALHO, dic2);
                            sqlTmpEc.ComitaTransacao();

                            #region Obsoleto

                            /*
                             *  Dionei Beilke dos Santos
                             *  28/11/2018, at 03:25 PM
                             *  Removi o trecho de código abaixo, pois não há motivos
                             * de ter essa informação no banco de dados do Edgecam, 
                             * então, eu não a insiro.
                             *  Deixei o código abaixo apenas para caso eu precisar
                             * utilizar em um futuro.
                             */

                            //Busca o parent id para os próximos trabalhos.
                            //if (String.IsNullOrEmpty(parentJobId))
                            //{
                            //    DataTable dt = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_ULTIMO_JOB_ID);

                            //    if (dt != null && dt.Rows.Count > 0)
                            //    {
                            //        parentJobId = dt.Rows[0]["JOB_JOB_ID"].ToString();
                            //        dic2["@PARENT_JOB"] = Convert.ToInt32(parentJobId);//Converto para inteiro para ver se é válido ID, caso contrário, irá gerar uma exceção.
                            //    }                                
                            //}
                            //else dic2["@PARENT_JOB"] = Convert.ToInt32(parentJobId);

                            #endregion

                            //  Se o usuário informar que as ordens devem ser incrementais e existir uma referência
                            //eu busco novas referências de ordens de produção e atualiza o controle 'txtOrdem.Text'.
                            if (cbxOrdensInc.Checked)
                                btnRefAuto_Job_Click(new object(), new EventArgs());

                            //Se o botão de buscar referência automática para trabalhos estiver visível,
                            //eu busco uma nova referência do trabalho, caso contrário, eu utilizo um
                            //contador para ter essa manipulação de criação de trabalhos.
                            if (btnRefAuto_Job.Visible)
                                dic2["@DESC"] = String.Format("{0}-{1}", txtNewJobName.Text, contador.ToString());
                            else btnRefAuto_Job_Click(new object(), new EventArgs());
                        }

                        //  Sempre que inserir um registro corretamente no banco de dados,
                        //removo o item da grade para não acontecer um novo problema.
                        utv.Nodes.Remove(n);
                    }
                    catch
                    {
                        sqlTmpMg.RoolbackTransacao();
                        sqlTmpEc.RoolbackTransacao();
                    }
                    finally { contador++; }

                }
                Cursor = Cursors.Arrow;
                Messages.Msg016();
                btnReturn_Click(new object(), new EventArgs());
            }
            else
            {
                if (MessageBox.Show("Alguns campos não foram preenchidos. Deseja visualizar?", "Campos não preenchidos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    FrmCamposNaoPreenchidos frm = new FrmCamposNaoPreenchidos(mLstCampos);
                    frm.ShowDialog();
                }
            }
        }

        /// <summary>
        ///     Método que verifica os arquivos atribuídos às ordens de produção.
        /// </summary>
        /// <param name="IdOrdem">Id da ordem de produção à ser vinculado às peças</param>
        /// <param name="NomePeca">Nome da peça que terá seus arquivos adicionados</param>
        private void AtribuiDadosAuxiliaresOrdem(String IdOrdem, String NomePeca)
        {
            if (udgv_Dados.Rows.Count > 0)
            {
                for (int x = 0; x < udgv_Dados.Rows.Count; x++)
                {
                    var r = udgv_Dados.Rows[x];

                    if (NomePeca.ToUpper().Trim() == r.Cells["Peça"].Text.ToUpper().Trim())
                    {
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.VINCULA_DADO_AUXILIAR_ORDEM, new Dictionary<string, object>() 
                        { 
                            { "@ORDEM", IdOrdem }, 
                            { "@DADOAUX", r.Cells["id"].Value.ToString() },
                            { "@USR", Objects.UsuarioAtual.Login }
                        });

                        //Remove a linha para não ter o risco de eu ler ela novamente.
                        r.Delete(false);//Determinar como false, pq senão fica apresentando uma mensagem para o usuário.
                    } else continue;
                }
            }
        }

        /// <summary>
        ///     Método que verifica os arquivos atribuídos às ordens de produção.
        /// </summary>
        /// <param name="IdOrdem">Id da ordem de produção à ser vinculado às peças</param>
        /// <param name="NomePeca">Nome da peça que terá seus arquivos adicionados</param>
        private void AtribuiArquivosAuxiliaresOrdem(String IdOrdem, String NomePeca)
        {
            if (udgv_Arquivos.Rows.Count > 0)
            {
                for (int x = 0; x < udgv_Arquivos.Rows.Count; x++)
                {
                    var r = udgv_Arquivos.Rows[x];

                    if (NomePeca.ToUpper().Trim() == r.Cells["Peça"].Text.ToUpper().Trim())
                    {
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.VINCULA_ARQUIVO_AUXILIAR_ORDEM, new Dictionary<string, object>() 
                        { 
                            { "@ORDEM", IdOrdem }, 
                            { "@DADOAUX", r.Cells["id"].Value.ToString() },
                            { "@USR", Objects.UsuarioAtual.Login }
                        });

                        //Remove a linha para não ter o risco de eu ler ela novamente.
                        r.Delete(false);//Determinar como false, pq senão fica apresentando uma mensagem para o usuário.
                    } else continue;
                }
            }
        }

        /// <summary>
        ///     Método que adiciona uma info no histórico da ordem de produção.
        /// </summary>
        /// <param name="IdOrdem">Id da ordem.</param>
        private void AtribuiHistoricoOrdem(String IdOrdem)
        {
            Dictionary<String, Object> dic = new Dictionary<string, object>();
            dic.Add("@IDORDEM", IdOrdem);
            dic.Add("@INFO", String.Format("Ordem de id '{0}' criada", IdOrdem));
            dic.Add("@USR", Objects.UsuarioAtual.Login);

            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ADICIONA_HISTORICO_ORDENS, dic);
        }

        /// <summary>
        ///     Método que abre o formulário para seleção de peças do banco de dados
        /// do edgecam manager para criação das ordens de produção.
        /// </summary>
        private void SelecionaPecasDb()
        {
            FrmPeca_Seleciona_Db frm = new FrmPeca_Seleciona_Db();
            frm.ShowDialog();

            foreach (UltraTreeNode n in frm._NosSelecionados)
            {
                //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                UltraTreeNode tmp = utv.Nodes.Add();

                //Primeira adiciona o no pai
                tmp.Cells[(int)e_Colunas.Selecionar].Value = true;

                //Adiciona a imagem ao tipo de componente (peça ou conjunto)
                EmbeddableImageRenderer embeddableImageRenderer = new EmbeddableImageRenderer();
                embeddableImageRenderer.DrawBorderShadow = false;
                tmp.Cells[(int)e_Colunas.Tipo].Editor = embeddableImageRenderer;
                tmp.Cells[(int)e_Colunas.Tipo].Value = Properties.Resources.sw_part;

                tmp.Cells[(int)e_Colunas.NomeItem].Value = n.Cells[(int)e_Colunas.NomeItem].Value;
                tmp.Cells[(int)e_Colunas.Revisao].Value = n.Cells[5].Value;//Coluna da revisão.
                tmp.Cells[(int)e_Colunas.QuantidadeFabricar].Value = 1;//Sempre deixar como 1, pois ficaria inviável deixar zero.

                utv.Nodes.Add(tmp);
            }

            HabilitaComandosArvore();
        }

        /// <summary>
        ///     Método que abre o dialog para seleção de peças do windows, habilitando
        /// a múltipla seleção de arquivos e validando se os mesmos são válidos para
        /// importar para dentro do edgecam.
        /// </summary>
        private void SelecionaPecasWindows()
        {
            Utilities util = new Utilities();
            var q = util.BuscaArquivos("Arquivos 3D", "Todos os arquivos (*.*)|*.*");

            //Significa que, ou o usuário cancelou a ação ou não selecionou nada.
            if (q == null) return;

            if (q.Count == 0) return;

            for (int x = 0; x < q.Count; x++)
            {
                //só adiciono o item se for de extensão válida.
                if (Edgecam.LstExtensoesValidasSolidos().Where(y => q[x].ToString().ToUpper().EndsWith(y.Extensao.ToUpper())).Count() == 0) continue;

                //Verifico se o item já não foi adicionado previamente. Se sim, passa para o próximo.
                if (JaExisteItemArvore(q[x].ToString().ToUpper().Substring(q[x].ToString().LastIndexOf("\\") + 1))) continue;

                //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                UltraTreeNode tmp = utv.Nodes.Add();

                //Primeira adiciona o no pai
                tmp.Cells[(int)e_Colunas.Selecionar].Value = true;

                //Adiciona a imagem ao tipo de componente (peça ou conjunto)
                EmbeddableImageRenderer embeddableImageRenderer = new EmbeddableImageRenderer();
                embeddableImageRenderer.DrawBorderShadow = false;
                tmp.Cells[(int)e_Colunas.Tipo].Editor = embeddableImageRenderer;
                tmp.Cells[(int)e_Colunas.Tipo].Value = Properties.Resources.sw_part;

                tmp.Cells[(int)e_Colunas.NomeItem].Value = q[x].ToString().ToUpper().Substring(q[x].ToString().LastIndexOf("\\") + 1);
                tmp.Cells[(int)e_Colunas.CaminhoItem].Value = q[x].ToString().Substring(0, q[x].ToString().LastIndexOf("\\"));
                tmp.Cells[(int)e_Colunas.Revisao].Value = 0;//Coluna da revisão.
                tmp.Cells[(int)e_Colunas.QuantidadeFabricar].Value = 1;//Sempre deixar como 1, pois ficaria inviável deixar zero.

                utv.Nodes.Add(tmp);
            }

            HabilitaComandosArvore();

            if (utv.Nodes.Count > 0)
            {
                btnAddDadoAux.Enabled = true;
                btnNewDadoAux.Enabled = true;

                btnNewFile.Enabled = true;
                btnAddFile.Enabled = true;
            }
        }

        /// <summary>
        ///     Verifica se uma determinada peça já foi adicionada à árvore.
        /// </summary>
        /// <param name="NomePeca">Nome da peça</param>
        /// <returns>Retorna true caso a peça já tenha sido adicionada.</returns>
        private Boolean JaExisteItemArvore(String NomePeca)
        {
            UltraTreeNode tmp = null;

            for (int x = 0; x < utv.Nodes.Count; x++)
            {
                tmp = utv.Nodes[x];

                if (tmp.Cells[(int)e_Colunas.NomeItem].Value.ToString().ToUpper().Trim() == NomePeca.ToUpper().Trim())
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Caso haja mais um ou mais nós dentro da árvore, habilita-se os comandos
        /// com o botão direito do mouse para remoção das peças para o usuário.
        /// </summary>
        private void HabilitaComandosArvore()
        {
            if (utv.Nodes.Count > 0)
            {
                tsm_removePeca.Enabled = true;
                tsm_removeTodasPecas.Enabled = true;
            }
        }

        /// <summary>
        ///     Método que remove o nó selecionado pelo usuário (ativo no caso).
        /// </summary>
        private void RemovePecaSelecionada()
        {
            utv.ActiveNode.Remove();

            if (utv.Nodes.Count <= 0)
            {
                tsm_removePeca.Enabled = false;
                tsm_removeTodasPecas.Enabled = false;
            }
        }

        /// <summary>
        ///     Método que remove todos os nós da árvore.
        /// </summary>
        private void RemoveTodasPecas()
        {
            utv.Nodes.Clear();

            if (utv.Nodes.Count <= 0)
            {
                tsm_removePeca.Enabled = false;
                tsm_removeTodasPecas.Enabled = false;
            }
        }

        /// <summary>
        ///     Abre a interface para o usuário cadastrar um novo dado auxiliar e já o carrega na interface.
        /// </summary>
        private void CadastraNovoDadoAuxiliar()
        {
            FrmDadosAuxiliares_New frm = new FrmDadosAuxiliares_New();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdDadoAuxiliar))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Dados.DataSource;

                if (dt == null)
                {
                    //udgv_Dados.DataSource = udgv_Dados.DataSource = SQLQueries.Consulta_DadosAuxiliares(frm._IdDadoAuxiliar, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                    udgv_Dados.DataSource = SQLQueries.Consulta_DadosAuxiliares(frm._IdDadoAuxiliar, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_DadosAuxiliares(frm._IdDadoAuxiliar, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Dados.DataSource = dt;
                }
            }
        }

        /// <summary>
        ///     Abre a interface que contém uma série de dados auxiliares previamente cadastrados.
        /// </summary>
        private void Consulta_DadoAuxiliar()
        {
            FrmDadosAuxiliares_Seleciona frm = new FrmDadosAuxiliares_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdDadoAuxSelecionado))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Dados.DataSource;

                if (dt == null)
                {
                    udgv_Dados.DataSource = SQLQueries.Consulta_DadosAuxiliares(frm._IdDadoAuxSelecionado, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_DadosAuxiliares(frm._IdDadoAuxSelecionado, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Dados.DataSource = dt;
                }

                //Chamo esse método para ele re-consultar o nome das peças selecionadas pelo usuário.
                udgv_Dados_InitializeLayout(new object(), null);
            }
        }

        /// <summary>
        ///     Método que carrega a lista de peças na dropdown list na grade de dados 'Dados auxiliares'
        /// </summary>
        private void CarregaListaPecas_GridDados()
        {
            //Só entra na função quando o usuário tiver selecionado peças.
            if (utv.Nodes.Count <= 0)
                return;

            ValueList vl = new ValueList();
            vl.ValueListItems.Add(0, "<Selecione>");

            int contador = 1;

            foreach (UltraTreeNode n in utv.Nodes)
            {
                vl.ValueListItems.Add(contador, n.Cells[(int)e_Colunas.NomeItem].Value.ToString());
                contador++;
            }

            //Define o índice.
            vl.SelectedIndex = 0;
            //Cor de fundo.
            vl.Appearance.BackColor = Color.LightYellow;

            //Adiciona a lista à coluna e define a coluna do tipo dropown.
            udgv_Dados.DisplayLayout.Bands[0].Columns["Peça"].ValueList = vl;
            udgv_Dados.DisplayLayout.Bands[0].Columns["Peça"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

            //  Sempre depois de selecionar um item e adicioná-lo a grade de dados
            //irei setar o indice na dropdownlist.
            SetaIndiceListaPecas();
        }

        /// <summary>
        ///     Método que carrega a lista de peças na dropdown list na grade de dados 'Arquivos'
        /// </summary>
        private void CarregaListaPecas_GridArquivos()
        {
            //Só entra na função quando o usuário tiver selecionado peças.
            if (utv.Nodes.Count <= 0)
                return;

            ValueList vl = new ValueList();
            vl.ValueListItems.Add(0, "<Selecione>");

            int contador = 1;

            foreach (UltraTreeNode n in utv.Nodes)
            {
                vl.ValueListItems.Add(contador, n.Cells[(int)e_Colunas.NomeItem].Value.ToString());
                contador++;
            }

            //Define o índice.
            vl.SelectedIndex = 0;
            //Cor de fundo.
            vl.Appearance.BackColor = Color.LightYellow;

            //Adiciona a lista à coluna e define a coluna do tipo dropown.
            udgv_Arquivos.DisplayLayout.Bands[0].Columns["Peça"].ValueList = vl;
            udgv_Arquivos.DisplayLayout.Bands[0].Columns["Peça"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

            //  Sempre depois de selecionar um item e adicioná-lo a grade de dados
            //irei setar o indice na dropdownlist.
            SetaIndiceListaPecas();
        }

        /// <summary>
        ///     Método responsável por definir o índice igual à zero na lista de nome das peças
        /// na grade de grados 'udgv_Dados' (aba dados auxiliares).
        /// </summary>
        private void SetaIndiceListaPecas()
        {
            if (udgv_Dados.Rows.Count > 0)
            {
                for (int x = 0; x < udgv_Dados.Rows.Count; x++)
                {
                    IValueList vl1 = udgv_Dados.Rows[x].Cells["Peça"].ValueListResolved;

                    //  Se o valor da célula for diferente de null, significa que já foi preenchida previamente,
                    //então, nesse caso, eu desconsidero a mesma.
                    if (udgv_Dados.Rows[x].Cells["Peça"].Value == null)
                        udgv_Dados.Rows[x].Cells["Peça"].Value = vl1.GetValue(0);
                }
            }

            if (udgv_Arquivos.Rows.Count > 0)
            {
                for (int x = 0; x < udgv_Arquivos.Rows.Count; x++)
                {
                    IValueList vl2 = udgv_Arquivos.Rows[x].Cells["Peça"].ValueListResolved;

                    //  Se o valor da célula for diferente de null, significa que já foi preenchida previamente,
                    //então, nesse caso, eu desconsidero a mesma.
                    if (udgv_Arquivos.Rows[x].Cells["Peça"].Value == null)
                        udgv_Arquivos.Rows[x].Cells["Peça"].Value = vl2.GetValue(0);
                }
            }
        }

        /// <summary>
        ///     Método que permite o usuário selecionar um novo arquivo físico do computador
        /// ou da rede e o adiciona na interface para o usuário defini-lo para alguma peça
        /// em específica.
        /// </summary>
        private void CadastraNovoArquivoAuxiliar()
        {
            Utilities util = new Utilities();
            var q = util.BuscaArquivos("Todos os arquivos");

            if (q != null && q.Count > 0)
            {
                Boolean existeAlgumArquivo = false;
                List<String> lstIds = new List<String>();

                for (int x = 0; x < q.Count; x++)
                {
                    //Caso o arquivo já exista, eu apenas desconsidero o arquivo.
                    if ((Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_ARQUIVO_AUXILIAR_BANCO, new Dictionary<string, object>() { { "@ARQ", q[x].ToString() } }).Rows.Count > 0))
                    {
                        existeAlgumArquivo = true;
                        continue;
                    }
                    else
                    {
                        Dictionary<String, object> dic = new Dictionary<string, object>();
                        dic.Add("@PATH", q[x].ToString());
                        dic.Add("@EXT", q[x].ToString().Substring(q[x].LastIndexOf(".") + 1).ToUpper());
                        dic.Add("@USR", Objects.UsuarioAtual.Login);

                        String queryStr = String.Format("{0}\n\n{1}", Consultas_EcMgr.CADASTRA_NOVO_ARQUIVO_AUXILIAR, Consultas_EcMgr.CONSULTA_ULTIMO_ID_ARQUIVO);

                        lstIds.Add(Objects.CnnBancoEcMgr.ExecutaSql(queryStr, dic).AsEnumerable().Select(r => r.ItemArray[0].ToString()).FirstOrDefault());
                    }
                }

                String msg = "Alguns arquivos já foram utilizados previamente em outras ordens de produção e não podem ser inseridos novamente. Favor, Selecione os mesmos clicando sobre o botão adicionar";

                ConsultaArquivosRecemCadastrados(lstIds);

                if (existeAlgumArquivo)
                    MessageBox.Show(msg, "Arquivos já utilizados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else MessageBox.Show("Arquivos importados com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///     Método que consulta o banco de dados intermediário após selecionar arquivos
        /// para vincular com a ordem de produção.
        /// </summary>
        /// <param name="lstIds">Lista de ID's dos arquivos auxiliares.</param>
        private void ConsultaArquivosRecemCadastrados(List<string> lstIds)
        {
            if (lstIds != null && lstIds.Count > 0)
            {
                String ids = lstIds.Select(x => "'" + x.ToString() + "'").Aggregate((oldV, newV) => String.Format("{0}, {1}", oldV, newV));

                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Arquivos.DataSource;

                if (dt == null)
                {
                    udgv_Arquivos.DataSource = SQLQueries.Consulta_ArquivosAuxiliares(ids, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_ArquivosAuxiliares(ids, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Arquivos.DataSource = dt;
                }

                //Chamo esse método para ele re-consultar o nome das peças selecionadas pelo usuário.
                udgv_Arquivos_InitializeLayout(new object(), null);
            }
        }

        /// <summary>
        ///     Abre a interface que contém uma série de dados auxiliares previamente cadastrados.
        /// </summary>
        private void PermiteSelecaoArquivos()
        {
            FrmArquivos_Seleciona frm = new FrmArquivos_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdArqSelecionado))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Arquivos.DataSource;

                if (dt == null)
                {
                    udgv_Arquivos.DataSource = SQLQueries.Consulta_ArquivosAuxiliares(frm._IdArqSelecionado, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_ArquivosAuxiliares(frm._IdArqSelecionado, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Arquivos.DataSource = dt;
                }

                //Chamo esse método para ele re-consultar o nome das peças selecionadas pelo usuário.
                udgv_Arquivos_InitializeLayout(new object(), null);

                //  Sempre depois de selecionar um item e adicioná-lo a grade de dados
                //irei setar o indice na dropdownlist.
                SetaIndiceListaPecas();
            }
        }

        #endregion

        #region Eventos

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Dispose();
            GC.Collect();

            //Remove o controle para permitir o sistema consultar os dados.
            Objects.FechaTelaPendenteInterface(this.GetHashCode().ToString());
            Objects.FormularioPrincipal.Controls.Remove(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaOrdem();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao Criar a ordem de produção", "FrmOrdens_New", "btnSave_Click", "Exceção não tratada em uma tentativa de criar/atualizar uma ordem",
                                           "Consultas_EcMgr.CADASTRA_NOVA_ORDEM_PRODUCAO", e_TipoErroEx.Erro, ex);
            }
            finally { Cursor = Cursors.Arrow; }
        }

        /// <summary>
        ///     Método que, ao clicar sobre a check box, habilita/desabilita os controles
        /// </summary>
        private void cbxCriarNewJob_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxCriarNewJob.Checked)
            {
                label15.Visible         = true;
                txtNewJobName.Visible   = true;
                cbxAddInfo.Visible      = true;

                if (mPodeHabilitarRefAutoJob)
                    btnRefAuto_Job.Visible  = true;

                label27.Visible         = true;
                txtJobComment.Visible   = true;
                label28.Visible         = true;
                cbFamilias.Visible      = true;

                lblAviso.Visible        = true;
            }
            else if (!cbxCriarNewJob.Checked)
            {
                label15.Visible         = false;
                txtNewJobName.Visible   = false;
                cbxAddInfo.Visible      = false;

                if (mPodeHabilitarRefAutoJob)
                    btnRefAuto_Job.Visible = false;

                label27.Visible         = false;
                txtJobComment.Visible   = false;
                label28.Visible         = false;
                cbFamilias.Visible      = false;

                lblAviso.Visible        = false;
            }
        }

        private void btnAddParts_Click(object sender, EventArgs e)
        {
            cms_TipoSelecao.Show(btnAddParts, new Point(0, btnAddParts.Height));
        }

        private void cbxAddInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAddInfo.Checked)
            {
                ultraExpandableGroupBox4.Visible = true;
                ultraExpandableGroupBox5.Visible = true;
                ultraExpandableGroupBox6.Visible = true;
            }
            else if (!cbxAddInfo.Checked)
            {
                ultraExpandableGroupBox4.Visible = false;
                ultraExpandableGroupBox5.Visible = false;
                ultraExpandableGroupBox6.Visible = false;
            }
        }

        private void cbxUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarData.Checked)
                dtEntrega.Enabled = true;
            else if (!cbxUsarData.Checked)
                dtEntrega.Enabled = false;
        }

        private void btnRefAuto_Op_Click(object sender, EventArgs e)
        {
            try
            {
                txtOrdem.Text = Objects.BuscaNovaReferenciaAutomatica("Ordens", "OrdemProducao");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao buscar uma nova referência de ordem de produção", "FrmOrdens_New", "btnRefAuto_Op_Click",
                                           "Exceção não tratada", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnRefAuto_Job_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewJobName.Text = Objects.BuscaNovaReferenciaAutomatica("Ordens", "Trabalho");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao buscar uma nova referência de trabalho", "FrmOrdens_New", "btnRefAuto_Job_Click",
                                           "Exceção não tratada", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que apresenta ao usuário, uma interface para cadastrar um novo dado auxiliar
        /// no banco de dados.
        /// </summary>
        private void btnNewDadoAux_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovoDadoAuxiliar();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar novo dado auxiliar", "FrmOrdens_New", "btnNewDadoAux_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_DADO_AUXILIAR", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que permite o usuário selecionar um dado auxiliar existente.
        /// </summary>
        private void btnAddDadoAux_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_DadoAuxiliar();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar dado auxiliar", "FrmOrdens_New", "btnAddDadoAux_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_DADO_AUXILIAR", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que permite o usuário adicionar um novo arquivo auxiliar à ordem de produção.
        /// </summary>
        private void btnNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovoArquivoAuxiliar();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar novo arquivo", "FrmOrdens_New", "btnNewFile_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que permite o usuário selecionar um arquivo auxiliar existente.
        /// </summary>
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                PermiteSelecaoArquivos();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar arquivo selecionado do banco de dados", "FrmOrdens_New", "btnAddFile_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que carrega a lista de peças da árvore (utv) no grid de dados auxiliares,
        /// onde, após o usuário selecionar os dados auxiliares, ele deverá atribuir à uma peça
        /// esses dados auxiliares.
        /// </summary>
        private void udgv_Dados_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            CarregaListaPecas_GridDados();
        }

        /// <summary>
        ///     Evento que carrega a lista de peças da árvore (utv) no grid de arquivos auxiliares,
        /// onde, após o usuário selecionar os dados auxiliares, ele deverá atribuir à uma peça
        /// esses dados auxiliares.
        /// </summary>
        private void udgv_Arquivos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            CarregaListaPecas_GridArquivos();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            HabilitaControles();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DesabilitaControles();
        }

        private void cbMaquinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaquinas.SelectedIndex > 0)
                groupBox4.Enabled = true;
            else groupBox4.Enabled = false;
        }

        #endregion

        #region Eventos para definir e remover o foco dos controles

        private void txtOrdem_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtOrdem);
        }

        private void txtOrdem_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtOrdem);
        }

        private void cbClientes_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbClientes);
        }

        private void cbClientes_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbClientes);
        }

        private void cbMateriais_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbMateriais);
        }

        private void cbMateriais_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbMateriais);
        }

        private void cbMaquinas_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbMaquinas);
        }

        private void cbMaquinas_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbMaquinas);
        }

        private void cbUsuarios_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbUsuarios);
        }

        private void cbUsuarios_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbUsuarios);
        }

        private void dtEntrega_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(dtEntrega);
        }

        private void dtEntrega_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(dtEntrega);
        }

        private void txtNewJobName_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtNewJobName);
        }

        private void txtNewJobName_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtNewJobName);
        }

        private void txtGeral_Title_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtGeral_Title);
        }

        private void txtGeral_Title_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtGeral_Title);
        }

        private void rtbGeral_Desc_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(rtbGeral_Desc);
        }

        private void rtbGeral_Desc_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(rtbGeral_Desc);
        }

        private void txtBruto_Title_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtBruto_Title);
        }

        private void txtBruto_Title_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtBruto_Title);
        }

        private void rtbBruto_Desc_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(rtbBruto_Desc);
        }

        private void rtbBruto_Desc_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(rtbBruto_Desc);
        }

        private void txtFixacao_Title_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtFixacao_Title);
        }

        private void txtFixacao_Title_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtFixacao_Title);
        }

        private void rtbFixacao_Desc_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(rtbFixacao_Desc);
        }

        private void rtbFixacao_Desc_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(rtbFixacao_Desc);
        }

        #endregion

        #region Eventos para limpar o erro provider clicando no TabPage/TabControl

        /// <summary>
        ///     Evento que limpa o erro provider e remove o destque dos controles (BackColor = Yellow).
        /// </summary>
        private void tabPage1_Click(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(tabPage1, true);
            txtOrdem.BackColor = Color.White;
            cbClientes.BackColor = Color.White;
            cbMateriais.BackColor = Color.White;
            cbMaquinas.BackColor = Color.White;
            cbUsuarios.BackColor = Color.White;
            dtEntrega.BackColor = Color.White;
            txtNewJobName.BackColor = Color.White;
            txtGeral_Title.BackColor = Color.White;
            rtbGeral_Desc.BackColor = Color.White;
            txtBruto_Title.BackColor = Color.White;
            rtbBruto_Desc.BackColor = Color.White;
            txtFixacao_Title.BackColor = Color.White;
            rtbFixacao_Desc.BackColor = Color.White;
        }

        /// <summary>
        ///     Evento que limpa o erro provider e remove o destque dos controles (BackColor = Yellow).
        /// </summary>
        private void tabPage3_Click(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(tabPage3, true);
            txtOrdem.BackColor = Color.White;
            cbClientes.BackColor = Color.White;
            cbMateriais.BackColor = Color.White;
            cbMaquinas.BackColor = Color.White;
            cbUsuarios.BackColor = Color.White;
            dtEntrega.BackColor = Color.White;
            txtNewJobName.BackColor = Color.White;
            txtGeral_Title.BackColor = Color.White;
            rtbGeral_Desc.BackColor = Color.White;
            txtBruto_Title.BackColor = Color.White;
            rtbBruto_Desc.BackColor = Color.White;
            txtFixacao_Title.BackColor = Color.White;
            rtbFixacao_Desc.BackColor = Color.White;
        }

        /// <summary>
        ///     Evento que limpa o erro provider e remove o destque dos controles (BackColor = Yellow).
        /// </summary>
        private void tabPage2_Click(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(tabPage2, true);
            txtOrdem.BackColor = Color.White;
            cbClientes.BackColor = Color.White;
            cbMateriais.BackColor = Color.White;
            cbMaquinas.BackColor = Color.White;
            cbUsuarios.BackColor = Color.White;
            dtEntrega.BackColor = Color.White;
            txtNewJobName.BackColor = Color.White;
            txtGeral_Title.BackColor = Color.White;
            rtbGeral_Desc.BackColor = Color.White;
            txtBruto_Title.BackColor = Color.White;
            rtbBruto_Desc.BackColor = Color.White;
            txtFixacao_Title.BackColor = Color.White;
            rtbFixacao_Desc.BackColor = Color.White;
        }

        #endregion

        #region Eventos com o clique do botão direito sobre o UltraTree

        /// <summary>
        ///     Clique com o botão direito do mouse sobre o UltraTree que ativa a opção
        /// de remover um determinado item da árvore.
        /// </summary>
        private void tsm_removePeca_Click(object sender, EventArgs e)
        {
            try
            {
                RemovePecaSelecionada();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover peças da grade", "FrmOrdens_New", "tsm_removePeca_Click", "", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Clique com o botão direito do mouse sobre o UltraTree que ativa a opção
        /// de remover todos os itens da árvore
        /// </summary>
        private void tsm_removeTodasPecas_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveTodasPecas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover peças da grade", "FrmOrdens_New", "tsm_removeTodasPecas_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos para seleção de peças

        private void tsm_Db_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionaPecasDb();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar as peças para criar uma ordem de produção", "FrmOrdens_New",
                                           "tsm_Db_Click", "Exceção não tratada em uma tentativa de adicionar peças",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm_Pdm_Click(object sender, EventArgs e)
        {

        }

        private void tsm_TeamCenter_Click(object sender, EventArgs e)
        {

        }

        private void tsm_Win_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionaPecasWindows();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar as peças para criar uma ordem de produção", "FrmOrdens_New", "tsm_Db_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos sobre os dados e arquivos auxiliares (grades de dados)

        private void cms_RemoveDados_Opening(object sender, CancelEventArgs e)
        {
            if (udgv_Dados.Rows.Count == 0) tsmRemoveDadosOp.Enabled = false;
            else tsmRemoveDadosOp.Enabled = true;
        }

        private void tsmRemoveDadosOp_Click(object sender, EventArgs e)
        {
            if (udgv_Dados.Selected.Rows.Count > 0)
            {
                udgv_Dados.DeleteSelectedRows(false);
            }
        }

        private void cms_RemoveArquivos_Opening(object sender, CancelEventArgs e)
        {
            if (udgv_Arquivos.Rows.Count == 0) tsmRemoveArquivosOp.Enabled = false;
            else tsmRemoveArquivosOp.Enabled = true;
        }

        private void tsmRemoveArquivosOp_Click(object sender, EventArgs e)
        {
            if (udgv_Arquivos.Selected.Rows.Count > 0)
            {
                udgv_Arquivos.DeleteSelectedRows(false);
            }
        }

        #endregion


    }
}