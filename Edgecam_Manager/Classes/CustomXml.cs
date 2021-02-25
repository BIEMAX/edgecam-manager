/***********************************************************************************************************
 * 
 * 
 *                  CustomXml - Classe com métodos para criação de um arquivo XML
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Criar arquivo XML com uma determinada estrutura.
 *      Version:    1.1
 *      Date:       16/10/2018, at 07:44 AM
 *      Note:       <None>
 *      Updates:    Update      - 16/10/2018 - 07:44 AM - Lançada a primeira versão - V1.0 Lançada.
 *                  Update      - 17/10/2018 - 12:33 AM - Adicionado as propriedades '_ArquivoSalvo'
 *                      e '_LocalArqXml' - V1.1 Lançada
 *      
 * 
 * 
 * 
 * 
 ***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

/// <summary>
///     Criar arquivo XML com uma determinada estrutura.
/// </summary>
public class CustomXml
{

    #region Variáveis globais

    /// <summary>
    ///     Contém o caminho e nome do arquivo XML salvo.
    /// </summary>
    private String mLocalArqXml;

    /// <summary>
    ///     Contém o nome do elemento inicial do XML.
    /// </summary>
    private String mNomeElementoInicial;

    /// <summary>
    ///     True para caso o arquivo tenha sido salvo corretamente.
    /// </summary>
    private Boolean mArquivoSalvo;

    #endregion

    #region Propriedades

    /// <summary>
    ///     True para caso o arquivo tenha sido salvo corretamente.
    /// </summary>
    public Boolean _ArquivoSalvo
    {
        get
        {
            return mArquivoSalvo;
        }
    }

    /// <summary>
    ///     Contém o caminho e nome do arquivo XML salvo.
    /// </summary>
    public String _LocalArqXml
    {
        get
        {
            return mLocalArqXml;
        }
    }

    #endregion

    #region Enumeradores

    /// <summary>
    ///     Enumerador que contém o destino de salvamento do arquivo XML.
    /// </summary>
    public enum e_SkaLocalSalvamento
    {
        AreaDeTrabalho,
        DocumentosUsuario,
        DocumentosPublico,
        PastaTemporaria
    }

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instância o objeto da classe e informa o diretório do arquivo e o nome do
    /// elemento inicial da estrutura do XML.
    /// </summary>
    /// <param name="LocalArq">Local de armazenamento do XML (o nome será gerado automaticamente)</param>
    /// <param name="NomePrefixoXml">Prefixo para adicionar no nome do XML.</param>
    /// <param name="NomeElementoInicial">Nome do elemento inicial.</param>
    public CustomXml(e_SkaLocalSalvamento LocalArq, String NomePrefixoXml, String NomeElementoInicial)
    {
        switch (LocalArq)
        {
            case e_SkaLocalSalvamento.AreaDeTrabalho:
                mLocalArqXml = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.xml", NomePrefixoXml, DateTime.Now.ToString("dd-MM-yyy-hh-mm-ss-ff")));
                break;
            case e_SkaLocalSalvamento.DocumentosPublico:
                mLocalArqXml = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), String.Format("{0}_{1}.xml", NomePrefixoXml, DateTime.Now.ToString("dd-MM-yyy-hh-mm-ss-ff")));
                break;
            case e_SkaLocalSalvamento.DocumentosUsuario:
                mLocalArqXml = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), String.Format("{0}_{1}.xml", NomePrefixoXml, DateTime.Now.ToString("dd-MM-yyy-hh-mm-ss-ff")));
                break;
            case e_SkaLocalSalvamento.PastaTemporaria:
                mLocalArqXml = Path.Combine(Path.GetTempPath(), String.Format("Exemplo_{0}.xml", DateTime.Now.ToString("dd-MM-yyy-hh-mm-ss-ff")));
                break;
            default: break;
        }

        if (String.IsNullOrEmpty(NomeElementoInicial))
            throw new NotImplementedException("O nome do elemento inicial do XML não pode ser vazio ou nulo.");

        mNomeElementoInicial = NomeElementoInicial;

        CriaArquivoXml();
    }

    #endregion

    #region Métodos privados

    /// <summary>
    ///     Cria um arquivo XML com o elemento inicial já incluso.
    /// </summary>
    private void CriaArquivoXml()
    {
        //Arquivo config sempre será criado no diretório de execução da aplicação.
        XmlTextWriter mXmlWrite = new XmlTextWriter(mLocalArqXml, Encoding.UTF8);

        //Configurações do xml.
        mXmlWrite.WriteStartDocument(true);
        mXmlWrite.Formatting = Formatting.Indented;
        mXmlWrite.Indentation = 2;
        mXmlWrite.WriteStartElement(mNomeElementoInicial);
        mXmlWrite.WriteEndElement();
        mXmlWrite.WriteEndDocument();
        mXmlWrite.Close();
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    ///     Método que adiciona uma lista de elementos a um novo ou a um arquivo XML existente.
    /// </summary>
    /// <param name="NomeElementoPai">Nome do elemento que receberá a lista de filhos</param>
    /// <param name="LstElementos">Lista de objeto contendo o nome e os valores dos itens a serem adicionados.</param>
    public void AdicionaElementosXml(String NomeElementoPai, List<LstElemXml> LstElementos)
    {
        if (!String.IsNullOrEmpty(NomeElementoPai) && LstElementos != null && LstElementos.Count > 0)
        {
            //Objeto responsável por ler os elementos já adicionados dentro do XML.
            XmlDocument xmlDoc = new XmlDocument();

            //Carrega o arquivo XML no objeto para adicionar novos elementos.
            xmlDoc.Load(mLocalArqXml);

            // Crio um elemento <Item> no XML de retorno
            XmlNode mXmlNode = xmlDoc.CreateElement(NomeElementoPai.ToUpper().Trim());

            for (int x = 0; x < LstElementos.Count; x++)
            {
                //Recarrega o documento após a segunda passada.
                //if (x > 0)
                //    xmlDoc.Load(mLocalArqXml);

                var e = LstElementos[x];

                XmlNode m1 = xmlDoc.CreateElement(e.NomeElemento);
                try
                {
                    m1.InnerText = e.ValorElemento;
                    mXmlNode.AppendChild(m1);
                }
                catch
                {
                    m1.InnerText = "";
                    mXmlNode.AppendChild(m1);
                }

                xmlDoc.DocumentElement.AppendChild(mXmlNode);
                xmlDoc.Save(mLocalArqXml);
            }

            mArquivoSalvo = true;
        }
        else mArquivoSalvo = false;
    }

    #endregion
}

/// <summary>
///     Classe que representa um objeto à ser adicionado a uma estrutura XML.
/// Essa classe deve ser utilizada em conjunto da classe 'CustomXml'.
/// </summary>
public class LstElemXml
{
    /// <summary>
    ///     Nome do elemento
    /// </summary>
    public String NomeElemento;

    /// <summary>
    ///     Valor do elemento.
    /// </summary>
    public String ValorElemento;
}