using Edgecam_Manager.Idiomas;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Edgecam_Manager_License;
using Edgecam_Manager_LicenseConfig;
using System.IO;
using System.Reflection;

namespace Edgecam_Manager
{
    internal partial class FrmLogin : Form
    {

        #region Variáveis globais (públicas e privadas) e/ou estáticas

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
        ///     Contém o objeto que dá acesso ao formulário main.
        /// </summary>
        //public FrmMain mFrmMain;

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Inicializa a instância do objeto e verifica configurações iniciais.
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();

            Text += " 2020 R1 - Beta 2";

            if (IsSystemConfigured())
            {
                mXml = new CustomXmlConfig("Config.xml");
                this.LoadConfig();
                this.LoadUser();
                this.EnableControls();
                this.DefineLanguage();

                /*
                 *  Dionei Beilke dos Santos
                 *  10/08/2018, at 11:41 AM
                 *  ECMGR-162
                 */
                Objects.MensagensSistema = new Messages(Objects.CfgAtual._Idioma.Name.ToUpper());
            }                
            else
            {
                /*
                 * Dionei Beilke dos Santos
                 * 10/08/2018, at 03:56 PM
                 * ECMGR-163
                 */
                System.Globalization.CultureInfo c = System.Globalization.CultureInfo.CurrentCulture;

                Objects.MensagensSistema = new Messages(c.Name.ToUpper());

                Messages.Msg001();
                txtLoginFailed.Visible = true;

                this.DisableControls();
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que troca o idioma do sistema para o idioma definido pelo usuário.
        /// </summary>
        private void DefineLanguage()
        {
            //Inglês.
            if (Objects.CfgAtual._Idioma.Name.ToUpper() == "EN-US") 
            {
                label1.Text = en_US.FrmLogin_Usuario;
                label2.Text = en_US.FrmLogin_Senha;
                txtLoginFailed.Text = en_US.FrmLogin_Falha_login;
                cbxManterConectado.Text = en_US.FrmLogin_Manter_conectado;
                linkLabel_ForgotPassword.Text = en_US.FrmLogin_Esqueceu_senha;
                btnIniciar.Text = en_US.FrmLogin_Iniciar_sessao;
            }
            //Português. (Não faz nada, o padrão é português.
            if (Objects.CfgAtual._Idioma.ToString() == "PT-BR") { }
        }

        /// <summary>
        ///     Bloqueia os controles da interface.
        /// </summary>
        private void DisableControls()
        {
            txtUser.Enabled = false;
            txtSenha.Enabled = false;
            btnIniciar.Enabled = false;
            linkLabel_ForgotPassword.Enabled = false;
            cbxManterConectado.Enabled = false;
        }

        /// <summary>
        ///     Desbloqueia os controles da interface.
        /// </summary>
        private void EnableControls()
        {
            txtUser.Enabled = true;
            txtSenha.Enabled = true;
            btnIniciar.Enabled = true;
            linkLabel_ForgotPassword.Enabled = true;
            cbxManterConectado.Enabled = true;
        }

        /// <summary>
        ///     Verifica se existe algum arquivo de configuração.
        /// </summary>
        /// <returns></returns>
        private Boolean IsSystemConfigured()
        {
            return System.IO.File.Exists(Application.StartupPath + "\\Config.xml");
        }

        /// <summary>
        ///     Carrega as configurações do arquivo XML.
        /// </summary>
        private void LoadConfig()
        {
            mEcServer   = mXml.StrXmlSimpleConfigValue("SERVER_EC");
            mEcDb       = mXml.StrXmlSimpleConfigValue("BANCO_EC");
            mEcUser     = mXml.StrXmlSimpleConfigValue("USER_EC");
            mEcPass     = mXml.StrXmlSimpleConfigValue("PASS_EC");

            mAuxServer  = mXml.StrXmlSimpleConfigValue("SERVER_AUX");
            mAuxDb      = mXml.StrXmlSimpleConfigValue("BANCO_AUX");
            mAuxUser    = mXml.StrXmlSimpleConfigValue("USER_AUX");
            mAuxPass    = mXml.StrXmlSimpleConfigValue("PASS_AUX");

            mLogServer  = mXml.StrXmlSimpleConfigValue("SERVER_LOG");
            mLogDb      = mXml.StrXmlSimpleConfigValue("BANCO_LOG");
            mLogUser    = mXml.StrXmlSimpleConfigValue("USER_LOG");
            mLogPass    = mXml.StrXmlSimpleConfigValue("PASS_LOG");

            mIdioma = new System.Globalization.CultureInfo(mXml.StrXmlSimpleConfigValue("IDIOMA"));
            String ColorTheme = mXml.StrXmlSimpleConfigValue("THEME");

            //mManterUserConectado = Convert.ToBoolean(mXml.StrXmlSimpleConfigValue("MANTER_CONECTADO"));

            //if (mManterUserConectado)
            //{
            //    txtUser.Text = mXml.StrXmlSimpleConfigValue("USUARIO");
            //    txtSenha.Text = mXml.StrXmlSimpleConfigValue("SENHA");
            //}

            String EcStrCnn     = Sql.CriaStringConexao(mEcServer, mEcDb, mEcUser, mEcPass);
            String AuxStrCnn    = Sql.CriaStringConexao(mAuxServer, mAuxDb, mAuxUser, mAuxPass);
            String LogStrCnn    = Sql.CriaStringConexao(mLogServer, mLogDb, mLogUser, mLogPass);

            //Cria um objeto estático, de maneira que possa 'reutilizar' os objetos em toda a interface.
            Objects.CfgAtual = new Config(mEcServer, mEcDb, mEcUser, mEcPass, EcStrCnn, mAuxServer, mAuxDb, mAuxUser, mAuxPass, AuxStrCnn, mIdioma, ColorTheme);

            //Cria os objetos estáticos para cada banco de dados.
            Objects.CnnBancoEc      = new Sql(EcStrCnn);
            Objects.CnnBancoEcMgr   = new Sql(AuxStrCnn);
            Objects.CnnBancoLog     = new Sql(LogStrCnn);
        }

        /// <summary>
        ///     Método responsável por identificar o usuário ativo.
        /// </summary>
        private void LoadUser()
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "ConfigCurrentUser.xml")))
            {
                CustomXmlConfig xml = new CustomXmlConfig("ConfigCurrentUser.xml");

                mManterUserConectado = Convert.ToBoolean(String.IsNullOrEmpty(xml.StrXmlSimpleConfigValue("MANTER_CONECTADO")) ? "false" : xml.StrXmlSimpleConfigValue("MANTER_CONECTADO"));

                if (mManterUserConectado)
                {
                    String tmpUsr = xml.StrXmlSimpleConfigValue("USUARIO");
                    String tmpPws = xml.StrXmlSimpleConfigValue("SENHA");

                    //CustomEncrypt e = new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, "Edgecam_Manager_Random.SkaRandom.e_h01x_h19xfffS4A");
                    CustomEncrypt e = new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A);

                    if (!String.IsNullOrEmpty(tmpUsr)) txtUser.Text = e.DecryptString(tmpUsr);
                    if (!String.IsNullOrEmpty(tmpPws)) txtSenha.Text = e.DecryptString(tmpPws);
                }
                //Load only the user.
                else
                {
                    String tmpUsr = xml.StrXmlSimpleConfigValue("USUARIO");

                    //CustomEncrypt e = new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, "Edgecam_Manager_Random.SkaRandom.e_h01x_h19xfffS4A");
                    CustomEncrypt e = new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A);

                    if (!String.IsNullOrEmpty(tmpUsr)) txtUser.Text = e.DecryptString(tmpUsr);
                }
            }
        }

        /// <summary>
        ///     Método que verifica se o usuário informando é válido, retornando
        /// true caso tenha logado com êxito e false caso contrário.
        /// </summary>
        private Boolean CheckUser()
        {
            try
            {
                Objects.UsuarioAtual = new Usuario(txtUser.Text, txtSenha.Text);

                if (Objects.UsuarioAtual != null && Objects.UsuarioAtual.Login != null)
                {
                    //Apenas para depuração, sempre irei reiniciar o usuário.
                    #if DEBUG
                        Objects.CnnBancoEcMgr.ExecutaSql($"update Usuarios set IsLogged = 0 where id = {Objects.UsuarioAtual.Id}");
                        Objects.UsuarioAtual.IsLogged = false;
                    #endif

                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper() == "Erro ao tentar conectar ao servidor e autenticar o usuário".ToUpper())
                {
                    if (Objects.CfgAtual._Idioma.Name.ToUpper() == "EN-US")
                    {
                        txtLoginFailed.Text = en_US.FrmLogin_Servico_sql_parado;
                    }
                    if (Objects.CfgAtual._Idioma.Name.ToUpper() == "PT-BR")
                    {
                        txtLoginFailed.Text = "Verifique se o serviço SQL Server do servidor está inicializado";
                    }
                }

                return false;
            }
        }

        /// <summary>
        ///     Método que verifica o estado da licença
        /// </summary>
        private void CheckLicenseStatus()
        {
            try
            {
                //Primeiro verifico se existe uma configuração de licença.
                if (File.Exists(Path.Combine(Application.StartupPath, "Edgecam_Manager_License.dll")) && 
                    File.Exists(Path.Combine(Application.StartupPath, "Edgecam_Manager_LicenseConfig.dll")))
                {
                    if (HasInstalledLicense())
                        ValidateLicense();
                    else
                    {
                        if (MessageBox.Show("Não há licenças instaladas no computador corrente. Deseja procurar uma arquivo de licença agora?", "Licença não localizada", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                        {
                            Utilities u = new Utilities();
                            String f = u.BuscaArquivo("lic", "Arquivo de licença SKA");

                            if (File.Exists(f))
                            {
                                this.LoadLicenseFromFile(f);
                                //Chamo esse método recursivo.
                                this.CheckLicenseStatus();
                            }
                            else throw new FileNotFoundException("O sistema não pode localizar uma licença válida. Contate o administrador do sistema.");
                        }
                        else throw new FileNotFoundException("O sistema não pode localizar uma licença válida. Contate o administrador do sistema.");
                    }
                }
                else throw new ApplicationException("Não foi possível carregar os assemblies da licença. Contate o administrador do sistema.");
            }
            catch (Exception ex)
            {
                //Só faço um try catch para caso eu não consiga apagar o arquivo.
                try { File.Delete(mLocalLicenseFile); } catch { }

                MessageBox.Show(ex.Message, "Licença não definida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Método que verifica se existe uma licença instalada.
        /// </summary>
        /// <returns>True caso há alguma licença instalada.</returns>
        private Boolean HasInstalledLicense()
        {
            try
            {
                return File.Exists(Path.Combine(Application.StartupPath, "License.lic"));
            }
            catch { return false; }
        }

        /// <summary>
        ///     Método responsável por carregar os dados de uma licença no computador atual,
        /// dentre outras informações.
        /// </summary>
        /// <param name="LicenseFile">Caminho do arquivo da licença</param>
        /// <param name="CopyToStartupPath">Copiar o arquivo de licença para o diretório do executável.</param>
        private void LoadLicenseFromFile(String LicenseFile, Boolean CopyToStartupPath = true)
        {
            try
            {
                String content = File.ReadAllText(LicenseFile);

                if (!String.IsNullOrEmpty(content))
                {
                    CustomEncrypt e = new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h19xfffS4A);

                    String[] lines = content.Split(new char[] { '\n' });

                    Objects.License = new LicenseInfo();
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h01xfffS4A, e.DecryptString(lines[0].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h02xfffS4A, e.DecryptString(lines[1].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h03xfffS4A, e.DecryptString(lines[2].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h04xfffS4A, e.DecryptString(lines[3].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h05xfffS4A, lines[4].Replace("#", ""));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h06xfffS4A, e.DecryptString(lines[5].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h07xfffS4A, e.DecryptString(lines[6].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h08xfffS4A, e.DecryptString(lines[7].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h09xfffS4A, e.DecryptString(lines[8].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h10xfffS4A, e.DecryptString(lines[9].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h11xfffS4A, e.DecryptString(lines[10].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h12xfffS4A, e.DecryptString(lines[11].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h13xfffS4A, e.DecryptString(lines[12].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h14xfffS4A, e.DecryptString(lines[13].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h15xfffS4A, e.DecryptString(lines[14].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h16xfffS4A, e.DecryptString(lines[15].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h17xfffS4A, e.DecryptString(lines[16].Replace("#", "")));
                    Objects.License.UpdateConfigValue(LicenseInfo.e_h01x.h18xfffS4A, e.DecryptString(lines[17].Replace("#", "")));

                    if (CopyToStartupPath)
                        File.Copy(LicenseFile, mLocalLicenseFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar identificar os módulos da licença instalada. " + ex.Message, ex);
            }
        }

        /// <summary>
        ///     Método que checa se a licença instalada está com os dados coerentes com
        /// o computador atual.
        /// </summary>
        private void ValidateLicense()
        {
            this.LoadLicenseFromFile(mLocalLicenseFile, false);

            if (Objects.License != null)
            {
                Dns d = new Dns();

                //Licença local
                if (Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h06xfffS4A) == "0")
                {
                    //Verifica se a licença já expirou.
                    if (CustomTimes.CompareDates(DateTime.Now, Convert.ToDateTime(Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h03xfffS4A))))
                        throw new EcMGRLicenseExpiredException(Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h03xfffS4A));
                    //Verifica se o nome do server é o mesmo.
                    else if (Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h07xfffS4A).ToUpper().Trim() != d._MachineName.ToUpper().Trim())
                        throw new EcMGRLicenseChangedException(d._MachineName.ToUpper().Trim(), "machine name");
                    //Verifica se o MacAddress é dessa máquina.
                    else if (!d.ExistMacAddressInHost(Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h10xfffS4A)))
                        throw new EcMGRComputerChangedExpection(d._MachineName.ToUpper().Trim(), "mac address");
                    //Verifica se o sistema ainda é o mesmo.
                    else if (Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h12xfffS4A).ToUpper().Trim() != d._OsVersion.ToUpper().Trim())
                        throw new EcMGRComputerChangedExpection(d._MachineName, "windows version");
                    //Valida o domínio para verificar se trocou (não deve acontecer, mas pode)
                    else if (Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h09xfffS4A).ToUpper().Trim() != d._MachineDomainName.ToUpper().Trim())
                        throw new EcMGRComputerChangedExpection(d._MachineName, "machine domain has changed");                    
                }
                else throw new EcMGRLicenseTypeNotSupportedException(Objects.License.GetSingleConfigValue(LicenseInfo.e_h01x.h06xfffS4A));
            }
            else throw new EcMGRLicenseNotLoadedException("'License' is null, we cannot read the information from license file or connect to server.");
        }

        /// <summary>
        ///     Check the database version based on application version.
        /// </summary>
        /// <returns>True if is the same version (Application and database)</returns>
        private Boolean CheckDatabaseVersion()
        {
            String av = Application.ProductVersion.ToString();

            try
            {
                DataTable dv = SQLQueries.Query_DatabaseInfo();

                if(dv != null && dv.Rows.Count > 0)
                {
                    if (av.ToUpper().Trim() != dv.Rows[0]["VersaoDataBase"].ToString().ToUpper().Trim())
                    {
                        txtLoginFailed.Text = $"A versão da aplicação ({av}) não corresponde a versão \ndo banco de dados ({dv.Rows[0]["VersaoDataBase"].ToString()}). Contate um administrador do sistema.";
                        txtLoginFailed.Visible = true;

                        Objects.CadastraNovoLog(false, txtLoginFailed.Text, "FrmLogin", "Login", "", "", e_TipoErroEx.Informacao);

                        return false;
                    }
                    else return true;
                }
                else return true;
            }
            catch { return false; }
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Ao carregar a interface, verifica se o usuário deseja se manter conectado
        /// e já o verifica se está com os dados corretos (Login e senha)
        /// </summary>
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //Não adicionar 'try/catch' nesse trecho, pois já existe um dentro do evento 'btnIniciar_Click'
            txtLoginFailed.Visible = false;

            CheckLicenseStatus();

            if (mManterUserConectado && CheckUser())
            {
                btnIniciar_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Quando acionado o evento, verifica se existe um usuário correspondente
        /// no banco intermediário da integração.
        /// </summary>
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckUser())
                {
                    txtLoginFailed.Visible = true;
                }
                else if (Objects.UsuarioAtual.UserAtivo == 0)
                {
                    if (Objects.CfgAtual._Idioma.Name.ToUpper() == "EN-US")
                    {
                        txtLoginFailed.Text = en_US.FrmLogin_Usuario_desativado;
                        txtLoginFailed.Visible = true;
                        return;
                    }
                    if (Objects.CfgAtual._Idioma.Name.ToUpper() == "PT-BR")
                    {
                        txtLoginFailed.Text = "Seu usuário não está mais ativo";
                        txtLoginFailed.Visible = true;
                        return;
                    }
                }
                else
                {
                    if (Objects.UsuarioAtual.IsLogged == true)
                    {
                        txtLoginFailed.Text = "Usuário já está conectado. \nSistema não suporta múltiplos dispositivos conectados.";
                        txtLoginFailed.Visible = true;
                        return;
                    }
                    else if (cbxManterConectado.Checked)
                    {
                        mXml.UpdateXmlConfigValue("USUARIO", txtUser.Text);
                        mXml.UpdateXmlConfigValue("SENHA", txtSenha.Text);
                    }

                    //ECMGR-2
                    //Note: Inside the method, we call the error.
                    if (!CheckDatabaseVersion())
                        return;

                    //'Esconde' essa interface
                    this.Visible = false;
                    this.ShowInTaskbar = false;
                    this.Opacity = 0;
                    this.Hide();

                    //ECMGR-359
                    //Define user as 'logged'
                    Objects.CnnBancoEcMgr.ExecutaSql($"update Usuarios set IsLogged = 1 where id = {Objects.UsuarioAtual.Id}");

                    //Instância do objeto
                    if (Objects.FormularioPrincipal == null)
                    {
                        //Objects.FormularioPrincipal = new FrmMain();
                        Objects.FormularioPrincipal = new FrmMain2();
                    }
                    else
                    {
                        Objects.FormularioPrincipal.ShowInTaskbar = true;
                        Objects.FormularioPrincipal.Opacity = 100;
                    }

                    Objects.FormularioPrincipal.ShowInTaskbar = false;
                    Objects.FormularioPrincipal.Visible = false;
                    Objects.FormularioPrincipal.Opacity = 0;
                    Objects.FormularioPrincipal.Show();
                    Objects.FormularioPrincipal.ShowInTaskbar = false;
                    Objects.FormularioPrincipal.Visible = false;
                    Objects.FormularioPrincipal.Opacity = 0;

                    FrmLoadingScreen frm = new FrmLoadingScreen();
                    frm.Focus();
                    frm.ShowDialog();

                    Objects.FormularioPrincipal.ShowInTaskbar = true;
                    Objects.FormularioPrincipal.Opacity = 100.0;
                    Objects.FormularioPrincipal.Visible = true;
                                        
                    DialogResult formaSaida = Objects.FormularioPrincipal.DialogResult;

                    do
                    {
                        Application.DoEvents();
                        formaSaida = Objects.FormularioPrincipal.DialogResult;
                    }
                    while (formaSaida == System.Windows.Forms.DialogResult.None);

                    //Se for CANCEL, usuário decidiu fechar a interface.
                    if (formaSaida == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.Close();
                        GC.Collect();
                    }
                    //Se for OK, usuário efetuou logoff.
                    else if (formaSaida == System.Windows.Forms.DialogResult.OK)
                    {
                        //Reinicializa o objeto
                        Objects.FormularioPrincipal = null;

                        //Recarrega as configurações do sistema, pois o usuário fez um logoff.
                        LoadConfig();

                        //'Mostra' essa interface
                        Visible = true;
                        ShowInTaskbar = true;
                        Opacity = 100;
                        Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar efetuar logon", "FrmLogin", "Login", "Exceção não tratada em uma tentativa de login",
                                           "Consultas_EcMgr.CONSULTA_USUARIO", e_TipoErroEx.Erro, ex);
                this.Close();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Abre uma interface para o usuário trocar a senha do mesmo.
        /// </summary>
        private void linkLabel_ForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FrmLogin_NewPass frm = new FrmLogin_NewPass();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar trocar a senha", "FrmNewPass", "TrocarSenha", "Exceção não tratada em uma tentativa de troca de senha",
                                           "Consultas_EcMgr.ATUALIZA_SENHA_USUARIO", e_TipoErroEx.Erro, ex);
            }            
        }

        /// <summary>
        ///     Abre uma interface que permite o usuário solicitar o login.
        /// </summary>
        private void linkLabel_ReqLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FrmLogin_Req frm = new FrmLogin_Req();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao solicitar login", "FrmLogin_Req", "solicitar novo usuario", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Método que abre a configuração dos bancos de dados para utilizar o sistema.
        /// </summary>
        private void btnCfgDbs_Click(object sender, EventArgs e)
        {
            try
            {
                FrmLogin_UserCnns frm = new FrmLogin_UserCnns();
                frm.ShowDialog();

                if (FrmLogin_UserCnns.sSalvouArq)
                {
                    LoadConfig();
                    EnableControls();
                }
                else
                {
                    Messages.Msg001();
                    txtLoginFailed.Visible = true;

                    DisableControls();
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar carregar as configurações realizadas pelo usuário", "FrmLogin/FrmUserConfigCnns", "Login", 
                                           "Exceção não tratada em uma tentativa de login", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que ao pressionar a tecla enter, tenta realizar uma tentativa de login do usuário.
        /// </summary>
        /// <remarks>Não adicionar exceção, dentro do método chamado por esse evento já tem uma tratativa.</remarks>
        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIniciar_Click(new object(), new EventArgs());
            }
        }

        #endregion
    }
}