using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Edgecam_Manager;
using System.Data;
using System.Drawing;

/// <summary>
///     Classe que possui objetos estáticos para poupar N consultas dentro dos bancos de dados,
/// dessa maneira agilizando a busca dos valores.
/// </summary>
/// <remarks>Não modificar esses objetos, pois eles são utilizados em N locais.</remarks>
internal class Objects
{
    #region Objetos estáticos

    /// <summary>
    ///     Contém o objeto que representa o usuário atualmente logado.
    /// </summary>
    public static Usuario UsuarioAtual;

    /// <summary>
    ///     Contém o objeto que representa todas as mensagens do sistema em uma 
    /// única classe.
    /// </summary>
    public static Messages MensagensSistema;

    /// <summary>
    ///     Contém um objeto que possuí informações sobre a versão (informação
    /// armazenada no banco de dados).
    /// </summary>
    public static Versao Versao;

    /// <summary>
    ///     Error provider que apresenta fundo amarelo nas caixas em que o usuário
    /// clica/seleciona para preenchimento.
    /// </summary>
    public static ErrorProvider Alerta = new ErrorProvider();

    /// <summary>
    ///     Objeto que dá acesso a licença.
    /// </summary>
    public static Edgecam_Manager_License.LicenseInfo License;

    /// <summary>
    ///     Represents a list with all filters.
    /// </summary>
    public static List<Filter> LstFilters;

    /// <summary>
    ///     Represents a list with all reports.
    /// </summary>
    public static List<Relatorio> LstReports;

    #endregion

    #region Listas estáticas

    /// <summary>
    ///     Contém um objeto contendo a configuração atual do sistema.
    /// </summary>
    public static Config CfgAtual;

    /// <summary>
    ///     Lista de string contendo todos os usuários do banco SQL.
    /// </summary>
    public static List<String> LstUsuarios;

    /// <summary>
    ///     Lista das máquinas (centros de trabalhos) cadastrados no banco de dados
    /// intermediário.
    /// </summary>
    public static ListaMachines LstMaquinas;

    /// <summary>
    ///     Representa uma lista de materiais cadastrados no edgecam.
    /// </summary>
    public static ListaMaterais LstMateriais;

    /// <summary>
    ///     Representa uma lista de clientes cadastrados no edgecam manager.
    /// </summary>
    public static ListaClientes LstClientes;

    /// <summary>
    ///     Representa uma lista de famílias cadastradas nos trabalhos do Edgecam.
    /// </summary>
    public static ListaFamilia LstFamilias;

    /// <summary>
    ///     Representa uma lista de métodos de pagamento para orçamentos, cadastrados no MGR.
    /// </summary>
    public static ListaMetodosPay LstPagamentos;

    /// <summary>
    ///     Representa uma lista de tipos de orçamentos cadastrados previamente pelos usuários.
    /// </summary>
    public static ListQuotesType LstTiposOrcamentos;

    /// <summary>
    ///     Representa uma lista de unidades organizacionais, cadastradas no MGR.
    /// </summary>
    public static ListaUnidades LstUnidOrg;
    
    /// <summary>
    ///     Contém uma lista dos controles não salvos.
    /// </summary>
    public static List<Form> LstItensNaoSalvos = new List<Form>();

    #endregion

    #region Controles estáticos

    /// <summary>
    ///     Contém o objeto que dá acesso ao formulário main antigo.
    /// </summary>
    //public static FrmMain FormularioPrincipal;

    /// <summary>
    ///     Contém o objeto que dá acesso ao formulário main novo (repaginado).
    /// </summary>
    public static FrmMain2 FormularioPrincipal;

    /// <summary>
    ///     Contém o objeto que da acesso ao formulário de crypto wallet market.
    /// </summary>
    public static FrmOrcamentos_Criptomoedas Formulario_Criptomoedas;

    /// <summary>
    ///     Contém o objeto que dá acesso ao formulário do syneco.
    /// </summary>
    public static FrmSyneco Formulario_Syneco;

    #endregion

    #region Conexões SQL estáticas

    /// <summary>
    ///     Contém a conexão com o banco de dados SQL do Edgecam.
    /// </summary>
    public static Sql CnnBancoEc;

