///***********************************************************************************************************
// * 
// * 
// *                  SwDocMgr - Classe para ler informações de componentes do SolidWorks 
// *                                      (sem o solidworks instalado)
// * 
// * 
// *      Developer:  Dionei Beilke dos Santos
// *      Function:   Classe para ler informações de componentes do SolidWorks (sem o solidworks instalado)
// *      Version:    1.1
// *      Date:       04/09/2018, at 05:07 PM
// *      Note:       <None>
// *      History:    Update      - 05/09/2018 - 04:45 PM - Primeira versão concluída - V1.0 Lançada
// *                  Update      - 06/09/2018 - 10:07 AM - Adicionado o método 'ObtemEstruturaMontagem' - V1.1 Lançada
// * 
// * 
// * 
// * 
// **********************************************************************************************************/

////TODO: PRECISA DESCOMENTAR ESSA PARTE E ARRUMAR A PARTE DO PDM

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using SolidWorks.Interop.swdocumentmgr;
//using System.IO;

///// <summary>
/////     Classe responsável por ler/validar informações dos componentes do SolidWorks
///// via 'document manager' (não precisa do SolidWorks instalado).
///// </summary>
//public class SkaSwDocMgr
//{

//    #region Licenças Document Manager

//    /// <summary>
//    ///     Licença do document manager da versão 2015.
//    /// </summary>
//    private String mLicenca2015 = "SkaAutomacaoDeEngenhariasLtdaNorth:swdocmgr_general-11785-02051-00064-01025-08442-34307-00007-42504-38812-30571-49063-06799-62510-49278-57344-16482-36631-62091-44901-00945-06963-23793-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-14725-51645-41425-14337-28768-54362-56514-25690-25184-1067";

//    /// <summary>
//    ///     Licença do document manager da versão 2016.
//    /// </summary>
//    private String mLicenca2016 = "SKA:swdocmgr_general-11785-02051-00064-17409-08473-34307-00007-46296-32134-50731-21156-45522-64048-28410-36868-47872-11222-51218-65326-12368-25770-24212-11596-00261-24632-23144-49882-23268-24676-27746-6";

//    /// <summary>
//    ///     Licença do document manager da versão 2017.
//    /// </summary>
//    private String mLicenca2017 = "SkaAutomacaoDeEngenhariasLtda:swdocmgr_general-11785-02051-00064-33793-08504-34307-00007-51120-17538-33536-13247-03710-20832-05764-06147-28397-37494-61339-47835-34924-16368-22785-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-27746-1,swdocmgr_previews-11785-02051-00064-33793-08504-34307-00007-02064-14273-52079-09273-11984-48333-07793-30726-04367-10415-46106-37906-18001-36227-22618-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-27746-3";

//    /// <summary>
//    ///     Licença do document manager da versão 2018.
//    /// </summary>
//    private String mLicenca2018 = "SkaAutomacaoDeEngenhariasLtda:swdocmgr_general-11785-02051-00064-50177-08535-34307-00007-00192-12120-55762-61915-46952-10792-35670-49156-09943-63791-36009-43019-59550-53016-23650-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-28258-5,swdocmgr_previews-11785-02051-00064-50177-08535-34307-00007-58488-38013-61035-06057-07087-05408-37005-49156-44830-39843-05033-21857-11842-11811-22765-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-28258-8,swdocmgr_dimxpert-11785-02051-00064-50177-08535-34307-00007-28432-50885-10266-12280-21969-19257-01637-24582-12749-02142-57407-05760-45789-23065-23619-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-28258-3,swdocmgr_geometry-11785-02051-00064-50177-08535-34307-00007-14608-53439-45834-65231-29455-24330-12634-26629-04815-21723-46529-23525-64993-23745-23051-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-28258-9,swdocmgr_xml-11785-02051-00064-50177-08535-34307-00007-23032-51949-12148-16491-46750-58665-63763-61445-53293-19356-24841-21529-19428-53512-22867-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-28258-9,swdocmgr_tessellation-11785-02051-00064-50177-08535-34307-00007-25536-34101-33249-38832-33187-31024-25729-28679-19472-23459-33622-05173-21911-31108-23272-44364-01413-53717-46525-36229-48517-38161-47381-38301-41401-51589-34213-12749-37329-00389-25656-23152-57052-23276-24676-28258-3";

