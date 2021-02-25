/*****************************************************************************************************************************
 * 
 * 
 *                  SkaException - Classe com métodos específicos para tratar erros
 *                                      não gerenciados pela aplicação.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe para erros não gerenciados.
 *      Version:    1.4
 *      Date:       09/08/2017, at 12:07 AM
 *      Note:       <None>
 *      History:    Update - 09/08/2017 - 03:25 PM - Adicionado um novo parâmetro no construtor da classe - V1.1 Lançada
 *                  Update - 05/12/2017 - 01:15 PM - Adicionado um novo construtor na classe, permitindo o salvamento
 *                      das informações em um banco de dados - V1.2 Lançada.
 *                  Update - 14/08/2018 - 04:07 PM - Adicionado um novo parâmetro na instanciacao da classe 'SkaException' -
 *                      V1.3 Lançada.
 *                  Update - 21/08/2018 - 12:41 AM - Refatorado toda a classe - V1.4 Lançada.
 *      
 *****************************************************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

//Para Criptografia
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

/// <summary>
///     Classe que trata erros não gerenciados por aplicações.
/// </summary>
internal class SkaException
{

    #region Variáveis Globais

    private Exception mEx;
    private String mMsg;
    private String mDirArqLog;
    private String mUsuario = Environment.UserName;
    private String mApplicatioName = Application.ProductName;
    /// <summary>
    ///     Contém o Type da exception.
    /// </summary>
    private String mFirstExeType;
    /// <summary>
    ///     Contém o number da exception.
    /// </summary>
    private String mFirstExeCode;

    private Boolean mCriptografar;

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o caminho à ser salvo o arquivo de log.
    /// </summary>
    public String _DiretorioArquivoLog
    {
        get
        {
            return mDirArqLog;
        }
        set
        {
            mDirArqLog = value;
        }
    }

    #endregion

    #region Instância dos objetos da classe


    /// <summary>
    ///     Método para acessar a classe somente.
    /// </summary>
    public SkaException() { }

    /// <summary>
    ///     Método que obtém uma exceção não tratada e cria uma arquivo de log.
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="MsgAuxiliar"></param>
    /// <param name="CriptografarArqLog"></param>
    public SkaException(Exception ex, String MsgAuxiliar, Boolean CriptografarArqLog = false)
    {
        try
        {
            mEx = ex;
            mMsg = MsgAuxiliar;

            mCriptografar = CriptografarArqLog;

            SalvaExcecao();
        }
        catch
        {
            //  Não deve ser adicionado nada aqui, pois no método recursivo TrataExcecao,
            //ele já faz isso automaticamente.
        }
    }

    /// <summary>
    /// Método destrutivo da classe.
    /// </summary>
    ~SkaException() { }

    #endregion

    #region Métodos

    /// <summary>
    ///     Método que inicia o salvamento da exceção não tratada.
    /// </summary>
    private void SalvaExcecao()
    {
        try
        {
            TrataExcecao(mEx, null, 0);
        }
        catch
        {
            mDirArqLog = "[Não foi possível salvar o arquivo de log de erros. Contate o administrador do sistema na SKA.]";
        }
    }

    /// <summary>
    ///     Método que trata a exceção gerada.
    /// </summary>
    /// <param name="Ex">Exceção não tratada</param>
    /// <param name="DsEx">DataSet que possui os dados da exceção</param>
    /// <param name="Nivel">Nível da exceção (caso ocorra exceções derivadas de outras)</param>
    private void TrataExcecao(Exception Ex, DataSet DsEx, int Nivel)
    {
        if (DsEx == null) DsEx = new DataSet();

        DataTable dt = new DataTable("Erro nível " + Nivel);
        DataRow r;

        //Adiciona uma coleção ao DataTable para criar uma arquivo XML.
        dt.Columns.Add("HelpLink");
        dt.Columns.Add("Message");
        dt.Columns.Add("Source");
        dt.Columns.Add("StackTrace");
        dt.Columns.Add("MsgAuxiliar");
        dt.Columns.Add("Username");
        dt.Columns.Add("Application");

        //Tratativa de erros para banco de dados.
        if (Ex.GetType().ToString() == "Oracle.DataAccess.Client.OracleException" || Ex.GetType().ToString() == "SqlClient.SqlException")
            dt.Columns.Add("ErrorCode");

        DsEx.Tables.Add(dt);
        r = dt.NewRow();

        r["HelpLink"] = Ex.HelpLink;
        r["Message"] = Ex.Message;
        r["Source"] = Ex.Source;
        r["StackTrace"] = Ex.StackTrace;
        r["MsgAuxiliar"] = Ex;
        r["Username"] = mUsuario;
        r["Application"] = mApplicatioName;

        //Caso ser um erro provido em um banco de dados, pego o número (para abrir uma investigação posteriormente).
        if (Ex.GetType().ToString() == "Oracle.DataAccess.Client.OracleException")
        {
            r["ErrorCode"] = Ex.GetType().GetProperty("Number");

            mFirstExeType = Ex.GetType().ToString();
            mFirstExeCode = Ex.GetType().GetProperty("Number").ToString();
        }

        else if (Ex.GetType().ToString() == "SqlClient.SqlException")
        {
            r["ErrorCode"] = ((SqlException)Ex).ErrorCode;

            mFirstExeType = Ex.GetType().ToString();
            mFirstExeCode = ((SqlException)Ex).ErrorCode.ToString();
        }


        dt.Rows.Add(r);

        //Método recursivo, onde, se o nível (exceptions geradas) for maior que um, salvo no arquivo XML a nova exceção.
        if (Ex.InnerException != null && Nivel < 5)
            TrataExcecao(Ex.InnerException, DsEx, Nivel + 1);

        //Gera arquivo físico
        if (Nivel == 0)
        {
            mDirArqLog = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".SkaErro");
            DsEx.WriteXml(mDirArqLog);

            if (mCriptografar)
            {
                SkaEncrypt skaEx = new SkaEncrypt(SkaEncrypt.CryptProvider.DES, "DD01039582dd", mDirArqLog);
            }
        }
    }

    /// <summary>
    ///     Método que mostra uma mensagem de erro para o usuário.
    /// </summary>
    public void ApresentaMensagemUsuario()
    {

        MessageBox.Show(
                        string.Format("{0}{1}{2}{3}Arquivo com informações para suporte salvo em {4}",
                                      mMsg,
                                      System.Environment.NewLine,
                                      mEx.Message,
                                      System.Environment.NewLine,
                                      mDirArqLog),
                         "Erro",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error);

    }

    #endregion

}




/// <summary>
///     Classe que encripta um arquivo (qualquer extensão).
/// </summary>
internal class SkaEncrypt
{

    private String _key = String.Empty;
    private CryptProvider _cryptProvider;
    private SymmetricAlgorithm _algorithm;
    private String _filePath;


    /// <summary>
    /// Chave secreta para o algoritmo simétrico de criptografia.
    /// </summary>
    public string Key
    {
        get { return _key; }
        set { _key = value; }
    }

    /// <summary>
    /// Enumerator com os tipos de classes para criptografia.
    /// </summary>
    public enum CryptProvider
    {
        /// <summary>
        /// Representa a classe base para implementações criptografia dos algoritmos simétricos Rijndael.
        /// </summary>
        Rijndael,
        /// <summary>
        /// Representa a classe base para implementações do algoritmo RC2.
        /// </summary>
        RC2,
        /// <summary>
        /// Representa a classe base para criptografia de dados padrões (DES - Data Encryption Standard).
        /// </summary>
        DES,
        /// <summary>
        /// Representa a classe base (TripleDES - Triple Data Encryption Standard).
        /// </summary>
        TripleDES
    }


    /// <summary>
    ///     Construtor com o tipo de criptografia a ser usada.
    /// </summary>
    /// <param name="cryptProvider">Tipo de criptografia.</param>
    /// <param name="ChaveSimetrica">Chave para critografar o arquivo.</param>
    /// <param name="FilePath">Caminho completo do arquivo a ser encriptado.</param>
    /// <param name="Descriptografar">Quando setado como true, informa à classe que não é para
    /// criptografar o arquivo, e sim descriptografá-lo.</param>
    public SkaEncrypt(CryptProvider cryptProvider, String ChaveSimetrica, String FilePath, Boolean Descriptografar = false)
    {
        // Seleciona algoritmo simétrico
        switch (cryptProvider)
        {

            case CryptProvider.Rijndael:
                _algorithm = new RijndaelManaged();
                _cryptProvider = CryptProvider.Rijndael;
                break;

            case CryptProvider.RC2:
                _algorithm = new RC2CryptoServiceProvider();
                _cryptProvider = CryptProvider.RC2;
                break;

            case CryptProvider.DES:
                _algorithm = new DESCryptoServiceProvider();
                _cryptProvider = CryptProvider.DES;
                break;

            case CryptProvider.TripleDES:
                _algorithm = new TripleDESCryptoServiceProvider();
                _cryptProvider = CryptProvider.TripleDES;
                break;

        }
        _algorithm.Mode = CipherMode.CBC;
        _key = ChaveSimetrica;
        _filePath = FilePath;

        if (!Descriptografar)
            EncriptarArquivo();
    }


    /// <summary>
    /// Inicialização do vetor do algoritmo simétrico
    /// </summary>
    private void SetIV()
    {
        switch (_cryptProvider)
        {
            case CryptProvider.Rijndael:
                _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                break;

            default:
                _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                break;
        }
    }

    /// <summary>
    /// Gera a chave de criptografia válida dentro do array.
    /// </summary>
    /// <returns>Chave com array de bytes.</returns>
    public virtual byte[] GetKey()
    {
        string salt = string.Empty;

        // Ajusta o tamanho da chave se necessário e retorna uma chave válida
        if (_algorithm.LegalKeySizes.Length > 0)
        {
            // Tamanho das chaves em bits
            int keySize = _key.Length * 8;

            int minSize = _algorithm.LegalKeySizes[0].MinSize;

            int maxSize = _algorithm.LegalKeySizes[0].MaxSize;

            int skipSize = _algorithm.LegalKeySizes[0].SkipSize;


            if (keySize > maxSize)
            {
                // Busca o valor máximo da chave

                _key = _key.Substring(0, maxSize / 8);
            }

            else if (keySize < maxSize)
            {

                // Seta um tamanho válido
                int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                if (keySize < validSize)
                {
                    // Preenche a chave com arterisco para corrigir o tamanho
                    _key = _key.PadRight(validSize / 8, '*');
                }
            }
        }

        PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(salt));
        return key.GetBytes(_key.Length);

    }

    /// <summary>
    ///     Criptografa um arquivo.
    /// </summary>
    public void EncriptarArquivo()
    {
        if (File.Exists(_filePath))
        {
            String content = File.ReadAllText(_filePath);

            byte[] plainByte = Encoding.UTF8.GetBytes(content);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            _algorithm.Key = keyByte;

            SetIV();

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();

            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

            // Grava os dados criptografados no MemoryStream
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();

            // Busca o tamanho dos bytes encriptados
            byte[] cryptoByte = _memoryStream.ToArray();

            // Converte para a base 64 string para uso posterior em um xml.
            String contentCripty = Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));

            //Fecho o memory stream.
            _memoryStream.Close();
            _cryptoStream.Close();

            //Salva o conteúdo no mesmo arquivo.
            File.Delete(_filePath);
            File.AppendAllText(_filePath, contentCripty);

        }
    }

    /// <summary>
    ///     Descriptografa um arquivo.
    /// </summary>
    public void DesencriptarArquivo()
    {
        if (File.Exists(_filePath))
        {
            String content = File.ReadAllText(_filePath);

            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(content);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            _algorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);

                String contentDecrypt = _streamReader.ReadToEnd();

                //Salva o conteúdo no mesmo arquivo.
                //Salva o conteúdo no mesmo arquivo.
                File.Delete(_filePath);
                File.AppendAllText(_filePath, contentDecrypt);
            }
            catch { }
        }
    }

    /// <summary>
    ///     Retorna o conteúdo de um arquivo desencriptografado em uma string.
    /// </summary>
    /// <returns>String.</returns>
    public String RetornaConteudoDesencriptografado()
    {
        if (File.Exists(_filePath))
        {
            String content = File.ReadAllText(_filePath);

            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(content);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            _algorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);

                return _streamReader.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }
        else return "";
    }
}