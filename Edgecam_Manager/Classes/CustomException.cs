/*****************************************************************************************************************************
 * 
 * 
 *                  CustomException - Classe com métodos específicos para tratar erros
 *                                      não gerenciados pela aplicação.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe para erros não gerenciados.
 *      Version:    2.0
 *      Date:       09/08/2017, at 12:07 AM
 *      Note:       <None>
 *      History:    Update - 09/08/2017 - 03:25 PM - Adicionado um novo parâmetro no construtor da classe - V1.1 Lançada
 *                  Update - 05/12/2017 - 01:15 PM - Adicionado um novo construtor na classe, permitindo o salvamento
 *                      das informações em um banco de dados - V1.2 Lançada.
 *                  Update - 14/08/2018 - 04:07 PM - Adicionado um novo parâmetro na instanciacao da classe 'CustomException' - V1.3 Lançada.
 *                  Update - 21/08/2018 - 12:41 AM - Refatorado toda a classe - V1.4 Lançada.
 *                  Update - 21/08/2019 - 02:44 PM - Alterado a propriedade 'CustomEncrypt.Key' para 'CustomEncrypt._Key' - V1.5 lançado
 *                  Update - 21/08/2019 - 03:08 PM - Adicionado uma nova instância da classe 'CustomEncrypt' - V1.6 Lançada.
 *                  Update - 21/08/2019 - 03:15 PM - Adicionado o método 'CustomEncrypt.EncryptString' - V1.7 Lançada.
 *                  Update - 26/08/2019 - 12:42 AM - Adicionado o método 'DecryptString' - V1.8 Lançada.
 *                  Update - 26/08/2019 - 12:52 AM - Corrigido um problema no método 'DecryptString' - V1.9 Lançada.
 *                  Update - 26/09/2019 - 01:05 AM - Corrigido um problema no método 'DecryptString' - V2.0 Lançada.
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
public class CustomException
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
    public CustomException() { }

    /// <summary>
    ///     Método que obtém uma exceção não tratada e cria uma arquivo de log.
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="MsgAuxiliar"></param>
    /// <param name="CriptografarArqLog"></param>
    public CustomException(Exception ex, String MsgAuxiliar, Boolean CriptografarArqLog = false)
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
    ~CustomException() { }

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
                CustomEncrypt skaEx = new CustomEncrypt(CustomEncrypt.CryptProvider.DES, "DD01039582dd", mDirArqLog);
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
public class CustomEncrypt
{

    #region Variáveis globais

    private String mKey = String.Empty;
    private CryptProvider mCryptProvider;
    private SymmetricAlgorithm mAlgorithm;
    private String mFilePath;

    #endregion

    #region Propriedades

    /// <summary>
    /// Chave secreta para o algoritmo simétrico de criptografia.
    /// </summary>
    public string _Key
    {
        get { return mKey; }
        set { mKey = value; }
    }

    #endregion

    #region Enumeradores

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

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Construtor com o tipo de criptografia a ser usada.
    /// </summary>
    /// <param name="cryptProvider">Tipo de criptografia.</param>
    /// <param name="ChaveSimetrica">Chave para critografar o arquivo.</param>
    /// <param name="FilePath">Caminho completo do arquivo a ser encriptado.</param>
    /// <param name="Descriptografar">Quando setado como true, informa à classe que não é para
    /// criptografar o arquivo, e sim descriptografá-lo.</param>
    public CustomEncrypt(CryptProvider cryptProvider, String ChaveSimetrica, String FilePath, Boolean Descriptografar = false)
    {
        // Seleciona algoritmo simétrico
        switch (cryptProvider)
        {

            case CryptProvider.Rijndael:
                mAlgorithm = new RijndaelManaged();
                mCryptProvider = CryptProvider.Rijndael;
                break;

            case CryptProvider.RC2:
                mAlgorithm = new RC2CryptoServiceProvider();
                mCryptProvider = CryptProvider.RC2;
                break;

            case CryptProvider.DES:
                mAlgorithm = new DESCryptoServiceProvider();
                mCryptProvider = CryptProvider.DES;
                break;

            case CryptProvider.TripleDES:
                mAlgorithm = new TripleDESCryptoServiceProvider();
                mCryptProvider = CryptProvider.TripleDES;
                break;

        }
        mAlgorithm.Mode = CipherMode.CBC;
        mKey = ChaveSimetrica;
        mFilePath = FilePath;

        if (!Descriptografar)
            EncryptFile();
    }

    /// <summary>
    ///     Construtor utilizando apenas para criptografar textos (sem arquivos físicos).
    /// </summary>
    /// <param name="cryptProvider">Tipo de criptografia.</param>
    /// <param name="ChaveSimetrica">Chave para critografar o arquivo.</param>
    public CustomEncrypt(CryptProvider cryptProvider, String ChaveSimetrica)
    {
        // Seleciona algoritmo simétrico
        switch (cryptProvider)
        {

            case CryptProvider.Rijndael:
                mAlgorithm = new RijndaelManaged();
                mCryptProvider = CryptProvider.Rijndael;
                break;

            case CryptProvider.RC2:
                mAlgorithm = new RC2CryptoServiceProvider();
                mCryptProvider = CryptProvider.RC2;
                break;

            case CryptProvider.DES:
                mAlgorithm = new DESCryptoServiceProvider();
                mCryptProvider = CryptProvider.DES;
                break;

            case CryptProvider.TripleDES:
                mAlgorithm = new TripleDESCryptoServiceProvider();
                mCryptProvider = CryptProvider.TripleDES;
                break;

        }
        mAlgorithm.Mode = CipherMode.CBC;
        mKey = ChaveSimetrica;
        mFilePath = "";
    }

    #endregion

    #region Métodos privados

    /// <summary>
    /// Inicialização do vetor do algoritmo simétrico
    /// </summary>
    private void SetIV()
    {
        switch (mCryptProvider)
        {
            case CryptProvider.Rijndael:
                mAlgorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                break;

            default:
                mAlgorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                break;
        }
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    /// Gera a chave de criptografia válida dentro do array.
    /// </summary>
    /// <returns>Chave com array de bytes.</returns>
    public virtual byte[] GetKey()
    {
        string salt = string.Empty;

        // Ajusta o tamanho da chave se necessário e retorna uma chave válida
        if (mAlgorithm.LegalKeySizes.Length > 0)
        {
            // Tamanho das chaves em bits
            int keySize = mKey.Length * 8;

            int minSize = mAlgorithm.LegalKeySizes[0].MinSize;

            int maxSize = mAlgorithm.LegalKeySizes[0].MaxSize;

            int skipSize = mAlgorithm.LegalKeySizes[0].SkipSize;


            if (keySize > maxSize)
            {
                // Busca o valor máximo da chave

                mKey = mKey.Substring(0, maxSize / 8);
            }

            else if (keySize < maxSize)
            {

                // Seta um tamanho válido
                int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                if (keySize < validSize)
                {
                    // Preenche a chave com arterisco para corrigir o tamanho
                    mKey = mKey.PadRight(validSize / 8, '*');
                }
            }
        }

        PasswordDeriveBytes key = new PasswordDeriveBytes(mKey, ASCIIEncoding.ASCII.GetBytes(salt));
        return key.GetBytes(mKey.Length);

    }

    /// <summary>
    ///     Criptografa um arquivo.
    /// </summary>
    public void EncryptFile()
    {
        if (File.Exists(mFilePath))
        {
            String content = File.ReadAllText(mFilePath);

            byte[] plainByte = Encoding.UTF8.GetBytes(content);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            mAlgorithm.Key = keyByte;

            SetIV();

            // Interface de criptografia / Cria objeto de criptografia
            ICryptoTransform cryptoTransform = mAlgorithm.CreateEncryptor();

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
            File.Delete(mFilePath);
            File.AppendAllText(mFilePath, contentCripty);

        }
    }

    /// <summary>
    ///     Descriptografa um arquivo.
    /// </summary>
    public void DecryptFile()
    {
        if (File.Exists(mFilePath))
        {
            String content = File.ReadAllText(mFilePath);

            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(content);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            mAlgorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = mAlgorithm.CreateDecryptor();

            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);

                String contentDecrypt = _streamReader.ReadToEnd();

                //Salva o conteúdo no mesmo arquivo.
                //Salva o conteúdo no mesmo arquivo.
                File.Delete(mFilePath);
                File.AppendAllText(mFilePath, contentDecrypt);
            }
            catch { }
        }
    }

    /// <summary>
    ///     Retorna o conteúdo de um arquivo desencriptografado em uma string.
    /// </summary>
    /// <returns>String.</returns>
    public String GetDecrypFileContent()
    {
        if (File.Exists(mFilePath))
        {
            String content = File.ReadAllText(mFilePath);

            // Converte a base 64 string em num array de bytes
            byte[] cryptoByte = Convert.FromBase64String(content);
            byte[] keyByte = GetKey();

            // Seta a chave privada
            mAlgorithm.Key = keyByte;
            SetIV();

            // Interface de criptografia / Cria objeto de descriptografia
            ICryptoTransform cryptoTransform = mAlgorithm.CreateDecryptor();

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

    /// <summary>
    ///     Método responsável por criptografar um texto e devolvê-lo.
    /// </summary>
    /// <param name="Content">Conteúdo a criptografar</param>
    /// <param name="AddCode">True para adicionar um prefixo e sufixo.</param>
    /// <param name="Code">Código do prefixo e sufixo</param>
    /// <returns>Texto codificado.</returns>
    public String EncryptString(String Content, Boolean AddCode = false, String Code = "#")
    {
        byte[] plainByte = Encoding.UTF8.GetBytes(Content);
        byte[] keyByte = GetKey();

        // Seta a chave privada
        mAlgorithm.Key = keyByte;

        SetIV();

        // Interface de criptografia / Cria objeto de criptografia
        ICryptoTransform cryptoTransform = mAlgorithm.CreateEncryptor();

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

        return AddCode ? String.Format("{0}{1}{0}", Code, contentCripty) : contentCripty;
    }

    /// <summary>
    ///     Método responsável por descriptografar um texto e devolvê-lo.
    /// </summary>
    /// <param name="Content">Conteúdo a descriptografar</param>
    /// <param name="HasCode">True para considerar que existe um prefixo e sufixo.</param>
    /// <param name="Code">Código do prefixo e sufixo</param>
    /// <returns>Texto decodificado</returns>
    public String DecryptString(String Content, Boolean HasCode = false, String Code = "#")
    {
        // Converte a base 64 string em num array de bytes
        byte[] cryptoByte = Convert.FromBase64String(Content);
        byte[] keyByte = GetKey();

        // Seta a chave privada
        mAlgorithm.Key = keyByte;
        SetIV();

        // Interface de criptografia / Cria objeto de descriptografia
        ICryptoTransform cryptoTransform = mAlgorithm.CreateDecryptor();

        MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
        CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

        // Busca resultado do CryptoStream
        StreamReader _streamReader = new StreamReader(_cryptoStream);

        String contentDecrypt = _streamReader.ReadToEnd();

        return HasCode ? String.Format("{1}", contentDecrypt.Replace(Code, "")) : contentDecrypt;
    }

    #endregion
}