//    #endregion

//    #region Variáveis Globais

//    /// <summary>
//    ///     Contém a versão atual para utilizar a documento manager.
//    /// </summary>
//    private e_SkaVersaoDc mVersao;

//    /// <summary>
//    ///     Contém o objeto para validar a licença da document manager.
//    /// </summary>
//    private SwDMClassFactory mSwClassFact;

//    /// <summary>
//    ///     Contém o objeto da document manager.
//    /// </summary>
//    private SwDMApplication4 mSwDocMgr;

//    /// <summary>
//    ///     Contém o documento do solidworks.
//    /// </summary>
//    private SwDMDocument18 mSwDocumento;

//    /// <summary>
//    ///     Contém o objeto que dá acesso as configurações do documento.
//    /// </summary>
//    private SwDMConfigurationMgr mSwConfigMgr;

//    /// <summary>
//    ///     Possuí o nome da configuração ativa do componente atual.
//    /// </summary>
//    private String mNomeConfiguracaoAtiva;

//    #endregion

//    #region Enumeradores

//    /// <summary>
//    ///     Versão da document manager.
//    /// </summary>
//    public enum e_SkaVersaoDc
//    {
//        V2015,
//        V2016,
//        V2017,
//        V2018
//    }

//    /// <summary>
//    ///     Enumerador que contém os tipos de peças do SolidWorks.
//    /// </summary>
//    public enum e_SkaTipoPeca
//    {
//        /// <summary>
//        ///     Peça
//        /// </summary>
//        SldPrt,
//        /// <summary>
//        ///     Montagem
//        /// </summary>
//        SldAsm,
//        /// <summary>
//        ///     Desenho
//        /// </summary>
//        SldDrw
//    }

//    /// <summary>
//    ///     Enumerador que contém o tipo de dado de saída da propriedade personalizada.
//    /// </summary>
//    public enum e_SkaTipoDado
//    {
//        /// <summary>
//        ///     SwDmCustomInfoType.swDmCustomInfoDate
//        /// </summary>
//        Data = 63,
//        /// <summary>
//        ///     SwDmCustomInfoType.swDmCustomInfoNumber
//        /// </summary>
//        Numero = 3,
//        /// <summary>
//        ///     SwDmCustomInfoType.swDmCustomInfoText
//        /// </summary>
//        Texto = 30,
//        /// <summary>
//        ///     SwDmCustomInfoType.swDmCustomInfoYesOrNo
//        /// </summary>
//        SimNao = 11,
//        /// <summary>
//        ///     SwDmCustomInfoType.swDmCustomInfoUnknown
//        /// </summary>
//        Desconhecido = 0
//    }

//    #endregion

//    #region Propriedades

//    /// <summary>
//    ///     Possuí o nome da configuração ativa do componente atual.
//    /// </summary>
//    public String _NomeConfiguracaoAtiva
//    {
//        get
//        {
//            return mNomeConfiguracaoAtiva;
//        }
//        set
//        {
//            mNomeConfiguracaoAtiva = value;
//        }
//    }

//    #endregion

//    #region Instância dos objetos da classe

//    /// <summary>
//    ///     Instância o objeto da classe.
//    /// </summary>
//    /// <param name="Versao">Versão da document manager para trabalhar.</param>
//    public SkaSwDocMgr(e_SkaVersaoDc Versao)
//    {
//        mVersao = Versao;

//        InstanciaObjetosDocumentManager();
//    }

//    /// <summary>
//    ///     Instância o objeto da classe e já carrega o componente nos objetos da
//    /// document manager.
//    /// </summary>
//    /// <param name="Versao">Versão da document manager para trabalhar.</param>
//    /// <param name="Componente">Caminho completo do componente (peça/montagem)</param>
//    public SkaSwDocMgr(e_SkaVersaoDc Versao, String Componente)
//    {
//        mVersao = Versao;

