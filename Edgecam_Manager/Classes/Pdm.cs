/***********************************************************************************************************
 * 
 * 
 *                  PDM - Classe com métodos específicos para atuar em conjunto com o SolidWorks PDM PRO
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Funções específicas relacionadas ao software SolidWorks PDM PRO
 *      Version:    1.0
 *      Date:       24/09/2018, at 12:37 AM
 *      Note:       <None>
 *      Updates:    Update - 09/02/2017 - 4:51 PM - Criado a primeira versão - V1.0 Lançada
 * 
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using EPDM.Interop.epdm;

public class PDM
{
    #region Variáveis globais

    /// <summary>
    ///     Contém o objeto que dá acesso ao PDM.
    /// </summary>
    //private EdmVault5 mPdmVault;

    /// <summary>
    ///     Contém o nome do cofre para se conectar.
    /// </summary>
    private String mCofre;

    /// <summary>
    ///     Contém o login do PDM.
    /// </summary>
    private String mLogin;

    /// <summary>
    ///     Contém a senha para login no PDM.
    /// </summary>
    private String mSenha;

    /// <summary>
    ///     Contém a informação para verificar se 
    /// </summary>
    //private Boolean mIsLoggedIn;

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o nome do cofre para se conectar.
    /// </summary>
    public String _Cofre
    {
        get
        {
            return mCofre;
        }
        set
        {
            mCofre = value;
        }
    }

    /// <summary>
    ///     Contém o login do PDM (propriedade somente leitura).
    /// </summary>
    public String _Login
    {
        get
        {
            return mLogin;
        }
    }

    /// <summary>
    ///     Contém a senha para login no PDM (propriedade somente leitura).
    /// </summary>
    public String _Senha
    {
        get
        {
            return mSenha;
        }
    }

    #endregion

    #region Instância dos objetos da classe

    public PDM()
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar se conectar com o PDM Professional", ex);
        }
    }

    public PDM(String Cofre, String Login, String Senha)
    {
        try
        {
            mCofre = Cofre;
            mLogin = Login;
            mSenha = Senha;

            InicializaConexao();

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao instanciar a classe 'PDM'.", ex);
        }
    }

    ~PDM() { }

    #endregion

    #region Métodos

    /// <summary>
    ///     Método responsável por estabelecer conexão com o PDM (instanciar o objeto).
    /// </summary>
    /// <remarks>Lembre-se de informar o login, senha e o nome do cofre para conexão.</remarks>
    public void InicializaConexao()
    {
        if (String.IsNullOrEmpty(mLogin))
            throw new ArgumentNullException("A propriedade 'Login' não foi definida.");

        if (String.IsNullOrEmpty(mCofre))
            throw new ArgumentNullException("A propriedade 'Cofre' não foi definida.");


        //TODO: PRECISA DESCOMENTAR ESSA PARTE E ARRUMAR A PARTE DO PDM
        //mPdmVault = new EdmVault5();
        //mPdmVault.Login(mLogin, mSenha, mCofre);

        //mPdmVault.IsLoggedIn
    }

    #endregion

}