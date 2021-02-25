using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
///     Contém a configuração do sistema atualmente. Essa configuração
/// é identificada/obtida a partir de um arquivo de configuração.
///     Esta classe é um objeto estático no sistema.
/// </summary>
internal class Config
{

    #region Variáveis privadas globais da classe

    //Atributos relacionados à conexão com o banco de dados do Edgecam
    private String mEcServer;
    private String mEcDataBase;
    private String mEcUser;
    private String mEcPass;
    private String mEcStringConnectionSql;
    //Atributos relacionados à conexão com o banco de dados intermediário
    private String mAuxServer;
    private String mAuxDataBase;
    private String mAuxUser;
    private String mAuxPass;
    private String mAuxStringConnectionSql;
    /// <summary>
    ///     Contém o idioma definido para a interface.
    /// </summary>
    private System.Globalization.CultureInfo mIdioma;
    /// <summary>
    ///     Contém o thema que será apresentado na interface.
    /// </summary>
    private String mTheme;

    #endregion

    #region Propriedades da classe (Properties)

    public String _EcServer
    {
        get
        {
            return mEcServer.ToUpper().Trim();
        }
        set
        {
            mEcServer = value;
        }
    }

    public String _EcDataBase
    {
        get
        {
            return mEcDataBase.ToUpper().Trim();
        }
        set
        {
            mEcDataBase = value;
        }
    }

    public String _EcUser
    {
        get
        {
            return mEcUser.ToUpper().Trim();
        }
        set
        {
            mEcUser = value;
        }
    }

    public String _EcPass
    {
        get
        {
            return mEcPass.ToUpper().Trim();
        }
        set
        {
            mEcPass = value;
        }
    }

    public String _EcStringConnectionSql
    {
        get
        {
            return mEcStringConnectionSql.ToUpper().Trim();
        }
        set
        {
            mEcStringConnectionSql = value;
        }
    }



    public String _AuxServer
    {
        get
        {
            return mAuxServer.ToUpper().Trim();
        }
        set
        {
            mAuxServer = value;
        }
    }

    public String _AuxDataBase
    {
        get
        {
            return mAuxDataBase.ToUpper().Trim();
        }
        set
        {
            mAuxDataBase = value;
        }
    }

    public String _AuxUser
    {
        get
        {
            return mAuxUser.ToUpper().Trim();
        }
        set
        {
            mAuxUser = value;
        }
    }

    public String _AuxPass
    {
        get
        {
            return mAuxPass.ToUpper().Trim();
        }
        set
        {
            mAuxPass = value;
        }
    }

    public String _AuxStringConnectionSql
    {
        get
        {
            return mAuxStringConnectionSql.ToUpper().Trim();
        }
        set
        {
            mAuxStringConnectionSql = value;
        }
    }

    public System.Globalization.CultureInfo _Idioma
    {
        get
        {
            return mIdioma;
        }
        set
        {
            mIdioma = value;
        }
    }

    public String _Theme
    {
        get
        {
            return mTheme;
        }
        set
        {
            mTheme = value;
        }
    }

    #endregion

    #region Métodos

    /// <summary>
    ///     Inicializa o objeto e salva os dados estáticos.
    /// </summary>
    /// <param name="EcServer">Edgecam server + instancew</param>
    /// <param name="EcDb">Nome do banco de dados do Edgecam</param>
    /// <param name="EcUs">Usuário SQL</param>
    /// <param name="EcPss">Senha (caso tenha) do usuário</param>
    /// <param name="StrCnnEc">String de conexão com o banco</param>
    /// <param name="AxServer">Servidor + instância com o banco intermediário</param>
    /// <param name="AxDb">Nome do banco de dados intermediário</param>
    /// <param name="AxUs">Usuário SQL</param>
    /// <param name="AxPss">Senha do usuário SQL</param>
    /// <param name="StrCnnAx">String de conexão com o banco</param>
    /// <param name="Idioma">Cultura que definirá o idioma da interface</param>
    /// <param name="Theme">Thema de uso na interface.</param>
    public Config(String EcServer, String EcDb, String EcUs, String EcPss, String StrCnnEc, String AxServer, String AxDb, String AxUs, String AxPss, String StrCnnAx, System.Globalization.CultureInfo Idioma, String Theme)
    {
        _EcServer = EcServer;
        _EcDataBase = EcDb;
        _EcUser = EcUs;
        _EcPass = EcPss;
        _EcStringConnectionSql = StrCnnEc;

        _AuxServer = AxServer;
        _AuxDataBase = AxDb;
        _AuxUser = AxUs;
        _AuxPass = AxPss;
        _AuxStringConnectionSql = StrCnnAx;
        _Idioma = Idioma;
        _Theme = Theme;
    }

    #endregion
}