//        InstanciaObjetosDocumentManager();

//        CarregaPeca(Componente);
//    }

//    /// <summary>
//    ///     Método destrutor da classe.
//    /// </summary>
//    ~SkaSwDocMgr() { }

//    #endregion

//    #region Métodos

//    /// <summary>
//    ///     Método responsável por instanciar os objetos da document manager.
//    /// </summary>
//    private void InstanciaObjetosDocumentManager()
//    {
//        mSwClassFact = new SwDMClassFactory();
//        mSwDocMgr = (SwDMApplication4)mSwClassFact.GetApplication(DeterminaLicenca());
//        mSwConfigMgr = default(SwDMConfigurationMgr); ;
//    }

//    /// <summary>
//    ///     Método que determina a licença da document manager.
//    /// </summary>
//    /// <returns></returns>
//    private String DeterminaLicenca()
//    {
//        if (mVersao == e_SkaVersaoDc.V2015)
//            return mLicenca2015;
//        if (mVersao == e_SkaVersaoDc.V2016)
//            return mLicenca2016;
//        if (mVersao == e_SkaVersaoDc.V2017)
//            return mLicenca2017;
//        if (mVersao == e_SkaVersaoDc.V2018)
//            return mLicenca2018;
//        else return "";
//    }

//    /// <summary>
//    ///     Método que valida se a peça é valida para trabalhar com o Document manager.
//    /// </summary>
//    /// <param name="Componente">Nome ou caminho da peça à validar.</param>
//    /// <param name="Tipo">Tipo de arquivo que deve ser validado</param>
//    /// <returns>True caso estiver apta, false para o contrário.</returns>
//    public Boolean ValidaPeca(String Componente)
//    {
//        if (Componente.ToString().ToUpper().EndsWith(e_SkaTipoPeca.SldPrt.ToString().ToUpper())) return true;
//        if (Componente.ToString().ToUpper().EndsWith(e_SkaTipoPeca.SldAsm.ToString().ToUpper())) return true;
//        if (Componente.ToString().ToUpper().EndsWith(e_SkaTipoPeca.SldDrw.ToString().ToUpper())) return true;

//        return false;
//    }

//    /// <summary>
//    ///     Método responsável por interpretar a extensão do arquivo e devolve um enumerador com o tipo
//    /// de arquivo que o mesmo corresponde.
//    /// </summary>
//    /// <param name="Componente">Caminho completo do componente do solidworks (peça/montagem)</param>
//    /// <returns>SwDmDocumentType</returns>
//    public SwDmDocumentType TipoDocumento(String Componente)
//    {
//        if (Componente.ToUpper().EndsWith("SLDPRT"))
//            return SwDmDocumentType.swDmDocumentPart;
//        if (Componente.ToUpper().EndsWith("SLDASM"))
//            return SwDmDocumentType.swDmDocumentAssembly;
//        if (Componente.ToUpper().EndsWith("SLDDRW"))
//            return SwDmDocumentType.swDmDocumentDrawing;
//        else return SwDmDocumentType.swDmDocumentUnknown;
//    }

//    /// <summary>
//    ///     Método responsável por carregar a peça (instanciar os objetos da document manager)
//    /// e carregar os valores nas variáveis globais da classe.
//    /// </summary>
//    /// <param name="Componente">Caminho completo do componente (peça/montagem)</param>
//    /// <remarks>Não é permitido obter documentos com o estado somente leitura</remarks>
//    private void CarregaPeca(String Componente)
//    {
//        SwDmDocumentOpenError retValorErroAbrir = 0;

//        if (ValidaPeca(Componente))
//        {
//            mSwDocumento = (SwDMDocument18)mSwDocMgr.GetDocument(Componente, TipoDocumento(Componente), false, out retValorErroAbrir);

