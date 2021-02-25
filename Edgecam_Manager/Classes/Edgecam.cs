/***********************************************************************************************************
 * 
 * 
 *                  Edgecam - Classe com métodos específicos para atuar em conjunto com o EDGECAM
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Funções específicas relacionadas ao software EDGECAM
 *      Version:    4.9
 *      Date:       14/12/2016, at 12:16 AM
 *      Note:       <None>
 *      Updates:    Update - 09/02/2017 - 4:51 PM - Corrigido no método 'StrConexaoConexaoSqlEc', o problema
 *                      		                do mesmo não substituir o servidor, pois no switch case, estava como 'DATABASE',
 *                     		                    mas na realidade deveria ser 'DATA SOURCE'. - V1.1 Lançada
 *                  Update - 09/02/2017 - 11:08 PM - Adicionado no método 'StrConexaoConexaoSqlEc', o switch 'SERVER',
 *                     		                    para computadores que estão apontando o caminho na rede. - V1.2 Lançada
 *					Update - 17/02/2017 - 02:17 PM - Corrigido o problema no método 'InstalledMachinesEc', quanto ao 
 *                                              retorno das máquinas instaladas no edgecam. - V1.3 Lançada
 *                  Update - 20/03/2017 - 03:16 PM - Corrigido o problema no método 'BuscaCaminhoEcVersionSpeficly', o qual
 *                                              estava retornando sempre 'vazio' ("") - V1.4 Lançada.
 *                  Update - 27/03/2017 - 09:47 AM - Adicionado um novo método 'StrLicencaAtivaEc' - V1.5 Lançada.
 *                  Update - 30/03/2017 - 09:53 AM - Adicionado um novo parâmetro no método 'InstalledMachinesEc', onde, adicionei
 *                                              a opção de incluir os pós-processadores padrões do Edgecam. - V1.6 Lançada.
 *                  Update - 31/03/2017 - 02:05 PM - Corridigo o problema no método 'VersoesInstaladasEc', pois o mesmo não estava retornando
 *                                              todas as versões (apenas as versões R1, pois os apontamentos dos registros estavam errados) - V1.7 Lançada
 *                  Update - 31/03/2017 - 02:32 PM - Corrigido o problema de, na lista de máquinas, dar excessão para versões inferiores
 *                                              à 2015 R1. Criei uma lógica para isso, verificando se a versão passada por parâmetro é
 *                                              maior que 2015.10, se sim, é vero software. - V1.8 Lançada.
 *                  Update - 31/03/2017 - 03:19 PM - Corrigido o problema no método 'StrLicencaAtivaEc', onde, caso passava a versão com '.20'[
 *                                              no final, ele dava erro, sendo assim, eu faço um cálculo diminuindo '.10'. - V1.9 Lançada.
 *                  Update - 06/04/2017 - 10:26 PM - Adicionado um novo método 'StrReturnToolStoreDirectory' - V2.0 Lançada
 *                  Update - 10/04/2017 - 09:47 AM - Corrigido um problema no método 'StrReturnToolStoreDirectory' - V2.1 Lançada
 *                  Update - 25/04/2017 - 14:26 PM - Implementado uma nova funcionalidade no método 'InstalledMachinesEc', onde, eu
 *                                              valido se o Edgecam foi aberto em algum momento (para criar os registros de onde ele
 *                                              está buscando os arquivos de pós, do arquivamento público ou do usuário) - V2.2 Lançada
 *                  Update - 28/04/2017 - 11:43 AM - Implementado um novo Método 'IsHomeworkMode' - V2.3 Lançada.
 *                  Update - 02/05/2017 - 10:30 PM - Corrigido o problema no método 'StrLicencaAtivaEc' o qual retornava
 *                                              sempre nulo - V2.0.1 Lançada
 *                  Update - 23/05/2017 - 03:07 PM - Corrigido o problema do método 'StrLicencaAtivaEc' - V2.4 Lançada
 *                  Update - 23/05/2017 - 03:28 PM - Corrigido o problema do método 'StrLicencaAtivaEc' - V2.5 Lançada
 *                  Update - 24/05/2017 - 08:11 AM - Adicionado novas validações no método 'StrLicencaAtivaEc' - V2.6 Lançada
 *                  Update - 30/06/2017 - 06:11 PM - No método 'LstInstalledMachinesEc', retorna todas as máquinas como minúsculo - V2.7 Lançada
 *                  Update - 14/08/2017 - 12:00 AM - No método 'OpenNewEdgecamWithArg2', adicionado um novo parâmetro - V2.8 Lançada
 *                  Update - 06/09/2018 - 10:15 AM - Refatorado toda a classe - V2.9 Lançada
 *                  Update - 06/09/2018 - 03:29 PM - Adicionado novas instancias dos objetos da classe - V3.0 Lançada
 *                  Update - 06/09/2018 - 03:29 PM - Adicionado enumeradores e propriedades à classe - V3.1 Lançada
 *                  Update - 18/09/2018 - 05:17 PM - Corrigido alguns problemas no método 'LstMaquinasInstaladas' - V3.2 Lançada
 *                  Update - 24/09/2018 - 09:40 PM - Adicionado um novo método estático 'LstExtensoesValidasSolidos' - V3.3 Lançada
 *                  Update - 22/10/2018 - 12:33 AM - Corrigido um problema de exceção no método 'BuscaDadosUltimaVersao' - V3.4 Lançada
 *                  Update - 22/10/2018 - 05:44 PM - Adicionado um novo método de nome 'LstUltimosArquivosAbertos' - V3.5 Lançada
 *                  Update - 28/02/2019 - 09:08 AM - Adicionado um novo método estático 'ConvertAscII_ToString' - V3.6 Lançada
 *                  Update - 11/06/2019 - 07:30 AM - Adicionado um novo método estático 'ConverteImagem_ToByteArray' - V3.7 Lançada.
 * 					Update - 27/06/2019 - 10:00 AM - Resolvido um problema no método 'RetornaValorRegistro' - V3.8 Lançada.
 * 					Update - 15/07/2019 - 02:48 PM - Resolvido um problema no método 'RetornaValorRegistro' - V3.9 Lançada.
 * 					Update - 15/07/2019 - 15:16 PM - Implementado o suporte para a versão 2020.0 do Edgecam - V4.0 Lançada.
 * 					Update - 15/10/2019 - 11:45 AM - Adicionado o suporte para a versão 2020.1 do Edgecam - V4.1 Lançada.
 * 					Update - 15/10/2019 - 11:52 AM - Adicionado a classe 'EdgecamNotInstalledException' para exceções especiais - V4.2 Lançada.
 * 					Update - 15/10/2019 - 11:53 AM - Corrigido um problema no método 'BuscaDadosUltimaVersao' - V4.3 Lançada.
 * 					Update - 15/10/2019 - 01:37 PM - Adicionado o suporte em todos os métodos para o diretório 'Hexagon' - V4.4 Lançada.
 * 					Update - 15/10/2019 - 01:54 PM - Corrigido um problema no méotodo 'IsLicencaClienteValida' - V4.5 Lançada.
 * 					Update - 15/10/2019 - 01:57 PM - Adicionado o parâmetro 'Version' no método 'IsLicencaClienteValida' - V4.5 Lançada.
 * 					Update - 15/10/2019 - 02:11 PM - Adicionado o método 'GetCLS_XmlDirectory' - V4.6 Lançada.
 * 					Update - 15/10/2019 - 02:32 PM - Corrigido um problema no método 'RetornaValorRegistro' - V4.7 Lançada.
 * 					Update - 15/10/2019 - 02:42 PM - Corrigido um problema no método 'IsLicencaClienteValida' - V4.8 Lançada.
 * 					Update - 15/10/2019 - 15:28 PM - Corrigido um problema no método 'LstUltimosArquivosAbertos' - V4.9 Lançada.
 * 
 * 
***********************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

public class Edgecam
{

    #region Variáveis Globais

    /// <summary>
    ///     Local no REGEDIT para versões da PLANIT (Versão 2015.10 deixou de ser planit)
    /// </summary>
    private String mRegistro_Planit = "SOFTWARE\\Wow6432Node\\Planit\\Edgecam";

    /// <summary>
    ///     Local no REGEDIT para versões da VERO SOFTWARE (Versão 2015.10 deixou de ser planit)
    /// </summary>
    private String mRegistro_Vero = "SOFTWARE\\Wow6432Node\\Vero Software\\Edgecam";

    /// <summary>
    ///     Local no REGEDIT para versões da HEXAGON MI (versão 2002.1 deixou de ser vero software).
    /// </summary>
    private String mRegistro_Hexagon = "SOFTWARE\\Wow6432Node\\Hexagon\\Edgecam";

    /// <summary>
    ///     Contém a licença do cliente (para validar se a licença é válida)
    /// </summary>
    private String mLicencaCliente;

    /// <summary>
    ///     Contém a licença ativa (instalada) no computador do usuário corrente.
    /// </summary>
    private String mLicencaAtiva;

    /// <summary>
    ///     Contém a informação que determina se a licença do cliente é ou não válida.
    /// </summary>
    private Boolean mIsLicencaClienteValida;

    /// <summary>
    ///     Contém a última versão instalada do Edgecam no computador corrente.
    /// </summary>
    private String mUltimaVersaoInstalada;

    /// <summary>
    ///     Contém o caminho da pasta de instalação do Edgecam
    /// </summary>
    private String mCaminhoInstalacao;

    /// <summary>
    ///     Contém o caminho do executável do Edgecam.
    /// </summary>
    private String mCaminhoExecutavel;

    /// <summary>
    ///     Contém o caminho do arquivamento do Edgecam
    /// </summary>
    private String mCaminhoToolStore;

    /// <summary>
    ///     Contém o brand manager (versão da planit, da vero software ou da hexagon mi) instalado no computador atual.
    /// </summary>
    private e_SkaBrandInstalled mBrandInstall;

    #endregion

    #region Enumeradores

    /// <summary>
    ///     Enumerador que contém os campos que podem ser lidos do arquivo XML,
    /// o qual contém informações da CLS (Utilizável a partir da versão 2018 R1).
    /// </summary>
    public enum e_SkaCamposClsXml
    {
        UserConfiguration,
        Ignore_Network_Dongle,
        Network_License_Server_Name,
        /// <summary>
        ///     Preference dongle, que determina que tipo de licença é:
        /// 19 - HardLock/HASP/USB
        /// 23 - Local/Standalone
        /// 24 - Rede/network
        /// 28 - Estudante/homework mode
        /// </summary>
        Preferred_Dongle,
        DiskID,
        NetworkAddress,
        NicName,
        LegacyKey,
        LicenseFileFolder,
        DongleType,
        DongleNumber,
        LastMessageUpdate,
        /// <summary>
        ///     Contém o número de licença do cliente (licença ativa)
        /// </summary>
        CurrentServerCode,
        /// <summary>
        ///     Contém o nome do cliente
        /// </summary>
        CurrentCustomerName,
        GracePeriod,
        UpdateExpiryDate,
        LicenseExpiryTime
    }

    /// <summary>
    ///     Enumerador que contém os campos possíveis de se obter os valores
    /// do registro de cada versão do Edgecam.
    /// </summary>
    public enum e_SkaCamposRegistro
    {
        AuxiliaryStrategyFolder,
        CodeGeneratorFolder,
        Default_Unit,
        Defaults_File,
        /// <summary>
        ///     Caminho contendo os pós-processadores padrões (Sample Mill Vertical, etc...)
        /// </summary>
        FeatureTemplateFolder,
        ImagesFolder,
        JobReportsFolder,
        MasterStrategyFiles,
        MillDefaultsFile,
        PCIIncludePath,
        SupportDefaultFolder,
        SupportProfilesFolder,
        /// <summary>
        ///     Arquivamento do Edgecam (gráficos de ferramentas, brutos, fixações, etc).
        /// </summary>
        Tool_Store_Directory,
        /// <summary>
        ///     Servidor SQL (String de conexão).
        /// </summary>
        Tool_Store_Server
    }

    /// <summary>
    ///     Define qual brand manager está instalado no computdor atual:
    /// se é uma versão da planit, da vero software ou da hexagon mi.
    /// </summary>
    private enum e_SkaBrandInstalled
    {
        Planit = 0,
        VeroSoftware = 1,
        Hexagon = 2,
        /// <summary>
        ///     Significa que não identificou uma versão instalada do Edgecam no computador corrente.
        /// </summary>
        None = 3
    }

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém a licença do cliente que deverá ser validada.
    /// </summary>
    public String _LicencaCliente
    {
        get
        {
            return mLicencaCliente;
        }
        set
        {
            mLicencaCliente = value;
        }
    }

    /// <summary>
    ///     Contém o número da licença ativa no computador do usuário corrente (o serial)
    /// (propriedade somente leitura).
    /// </summary>
    public String _LicencaAtiva
    {
        get
        {
            return mLicencaAtiva;
        }
    }

    /// <summary>
    ///     Contém a informação que determina se a licença do cliente é ou não válida
    /// (propriedade somente leitura).
    /// </summary>
    public Boolean _IsLicencaClienteValida
    {
        get
        {
            return mIsLicencaClienteValida;
        }
    }

    /// <summary>
    ///     Contém a última versão instalada do Edgecam no computador corrente (propriedade somente leitura).
    /// </summary>
    public String _UltimaVersaoInstalada
    {
        get
        {
            return mUltimaVersaoInstalada;
        }
    }

    /// <summary>
    ///     Contém o caminho de instalação do EdgeCAM.
    /// </summary>
    public String _CaminhoInstalacao
    {
        get
        {
            return mCaminhoInstalacao;
        }
    }

    /// <summary>
    ///     Contém o caminho do executável do Edgecam (propriedade somente leitura).
    /// </summary>
    public String _CaminhoExecutavel
    {
        get
        {
            return mCaminhoExecutavel;
        }
    }

    /// <summary>
    ///     Contém o caminho do arquivamento do Edgecam (Trabalhos, imagens dos trabalhos,
    /// gráficos de ferramentas, etc) (propriedade somente leitura).
    /// </summary>
    public String _CaminhoToolStore
    {
        get
        {
            return mCaminhoToolStore;
        }
    }

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instancia o objeto que permite utilizar as funções da classe. É possível
    /// também obter as informações (dados) da última versão instalada.
    /// </summary>
    /// <param name="BuscarUltVer">'True' para buscar as informações da última instalação</param>
    public Edgecam(Boolean BuscarUltVer = false)
    {
        try
        {
            if (BuscarUltVer)
                this.BuscaDadosUltimaVersao();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    /// <summary>
    ///     Instancia o objeto que permite utilizar as funções da classe e valida se a licença do cliente
    /// é válida. É possível também obter as informações (dados) da última versão instalada.
    /// </summary>
    /// <param name="LicencaCliente">Número da licença do cliente para verificar se é a mesma que está ativa no computador corrente</param>
    /// <param name="BuscarUltVer">'True' para buscar as informações da última instalação</param>
    public Edgecam(String LicencaCliente, Boolean BuscarUltVer = false)
    {
        try
        {
            mLicencaCliente = LicencaCliente;

            if (BuscarUltVer)
                this.BuscaDadosUltimaVersao();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    /// <summary>
    ///     Instancia o objeto que permite utilizar as funções da classe e valida se a licença do cliente
    /// é válida. É possível também obter as informações (dados) da última versão instalada.
    /// </summary>
    /// <param name="Versao">Versão específica à buscar os dados (Ex.: 2017.20, 2018.20, 2019.10)(</param>
    /// <param name="LicencaCliente">Número da licença do cliente para verificar se é a mesma que está ativa no computador corrente</param>
    /// <param name="BuscarUltVer">'True' para buscar as informações da última instalação</param>
    private Edgecam(String Versao, String LicencaCliente = "")
    {

    }

    ~Edgecam() { }

    #endregion

    #region Métodos privados

    /// <summary>
    ///     Método que monta o diretório das configurações da CLS (que antes eram salvas no regedit e agora são
    /// salvas em um XML dentro do %localappdata%) da licença do Edgecam.
    /// </summary>
    /// <returns>String contendo o caminho do XML</returns>
    private String GetCLS_XmlDirectory(String Version)
    {
        if (Convert.ToDouble(Version.Replace(".", ",")) >= Convert.ToDouble("2018,10") && Convert.ToDouble(Version.Replace(".", ",")) <= Convert.ToDouble("2020,0"))
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "vero software", Version, "Cls\\ClsUserConfig.xml");
        else if (Convert.ToDouble(Version.Replace(".", ",")) >= Convert.ToDouble("2020,1"))
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "hexagon\\Cls", Version, "ClsUserConfig.xml");
        else return "";
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    ///     Método que busca os dados da última versão do EdgeCAM instalada e atualiza
    /// os valores das propriedades.
    /// </summary>
    /// <remarks>Sempre irá buscar a última versão da caminho do regedit 'VERO SOFTWARE'</remarks>
    public void BuscaDadosUltimaVersao()
    {
        if (!this.EdgecamEstaInstalado())
            throw new EdgecamNotInstalledException("Nenhuma versão localizada", "EdgecamEstaInstalado");
        else
        {
            Microsoft.Win32.RegistryKey r = null, sr = null;

            if (mBrandInstall == e_SkaBrandInstalled.None)
                throw new EdgecamNotInstalledException("Não foi possível identificar o brand manager", "BuscaDadosUltimaVersao");

            //Carrega a última versão instalada de acordo com o produto rastreado no método 'EdgecamEstaInstalado'.
            switch (mBrandInstall)
            {
                case e_SkaBrandInstalled.Planit: r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(mRegistro_Planit); break;
                case e_SkaBrandInstalled.VeroSoftware: r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(mRegistro_Vero); break;
                case e_SkaBrandInstalled.Hexagon: r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(mRegistro_Hexagon); break;
            }

            String[] versoes = r.GetSubKeyNames();

            //Aqui obtém a última versão.
            var q = from string v in versoes orderby v descending where v.ToUpper() != "LAST INSTALLED" select v;
            mUltimaVersaoInstalada = q.ToList()[0];

            //Esse cara acessa a pasta de instalação e obtém o valor dele.
            sr = r.OpenSubKey(mUltimaVersaoInstalada + "\\Installation");

            //Aqui obtém a pasta de instalação do Edgecam (valor '(Padrão)' no regedit).
            mCaminhoInstalacao = (string)sr.GetValue("");

            if (mCaminhoInstalacao == "") throw new Exception("Caminho do Edgecam não encontrado");

            mCaminhoExecutavel = System.IO.Path.Combine(mCaminhoInstalacao, "cam\\Edgecam.exe");

            //Alimenta a variável 'mIsLicensaClienteValida' e 'mCaminhoToolStore'
            if (!String.IsNullOrEmpty(mLicencaCliente)) mIsLicencaClienteValida = this.IsLicencaClienteValida(mUltimaVersaoInstalada);

            mCaminhoToolStore = this.RetornaValorRegistro(mUltimaVersaoInstalada, e_SkaCamposRegistro.Tool_Store_Directory);
        }
    }

    /// <summary>
    ///     Método booelano que verifica nos registros do windows, se há uma versão qualquer do Edgecam instalada.
    /// </summary>
    /// <returns>'True' caso haver uma instalação válida.</returns>
    public Boolean EdgecamEstaInstalado()
    {
        Microsoft.Win32.RegistryKey r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(mRegistro_Vero);

        if (r == null)
        {
            //Verifica se não tem versão antiga instalada.
            r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(mRegistro_Planit);

            //if (r == null) return false;
            if (r == null)
            {
                //agora verifica se tem versão superior à 2020.1 instalado no computador.
                r = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(mRegistro_Hexagon);

                if (r == null)
                {
                    mBrandInstall = e_SkaBrandInstalled.None;
                    return false;
                }
                else
                {
                    mBrandInstall = e_SkaBrandInstalled.Hexagon;

                    string[] versoes = r.GetSubKeyNames();

                    if (versoes == null || versoes.Length == 0)
                        return false;
                    else return true;
                }
            }
            else
            {
                mBrandInstall = e_SkaBrandInstalled.Planit;

                string[] versoes = r.GetSubKeyNames();

                if (versoes == null || versoes.Length == 0)
                    return false;
                else return true;
            }
        }
        else
        {
            mBrandInstall = e_SkaBrandInstalled.VeroSoftware;

            string[] versoes = r.GetSubKeyNames();

            if (versoes == null || versoes.Length == 0)
                return false;
            else return true;
        }
    }

    /// <summary>
    ///     Método que verifica (compara) se a licença do cliente informada é a mesma que está instalada no computador cliente.
    /// </summary>
    /// <returns>True caso a licença informada e a licença ativa forem iguais (idênticas)</returns>
    public Boolean IsLicencaClienteValida(String Version)
    {
        //Pesquisa na planit
        if (Convert.ToDouble(Version.Replace(".", ",")) <= Convert.ToDouble("2015,10"))
        {
            mLicencaAtiva = (String)Microsoft.Win32.Registry.GetValue(String.Format(@"HKEY_CURRENT_USER\SOFTWARE\Planit\Cls\{0}\Settings", Version), "CurrentServerCode", "");
        }

        //Pesquisa na vero
        else if (Convert.ToDouble(Version.Replace(".", ",")) > Convert.ToDouble("2015,10") && Convert.ToDouble(Version.Replace(".", ",")) < Convert.ToDouble("2018,1"))
        {
            mLicencaAtiva = (String)Microsoft.Win32.Registry.GetValue(String.Format(@"HKEY_CURRENT_USER\SOFTWARE\Vero Software\Cls\{0}\Settings", Version), "CurrentServerCode", "");
        }
        //  A partir da versão 2018 R1, o local mudou, não fica mais salvo nos registros do windows, e sim em um XML
        //que pode ser localizado em: C:\Users\dionei\AppData\Local\vero software\2018.20\CLS\ClsUserConfig.xml
        else
        {
            //String dirXml = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "vero software", mUltimaVersaoInstalada, "Cls\\ClsUserConfig.xml");
            //mLicencaAtiva = this.LeConfiguracaoClsXml(dirXml, e_SkaCamposClsXml.CurrentServerCode);
            mLicencaAtiva = this.LeConfiguracaoClsXml(this.GetCLS_XmlDirectory(Version), e_SkaCamposClsXml.CurrentServerCode);
        }

        if (mLicencaAtiva.ToUpper().Trim() != (String.IsNullOrEmpty(mLicencaCliente) ? "" : mLicencaCliente).ToUpper().Trim().Replace("-", "")) return false;
        else return true;
    }

    /// <summary>
    ///     Método responsável por ler o arquivo 'ClsUserConfig.xml' para obter os valores
    /// de atributos específicos da licença do cliente (utilizar para versões maiores que
    /// 2018 R1, pois nas anteriores essas informações ficavam armazenadas no REGEDIT de
    /// cada computador).
    /// </summary>
    /// <returns>String contendo o valor, ou vazio caso: o arquivo não existir e em caso de exceção.</returns>
    public String LeConfiguracaoClsXml(String CaminhoXml, e_SkaCamposClsXml Campo)
    {
        try
        {
            if (!System.IO.File.Exists(CaminhoXml))
            {
                return "";
            }

            XDocument doc = XDocument.Load(CaminhoXml, LoadOptions.None);

            var tmp = from s in doc.Root.Descendants(Campo.ToString().Replace("_", "-")) select (string)s.Value;

            string strTmp = tmp.Select(x => x.ToString()).FirstOrDefault();

            return String.IsNullOrEmpty(strTmp) ? "<None>" : strTmp;
        }
        catch { return ""; }
    }

    /// <summary>
    ///     Método responsável por consultar os registros do windows e retornar os valores configurados
    /// na versão informada pelo usuário.
    /// </summary>
    /// <param name="Versao">Versão do Edgecam (Ex.: 2017.10, 2018.20, 2019.10, etc)</param>
    /// <param name="CampoRetorno">Campo que deverá ser buscado o valor</param>
    /// <returns>String contendo vazio (caso não tenha encontrado uma instalação válida) ou o valor encontrado nos registros</returns>
    public String RetornaValorRegistro(String Versao, e_SkaCamposRegistro CampoRetorno)
    {
        String chaveRegistro = "", caminhoPastaWindows = "";

        //Adicionado o suporte a versão 2020.0 ou superior (2020.1)
        if (!Versao.ToUpper().Trim().EndsWith(".10") && !Versao.ToUpper().Trim().EndsWith(".20") && !Versao.ToUpper().Trim().EndsWith(".0") && !Versao.ToUpper().Trim().EndsWith(".1"))
            throw new ArgumentOutOfRangeException("A versão só pode terminar em '.0', '.1', '.10' ou '.20'.");

        if (!String.IsNullOrEmpty(Versao))
        {
            //Pesquisa na planit
            if (Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2015,10"))
            {
                chaveRegistro = String.Format("HKEY_CURRENT_USER\\SOFTWARE\\Planit\\Edgecam\\{0}\\Location", Versao);

                if (CampoRetorno == e_SkaCamposRegistro.Tool_Store_Directory)
                    caminhoPastaWindows = "{0}\\Planit\\{1}\\Edgecam\\cam\\TStore";

                if (CampoRetorno == e_SkaCamposRegistro.FeatureTemplateFolder)
                    caminhoPastaWindows = "{0}\\Planit\\{1}\\Edgecam\\cam\\Machdef";
            }
            //Pesquisa na vero
            else if (Convert.ToDouble(Versao.Replace(".", ",")) > Convert.ToDouble("2015,10") && Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2020,0"))
            {
                chaveRegistro = String.Format("HKEY_CURRENT_USER\\SOFTWARE\\Vero Software\\Edgecam\\{0}\\Location", Versao);

                if (CampoRetorno == e_SkaCamposRegistro.Tool_Store_Directory)
                    caminhoPastaWindows = "{0}\\Vero Software\\{1}\\Edgecam\\cam\\TStore";

                if (CampoRetorno == e_SkaCamposRegistro.FeatureTemplateFolder)
                    caminhoPastaWindows = "{0}\\Vero Software\\{1}\\Edgecam\\cam\\Machdef";
            }
            else if (Convert.ToDouble(Versao.Replace(".", ",")) >= Convert.ToDouble("2020,1"))
            {
                chaveRegistro = String.Format("HKEY_CURRENT_USER\\SOFTWARE\\Hexagon\\Edgecam\\{0}\\Location", Versao);

                if (CampoRetorno == e_SkaCamposRegistro.Tool_Store_Directory)
                    caminhoPastaWindows = "{0}\\Hexagon\\Edgecam\\{1}\\cam\\TStore";

                if (CampoRetorno == e_SkaCamposRegistro.FeatureTemplateFolder)
                    caminhoPastaWindows = "{0}\\Hexagon\\Edgecam\\{1}\\cam\\Machdef";
            }

            String strValorRegistro = (string)Microsoft.Win32.Registry.GetValue(chaveRegistro, CampoRetorno.ToString().Replace("_", " "), "");

            if (String.IsNullOrEmpty(strValorRegistro)) return "";
            else
            {
                //  Tive de adicionar essa condição abaixo, pois nessas variáveis no REGEDIT,
                //não há códigos que estão dentro do Switch case, só há valor!!!
                if (CampoRetorno == e_SkaCamposRegistro.Tool_Store_Server || CampoRetorno == e_SkaCamposRegistro.MillDefaultsFile ||
                    CampoRetorno == e_SkaCamposRegistro.Default_Unit || CampoRetorno == e_SkaCamposRegistro.Defaults_File)
                    return strValorRegistro;

                switch (strValorRegistro.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries)[0])
                {
                    case "<NETWORKLOCATION>": throw new Exception("O sistema não suporta arquivos em ambiente de rede");
                    case "<USER>": return String.Format(caminhoPastaWindows, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Versao);
                    case "<PUBLIC>": return String.Format(caminhoPastaWindows, Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), Versao);
                    case "C:": return strValorRegistro;
                    default: return strValorRegistro;
                }
            }
        }

        return "";
    }

    /// <summary>
    ///     Método que verifica se o EdgeCAM está utilizando o módulo de estudo.
    /// </summary>
    /// <param name="Versao">Versão do Edgecam (Ex.: 2017.10, 2018.20, 2019.10, etc)</param>
    /// <returns>True determina que o módulo de estudo está ativo.</returns>
    public Boolean IsHomeworkMode(String Versao)
    {
        String chaveRegistro = "", tmpValue = "";

        if (!Versao.ToUpper().Trim().EndsWith(".10") && !Versao.ToUpper().Trim().EndsWith(".20") && !Versao.ToUpper().Trim().EndsWith(".0") && !Versao.ToUpper().Trim().EndsWith(".1"))
            throw new ArgumentOutOfRangeException("A versão só pode terminar em '.0', '.1', '.10' ou '.20'.");

        if (!String.IsNullOrEmpty(Versao))
        {
            //Pesquisa na planit
            if (Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2015,10"))
            {
                chaveRegistro = String.Format("HKEY_CURRENT_USER\\SOFTWARE\\Planit\\CLS\\{0}\\License", Versao);
                tmpValue = (string)Microsoft.Win32.Registry.GetValue(chaveRegistro, "Preferred Dongle", "");

                if (tmpValue == null) return false;
                else if (tmpValue != null)
                {
                    switch (Convert.ToInt16(tmpValue))
                    {
                        case 28: return true;
                        default: return false;
                    }
                }
            }
            //Pesquisa na vero software
            else if (Convert.ToDouble(Versao) > Convert.ToDouble("2015.10") && Convert.ToDouble(Versao) < Convert.ToDouble("2018.10"))
            {
                chaveRegistro = String.Format("HKEY_CURRENT_USER\\SOFTWARE\\Vero Software\\CLS\\{0}\\License", Versao);
                tmpValue = (string)Microsoft.Win32.Registry.GetValue(chaveRegistro, "Preferred Dongle", "");

                if (tmpValue == null) return false;
                else if (tmpValue != null)
                {
                    switch (Convert.ToInt16(tmpValue))
                    {
                        case 28: return true;
                        default: return false;
                    }
                }
            }
            //  A partir da versão 2018 R1, o local mudou, não fica mais salvo nos registros do windows, e sim em um XML
            //que pode ser localizado em: C:\Users\dionei\AppData\Local\vero software\2018.20\CLS\ClsUserConfig.xml
            else
            {
                //string dirXml = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "vero software", Versao, "Cls\\ClsUserConfig.xml");
                //tmpValue = this.LeConfiguracaoClsXml(dirXml, e_SkaCamposClsXml.Preferred_Dongle);

                tmpValue = this.LeConfiguracaoClsXml(this.GetCLS_XmlDirectory(Versao), e_SkaCamposClsXml.Preferred_Dongle);

                if (tmpValue == null) return false;
                else if (tmpValue != null)
                {
                    switch (Convert.ToInt16(tmpValue))
                    {
                        case 28: return true;
                        default: return false;
                    }
                }
            }
        }


        return false;
    }

    /// <summary>
    ///     Método que mata os processos do Edgecam em execução.
    /// </summary>
    public void MatarProcessoEdgecam()
    {
        if (this.IsEdgecamRunning())
        {
            System.Diagnostics.Process[] EcProcess = System.Diagnostics.Process.GetProcessesByName("Edgecam");

            foreach (System.Diagnostics.Process p in EcProcess)
            {
                p.Kill();
            }
        }
    }

    /// <summary>
    ///     Método booleano que identifica se o edgecam está sendo executado no computador cliente
    /// a partir do processo do Edgecam.
    /// </summary>
    /// <returns>True caso estiver algum processo ativo e 'false' caso contrário.</returns>
    public Boolean IsEdgecamRunning()
    {
        System.Diagnostics.Process[] EdegcamProcess = System.Diagnostics.Process.GetProcessesByName("Edgecam");

        if (EdegcamProcess.Length > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    ///     Método que inicia uma nova instância do Edgecam e manda o mesmo executar um(ns) script(s)
    /// logo após a sua abertura e espera ele concluir (caso o script tenha uma função de 'matar' o 
    /// Edgecam após sua execução) ou o usuário fechar o sistema para continuar com o processo.
    /// </summary>
    /// <param name="EsperarFechamento">True para esperar o edgecam fechar, false para simplesmente abrir e sair do método.</param>
    /// <param name="Argumentos">Argumentos (macros por exemplo) para abrir dentro do Edgecam</param>
    public void AbrirEdgecam(Boolean EsperarFechamento, params String[] Argumentos)
    {
        System.Diagnostics.Process ecProcess;

        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
        ecProcess = new System.Diagnostics.Process();

        if (!String.IsNullOrEmpty(mCaminhoExecutavel))
            psi.FileName = mCaminhoExecutavel;

        if (Argumentos.Count() > 0)
        {
            foreach (String s in Argumentos)
            {
                psi.Arguments = s;
            }
        }

        ecProcess.StartInfo = psi;
        ecProcess.Start();

        if (EsperarFechamento)
        {
            while (!ecProcess.HasExited)
            {

                System.Threading.Thread.Sleep(100);
                System.Windows.Forms.Application.DoEvents();

            }
        }
    }

    /// <summary>
    ///     Método que retornar uma string de conexão do banco de dados SQL ativo no Edgecam de acordo com a versão
    /// informada como parâmetro.
    /// </summary>
    /// <param name="Versao">Versão do Edgecam a ser iniciado uma nova instância. Ex.: 2017.10</param>
    /// <returns>A string de retorno já formatada para utlizar em conexões SQL com banco de dados.</returns>
    public String ObtemStringConexaoSql(String Versao)
    {
        if (!Versao.ToUpper().Trim().EndsWith(".10") && !Versao.ToUpper().Trim().EndsWith(".20") && !Versao.ToUpper().Trim().EndsWith(".0") && !Versao.ToUpper().Trim().EndsWith(".1"))
            throw new ArgumentOutOfRangeException("A versão só pode terminar em '.0', '.1', '.10' ou '.20'.");

        String strRetCn = "Data Source=@SERVIDOR;Initial Catalog=@BANCO;User ID=@USER;Password=@PASSWORD;";

        try
        {
            String[] tmp = this.RetornaValorRegistro(Versao, e_SkaCamposRegistro.Tool_Store_Server).Split(new char[] { ';' });

            //Para instâncias locais.
            if (tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "DATA SOURCE").Count() > 0)
                strRetCn = strRetCn.Replace("@SERVIDOR", tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "DATA SOURCE").Select(y => y.ToUpper().Split(new char[] { '=' })[1]).FirstOrDefault());

            //Para instâncias em servidores.
            if (tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "SERVER").Count() > 0)
                strRetCn = strRetCn.Replace("@SERVIDOR", tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "SERVER").Select(y => y.ToUpper().Split(new char[] { '=' })[1]).FirstOrDefault());

            strRetCn = strRetCn.Replace("@BANCO", tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "DATABASE").Select(y => y.Split(new char[] { '=' })[1]).FirstOrDefault());
            strRetCn = strRetCn.Replace("@USER", tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "USER ID").Select(y => y.Split(new char[] { '=' })[1]).FirstOrDefault());
            strRetCn = strRetCn.Replace("@PASSWORD", tmp.Where(x => x.ToUpper().Split(new char[] { '=' })[0] == "PASSWORD").Select(y => y.Split(new char[] { '=' })[1]).FirstOrDefault());

            return strRetCn;
        }
        catch { return ""; }
    }

    /// <summary>
    ///     Lista contendo as máquinas instaladas no Edgecam, cujo o diretório de leitura é tomado com base
    /// nas definições impostas pelo usuário em configurações gerais do sistema Edgecam.
    ///     O local das máquinas é identificado pelo 'editor de registros do Windows'.
    /// </summary>
    /// <param name="Versao">Versão que deve ser efetuada a busca no 'editor de registros do Windows'. Ex.: 2017.10, 2016.20 </param>
    /// <param name="IncluirMaquinasPadrao"></param>
    /// <returns>Retorna uma lista com as máquinas instaladas, sendo elas de torneamento ou fresamento.</returns>
    /// <remarks>Essa lista retorna uma 'ArgumentException' quando o caminho 'FeatureTemplateFolder' não existir
    /// no registro do Windows. Isso siginifica que o Edgecam nunca foi aberto.</remarks>
    public List<String> LstMaquinasInstaladas(String Versao, Boolean IncluirMaquinasPadrao = false)
    {
        if (!Versao.ToUpper().Trim().EndsWith(".10") && !Versao.ToUpper().Trim().EndsWith(".20") && !Versao.ToUpper().Trim().EndsWith(".0") && !Versao.ToUpper().Trim().EndsWith(".1"))
            throw new ArgumentOutOfRangeException("A versão só pode terminar em '.0', '.1', '.10' ou '.20'.");

        List<String> lstTmp = new List<String>();
        String localMaquinas = "";

        String pastaInstalacaoEc = "";

        try
        {
            //Era da versão da planit, logo, a pasta de instalação muda.
            if (Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2015,10"))
            {
                pastaInstalacaoEc = String.Format(@"C:\Program Files\Planit\Edgecam {0} R{1}\cam\Machdef", Versao.Split(new char[] { '.' })[0], Versao.Split(new char[] { '.' })[1].Replace("0", ""));
            }
            else if (Convert.ToDouble(Versao.Replace(".", ",")) > Convert.ToDouble("2015,10") && Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2020,0"))
            {
                pastaInstalacaoEc = String.Format(@"C:\Program Files\Vero Software\Edgecam {0} R{1}\cam\Machdef", Versao.Split(new char[] { '.' })[0], Versao.Split(new char[] { '.' })[1].Replace("0", ""));
            }
            //Versão 2020 mudou.
            else if (Convert.ToDouble(Versao.Replace(".", ",")) > Convert.ToDouble("2020,00"))
            {
                pastaInstalacaoEc = String.Format(@"C:\Program Files\Hexagon\Edgecam {0}\cam\Machdef", Versao);
            }


            //O método abaixo já devolve o caminho correto!!!
            localMaquinas = this.RetornaValorRegistro(Versao, e_SkaCamposRegistro.FeatureTemplateFolder);

            if (String.IsNullOrEmpty(localMaquinas))
                throw new ArgumentException(string.Format("Certifique-se que a versão '{0}' do Edgecam foi aberto ao menos uma vez.", Versao));

            System.IO.DirectoryInfo diretorioMqns = new System.IO.DirectoryInfo(localMaquinas);
            System.IO.FileInfo[] maquinas = diretorioMqns.GetFiles("*.mcp");//mcp - Fresamento | tcp - Torneamento

            //Adiciono à lista, os pós-processadores de Mill.
            if (maquinas.Count() > 0)
                for (int i = 0; i <= maquinas.Length - 1; i++)
                    lstTmp.Add(maquinas.GetValue(i).ToString().ToLower());//Precisa ser caixa baixa, pq se for adicionar no Edgecam, haverá problemas.

            maquinas = diretorioMqns.GetFiles("*.tcp");
            //Adiciono à lista, os pós-processadores de Turn.
            if (maquinas.Count() > 0)
                for (int i = 0; i <= maquinas.Length - 1; i++)
                    lstTmp.Add(maquinas.GetValue(i).ToString().ToLower());//Precisa ser caixa baixa, pq se for adicionar no Edgecam, haverá problemas.

            if (IncluirMaquinasPadrao)
            {
                diretorioMqns = new System.IO.DirectoryInfo(pastaInstalacaoEc);
                maquinas = diretorioMqns.GetFiles("*.mcp");

                if (maquinas.Count() > 0)
                    for (int i = 0; i <= maquinas.Length - 1; i++)
                        lstTmp.Add(maquinas.GetValue(i).ToString().ToLower());

                maquinas = diretorioMqns.GetFiles("*.tcp");
                if (maquinas.Count() > 0)
                    for (int i = 0; i <= maquinas.Length - 1; i++)
                        lstTmp.Add(maquinas.GetValue(i).ToString().ToLower());
            }

            var q = from f in lstTmp orderby f select f;

            return q.AsEnumerable().ToList();
        }
        catch { return null; }
    }

    /// <summary>
    ///     Contém uma lista dos últimos arquivos abertos dentro da versão especificada do Edgecam.
    /// </summary>
    /// <param name="Versao">Versão do Edgecam. Ex.: 2018.20, 2017.10, etc</param>
    /// <returns>Lista de string contendo o caminho completo dos arquivos.</returns>
    public List<String> LstUltimosArquivosAbertos(String Versao)
    {
        if (!Versao.ToUpper().Trim().EndsWith(".10") && !Versao.ToUpper().Trim().EndsWith(".20") && !Versao.ToUpper().Trim().EndsWith(".0") && !Versao.ToUpper().Trim().EndsWith(".1"))
            throw new ArgumentOutOfRangeException("A versão só pode terminar em '.0', '.1', '.10' ou '.20'.");

        List<String> ret = new List<String>();

        Microsoft.Win32.RegistryKey reg = null;

        if (Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2015,10"))
        {
            //Registros antigos (antiga planit)
            reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(String.Format("SOFTWARE\\PLANIT\\EDGECAM\\{0}\\CAM\\Recent File List", Versao));
        }
        else if (Convert.ToDouble(Versao.Replace(".", ",")) > Convert.ToDouble("2015,10") && Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2020,0"))
        {
            //Registros novos
            reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(String.Format("SOFTWARE\\VERO SOFTWARE\\EDGECAM\\{0}\\CAM\\Recent File List", Versao));
        }
        else if (Convert.ToDouble(Versao.Replace(".", ",")) >= Convert.ToDouble("2020,1"))
        {
            reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(String.Format("SOFTWARE\\HEXAGON\\EDGECAM\\{0}\\CAM\\Recent File List", Versao));
        }

        if (reg != null)
        {
            for (int x = 0; x < reg.GetValueNames().Count(); x++)
            {
                //registroVeroSo.GetValue("File1")
                ret.Add(reg.GetValue(reg.GetValueNames().ToList()[x]).ToString());
            }
        }

        return ret;
    }

    /// <summary>
    ///     Dicionário de dados que contém dados do banco de dados SQL ativo no Edgecam referente
    /// a versão informada, onde:
    /// Dado [0]: Provider
    /// Dado [1]: Server
    /// Dado [2]: Database
    /// Dado [3]: DataTypeCompatibility
    /// Dado [4]: User ID
    /// Dado [5]: Password
    /// </summary>
    /// <param name="Versao">Versão do Edgecam a ser iniciado uma nova instância. Ex.: 2017.10</param>
    /// <returns>Dicionário de dados contendo os dados de conexão do SQL.</returns>
    public Dictionary<String, String> GetActiveSqlDatabase(String Versao)
    {
        if (!Versao.ToUpper().Trim().EndsWith(".10") && !Versao.ToUpper().Trim().EndsWith(".20") && !Versao.ToUpper().Trim().EndsWith(".0") && !Versao.ToUpper().Trim().EndsWith(".1"))
            throw new ArgumentOutOfRangeException("A versão só pode terminar em '.0', '.1', '.10' ou '.20'.");

        Dictionary<String, String> dic = new Dictionary<String, String>();

        //string strToolStoreData = (string)Microsoft.Win32.Registry.GetValue(String.Format("HKEY_CURRENT_USER\\SOFTWARE\\Vero Software\\Edgecam\\{0}\\Location", Versao), "Tool Store Server", "");
        String strToolStoreData = "";

        if (Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2015,10"))
            strToolStoreData = (string)Microsoft.Win32.Registry.GetValue(String.Format("HKEY_CURRENT_USER\\SOFTWARE\\planit\\Edgecam\\{0}\\Location", Versao), "Tool Store Server", "");
        else if (Convert.ToDouble(Versao.Replace(".", ",")) >= Convert.ToDouble("2018,10") && Convert.ToDouble(Versao.Replace(".", ",")) <= Convert.ToDouble("2020,0"))
            strToolStoreData = (string)Microsoft.Win32.Registry.GetValue(String.Format("HKEY_CURRENT_USER\\SOFTWARE\\vero software\\Edgecam\\{0}\\Location", Versao), "Tool Store Server", "");
        else if (Convert.ToDouble(Versao.Replace(".", ",")) >= Convert.ToDouble("2020,1"))
            strToolStoreData = (string)Microsoft.Win32.Registry.GetValue(String.Format("HKEY_CURRENT_USER\\SOFTWARE\\hexagon\\Edgecam\\{0}\\Location", Versao), "Tool Store Server", "");

        char[] sep = { ';' };
        char[] sep2 = { '=' };
        string[] content = strToolStoreData.Split(sep);

        foreach (string s in content)
        {
            if (!String.IsNullOrEmpty(s) && s != "") dic.Add(s.Split(sep2)[0], s.Split(sep2)[1]);
        }

        return dic;
    }

    #endregion

    #region Métodos estáticos

    /// <summary>
    ///     Método que retorna uma lista de string contendo todas as versões do EdgeCAM
    /// instaladas no compputador corrente.
    /// </summary>
    /// <returns>Lista de string</returns>
    /// <remarks>Caso encontrar alguma excessão, a lista irá retornar null</remarks>
    public static List<String> LstVersoesInstaladas()
    {
        List<string> lstTmp = new List<string>();

        try
        {
            Microsoft.Win32.RegistryKey registroPlanit = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\PLANIT\\EDGECAM");
            Microsoft.Win32.RegistryKey registroVeroSo = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\VERO SOFTWARE\\EDGECAM");
            Microsoft.Win32.RegistryKey registroHexago = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\HEXAGON\\EDGECAM");

            if (registroPlanit != null) lstTmp.AddRange(registroPlanit.GetSubKeyNames().ToList());
            if (registroVeroSo != null) lstTmp.AddRange(registroVeroSo.GetSubKeyNames().ToList());
            if (registroVeroSo != null) lstTmp.AddRange(registroHexago.GetSubKeyNames().ToList());

            return lstTmp;

        }
        catch { return null; }
    }

    /// <summary>
    ///     Método que retorna uma lista de objeto contendo todas as extensões suportadas
    /// pelo EdgeCAM.
    /// </summary>
    /// <returns>Lista de objetos do tipo 'EdgecamExtensions'</returns>
    public static List<EdgecamExtensions> LstExtensoesValidasSolidos()
    {
        List<EdgecamExtensions> lst = new List<EdgecamExtensions>();

        lst.Add(new EdgecamExtensions() { Extensao = ".pmod", Descricao = "Arquivos do part modeler" });
        lst.Add(new EdgecamExtensions() { Extensao = ".a3mod", Descricao = "Arquivos do part modeler" });
        lst.Add(new EdgecamExtensions() { Extensao = ".v_t", Descricao = "Arquivos vero transport" });
        lst.Add(new EdgecamExtensions() { Extensao = ".wkf", Descricao = "Arquivos visi" });
        lst.Add(new EdgecamExtensions() { Extensao = ".CATPart", Descricao = "Arquivos do catia v5" });
        lst.Add(new EdgecamExtensions() { Extensao = ".CATProduct", Descricao = "Arquivos do catia v5" });
        lst.Add(new EdgecamExtensions() { Extensao = ".prt", Descricao = "Arquivos creo" });
        lst.Add(new EdgecamExtensions() { Extensao = ".asm", Descricao = "Arquivos creo" });
        lst.Add(new EdgecamExtensions() { Extensao = ".ipt", Descricao = "Arquivos do inventor" });
        lst.Add(new EdgecamExtensions() { Extensao = ".iam", Descricao = "Arquivos do inventor" });
        lst.Add(new EdgecamExtensions() { Extensao = ".a3d", Descricao = "Arquivos kompas" });
        lst.Add(new EdgecamExtensions() { Extensao = ".m3d", Descricao = "Arquivos kompas" });
        lst.Add(new EdgecamExtensions() { Extensao = ".par", Descricao = "Arquivos solid edge" });
        lst.Add(new EdgecamExtensions() { Extensao = ".asm", Descricao = "Arquivos solid edge" });
        lst.Add(new EdgecamExtensions() { Extensao = ".sldprt", Descricao = "Arquivos solidworks" });
        lst.Add(new EdgecamExtensions() { Extensao = ".prt", Descricao = "Arquivos solidworks" });
        lst.Add(new EdgecamExtensions() { Extensao = ".scdoc", Descricao = "Arquivos spaceclaim" });
        lst.Add(new EdgecamExtensions() { Extensao = ".prt", Descricao = "Arquivos siemens nx" });
        lst.Add(new EdgecamExtensions() { Extensao = ".jt", Descricao = "Arquivos jt" });
        lst.Add(new EdgecamExtensions() { Extensao = ".sat", Descricao = "Arquivos acis" });
        lst.Add(new EdgecamExtensions() { Extensao = ".sab", Descricao = "Arquivos acis" });
        lst.Add(new EdgecamExtensions() { Extensao = ".x_t", Descricao = "Arquivos parasolid" });
        lst.Add(new EdgecamExtensions() { Extensao = ".x_b", Descricao = "Arquivos parasolid" });
        lst.Add(new EdgecamExtensions() { Extensao = ".xmt_txt", Descricao = "Arquivos parasolid" });
        lst.Add(new EdgecamExtensions() { Extensao = ".xmt_bin", Descricao = "Arquivos parasolidparasolid" });
        lst.Add(new EdgecamExtensions() { Extensao = ".xmt", Descricao = "Arquivos parasolid" });
        lst.Add(new EdgecamExtensions() { Extensao = ".xmb", Descricao = "Arquivos parasolid" });
        lst.Add(new EdgecamExtensions() { Extensao = ".step", Descricao = "Arquivos step" });
        lst.Add(new EdgecamExtensions() { Extensao = ".stp", Descricao = "Arquivos step" });
        lst.Add(new EdgecamExtensions() { Extensao = ".igs", Descricao = "Arquivos iges" });
        lst.Add(new EdgecamExtensions() { Extensao = ".iges", Descricao = "Arquivos iges" });
        lst.Add(new EdgecamExtensions() { Extensao = ".stl", Descricao = "Arquivos stl" });
        lst.Add(new EdgecamExtensions() { Extensao = ".dxf", Descricao = "Arquivos de desenho do autocad" });
        lst.Add(new EdgecamExtensions() { Extensao = ".dwg", Descricao = "Arquivos do autocad" });
        lst.Add(new EdgecamExtensions() { Extensao = ".vda", Descricao = "Arquivos vda" });

        return lst;
    }

    /// <summary>
    ///     Método que convert os caracteres em ASCII (Base legada de trabalhos do Edgecam)
    /// em seus caracteres reais. Caso não contenha nenhum código em ASCII, o método retorna
    /// o valor recebido por parâmetro.
    /// </summary>
    /// <param name="Texto">String que deverá verificar os caracteres</param>
    /// <returns>String purificada, sem os caracteres em ASCII.</returns>
    public static String ConvertAscII_ToString(String Texto)
    {
        String strRet = Texto;

        if (Texto.Contains('{') || Texto.Contains('}'))
        {
            //Pega todos os códigos em ASCII da string
            List<String> numAscii = Texto.Split('{', '}').Where(s => !String.IsNullOrEmpty(s) && s.ToCharArray().Count() <= 2).ToList();

            for (int x = 0; x < numAscii.Count; x++)
            {
                //  Caso não for um número, cairá em uma execeção quando tentar uma conversão de texto para int
                //e irá ignorar o item.
                try
                {
                    strRet = strRet.Replace(String.Format("{0}{1}{2}", "{", Convert.ToInt16(numAscii[x].Replace("{", "").Replace("}", "")), "}"), Convert.ToChar(Convert.ToInt16(numAscii[x].Replace("{", "").Replace("}", ""))).ToString());
                }
                catch { continue; }
            }
            return strRet;
        }
        else return Texto;
    }

    /// <summary>
    ///     Método que converte uma imagem em um bytearray para inserir no
    /// banco de dados do Edgecam.
    /// </summary>
    /// <param name="CaminhoImagem">Caminho da imagem física</param>
    /// <returns>Byte array contendo um buffer da imagem.</returns>
    public static byte[] ConverteImagem_ToByteArray(String CaminhoImagem)
    {
        try
        {
            if (!String.IsNullOrEmpty(CaminhoImagem) && System.IO.File.Exists(CaminhoImagem))
            {
                System.IO.FileStream fStreamImgUnk = System.IO.File.OpenRead(CaminhoImagem);
                byte[] imgBuffer = new byte[fStreamImgUnk.Length];
                fStreamImgUnk.Read(imgBuffer, 0, imgBuffer.Length);

                return imgBuffer;
            }
            else return null;
        }
        catch { return null; }
    }

    #endregion
}

/// <summary>
///     Classe auxiliar na classe 'SkaEdgecam', no método estático 'LstExtensoesValidasSolidos'
/// </summary>
public class EdgecamExtensions
{
    /// <summary>
    ///     Extensão do arquivo
    /// </summary>
    public String Extensao { get; set; }

    /// <summary>
    ///     Breve descrição do arquivo.
    /// </summary>
    public String Descricao { get; set; }
}

/// <summary>
///     Classe serializável para exceções dentro da classe do edgecam.
/// </summary>
[Serializable]
internal class EdgecamNotInstalledException : Exception
{
    public EdgecamNotInstalledException() { }

    public EdgecamNotInstalledException(String Version)
        : base(String.Format("Is was not possible to identify a valid installation of version '{0}'", Version))
    {

    }

    public EdgecamNotInstalledException(String InfoAux, String Method = "")
        : base(String.Format("Is was not possible to identify a valid installation of edgecam. Additional information from method '{0}': '{1}'", Method, InfoAux))
    {

    }
}