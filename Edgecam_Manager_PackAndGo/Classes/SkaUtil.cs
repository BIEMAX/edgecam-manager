/***********************************************************************************************************
 * 
 * 
 *                              SKA - Utilidades Genéricas
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe com funções genéricas
 *      Version:    4.7
 *      Date:       Agosto de 2016
 *      Note:       <None>
 *      History:    Update      - 15/12/2016 - 2:25 PM - Adicionado nova método 'StringsIguais' - V1.1 Lançada
 *                  Update      - 15/12/2016 - 2:54 PM - Adicionado novo método para salvar var dados em um arq ini existente - V1.2 Lançada
 *                  Update      - 15/12/2016 - 2:54 PM - Alterado o método 'InsereDadosConfig', adicionado novo parâmetro
 *                                              para informar um arquivo ini diferente do nome 'config.ini' - V1.3 Lançada
 *                  Update      - 15/12/2016 - 3:03 PM - Alterado o método 'LeParametroIni', adicionado novo parâmetro
 *                                              para informar um arquivo ini diferente do nome 'config.ini' - V1.4 Lançada
 *                  Update      - 16/12/2016 - 8:58 PM - Adicionado ao método 'InsereDadosConfig' a opção de adicionar os comentários - V1.5
 *                  Update      - 08/02/2017 - 11:09 PM - Adicionado novo método 'ReplaceTextInStrings' - V1.6 Lançada
 *                  Update      - 15/03/2017 - 02:56 PM - Adicionado um novo método 'IsStringCodigoValido' - V1.7 Lançada
 *                  Update      - 15/03/2017 - 03:19 PM - Adicionado um novo método 'IsNomeValido' - V1.8 Lançada.
 *                  Update      - 23/03/2017 - 08:45 AM - Alterado o método de 'public void LeArquivosPasta' para 'public List<string>' - V1.9 Lançada.
 *                  Update      - 27/03/2017 - 10:12 AM - Adicionado um novo método 'StrMostraVersao' - V2.0 Lançada
 *                  Update      - 27/06/2017 - 03:52 PM - Removido alguns métodos não utilizados - V2.1 Lançada
 *                  Update      - 28/06/2017 - 03:07 PM - Adicionado um novo método 'CarregaImagem' - V2.2 Lançada
 *                  Update      - 18/07/2017 - 04:07 PM - Adicionado um novo parâmetro no método 'LstArquivosPasta' - V2.3 Lançada
 *                  Update      - 19/07/2017 - 08:51 AM - Adicionado um novo método 'VerifyNullValues' - V2.4 Lançada
 *                  Update      - 16/08/2017 - 05:29 PM - Corrigido o problema no método 'CarregaImagem' - V2.5 Lançada
 *                  Update      - 17/08/2017 - 02:45 AM - Adicionado novos métodos 'BuscaDiretorio', 'BuscaArquivos', 'BuscaArquivo' - V2.6 Lançada 
 *					Update 		- 21/08/2017 - 02:17 PM - Renomeado o método de 'VerifyNullValues' para 'IsObjectNull' - V2.7 Lançada
 *					Update 		- 16/10/2017 - 02:43 PM - Adicionado o método de 'TesteNull' - V2.8 Lançada
 *					Update		- 21/02/2018 - 01:40 PM - Adicionado o método 'GetUniversalPathFromFile' - V2.9 Lançada
 *					Update      - 24/08/2018 - 07:51 AM - Removido o método 'GetUniversalPathFromFile' - V3.0 Lançada
 *					Update      - 24/08/2018 - 08:10 AM - Refatorado a classe inteira e organizado - V3.1 Lançada
 *					Update      - 24/08/2018 - 03:23 PM - Corrigido um problema no método 'BuscaArquivo' - V3.2 Lançada.
 *					Update      - 04/09/2018 - 05:24 PM - Refatorado o método 'BuscaArquivos' - V3.3 Lançada.
 *					Update      - 18/09/2018 - 05:38 PM - Corrigido um problema de exceção no método 'BuscaArquivos' - V3.4 Lançada
 *					Update      - 18/09/2018 - 10:02 PM - Implementado a múltipla seleção no método 'BuscaArquivos' - V3.5 Lançada
 *					Update      - 19/11/2018 - 09:40 PM - Alterado o método 'BuscaArquivos' - V3.6 Lançada.
 *					Update      - 26/11/2018 - 04:12 PM - Adicionado um comentário no método 'BuscaArquivo' - 3.7 Lançada
 *					Update      - 28/01/2019 - 03:33 PM - Adicionado uma nova instância do método 'BuscaArquivo' - V3.8 Lançada.
 *					Update      - 28/01/2019 - 04:49 PM - Renomeado o método 'CarregaImagem' para 'ConverteImagemEmByteArray' - V3.9 Lançada.
 *                  Update 		- 28/01/2019 - 04:53 PM - Adicionado o método de 'ConverteByteArrayEmImagem' - V4.0 Lançada
 *                  Update      - 01/02/2019 - 03:23 PM - Corrigido um problema no método 'ConverteByteArrayEmImagem' -V4.1 Lançada.
 *                  Update      - 04/02/2019 - 09:58 AM - Adicionado o método 'SalvaArquivo' - V4.2 Lançada.
 *                  Update      - 19/02/2019 - 09:50 AM - Adicionado o método 'CheckInternetConnection' - V4.3 Lançada.
 *                  Update      - 19/02/2019 - 10:21 AM - Adicionado o método 'GetGlobalIpAddress' - V4.4 Lançada.
 *                  Update      - 21/02/2019 - 09:00 AM - Adicionado o método 'GetLocalIpAddress' - V4.5 Lançada.
 *                  Update      - 06/03/2019 - 04:49 PM - Adicionado o método 'FormatLargeNumbers' - V4.6 Lançada.
 *                  Update      - 06/03/2019 - 11:26 PM - Adicionado todas as propriedades - V4.7 Lançada.
 * 
***********************************************************************************************************/

