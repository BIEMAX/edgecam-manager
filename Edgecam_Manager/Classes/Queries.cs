using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Edgecam_Manager;


/// <summary>
///     Classe que executa consultas no banco de dados (EC e MGR) e devolve data tables já formatados.
/// </summary>
internal class SQLQueries
{
    internal enum e_SkaTipoConsultaDados
    {
        ConsultarPorId,
        ConsultarParaSelecionar
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando as tarefas.
    /// </summary>
    /// <param name="Assunto">Título do assunto</param>
    /// <param name="Prioridade">Nível de prioridade</param>
    /// <param name="Data">Data para consultar</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Tarefas(String Assunto = "", int Prioridade = 0, String Data = "")
    {
        try
        {
            // Aplica os filtros
            String sqlQuery = Consultas_EcMgr.CONSULTA_TAREFAS_USUARIO_LOGADO;

            sqlQuery += String.Format(" where UsuarioDesignado = '{0}'", Objects.UsuarioAtual.Login);
            sqlQuery += " and Completo = 0";

            if (Assunto.ToUpper() != "(TODOS)" && !String.IsNullOrEmpty(Assunto))
                sqlQuery += String.Format(" and Assunto like '%{0}%'", Assunto);

            if (Prioridade != 0)
                sqlQuery += String.Format(" and PrioridadeTarefa = '{0}'", Prioridade);

            if (Data != "")
                sqlQuery += String.Format(" and DtInicio = '{0}'", Data);

            // Armazena temporariamente a tabela
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clPrioridade = new DataColumn("Prioridade", typeof(System.Drawing.Image));
            dt.Columns.Add(clPrioridade);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 1 = Baixa
                if (dt.Rows[i]["PrioridadeTarefa"].ToString().ToUpper() == "1")
                    dt.Rows[i]["Prioridade"] = Edgecam_Manager.Properties.Resources.p_baixa;

                // 2 = Média
                if (dt.Rows[i]["PrioridadeTarefa"].ToString().ToUpper() == "2")
                    dt.Rows[i]["Prioridade"] = Edgecam_Manager.Properties.Resources.p_normal;

                // 3 = Alta
                if (dt.Rows[i]["PrioridadeTarefa"].ToString().ToUpper() == "3")
                    dt.Rows[i]["Prioridade"] = Edgecam_Manager.Properties.Resources.p_alta;
            }

            return dt;
        }
        catch (Exception ex)
        {
            // Em caso de alguma exceção, retorna vazio.
            throw new Exception("Erro ao consultar as tarefas do usuário corrente", ex);
        }
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando as ordens de produção.
    /// </summary>
    /// <param name="Ordem">Ordem de produção</param>
    /// <param name="Trabalho">Nome do trabalho do Edgecam</param>
    /// <param name="Cliente">Nome do cliente</param>
    /// <param name="DataEntrega">Data de entrega</param>
    /// <param name="Estado">Estado da ordem (ns) de produção</param>
    /// <param name="CentroTrabalho">Nome da máquina</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_OrdensProducao(String Ordem = "", String Trabalho = "", String Cliente = "", String DataEntrega = "", Int32 Estado = 0, String CentroTrabalho = "", String Material = "")
    {
        try
        {
            //String de consulta das ordens de produção.
            String sqlQuery = Consultas_EcMgr.CONSULTA_ORDENS;

            //Adiciono um cláusula somente para adicionar os filtros e não me preocupar com os demais.
            sqlQuery += " where id != 0";

            if (Ordem.ToUpper() != "(TODOS)" && !String.IsNullOrEmpty(Ordem))
                sqlQuery += String.Format(" and OrdemProducao like '%{0}%'", Ordem);

            if (!String.IsNullOrEmpty(Trabalho))
                sqlQuery += String.Format(" and Trabalho like '%{0}%'", Trabalho);

            if (Cliente.ToUpper() != "(TODOS)" && !String.IsNullOrEmpty(Cliente))
                sqlQuery += String.Format(" and Cliente like '%{0}%'", Cliente);

            if (!String.IsNullOrEmpty(DataEntrega))
                sqlQuery += String.Format(" and DataEntrega like '%{0}%'", DataEntrega);

            if (Estado != 0 && Estado > 0)
                sqlQuery += String.Format(" and Estado = {0}", Estado);

            if (CentroTrabalho.ToUpper() != "(TODOS)" && !String.IsNullOrEmpty(CentroTrabalho))
                sqlQuery += String.Format(" and CentroTrabalho like '%{0}%'", CentroTrabalho);

            if (!String.IsNullOrEmpty(Material))
                sqlQuery += String.Format(" and Material like '%{0}%'", Material);

            //  Armazena temporariamente a tabela
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Estado", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            DataColumn clAmbiente = new DataColumn("Ambiente", typeof(String));
            dt.Columns.Add(clAmbiente);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["Estado_Db"].ToString().ToUpper())
                {
                    case "1": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.White;             break;
                    case "2": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Green;             break;
                    case "3": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.PartiallyReleased; break;
                    case "4": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Orange;            break;
                    case "5": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Global;            break;
                    case "6": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Cancel;            break;
                    case "7": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Red;               break;
                    default: break;
                }

                //Define o tipo de ambiente (texto na interface do usuário).
                if (dt.Rows[i]["Ambiente_Db"].ToString().ToUpper() == "0")
                    dt.Rows[i]["Ambiente"] = "Fresamento";
                else dt.Rows[i]["Ambiente"] = "Torneamento";
            }

            return dt;
        }
        catch (Exception ex)
        {
            // Em caso de alguma exceção, retorna vazio.
            throw new Exception("Erro ao consultar as ordens de produção", ex);
        }
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os dados auxiliares
    /// das ordens de produção.
    /// </summary>
    /// <param name="IdDadoAuxiliar">Id do dado auxiliar (precisa ser maior que 0)</param>
    /// <param name="Descricao">Descrição do dado auxiliar</param>
    /// <param name="TipoConsulta">Tipo de consulta, se é por buscar todos ou um item específico</param>
    /// <param name="AdicionarColunaImagem">True para adicionar uma coluna do tipo imagem que demonstra se
    /// o dado auxiliar está ou não ativo.</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable Consulta_DadosAuxiliares(String IdDadoAuxiliar = "", String Descricao = "", e_SkaTipoConsultaDados TipoConsulta = e_SkaTipoConsultaDados.ConsultarPorId, Boolean AdicionarColunaImagem = false)
    {
        try
        {
            String sqlQuery = "";

            if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarPorId)
                sqlQuery = Consultas_EcMgr.CONSULTA_DADOS_AUXILIARES_POR_ID;
            else if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarParaSelecionar)
                sqlQuery = Consultas_EcMgr.CONSULTA_TODOS_DADOS_AUXILIARES;

            if (!String.IsNullOrEmpty(IdDadoAuxiliar))
                sqlQuery += String.Format(" AND id in ({0})", IdDadoAuxiliar);

            if (!String.IsNullOrEmpty(Descricao))
                sqlQuery += String.Format(" AND DadoAuxiliar like '%{0}%'", Descricao);

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