//            if ((int)retValorErroAbrir != 0)
//            {
//                //alguma coisa deu errado na tentativa de leitura das propriedades
//                switch (retValorErroAbrir)
//                {
//                    //1 = File failed to open; reasons could be related to permissions or the file is in use by some other application or the file does not exist
//                    case SwDmDocumentOpenError.swDmDocumentOpenErrorFail:
//                        throw new Exception(String.Format("Não foi possível carregar o componente '{0}', pode estar atrelado a permissões ou o arquivo está em uso por outra aplicação.", Componente));

//                    //2 - Non-SOLIDWORKS file was opened
//                    case SwDmDocumentOpenError.swDmDocumentOpenErrorNonSW:
//                        throw new Exception(String.Format("O componente '{0}' não é um arquivo SolidWorks.", Componente));

//                    //3 - File not found
//                    case SwDmDocumentOpenError.swDmDocumentOpenErrorFileNotFound:
//                        throw new Exception(String.Format("O componente '{0}' não foi localizado.", Componente));

//                    //4 - File is read only
//                    case SwDmDocumentOpenError.swDmDocumentOpenErrorFileReadOnly:
//                        throw new Exception(String.Format("O componente '{0}' é somente leitura, e este não é hábil para trabalhar.", Componente));

//                    //5 - No SOLIDWORKS Document Manager API license
//                    case SwDmDocumentOpenError.swDmDocumentOpenErrorNoLicense:
//                        throw new Exception("Não há licença da document manager disponível");

//                    //6 - File was created in a version of SOLIDWORKS more recent than the version attempting to open the file
//                    case SwDmDocumentOpenError.swDmDocumentOpenErrorFutureVersion:
//                        throw new Exception(String.Format("O componente '{0}' foi criado em uma versão futura do SolidWorks, a qual não é suportada pela API.", Componente));
//                }
//            }
//            else
//            {
//                //se for nulo, não há nenhuma propriedade personalizada.
//                if (mSwDocumento == null)
//                    throw new Exception(String.Format("O componente '{0}' não possuí nenhum tipo de propriedade atribuída/criada/customizada.", Componente));

//                //Recebe as configurações do documento
//                mSwConfigMgr = mSwDocumento.ConfigurationManager;
//            }
//        }
//    }

//    /// <summary>
//    ///     Obtém o valor de uma propriedade referênciada da aba 'Personalizado' do SolidWorks.
//    /// </summary>
//    /// <param name="Componente">Caminho completo do componente (peça/montagem) do solidworks</param>
//    /// <param name="NomeCampo">Nome da propriedades referênciada</param>
//    /// <param name="VerificarTipoCampo">True para verificar o tipo de propriedade (boolean, text, number, date)</param>
//    /// <param name="TipoDado">Tipo de dado que deve ser validado. Caso o dado for diferente, 'retorna vazio'</param>
//    /// <returns>Retorna o valor da propriedade</returns>
//    /// <remarks>Retorna vazio (sempre) caso o parâmetro 'VerificarTipoCampo' for igual a true e o tipo de dado
//    /// não for definido.</remarks>
//    public String ObtemPropriedadesPersonalizadas(String Componente, String NomeCampo, Boolean VerificarTipoCampo = false, e_SkaTipoDado TipoDado = e_SkaTipoDado.Desconhecido)
//    {
//        try
//        {
//            CarregaPeca(Componente);

//            _NomeConfiguracaoAtiva = mSwConfigMgr.GetActiveConfigurationName();

//            String[] cfgNames = (String[])mSwConfigMgr.GetConfigurationNames();

//            //Esse cara é responsável por obter as configurações da peça
//            SwDMConfiguration14 swCfg = default(SwDMConfiguration14);

//            //Contém o tipo de dado da propriedade (texto, data, inteiro, etc...)
//            SwDmCustomInfoType customInfoType = 0;

//            //Possuí a informação de link a outra propriedade personalizada (caso exista)
//            String linkedTo;

//            foreach (string vCfgName in cfgNames)
//            {
//                if (vCfgName.ToUpper().Trim() == _NomeConfiguracaoAtiva.ToUpper().Trim())
//                {
//                    swCfg = (SwDMConfiguration14)mSwConfigMgr.GetConfigurationByName(vCfgName);