using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;       //List<String>
using System.Text.RegularExpressions;   //Matches, Regex
using System.Data.SqlClient;            //SqlConnection
using System.Data;                      //DataTable
using Microsoft.VisualBasic;
using System.Drawing;                   //Utilizado no método 'ReplaceTextInStrings'
//using System.Management;              //Utilizado no método 'GetUniversalPathFromFile'

/// <summary>
///     Classe que possui métodos estáticos para auxílio do programador.
/// </summary>
internal class SkaUtil
{

    #region Variáveis privadas

    private String mMyComputer = Environment.MachineName;
    private String mCurrentUser = Environment.UserName;
    private String mMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private String mMyDocumentsPublic = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
    private String mTempPath = Path.GetTempPath();
    private String mDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o nome do computador.
    /// </summary>
    public String _NomeComputador
    {
        get { return mMyComputer; }
    }

    /// <summary>
    ///     Contém o nome do usuário logado atualmente no windows.
    /// </summary>
    public String _NomeUsuarioWindows
    {
        get { return mCurrentUser; }
    }

    /// <summary>
    ///     Contém o diretório dos documentos do usuário.
    /// </summary>
    public String _DiretorioDocumentosUsuario
    {
        get { return mMyDocuments; }
    }

    /// <summary>
    ///     Contém o diretório dos documentos públicos do computador.
    /// </summary>
    public String _DiretorioDocumentosPublicos
    {
        get { return mMyDocumentsPublic; }
    }

    /// <summary>
    ///     Contém o diretório dos documentos temporários do usuário.
    /// </summary>
    public String _DiretorioTemporario
    {
        get { return mTempPath; }
    }

    /// <summary>
    ///     Contém o diretório da área de trabalho do usuário.
    /// </summary>
    public String _DiretorioDesktop
    {
        get { return mDesktop; }
    }

    #endregion

    #region Enumeradores

    public enum e_TipoDado
    {
        Inteiro,
        Booleano,
        Texto,
        Decimal
    }

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instancia o objeto e dá acesso à métodos utilitários.
    /// </summary>
    public SkaUtil() { }

    ~SkaUtil() { }

    #endregion

    #region Métodos públicos

    /// <summary>
    /// Mostra a versão para o usuário
    /// </summary>
    /// <param name="frm">Sugestão: passar como parâmetro '.</param>
    /// <param name="titulo">Titulo que deverá ser demonstrado no escopo da aplicação</param>
    public void MostraVersao(Form frm, String titulo)
    {
        frm.Text = titulo + Application.ProductVersion;
    }

