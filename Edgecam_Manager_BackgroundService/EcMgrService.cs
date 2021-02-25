using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

//using Tulpep.NotificationWindow;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace Edgecam_Manager_BackgroundService
{
    public partial class EcMgrService : ServiceBase
    {

        #region Variáveis globais

        /// <summary>
        ///     Contém o caminho da licença no diretório de executável do sistema.
        /// </summary>
        private String mLocalLicenseFile = Path.Combine(Application.StartupPath, "License.lic");

        //Dados para conexão com o banco de dados do Edgecam
        private String mEcServer;
        private String mEcDb;
        private String mEcUser;
        private String mEcPass;
        //Dados para conexão com o banco de dados intermediário.
        private String mAuxServer;
        private String mAuxDb;
        private String mAuxUser;
        private String mAuxPass;
        //Dados para conexão com o banco de dados de log.
        private String mLogServer;
        private String mLogDb;
        private String mLogUser;
        private String mLogPass;
        //Contém o idioma definido pelo usuário.
        private System.Globalization.CultureInfo mIdioma;

        /// <summary>
        ///     True para caso o usuário tenha definido que queira
        /// se manter conectado.
        /// </summary>
        private Boolean mManterUserConectado;

        //Instanciação dos objetos das classes.
        private CustomXmlConfig mXml;

        /// <summary>
        ///     SQL Connection to Edgecam.
        /// </summary>
        private Sql mCnnBancoEc;

        /// <summary>
        ///     SQL Connection to Edgecam manager.
        /// </summary>
        private Sql mCnnBancoEcMgr;

        /// <summary>
        ///     SQL Connection to Log.
        /// </summary>
        private Sql mCnnBancoLog;

        private Boolean mHasInternetConnection;

        #endregion

        #region Enumeradores

        /// <summary>
        ///     Enumerador voltado para exceções (exclusivamente)
        /// </summary>
        internal enum e_TipoErroEx
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

        #endregion

        #region Instância dos objetos da classe

        public EcMgrService()
        {
            this.InitializeComponent();
            this.LoadConfig();
        }

        #endregion

        #region Eventos

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer tempoServico = new System.Timers.Timer();

            String mDirExecucaoServico = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\") + 1);

            //Tempo do serviço (60000 = 1 minuto)
            tempoServico.Interval = 10000;
            tempoServico.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            tempoServico.Start();
        }

        protected override void OnStop()
        {

        }

        protected void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            
        }

        public void OnDebug()
        {
            try
            {
                //Process.Start(@"C:\@Backup\@@@@@@@backup\Projects\@Edgecam_Manager - 2019 R2 - beta 1\Edgecam_Manager_Notifications\bin\Debug\Edgecam_Manager_Notifications.exe");
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Config

        /// <summary>
        ///     Load default configuration from XML file.
        /// </summary>
        private void LoadConfig()
        {
            mEcServer = mXml.StrXmlSimpleConfigValue("SERVER_EC");
            mEcDb = mXml.StrXmlSimpleConfigValue("BANCO_EC");
            mEcUser = mXml.StrXmlSimpleConfigValue("USER_EC");
            mEcPass = mXml.StrXmlSimpleConfigValue("PASS_EC");

            mAuxServer = mXml.StrXmlSimpleConfigValue("SERVER_AUX");
            mAuxDb = mXml.StrXmlSimpleConfigValue("BANCO_AUX");
            mAuxUser = mXml.StrXmlSimpleConfigValue("USER_AUX");
            mAuxPass = mXml.StrXmlSimpleConfigValue("PASS_AUX");

            mLogServer = mXml.StrXmlSimpleConfigValue("SERVER_LOG");
            mLogDb = mXml.StrXmlSimpleConfigValue("BANCO_LOG");
            mLogUser = mXml.StrXmlSimpleConfigValue("USER_LOG");
            mLogPass = mXml.StrXmlSimpleConfigValue("PASS_LOG");

            mIdioma = new System.Globalization.CultureInfo(mXml.StrXmlSimpleConfigValue("IDIOMA"));

            //Cria os objetos estáticos para cada banco de dados.
            mCnnBancoEc = new Sql(mEcServer, mEcDb, mEcUser, mEcPass);
            mCnnBancoEcMgr = new Sql(mAuxServer, mAuxDb, mAuxUser, mAuxPass);
            mCnnBancoLog = new Sql(mLogServer, mLogDb, mLogUser, mLogPass);

            //Verifica se o sistema possuí conexão com a internet.
            mHasInternetConnection = Utilities.CheckInternetConnection();
        }

        #endregion

        #region Coin cotation

        private void LoadQuotetionCoins()
        {
            DataTable dt = mCnnBancoEcMgr.ExecutaSql(QueriesSQL.CONSULTA_COTACOES_ATIVAS);

            if (dt != null && dt.Rows.Count > 0)
            {
                String hojeAtt = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

                //Se não tiver internet, o sistema ignora
                if (mHasInternetConnection)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        //Apenas Salvo o nome da moeda para facilitar.
                        String coinName = dt.Rows[x]["MoedaPesquisa"].ToString();

                        //Adicionei essa variável, pois se a moeda já foi pesquisada, eu trago do banco o valor
                        //e a data/hora da atualização.
                        String cotacaoHoje = GetCoinCotation(coinName);
                        String dtAtt = cotacaoHoje.Contains("|") ? cotacaoHoje.Split(new char[] { '|' })[1] : "";

                        //Contém a hora da consulta do valor da cotação.
                        UltraExplorerBarItem i2 = new UltraExplorerBarItem();
                        i2.Text = String.Format("Última atualização: '{0}'", !String.IsNullOrEmpty(dtAtt) ? dtAtt : hojeAtt);
                        i2.Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.relogio_despertador_16;
                        lstGrupos.Items.Add(i2);

                        //Contém a opção para consultar todo o histórico.
                        UltraExplorerBarItem i3 = new UltraExplorerBarItem();
                        i3.Text = String.Format("Histórico da moeda '{0}'", coinName);
                        i3.Settings.AppearancesSmall.Appearance.Image = Edgecam_Manager.Properties.Resources.table;
                        i3.Settings.AppearancesSmall.Appearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                        i3.Settings.AppearancesSmall.Appearance.ForeColor = Color.Blue;
                        lstGrupos.Items.Add(i3);

                        //Contém a opção para consultar todo o histórico.
                        UltraExplorerBarItem i4 = new UltraExplorerBarItem();
                        i4.Text = String.Format("Atualizar o valor da cotação da moeda '{0}'", coinName);
                        i4.Settings.AppearancesSmall.Appearance.Image = Edgecam_Manager.Properties.Resources.refresh;
                        i4.Settings.AppearancesSmall.Appearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                        i4.Settings.AppearancesSmall.Appearance.ForeColor = Color.Blue;
                        lstGrupos.Items.Add(i4);

                        ueb.Groups.Add(lstGrupos);
                    }
                }
            }
        }

        /// <summary>
        ///     Método que busca as cotações ativas no banco de dados intermediário
        /// e as adiciona na interface para o usuário.
        /// </summary>
        /// <param name="Moeda">Nome da moeda a ser pesquisada.</param>
        private void GetCoinCotation(String Moeda)
        {
            try
            {
                System.Net.WebClient client;

                //  Primeiro verifica se já foi pesquisada a cotação do dia que está armazenada
                //no banco de dados, caso ela exista, retorna ela.
                if (!CoinAlreadyQueried(Moeda))
                {

                }

                if (!String.IsNullOrEmpty(cotacaoSalvaBanco))
                    return cotacaoSalvaBanco;
                else
                {
                    //O sistema só consulta/obtém a cotação atual se tiver internet.
                    if (mPossuiConexaoInternet)
                    {
                        client = new System.Net.WebClient();
                        System.IO.Stream data = client.OpenRead(String.Format("https://www.google.com.br/search?client=opera&q={0}+hoje&sourceid=opera&ie=UTF-8&oe=UTF-8", Moeda));
                        //System.IO.Stream data = client.OpenRead(String.Format("https://dolarhoje.com/{0}", Moeda.ToUpper().Contains("DÓLAR") ? "" : Moeda.Replace(" ", "") + "-hoje"));
                        System.IO.StreamReader reader = new System.IO.StreamReader(data);
                        string s = reader.ReadToEnd();

                        data.Close();
                        reader.Close();

                        //Contém o valor da cotação do dia de hoje de acordo com o google.
                        //String valorCotacaoAtual = CustomStrings.GetTextBetween(s, "<div class=\"J7UKTe\">", "</div>");
                        String valorCotacaoAtual = CustomStrings.GetTextBetween(s, "<div class=\"BNeawe iBp4i AP7Wnd\">", "</div></div></div></div></div><div class=").Replace(".", ",");
                        //String valorCotacaoAtual = CustomStrings.GetTextBetween(s, "<input type=\"text\" id=\"nacional\" value=\"", "\"/></span><span class=\"optional\">").Replace(".", ",");

                        //Criei essa condição, pois em determinadas situações, o sistema
                        //não consegue obter o valor da moeda do dia (por alguma razão
                        //desconhecida).
                        if (String.IsNullOrEmpty(valorCotacaoAtual))
                        {
                            valorCotacaoAtual = String.Format("1 {0} = ", Moeda);
                            valorCotacaoAtual += CustomStrings.GetTextBetween(s, "<div><div><span><div class=", "</div></span></div><div><span><div").Split(new char[] { '>' })[1].Replace(".", ",");
                        }

                        valorCotacaoAtual = String.Format("1 {0} = {1} Real Brasileiro", Moeda, valorCotacaoAtual.ToUpper().Replace("REAL BRASILEIRO", "").Trim());

                        SalvaCotacaoDiaria(Moeda, valorCotacaoAtual);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        ///     Verifica no banco de dados se já existe uma cotação atual do dia.
        /// </summary>
        /// <param name="Moeda">Moeda/Cotação à ser verificada.</param>
        /// <returns>String contendo o valor do banco ou vazio caso o valor ainda não exista.</returns>
        private Boolean CoinAlreadyQueried(String Moeda)
        {
            try
            {
                DataTable dt = mCnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_COTACAO_DE_HOJE, new Dictionary<string, object>() { { "@MOEDA", Moeda }, { "@DT", DateTime.Now.ToString("yyyy-MM-dd") } });

                return dt.Rows.Count > 0;
            }
            catch { return false; }
        }

        #endregion

        #region Integration

        private void Import()
        {

        
        }

        private void Export()
        {

        }

        #endregion

        /// <summary>
        ///     Save error inside the log database.
        /// </summary>
        /// <param name="TituloErro"></param>
        /// <param name="Acao"></param>
        /// <param name="TipoErro"></param>
        /// <param name="MsgAux"></param>
        /// <param name="Ex"></param>
        private void SaveError(String TituloErro, String Acao, e_TipoErroEx TipoErro, String MsgAux, Exception Ex)
        {
            //Dicionário contendo os parâmetros do insert.
            Dictionary<String, object> dic = new Dictionary<string, object>();

            dic.Add("@APP", "Edgecam Manager Service sync");
            dic.Add("@APPVER", Application.ProductVersion);
            dic.Add("@FORMNAME", DBNull.Value);
            dic.Add("@ACAO", Acao);
            dic.Add("@MSG", String.IsNullOrEmpty(MsgAux) == true ? "<None>" : MsgAux);
            dic.Add("@USER", Environment.UserName);
            dic.Add("@DATA", DateTime.Now);
            dic.Add("@TIPOERRO", (int)TipoErro);
            dic.Add("@TITLE", TituloErro);

            if (Ex != null)
            {
                dic.Add("@EXTIPOERRO", Ex.GetType().ToString());

                /*
                 *  Dionei Beilke dos Santos
                 *  21/08/2018, at 12:43 AM
                 *  Adicionei as condições abaixo, pois estava salvando somente o primeiro nível
                 * da exceção. Agora, o sistema trata, gera um arquivo temporário e armazena o log
                 * no banco de dados.
                 */
                CustomException skaEx = new CustomException(Ex, Ex.Message, false);
                String stackStrace = System.IO.File.ReadAllText(skaEx._DiretorioArquivoLog);
                System.IO.File.Delete(skaEx._DiretorioArquivoLog);

                dic.Add("@EXSTACKRACE", stackStrace);

                //Caso ser um erro provido em um banco de dados, pego o número (para abrir uma investigação posteriormente).
                if (Ex.GetType().ToString() == "Oracle.DataAccess.Client.OracleException")
                {
                    String errorCode = Ex.GetType().GetProperty("Number").ToString();

                    dic.Add("@EXERRORCODE", errorCode);
                }
                else if (Ex.GetType().ToString() == "SqlClient.SqlException")
                {
                    String errorCode = Ex.GetType().GetProperty("Number").ToString();

                    dic.Add("@EXERRORCODE", errorCode);
                }
                else
                {
                    dic.Add("@EXERRORCODE", "<None>");
                }

                dic.Add("@COMMENTUSER", DBNull.Value);
                dic.Add("@QUERY", DBNull.Value);
            }
            else
            {
                dic.Add("@EXTIPOERRO", "");
                dic.Add("@EXSTACKRACE", "");
                dic.Add("@EXERRORCODE", "");
                dic.Add("@COMMENTUSER", DBNull.Value);
                dic.Add("@QUERY", DBNull.Value);
            }



            //Grava a exceção no banco de dados.
            mCnnBancoLog.ExecutaSql(QueriesSQL.INSERE_NOVA_EXCECAO, dic);
        }

        #endregion
    }
}
