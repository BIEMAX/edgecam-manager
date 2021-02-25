/***********************************************************************************************************
 * 
 * 
 *             SkaSql - Classe com métodos conexão com banco de dados, seja ele local ou em rede.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Conectar à um banco de dados SQL específico, executar consultas, etc.
 *      Version:    3.1
 *      Date:       24/04/2017, at 09:41 AM
 *      Note:       <None>
 *      History:    Update      - 23/08/2017 - 03:03 PM - Concluído as atribuições dos valores nas propriedades e comentários nos métodos - V1.1 Lançada.
 *                  Update      - 27/09/2017 - 08:15 AM - Adicionado uma nova instanciaçãoda classe com uma string de conexão pronta - V1.2 Lançada
 *                  Update      - 16/10/2017 - 08:45 AM - Adicionado um novo método estático 'CriaStringConexao' - V1.3 Lançada
 *                  Update      - 09/11/2017 - 08:05 AM - Adicionado limpeza de pools nos métodos que executam consultas no banco de dados - V1.4 Lançada.
 *                  Update      - 09/11/2017 - 11:29 AM - Adicionado novas funcionalidades nas consultas onde, adicionei opções de ROLLBACK nas consultas - V1.5 Lançada
 *                  Update      - 09/11/2017 - 02:08 PM - Adicionado uma nova classe para adicionar parâmetros SQL em uma determinada consulta - V1.6 Lançada
 *                  Update      - 05/12/2017 - 10:06 AM - Adicionado um novo construtor para salvar as exceções em um determinado banco de dados SQL - V1.7 Lançada.
 *                  Update      - 02/02/2018 - 09:50 AM - Adicionado tratativas para as exceções no momento de instânciar o objeto SkaSql- V1.8 Lançada.
 *                  Update      - 04/05/2018 - 03:20 PM - A classe SkaSql agora herda 'IDisposable' para ser utilizado em elementos 'using' - V1.9 Lançada
 *                  Update      - 06/07/2018 - 09:36 AM - Resolvido o problema do método 'ExecutaSql' não retornar exceção - V2.0 Lançada
 *                  Update      - 13/12/2018 - 04:11 PM - Refatorado as variáveis, propriedades e a classe em si - V2.1 Lançada
 *                  Update      - 13/12/2018 - 04:18 PM - Renomeado o método de 'ExecutaQueryTransacao' para 'ExecutaInsertUpdateTransacao' - V2.2 Lançada
 *                  Update      - 14/12/2018 - 03:10 PM - Adicionado o método 'ExecutaTransacaoComRetorno' - V2.3 Lançada.
 *                  Update      - 04/02/2018 - 07:55 PM - Corrigido alguns problemas no método 'TestarConexaoSql' - V2.4 Lançada.
 *                  Update      - 12/02/2019 - 10:49 AM - Corrigido alguns erros de tradução no método 'SkaSql' - V2.5 Lançada.
 * 					Update      - 21/02/2019 - 08:25 AM - Corrigido um problema no método 'ConectaSQL' - V2.6 Lançada.
 * 					Update      - 27/02/2019 - 09:34 AM - Corrigido um problema no método 'DesconectaSQL' - V2.7 Lançada.
 * 					Update      - 27/02/2019 - 09:45 AM - Corrigido um problema no método 'ExecutaTransacaoSemRetorno' - V2.8 Lançada.
 * 					Update      - 27/02/2019 - 09:46 AM - Corrigido um problema no método 'ExecutaTransacaoComRetorno' - V.29 Lançada.
 * 					Update      - 07/06/2019 - 11:13 AM - Adicionado o método 'SalvaDataTable_EmArquivoXml' - V3.0 Lançada.
 * 					Update      - 11/03/2019 - 08:29 AM - Adicionado um novo parâmetro no método 'SalvaDataTable_EmArquivoXml' - V3.1 Lançada.
 * 
 * 
 * Exemplo de uso da parte de Transact SQL.
 * 
 * 
 *          SkaSql sql;
 * 
 * try
 * {
 *          //Instância o objeto
 *          sql = new SkaSql("String conexao", true);
 *          
 *          //Executa o comando
 *          sql.ExecutaQueryTransacao("");
 *
 *          //Comita (finaliza) a transação
 *          sql.ComitaTransacao();
 * }
 * catch
 * {
 *          sql.RoolbackTransacao(); 
 * }
 * 
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///     Classe voltada para consultas em um banco de dados, podendo usufruir
/// de recursos de transct, como begin, commit e rollback (desfazer um insert
/// no banco por exemplo).
/// </summary>
internal class SkaSql : IDisposable
{

    #region Variáveis globais

    protected string mServidorSQL;
    protected string mUsuarioSQL;
    protected string mSenhaSQL;
    protected string mDataBaseSQL;
    protected SqlConnection mCnSQL;
    protected SqlTransaction mTrnsSQL;

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o nome do servidor SQL.
    /// </summary>
    public string _ServidorSql
    {
        get
        {
            return mServidorSQL.ToUpper().Trim();
        }
        set
        {
            mServidorSQL = value;
        }
    }

    /// <summary>
    ///     Contém o usuário SQL.
    /// </summary>
    public string _UsuarioSql
    {
        get
        {
            return mUsuarioSQL;
        }
        set
        {
            mUsuarioSQL = value;
        }
    }

    /// <summary>
    ///     Contém a senha do usuário SQL.
    /// </summary>
    public string _SenhaSql
    {
        get
        {
            return mSenhaSQL;
        }
        set
        {
            mSenhaSQL = value;
        }
    }

    /// <summary>
    ///     Contém o nome do banco de dados SQL.
    /// </summary>
    public string _DataBaseSql
    {
        get
        {
            return mDataBaseSQL;
        }
        set
        {
            mDataBaseSQL = value;
        }
    }

    /// <summary>
    ///     Contém a conexão (aberta) ao banco de dados.
    /// </summary>
    public SqlConnection _CnSql
    {
        get
        {
            return mCnSQL;
        }
        set
        {
            mCnSQL = value;
        }
    }

    /// <summary>
    ///     Contém uma transição SQL, para executar commit's, rollbacks, etc...
    /// </summary>
    public SqlTransaction _TransSQl
    {
        get
        {
            return mTrnsSQL;
        }
        set
        {
            mTrnsSQL = value;
        }
    }


    #endregion

    #region Intância dos objetos da classe

    /// <summary>
    ///     Instância a classe sem fazer nada. Instanciar apenas quando,
    /// quiser utilizar métodos auxiliares.
    /// </summary>
    public SkaSql()
    {

    }

    /// <summary>
    ///     Instância a classe com os parâmetro de conectividade do banco
    /// de dados em questão.
    /// </summary>
    /// <param name="Servidor">Servidor (nome do computador) mais à instância SQL</param>
    /// <param name="DataBase">Banco de dados SQL</param>
    /// <param name="User">Usuário SQL autenticado</param>
    /// <param name="Password">Senha do usuário SQL autenticado (passar vazio se não houver senha configurada).</param>
    /// <param name="ExecutarTransacao">Caso definido como true, poderá ser utilizado as opções commit e rollback na conexão.</param>
    public SkaSql(string Servidor, string DataBase, string User, string Password, Boolean ExecutarTransacao = false)
    {
        try
        {
            _ServidorSql = Servidor.ToUpper();
            _DataBaseSql = DataBase.ToUpper();
            _UsuarioSql = User;
            _SenhaSql = Password;
            _CnSql = new SqlConnection();
            _CnSql = ConectaSQL();

            if (ExecutarTransacao)
            {
                IniciaTransacao();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao instanciar o objeto SkaSql", ex);
        }
    }

    /// <summary>
    ///     Instância a classe passando como parâmetro a string de conexão ao banco.
    /// </summary>
    /// <param name="StrConexao">String de conexão que deverá ser similar à:
    /// 'Data Source=@SERVIDOR;Initial Catalog=@BANCO;User ID=@USER;Password=@PASSWORD;</param>
    /// <param name="ExecutarTransacao">Caso definido como true, poderá ser utilizado as opções commit e rollback na conexão.</param>
    public SkaSql(string StrConexao, Boolean ExecutarTransacao = false)
    {
        try
        {
            string[] strParams = StrConexao.Split(';');

            foreach (string s in strParams)
            {
                switch (s.Split('=')[0].ToUpper())
                {
                    case "DATA SOURCE":
                        _ServidorSql = s.Split('=')[1];
                        break;
                    case "INITIAL CATALOG":
                        _DataBaseSql = s.Split('=')[1];
                        break;
                    case "USER ID":
                        _UsuarioSql = s.Split('=')[1];
                        break;
                    case "PASSWORD":
                        _SenhaSql = s.Split('=')[1];
                        break;
                }
            }

            _CnSql = new SqlConnection();
            _CnSql = ConectaSQL();

            if (ExecutarTransacao)
            {
                IniciaTransacao();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao instanciar o objeto SkaSql", ex);
        }
    }

    /// <summary>
    ///     Método destrutivo da classe.
    /// </summary>
    ~SkaSql() { }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    ///     Método que conecta ao banco de dados e intância o objeto
    /// rebendo/setando a conexão como o próprio objeto.
    /// </summary>
    /// <returns>SqlConnection ao banco de dados atual</returns>
    public SqlConnection ConectaSQL()
    {
        string strConexao = "Data Source=@SERVIDOR;Initial Catalog=@BANCO;User ID=@USER;Password=@PASSWORD;";

        try
        {
            strConexao = strConexao.Replace("@SERVIDOR", _ServidorSql).Replace("@BANCO", _DataBaseSql).Replace("@USER", _UsuarioSql).Replace("@PASSWORD", _SenhaSql);

            if (mCnSQL.State == 0)
            {
                mCnSQL.ConnectionString = strConexao;
                mCnSQL.Open();
            }

        }
        catch (Exception ex)
        {
            //Limpa as pools
            SqlConnection.ClearAllPools();
            SqlConnection.ClearPool(_CnSql);

            if (ex.HResult == -2146232060)
            {
                throw new Exception("Verifique se o serviço SQL Server do servidor está iniciado.");
            }
            else throw new Exception("Não foi possível iniciar uma conexão com o banco de dados.");
        }

        return mCnSQL;

    }

    /// <summary>
    ///     Método que desconecta o SQL.
    /// </summary>
    public void DesconectaSQL()
    {
        try
        {
            if (mCnSQL.State != ConnectionState.Open)
            {
                mCnSQL.Dispose();
                mCnSQL.Close();
            }
        }
        catch { }
    }

    /// <summary>
    ///     Método que executa um comando SQL (Delete, Select, Insert, Update) no SQL,
    /// e retorna um DataTable com dados quando, o comando SQL for um comando de Query.
    /// </summary>
    /// <param name="ComandoSql">String contendo o comando SQL à ser executado</param>
    /// <param name="dicDados">Parâmetros da consulta. Ex.: '@CHAPA, X001.002.003'</param>
    /// <returns>DataTable contendo os dados advindos de uma consulta.</returns>
    public DataTable ExecutaSql(string ComandoSql, Dictionary<string, object> dicDados = null)
    {
        //SqlConnection cn = null;
        DataTable dt = new DataTable();

        try
        {
            if (_CnSql == null) throw new Exception("Dados para conectividade ao banco de dados SQL Server não estão definidas.");
            else if (_CnSql.State == 0) ConectaSQL();

            SqlCommand cmd = new SqlCommand(ComandoSql, _CnSql);

            if (dicDados != null)
            {
                foreach (KeyValuePair<string, object> it in dicDados)
                {
                    cmd.Parameters.AddWithValue(it.Key, it.Value);
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar instrução SQL", ex);
        }
        finally
        {
            if (_CnSql != null && _CnSql.State == ConnectionState.Open)
            {
                _CnSql.Dispose();
                SqlConnection.ClearPool(_CnSql);
            }
        }

        return dt;
    }

    /// <summary>
    ///     Método Booleano que valida se a conexão com o banco de dados
    /// foi estabelecida com êxito.
    /// </summary>
    /// <returns></returns>
    public Boolean TestarConexaoSql()
    {
        bool ret = false;

        try
        {
            if (mCnSQL.State == ConnectionState.Open)
            {
                ret = true;
                DesconectaSQL();
            }

        }
        catch
        {
            ret = false;
        }
        finally
        {
            if (mCnSQL.State != ConnectionState.Open)
            {
                DesconectaSQL();
            }
        }

        return ret;
    }

    /// <summary>
    ///     Inicia a transição SQL, ou seja, recebe a conexão SQL e todas as chamadas
    /// utilizaram o Transact para serem executadas, dessa forma, poderei executar 
    /// comandos de rollback.
    /// </summary>
    public void IniciaTransacao()
    {
        if (_CnSql == null && _CnSql.State == ConnectionState.Closed)
        {
            _CnSql = ConectaSQL();
        }

        _TransSQl = _CnSql.BeginTransaction();
    }

    /// <summary>
    ///     Método desenvolvido para executar Insert/Update no banco de dados, 
    /// sendo controlada por uma 'Transaction SQL', para executar um RollBack caso preciso.
    /// <param name="SqlQuery">Consulta SQL à ser executada (insert for example)</param>
    /// <param name="dic">Dicionário de dados com os parâmetros da consulta.</param>
    /// <remarks>Não deve ser utilizada quando o intuito é executar um insert/update
    /// e já trazer dados no retorno. Para isso, utilizar o método 'ExecutaTransacaoComRetorno'</remarks>
    /// </summary>
    public void ExecutaTransacaoSemRetorno(String SqlQuery, Dictionary<String, Object> dic = null)
    {
        if (_CnSql == null || _TransSQl == null)
        {
            throw new ArgumentNullException("Este método deve ser chamado após a criação de uma conexão e transação por meio do método IniciaTransação.");
        }

        SqlCommand cmd = new SqlCommand(SqlQuery, _CnSql, _TransSQl);

        if (dic != null && dic.Count > 0)
        {
            foreach (var it in dic)
            {
                //cmd.Parameters.Add(it.Key, it.Value);
                cmd.Parameters.AddWithValue(it.Key, it.Value);
            }
        }

        cmd.ExecuteNonQuery();

    }

    /// <summary>
    ///     Executa uma consulta SQL em um banco de dados, sendo controlada por uma 'Transaction SQL',
    /// para executar um RollBack caso preciso, a qual não retorna nenhum dado.
    /// </summary>
    /// <param name="SqlQuery">Consulta SQL à ser executada (insert for example)</param>
    /// <param name="dic">Dicionário de dados com os parâmetros da consulta.</param>
    /// <returns>DataTable contendo os dados advindos de uma consulta</returns>
    public DataTable ExecutaTransacaoComRetorno(String SqlQuery, Dictionary<String, Object> dic = null)
    {
        DataTable dt = new DataTable();

        if (_CnSql == null || _TransSQl == null)
        {
            throw new ArgumentNullException("Este método deve ser chamado após a criação de uma conexão e transação por meio do método IniciaTransação.");
        }

        SqlCommand cmd = new SqlCommand(SqlQuery, _CnSql, _TransSQl);

        if (dic != null && dic.Count > 0)
        {
            foreach (var it in dic)
            {
                //cmd.Parameters.Add(it.Key, it.Value);
                cmd.Parameters.AddWithValue(it.Key, it.Value);
            }
        }

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        return dt;
    }

    /// <summary>
    ///     Comita (finaliza) a transação criada em IniciaTransacao e fecha a conexão.
    /// </summary>
    public void ComitaTransacao()
    {
        if (_TransSQl != null && _CnSql != null)
        {
            _TransSQl.Commit();
            _CnSql.Close();

            _TransSQl = null;
            _CnSql = null;
        }
    }

    /// <summary>
    ///     Cancela a transação criada em IniciaTransacao e fecha a conexão com o banco de dados
    /// limpando as pool's atreladas ao sistema, vulgo cache SQL.
    /// </summary>
    public void RoolbackTransacao()
    {
        if (_TransSQl != null && _CnSql != null)
        {
            _TransSQl.Rollback();
            _CnSql.Close();

            _TransSQl = null;
            _CnSql = null;
        }
    }

    #endregion

    #region Métodos obsoletos/privados

    /// <summary>
    ///     Método que executa um comando SQL e retorna um inteiro contendo
    /// as linhas afetadas no banco de dados.
    /// </summary>
    /// <param name="ComandoSql">String com o comando SQL à ser executado.</param>
    /// <returns>Inteiro contendo o número de linhas afetadas.</returns>
    protected int ExecutaCmdSql(string ComandoSql)
    {
        int intRet = 0;

        try
        {
            if (_CnSql == null) throw new ArgumentNullException("Dados para conectividade ao banco de dados SQL Server não estão definidas.");
            else if (_CnSql.State == 0) ConectaSQL();

            SqlCommand cmd = new SqlCommand(ComandoSql, _CnSql);

            intRet = cmd.ExecuteNonQuery();
        }
        catch { }
        finally
        {
            if (_CnSql.State != 0)
            {
                _CnSql.Dispose();
                _CnSql.Close();

                //Limpa as pools
                SqlConnection.ClearPool(_CnSql);
            }
        }
        return intRet;
    }

    #endregion

    #region Métodos estáticos

    /// <summary>
    ///     Método estático que retorna os servidores que abrigam algum tipo de instalação
    /// SQL, ou seja, retorna uma Lista de String contendo o nome dos computadores da rede.
    /// </summary>
    /// <returns>Retorna uma Lista de String contendo o nome dos computadores da rede</returns>
    public static List<string> LstServidoresSqlNetwork()
    {
        List<string> lstRet = new List<string>();

        try
        {
            SqlDataSourceEnumerator servers = SqlDataSourceEnumerator.Instance;
            DataTable dt = servers.GetDataSources();

            foreach (DataRow r in dt.Rows)
            {
                if (lstRet.Where(l => l.ToUpper() == r[0].ToString().ToUpper()).Count() > 0)
                    continue;
                else lstRet.Add(r[0].ToString());
            }

        }
        catch { }

        return lstRet.OrderBy(l => l).ToList();
    }

    /// <summary>
    ///     Método estático que retorar instâncias SQL's de um determinado host (server).
    /// </summary>
    /// <param name="ServerName">Nome do servidor à ser validado as instâncias</param>
    /// <returns>Retorna uma List de String contendo o nome das instâncias SQL do host.</returns>
    public static List<string> LstInstanciasServerSql(string ServerName)
    {
        List<string> lstRet = new List<string>();

        try
        {
            SqlDataSourceEnumerator servers = SqlDataSourceEnumerator.Instance;
            DataTable dt = servers.GetDataSources();

            foreach (DataRow r in dt.Rows)
            {
                if (ServerName.ToUpper() == r[0].ToString().ToUpper())
                    lstRet.Add(r[1].ToString().ToUpper());
                else continue;
            }
        }
        catch { }
        return lstRet.OrderBy(l => l).ToList();
    }

    /// <summary>
    ///     Método estático que retorna uma lista de String, todos os bancos de dados
    /// da computador host.
    /// </summary>
    /// <param name="ServerName">Nome do servidor</param>
    /// <param name="InstanceName">Nome da instância</param>
    /// <param name="User">Usuário SQL</param>
    /// <param name="Password">Senha do usuário SQL</param>
    /// <returns>Retorna uma lista de string cotendo o nome dos bancos de dados
    /// abrigados no servidor host.</returns>
    public static List<string> LstBancosInstanciaSql(string ServerName, string InstanceName, string User, string Password = null)
    {
        List<string> lstRet = new List<string>();
        DataTable dt = new DataTable();
        SqlConnection cn;
        string sqlCn = string.Format("Data Source={0}\\{1};User ID={2};Password={3};", ServerName, InstanceName, User, Password);
        string sqlCmd = "select db.name from sys.databases as db where db.name <> 'master'" +
                        "and db.name <> 'tempdb' and db.name <> 'model' and db.name <> 'msdb'";

        try
        {
            cn = new SqlConnection(sqlCn);

            SqlCommand cmd = new SqlCommand(sqlCmd, cn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                lstRet.Add(r[0].ToString().ToUpper());
            }

        }
        catch { }

        return lstRet.OrderBy(q => q).ToList();
    }

    /// <summary>
    ///     Recebe parâmetros de um servidor e cria uma string de conexão SQL. Esse método
    /// não executa uma verificação para averiguar se os dados inseridos estão corretos.
    /// </summary>
    /// <param name="Server">Servidor SQL</param>
    /// <param name="Db">Nome do banco de dados</param>
    /// <param name="User">Usuário autenticado SQL</param>
    /// <param name="Pass">Senha do usuário Autenticado (opcional)</param>
    /// <returns>String contendo a String de conexão SQL.</returns>
    public static String CriaStringConexao(String Server, String Db, String User, String Pass = "")
    {
        String ret = "Data Source=@SERVIDOR;Initial Catalog=@BANCO;User ID=@USER;Password=@PASSWORD;";

        return ret.Replace("@SERVIDOR", Server).Replace("@BANCO", Db).Replace("@USER", User).Replace("@PASSWORD", Pass);
    }

    /// <summary>
    ///     Método que pega um DataTable populado (com dados) e salva em um arquivo de texto
    /// no formato XML (como um banco de dados offline).
    /// </summary>
    /// <param name="Dt">DataTable contendo os dados</param>
    /// <param name="Caminho">Caminho para salvar o arquivo XML (Caso não ser decidido, o sistema
    /// <param name="LstNomesTabelas">Lista contendo o nome das tabelas. Estas devem estar em ordem.</param>
    /// irá salvar automaticamente o XML na área de trabalho do usuário).</param>
    /// <returns>String contendo o caminho do arquivo XML (em caso de sucesso) ou String
    /// vazia (em caso de erro/exceção).</returns>
    public static String SalvaDataTable_EmArquivoXml(DataTable Dt, String Caminho = "", List<String> LstNomesTabelas = null)
    {
        try
        {
            if (Dt != null && Dt.Rows.Count > 0)
            {
                DataSet ds = new DataSet();
                ds.Merge(Dt);

                //Adiciona o nome das tabelas no dataset
                if (LstNomesTabelas != null && LstNomesTabelas.Count > 0)
                {
                    for (int x = 0; x < LstNomesTabelas.Count; x++)
                    {
                        ds.Tables[x].TableName = LstNomesTabelas[x];
                    }
                }

                if (!String.IsNullOrEmpty(Caminho) && Caminho.ToUpper().Trim().EndsWith(".XML"))
                {
                    if (System.IO.File.Exists(Caminho))
                        System.IO.File.Delete(Caminho);
                    
                    ds.WriteXml(Caminho);

                    return Caminho;
                }
                else
                {
                    //Caminho que é criado automaticamente pelo sistema.
                    String caminhoAuto = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}.xml", DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss")));

                    ds.WriteXml(caminhoAuto);

                    return caminhoAuto;
                }
            }
            else return "";
        }
        catch { return ""; }
    }

    #endregion

}