    /// <summary>
    ///     Método que retorna uma string contendo a versão do Assemby.
    /// </summary>
    /// <param name="VMajor">Definir como true caso deseja retornar esse parâmetro (opicional)</param>
    /// <param name="VMinor">Definir como true caso deseja retornar esse parâmetro (opicional)</param>
    /// <param name="VBuild">Definir como true caso deseja retornar esse parâmetro (opicional)</param>
    /// <param name="VRevision">Definir como true caso deseja retornar esse parâmetro (opicional)</param>
    /// <returns>retorna uma string contendo a versão do Assemby</returns>
    public String StrMostraVersao(Boolean VMajor, Boolean VMinor = false, Boolean VBuild = false, Boolean VRevision = false)
    {
        string ret = "";
        Version version = Assembly.GetExecutingAssembly().GetName().Version;

        ret = version.Major.ToString();

        if (VMinor != false) ret += "." + version.Minor.ToString();
        if (VBuild != false) ret += "." + version.Build.ToString();
        if (VRevision != false) ret += "." + version.Revision.ToString();

        return ret;
    }

    /// <summary>
    /// Função que cria um arquivo de log (Meus Documentos) para salvar os logs da aplicação.
    /// </summary>
    /// <param name="msg">Mensagem a ser apendada no arquivo de texto salvo nos meus documentos..</param>
    /// <note>O arquivo de Log está com nome fixo como SKA + hora de inicio de gravação dos dados.</note>
    public void GravaLog(String msg)
    {
        Application.DoEvents();

        //"Serializo" o arquivo de log.
        string arqLog = string.Format("\\SKA-{0:yyyy-MM-dd-HH.mm}.log", DateTime.Now);

        File.AppendAllText(mMyDocuments + arqLog, string.Format("\n {0} {1}{2}", DateTime.Now, msg, "\r\n"));
    }

    /// <summary>
    /// Método que cria um arquivo de log no diretório informado pelo programador.
    /// </summary>
    /// <param name="DirLog"></paramparam>
    /// <param name="msg">Mensagem a ser apendada no arquivo de texto salvo nos meus documentos..</param>
    /// <param name="NomeArqLog"></param>
    public void GravaLog2(String DirLog, String msg, String NomeArqLog)
    {
        Application.DoEvents();

        //"Serializo" o arquivo de log.
        //string arqLog = string.Format("\\SKA-{0:yyyy-MM-dd-HH.mm}.log", DateTime.Now);

        string arqLog = Path.Combine(DirLog, NomeArqLog);

        File.AppendAllText(arqLog, string.Format("{0}", msg));
    }

    /// <summary>
    /// Função que verifica as permissões do usuário atual em um diretório específico, retornando como true se possuir permissões.
    /// </summary>
    /// <param name="Folder">Pasta a ser verificado as respectivas permissões.</param>
    public Boolean VerificaPermissaoPasta(String Folder)
    {
        try
        {
            System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(Folder);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(string.Format("Usuário não possui permissão na pasta '{0}'", Folder), ex);
        }
    }

    /// <summary>
    /// Tratamento de valores para validação e verificação de dados para inserir em bancos de dados.
    /// </summary>
    /// <param name="Valor">Valor a ser verificado.</param>
    /// <param name="tipo">Tipo de dado a ser verificado (string, int, double).</param>
    /// <returns></returns>
    public Object VerificaTipoValor(String Valor, e_TipoDado Tipo)
    {

        System.Globalization.CultureInfo cult = new System.Globalization.CultureInfo("en-US");

        switch (Tipo)
        {
            case e_TipoDado.Inteiro:

                int valint;

                if (!int.TryParse(Valor, System.Globalization.NumberStyles.Any, cult, out valint))
                {
                    throw new Exception(string.Format("Valor não é inteiro. Valor inserido: {0}", Valor));
                }
                else return valint;

            case e_TipoDado.Booleano:

                if (Valor == "0" || Valor == "1") return int.Parse(Valor);
                else throw new Exception(string.Format("Valor não é True/False. Valor inserido: {0}", Valor));

            case e_TipoDado.Texto:

                return Valor == null ? "" : Valor.ToString();

            case e_TipoDado.Decimal:

                decimal valdecimal;
                if (!decimal.TryParse(Valor, System.Globalization.NumberStyles.Any, cult, out valdecimal))
                {
                    throw new Exception(string.Format("Valor não é Decimal. Valor inserido: {0}", Valor));
                }
                else return Valor.Replace(".", ",");

            default:
                return Valor;
        }
    }