//                    if (!VerificarTipoCampo)
//                        return swCfg.GetCustomPropertyValues(NomeCampo, out customInfoType, out linkedTo);
//                    else
//                    {
//                        String tmp = swCfg.GetCustomPropertyValues(NomeCampo, out customInfoType, out linkedTo);
//                        if ((int)customInfoType == (int)TipoDado)
//                            return tmp;
//                        else return "";
//                    }
//                }
//            }
//            return "";
//        }
//        catch { return ""; }
//    }

//    /// <summary>
//    ///     Obtém o valor de uma propriedade referênciada da aba 'Específico da configuração' do SolidWorks.
//    /// </summary>
//    /// <param name="Componente">Caminho completo do componente (peça/montagem) do solidworks</param>
//    /// <param name="NomeCampo">Nome da propriedades referênciada</param>
//    /// <param name="VerificarTipoCampo">True para verificar o tipo de propriedade (boolean, text, number, date)</param>
//    /// <param name="TipoDado">Tipo de dado que deve ser validado. Caso o dado for diferente, 'retorna vazio'</param>
//    /// <returns>Retorna o valor da propriedade</returns>
//    /// <remarks>Retorna vazio (sempre) caso o parâmetro 'VerificarTipoCampo' for igual a true e o tipo de dado
//    /// não for definido.</remarks>
//    public String ObtemPropriedadesEspecificas(String Componente, String NomeCampo, Boolean VerificarTipoCampo = false, e_SkaTipoDado TipoDado = e_SkaTipoDado.Desconhecido)
//    {
//        try
//        {
//            CarregaPeca(Componente);

//            _NomeConfiguracaoAtiva = mSwConfigMgr.GetActiveConfigurationName();

//            String[] cfgNames = (String[])mSwDocumento.GetCustomPropertyNames();

//            //Contém o tipo de dado da propriedade (texto, data, inteiro, etc...)
//            SwDmCustomInfoType customInfoType = 0;

//            if ((cfgNames == null && cfgNames.Count() == 0)) return "";

//            foreach (string cfg in cfgNames)
//            {
//                if (cfg.ToUpper() == NomeCampo.ToUpper()) return mSwDocumento.GetCustomProperty(cfg, out customInfoType).ToUpper().Trim();
//            }

//            return "";
//        }
//        catch { return ""; }
//    }

//    /// <summary>
//    ///     Método responsável por ler a estrutura de uma montagem e obter sua estrutura de filhos
//    /// (estes podem ser peças ou sub-montagens) e sub-filhos.
//    /// </summary>
//    /// <param name="Componente">Caminho completo do componente (peça/montagem) do solidworks</param>
//    /// <returns>Null em caso de alguma exceção. Lista de objetos caso tenha conseguido ler o documento.</returns>
//    public List<SkaSwStruct> ObtemEstruturaMontagem(String Componente)
//    {
//        try
//        {
//            List<SkaSwStruct> lstRef = new List<SkaSwStruct>();

//            //HELP LINK: http://help.solidworks.com/2016/english/api/swdocmgrapi/get_current_name_of_configuration_of_suppressed_component_example_csharp.htm
//            SwDmDocumentOpenError nRetVal = 0;

//            mSwDocumento = (SwDMDocument18)mSwDocMgr.GetDocument(Componente, TipoDocumento(Componente), false, out nRetVal);

//            //Isso aqui serve para consultar itens suprimidos na estrutura;
//            //SwDMExternalReferenceOption2 dmExtRefOption = swDocMgr.GetExternalReferenceOptionObject2();
//            //SwDMSearchOption dmSearchOpt = swDocMgr.GetSearchOptionObject();
//            //dmExtRefOption.SearchOption = dmSearchOpt;
//            //dmExtRefOption.Configuration = "Default";
//            //dmExtRefOption.NeedSuppress = false;
//            //Array arrExtRefs = (Array)(dmExtRefOption.ExternalReferences);

//            int nivel = 0;

//            PercoreEstrutura(mSwDocumento, mSwDocumento.FullName.Substring(mSwDocumento.FullName.LastIndexOf("\\") + 1), ref nivel, ref lstRef);

