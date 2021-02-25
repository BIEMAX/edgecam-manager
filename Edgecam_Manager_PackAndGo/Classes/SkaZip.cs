/***********************************************************************************************************
 * 
 * 
 *              SkaZip - Classe com métodos para compactação ou extração de arquivos.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Criar arquivo compactado, extrair arquivos ou adicionar arquivos.
 *      Version:    1.0
 *      Date:       19/02/2018, at 10:44 AM
 *      Note:       <None>
 *      History:    Update      - 19/02/2018 - 10:44 AM - Concluído as atribuições dos valores nas propriedades e comentários nos métodos - V1.0 Lançada.
 * 
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
//Compressão de arquivos.
using System.IO.Compression;

internal class SkaZip
{

    #region Global Variables

    /// <summary>
    ///     Diretório que será criado o arquivo ZIP.
    /// </summary>
    protected String mFilePath;

    /// <summary>
    ///     Nome do arquivo ZIP.
    /// </summary>
    protected String mFileName;

    /// <summary>
    ///     Contpem o caminho completo do arquivo ZIP.
    /// </summary>
    protected String mFilePathZip;

    /// <summary>
    ///     Contém a extensão à ser compactado os arquivos.
    /// </summary>
    protected ExtensionZip mExt;

    /// <summary>
    ///     Contém a definição de sobrescrever o arquivo ZIP caso o mesmo já exista (no caso
    /// de instanciar o objeto para criar uma nova pasta compactada.
    /// </summary>
    protected Boolean mOverwriteFile;

    #endregion

    #region Properties

    /// <summary>
    ///     Propriedade que contém o diretório que o arquivo ZIP está/estará (em caso de o mesmo ser criado).
    /// </summary>
    public String _FilePath
    {
        get
        {
            return mFilePath.ToUpper().Trim();
        }
        set
        {
            mFilePath = value;
        }
    }

    /// <summary>
    ///     Propriedade que contém o nome do arquivo ZIP (Com ou sem extensão).
    /// </summary>
    public String _FileName
    {
        get
        {
            return mFileName.ToUpper().Trim();
        }
        set
        {
            mFileName = value;
        }
    }

    /// <summary>
    ///     Propriedade somente leitura que possuí o caminho completo do arquivo compactado.
    /// </summary>
    public String _FilePathZip
    {
        get
        {
            return mFilePathZip.ToUpper().Trim();
        }
    }

    /// <summary>
    ///     Propriedade possuí a extensão do arquivo compactado.
    /// </summary>
    public ExtensionZip _Extension
    {
        get
        {
            return mExt;
        }
        set
        {
            mExt = value;
        }
    }

    /// <summary>
    ///     Enumerador para compactar em uma determinada extensão.
    /// </summary>
    public enum ExtensionZip
    {
        /// <summary>
        ///     Extensão nativa de arquivos compactados do Windows
        /// </summary>
        Zip,
        /// <summary>
        ///     Extensão 7ZIP. Requer um aplicativo para visualizar os arquivos.
        /// </summary>
        Zip_7,
        /// <summary>
        ///     Extensão própria da aplicação.
        /// </summary>
        SkaZip,
        /// <summary>
        ///     Extensão do WinRar.
        /// </summary>
        Rar        
    }

    #endregion


    /// <summary>
    ///     Método que instância o objeto que cria um novo arquivo ZIP.
    /// </summary>
    /// <param name="FilePath">Caminho em que o arquivo ZIP será criado</param>
    /// <param name="FileName">Nome do arquivo ZIP (sem extensão)</param>
    /// <param name="Extension">Extensão para compactar o arquivo</param>
    public SkaZip(String FilePath, String FileName, ExtensionZip Extension, Boolean OverwriteExistingFile)
    {
        try
        {
            _FilePath = FilePath;
            _FileName = FileName;
            _Extension = Extension;
            mOverwriteFile = OverwriteExistingFile;

            if (ValidatePath())
                CreateNewZipFile();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao criar o arquivo compactado.", ex);
        }
    }

    /// <summary>
    ///     Método que instância o objeto para apenas extrair ou adicionar arquivos.
    /// </summary>
    /// <param name="ZipFile">Caminho completo do arquivo compactado</param>
    public SkaZip(String ZipFilePath)
    {
        try
        {
            ValidateZipFile(ZipFilePath);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar ler o arquivo compactado.", ex);
        }
    }

    /// <summary>
    ///     Destrutor da classe.
    /// </summary>
    ~SkaZip() { }



    #region Methods

    /// <summary>
    ///     Método que valida a existência do diretório que o usuário informou como parâmetro
    /// no momento da instanciação do objeto 'SkaZip'.
    /// </summary>
    /// <returns>True caso estiver tudo certo, false caso acontecer alguma exceção.</returns>
    private Boolean ValidatePath()
    {
        try
        {
            if (!Directory.Exists(mFilePath))
                Directory.CreateDirectory(mFilePath);

            return true;
        }
        catch (PathTooLongException pEx)
        {
            throw new PathTooLongException("O nome da pasta é longo demais", pEx);
        }
        catch (IOException iEx)
        {
            throw new IOException("A pasta 'pai' da pasta que estamos tentando criar é somente leitura.", iEx);
        }
        catch (ArgumentNullException aEx)
        {
            throw new ArgumentNullException("O nome da pasta é 'NULL'", aEx);
        }
    }

    /// <summary>
    ///     Método que valida se o arquivo ZIP está correto.
    /// </summary>
    private void ValidateZipFile(String ZipFilePath)
    {
        try
        {
            if (ZipFilePath != "" && ZipFilePath.ToUpper() != "NULL" && ZipFilePath.ToUpper().Contains("\\"))
            {
                if (File.Exists(ZipFilePath))
                {
                    _FileName = ZipFilePath.Substring(ZipFilePath.LastIndexOf("\\") + 1);
                    _FilePath = ZipFilePath.Substring(0, ZipFilePath.LastIndexOf("\\") + 1);
                    mFilePathZip = ZipFilePath;

                    //Se a extensão não for válida, gero uma exceção, caso contrário, está tudo certo para o usuário trabalhar com o arquivo.
                    if (!ValidadeZipFileExtension(mFilePathZip.Substring(mFilePathZip.LastIndexOf(".") + 1)))
                        throw new NotSupportedException("A extensão do arquivo compactado não é suportado pelo sistema!");
                }
                else throw new FileNotFoundException(String.Format("Não foi possível localizar o arquivo '{0}'.", ZipFilePath));
            }
            else throw new ArgumentNullException("O caminho do arquivo zip está vazio, contém 'NULL' em seu nome ou ainda não é um diretório válido do Windows.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    /// <summary>
    ///     Método que valida a extensão do arquivo compactado.
    /// </summary>
    /// <param name="Extension">Extensão do arquivo.</param>
    /// <returns>True caso estiver em uma extensão válida, false caso contrário.</returns>
    private Boolean ValidadeZipFileExtension(String Extension)
    {
        try
        {
            //Troco a extensão, pois em programação, não é possível criar variáveis com numéricos na frente de letras. Ex.: String 7ZIP = "";
            Extension = Extension.ToUpper().EndsWith("7ZIP") == true ? Extension.ToUpper().Trim().Replace("7ZIP", "ZIP_7") : Extension;

            switch ((ExtensionZip)Enum.Parse(typeof(ExtensionZip), Extension))
            {
                case ExtensionZip.Zip: return true;
                case ExtensionZip.Zip_7: return true;
                case ExtensionZip.SkaZip: return true;
                case ExtensionZip.Rar: return true;
                default: return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Cria um novo arquivo ZIP de acordo com as especificações do usuário.
    /// </summary>
    private void CreateNewZipFile()
    {
        try
        {
            mFilePathZip = Path.Combine(mFilePath, String.Format("{0}.{1}", mFileName, mExt.ToString() == "Zip_7" ? "7Zip" : mExt.ToString()));

            if (File.Exists(mFilePathZip))
            {
                if (mOverwriteFile)
                {
                    //Deleta o arquivo existente.
                    File.Delete(mFilePathZip);

                    using (File.Create(mFilePathZip))
                    GC.Collect();
                }                    
                else throw new IOException("Um arquivo compactado já existe no diretório especificado, no entanto, foi definido para não sobrescrevê-lo.");
            }
            else
            {
                using (File.Create(mFilePathZip))
                GC.Collect();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Propriedade que define se o arquivo ZIP (caso) existente está definido como não substituir arquivo.", ex);
        }
    }

    /// <summary>
    ///     Método que permite adicionar um arquivo dentro do arquivo compactado.
    /// </summary>
    /// <param name="FilePath">Caminho do arquivo à ser adicionado.</param>
    public void AddFile(String FilePath)
    {
        //TODO: Verificar se o arquivo possui caracteres especiais e substituí-los pelos caracteres naturais.
        try
        {
            if (FilePath != "" && FilePath.ToUpper() != "NULL")
            {
                if (File.Exists(FilePath))
                {
                    //Caso der erro de referência abaixo, adicionar as seguintes referências na solution: System.IO.Compression 
                    //                                                                                    System.IO.Compression.FileSystem
                    using (var zip = ZipFile.Open(mFilePathZip, System.IO.Compression.ZipArchiveMode.Update))
                    {
                        var fileInfo = new FileInfo(FilePath);
                        zip.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                    }
                }
            }
        }
        catch
        {

        }
    }

    /// <summary>
    ///     Método que extraí os arquivos da pasta compactada.
    /// </summary>
    /// <param name="DestPath">Diretório de destino dos arquivos extraídos</param>
    /// <returns>True caso houve êxito ao descompactar os arquivos</returns>
    public Boolean ExtractFilesToDirectory(String DestPath)
    {
        try
        {
            if (DestPath != ""&& DestPath.ToUpper().Trim().Contains("\\"))
            {
                if (!Directory.Exists(DestPath))
                    Directory.CreateDirectory(DestPath);

                //Testa as permissões na pasta. Ele irá continuar caso o usuário corrente tenhas as devidas permissões.
                File.GetAccessControl(DestPath);

                //Não usar esse método, cria exceção caso o arquivo já exista.
                //ZipFile.ExtractToDirectory(mFilePathZip, DestPath);

                //Utilizar o using para que o arquivo não fique preso na memória da aplicação.
                using (ZipArchive zip = ZipFile.OpenRead(mFilePathZip))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        entry.ExtractToFile(Path.Combine(DestPath, entry.FullName), true);
                    }
                }

                //Limpa a memória utilizada pelo sistema.
                GC.Collect();

                return true;
            }
            else return false;
        }
        catch (UnauthorizedAccessException uEx)
        {
            throw new UnauthorizedAccessException("O usuário não possuí permissões no diretório informado para descompactação do arquivo.", uEx);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion
}