            if (AdicionarColunaImagem)
            {
                // Cria as colunas de armazenamento dos ícones da interface.
                DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
                dt.Columns.Add(clEstado);

                // Adiciona os ícones na tuplas da table (ícones da prioridade)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Define o tipo de ambiente (texto na interface do usuário).
                    if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                        dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                    else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
                }
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar os dados auxiliares das ordens de produção", ex);
        }
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os arquivos
    /// das ordens de produção.
    /// </summary>
    /// <param name="IdArquivo">Id do dado auxiliar (precisa ser maior que 0)</param>
    /// <param name="Descricao">Descrição do arquivo (nome ou nome da pasta em que o mesmo se encontra).</param>
    /// <param name="TipoConsulta">Tipo de consulta, se é por buscar todos ou um item específico</param>
    /// <param name="AdicionarColunaImagem">True para adicionar uma coluna do tipo imagem que demonstra se
    /// o dado auxiliar está ou não ativo.</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable Consulta_ArquivosAuxiliares(String IdArquivo = "", String Descricao = "", e_SkaTipoConsultaDados TipoConsulta = e_SkaTipoConsultaDados.ConsultarPorId, Boolean AdicionarColunaImagem = false)
    {
        try
        {
            String sqlQuery = "";

            if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarPorId)
                sqlQuery = Consultas_EcMgr.CONSULTA_ARQUIVOS_POR_ID;
            else if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarParaSelecionar)
                sqlQuery = Consultas_EcMgr.CONSULTA_ARQUIVOS_FOR_SELECAO;

            if (!String.IsNullOrEmpty(IdArquivo))
            {
                //Se o arquivo conter apóstrofo, significa que tem mais de um ID na String, caso contrário, é apenas um.
                if (IdArquivo.Contains("'")) sqlQuery += String.Format(" AND id in ({0})", IdArquivo);
                else sqlQuery += String.Format(" AND id in ('{0}')", IdArquivo);
            }

            if (!String.IsNullOrEmpty(Descricao))
                sqlQuery += String.Format(" AND CaminhoArquivo like '%{0}%'", Descricao);

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

            if (AdicionarColunaImagem)
            {
                // Cria as colunas de armazenamento dos ícones da interface.
                DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
                dt.Columns.Add(clEstado);

                // Adiciona os ícones na tuplas da table (ícones da prioridade)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Define o tipo de ambiente (texto na interface do usuário).
                    if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                        dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                    else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
                }
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar os dados auxiliares das ordens de produção", ex);
        }
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os orçamentos.
    /// </summary>
    /// <param name="Orcamento">Orçamento</param>
    /// <param name="Cliente">Nome do cliente</param>
    /// <param name="DataEmissao">Data de emissão (envio para o cliente) do orçamento</param>
    /// <param name="Estado">Estado d o orçamento (s)</param>
    /// <param name="TipoOrcamento">Tipo de orçamento</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Orcamentos(String Orcamento = "", String Cliente = "", String DataEmissao = "", Int32 Estado = 0, String TipoOrcamento = "")
    {
        try
        {
            //String de consulta das ordens de produção.
            String sqlQuery = Consultas_EcMgr.CONSULTA_ORCAMENTOS;

            if (Orcamento.ToUpper() != "(TODOS)" && !String.IsNullOrEmpty(Orcamento))
                sqlQuery += $" and Orcamento like '%{Orcamento}%'";

            if (Cliente.ToUpper() != "(TODOS)" && !String.IsNullOrEmpty(Cliente))
                sqlQuery += $" and Cliente like '%{Cliente}%'";

            if (!String.IsNullOrEmpty(DataEmissao))
                sqlQuery += $" and DtEmissao like '%{DataEmissao}%'";

            if (Estado != 0 && Estado > 0)
                sqlQuery += $" and EstadoOrcamento = {Estado}";

            if (!String.IsNullOrEmpty(TipoOrcamento) && TipoOrcamento.ToUpper().Trim() != "(TODOS)")
                sqlQuery += $" and TipoOrcamento = {Objects.LstTiposOrcamentos.Where(x => x.Nome.ToUpper().Trim() == TipoOrcamento.ToUpper().Trim()).Select(y => y.Id).FirstOrDefault()}";

            //Consulta os tipos de orçamentos cadastrados pelo usuário previamente
            //DataTable dtTipos = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TIPOS_ORCAMENTOS);

            //  Armazena temporariamente a tabela
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Estado", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            DataColumn clAtivo = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clAtivo);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["EstadoOrcamento_db"].ToString().ToUpper())
                {
                    case "1": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.White;  break;
                    case "2": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Green;  break;
                    case "3": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Orange; break;
                    case "4": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Global; break;
                    case "5": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Cancel; break;
                    case "6": dt.Rows[i]["Estado"] = Edgecam_Manager.Properties.Resources.Red;    break;
                    default: break;
                }

                switch (dt.Rows[i]["OrcOrigem_db"].ToString().ToUpper())
                {
                    case "1": dt.Rows[i]["Origem do orçamento"] = "Orçamento simples"; break;
                    case "2": dt.Rows[i]["Origem do orçamento"] = "Orçamento detalhado"; break;
                    case "3": dt.Rows[i]["Origem do orçamento"] = "Orçamento técnico"; break;
                    default: break;
                }

                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString()) == false)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
            }

            return dt;
        }
        catch (Exception ex)
        {
            // Em caso de alguma exceção, retorna vazio.
            throw new Exception("Erro ao consultar as ordens de produção", ex);
        }
    }

    /// <summary>
    ///     Consulta os protudos cadastrados no banco de dados.
    /// </summary>
    /// <param name="Nome">Nome do produto.</param>
    /// <param name="Obs">Observação/Nota do produto.</param>
    /// <param name="Tipo">Tipo do produto.</param>
    /// <returns></returns>
    internal static DataTable Consulta_ProdutosOrcamentos(String Nome, String Obs, String Tipo)
    {
        try
        {
            DataTable dt = new DataTable();
            String q = Consultas_EcMgr.CONSULTA_PRODUTOS;

            if (!String.IsNullOrEmpty(Nome)) q += $" and P.Nome like '%{Nome}%'";
            if (!String.IsNullOrEmpty(Obs)) q += $" and P.Descricao like '%{Obs}%'";
            if (!String.IsNullOrEmpty(Tipo) && Tipo.ToUpper().Trim() != "<SELECIONE>") q += $" and T.Nome like '%{Tipo}%'";

            dt = Objects.CnnBancoEcMgr.ExecutaSql(q);

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clAtivo = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clAtivo);

            //DataColumn clTipo = new DataColumn("Tipo", typeof(System.Drawing.Image));
            //dt.Columns.Add(clTipo);

            for(int i =0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["Ativo_Db"].ToString().ToUpper() == "0")
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;

                //dt.Rows[i]["Custo unitário"] = $"R$ {dt.Rows[i]["Custo unitário"].ToString()}";
                
                //switch (dt.Rows[i]["Tipo_Db"].ToString().ToUpper())
                //{
                //    case "1": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.produto_inventario; break;
                //    case "2": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.ferramentas; break;
                //    case "3": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.servico_manutencao_16; break;
                //    case "4": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.servico_suporte; break;
                //    case "5": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.producao_fabrica; break;
                //    case "6": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.cambio_moeda_cotacao; break;
                //    case "7": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.instalacao_software; break;
                //    case "8": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.consultoria_perguntar; break;
                //    case "9": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.development_prototype_16; break;
                //    case "10": dt.Rows[i]["Tipo"] = Edgecam_Manager.Imagens_NewLookInterface.compras_carrinho_16; break;
                //    case "11": dt.Rows[i]["Tipo"] = Edgecam_Manager.Properties.Resources.help; break;
                //    default: break;
                //}
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar os itens/produtos de orçamentos anteriores ou previamente cadastrados", ex);
        }
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os clientes apenas.
    /// </summary>
    /// <param name="IdCliente">Id do cliente</param>
    /// <param name="NomeCliente">Nome do cliente</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Clientes(String IdCliente = "", String NomeCliente = "")
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_CLIENTES_FOR_SELECAO;

        if (!String.IsNullOrEmpty(IdCliente)) sqlQuery += String.Format(" and id = {0}", IdCliente);
        if (!String.IsNullOrEmpty(NomeCliente)) sqlQuery += String.Format(" and NomeEmpresa like '%{0}%'", NomeCliente);

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os contatos apenas.
    /// </summary>
    /// <param name="Nome">Nome ou sobrenome do contato</param>
    /// <param name="Cliente">Nome do cliente</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Contatos(String Nome = "", String Cliente = "")
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_CONTATOS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(Nome)) sqlQuery += String.Format(" and Nome like '%{0}%' or SobreNome like '%{0}%'", Nome);
        if (!String.IsNullOrEmpty(Cliente)) sqlQuery += String.Format(" and Cliente like '%{0}%'", Cliente);

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os clientes apenas.
    /// </summary>
    /// <param name="NomeCidade">Nome da cidade</param>
    /// <param name="Estado">Estado</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Cidades(String NomeCidade = "", String Estado = "")
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_CIDADES_FOR_SELECAO;

        if (!String.IsNullOrEmpty(NomeCidade)) sqlQuery += String.Format(" and Cidade like '%{0}%'", NomeCidade);
        if (!String.IsNullOrEmpty(Estado)) sqlQuery += String.Format(" and Estado like '%{0}%'", Estado);

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os estados apenas.
    /// </summary>
    /// <param name="NomeEstado">Nome do estado</param>
    /// <param name="NomePais">Nome do pais</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Estados(String NomeEstado = "", String NomePais = "")
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_ESTADOS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(NomeEstado)) sqlQuery += String.Format(" and Estado like '%{0}%'", NomeEstado);
        if (!String.IsNullOrEmpty(NomePais)) sqlQuery += String.Format(" and Pais like '%{0}%'", NomePais);

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os países apenas.
    /// </summary>
    /// <param name="NomePais">Nome do país</param>
    /// <param name="Continente">Continente</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Paises(String NomePais = "", String Continente = "")
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_PAISES_FOR_SELECAO;

        if (!String.IsNullOrEmpty(NomePais)) sqlQuery += String.Format(" and Pais like '%{0}%'", NomePais);
        if (!String.IsNullOrEmpty(Continente) && Continente.ToUpper().Trim() != "<SELECIONE>") sqlQuery += String.Format(" and Continente = '{0}'", Continente);

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os materiais apenas.
    /// </summary>
    /// <param name="Material">Nome do material</param>
    /// <param name="AdicionarColunaImagem">True para definir a coluna das imagens</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Materiais(String Material = "", Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_MATERIAIS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(Material)) sqlQuery += String.Format(" and NomeMaterial like '%{0}%'", Material);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os materiais apenas. 
    /// </summary>
    /// <param name="Material">Nome do material</param>
    /// <param name="Fornecedor">Fornecedor do preço</param>
    /// <param name="DataValidade">Data de validade</param>
    /// <param name="AdicionarColunaImagem">True para definir a coluna das imagens</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_PrecosMateriais(String Material = "", String Fornecedor = "", String DataValidade = "", Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_CUSTOS_MATERIAIS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(Material)) sqlQuery += String.Format(" and Material like '%{0}%'", Material);
        if (!String.IsNullOrEmpty(Fornecedor)) sqlQuery += String.Format(" and Fornecedor like '%{0}%'", Fornecedor);
        if (!String.IsNullOrEmpty(Material)) sqlQuery += String.Format(" and cast ([OfertaValidaAte] as date) = '{0}'", DataValidade);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            DataColumn clPreco = new DataColumn("Preço por quilo", typeof(String));
            dt.Columns.Add(clPreco);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Preço por quilo"] = String.Format("{0:C} p/Kg", Convert.ToDouble(dt.Rows[i]["ValorPrecoQuilo"].ToString()));

                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os custos
    /// adicionais para os orçamentos.
    /// </summary>
    /// <param name="IdCusto">Id do custo (precisa ser maior que zero)</param>
    /// <param name="DescricaoCusto">Descrição do custo</param>
    /// <param name="TipoConsulta">Tipo de consulta, se é por buscar todos ou um item específico</param>
    /// <param name="AdicionarColunaImagem">True para adicionar uma coluna do tipo imagem que demonstra se
    /// o dado auxiliar está ou não ativo.</param>
    /// <returns>DataTable contendo os dados caso existam.</returns>
    internal static DataTable Consulta_CustosAdicionais(String IdCusto = "", String DescricaoCusto = "", e_SkaTipoConsultaDados TipoConsulta = e_SkaTipoConsultaDados.ConsultarPorId, Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = "";

        if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarPorId)
            sqlQuery = Consultas_EcMgr.CONSULTA_CUSTOS_ADICIONAIS_POR_ID;
        else if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarParaSelecionar)
            sqlQuery = Consultas_EcMgr.CONSULTA_CUSTOS_ADICIONAIS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(IdCusto))
        {
            //Se o arquivo conter apóstrofo, significa que tem mais de um ID na String, caso contrário, é apenas um.
            if (IdCusto.Contains("'")) sqlQuery += String.Format(" AND id in ({0})", IdCusto);
            else sqlQuery += String.Format(" AND id in ('{0}')", IdCusto);
        }

        if (!String.IsNullOrEmpty(DescricaoCusto))
            sqlQuery += String.Format(" AND CustoAdicional like '%{0}%'", DescricaoCusto);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os markups para orçamentos.
    /// </summary>
    /// <param name="Nome">Descrição do custo</param>
    /// <param name="AdicionarColunaImagem">True para adicionar uma coluna do tipo imagem que demonstra se
    /// o dado auxiliar está ou não ativo.</param>
    /// <returns>DataTable contendo os dados caso existam.</returns>
    internal static DataTable Consulta_Markup(String Nome = "", Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_MARKUPS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(Nome)) sqlQuery += $" AND Nome like '%{Nome}%'";

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando as tarifas
    /// para orçamentos.
    /// </summary>
    /// <param name="IdTarifa">Id da tarifa</param>
    /// <param name="NomeTarifa">Nome da tarifa</param>
    /// <param name="TarifasVencidas">True or false para buscar tarifas vencidas ou não, null para não buscar nada.</param>
    /// <param name="TipoConsulta">Consulta por ID ou para seleção.</param>
    /// <param name="AdicionarColunaImagem">True para adicionar a coluna de imagem para validade</param>
    /// <returns>DataTable contendo os dados caso existam.</returns>
    internal static DataTable Consulta_Tarifas(String IdTarifa = "", String NomeTarifa = "", Nullable<Boolean> TarifasVencidas = null, e_SkaTipoConsultaDados TipoConsulta = e_SkaTipoConsultaDados.ConsultarPorId, Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = "";

        if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarPorId)
            sqlQuery = Consultas_EcMgr.CONSULTA_TARIFAS_POR_ID;
        else if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarParaSelecionar)
            sqlQuery = Consultas_EcMgr.CONSULTA_TARIFAS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(IdTarifa))
        {
            //Se o arquivo conter apóstrofo, significa que tem mais de um ID na String, caso contrário, é apenas um.
            if (IdTarifa.Contains("'")) sqlQuery += String.Format(" AND id in ({0})", IdTarifa);
            else sqlQuery += String.Format(" AND id in ('{0}')", IdTarifa);
        }

        if (!String.IsNullOrEmpty(NomeTarifa)) sqlQuery += String.Format(" AND Tarifa like '%{0}%'", NomeTarifa);
        if (TarifasVencidas != null) sqlQuery += String.Format(" and TemValidade = {0}", (bool)TarifasVencidas ? "1" : "0");

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Vencido", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["TemValidade"].ToString().ToUpper()) == true)
                {
                    if (DateTime.Today > Convert.ToDateTime(dt.Rows[i]["Válido até"].ToString()))
                    {
                        dt.Rows[i]["Vencido"] = Edgecam_Manager.Properties.Resources.Red;
                    }
                    else dt.Rows[i]["Vencido"] = Edgecam_Manager.Properties.Resources.Green;
                }
                else dt.Rows[i]["Vencido"] = Edgecam_Manager.Properties.Resources.Green;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os impostos
    /// para orçamentos.
    /// </summary>
    /// <param name="IdImposto">Id do imposto</param>
    /// <param name="NomeImposto">Nome do imposto</param>
    /// <param name="ImpostosVencidos">True or false para buscar tarifas vencidas ou não, null para não buscar nada.</param>
    /// <param name="TipoConsulta">Consulta por ID ou para seleção.</param>
    /// <param name="AdicionarColunaImagem">True para adicionar a coluna de imagem para validade</param>
    /// <returns>DataTable contendo os dados caso existam.</returns>
    internal static DataTable Consulta_Impostos(String IdImposto = "", String NomeImposto = "", Nullable<Boolean> ImpostosVencidos = null, e_SkaTipoConsultaDados TipoConsulta = e_SkaTipoConsultaDados.ConsultarPorId, Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = "";

        if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarPorId)
            sqlQuery = Consultas_EcMgr.CONSULTA_IMPOSTOS_POR_ID;
        else if (TipoConsulta == e_SkaTipoConsultaDados.ConsultarParaSelecionar)
            sqlQuery = Consultas_EcMgr.CONSULTA_IMPOSTOS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(IdImposto))
        {
            //Se o arquivo conter apóstrofo, significa que tem mais de um ID na String, caso contrário, é apenas um.
            if (IdImposto.Contains("'")) sqlQuery += String.Format(" AND id in ({0})", IdImposto);
            else sqlQuery += String.Format(" AND id in ('{0}')", IdImposto);
        }

        if (!String.IsNullOrEmpty(NomeImposto)) sqlQuery += String.Format(" AND Imposto like '%{0}%'", NomeImposto);
        if (ImpostosVencidos != null) sqlQuery += String.Format(" and TemValidade = {0}", (bool)ImpostosVencidos ? "1" : "0");

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Vencido", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["TemValidade"].ToString().ToUpper()) == true)
                {
                    if (DateTime.Today > Convert.ToDateTime(dt.Rows[i]["Válido até"].ToString()))
                    {
                        dt.Rows[i]["Vencido"] = Edgecam_Manager.Properties.Resources.Red;
                    }
                    else dt.Rows[i]["Vencido"] = Edgecam_Manager.Properties.Resources.Green;
                }
                else dt.Rows[i]["Vencido"] = Edgecam_Manager.Properties.Resources.Green;

                //Define em forma de texto o valor.
                if (dt.Rows[i]["TipoImposto"].ToString() == "1")
                {
                    dt.Rows[i]["Tipo de imposto"] = "Percentual";
                    dt.Rows[i]["Valor do imposto"] = $"{dt.Rows[i]["ValorImposto"].ToString()} (%)";
                }
                else if (dt.Rows[i]["TipoImposto"].ToString() == "2")
                {
                    dt.Rows[i]["Tipo de imposto"] = "Valor fixo";
                    dt.Rows[i]["Valor do imposto"] = $"{dt.Rows[i]["ValorImposto"].ToString()} (R$)";
                }
                else
                {
                    dt.Rows[i]["Tipo de imposto"] = "Utilizado em markup";
                    dt.Rows[i]["Valor do imposto"] = $"{dt.Rows[i]["ValorImposto"].ToString()} (%)";
                }
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando as moedas.
    /// </summary>
    /// <param name="Moeda">Moeda a ser pesquisada</param>
    /// <param name="AdicionarColunaImagem">True para adicionar uma coluna do tipo imagem que demonstra se
    /// o dado auxiliar está ou não ativo.</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable Consulta_Moedas(String Moeda = "", Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_MOEDAS_COTACOES_FOR_SELECAO;

        if (!String.IsNullOrEmpty(Moeda)) sqlQuery += String.Format(" AND MoedaPesquisa like '%{0}%'", Moeda);

        //Sempre ordena a consulta
        sqlQuery += " order by MoedaPesquisa";

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clEstado = new DataColumn("Ativo", typeof(System.Drawing.Image));
            dt.Columns.Add(clEstado);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Define o tipo de ambiente (texto na interface do usuário).
                if (Convert.ToBoolean(dt.Rows[i]["Ativo_Db"].ToString().ToUpper()) == true)
                    dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando as cotações diárias do sistema.
    /// </summary>
    /// <param name="Moeda">Moeda a ser pesquisada</param>
    /// <param name="Data">Data de cotação</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable Consulta_CotacoesDiarias(String Moeda = "", String Data = "")
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_COTACOES_DIARIAS_FOR_SELECAO;

        if (!String.IsNullOrEmpty(Moeda)) sqlQuery += String.Format(" AND MoedaPesquisada like '%{0}%'", Moeda);
        if (!String.IsNullOrEmpty(Data)) sqlQuery += String.Format(" and cast ([DtConsulta] as date) = '{0}'", Data);

        //Sempre ordena a consulta
        sqlQuery += " order by MoedaPesquisada";

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    /// <summary>
    ///     Executa uma consulta no banco de dados do Edgecam e busca
    /// as ferramentas de fresamento.
    /// </summary>
    /// <param name="Descricao">Descrição da ferramenta</param>
    /// <param name="Tipo">Tipo da ferramenta</param>
    /// <param name="Diametro">Diâmetro da ferramenta</param>
    /// <param name="Unidade">Unidade de medida</param>
    /// <param name="Tecnologia">Tecnologia (para desbaste ou acabamento)</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable ConsultaFerramentas_Mill(String Descricao = "", int Tipo = 0, String Diametro = "", int Unidade = 0, int Tecnologia = 0)
    {
        String sqlQuery = Consultas_Ec.CONSULTA_TOOLS_MILL;

        if (Tipo != -1) sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_MILL_ID = {0}", Tipo);
        else sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_MILL_ID >= {0}", 0);

        if (Descricao != "") sqlQuery += string.Format(" and T1.TL_TOOL_DESCRIPTION like '%{0}%'", Descricao);
        if (Diametro.Replace(",", ".") != "0.0") sqlQuery += string.Format(" and T1.TL_DIAMETER = '{0}'", Diametro.ToString().Replace(',', '.'));

        if (Unidade != -1) sqlQuery += string.Format(" and T1.TL_UNITS_ID = '{0}'", Unidade);

        if (Tecnologia == 1) sqlQuery += " AND T2.MNT_ROUGHING_TOOL = 1";
        if (Tecnologia == 2) sqlQuery += " AND T2.MNT_FINISHING_TOOL = 1";

        sqlQuery += " ORDER BY T1.TL_TOOL_DESCRIPTION";

        DataTable dt = Objects.CnnBancoEc.ExecutaSql(sqlQuery);

        //Adiciona as colunas
        DataColumn clTipo = new DataColumn("Tipo de ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clTipo);

        DataColumn clUnidade = new DataColumn("Unidade", typeof(String));
        dt.Columns.Add(clUnidade);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            //Ícone da ferramenta de fresamento.
            switch (dt.Rows[x]["Tipo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_0; break;
                case "1": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_1; break;
                case "2": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_2; break;
                case "3": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_3; break;
                case "4": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_4; break;
                case "5": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_5; break;
                case "6": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_6; break;
                case "7": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_7; break;
                case "8": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_8; break;
                case "9": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Mill_9; break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["Unidade_Db"].ToString())
            {
                case "0": dt.Rows[x]["Unidade"] = "Polegada"; break;
                case "1": dt.Rows[x]["Unidade"] = "Milímetro"; break;
                default: break;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco de dados do Edgecam e busca
    /// as ferramentas de torneamento.
    /// </summary>
    /// <param name="Descricao">Descrição da ferramenta</param>
    /// <param name="Tipo">Tipo da ferramenta</param>
    /// <param name="Simbolo">Símbolo da ferramenta</param>
    /// <param name="Unidade">Unidade de medida</param>
    /// <param name="Tecnologia">Tecnologia (para desbaste ou acabamento)</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable ConsultaFerramentas_Turn(String Descricao = "", int Tipo = 0, int Simbolo = 0, int Unidade = 0, int Tecnologia = 0, int LadoFerramenta = 0)
    {
        String sqlQuery = Consultas_Ec.CONSULTA_TOOLS_TURN;

        if (Tipo != -1) sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_TURN_ID = {0}", Tipo);
        else sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_TURN_ID >= {0}", 0);

        if (Descricao != "") sqlQuery += string.Format(" and T1.TL_TOOL_DESCRIPTION like '%{0}%'", Descricao);

        if (Unidade != -1) sqlQuery += string.Format(" and T1.TL_UNITS_ID = '{0}'", Unidade);

        if (Tecnologia == 1) sqlQuery += " AND T2.MNT_ROUGHING_TOOL = 1";
        if (Tecnologia == 2) sqlQuery += " AND T2.MNT_FINISHING_TOOL = 1";

        if (LadoFerramenta == 0) sqlQuery += " AND T1.TL_HAND_OF_TOOL_ID = 0";
        else if (LadoFerramenta == 1) sqlQuery += " AND T1.TL_HAND_OF_TOOL_ID = 1";
        else if (LadoFerramenta == 2) sqlQuery += " AND T1.TL_HAND_OF_TOOL_ID = 2";

        sqlQuery += " ORDER BY T1.TL_TOOL_DESCRIPTION";

        DataTable dt = Objects.CnnBancoEc.ExecutaSql(sqlQuery);

        //Adiciona as colunas
        DataColumn clTipo = new DataColumn("Tipo de ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clTipo);

        DataColumn clUnidade = new DataColumn("Unidade", typeof(String));
        dt.Columns.Add(clUnidade);

        DataColumn clSimbolo = new DataColumn("Símbolo", typeof(String));
        dt.Columns.Add(clSimbolo);

        DataColumn clLado = new DataColumn("Lado da ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clLado);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            //Ícone da ferramenta de fresamento.
            switch (dt.Rows[x]["Tipo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_0; break;
                case "1": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_1; break;
                case "2": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_2; break;
                case "3": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_3; break;
                case "4": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_4; break;
                case "5": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_5; break;
                case "6": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_6; break;
                case "7": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Turn_7; break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["Unidade_Db"].ToString())
            {
                case "0": dt.Rows[x]["Unidade"] = "Polegada"; break;
                case "1": dt.Rows[x]["Unidade"] = "Milímetro"; break;
                default: break;
            }

            //Define o símbolo da ferramenta
            switch (dt.Rows[x]["Símbolo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Símbolo"] = "Nenhum"; break;
                case "1": dt.Rows[x]["Símbolo"] = "A 85 Paralelograma"; break;
                case "2": dt.Rows[x]["Símbolo"] = "B 82 Paralelograma"; break;
                case "3": dt.Rows[x]["Símbolo"] = "C 80 Losango"; break;
                case "4": dt.Rows[x]["Símbolo"] = "D 55 Losango"; break;
                case "5": dt.Rows[x]["Símbolo"] = "E 75 Losango"; break;
                case "6": dt.Rows[x]["Símbolo"] = "H Hexagonal"; break;
                case "7": dt.Rows[x]["Símbolo"] = "K 55 Paralelograma"; break;
                case "8": dt.Rows[x]["Símbolo"] = "L Retangular"; break;
                case "9": dt.Rows[x]["Símbolo"] = "M 86 Losango"; break;
                case "10": dt.Rows[x]["Símbolo"] = "O Octagonal"; break;
                case "11": dt.Rows[x]["Símbolo"] = "P Pentágono"; break;
                case "12": dt.Rows[x]["Símbolo"] = "R Redondo"; break;
                case "13": dt.Rows[x]["Símbolo"] = "S Quadrado"; break;
                case "14": dt.Rows[x]["Símbolo"] = "T Triangular"; break;
                case "15": dt.Rows[x]["Símbolo"] = "V 35 Losango"; break;
                case "16": dt.Rows[x]["Símbolo"] = "W 80 Hexagonal"; break;
                default: break;
            }

            if (dt.Rows[x]["Descrição"].ToString().Contains("{") || dt.Rows[x]["Descrição"].ToString().Contains("}"))
                dt.Rows[x]["Descrição"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Descrição"].ToString());

            //Define o lado de corte da ferramenta (esquerda, direita, etc)
            switch (dt.Rows[x]["LadoFerramenta_Db"].ToString())
            {
                case "0": dt.Rows[x]["Lado da ferramenta"] = Imagens_Edgecam.side_right; break;
                case "1": dt.Rows[x]["Lado da ferramenta"] = Imagens_Edgecam.side_left; break;
                case "2": dt.Rows[x]["Lado da ferramenta"] = Imagens_Edgecam.side_center; break;
                default: break;
            }
        }

        return dt;
    }
    
    /// <summary>
    ///     Executa uma consulta no banco de dados do Edgecam e busca
    /// as ferramentas de furação.
    /// </summary>
    /// <param name="Descricao">Descrição da ferramenta</param>
    /// <param name="Tipo">Tipo da ferramenta</param>
    /// <param name="Diametro">Diâmetro da ferramenta</param>
    /// <param name="Unidade">Unidade de medida</param>
    /// <param name="Tecnologia">Tecnologia (para desbaste ou acabamento)</param>
    /// <param name="LadoFerramenta">Lado da ferramenta (esquerda/direita)</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable ConsultaFerramentas_Hole(String Descricao = "", int Tipo = 0, String Diametro = "", int Unidade = 0, int Tecnologia = 0, int LadoFerramenta = 0)
    {
        String sqlQuery = Consultas_Ec.CONSULTA_TOOLS_HOLE;

        if (Tipo != -1) sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_HOLE_ID = {0}", Tipo);
        else sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_HOLE_ID >= {0}", 0);

        if (Descricao != "") sqlQuery += string.Format(" and T1.TL_TOOL_DESCRIPTION like '%{0}%'", Descricao);

        if (Unidade != -1) sqlQuery += string.Format(" and T1.TL_UNITS_ID = '{0}'", Unidade);

        if (Tecnologia == 1) sqlQuery += " AND T2.MNT_ROUGHING_TOOL = 1";
        if (Tecnologia == 2) sqlQuery += " AND T2.MNT_FINISHING_TOOL = 1";

        if (LadoFerramenta == 0) sqlQuery += " AND T1.TL_HAND_OF_THREAD_ID = 0";
        else if (LadoFerramenta == 1) sqlQuery += " AND T1.TL_HAND_OF_THREAD_ID = 1";
        else if (LadoFerramenta == 2) sqlQuery += " AND T1.TL_HAND_OF_THREAD_ID = 2";

        if (!String.IsNullOrEmpty(Diametro) && Diametro.ToUpper().Trim() != "0.0") sqlQuery += string.Format(" and T1.TL_DIAMETER = '{0}'", Diametro.ToString().Replace(',', '.'));

        sqlQuery += " ORDER BY T1.TL_TOOL_DESCRIPTION";

        DataTable dt = Objects.CnnBancoEc.ExecutaSql(sqlQuery);

        //Adiciona as colunas
        DataColumn clTipo = new DataColumn("Tipo de ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clTipo);

        DataColumn clUnidade = new DataColumn("Unidade", typeof(String));
        dt.Columns.Add(clUnidade);

        DataColumn clSimbolo = new DataColumn("Tipo de rosca", typeof(String));
        dt.Columns.Add(clSimbolo);

        DataColumn clLado = new DataColumn("Lado da ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clLado);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            //Ícone da ferramenta de fresamento.
            switch (dt.Rows[x]["Tipo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_0; break;
                case "1": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_1; break;
                case "2": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_2; break;
                case "3": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_3; break;
                case "4": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_4; break;
                case "5": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_5; break;
                case "6": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_6; break;
                case "7": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_7; break;
                case "8": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Hole_8; break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["Unidade_Db"].ToString())
            {
                case "0": dt.Rows[x]["Unidade"] = "Polegada"; break;
                case "1": dt.Rows[x]["Unidade"] = "Milímetro"; break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["TipoRosca_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo de rosca"] = "TPI (inch)"; break;
                case "1": dt.Rows[x]["Tipo de rosca"] = "Passo (mm)"; break;
                case "2": dt.Rows[x]["Tipo de rosca"] = "Não aplicável"; break;
                default: break;
            }

            if (dt.Rows[x]["Descrição"].ToString().Contains("{") || dt.Rows[x]["Descrição"].ToString().Contains("}"))
                dt.Rows[x]["Descrição"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Descrição"].ToString());

            //Define o lado de corte da ferramenta (esquerda, direita, etc)
            switch (dt.Rows[x]["LadoFerramenta_Db"].ToString())
            {
                case "0": dt.Rows[x]["Lado da ferramenta"] = Imagens_Edgecam.side_right; break;
                case "1": dt.Rows[x]["Lado da ferramenta"] = Imagens_Edgecam.side_left; break;
                default: break;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco de dados do Edgecam e busca
    /// as ferramentas de furação.
    /// </summary>
    /// <param name="Descricao">Descrição da ferramenta</param>
    /// <param name="Tipo">Tipo da ferramenta</param>
    /// <param name="Diametro">Diâmetro da ferramenta</param>
    /// <param name="Unidade">Unidade de medida</param>
    /// <param name="PontoDefasagem">Qual o ponto que o apalpador possuí para marcação</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable ConsultaFerramentas_Probe(String Descricao = "", int Tipo = 0, String Diametro = "", int Unidade = 0, int PontoDefasagem = 0)
    {
        String sqlQuery = Consultas_Ec.CONSULTA_TOOLS_PROBE;

        if (Tipo != -1) sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_PROBE_ID = {0}", Tipo);
        else sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_PROBE_ID >= {0}", 0);

        if (Descricao != "") sqlQuery += string.Format(" and T1.TL_TOOL_DESCRIPTION like '%{0}%'", Descricao);

        if (Unidade != -1) sqlQuery += string.Format(" and T1.TL_UNITS_ID = '{0}'", Unidade);

        if (PontoDefasagem == 0) sqlQuery += " AND T2.MNT_GAUGE_POINT_ID = 0";
        else if (PontoDefasagem == 1) sqlQuery += " AND T2.MNT_GAUGE_POINT_ID = 1";

        if (!String.IsNullOrEmpty(Diametro) && Diametro.ToUpper().Trim() != "0.0") sqlQuery += string.Format(" and T1.TL_DIAMETER = '{0}'", Diametro.ToString().Replace(',', '.'));

        sqlQuery += " ORDER BY T1.TL_TOOL_DESCRIPTION";

        DataTable dt = Objects.CnnBancoEc.ExecutaSql(sqlQuery);

        //Adiciona as colunas
        DataColumn clTipo = new DataColumn("Tipo de ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clTipo);

        DataColumn clUnidade = new DataColumn("Unidade", typeof(String));
        dt.Columns.Add(clUnidade);

        DataColumn clLado = new DataColumn("Ponto de defasagem", typeof(System.Drawing.Image));
        dt.Columns.Add(clLado);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            //Ícone da ferramenta de fresamento.
            switch (dt.Rows[x]["Tipo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Probe_01; break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["Unidade_Db"].ToString())
            {
                case "0": dt.Rows[x]["Unidade"] = "Polegada"; break;
                case "1": dt.Rows[x]["Unidade"] = "Milímetro"; break;
                default: break;
            }

            if (dt.Rows[x]["Descrição"].ToString().Contains("{") || dt.Rows[x]["Descrição"].ToString().Contains("}"))
                dt.Rows[x]["Descrição"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Descrição"].ToString());

            //Define o lado de corte da ferramenta (esquerda, direita, etc)
            switch (dt.Rows[x]["PontoDefasagem_Db"].ToString())
            {
                case "0": dt.Rows[x]["Ponto de defasagem"] = Imagens_Edgecam.probe_primary; break;
                case "1": dt.Rows[x]["Ponto de defasagem"] = Imagens_Edgecam.probe_secondary; break;
                case "null": dt.Rows[x]["Ponto de defasagem"] = Imagens_Edgecam.probe_none; break;
                default: dt.Rows[x]["Ponto de defasagem"] = Imagens_Edgecam.probe_none; break;
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco de dados do Edgecam e busca
    /// as ferramentas aditivas.
    /// </summary>
    /// <param name="Descricao">Descrição da ferramenta</param>
    /// <param name="Tipo">Tipo da ferramenta</param>
    /// <param name="Diametro">Diâmetro da ferramenta</param>
    /// <param name="Unidade">Unidade de medida</param>
    /// <returns>DataTable contendo os dados caso existam</returns>
    internal static DataTable ConsultaFerramentas_Aditive(String Descricao = "", int Tipo = 0, String Diametro = "", int Unidade = 0)
    {
        String sqlQuery = Consultas_Ec.CONSULTA_TOOLS_ADDITIVE;

        if (Tipo != -1) sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_ADDITIVE_ID = {0}", Tipo);
        else sqlQuery += string.Format(" WHERE T1.TL_TOOL_TYPE_ADDITIVE_ID >= {0}", 0);

        if (Descricao != "") sqlQuery += string.Format(" and T1.TL_TOOL_DESCRIPTION like '%{0}%'", Descricao);

        if (Unidade != -1) sqlQuery += string.Format(" and T1.TL_UNITS_ID = '{0}'", Unidade);

        if (!String.IsNullOrEmpty(Diametro) && Diametro.ToUpper().Trim() != "0.0") sqlQuery += string.Format(" and T1.TL_DIAMETER = '{0}'", Diametro.ToString().Replace(',', '.'));

        sqlQuery += " ORDER BY T1.TL_TOOL_DESCRIPTION";

        DataTable dt = Objects.CnnBancoEc.ExecutaSql(sqlQuery);

        //Adiciona as colunas
        DataColumn clTipo = new DataColumn("Tipo de ferramenta", typeof(System.Drawing.Image));
        dt.Columns.Add(clTipo);

        DataColumn clUnidade = new DataColumn("Unidade", typeof(String));
        dt.Columns.Add(clUnidade);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            //Ícone da ferramenta de fresamento.
            switch (dt.Rows[x]["Tipo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Additive_0; break;
                case "1": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Additive_1; break;
                case "2": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Additive_2; break;
                case "3": dt.Rows[x]["Tipo de ferramenta"] = Imagens_Edgecam.Additive_3; break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["Unidade_Db"].ToString())
            {
                case "0": dt.Rows[x]["Unidade"] = "Polegada"; break;
                case "1": dt.Rows[x]["Unidade"] = "Milímetro"; break;
                default: break;
            }

            if (dt.Rows[x]["Descrição"].ToString().Contains("{") || dt.Rows[x]["Descrição"].ToString().Contains("}"))
                dt.Rows[x]["Descrição"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Descrição"].ToString());
        }

        return dt;
    }

    /// <summary>
    ///     Método que consulta o inventário de ferramentas do Edgecam Manager.
    /// </summary>
    /// <param name="IdTool">Id da ferramenta</param>
    /// <param name="Descricao">Nome da ferramenta</param>
    /// <param name="Unidade">Unidade de medida (polegadas ou milímetros)</param>
    /// <param name="TipoFerramenta">Tipo de ferramenta (fresament, torneamento, furação, etc...)</param>
    /// <param name="SubtipoFerramenta">Sub tipo de ferramenta (fresa de topo, etc...)</param>
    /// <param name="UnidadeOrganizacional">Unidade da empresa em que se encontra o inventário</param>
    /// <param name="EstadoEstoque">Estado do estoque.</param>
    /// <returns>DataTable contendo os dados resultantes das consultas</returns>
    internal static DataTable Consulta_InventarioFerramentas(String IdTool = "", String Descricao = "", int Unidade = -1, int TipoFerramenta = -1, int SubtipoFerramenta = -1, String UnidadeOrganizacional = "", int EstadoEstoque = -1)
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_INVENTARIO_FERRAMENTAS;

        if (!String.IsNullOrEmpty(IdTool)) sqlQuery += String.Format(" and IdTool = {0}", IdTool);
        if (!String.IsNullOrEmpty(Descricao)) sqlQuery += String.Format(" and DescricaoTool like '%{0}%'", Descricao);
        if (Unidade != -1) sqlQuery += String.Format(" and UnidadeMedida = {0}", Unidade);
        if (TipoFerramenta != -1) sqlQuery += String.Format(" and TipoTool = {0}", TipoFerramenta);
        if (SubtipoFerramenta != -1) sqlQuery += String.Format(" and SubTipoTool = {0}", SubtipoFerramenta);
        if (!String.IsNullOrEmpty(UnidadeOrganizacional) && UnidadeOrganizacional.ToUpper().Trim() != "(TODOS)") sqlQuery += String.Format(" and Unidade = '{0}'", UnidadeOrganizacional);
        if (EstadoEstoque != -1) sqlQuery += String.Format(" and EstadoEstoque = {0}", EstadoEstoque);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        //Adiciona as colunas
        DataColumn clTipo = new DataColumn("Tipo", typeof(System.Drawing.Image));
        dt.Columns.Add(clTipo);

        DataColumn clSubtipo = new DataColumn("Sub tipo", typeof(System.Drawing.Image));
        dt.Columns.Add(clSubtipo);

        DataColumn clUnidade = new DataColumn("Unidade", typeof(String));
        dt.Columns.Add(clUnidade);

        DataColumn clEstado = new DataColumn("Estado do estoque", typeof(System.Drawing.Image));
        dt.Columns.Add(clEstado);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            String tipoTool = "";

            //Ícone da ferramenta de fresamento.
            switch (dt.Rows[x]["Tipo_Db"].ToString())
            {
                case "0": dt.Rows[x]["Tipo"] = Imagens_Edgecam.Mill_0; tipoTool = "0"; break;
                case "1": dt.Rows[x]["Tipo"] = Imagens_Edgecam.Turn_0; tipoTool = "1"; break;
                case "2": dt.Rows[x]["Tipo"] = Imagens_Edgecam.Hole_0; tipoTool = "2"; break;
                case "3": dt.Rows[x]["Tipo"] = Imagens_Edgecam.Probe_01; tipoTool = "3"; break;
                case "4": dt.Rows[x]["Tipo"] = Imagens_Edgecam.Additive_0; tipoTool = "4"; break;
                default: break;
            }

            //Ícone da ferramenta de fresamento.
            switch (tipoTool)
            {
                case "0":
                    switch (dt.Rows[x]["SubTipo_Db"].ToString())
                    {
                        case "0": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_0; break;
                        case "1": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_1; break;
                        case "2": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_2; break;
                        case "3": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_3; break;
                        case "4": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_4; break;
                        case "5": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_5; break;
                        case "6": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_6; break;
                        case "7": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_7; break;
                        case "8": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_8; break;
                        case "9": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Mill_9; break;
                        default: break;
                    }
                    break;
                case "1": 
                    switch (dt.Rows[x]["SubTipo_Db"].ToString())
                    {
                        case "0": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_0; break;
                        case "1": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_1; break;
                        case "2": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_2; break;
                        case "3": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_3; break;
                        case "4": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_4; break;
                        case "5": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_5; break;
                        case "6": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_6; break;
                        case "7": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Turn_7; break;
                        default: break;
                    }
                    break;
                case "2": 
                    switch (dt.Rows[x]["SubTipo_Db"].ToString())
                    {
                        case "0": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_0; break;
                        case "1": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_1; break;
                        case "2": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_2; break;
                        case "3": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_3; break;
                        case "4": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_4; break;
                        case "5": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_5; break;
                        case "6": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_6; break;
                        case "7": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_7; break;
                        case "8": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Hole_8; break;
                        default: break;
                    }
                    break;
                case "3": 
                    switch (dt.Rows[x]["SubTipo_Db"].ToString())
                    {
                        case "0": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Probe_01; break;
                        default: break;
                    }
                    break;
                case "4": 
                    switch (dt.Rows[x]["SubTipo_Db"].ToString())
                    {
                        case "0": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Additive_0; break;
                        case "1": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Additive_1; break;
                        case "2": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Additive_2; break;
                        case "3": dt.Rows[x]["Sub tipo"] = Imagens_Edgecam.Additive_3; break;
                        default: break;
                    }
                    break;
                default: break;
            }

            //Define a unidade de medida da ferramenta.
            switch (dt.Rows[x]["Unidade_Db"].ToString())
            {
                case "0": dt.Rows[x]["Unidade"] = "Polegada"; break;
                case "1": dt.Rows[x]["Unidade"] = "Milímetro"; break;
                default: break;
            }

            if (dt.Rows[x]["Nome da ferramenta"].ToString().Contains("{") || dt.Rows[x]["Nome da ferramenta"].ToString().Contains("}"))
                dt.Rows[x]["Nome da ferramenta"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Nome da ferramenta"].ToString());

            if (Convert.ToInt16(dt.Rows[x]["Quantidade em estoque"]) == 0) 
                dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.White;
            else if (Convert.ToInt16(dt.Rows[x]["Quantidade em estoque"]) >= Convert.ToInt16(dt.Rows[x]["Estoque mínimo"]))
                dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.Green;
            else if (Convert.ToInt16(dt.Rows[x]["Quantidade em estoque"]) < Convert.ToInt16(dt.Rows[x]["Estoque mínimo"]))
                dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.Red;
            else dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.Orange;

            //switch (dt.Rows[x]["Estado_Db"].ToString())
            //{
            //    case "0": dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.White; break;
            //    case "1": dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.Green; break;
            //    case "2": dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.Red; break;
            //    case "3": dt.Rows[x]["Estado do estoque"] = Edgecam_Manager.Properties.Resources.Orange; break;
            //    default: break;
            //}
        }

        return dt;
    }

    /// <summary>
    ///     Método que consulta os movimentos de inventários de ferramentas do Edgecam Manager.
    /// </summary>
    /// <param name="IdInventario">Id do inventário</param>
    /// <param name="Descricao">Descrição</param>
    /// <param name="AdicionarColunaImagem">True para adicionar uma coluna do tipo imagem que demonstra se
    /// o dado auxiliar está ou não ativo.</param>
    /// <returns></returns>
    internal static DataTable Consulta_MovEstFerramentas(String IdInventario = "", String Descricao = "", Boolean AdicionarColunaImagem = false)
    {
        String sqlQuery = Consultas_EcMgr.CONSULTA_MOVIMENTOS_INVENTARIO_TOOL;

        if (!String.IsNullOrEmpty(IdInventario)) sqlQuery += String.Format(" where IdTool = '{0}'", IdInventario);
        if (!String.IsNullOrEmpty(Descricao)) sqlQuery += String.Format(" and Descricao like '%{0}%'", Descricao);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);

        if (AdicionarColunaImagem)
        {
            DataColumn clAcao = new DataColumn("Ação", typeof(System.Drawing.Bitmap));
            dt.Columns.Add(clAcao);

            if (dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    switch (dt.Rows[x]["Acao"].ToString())
                    {
                        case "1": dt.Rows[x]["Ação"] = Edgecam_Manager.Properties.Resources.estoque_entrada; break;
                        case "2": dt.Rows[x]["Ação"] = Edgecam_Manager.Properties.Resources.estoque_saida;   break;
                        case "3": dt.Rows[x]["Ação"] = Imagens_NewLookInterface.caixa_16; break;
                        case "4": dt.Rows[x]["Ação"] = Imagens_NewLookInterface.preview_visualizar_16; break;
                    }
                }
            }            
        }

        return dt;
    }

    /// <summary>
    ///     Método que consulta as rotas de produção do Edgecam Manager.  
    /// </summary>
    /// <param name="MostrarColunaSelecionar">True para consultar a coluna 'selecionar'</param>
    /// <param name="NomeRota">Nome da rota de produção.</param>
    /// <returns>DataTable contendo os dados resultantes das consultas</returns>
    internal static DataTable Consulta_RotasProducao(Boolean MostrarColunaSelecionar, String NomeRota = "")
    {
        String sqlQuery = "";
        if (MostrarColunaSelecionar)
            sqlQuery += "select	'Selecionar' as Selecionar,";
        else sqlQuery += " select";

        sqlQuery += Consultas_EcMgr.CONSULTA_ROTAS_PRODUCAO;

        if (!String.IsNullOrEmpty(NomeRota)) sqlQuery += String.Format(" and r.NomeRota like '%{0}%'", NomeRota);

        sqlQuery += " order by r.NomeRota, o.OrdemExecucao";

        return Objects.CnnBancoEcMgr.ExecutaSql(sqlQuery);
    }

    internal static DataTable Consulta_Trabalhos(String IdTrabalho = "", String NomeTrabalho = "", String Cliente = "", int Estado = -1, String Maquina = "", String Programador = "", String Material = "", Boolean AdicionarColunaImagem = false)
    {
        String query = Consultas_Ec.CONSULTA_TRABALHOS;

        if (!String.IsNullOrEmpty(IdTrabalho)) query += String.Format(" and JOB_JOB_ID = {0}", IdTrabalho);
        if (!String.IsNullOrEmpty(NomeTrabalho) && NomeTrabalho != "(Todos)") query += String.Format(" and JOB_JOB_DESCRIPTION LIKE '%{0}%'", NomeTrabalho);
        if (!String.IsNullOrEmpty(Cliente) && Cliente != "(Todos)") query += String.Format(" and JOB_CUSTOMER = {0}", Cliente);
        if (Estado != -1) query += String.Format(" and JOB_JOB_STATUS_ID = {0}", Estado);
        if (!String.IsNullOrEmpty(Maquina) && Maquina != "(Todos)") query += String.Format(" and JOB_LOCATION = {0}", Maquina);
        if (!String.IsNullOrEmpty(Programador) && Programador != "(Todos)") query += String.Format(" and JOB_PROGRAMMER = {0}", Programador);
        if (!String.IsNullOrEmpty(Material) && Material != "(Todos)") query += String.Format(" and JOB_MATERIAL_DESC = {0}", Material);

        DataTable dt = Objects.CnnBancoEc.ExecutaSql(query);

        if (AdicionarColunaImagem)
        {
            DataColumn clEstado = new DataColumn("Estado", typeof(System.Drawing.Bitmap));
            dt.Columns.Add(clEstado);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    switch (dt.Rows[x]["Status_Db"].ToString().ToUpper())
                    {
                        case "0": dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.White; break;
                        case "1": dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.Green; break;
                        case "2": dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.PartiallyReleased; break;
                        case "3": dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.Orange; break;
                        case "4": dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.Global; break;
                        case "5": dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.Cancel; break;
                        default: dt.Rows[x]["Estado"] = Edgecam_Manager.Properties.Resources.White; break;
                    }

                    dt.Rows[x]["Nome do trabalho"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Nome do trabalho"].ToString());
                    dt.Rows[x]["Material"] = Edgecam.ConvertAscII_ToString(dt.Rows[x]["Material"].ToString());
                }
            }
        }

        return dt;
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os centros de trabalho.
    /// </summary>
    /// <param name="NomeMqn">Nome da máquina</param>
    /// <param name="Ambiente">Qual ambiente é a máquina</param>
    /// <param name="Visivel">Se a máquina está ou não ativa.</param>
    /// <returns>Retorna uma DataTable</returns>
    internal static DataTable Consulta_Maquinas(String NomeMqn = "", int Ambiente = 0, int Visivel = 0)
    {
        try
        {
            String querySql = Consultas_EcMgr.CONSULTA_MAQUINAS;

            if (!String.IsNullOrEmpty(NomeMqn)) querySql += String.Format(" and NomeMaquina like '%{0}%'", NomeMqn);
            if (Ambiente > 0) querySql += String.Format(" and Ambiente = '{0}'", Ambiente);
            if (Visivel > 0) querySql += String.Format(" and MqnAtiva = '{0}'", Visivel);

            // Armazena temporariamente a tabela
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(querySql);

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clPrioridade = new DataColumn("Ativa", typeof(System.Drawing.Image));
            dt.Columns.Add(clPrioridade);

            DataColumn clAmbiente = new DataColumn("Ambiente", typeof(String));
            dt.Columns.Add(clAmbiente);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Ícones de visibilidade
                if (dt.Rows[i]["MqnAtiva_db"].ToString().ToUpper() == "0") dt.Rows[i]["Ativa"] = Edgecam_Manager.Properties.Resources.Red;
                if (dt.Rows[i]["MqnAtiva_db"].ToString().ToUpper() == "1") dt.Rows[i]["Ativa"] = Edgecam_Manager.Properties.Resources.Green;

                //Define o ambiente em forma de texto.
                if (dt.Rows[i]["Ambiente_db"].ToString().ToUpper() == "0") dt.Rows[i]["Ambiente"] = "Torneamento";
                if (dt.Rows[i]["Ambiente_db"].ToString().ToUpper() == "1") dt.Rows[i]["Ambiente"] = "Fresamento";
                if (dt.Rows[i]["Ambiente_db"].ToString().ToUpper() == "2") dt.Rows[i]["Ambiente"] = "Aditivas";
            }

            return dt;
        }
        catch (Exception ex)
        {
            // Em caso de alguma exceção, retorna vazio.
            throw new Exception("Erro ao consultar as ordens de produção", ex);
        }
    }

    /// <summary>
    ///     Executa uma consulta no banco intermediário do sistema buscando os logs de erros do sistema.
    /// </summary>
    /// <param name="TipoErro">Tipo de erro, onde: Erro = 0 | Aviso = 1 | Informacao = 2 | Notificacao = 3</param>
    /// <param name="Data">Data dos erros ocorridos</param>
    /// <returns>DataTabel contendo os dados já filtrados para adicionar na grade de dados.</returns>
    internal static DataTable Consulta_Logs(int TipoErro, String Data)
    {
        try
        {
            String q = Consultas_Log.CONSULTA_TODOS_OS_LOGS;

            if (TipoErro >= 0)
                q += String.Format(" AND TipoErro = {0}", TipoErro);

            if (!String.IsNullOrEmpty(Data))
                q += String.Format(" AND CAST (DtHoraErro AS DATE) LIKE '%{0}%'", Data);

            q += " ORDER BY id";

            DataTable dt = Objects.CnnBancoLog.ExecutaSql(q);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Cria as colunas de armazenamento dos ícones da interface.
                DataColumn clImg = new DataColumn("Tipo", typeof(System.Drawing.Image));
                dt.Columns.Add(clImg);

                // Adiciona os ícones na tuplas da table (ícones da prioridade)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Ícones de visibilidade
                    if (dt.Rows[i]["Tipo_Db"].ToString().ToUpper() == "0") dt.Rows[i]["Tipo"] = Edgecam_Manager.Properties.Resources.ex_error;
                    if (dt.Rows[i]["Tipo_Db"].ToString().ToUpper() == "1") dt.Rows[i]["Tipo"] = Edgecam_Manager.Properties.Resources.ex_alert;
                    if (dt.Rows[i]["Tipo_Db"].ToString().ToUpper() == "2") dt.Rows[i]["Tipo"] = Edgecam_Manager.Properties.Resources.ex_information;
                    if (dt.Rows[i]["Tipo_Db"].ToString().ToUpper() == "3") dt.Rows[i]["Tipo"] = Edgecam_Manager.Properties.Resources.ex_notification;
                }
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar o registro/histórico de logs", ex);
        }
    }

    /// <summary>
    ///     Query database information about version, service pack, etc.
    /// </summary>
    /// <returns>DataTable with data or null (if occurs some unexpected exception)</returns>
    internal static DataTable Query_DatabaseInfo()
    {
        try { return Objects.CnnBancoEcMgr.ExecutaSql("select * from Versao"); }
        catch { return null; }
    }
}