//            //System.Runtime.InteropServices.Marshal.ReleaseComObject(swClassFact);
//            //System.Runtime.InteropServices.Marshal.ReleaseComObject(swDocMgr);
//            System.Runtime.InteropServices.Marshal.ReleaseComObject(mSwDocumento);

//            return lstRef;
//        }
//        catch
//        {
//            return null;
//        }
//    }

//    /// <summary>
//    ///     Método que percorre uma estrutura de montagem buscando os seus componentes filhos e adiciona em uma
//    /// lista referenciada de objetos (método recursivo);
//    /// </summary>
//    /// <param name="SwDocPai">Objeto contendo o pai (montagem/sub-montagem)</param>
//    /// <param name="NomePai">Nome do pai desse objeto que está sendo herdado</param>
//    /// <param name="Nivel">(Incremental) Nível na estrutura (sub-montagem)</param>
//    /// <param name="LstEstrutura">Lista contendo a estrutura antiga, a qual só é adicionado novos elementos.</param>
//    private void PercoreEstrutura(SwDMDocument18 SwDocPai, String NomePai, ref int Nivel, ref List<SkaSwStruct> LstEstrutura)
//    {
//        //Sempre começar o nível em 1!!!
//        int NivelFilho = 1;

//        SwDmDocumentOpenError nRetVal = 0;

//        SkaSwStruct s = new SkaSwStruct
//        {
//            NomeComponente = SwDocPai.FullName.Substring(SwDocPai.FullName.LastIndexOf("\\") + 1),
//            CaminhoComponente = SwDocPai.FullName,
//            TipoComponente = SkaSwStruct.e_SkaTipoComponente.Montagem,
//            NomePai = NomePai,
//            ConfiguracaoAtiva = (SwDocPai.ConfigurationManager).GetActiveConfigurationName().ToString(),
//            Nivel = Nivel,
//            ExisteArqFisico = File.Exists((SwDocPai.ConfigurationManager).GetActiveConfigurationName().ToString())
//        };

//        LstEstrutura.Add(s);

//        SwDMConfiguration12 config;
//        SwDMConfigurationMgr configMgr = SwDocPai.ConfigurationManager;
//        config = (SwDMConfiguration12)configMgr.GetConfigurationByName(configMgr.GetActiveConfigurationName());
//        object comps = config.GetComponents();
//        Array arrComps = (Array)comps;

//        foreach (SwDMComponent9 swComp in arrComps)
//        {
//            SwDMExternalReferenceOption2 dmExtRefOption = mSwDocMgr.GetExternalReferenceOptionObject2();
//            SwDMSearchOption dmSearchOpt = mSwDocMgr.GetSearchOptionObject();
//            dmExtRefOption.SearchOption = dmSearchOpt;
//            dmExtRefOption.Configuration = swComp.ConfigurationName;

//            Array arrExtRefs = (Array)(dmExtRefOption.ExternalReferences);

//            string ComponentPathName = swComp.PathName;

//            //Check validity of the Component's path name
//            //It might be out of date if the path changed after
//            //the component was suppressed
//            if (!File.Exists(ComponentPathName) && arrExtRefs != null)
//            {
//                //If that file cannot be found, look for an external 
//                //reference with the same name
//                ComponentPathName = ProcuraCaminhoReferenciaExterna(swComp.PathName, arrExtRefs);

//                s = new SkaSwStruct
//                {
//                    NomeComponente = ComponentPathName.Substring(ComponentPathName.LastIndexOf("\\") + 1),
//                    CaminhoComponente = ComponentPathName,
//                    TipoComponente = SkaSwStruct.e_SkaTipoComponente.Peca,
//                    NomePai = SwDocPai.FullName.Substring(SwDocPai.FullName.LastIndexOf("\\") + 1),
//                    ConfiguracaoAtiva = swComp.ConfigurationName,
//                    Nivel = NivelFilho,
//                    ExisteArqFisico = false
//                };

//                LstEstrutura.Add(s);

