using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Edgecam_Manager_PackAndGo
{
    public partial class FrmExportacao : Form
    {

        #region Variáveis globais/da classe

        private e_SkaModuloExportar mModulo;
        private SkaUtil mUtil = new SkaUtil();
        private SkaSql mCnnBancoEc;
        private SkaSql mCnnBancoEcMgr;
        private SkaSql mCnnBancoLog;

        #endregion

        #region Enumeradores

        /// <summary>
        ///     Define o tipo de exportação e consulta do sistema.
        /// </summary>
        public enum e_SkaModuloExportar
        {
            /// <summary>
            /// Usuário poderá definir o que será exportado
            /// </summary>
            Usuario,
            /// <summary>
            /// É utilizado para exportar os logs do sistema
            /// </summary>
            Sistema
        }

        /// <summary>
        ///     Enumerador voltado para exceções (exclusivamente)
        /// </summary>
        private enum e_SkaTipoErroEx
        {
            /// <summary>
            ///     Erro não tratado da aplicação.
            /// </summary>
            Erro = 0,
            /// <summary>
            ///     Aviso ao usuário de alguma ação que está tentando executar
            /// </summary>
            Aviso = 1,
            /// <summary>
            ///     Informação de 'algo' que está sendo feito pelo usuário.
            /// </summary>
            Informacao = 2,
            /// <summary>
            ///     Notificação do usuário.
            /// </summary>
            Notificacao = 3
        }

        /// <summary>
        ///     Enumerador que define qual banco de dados será executada determinada
        /// consulta.
        /// </summary>
        private enum e_SkaBancoConsulta
        {
            Manager,
            Edgecam,
            Log
        }

        #endregion

        #region Instância dos objetos da classe

        //  Renomiei os parâmetros dessa forma e os transformei em objetos pelo simples fato de que,
        //os clientes podem tentar utilizar essa biblioteca em algum futuro (dionei 2), dessa forma,
        //apenas eu sei quais parâmetos estou esperando.

        public FrmExportacao(e_SkaModuloExportar ModuloExportar)
        {
            InitializeComponent();
            mModulo = ModuloExportar;
            InicializaValoresDefault();
        }

        public FrmExportacao()
        {
            InitializeComponent();
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que faz a carga dos ícones na interface.
        /// </summary>
        private void InicializaValoresDefault()
        {
            CarregaConfiguracao();

            if (mModulo == e_SkaModuloExportar.Usuario)
            {
                //Inicialização de controles default
                rdExportarTudo.Checked = true;

                //Comandos para exportação (tipo de exportação, período, tudo, etc).
                label1.Visible = false;
                udtInicio.Visible = false;
                label2.Visible = false;
                udtFim.Visible = false;

                //Habilita as opções para compactar tudo em um arquivo.
                rdSalvarArquivo.Checked = true;
                rdExtSkaZip.Checked = true;

                //Desabilita os controles para pesquisa de pasta
                txtPasta.Enabled = false;
                btnPesquisa_Pasta.Enabled = false;

                //Apenas define um caminho pré-definido
                txtPasta.Text = mUtil._DiretorioDesktop;
                txtPastaArquivo.Text = mUtil._DiretorioDesktop;
            }
            else EscondeInterface();
        }

        /// <summary>
        ///     Carrega as configurações do sistema
        /// </summary>
        private void CarregaConfiguracao()
        {
            SkaXmlConfig xml = new SkaXmlConfig("SkaConfig.xml");
            String ecServer = xml.StrXmlSimpleConfigValue("SERVER_EC");
            String ecDb = xml.StrXmlSimpleConfigValue("BANCO_EC");
            String ecUser = xml.StrXmlSimpleConfigValue("USER_EC");
            String ecPass = xml.StrXmlSimpleConfigValue("PASS_EC");

            String auxServer = xml.StrXmlSimpleConfigValue("SERVER_AUX");
            String auxDb = xml.StrXmlSimpleConfigValue("BANCO_AUX");
            String auxUser = xml.StrXmlSimpleConfigValue("USER_AUX");
            String auxPass = xml.StrXmlSimpleConfigValue("PASS_AUX");

            String logServer = xml.StrXmlSimpleConfigValue("SERVER_LOG");
            String logDb = xml.StrXmlSimpleConfigValue("BANCO_LOG");
            String logUser = xml.StrXmlSimpleConfigValue("USER_LOG");
            String logPass = xml.StrXmlSimpleConfigValue("PASS_LOG");

            mCnnBancoEc = new SkaSql(ecServer, ecDb, ecUser, ecPass);
            mCnnBancoEcMgr = new SkaSql(auxServer, auxDb, auxUser, auxPass);
            mCnnBancoLog = new SkaSql(logServer, logDb, logUser, logPass);
        }

        /// <summary>
        ///     Esconde o sistema do usuário.
        /// </summary>
        private void EscondeInterface()
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.Opacity = 0;
            this.Hide();
        }

        /// <summary>
        ///     Método responsável por consultar o banco de dados do Manager e do Edgecam
        /// e as salva em um XML utilizando um método da classe 'DataSet'.
        /// </summary>
        private void ExportaDadosArquivos()
        {
            if (UsuarioSelecionouDiretorio())
            {
                MessageBox.Show("Você precisa obrigatoriamente definir um diretório para exportação dos dados", "Diretório não definido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //As consultas são opcionais
            else if (!UsuarioDefiniuConsultas())
            {
                //Caso não tenha selecionado as consultas, torna-se obrigatório a seleção de anexos.
                if (UsuarioSelecionouAnexo())
                {
                    MessageBox.Show("Você precisa obrigatoriamente, ou selecionar anexos ou definir dados à serem exportados", 
                                    "Dados para exportação não foram defnidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataSet ds = new DataSet();

            if (cbxTarefas.Checked)
            {
                if (rdExportarTudo.Checked)
                    MesclaDadosDataSet(ref ds, "select * from Tarefas", "Tarefas");
                else if (rdExportarUltimo.Checked)
                    MesclaDadosDataSet(ref ds, "select top 1 * from Tarefas order by id desc", "Tarefas");
                else MesclaDadosDataSet(ref ds, String.Format("select * from Tarefas where DtCriacao between '{0}' and '{1}'", udtInicio.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), udtFim.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff")), "Tarefas");
            }

            if (cbxOrdens.Checked)
            {
                if (rdExportarTudo.Checked)
                    MesclaDadosDataSet(ref ds, "select * from Ordens", "Ordens");
                else if (rdExportarUltimo.Checked)
                    MesclaDadosDataSet(ref ds, "select top 1 * from Ordens order by id desc", "Ordens");
                else MesclaDadosDataSet(ref ds, String.Format("select * from Ordens where DtCriacao between '{0}' and '{1}'", udtInicio.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), udtFim.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff")), "Ordens");
            }

            if (cbxOrcamentos.Checked)
            {
                if (rdExportarTudo.Checked)
                    MesclaDadosDataSet(ref ds, "select * from Orcamentos", "Orcamentos");
                else if (rdExportarUltimo.Checked)
                    MesclaDadosDataSet(ref ds, "select top 1 * from Orcamentos order by id desc", "Orcamentos");
                else  MesclaDadosDataSet(ref ds, String.Format("select * from Orcamentos where DtCriacao between '{0}' and '{1}'", udtInicio.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), udtFim.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff")), "Orcamentos");
            }

            if (cbxInventario.Checked)
            {
                if (rdExportarTudo.Checked)
                    MesclaDadosDataSet(ref ds, "select * from InventarioBruto", "InventarioBruto");
                else if (rdExportarUltimo.Checked)
                    MesclaDadosDataSet(ref ds, "select top 1 * from InventarioBruto order by id desc", "InventarioBruto");
                else MesclaDadosDataSet(ref ds, String.Format("select * from InventarioBruto where DtCriacao between '{0}' and '{1}'", udtInicio.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), udtFim.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff")), "InventarioBruto");
            }

            if (cbxTrabalhos.Checked)
            {
                if (rdExportarTudo.Checked)
                    MesclaDadosDataSet(ref ds, "select * from TS_JOB with (nolock);", "TS_JOB", e_SkaBancoConsulta.Edgecam);
                else if (rdExportarUltimo.Checked)
                    MesclaDadosDataSet(ref ds, "select top 1 * from TS_JOB with (nolock) order by JOB_JOB_ID desc", "TS_JOB");
                else MesclaDadosDataSet(ref ds, String.Format("select * from TS_JOB with (nolock) where JOB_CREATED between '{0}' and '{1}'", udtInicio.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff"), udtFim.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff")), "TS_JOB");
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                //Caminho que é criado automaticamente pelo sistema.
                String caminhoAuto = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}.xml", DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss")));
                ds.WriteXml(caminhoAuto);

                CompactaArquivos(caminhoAuto);
            }
            else CompactaArquivos("");
        }

        /// <summary>
        ///     Método que verifica se o usuário definiu um diretório para exportação dos dados.
        /// </summary>
        /// <returns>True determina que o usuário não definiu um diretório, false que está tudo certo.</returns>
        private Boolean UsuarioSelecionouDiretorio()
        {
            int errorsFound = 0;

            if (rdSalvarPasta.Checked)
                errorsFound += String.IsNullOrEmpty(txtPasta.Text) == true ? +1 : 0;
            else if (rdSalvarArquivo.Checked)
                errorsFound += String.IsNullOrEmpty(txtPastaArquivo.Text) == true ? +1 : 0;

            if (errorsFound > 0) return true; else return false;
        }

        /// <summary>
        ///     Método que verifica se o usuário marcou alguma opção de consulta de dados
        /// do banco do manager ou do edgecam.
        /// </summary>
        /// <returns>True para caso onde o usuário selecionou, false ele não selecionou nada</returns>
        private Boolean UsuarioDefiniuConsultas()
        {
            if (cbxTarefas.Checked)
                return true;
            else if (cbxOrdens.Checked)
                return true;
            else if (cbxOrcamentos.Checked)
                return true;
            else if (cbxInventario.Checked)
                return true;
            else if (cbxTrabalhos.Checked)
                return true;
            else return false;
        }

        /// <summary>
        ///     Método que verifica se o usuário adicionou arquivos para anexar.
        /// </summary>
        /// <returns>True para caso onde ele selecionou ao menos um arquivo, false para nenhum</returns>
        private Boolean UsuarioSelecionouAnexo()
        {
            int errorsFound = 0;

            //O usuário precisa obrigatoriamente selecionar 
            errorsFound += lstbxArquivos.Items.Count > 0 ? 0 : +1;

            if (errorsFound > 0) return true; else return false;
        }

        /// <summary>
        ///     Método que executa uma consulta no banco de dados e adiciona os dados (caso existam)
        /// em um data set passado como referência (parâmetro).
        /// </summary>
        /// <param name="DataSetOrigem">Dataset referência que receberá os dados</param>
        /// <param name="Sql">String contendo a consulta ao banco de dados.</param>
        /// <param name="NomeTabela">Nome da tabela a ser adicionada ao dataset</param>
        /// <param name="Banco">Qual banco de dados será efetuado a consulta.</param>
        private void MesclaDadosDataSet(ref DataSet DataSetOrigem, String Sql, String NomeTabela, e_SkaBancoConsulta Banco = e_SkaBancoConsulta.Manager)
        {
            DataTable dt;

            switch (Banco)
            {
                case e_SkaBancoConsulta.Edgecam: dt = mCnnBancoEc.ExecutaSql(Sql); break;
                case e_SkaBancoConsulta.Log: dt = mCnnBancoLog.ExecutaSql(Sql); break;
                case e_SkaBancoConsulta.Manager: dt = mCnnBancoEcMgr.ExecutaSql(Sql); break;
                default: dt = mCnnBancoEcMgr.ExecutaSql(Sql); break;
            }

            dt.TableName = NomeTabela;

            DataSetOrigem.Tables.Add(NomeTabela);
            DataSetOrigem.Tables[NomeTabela].Merge(dt);
        }

        /// <summary>
        ///     Método que move/compacta os arquivos de acordo com a escolha do usuário.
        /// </summary>
        /// <param name="ArquivoConsulta">Caminho do arquivo contendo os dados das consultas aos bancos de dados</param>
        private void CompactaArquivos(String ArquivoConsulta)
        {
            String tmpFolder = Path.GetTempPath();
            List<String> lstNewFilesPath = new List<string>();

            if (rdSalvarPasta.Checked)
            {
                //Se o diretório não existir, eu crio ele.
                if(!Directory.Exists(txtPasta.Text)) Directory.CreateDirectory(txtPasta.Text);

                //Se o usuário adicionou comentários, salvo em um arquivo de texto temporário.
                if (!String.IsNullOrEmpty(rtxtComentarios.Text))
                {
                    String tmpComm = Path.Combine(txtPasta.Text, "Comentarios.txt");

                    if (File.Exists(tmpComm)) File.Delete(tmpComm);

                    File.WriteAllText(tmpComm, String.Format("Comentários do cliente: \n\n{0}", rtxtComentarios.Text));
                }

                //Deve ser verificado sempre, pois esse arquivo não é obrigatório.
                if (File.Exists(ArquivoConsulta))
                {
                    File.Move(ArquivoConsulta, txtPasta.Text);

                    if (lstbxArquivos.Items.Count > 0)
                    {
                        for (int x = 0; x < lstbxArquivos.Items.Count; x++)
                        {
                            if (File.Exists(lstbxArquivos.Items[x].ToString()))
                                File.Copy(lstbxArquivos.Items[x].ToString(), Path.Combine(txtPasta.Text, lstbxArquivos.Items[x].ToString().Substring(lstbxArquivos.Items[x].ToString().LastIndexOf("\\") + 1)));
                        }
                    }

                    DeletaArquivosTemporários(lstNewFilesPath);

                    MessageBox.Show("Arquivos exportados com sucesso", "Êxito ao exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReturn_Click(new object(), new EventArgs());
                }
            }
            //Se não for pra salvar em uma pasta, deve ser compactado.
            else
            {
                //Se o diretório não existir, eu crio ele.
                if (!Directory.Exists(txtPasta.Text)) Directory.CreateDirectory(txtPasta.Text);

                //Instancia o objeto de compactação
                SkaZip zip = new SkaZip(txtPastaArquivo.Text, DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss"), DefineExtensaoZip(), true);

                //Deve ser verificado sempre, pois esse arquivo não é obrigatório.
                if (File.Exists(ArquivoConsulta))
                    zip.AddFile(ArquivoConsulta);

                //Se o usuário adicionou comentários, salvo em um arquivo de texto temporário.
                if (!String.IsNullOrEmpty(rtxtComentarios.Text))
                {
                    String tmpComm = Path.GetTempFileName() + ".txt";
                    File.WriteAllText(tmpComm, String.Format("Comentários do cliente: \n\n{0}", rtxtComentarios.Text));
                    zip.AddFile(tmpComm);

                    lstNewFilesPath.Add(tmpComm);
                }

                if (lstbxArquivos.Items.Count > 0)
                {
                    for (int x = 0; x < lstbxArquivos.Items.Count; x++)
                    {
                        //Verifico se o arquivo ainda existe
                        if (File.Exists(lstbxArquivos.Items[x].ToString()))
                        {
                            //  Caso o arquivo exista, copio ele para um diretório temporário,
                            //pois o diretório original pode conter caracteres especiais.
                            String newTmpFilePath = Path.Combine(Path.GetTempPath(), lstbxArquivos.Items[x].ToString().Substring(lstbxArquivos.Items[x].ToString().LastIndexOf("\\") + 1));

                            //  Só copia o arquivo se for de um diretório difernte do selecionado,
                            //pois o usuário poderá selecionar um arquivo no próprio diretório de exportação.
                            if (lstbxArquivos.Items[x].ToString().ToUpper().Trim() != newTmpFilePath.ToString().ToUpper().Trim())
                            {
                                File.Copy(lstbxArquivos.Items[x].ToString(), newTmpFilePath);

                                zip.AddFile(newTmpFilePath);

                                //Utilizo essa lista para deleter depois os arquivos em cache.
                                lstNewFilesPath.Add(newTmpFilePath);
                            }
                            else
                            {
                                //Se o arquivo estiver no mesmo dirétório de exportação, apenas copio o arquivo
                                //para dentro da pasta zip.
                                zip.AddFile(lstbxArquivos.Items[x].ToString());
                            }
                        }
                    }
                }

                DeletaArquivosTemporários(lstNewFilesPath);

                MessageBox.Show("Arquivos exportados com sucesso", "Êxito ao exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReturn_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Método que deleta os arquivos temporários.
        /// </summary>
        /// <param name="LstArquivosTemporarios">Lista de arquivos Temporários</param>
        private void DeletaArquivosTemporários(List<string> LstArquivosTemporarios)
        {
            if (LstArquivosTemporarios != null && LstArquivosTemporarios.Count > 0)
            {
                for (int x = 0; x < LstArquivosTemporarios.Count; x++)
                {
                    try
                    {
                        File.Delete(LstArquivosTemporarios[x].ToString());
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        ///     Método que identifica qual extensão o usuário definiu para compactação.
        /// </summary>
        /// <returns></returns>
        private SkaZip.ExtensionZip DefineExtensaoZip()
        {
            if (rdExt7Zip.Checked)
                return SkaZip.ExtensionZip.Zip_7;
            else if (rdExtRar.Checked)
                return SkaZip.ExtensionZip.Rar;
            else if (rdExtSkaZip.Checked)
                return SkaZip.ExtensionZip.SkaZip;
            else return SkaZip.ExtensionZip.Zip;
        }

        /// <summary>
        ///     Cadastra um novo log no banco de dados auxiliar (Banco específico para LOGS) apresentando ou
        /// não uma interface para o usuário preencher com alguma informação adicional.
        /// </summary>
        /// <param name="MostrarTela">True para exibir a tela para o usuário adicionar um comentário.</param>
        /// <param name="TituloErro">Título da mensagem que será apresentada ao usuário</param>
        /// <param name="NomeInterface">Nome do Formulário (Form)</param>
        /// <param name="Acao">Qual evento/método que gerou a exceção</param>
        /// <param name="MsgAux">Mensagem auxiliar (descritivo) do motivo que pode ter gerado a exceção (pode passar vazio como parâmetro).</param>
        /// <param name="QuerySql">Nome da consulta SQL realizada (caso houver)</param>
        /// <param name="TipoErro">Tipo de erro que gerou (aviso, informação, erro)</param>
        /// <param name="ex">Exceção gerada pelo sistema.</param>
        private void CadastraNovoLog(Boolean MostrarTela, String TituloErro, String NomeInterface, String Acao, String MsgAux, String QuerySql, e_SkaTipoErroEx TipoErro, Exception ex = null)
        {
            //Dicionário contendo os parâmetros do insert.
            Dictionary<String, object> dic = new Dictionary<string, object>();

            if (MostrarTela)
            {
                FrmErroInesperado frm = new FrmErroInesperado(TituloErro);
                frm.ShowDialog();

                dic.Add("@FORMNAME", NomeInterface);
                dic.Add("@ACAO", Acao);
                dic.Add("@MSG", String.IsNullOrEmpty(MsgAux) == true ? "<None>" : MsgAux);
                dic.Add("@USER", String.Format("{0} (Usuário do windows)", mUtil._NomeUsuarioWindows));
                dic.Add("@DATA", DateTime.Now);
                dic.Add("@TIPOERRO", (int)TipoErro);
                dic.Add("@TITLE", TituloErro);

                if (ex != null)
                {
                    dic.Add("@EXTIPOERRO", ex.GetType().ToString());

                    /*
                     *  Dionei Beilke dos Santos
                     *  21/08/2018, at 12:43 AM
                     *  Adicionei as condições abaixo, pois estava salvando somente o primeiro nível
                     * da exceção. Agora, o sistema trata, gera um arquivo temporário e armazena o log
                     * no banco de dados.
                     */
                    SkaException skaEx = new SkaException(ex, ex.Message, false);
                    String stackStrace = System.IO.File.ReadAllText(skaEx._DiretorioArquivoLog);
                    System.IO.File.Delete(skaEx._DiretorioArquivoLog);

                    dic.Add("@EXSTACKRACE", stackStrace);

                    //Caso ser um erro provido em um banco de dados, pego o número (para abrir uma investigação posteriormente).
                    if (ex.GetType().ToString() == "Oracle.DataAccess.Client.OracleException")
                    {
                        String errorCode = ex.GetType().GetProperty("Number").ToString();

                        dic.Add("@EXERRORCODE", errorCode);
                    }
                    else if (ex.GetType().ToString() == "SqlClient.SqlException")
                    {
                        String errorCode = ex.GetType().GetProperty("Number").ToString();

                        dic.Add("@EXERRORCODE", errorCode);
                    }
                    else
                    {
                        dic.Add("@EXERRORCODE", "<None>");
                    }

                    dic.Add("@COMMENTUSER", frm._ComentarioUsuario);
                    dic.Add("@QUERY", String.IsNullOrEmpty(QuerySql) == true ? "<None>" : QuerySql);
                }
                else
                {
                    dic.Add("@EXTIPOERRO", "");
                    dic.Add("@EXSTACKRACE", "");
                    dic.Add("@EXERRORCODE", "");
                    dic.Add("@COMMENTUSER", frm._ComentarioUsuario);
                    dic.Add("@QUERY", String.IsNullOrEmpty(QuerySql) == true ? "<None>" : QuerySql);
                }
            }
            else
            {
                dic.Add("@FORMNAME", NomeInterface);
                dic.Add("@ACAO", Acao);
                dic.Add("@MSG", MsgAux);
                dic.Add("@USER", String.Format("{0} (Usuário do windows)", mUtil._NomeUsuarioWindows));
                dic.Add("@DATA", DateTime.Now);
                dic.Add("@TIPOERRO", (int)TipoErro);
                dic.Add("@TITLE", TituloErro);

                if (ex != null)
                {
                    dic.Add("@EXTIPOERRO", ex.GetType().ToString());
                    dic.Add("@EXSTACKRACE", ex.StackTrace);

                    //Caso ser um erro provido em um banco de dados, pego o número (para abrir uma investigação posteriormente).
                    if (ex.GetType().ToString() == "Oracle.DataAccess.Client.OracleException")
                    {
                        String errorCode = ex.GetType().GetProperty("Number").ToString();

                        dic.Add("@EXERRORCODE", errorCode);
                    }
                    else if (ex.GetType().ToString() == "SqlClient.SqlException")
                    {
                        String errorCode = ex.GetType().GetProperty("Number").ToString();

                        dic.Add("@EXERRORCODE", errorCode);
                    }
                    else
                    {
                        dic.Add("@EXERRORCODE", "<None>");
                    }

                    dic.Add("@COMMENTUSER", "");
                    dic.Add("@QUERY", String.IsNullOrEmpty(QuerySql) == true ? "<None>" : QuerySql);
                }
                else
                {
                    dic.Add("@EXTIPOERRO", "");
                    dic.Add("@EXSTACKRACE", "");
                    dic.Add("@EXERRORCODE", "");
                    dic.Add("@COMMENTUSER", "");
                    dic.Add("@QUERY", String.IsNullOrEmpty(QuerySql) == true ? "<None>" : QuerySql);
                }
            }
            //Grava a exceção no banco de dados.
            ((SkaSql)mCnnBancoLog).ExecutaSql(Properties.Resources.INSERE_NOVA_EXCECAO, dic);

            if (MostrarTela)
                MessageBox.Show("Obrigado pela sua colaboração em melhorar o sistema", "Relatório recebido com êxito",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Caso habilitado, permite o usuário definir datas para exportação.
        /// </summary>
        private void rdExportarPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExportarPeriodo.Checked)
            {
                label1.Visible = true;
                udtInicio.Visible = true;
                label2.Visible = true;
                udtFim.Visible = true;
            }
            else
            {
                label1.Visible = false;
                udtInicio.Visible = false;
                label2.Visible = false;
                udtFim.Visible = false;
            }
        }

        /// <summary>
        ///     Permite o usuário salvar em uma pasta.
        /// </summary>
        private void rdSalvarPasta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSalvarPasta.Checked)
            {
                txtPasta.Enabled = true;
                btnPesquisa_Pasta.Enabled = true;

                //Desabilita as opções de compactação
                rdExtZip.Enabled = false;
                rdExtRar.Enabled = false;
                rdExt7Zip.Enabled = false;
                rdExtSkaZip.Enabled = false;
            }
            else
            {
                txtPasta.Enabled = false;
                btnPesquisa_Pasta.Enabled = false;

                //Habilita as opções de compactação
                rdExtZip.Enabled = true;
                rdExtRar.Enabled = true;
                rdExt7Zip.Enabled = true;
                rdExtSkaZip.Enabled = true;
            }
        }

        /// <summary>
        ///     Permite o usuário salvar em um arquivo.
        /// </summary>
        private void rdSalvarArquivo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSalvarArquivo.Checked)
            {
                txtPastaArquivo.Enabled = true;
                btnPesquisa_Pasta_Zip.Enabled = true;
            }
            else
            {
                txtPastaArquivo.Enabled = false;
                btnPesquisa_Pasta_Zip.Enabled = false;
            }
        }

        private void btnPesquisa_Pasta_Click(object sender, EventArgs e)
        {
            txtPasta.Text = mUtil.BuscaDiretorio("Diretório para salvamento dos arquivos");
        }

        private void btnPesquisa_Pasta_Zip_Click(object sender, EventArgs e)
        {
            txtPastaArquivo.Text = mUtil.BuscaDiretorio("Diretório para salvamento do arquivo compactado");
        }

        private void btnAnexar_Click(object sender, EventArgs e)
        {
            var files = mUtil.BuscaArquivos("Todos os arquivos");

            if (files != null && files.Count > 0)
            {
                for (int x = 0; x < files.Count; x++)
                {
                    lstbxArquivos.Items.Add(files[x].ToString());
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportaDadosArquivos();
            }
            catch (Exception ex)
            {
                CadastraNovoLog(true, "Erro ao exportar dados para suporte", "FrmExportacao", "btnExportar_Click", "Biblioteca 'Edgecam_Manager_PackAndGo", "", e_SkaTipoErroEx.Erro, ex);
                btnReturn_Click(new object(), new EventArgs());//Fecho a interface, pois se gerou algum erro, não adianta persistir.
            }
        }

        #endregion

        #region Eventos com o botão direito sobre a list box

        private void cms_Opening(object sender, CancelEventArgs e)
        {
            if (lstbxArquivos.Items.Count > 0)
                tsmRemoverArquivos.Enabled = true;
            else tsmRemoverArquivos.Enabled = false;

        }

        private void tsmRemoverArquivos_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < lstbxArquivos.SelectedItems.Count; x++)
            {
                lstbxArquivos.Items.RemoveAt(lstbxArquivos.SelectedIndices[x]);
                x--;
            }
        }

        #endregion

    }
}