    /// <summary>
    ///     Lista de string que retorna uma lista com todos os objetos contidos em um diretório informado
    /// popr parâmetro.
    /// </summary>
    /// <param name="Directory">Diretório à ser executado uma varredura</param>
    /// <param name="Extension">Extensão dos arquivos à serem buscados (somente a extensão, não precisa
    /// informar asterisco ou ponto em conjunto, o método já faz isso sozinho)</param>
    /// <param name="NameToSearch">Nome OPCIONAL para procurar arquivos em específico (só ira retornar
    /// arquivos que contenham o parâmetro de busca)</param>
    /// <param name="NumeroArquivos">Quantidade de arquivos que deve retornar (max)</param>
    public List<String> LstArquivosPasta(String Directory, String Extension, String NameToSearch = null, int NumeroArquivos = 0)
    {

        List<string> lstTemp = null;
        List<string> lstFiles = null;
        List<string> lstRet = null;

        try
        {
            //Primeiro válido os dados são ou não nulos.
            if (IsNullValue(Extension) || IsNullValue(Directory))
                throw new Exception("Parâmetros no método 'LeArquivosPasta' não podem ser vazios ou nulos.");
            else
            {
                DirectoryInfo dir = new DirectoryInfo(Directory);

                if (NameToSearch != null)
                {
                    lstFiles = dir.GetFiles("*." + Extension.ToUpper()).Select(f => f.Name.ToUpper()).ToList();
                    lstFiles.ToArray();

                    //Essas linhas servem para retornar uma lista de arquivos de retorno.
                    lstTemp = lstFiles.Where(f => f.ToUpper().Contains(NameToSearch.ToUpper())).OrderByDescending(x => x.ToUpper()).ToList();
                    lstTemp.ToArray();
                }
                else
                {
                    lstTemp = dir.GetFiles("*." + Extension.ToUpper()).Select(f => f.Name.ToUpper()).OrderByDescending(x => x.ToUpper()).ToList();
                    lstTemp.ToArray();
                }

                if (NumeroArquivos > 0)
                {
                    if (lstRet == null) lstRet = new List<string>();
                    for (int i = 0; i < NumeroArquivos; i++)
                    {
                        lstRet.Add(lstTemp[i]);
                    }
                }
                else lstRet = lstTemp;

            }
        }
        catch (Exception ex)
        {
            Exception skaEx = new Exception("Campos não podem ser vazios/nulos quando passados como parâmetros.", ex);
        }

        return lstRet;
    }

    /// <summary>
    /// Função para copiar um diretório especificado pelo usuário, contendo todos os arquivos e sub-diretórios.
    /// </summary>
    /// <param name="DiretorioCopiar"></param>
    /// <param name="DiretorioDestino"></param>
    /// <param name="CopiaSubDiretorios"></param>
    public void CopiaDiretorios(String DiretorioCopiar, String DiretorioDestino, Boolean CopiaSubDiretorios)
    {
        //Obtém uma info no diretório selecionado.
        DirectoryInfo dirPai = new DirectoryInfo(DiretorioCopiar);

        if (!dirPai.Exists)
            throw new DirectoryNotFoundException("O seguinte diretório não existe ou não pode ser localizado: " + DiretorioCopiar);

        //Obtenho os sub-diretórios.
        DirectoryInfo[] dirsFilhos = dirPai.GetDirectories();

        //Se o diretório destino não existir, ele é criado.
        if (!Directory.Exists(DiretorioDestino))
        {
            Directory.CreateDirectory(DiretorioDestino);
        }

        //Obtenho todos os arquivos do diretório pai.
        FileInfo[] arquivosDirPai = dirPai.GetFiles();

        //Primeiro, efetuo a cópia dos arquivos soltos.
        foreach (FileInfo arquivo in arquivosDirPai)
        {
            string dirTemporario = Path.Combine(DiretorioDestino, arquivo.Name);
            arquivo.CopyTo(DiretorioDestino, true);
        }

        //Se o parâmetro CopiaSubDiretorios for setado como true, irá copiar os sub-folders.
        if (CopiaSubDiretorios)
        {
            foreach (DirectoryInfo subDiretorio in dirsFilhos)
            {
                string dirTemporario = Path.Combine(DiretorioDestino, subDiretorio.Name);
                CopiaDiretorios(subDiretorio.FullName, dirTemporario, CopiaSubDiretorios);
            }
        }
    }