//                //Continuo com o loop, pois não há porque prosseguir.
//                continue;
//            }

//            SwDMDocument18 swDoc2 = (SwDMDocument18)mSwDocMgr.GetDocument(ComponentPathName, TipoDocumento(ComponentPathName), false, out nRetVal);

//            //Se for montagem (sub-montagem), chama o método recursivo.
//            if (ComponentPathName.ToUpper().EndsWith("SLDASM"))
//            {
//                Nivel++;

//                PercoreEstrutura(swDoc2, SwDocPai.FullName.Substring(SwDocPai.FullName.LastIndexOf("\\") + 1), ref Nivel, ref LstEstrutura);

//                Nivel--;
//            }
//            //Se for filho (peça), adiciono o mesmo na estrutura!!
//            else if (ComponentPathName.ToUpper().EndsWith("SLDPRT"))
//            {
//                NivelFilho++;

//                s = new SkaSwStruct
//                {
//                    NomeComponente = ComponentPathName.Substring(ComponentPathName.LastIndexOf("\\") + 1),
//                    CaminhoComponente = ComponentPathName,
//                    TipoComponente = SkaSwStruct.e_SkaTipoComponente.Peca,
//                    NomePai = SwDocPai.FullName.Substring(SwDocPai.FullName.LastIndexOf("\\") + 1),
//                    ConfiguracaoAtiva = swComp.ConfigurationName,
//                    Nivel = NivelFilho,
//                    ExisteArqFisico = File.Exists(ComponentPathName)
//                };

//                LstEstrutura.Add(s);
//            }
//        }

//        //Decremento para sair do nível.
//        Nivel--;
//    }

//    private string ProcuraCaminhoReferenciaExterna(String name, Array arrComps)
//    {
//        try
//        {
//            string ExtrefPathName = name;
//            string[] nameParts = name.Split('\\');
//            string justName = nameParts.GetValue(nameParts.GetUpperBound(0)).ToString();

//            int i = arrComps.GetLowerBound(0);
//            Boolean found = false;
//            while ((i <= arrComps.GetUpperBound(0)) && !found)
//            {
//                string extref = (arrComps.GetValue(i)).ToString();
//                string[] extrefParts = extref.Split('\\');
//                string justextrefName = extrefParts.GetValue(extrefParts.GetUpperBound(0)).ToString();
//                if (justextrefName == justName)
//                {
//                    found = true;
//                    ExtrefPathName = extref;
//                }
//                i++;
//            }

//            return ExtrefPathName;
//        }
//        catch
//        {
//            return "";
//        }
//    }

//    #endregion
//}

///// <summary>
/////     Classe que representa a estrutura de uma montagem, que contém seus filhos
///// e sub-filhos.
///// </summary>
//public class SkaSwStruct
//{
//    public enum e_SkaTipoComponente
//    {
//        NA,
//        Peca,
//        Montagem
//    }

//    /// <summary>
//    ///     Nome do pai/filho/sub-filho
//    /// </summary>
//    public String NomeComponente { get; set; }

//    /// <summary>
//    ///     Contém o caminho completo do componente do solidworks.swDoc
//    /// </summary>
//    public String CaminhoComponente { get; set; }

//    /// <summary>
//    ///     Tipo de componente, se é peça ou montagem (sub-montagem)
//    /// </summary>
//    public e_SkaTipoComponente TipoComponente { get; set; }

//    /// <summary>
//    ///     Nome do pai desse item (Caso estiver vazio, significa que é o nível zero (0))
//    /// </summary>
//    public String NomePai { get; set; }

//    /// <summary>
//    ///     Nome da configuração ativa
//    /// </summary>
//    public String ConfiguracaoAtiva { get; set; }

//    /// <summary>
//    ///     Nível do item na estrutura (0, 1, 2, 2.2, 2.3, 2.4, etc...)
//    /// </summary>
//    public Int32 Nivel { get; set; }

//    /// <summary>
//    ///     True para caso o arquivo existir e false para o contrário.
//    /// </summary>
//    public Boolean ExisteArqFisico { get; set; }
//}