    /// <summary>
    ///     Contém a conexão com o banco de dados SQL do Edgecam Manager.
    /// </summary>
    public static Sql CnnBancoEcMgr;

    /// <summary>
    ///     Contém a conexão com o banco de dados SQL dos logs da aplicação.
    /// </summary>
    public static Sql CnnBancoLog;

    #endregion

    #region Métodos estáticos privados

    /// <summary>
    ///     Método responsável por calcular uma nova String automática com base
    /// na configuração que o cliente impos dentro do sistema.
    /// </summary>
    /// <param name="Dt">DataTable contendo as informações das referências autoáticas.</param>
    /// <returns>String contendo uma nova referência.</returns>
    private static String CalculaNovaReferencia(DataTable Dt)
    {
        if (Dt.Rows.Count > 0)
        {
            int contador = Convert.ToInt32(Dt.Rows[0]["ContadorAtual"].ToString());
            string prefixo = Dt.Rows[0]["Prefixo"].ToString();
            Boolean inserirZeros = Convert.ToBoolean(Dt.Rows[0]["InserirZeros"].ToString());
            Boolean zerosEsquerda = Convert.ToBoolean(Dt.Rows[0]["ZerosAesquerda"].ToString());
            Boolean zerosDireita = Convert.ToBoolean(Dt.Rows[0]["ZerosAdireita"].ToString());
            int numZeros = Convert.ToInt32(Dt.Rows[0]["NumZerosInserir"].ToString());

            //Se não tiver configurado para inserir zeros, apenas retorno o prefixo + o valor do contador atual.
            if (!inserirZeros)
            {
                return prefixo + contador.ToString();
            }
            else if (inserirZeros && numZeros > 0)
            {
                if (zerosEsquerda) return String.Format("{0}{1}", prefixo, contador.ToString().PadLeft(numZeros, '0'));
                if (zerosDireita) return String.Format("{0}{1}", prefixo, contador.ToString().PadRight(numZeros, '0'));
            }
        }

        return "";
    }

    #endregion

    #region Métodos estáticos públicos - Uso genérico