    /// <summary>
    /// Função booleana que realiza um teste verificando se o valor é nulo ou não.
    /// Retorna true caso o parâmetro estiver vazio e false caso estiver com algum valor.
    /// </summary>
    /// <param name="Valor">Parâmetro à ser testado com qualquer valor.</param>
    /// <returns>True caso o falor for nulo</returns>
    public Boolean IsNullValue(String Valor)
    {
        try
        {
            if (Valor.ToString() == null || String.IsNullOrEmpty(Valor.ToString()) || Valor.ToString() == "") return true;
            else return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Se o valor passado for nulo (Nothing ou DBNull), devolve o default, senão devolve o próprio valor.
    /// </summary>
    /// <param name="Valor">Contém o valor original à ser verificado</param>
    /// <param name="ValorDefault">Valor para retornar, caso o 'Valor' esteja nulo.</param>
    /// <returns></returns>
    public Object IsObjectNull(Object Valor, Object ValorDefault)
    {
        if (Valor == null || object.ReferenceEquals(Valor, System.DBNull.Value))
        {
            return ValorDefault;
        }
        else return Valor;
    }

    /// <summary>
    ///     Retona um array de byte contendo uma imagem, cujo o nome da mesma deverá ser informada como parâmetro.
    /// </summary>
    /// <param name="NomeImg">Nome da imagem sem o caminho, pois irá buscar do 'Application.StatupPath'</param>
    /// <returns>Array de byte</returns>
    public Byte[] ConverteImagemEmByteArray(String NomeImg)
    {
        String DirImg = Path.Combine(Application.StartupPath, NomeImg);

        if (File.Exists(NomeImg))
        {
            FileStream fStreamImg = File.OpenRead(DirImg);
            byte[] iconRet = new byte[fStreamImg.Length];
            fStreamImg.Read(iconRet, 0, iconRet.Length);

            return iconRet;
        }
        else throw new FileNotFoundException(string.Format("Não foi possível encontrar o arquivo '{0}'", DirImg));
    }

    /// <summary>
    ///     Converte um byte array em uma imagem (objeto Bitmap) para poder
    /// carregar ou até mesmo salvar em um local desejado.
    /// </summary>
    /// <param name="Array">Byte array contendo os dados da imagem</param>
    /// <returns>Objeto do tipo bitmap.</returns>
    /// <remarks>Para carregar a imagem do banco de dados, você pode converter
    /// uma string diretamente em array com a linha abaixo:
    /// String.IsNullOrEmpty(dt.Rows[0]["Imagem"].ToString()) ? null : (byte[])dt.Rows[0]["Imagem"];
    /// </remarks>
    public Bitmap ConverteByteArrayEmImagem(byte[] Array)
    {
        if (Array == null) return null;

        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            ms.Write(Array, 0, Array.Length);
            ms.Position = 0L;

            return new Bitmap(ms);
        }
    }

    /// <summary>
    ///     Método que abre o windows explorer e permite o usuário selecionar apenas
    /// um único arquivo, de qualquer extensão.
    /// </summary>
    /// <returns>String contendo o caminho completo do arquivo.</returns>
    public String BuscaArquivo()
    {
        OpenFileDialog Explorer = new OpenFileDialog();
        Explorer.Filter = "Todos os arquivos (*.*)|*.*";
        Explorer.Multiselect = false;

        if (Explorer.ShowDialog() == DialogResult.OK)
            return Explorer.FileName;
        //Usuário cancelou a ação.
        else { }

        return "";
    }

    /// <summary>
    ///     Método que abre o windows explorer e permite o usuário selecionar apenas
    /// um único arquivo, informando um filtro completo para pesquisa.
    /// </summary>
    /// <param name="Filtro">Ex.: Arquivos de imagem (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg</param>
    /// <returns>String contendo o caminho do arquivo selecionado</returns>
    public String BuscaArquivo(String Filtro)
    {
        if (Filtro != null && Filtro != null)
        {
            OpenFileDialog Explorer = new OpenFileDialog();
            Explorer.Filter = Filtro;
            Explorer.Multiselect = false;

            if (Explorer.ShowDialog() == DialogResult.OK)
                return Explorer.FileName;

            return "";
        }
        else throw new Exception("Extensão nem tipo para pesquisa não pode ser vazia!");
    }

    /// <summary>
    ///     Método que abre o windows explorer e permite o usuário selecionar apenas
    /// um único arquivo, de extensões específicas.
    /// </summary>
    /// <param name="ExtensaoArq">Extensão do arquivo à ser buscado pelo Windows Explorer (informar somente a extensão)</param>
    /// <param name="DescricaoArq">Descrição dos arquivos. Ex.: Planilhas do Excel</param>
    /// <returns>String contendo o caminho completo do arquivo selecionado</returns>
    public String BuscaArquivo(String ExtensaoArq, String DescricaoArq)
    {
        if (ExtensaoArq != null && DescricaoArq != null)
        {
            OpenFileDialog Explorer = new OpenFileDialog();
            Explorer.Filter = string.Format("Arquivos {0}(*.{1})|*.{1}", DescricaoArq, ExtensaoArq);
            Explorer.Multiselect = false;

            if (Explorer.ShowDialog() == DialogResult.OK)
                if (Explorer.FileName.ToUpper().EndsWith(ExtensaoArq.ToUpper().Contains('?') ? ExtensaoArq.ToUpper().Replace("?", "") : ExtensaoArq.ToUpper()))
                {
                    return Explorer.FileName;
                }

            return "";
        }
        else throw new Exception("Extensão nem tipo para pesquisa não pode ser vazia!");
    }

    /// <summary>
    ///     Método que abre o windows explorer e permite o usuário selecionar apenas
    /// um único arquivo, de extensões específicas.
    /// </summary>
    /// <param name="Titulo">Título para auxiliar o usuário. Exemplo: Selecione um ou mais arquivos nativos do SOLIDWORKS</param>
    /// <param name="MultiplasExtensoes">Exemplo (Precisa ser da mesma forma): Arquivos SOLIDWORKS (*.sldprt;*.sldasm)|*.sldprt;*.sldas</param>
    /// <param name="Params">Key (descrição): Montagens  |  Value (Extensão): sldasm</param>
    public List<String> BuscaArquivos(String Titulo, String MultiplasExtensoes = "", Dictionary<String, Object> Params = null)
    {
        List<String> ret = new List<string>();
        OpenFileDialog explorer = new OpenFileDialog();

        try
        {
            explorer.Multiselect = true;

            if (!String.IsNullOrEmpty(Titulo))
                explorer.Title = Titulo;

            if (!String.IsNullOrEmpty(MultiplasExtensoes))
                explorer.Filter = MultiplasExtensoes;
            else explorer.Filter = "Todos os arquivos (*.*)|*.*";

            if (Params != null && Params.Count > 0)
                foreach (KeyValuePair<String, Object> d in Params)
                    explorer.Filter += String.Format("|{0}(*.{1})|*.{1}", d.Key, d.Value);

            if (explorer.ShowDialog() == DialogResult.OK)
            {
                //  Array com o nome & extensão de todos os arquivos selecionados pelo usuário.
                String[] arqSelecionados = explorer.SafeFileNames;
                String dir = explorer.FileName.Substring(0, explorer.FileName.LastIndexOf("\\"));

                foreach (string s in arqSelecionados)
                {
                    ret.Add(Path.Combine(dir, s));
                }
            }
            else throw new Exception();//Retorna nulo, pois o usuário cancelou.

            return ret;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    ///     Método que abre o windows explorer e permite o usuário selecionar vários
    /// arquivos, de extensões específicas.
    /// </summary>
    /// <param name="ExtensaoArq">Extensão do arquivo à ser buscado pelo Windows Explorer.</param>
    /// <param name="DescricaoArq">Descrição dos arquivos. Ex.: Planilhas do Excel</param>
    /// <returns>Lista contendo todos os arquivos selecionados pelo usuário.</returns>
    /// <remarks>MÉTODO OBSOLETO</remarks>
    protected List<String> BuscaArquivos(String ExtensaoArq, String DescricaoArq)
    {
        List<String> lstRet = new List<string>();

        if (ExtensaoArq != null && DescricaoArq != null)
        {
            OpenFileDialog Explorer = new OpenFileDialog();

            Explorer.Filter = string.Format("Arquivos {0}(*.{1})|*.{1}", DescricaoArq, ExtensaoArq);
            Explorer.Multiselect = false;

            String[] arqSelecionados;
            String dirSelecionado;

            if (Explorer.ShowDialog() == DialogResult.OK)
            {
                //  Array com o nome & extensão de todos os arquivos selecionados pelo usuário.
                arqSelecionados = Explorer.SafeFileNames;
                dirSelecionado = Explorer.FileName.Substring(0, Explorer.FileName.LastIndexOf("\\"));

                foreach (string s in arqSelecionados)
                {
                    lstRet.Add(Path.Combine(dirSelecionado, s));
                }

                return lstRet;
            }
            return null;
        }
        else throw new Exception("Extensão nem tipo para pesquisa não pode ser vazia!");
    }

    /// <summary>
    ///     Método que abre a caixa de diálogo do windows explorer, para o usuário selecionar um diretório
    /// para carregamento e retorna o caminho do diretório selecionado.
    /// </summary>
    /// <param name="Descricao">Descrição para facilitar o entendimento do usuário.</param>
    /// <returns>Diretório completo do diretório selecionado pelo usuário</returns>
    public String BuscaDiretorio(String Descricao)
    {
        FolderBrowserDialog Explorer = new FolderBrowserDialog();
        if (Descricao != null)
        {
            Explorer.Description = string.Format("Diretório {0}", Descricao);
        }
        Explorer.ShowNewFolderButton = true;

        if (Explorer.ShowDialog() == DialogResult.OK)
            return Explorer.SelectedPath;
        else if (Convert.ToBoolean(DialogResult.Cancel))
        {
            //usuário cancelou a ação e eu não faço nada, simplesmente devolvo vazio.
        }

        return "";
    }

    /// <summary>
    ///     Método que salva um arquivo do usuário em um local determinado pelo usuário
    /// com base em um filtro fornecido como parâmetro.
    /// </summary>
    /// <param name="Conteudo">String contendo o conteúdo do arquivo  à ser salvo.</param>
    /// <param name="Filtro">Filtro para salvamento. Ex.: Arquivos de imagem (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg</param>
    /// <param name="DefinirDiretorioPadrao">True para definir o diretório padrão como DESKTOP.</param>
    /// <returns>True para caso o arquivo tenha êxito ao salvar.</returns>
    public Boolean SalvaArquivo(String Conteudo, String Filtro, Boolean DefinirDiretorioPadrao = false)
    {
        SaveFileDialog fs = new SaveFileDialog();

        if (DefinirDiretorioPadrao)
            fs.InitialDirectory = mDesktop;

        if (!String.IsNullOrEmpty(Filtro))
            fs.Filter = String.Format(Filtro);

        //if (!String.IsNullOrEmpty(Extensao.Replace(".", "")))
        //    fs.DefaultExt = Extensao.Replace(".", "");

        if (fs.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(fs.FileName, Conteudo);
            return true;
        }
        else return false;
    }

    /// <summary>
    ///     Método que salva um arquivo do usuário em um local determinado pelo usuário
    /// com base em um filtro fornecido como parâmetro.
    /// </summary>
    /// <param name="Conteudo">String contendo o conteúdo do arquivo à ser salvo.</param>
    /// <param name="ExtensaoArq">Extensão do arquivo à ser salvo (somente uma extensão e sem ponto).</param>
    /// <param name="DescricaoArq">Descrição do tipo do arquivo à ser salvo (Já possuí o texto 'Arquivos').</param>
    /// <param name="DefinirDiretorioPadrao">True para definir o diretório padrão como DESKTOP.</param>
    /// <returns>True para caso o arquivo tenha êxito ao salvar.</returns>
    public Boolean SalvaArquivo(String Conteudo, String ExtensaoArq, String DescricaoArq, Boolean DefinirDiretorioPadrao = false)
    {
        SaveFileDialog fs = new SaveFileDialog();

        if (DefinirDiretorioPadrao)
            fs.InitialDirectory = mDesktop;

        if (!String.IsNullOrEmpty(ExtensaoArq) && !String.IsNullOrEmpty(DescricaoArq))
            fs.Filter = String.Format("Arquivos {0}(*.{1})|*.{1}", DescricaoArq, ExtensaoArq);

        if (fs.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(fs.FileName, Conteudo);
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Função booleana que realiza um teste verificando se o valor é nulo ou não.
    /// Retorna true caso o parâmetro estiver vazio e false caso estiver com algum valor.
    /// </summary>
    /// <param name="Valor">Parâmetro à ser testado com qualquer valor.</param>
    /// <returns>True caso o falor for nulo</returns>
    public Boolean TesteNull(String Valor)
    {
        try
        {
            if (Valor.ToString() == null || String.IsNullOrEmpty(Valor.ToString()) || Valor.ToString() == "")
                //É nulo.
                return true;
            else
                //Não é nulo.
                return false;
        }
        catch (Exception ex)
        {
            Exception skaEx = new Exception("Erro ao executar a função para teste de valores nulos.", ex);
            return false;
        }
    }

    ///// <summary>
    ///// 	Método que obtém um caminho de rede existente, ou seja, informa como parâmetro um diretório
    ///// mapeado de rede e converte para um diretório de rede válido, o caminho universal dele.
    ///// </summary>
    ///// <param name="MappedFilePath">Caminho do arquivo contendo a unidade de rede mapeado.</param>
    ///// <returns>URL contendo o caminho universal do arquivo</returns>
    //public String GetUniversalPathFromFile(string sFilePath)
    //{
    //    string retString = "";
    //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT RemoteName FROM win32_NetworkConnection WHERE LocalName = '" + sFilePath.Substring(0, 2) + "'");

    //    foreach (ManagementObject managementObject in searcher.Get())
    //    {
    //        string sRemoteName = managementObject["RemoteName"] as string;
    //        sRemoteName += sFilePath.Substring(2);
    //        retString = new Uri(sRemoteName).ToString().ToUpper().Replace("FILE:", "");
    //    }

    //    return retString;

    //}

    #endregion

    #region Métodos estáticos

    /// <summary>
    ///     Método que verifica a conexão com a internet.
    /// </summary>
    /// <returns>True caso houver uma conexão válida, false para o contrário.</returns>
    public static Boolean CheckInternetConnection()
    {
        try
        {
            using (var client = new System.Net.WebClient())
            {
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
        }
        catch { return false; }
    }

    /// <summary>
    ///     Método que abre um arquivo no windows de acordo com o aplicativo padrão
    /// definido para abertura do arquivo (Exemplo: Paint irá abrir imagens, notepad
    /// arquivos de texto e assim em diante).
    /// </summary>
    /// <param name="Arquivo">Caminho completo do arquivo.</param>
    public static void AbreArquivo(String Arquivo)
    {
        if (File.Exists(Arquivo))
        {
            System.Diagnostics.Process.Start(Arquivo);
        }
        else MessageBox.Show(String.Format("Não foi possível abrir o arquivo '{0}'.", Arquivo), "Arquivo não localizado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Obtém o IP global do computador (caso possuir internet).
    /// </summary>
    /// <returns>String contendo o IP ou vazio caso ocorreu alguma exceção.</returns>
    public static String GetGlobalIpAddress()
    {
        try
        {
            return new System.Net.WebClient().DownloadString("http://icanhazip.com").Replace("\n", "");
        }
        catch { return ""; }
    }

    /// <summary>
    ///     Obtém o IP da rede do computador.
    /// </summary>
    /// <returns>String contendo o IP ou vazio caso ocorreu alguma exceção.</returns>
    public static String GetLocalIpAddress()
    {
        try
        {
            using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                System.Net.IPEndPoint endPoint = socket.LocalEndPoint as System.Net.IPEndPoint;
                return endPoint.Address.ToString();
            }
        }
        catch { return ""; }
    }

    /// <summary>
    ///     Formata valores muito grandes para texto.
    /// </summary>
    /// <param name="Number">Número grande (Caso for um valor de tipo float/double, apenas converter utilizando 'Converter.ToInt64("Valor")'</param>
    /// <returns>String contendo a formatação do valor</returns>
    public static String FormatLargeNumbers(Int64 Number)
    {
        //Help link:
        //https://stackoverflow.com/questions/30180672/string-format-numbers-to-millions-thousands-with-rounding

        // Ensure number has max 3 significant digits (no rounding up can happen)
        long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(Number) - 2));
        Number = Number / i * i;

        if (Number >= 1000000000)
            return (Number / 1000000000D).ToString("0.##") + "B";
        if (Number >= 1000000)
            return (Number / 1000000D).ToString("0.##") + "M";
        if (Number >= 1000)
            return (Number / 1000D).ToString("0.##") + "K";

        return Number.ToString("#,0000");
    }

    #endregion

}