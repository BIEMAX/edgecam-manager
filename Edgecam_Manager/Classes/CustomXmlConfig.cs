/***********************************************************************************************************
 * 
 * 
 *                  CustomXmlConfig - Classe com métodos para criação de um arquivo XML de configuração
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Criar arquivo XML, adicionar variáveis/valores ao conteúdo do mesmo.
 *      Version:    4.1
 *      Date:       13/02/2017, at 08:31 AM
 *      Note(s):    Adiciona a seguinte linha em 'Build Events' da solution: copy "$(ProjectDir)SkaConfig.xml" "$(TargetDir)SkaConfig.xml"
 *      History:    Regression - 15/02/2017 - 09:59 PM  - Adicionado novo método 'IsArqXmlFromSka' - V1.1 Regredida
 * 					Update	   - 16/02/2017 - 05:44 PM  - Corrigido o problema no método 'CreateXmlFileConfig' - V1.2 Lançada
 *					Update 	   - 16/02/2017 - 05:46 PM  - Adicionado ao método 'CreateXmlFileConfig', um novo parâmetro para criar
 *													um arquivo XML com uma nomenclatura especial no primeiro node. - V1.3 Lançada
 *                  Update     - 17/03/2017 - 07:39 AM  - Adicionado um novo método 'ExistXmlConfigParam' - V1.4 Lançada
 *                  Update     - 17/03/2017 - 08:17 AM  - Implementado uma nova função no método 'AddXmlParameters', cujo verifica
 *                                                  se uma chave já não existe no mesmo e em seguida, atualiza do dados. - V1.5 Lançada
 *                  Update     - 17/03/2017 - 11:18 AM - Adicionado um novo método 'ExistFatherNode' - V1.6 Lançada
 *                  Update     - 17/03/2017 - 04:47 PM - Adicionado um novo método 'UpdateXmlConfigValue2' - V1.7 Lançada
 *                  Update     - 20/03/2017 - 09:58 AM - Adicionado um novo método 'AddXmlParametersDictionary' - V1.8 Lançada
 *                  Update     - 20/03/2017 - 10:07 AM - Adicionado um novo método 'ReturnXmlConfigValuesDic' - V1.9 Lançada
 *                  Update     - 20/03/2017 - 12:15 AM - Adicionado um novo método 'QtdeFilhos' - V2.0 Lançada.
 *                  Update     - 20/03/2017 - 01:43 PM - Adicionado um novo método 'ReturnXmlConfigValuesDicByPosition' - V2.1 Lançada.
 *                  Update     - 22/03/2017 - 11:24 AM - Adicionado um novo método 'ReturnXmlVarAndValue' -V2..2 Lançada
 *                  Update     - 22/03/2017 - 02:18 PM - Corrigido o problema no método 'AddXmlParameters', cujo estava criando
 *                                                  sempre um novo 'pai' dentro do configuration - V2.3 Lançada.
 *                  Update     - 23/03/2017 - 11:07 AM - Adicionar um novo método 'lstNomeNodesPais' - V2.4 Lançada.
 *                  Update     - 24/03/2017 - 01:54 PM - Adicionado um novo método 'RemoveXmlConfigElement' - V2.5 Lançada.
 *                  Update     - 29/03/2017 - 01:59 PM - Corrigido o problema de não salvar novos valores no arquivo XML de configuração
 *                                                  no método 'AddXmlParameters' - V2.6 Lançada
 *                                                  
 *                  Update     - 20/04/2017 - 09:57 AM - Alterado toda a estrutuda da classe, onde, quando orientado um novo objeto dessa
 *                                                  classe, eu ja solicito o nome do arquivo de configuração, e para todos os demais métodos,
 *                                                  eu utilizo o nome que foi instanciado na classe. - V2.7 Lançada
 *                                                  
 *                  Update     - 24/04/2017 - 17:55 PM - Corrigido o problema de atualizar os dados contidos nas variáveis do
 *                                                  do arquivo de configuração, onde, o mesmo estava dando problema em substituir
 *                                                  '""' (vazio/nulo) por um novo valor. - V2.8 Lançada.
 *                  Update     - 25/04/2017 - 13:58 PM - Adicionado um novo 'catch' no método 'AddXmlParameters', verificando as 
 *                                                  permissões do usuário no diretório para salvamento - V2.9 Lançada
 *                  Update     - 25/04/2017 - 17:02 PM - Adicionado um novo médoto 'CreateXmlSubNodes' - V3.0 Lançada
 *                  Update     - 08/05/2017 - 16:57 PM - Corrigido o problema de retornar dados duplicados 
 *                                                  no método 'LstNomeNodesPais' - V3.1 Lançada
 *                  Update     - 08/05/2017 - 17:05 PM - Corrigido o problema no método 'AddXmlParametersDictionary' de replicar os nós pais - V3.2 Lançada.
 *                  Update     - 08/05/2017 - 17:35 PM - Corrigido o problema de objeto ou instância não iniciado no método 'AddXmlParametersDictionary' - V3.3 Lançada
 *                  Update     - 22/05/2017 - 06:16 PM - Corrigido o problema no método 'DicXmlVarNameAndValue', onde o mesmo
 *                                                  gerava uma exceção não tratada quando o XML estava com dados vazios - V3.4 Lançada
 *                  Update     - 06/12/2017 - 11:14 AM - Corrigido um erro grave no método 'StrXmlSimpleConfigValue', onde, caso
 *                                                  o campo estivesse vazio, ele gerava exceção e apresentava uma mensagem. Agora,
 *                                                  ele dá a exceção, não apresenta mensagem e retorna vazio (String.Empty == "") - V3.5 Lançada.
 *                  Update     - 23/02/2018 - 11:19 AM - Refatorado a classe inteira, removido métodos desnecessários - V3.6 Lançada.
 *                  Update     - 06/07/2018 - 10:12 AM - Adicionado uma nova propriedade para verificar se o arquivo de configuração existe no caminho informado - V3.7 Lançada.
 *                  Update     - 14/08/2018 - 14:32 PM - Implementado novas tratativas para não deixar os arquivos de configuração
 *                                                  XML presos à classe (objetos não eram descartados) - V3.8 Lançada.
 *                  Update     - 19/11/2018 - 20:19 PM - Adicionado novas tratativas na instância do objeto - V3.9 Lançada.
 *                  Update     - 27/02/2019 - 09:39 AM - Corrigido problemas no método 'StrXmlSimpleConfigValue' - V4.0 Lançada.
 *                  Update     - 27/02/2019 - 09:41 AM - Corrigido um problema no método 'BoolExistFatherNode' - V4.1 Lançada.
 * 
 * 
 *  Adicionar aos eventos da solution: copy "$(ProjectDir)SkaConfig.xml" "$(TargetDir)SkaConfig.xml"
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;
using System.IO;
//using System.Windows.Forms;


/// <summary>
///     Classe para criar um arquivo XML de configuração. O mesmo permite criar um arquivo XML, adicionar valores e
/// retornar os valores em texto.
/// </summary>
public class CustomXmlConfig
{

    #region Variáveis Globais

    /// <summary>
    ///     Variável que contém o nome do arquivo XML de configuração.
    /// </summary>
    protected String mNomeArqCfg;

    /// <summary>
    ///     Variável que contém um valor booleano que define se foi localizado o
    /// arquivo de configuração informado por parâmetro.
    /// </summary>
    protected Boolean mExisteArqCfg;

    #endregion

    #region Propriedades

    /// <summary>
    ///     Propriedade somente leitura que contém o nome do arquivo XML.
    /// </summary>
    public String _NomeArqCfg
    {
        get
        {
            return mNomeArqCfg;
        }
    }

    /// <summary>
    ///     Propriedade somente leitura que contém o valor se o arquivo XML
    /// existe no local informado.
    /// </summary>
    public Boolean _ExisteArqCfg
    {
        get
        {
            return mExisteArqCfg;
        }
    }

    #endregion

    #region Instancia dos objetos da classe

    /// <summary>
    /// Método para instância a criação de um novo arquivo XML de configuração ou a carga
    /// de dados deste.
    /// </summary>
    /// <param name="NomeArqConfig">Nome do arquivo (pode conter ou não a extensão XML)</param>
    public CustomXmlConfig(String NomeArqConfig)
    {
        try
        {
            if (String.IsNullOrEmpty(NomeArqConfig)) throw new ArgumentNullException("Nome do arquivo XML não pode ser nulo ou vazio");
            else if (NomeArqConfig.ToUpper().EndsWith(".XML")) mNomeArqCfg = NomeArqConfig;
            else mNomeArqCfg = string.Format("{0}.xml", NomeArqConfig);

            //Verifico se o arquivo não contém backslash, se não conter, pode ser que
            //seja um serviço tentando ler o arquivo, nesse caso, a classe irá falhar.
            if (!NomeArqConfig.Contains('\\'))
            {
                String caminhoArqXml = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\") + 1), mNomeArqCfg);

                if (File.Exists(caminhoArqXml))
                {
                    //Variável recebe o novo caminho do arquivo XML.
                    mNomeArqCfg = caminhoArqXml;
                    mExisteArqCfg = true;
                }
            }
            else if (!File.Exists(mNomeArqCfg)) mExisteArqCfg = false;
            else if (File.Exists(mNomeArqCfg)) mExisteArqCfg = true;
        }
        catch (Exception ex)
        {
            throw new Exception("Não foi possível instanciar o objeto na classe 'CustomXmlConfig'. Motivo do erro: " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Destruidor da classe (limpa as declarações de objetos, métodos, campos, propriedades, tudo na realidade).
    /// </summary>
    ~CustomXmlConfig() { }

    #endregion

    #region Métodos

    /// <summary>
    ///     Método que cria um arquivo XML de configuração no local de execução da aplicação.
    /// </summary>
    /// <param name="StartNode">Caso deseja criar um arquivo 'XML' com um nome personalizado no primeiro node.</param>
    public void CreateNewXmlFileConfig(String StartNode = "")
    {

        XmlTextWriter xmlWrite;

        try
        {
            String pathXml = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\") + 1), mNomeArqCfg);
            xmlWrite = new XmlTextWriter(pathXml, Encoding.UTF8);

            //Configurações do xml.
            xmlWrite.WriteStartDocument(true);
            xmlWrite.Formatting = Formatting.Indented;
            xmlWrite.Indentation = 2;

            //Nó principal
            xmlWrite.WriteStartElement("Configuration");
            xmlWrite.WriteComment("Arquivo de configuração SKA");

            xmlWrite.WriteEndElement();
            xmlWrite.WriteEndDocument();
            xmlWrite.Close();


            //Agora irá adicionar a configuração que o usuário deseja.
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathXml);

            XmlNode n;

            if (StartNode != null)
                n = xmlDoc.CreateNode(XmlNodeType.Element, StartNode, null);
            else n = xmlDoc.CreateNode(XmlNodeType.Element, "SkaConfiguration", null);

            xmlDoc.DocumentElement.AppendChild(n);
            xmlDoc.Save(pathXml);

            //Não deixa o XML preso à aplicação(impede leituras futuras).
            n = null;
            xmlDoc = null;
            GC.Collect();

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar criar um novo arquivo de configuração", ex);
        }

    }

    /// <summary>
    ///     Método que adiciona parâmetros em um arquivo XML de configuração, onde, no dicionário de dados,
    /// conterá o nome da variável (Key) e o valor (Value) que será armazenado.
    /// </summary>
    /// <param name="NodeName">Nó cujo armazenará os valores das variáveis e respectivamente seus valores.</param>
    /// <param name="DicParam">Dicionário de dados contendo o nome da variável e seu valor para adicionar ao arquivo XML.</param>
    public void AddXmlParameters(String NodeName, Dictionary<string, object> DicParam)
    {
        try
        {
            if (!File.Exists(mNomeArqCfg))
                throw new Exception(string.Format("Arquivo XML de nome '{0}' não encontrado!", mNomeArqCfg));
            else
            {
                //  Instância um novo objeto XmlDocument.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(mNomeArqCfg);

                //  Instância um novo objeto XDocument.
                XDocument xDoc = XDocument.Load(mNomeArqCfg);

                foreach (KeyValuePair<string, object> it in DicParam)
                {
                    //Novo nó (ConfigValue)
                    XmlNode mXmlNode;

                    //Valida o nó pai, caso não existir nenhum, cria um novo.
                    if (!BoolExistFatherNode(NodeName)) mXmlNode = xmlDoc.CreateNode(XmlNodeType.Element, NodeName, null);
                    else mXmlNode = xmlDoc.DocumentElement[NodeName];

                    //Pega todas as variávels 'ConfigValue' dentro do XML. Isso não pode mudar, caso contrário, irá dar erro!!!
                    var aux = xDoc.Elements().ToArray()[0].Elements().ToArray().Elements().ToArray();

                    //Se existir uma variável de mesmo nome no arquivo XML, atualizo os dados.
                    if (aux.Where(x => x.FirstAttribute.Value.ToString().ToUpper() == it.Key.ToUpper()).Count() > 0)
                    {
                        UpdateXmlConfigValue(it.Key.ToString(), it.Value.ToString());
                    }
                    //Caso contrário, eu crio uma nova chave no mesmo.
                    else
                    {

                        //  Elemento <ConfigValue>
                        XmlNode mUserNode = xmlDoc.CreateElement("ConfigValue");
                        //  Atributo <Variable>
                        XmlAttribute mXmlAttr = xmlDoc.CreateAttribute("Variable");
                        mXmlAttr.Value = it.Key.ToUpper();
                        mUserNode.Attributes.Append(mXmlAttr);

                        //  Atributo <Value>
                        mXmlAttr = xmlDoc.CreateAttribute("Value");
                        mXmlAttr.Value = it.Value.ToString();
                        mUserNode.Attributes.Append(mXmlAttr);

                        if (!BoolExistFatherNode(NodeName))
                        {
                            //  Apenda conteúdo.
                            //  Ex.: <ConfigValue Variable="it.Key" Value="it.Value" />
                            mXmlNode.AppendChild(mUserNode);

                            //  Adiciona os elementos criados à coleção já existente.
                            xmlDoc.DocumentElement.AppendChild(mXmlNode);

                            //  Salva o arquivo XML no mesmo caminho.
                            xmlDoc.Save(mNomeArqCfg);
                        }
                        else
                        {
                            mXmlNode.AppendChild(mUserNode);

                            //  Salva o arquivo XML no mesmo caminho.
                            xmlDoc.Save(mNomeArqCfg);
                        }
                    }
                }

                //Não deixa o XML preso à aplicação(impede leituras futuras).
                xmlDoc = null;
                xDoc = null;
                GC.Collect();
            }

        }
        catch (UnauthorizedAccessException exAc)
        {
            throw new UnauthorizedAccessException("O usuário corrente não possui permissões suficientes para salvar o arquivo de configuração XML. " + exAc.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado: " + ex.Message);
        }
    }

    /// <summary>
    ///     Método que atualiza o valor de uma variável contida no arquivo XML.
    /// </summary>
    /// <param name="VariableName">Nome da variável que terá o seu valor atualizado no arquivo XML de configuração</param>
    /// <param name="NewValue">Novo valor à ser atualizado na variável informada no parâmetro</param>
    public void UpdateXmlConfigValue(String VariableName, String NewValue)
    {
        try
        {
            if (!File.Exists(mNomeArqCfg))
                throw new FileNotFoundException("Arquivo de configuração XML não foi localizado");

            XDocument XmlDoc = XDocument.Load(mNomeArqCfg);

            var aux = XmlDoc.Elements().ToArray()[0].Elements().ToArray()[0].Elements().ToArray();

            foreach (var filho in aux)
            {
                if (filho.FirstAttribute.Value.ToString().ToUpper() == VariableName.ToUpper())
                {
                    filho.LastAttribute.Value = NewValue;
                    break;//Sai do loop, pois não precisa percorrer a lista novamente (valor já foi atualizado).
                }
            }

            //  Salva o arquivo XML com os novos valores.
            XmlDoc.Save(mNomeArqCfg);

            //Não deixa o XML preso à aplicação(impede leituras futuras).
            XmlDoc = null;
            GC.Collect();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar atualizar parâmetro no arquivo de configuração", ex);
        }

    }

    /// <summary>
    ///     Método que retorna o valor de uma variável contida no arquivo XML.
    /// </summary>
    /// <param name="VariableName">Nome da variável que irá buscar o valor</param>
    /// <returns>Valor da variável desejada</returns>
    public string StrXmlSimpleConfigValue(String VariableName)
    {
        try
        {
            //  Só irá ler o arquivo de configuração caso o mesmo exista.
            if (!File.Exists(mNomeArqCfg))
                return "";

            XDocument xmldoc = XDocument.Load(mNomeArqCfg);

            //  Para cada filho no '<Configuration />', obtenho todos os seus elementos '<ConfigValue />'.
            //                                          [Pega tudo do nó filho] [Pega o primeiro elemento filho]
            var filhos = xmldoc.Elements().ToArray()[0].Elements().ToArray()[0].Elements();

            String temp = filhos.Where(x => x.FirstAttribute.Value.ToUpper() == VariableName.ToUpper()).Select(x => x.FirstAttribute.NextAttribute.Value).FirstOrDefault();

            return String.IsNullOrEmpty(temp) == false ? temp : "";
        }
        catch { return ""; }
    }

    /// <summary>
    ///     Método booelano que identifica se um nó filho (nó pai sempre será o primeiro item) existe
    /// no arquivo XML.
    /// </summary>
    /// <param name="FatherNodeName">Nome do nó pai(aqui eu o trato como um pai, mas na realdiade é um filho do XML, 
    /// cujo nó principal é '<CONFIGURATION/>')</param>
    /// <returns>False caso não exista o nó pai informado como parâmetro.</returns>
    private bool BoolExistFatherNode(String FatherNodeName)
    {
        XDocument xmlDoc;

        try
        {
            if (File.Exists(mNomeArqCfg))
                xmlDoc = XDocument.Load(mNomeArqCfg);
            else return false;

            //  Identifico quantos filhos tem no nó pai    [0]<Configuration />
            var mLstElements = xmlDoc.Elements().ToArray()[0].Elements().ToArray();

            var SubFilhos = xmlDoc.Elements().ToArray()[0].Elements().ToArray();

            return SubFilhos.Where(x => x.Name.ToString().ToUpper() == FatherNodeName.ToUpper()).Count() > 0;
        }
        catch { return false; }
    }

    #endregion



    //    //TODO: 'CustomXmlConfig.cs' investigar a possibilidade do método 'SaveNewXmlFileConfig'
    //    /// <summary>
    //    ///     Método que atualiza os dados do Arquivo XML (apaga o arquivo e depois o cria novamente
    //    /// com os dados no dicionário de dados).
    //    /// </summary>
    //    /// <param name="dic">Dicionário de dados que possui o nome da variável que terá um valor
    //    /// alocado (valor configurado pelo usuário).</param>
    //    private void SaveNewXmlFileConfig(Dictionary<string, object> dic)
    //    {
    //        List<string> lstFathers = new List<string>();
    //        string StartNodeName = "";
    //        string SubNodeName = "";
    //        Dictionary<string, object> dicCfgVar = new Dictionary<string, object>();

    //        XDocument mXmldoc;

    //        #region 1- Lê o StartNode do documento XML
    //        try
    //        {
    //            mXmldoc = XDocument.Load(nomeArqCfg);

    //            StartNodeName = mXmldoc.Elements().ToArray()[0].Name.ToString();
    //            SubNodeName = mXmldoc.Elements().ToArray()[0].Elements().ToArray()[0].Name.ToString();
    //        }
    //        catch { }
    //        #endregion

    //        #region 2- Lê todos os sub-nodes
    //        try
    //        {
    //            mXmldoc = XDocument.Load(nomeArqCfg);

    //            var lstElementos = mXmldoc.Elements().ToArray()[0].Elements().ToArray();

    //            foreach (var Filhos in lstElementos)
    //            {
    //                //Se não existir um node pai, eu adiciono na lista (controle para não repetir dados na lista).
    //                if (lstFathers.Where(x => x.ToUpper() == Filhos.Name.ToString().ToUpper()).Count() == 0)
    //                    lstFathers.Add(Filhos.Name.ToString());
    //            }
    //        }
    //        catch { }
    //        #endregion

    //        #region 3- Deleta o arquivo antigo de configuração
    //        try
    //        {
    //            if (System.IO.File.Exists(nomeArqCfg))
    //            {
    //                System.IO.File.Delete(nomeArqCfg);
    //            }
    //            else { }
    //        }
    //        catch { }
    //        #endregion

    //        #region 4- Cria um novo arquivo de configuração com o StartNode
    //        try
    //        {
    //            CreateNewXmlFileConfig(StartNodeName);
    //        }
    //        catch { }
    //        #endregion

    //        #region 5- Cria os sub-nodes
    //        try
    //        {
    //            foreach (string s in lstFathers)
    //            {
    //                CreateXmlSubNodes(s.ToUpper());
    //            }
    //        }
    //        catch { }
    //        #endregion

    //        #region 6- Adiciona os parâmetros no arquivo XML
    //        try
    //        {
    //            AddXmlParameters(StartNodeName, dic);
    //        }
    //        catch { }
    //        #endregion
    //    }

}