    /// <summary>
    ///     Load user list from database and populated the list 'Objects.LstUsuarios'.
    /// </summary>
    public static void LoadUsersList()
    {
        //Reinicia a lista.
        LstUsuarios = new List<string>();

        LstUsuarios = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_LISTA_USUARIOS).AsEnumerable().Select(x =>
                      (new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A)).DecryptString(x["Login"].ToString()).ToUpper()).OrderBy(y => y.ToString()).ToList();
    }

    public static void LoadConfigAPI(String ApiUserName, String ApiUserPass)
    {
        if (ApiUserName.ToUpper().Trim() == "X154812A85SD4DSDS5A1A1S8A" && ApiUserPass.ToUpper().Trim() == "S31X8A8E12385532SDI;/SP43WED")
        {
            CustomXmlConfig xml = new CustomXmlConfig("SkaConfig.xml");

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

            System.Globalization.CultureInfo mIdioma = new System.Globalization.CultureInfo(xml.StrXmlSimpleConfigValue("IDIOMA"));
            String ColorTheme = xml.StrXmlSimpleConfigValue("THEME");

            Boolean mManterUserConectado = Convert.ToBoolean(xml.StrXmlSimpleConfigValue("MANTER_CONECTADO"));

            String EcStrCnn = Sql.CriaStringConexao(ecServer, ecDb, ecUser, ecPass);
            String AuxStrCnn = Sql.CriaStringConexao(auxServer, auxDb, auxUser, auxPass);
            String LogStrCnn = Sql.CriaStringConexao(logServer, logDb, logUser, logPass);

            //Cria um objeto estático, de maneira que possa 'reutilizar' os objetos em toda a interface.
            Objects.CfgAtual = new Config(ecServer, ecDb, ecUser, ecPass, EcStrCnn, auxServer, auxDb, auxUser, auxPass, AuxStrCnn, mIdioma, ColorTheme);

            //Cria os objetos estáticos para cada banco de dados.
            Objects.CnnBancoEc = new Sql(EcStrCnn);
            Objects.CnnBancoEcMgr = new Sql(AuxStrCnn);
            Objects.CnnBancoLog = new Sql(LogStrCnn);

            Objects.UsuarioAtual = new Usuario("X154812A85SD4DSDS5A1A1S8A", "S31X8A8E12385532SDI;/SP43WED");
        }
        else throw new ArgumentOutOfRangeException("User and password from API user is not correct");
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
    public static void CadastraNovoLog(Boolean MostrarTela, String TituloErro, String NomeInterface, String Acao, String MsgAux, String QuerySql, e_TipoErroEx TipoErro, Exception ex = null)
    {
        //Dicionário contendo os parâmetros do insert.
        Dictionary<String, object> dic = new Dictionary<string, object>();

        if (MostrarTela)
        {
            FrmErroInesperado frm = new FrmErroInesperado(TituloErro);
            frm.ShowDialog();

            dic.Add("@APP", Application.ProductName);
            dic.Add("@APPVER", Application.ProductVersion);
            dic.Add("@FORMNAME", NomeInterface);
            dic.Add("@ACAO", Acao);
            dic.Add("@MSG", String.IsNullOrEmpty(MsgAux) == true ? "<None>" : MsgAux);
            dic.Add("@USER", Objects.UsuarioAtual != null ? Objects.UsuarioAtual.Login : Environment.UserName);
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
                CustomException skaEx = new CustomException(ex, ex.Message, false);
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
            dic.Add("@APP", Application.ProductName);
            dic.Add("@APPVER", Application.ProductVersion);
            dic.Add("@FORMNAME", NomeInterface);
            dic.Add("@ACAO", Acao);
            dic.Add("@MSG", MsgAux);
            dic.Add("@USER", Objects.UsuarioAtual.Login != "" ? Objects.UsuarioAtual.Login : "");
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
        Objects.CnnBancoLog.ExecutaSql(Consultas_Log.INSERE_NOVA_EXCECAO, dic);

        if(MostrarTela)
            System.Windows.Forms.MessageBox.Show("Obrigado pela sua colaboração em melhorar o sistema", "Relatório recebido com êxito", 
                                             System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Implementa/adiciona um novo fomulário na variável 'FormularioPrincipal', que contém
    /// um objeto do form 'FrmMain'.
    /// </summary>
    /// <param name="Frm">Formulário já instânciado</param>
    /// <param name="AddLstNaoSalvos">True para que esse formulário seja adicionado a 
    /// lista de itens não salvos</param>
    public static void ImplementaNovoFormTela(Form Frm, Boolean AddLstNaoSalvos = false)
    {
        Frm.MdiParent = FormularioPrincipal.ParentForm;

        Frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Frm.TopLevel = false;
        Frm.ControlBox = false;
        Frm.MaximizeBox = false;
        Frm.MinimizeBox = false;
        Frm.ShowIcon = false;
        Frm.Dock = System.Windows.Forms.DockStyle.Fill;
        Frm.KeyPreview = true;

        FormularioPrincipal.Controls.Add(Frm);

        if (AddLstNaoSalvos)
        {
            Frm.Text += String.Format("- {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            LstItensNaoSalvos.Add(Frm);
        }

        Frm.Show();
        Frm.BringToFront();
    }

    /// <summary>
    ///     Método que abre o form para seleção das colunas.
    /// </summary>
    /// <param name="UltraGrid">UltraDataGridView que terá suas colunas apresentadas para o usuário.</param>
    public static void EscolheColunasGrid(Infragistics.Win.UltraWinGrid.UltraGrid UltraGrid)
    {
        FrmSelColunasGrid frm = new FrmSelColunasGrid(UltraGrid);
        frm.ShowDialog();

        UltraGrid = frm._Grid;
    }

    /// <summary>
    ///     Método responsável por limpar a ordenação das colunas de todos os UltraGridsView
    /// </summary>
    /// <param name="UltraGrid">UltraDataGridView que terá suas colunas desordenadas.</param>
    public static void LimpaOrdenacaoColunasGrid(Infragistics.Win.UltraWinGrid.UltraGrid UltraGrid)
    {
        UltraGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
        UltraGrid.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
    }

    /// <summary>
    ///     Método que seta a seleção da tupla recém adicionada (último registro da grade de dados).
    /// Esse método deve ser chamado somente quando for criar novos resgistros.
    /// </summary>
    /// <param name="UltraGrid"></param>
    public static void SetaUltimaTuplaSelecionada(Infragistics.Win.UltraWinGrid.UltraGrid UltraGrid)
    {
        if (UltraGrid.Rows.Count > 0)
        {
            UltraGrid.Rows[UltraGrid.Rows.Count - 1].Activate();
        }
    }

    /// <summary>
    ///     Método que troca a cor do theme da interface e seus respectivos controles (GroupBox,
    /// ToolStrip, MenuStrip, Panel).
    /// </summary>
    /// <param name="Frm">Objeto contendo o formulário de terá seu tema alterado.</param>
    public static void DefineColorThemeInterface(System.Windows.Forms.Form Frm)
    {
        System.Drawing.Color corFundoControles = CfgAtual._Theme.ToUpper().Trim() == "DARK" ? System.Drawing.SystemColors.GrayText : System.Drawing.SystemColors.Control;
        System.Drawing.Color corFundoForms = CfgAtual._Theme.ToUpper().Trim() == "DARK" ? System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38))))) : System.Drawing.SystemColors.ControlLightLight;

        if (Frm != null)
        {
            Frm.BackColor = corFundoForms;

            foreach (Control c in Frm.Controls)
            {
                if (c is GroupBox) c.BackColor = corFundoControles;
                else if (c is ToolStrip) c.BackColor = corFundoControles;
                else if (c is MenuStrip) c.BackColor = corFundoControles;
                else if (c is Panel)
                {
                    c.BackColor = corFundoControles;

                    //  Caso for um panel, ele pode conter filhos, e estes (groupboxes),
                    //eu não posso modificar a cor de fundo deles.
                    if (c.Controls.Count > 0)
                    {
                        foreach (Control cc in c.Controls)
                            if (cc is GroupBox) cc.BackColor = System.Drawing.SystemColors.Control;//Cor fixa, tudo preto fica 'estranho' e 'irritante'
                    }
                }

                //Caso for um controle do tipo 'SplitContainer', poderá ter filhos
                else if (c is SplitContainer)
                {
                    //Troca a cor de fundo do controle 'SplitContainer'
                    c.BackColor = corFundoForms;

                    //SplitContainer.Panel1.Controls
                    for (int x = 0; x < c.Controls[0].Controls.Count; x++)
                    {
                        Control cc = c.Controls[0].Controls[x];//Aqui ele tem o primeiro panel.

                        if (cc is Panel)
                        {
                            cc.BackColor = corFundoControles;

                            //Esse loop serve para os subpanels.
                            foreach (Control ccc in cc.Controls)
                                if (ccc is GroupBox) ccc.BackColor = System.Drawing.SystemColors.Control;//Cor fixa, tudo preto fica 'estranho' e 'irritante'

                            //Para o LOOP, pois troco a cor somente do primeiro PANEL (pai dos demais controles)
                            break;
                        }
                    }

                    //SplitContainer.Panel2.Controls
                    foreach (Control cc in c.Controls[1].Controls)
                    {
                        if (cc is GroupBox || cc is Panel)
                            cc.BackColor = corFundoControles;
                    }
                }
                //Se for tab control, ele pode conter Abas, para isso, criei o loop abaixo.
                else if (c is TabControl)
                {
                    foreach (Control cc in c.Controls)
                        if (cc is TabPage) cc.BackColor = corFundoControles; ;
                }
            }
        }
    }

    /// <summary>
    ///     Método que busca a versão do banco de dados.
    /// </summary>
    public static void BuscaVersaoBanco()
    {
        if (Versao == null)
        {
            System.Data.DataTable dt = CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_VERSAO_DATABASE);

            Versao = new Versao()
            {
                VersaoSistema = dt.Rows[0]["VersaoSistema"].ToString(),
                VersaoDatabase = dt.Rows[0]["VersaoDatabase"].ToString(),
                ServicePackSistema = dt.Rows[0]["ServicePackSistema"].ToString(),
                Cliente = dt.Rows[0]["Cliente"].ToString(),
                DataUltAtualizacao = dt.Rows[0]["DtUltAtualizacao"].ToString()

            };
        }
    }

    /// <summary>
    ///     Método que limpa a seleção de todos os controles da interface (background color = yellow e o errorProvider)
    /// e seta a seleção em um único controle na interface, destacando para o usuário.
    /// </summary>
    /// <param name="Ctrl">Controle que deverá ser setado como conrtole ativo.</param>
    public static void SetaSelecaoCaixa(Control Ctrl)
    {
        Alerta.Clear();
        //Alerta.Icon = "";
        //Ctrl.BackColor = System.Drawing.Color.Yellow;
        Ctrl.BackColor = Color.FromArgb((int)byte.MaxValue, 180, 180);

        Alerta.SetError(Ctrl, "Campo vazio ou não definido");
    }

    /// <summary>
    ///     Método que limpa a seleção de todos os controles da interface (background color = white e o errorProvider.Clear()).
    /// </summary>
    /// <param name="Ctrl">Controle que deverá perder o foco do alerta.</param>
    /// <param name="LimparApenasErrorProvider">True para apenas limpar o error provider (não trocar a cor de fundo da interface)</param>
    public static void RemoveSelecaoCaixa(Control Ctrl, Boolean LimparApenasErrorProvider = false)
    {
        if (LimparApenasErrorProvider)
        {
            Alerta.Clear();
        }
        else
        {
            Alerta.Clear();
            Ctrl.BackColor = System.Drawing.Color.White;
        }
    }

    /// <summary>
    ///     Método que consulta o banco de dados e obtém uma nova referência (novo valor do contador)
    /// para os parâmetros informados.
    /// </summary>
    /// <param name="Tabela">Nome da tabela a ser obtida a referência automática</param>
    /// <param name="Coluna">Nome da coluna da tabela.</param>
    /// <returns>String contendo o novo valor da referência automática já calculada</returns>
    public static String BuscaNovaReferenciaAutomatica(String Tabela, String Coluna)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("@TBL", Tabela);
        dic.Add("@CLM", Coluna);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.BUSCA_NOVO_VALOR_CONTADOR_REFERENCIA_AUTOMATICA, dic);

        String novoValorContador = dt.Rows[0]["ContadorAtual"].ToString();

        if (!String.IsNullOrEmpty(novoValorContador))
        {
            dic.Add("@NEWVALUE", novoValorContador);
            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_VALOR_CONTADOR_REFERENCIA_AUTOMATICA, dic);

            return CalculaNovaReferencia(dt);
        }
        else return "";
    }

    /// <summary>
    ///     Método que verifica se exista uma referência automática configurada no sistema.
    /// </summary>
    /// <param name="Tabela">Nome da tabela do banco de dados</param>
    /// <param name="Coluna">Nome da coluna</param>
    /// <returns>True para caso ela exista, false para o contrário.</returns>
    public static Boolean ExisteReferenciaAutomatica(String Tabela, String Coluna)
    {
        try
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@TBL", Tabela);
            dic.Add("@CLM", Coluna);

            //Verifica se existe 'ref auto para OP'
            if (Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_REFERENCIA_AUTOMATICA, dic).Rows.Count > 0)
                return true;
            else return false;
        }
        catch { return false; }
    }

    /// <summary>
    ///     Método responsável por verificar se um determinado valor existe já no banco de dados.
    /// </summary>
    /// <param name="Tabela">Nome da tabela</param>
    /// <param name="Coluna">Nome da coluna a ser validada</param>
    /// <param name="Valor">Valor em forma de texto</param>
    /// <returns>True caso já existe, false para o contrário.</returns>
    public static Boolean ExisteValorBanco(String Tabela, String Coluna, String Valor)
    {
        try
        {
            DataTable dt = CnnBancoEcMgr.ExecutaSql($"select 1 from {Tabela} where {Coluna} like '%{Valor}%'");

            return dt.Rows.Count > 0 ? true : false;
        }
        catch { return false; }
    }

    public static void AddHistoricoGrid(Infragistics.Win.UltraWinGrid.UltraGrid Udgv, String Mensagem)
    {
        List<String> lstColumns = Udgv.DisplayLayout.Bands[0].Columns.All.AsEnumerable().Select(c => c.ToString()).ToArray().ToList();

        DataTable dt = new DataTable();

        foreach(String c in lstColumns)
        {
            dt.Columns.Add(new DataColumn(c.ToString(), c.Contains("Data") ? typeof(System.DateTime) : typeof(System.String)));
        }

        //Histórico de orçamentos contém a coluna versão.
        if (lstColumns.Contains("Versão")) dt.Columns.Add(new DataColumn("Versão", typeof(String)));

        //dt.Rows.Add()
    }

    #endregion

    #region Itens não salvos

    /// <summary>
    ///     Método responsável por atualizar a lista dos itens não salvos
    /// (SkaObjeto.LstItensNaoSalvos).
    /// </summary>
    /// <param name="Udgv">UltraGrid que irá receber a lista de itens não salvos.</param>
    public static void AtualizaListaItensNaoSalvos(ref Infragistics.Win.UltraWinGrid.UltraGrid Udgv)
    {
        if (LstItensNaoSalvos != null && LstItensNaoSalvos.Count > 0)
        {
            Udgv.Visible = true;

            //DataTable temporário
            DataTable dt = new DataTable();

            //Colunas
            dt.Columns.Add("Selecionar", typeof(string));
            dt.Columns.Add("Item não salvo", typeof(string));
            dt.Columns.Add("HashCode", typeof(string));

            for (int x = 0; x < LstItensNaoSalvos.Count; x++)
            {
                //Nome do arquivo, Custo, Quantidade, CaminhoCompleto
                dt.Rows.Add("Continuar", LstItensNaoSalvos[x].Text, LstItensNaoSalvos[x].GetHashCode().ToString());
            }

            Udgv.DataSource = dt;
        }
        else Udgv.Visible = false;
    }

    /// <summary>
    ///     Método que traz para frente uma interface não salva com base
    /// no seu id (HashCode).
    /// </summary>
    /// <param name="HashCode">Id da interface (nunca irá se repetir)</param>
    public static void ChamaTelaPendenteInterface(String HashCode)
    {
        if (LstItensNaoSalvos != null && LstItensNaoSalvos.Count > 0)
        {
            for (int x = 0; x < LstItensNaoSalvos.Count; x++)
            {
                if (HashCode.ToUpper().Trim() == LstItensNaoSalvos[x].GetHashCode().ToString().ToUpper().Trim())
                {
                    LstItensNaoSalvos[x].BringToFront();
                }
            }
        }
    }

    /// <summary>
    ///     Método que remove uma interface de item não salvo (independente de qual seja).
    /// O sistema remove da lista de itens não salvos apenas.
    /// </summary>
    /// <param name="HashCode">Id da interface (nunca irá se repetir)</param>
    public static void FechaTelaPendenteInterface(String HashCode)
    {
        LstItensNaoSalvos.Remove(LstItensNaoSalvos.Where(x => x.GetHashCode().ToString().ToUpper().Trim() == HashCode.ToUpper().Trim()).FirstOrDefault());
    }

    #endregion

    #region Filters

    /// <summary>
    ///     Load all filters from database
    /// </summary>
    public static void LoadUsersFilter()
    {
        DataTable dt = CnnBancoEcMgr.ExecutaSql("Select * from Filtros");

        if (dt != null && dt.Rows.Count > 0)
        {
            LstFilters = new List<Filter>();
            LstFilters = dt.AsEnumerable().Select(r => new Filter()
            {
                Id          = Convert.ToInt16(r["id"].ToString()),
                Interface   = r["Interface"].ToString(),
                Modulo      = r["Modulo"].ToString(),
                Usuario     = r["Usuario"].ToString(),
                IdUsuario   = Convert.ToInt16(r["IdUsuario"].ToString()),
                NomeFiltro  = r["NomeFiltro"].ToString(),
                IsPrivate   = Convert.ToBoolean(r["Private"].ToString()),
                Fields      = r["Filters"].ToString(),
                Ativo       = Convert.ToBoolean(r["Ativo"]),
                DtCriacao   = Convert.ToDateTime(r["DtCriacao"].ToString()),
                DtUltMod    = Convert.ToDateTime(r["DtUltModificacao"].ToString())
            }).ToList();
        }
    }

    /// <summary>
    ///     Create a XML structure with the fields and they values to save inside the database.
    /// </summary>
    /// <param name="GroupBox">Group box that contains the fields to save</param>
    /// <returns>String with XML structure with the fields and values.</returns>
    public static String CreateFilterFromControls(Infragistics.Win.Misc.UltraGroupBox GroupBox)
    {
        String ret = "";
        try
        {
            if (GroupBox != null && GroupBox.Controls.Count > 0)
            {
                foreach (Control c in GroupBox.Controls)
                {
                    switch (c.GetType().ToString().Substring(c.GetType().ToString().LastIndexOf(".") + 1).ToUpper())
                    {
                        case "TEXTBOX":             ret += $"<{c.Name}>{c.Text}</{c.Name}>\n"; break;
                        case "IMAGEDCOMBOBOX":      ret += $"<{c.Name}>{((ImagedComboBox.ImagedComboBox)c).SelectedIndex}</{c.Name}>\n"; break;
                        case "CHECKBOX":            ret += $"<{c.Name}>{((CheckBox)c).Checked.ToString()}</{c.Name}>\n"; break;
                        case "ULTRADATETIMEEDITOR": ret += $"<{c.Name}>{(((Infragistics.Win.UltraWinEditors.UltraDateTimeEditor)c).Enabled == true ? ((Infragistics.Win.UltraWinEditors.UltraDateTimeEditor)c).DateTime.ToString("yyyy-MM-dd") : "")}</{c.Name}>\n"; break;
                        default: break;
                    }
                } 
                return ret;
            } else return ret;
        }
        catch (Exception) { return String.Empty; }
    }

    /// <summary>
    ///     Load all filters name according the interface (form) and the user configurations
    /// (private or public filter)
    /// </summary>
    /// <param name="ComboList">ComboBox list to added itens.</param>
    /// <param name="Interface"></param>
    /// <param name="Module"></param>
    public static void LoadFilterNameListInComboBox(ref Infragistics.Win.UltraWinEditors.UltraComboEditor ComboList, String Interface, String Module)
    {
        if(ComboList != null)
        {
            //Clear the list
            ComboList.Items.Clear();
            ComboList.Items.Add("<Selecione>");

            //Recarrega os filtros (outros usuários podem ter criados filtros nesse meio tempo).
            Objects.LoadUsersFilter();

            //Get only the filters from the specified interface.
            foreach (Filter f in LstFilters.Where(x => x.Interface.ToUpper().Trim() == Interface.ToUpper().Trim() && x.Modulo.ToUpper().Trim() == Module.ToUpper().Trim()))
            {
                //Name that will show to the user
                //'!f.IsPrivate' means that this filter/report is public, all users can see/view
                if (!f.IsPrivate) ComboList.Items.Add($"[{f.Usuario}] - {f.NomeFiltro}");
                else ComboList.Items.Add($"{f.NomeFiltro}");
            }
        }
    }

    /// <summary>
    ///     Load a specific filter in the interface (each field with a specific value).
    /// </summary>
    /// <param name="GroupBox">Group box that will filled with a value</param>
    /// <param name="NameFilter">Filter name</param>
    public static void LoadFilterValuesInControls(Infragistics.Win.Misc.UltraGroupBox GroupBox, String NameFilter)
    {
        if (GroupBox != null && GroupBox.Controls.Count > 0 && NameFilter.ToUpper().Trim() != "<SELECIONE>" && !String.IsNullOrEmpty(NameFilter))
        {
            Filter f = null;

            //I did this logic because the public filters contains this special characters, but private doesn't.
            if (NameFilter.Contains("-") || NameFilter.Contains("[") || NameFilter.Contains("]"))
                f = LstFilters.Where(x => x.NomeFiltro.ToUpper().Trim() == NameFilter.Split(new char[] { '-' })[1].ToUpper().Trim()).FirstOrDefault();
            else f = LstFilters.Where(x => x.NomeFiltro.ToUpper().Trim() == NameFilter.ToUpper().Trim()).FirstOrDefault();

            if (f != null)
            {
                //this represents each filter
                foreach (String filter in f.Fields.Split(new char[] { '\n' }))
                {
                    //Por causa da quebra de linhas (linha acima), a última linha está vazia. Isso impede o sistema de gerar erros.
                    if (String.IsNullOrEmpty(filter)) continue;

                    String fieldName = filter.Split(new char[] { '>' })[0].Replace("<", "");
                    String fieldValue = filter.Split(new char[] { '>', '<' })[2];

                    switch (GroupBox.Controls[fieldName].GetType().ToString().Substring(GroupBox.Controls[fieldName].GetType().ToString().LastIndexOf(".") + 1).ToUpper())
                    {
                        case "TEXTBOX":
                            GroupBox.Controls[fieldName].Text = fieldValue;
                            break;

                        case "IMAGEDCOMBOBOX":
                            ((ImagedComboBox.ImagedComboBox)GroupBox.Controls[fieldName]).SelectedIndex = Convert.ToInt16(String.IsNullOrEmpty(fieldValue) ? "0" : fieldValue);
                            break;

                        case "CHECKBOX":
                            ((CheckBox)GroupBox.Controls[fieldName]).Checked = Convert.ToBoolean(fieldValue.ToString());
                            break;

                        case "ULTRADATETIMEEDITOR":
                            ((Infragistics.Win.UltraWinEditors.UltraDateTimeEditor)GroupBox.Controls[fieldName]).DateTime = Convert.ToDateTime(String.IsNullOrEmpty(fieldValue) ? DateTime.Now.ToString() : fieldValue);
                            break;

                        default: break;
                    }
                }
            }
        }
    }

    #endregion

    #region Reports

    /// <summary>
    ///     Load all filters from database
    /// </summary>
    public static void LoadReports()
    {
        DataTable dt = CnnBancoEcMgr.ExecutaSql("Select * from Relatorios");

        if (dt != null && dt.Rows.Count > 0)
        {
            LstReports = new List<Relatorio>();
            LstReports = dt.AsEnumerable().Select(r => new Relatorio()
            {
                Id = Convert.ToInt16(r["id"].ToString()),
                NomeRelatorio = r["NomeRelatorio"].ToString(),
                Descricao = r["Descricao"].ToString(),
                Interface = r["Interface"].ToString(),
                Modulo = r["Modulo"].ToString(),
                ConteudoRelatorio = r["ConteudoRelatorio"].ToString(),
                IsPrivate = Convert.ToBoolean(r["Private"].ToString()),
                Idioma = r["Idioma"].ToString(),
                SQL = r["SQL"].ToString(),
                ViewOneRecord = Convert.ToBoolean(r["ViewOneRecord"].ToString()),
                Versao = r["Versao"].ToString(),
                Ativo = Convert.ToBoolean(r["Ativo"]),
                UsrCrt = r["UsuarioCriacao"].ToString(),
                DtCrt = Convert.ToDateTime(r["DtCriacao"].ToString()),
                UsrUltMod = r["UsuarioUltMod"].ToString(),
                DtUltMod = Convert.ToDateTime(r["DtUltimaMod"].ToString())
            }).ToList();
        }
    }

    public static void LoadReportNameListInComboBox(ref Infragistics.Win.UltraWinEditors.UltraComboEditor ComboList, String Interface, String Module)
    {
        if (ComboList != null)
        {
            //Clear the list
            ComboList.Items.Clear();
            ComboList.Items.Add("<Selecione>");

            //Recarrega os filtros (outros usuários podem ter criados filtros nesse meio tempo).
            Objects.LoadReports();

            //Get only the filters from the specified interface.
            foreach (Relatorio r in LstReports.Where(x => x.Interface.ToUpper().Trim() == Interface.ToUpper().Trim() && x.Modulo.ToUpper().Trim() == Module.ToUpper().Trim()))
            {
                //Name that will show to the user
                //'!r.IsPrivate' means that this filter/report is public, all users can see/view
                if (!r.IsPrivate) ComboList.Items.Add($"[{r.UsrCrt}] - {r.NomeRelatorio}");
                else ComboList.Items.Add($"{r.NomeRelatorio}");
            }
        }
    }

    #endregion

}