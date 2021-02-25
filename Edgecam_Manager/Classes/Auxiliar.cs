using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using Edgecam_Manager;




#region Enumeradores públicos

    /// <summary>
    ///     Enumerador que define a unidade de medida.
    /// </summary>
    internal enum e_UnidadeMedida
    {
        Polegadas = 0,
        Milimetros = 1,
        Todos = 2
    }

    /// <summary>
    ///     Enumerador que determina qual tipo de campos deverá ser consultado (import/expor).
    /// </summary>
    internal enum e_Campos
    {
        Importar,
        Exportar
    }

    /// <summary>
    ///     Enumerador que determina 
    /// </summary>
    internal enum e_Extensoes
    {
        Bloqueados,
        Permitidos
    }

    /// <summary>
    ///     Enumerador voltado para exceções (exclusivamente)
    /// </summary>
    internal enum e_TipoErroEx
    {
        /// <summary>
        ///     Erro não tratado da aplicação.
        /// </summary>
        Erro = 0,
        /// <summary>
        ///     Aviso ao usuário de alguma ação que está tentando executar
        /// </summary>
        Aviso = 1,
        /// <summary>
        ///     Informação de 'algo' que está sendo feito pelo usuário.
        /// </summary>
        Informacao = 2,
        /// <summary>
        ///     Notificação do usuário.
        /// </summary>
        Notificacao = 3
    }

    /// <summary>
    ///     Enumerado que contém o tipo de movimento de inventário à ser realizado.
    /// </summary>
    internal enum e_TipoMovEstoque
    {
        Entrada,
        Saida,
        Transferencia,
        Outro
    }

#endregion

#region Classes

/// <summary>
///     Contém as informações de versão armazenadas no banco de dados auxiliar.
/// </summary>
internal class Versao
{
    /// <summary>
    ///     Contém a versão atual do sistema.
    /// </summary>
    public String VersaoSistema;

    /// <summary>
    ///     Contém a versão do banco de dados.
    /// </summary>
    public String VersaoDatabase;

    /// <summary>
    /// Contém o service pack do sistema.
    /// </summary>
    public String ServicePackSistema;

    /// <summary>
    ///     Contém o nome do cliente.
    /// </summary>
    public String Cliente;

    /// <summary>
    ///     Contém a data da última atualização do lantek.
    /// </summary>
    public String DataUltAtualizacao;
}

/// <summary>
///     Representa um usuário logado atualmente no sistema.
/// </summary>
internal class Usuario
{
    public int Id { get; set; }
    public String Login { get; set; }
    public String Senha { get; set; }
    public String Nome { get; set; }
    public String SobreNome { get; set; }
    public Nullable<Int16> TipoUser { get; set; }
    public Nullable<Int16> Idade { get; set; }

    /// <summary>
    ///     M - Masculino
    ///     F - Feminino
    /// </summary>
    public String Sexo { get; set; }
    public String Cargo { get; set; }
    public String Endereco { get; set; }

    /// <summary>
    ///     Array de byte contendo a imagem
    /// </summary>
    public byte[] Imagem { get; set; }
    public String Telefone { get; set; }
    public String Ramal { get; set; }

    public String Cep { get; set; }
    public String Email { get; set; }
    public String Gestor { get; set; }

    /// <summary>
    ///     1 - Ativo
    ///     0 - Inativo
    /// </summary>
    public Nullable<Int16> UserAtivo { get; set; }
    public String UnidadeOrg { get; set; }

    /// <summary>
    ///     0 - Usuário não trocou a senha
    ///     1 - Usuário trocou a senha
    /// </summary>
    public Nullable<Int16> SenhaTrocada { get; set; }

    /// <summary>
    ///     0 - Não expirar senha
    ///     1 - Expirar senha
    /// </summary>
    public Nullable<Int16> ExpirarSenha { get; set; }

    /// <summary>
    ///     NULL - Não tem data para expiração
    /// </summary>
    public Nullable<DateTime> DtExpiracaoSenha { get; set; }

    /// <summary>
    ///     0 - Usuário não está logado
    ///     1 - Usuário está logado
    /// </summary>
    public Boolean IsLogged { get; set; }

    /// <summary>
    ///     Instância o objeto informando como parâmetro a string de conexão com o banco auxiliar.
    /// </summary>
    public Usuario(String Usuario, String Senha)
    {
        try
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@LOGIN", new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A).EncryptString(Usuario));
            dic.Add("@SENHA", new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A).EncryptString(Senha));

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_USUARIO, dic);

            LoadUser(dt);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar conectar ao servidor e autenticar o usuário", ex);
        }
    }

    private void LoadUser(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            Utilities util = new Utilities();
            Id                  = Convert.ToInt16(dt.Rows[0]["id"].ToString());
            Login               = (new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A)).DecryptString(dt.Rows[0]["Login"].ToString());
            Senha               = dt.Rows[0]["Senha"].ToString();
            Nome                = dt.Rows[0]["Nome"].ToString();
            SobreNome           = dt.Rows[0]["Sobrenome"].ToString();
            TipoUser            = Convert.ToInt16(dt.Rows[0]["TipoUsuario"].ToString() == "" ? "0" : dt.Rows[0]["TipoUsuario"].ToString());
            Idade               = Convert.ToInt16(dt.Rows[0]["Idade"].ToString() == "" ? "0" : dt.Rows[0]["Idade"].ToString());

            Sexo                = dt.Rows[0]["Sexo"].ToString();
            Cargo               = dt.Rows[0]["Cargo"].ToString();
            Endereco            = dt.Rows[0]["Endereço"].ToString();

            Imagem              = String.IsNullOrEmpty(dt.Rows[0]["Imagem"].ToString()) ? null : (byte[])dt.Rows[0]["Imagem"];
            //Imagem              = dt.Rows[0]["Imagem"].ToString();
            Telefone            = dt.Rows[0]["Telefone"].ToString();
            Ramal               = dt.Rows[0]["Ramal"].ToString();

            Cep                 = dt.Rows[0]["CEP"].ToString();
            Email               = dt.Rows[0]["Email"].ToString();
            Gestor              = dt.Rows[0]["Gestor"].ToString();
            UserAtivo           = Convert.ToInt16(dt.Rows[0]["Ativo"].ToString());
            UnidadeOrg          = dt.Rows[0]["UnidadeOrg"].ToString();
            SenhaTrocada        = Convert.ToInt16(dt.Rows[0]["SenhaTrocada"].ToString() == "" ? "0" : dt.Rows[0]["SenhaTrocada"].ToString());
            ExpirarSenha        = Convert.ToInt16(dt.Rows[0]["ExpirarSenha"].ToString() == "" ? "0" : dt.Rows[0]["ExpirarSenha"].ToString());
            DtExpiracaoSenha    = Convert.ToDateTime(util.IsObjectNull(dt.Rows[0]["DataExpiracaoSenha"], null));
            IsLogged            = Convert.ToBoolean(dt.Rows[0]["IsLogged"].ToString());
        }
    }

    /// <summary>
    ///     Convert a String em um byte array.
    /// </summary>
    /// <param name="Imagem">String da imagem.</param>
    /// <returns>Byte array</returns>
    private byte[] ConvertStringEmByte(String Imagem)
    {
        if (String.IsNullOrEmpty(Imagem))
            return null;
        else
        {
            return System.IO.File.ReadAllBytes(Imagem);
        }
    }

}

/// <summary>
///     Representes a single filter saved in database.
/// </summary>
internal class Filter
{
    public int Id;
    public String Interface;
    public String Modulo;
    public String Usuario;
    public int IdUsuario;
    public String NomeFiltro;
    public Boolean IsPrivate;
    public String Fields;
    public Boolean Ativo;
    public DateTime DtCriacao;
    public DateTime DtUltMod;
}

/// <summary>
///     Representes a single filter saved in database.
/// </summary>
internal class Relatorio
{
    public int Id;
    public String NomeRelatorio;
    public String Descricao;
    public String Interface;
    public String Modulo;
    /// <summary>
    ///     Has a XML inside this string.
    /// </summary>
    public String ConteudoRelatorio;
    public Boolean IsPrivate;
    /// <summary>
    /// 1= Visible to all interfaces, 2 = visible to main form, 3 = visible to child forms.
    /// </summary>
    public int VisibilidadeInterface;
    public String Idioma;
    public String SQL;
    /// <summary>
    ///     0 = Significa que é um relatório do tipo lista, 1 = Significa que é um relatório
    /// para ver um registro apenas.
    /// </summary>
    public Boolean ViewOneRecord;
    //public String Usuario;
    public String Versao;    
    public Boolean Ativo;
    public String UsrCrt;
    public DateTime DtCrt;
    public String UsrUltMod;
    public DateTime DtUltMod;
}

/// <summary>
///     Classe que representa uma tarefa.
/// </summary>
internal class Tarefa
{
    /// <summary>
    ///     Chave primária
    /// </summary>
    public String IdTarefa { get; set; }

    /// <summary>
    ///     Título da tarefa
    /// </summary>
    public String Assunto { get; set; }

    /// <summary>
    ///     Conteúdo da tarefa
    /// </summary>
    public String Instrucoes { get; set; }

    /// <summary>
    ///     Define o tipo de tarefa que deve ser executada.
    ///     1 - Notificação
    ///     2 - Chamada telefônica
    ///     3 - Email
    ///     4 - Reunião
    ///     5 - Processo
    ///     6 - Tarefa 
    /// </summary>
    public String TipoTarefa { get; set; }

    /// <summary>
    ///     1 - Baixa
    ///     2 - Média
    ///     3 - Alta
    /// </summary>
    public String Prioridade { get; set; }

    /// <summary>
    ///     Data para iniciar a tarefa
    /// </summary>
    public String DtInicio { get; set; }

    /// <summary>
    ///     Data limite (prazo) para conclusão da tarefa
    /// </summary>
    public String DtLimitePrazo { get; set; }

    /// <summary>
    ///     Data de solicitação desta tarefa (data de criação)
    /// </summary>
    public String DtCriacao { get; set; }

    /// <summary>
    ///     Data da última modificação desta tarefa.
    /// </summary>
    public String DtUltimaMod { get; set; }

    /// <summary>
    ///     Usuário responsável pela última modificação
    /// </summary>
    public String UsuarioUltimaMod { get; set; }

    /// <summary>
    ///     Usuário responsável por executar esta tarefa.
    /// </summary>
    public String UsuarioDesignado { get; set; }

    /// <summary>
    ///     Usuário que solicitou a tarefa
    /// </summary>
    public String UsuarioSolicitante { get; set; }

    /// <summary>
    ///     0 - Não executar a cada um X período de tempo
    ///     1 - Executar a cada um X período de tempo
    /// </summary>
    public String ExecucaoRecursiva { get; set; }

    /// <summary>
    ///     0 – Nada
    ///     1 – Dias
    ///     2 – Semanas
    ///     3– Meses
    ///     4 – Anos
    /// </summary>
    public String PeriodoExecucao { get; set; }

    /// <summary>
    ///     Primeira data de inicio da tarefa
    /// </summary>
    public String DtPeriodoInicio { get; set; }

    /// <summary>
    ///     Próxima data para execução da tarefa
    /// </summary>
    public String DtNextPeriodo { get; set; }

    /// <summary>
    ///     0 - Não lido
    ///     1 - Lido
    /// </summary>
    public String Lido { get; set; }

    /// <summary>
    ///     0 - Não completada ainda
    ///     1 - Tarefa completa/concluída.
    /// </summary>
    public String Completo { get; set; }
}

/// <summary>
///     Objeto que representa uma ordem de serviço
/// </summary>
internal class Ordem
{
    /// <summary>
    ///     Chave primária da tabela.
    /// </summary>
    public String IdOrdem { get; set; }

    /// <summary>
    ///     Ordem de produção avinda do banco intermediário.
    /// </summary>
    public String OrdemProducao { get; set; }

    /// <summary>
    ///     Contém o pedido da ordem de produção.
    /// </summary>
    public String Pedido { get; set; }

    /// <summary>
    ///     Nome do cliente.
    /// </summary>
    public String Cliente { get; set; }

    /// <summary>
    ///     Material para usinagem.
    /// </summary>
    public String Material { get; set; }

    /// <summary>
    ///     Usuário responsável por realizar/executar a ordem de produção.
    /// </summary>
    public String UsuarioResp { get; set; }

    /// <summary>
    ///     1 – Fresamento
    ///     2 – Torneamento
    ///     3 – Aditiva
    ///     4 – Wire (Erosão a fio)
    /// </summary>
    public String Ambiente { get; set; }

    /// <summary>
    ///     1 Pendente de planejamento
    ///     1 – Pendente de planejamento
    ///     2 – Em andamento (iniciado)
    ///     3 – Aguardando aprovação (sob revisão)
    ///     4 – Não aprovado (Em revisão)
    ///     5 – Liberado (Concluído)
    ///     6 – Cancelado
    ///     7 – Atrasado
    /// </summary>
    public String Estado { get; set; }

    /// <summary>
    ///     Nome da peça
    /// </summary>
    public String Artigo { get; set; }

    /// <summary>
    ///     Contém o diretório físico da peça.
    /// </summary>
    public String CaminhoArtigo { get; set; }

    /// <summary>
    ///     1 Não iniciado
    ///     2 Encaixado (iniciado)
    ///     3 Em programação
    ///     4 Disponível para produção
    ///     5 Em produção
    ///     6 Finalizado
    /// </summary>
    public String EstadoOperacao { get; set; }

    /// <summary>
    ///     Máquina
    /// </summary>
    public String CentroTrabalho { get; set; }

    /// <summary>
    ///     Nome do trabalho no Edgecam
    /// </summary>
    public String Trabalho { get; set; }

    /// <summary>
    ///     Quantidade solicitada.
    /// </summary>
    public String QtdeSolicitada { get; set; }

    /// <summary>
    ///     Quantidade realizada (programada)
    /// </summary>
    public String QtdeRealizada { get; set; }

    /// <summary>
    ///     Data Requerida
    /// </summary>
    public String DataRequerida { get; set; }

    /// <summary>
    ///     Data para entrega
    /// </summary>
    public String DataEntrega { get; set; }

    /// <summary>
    ///     Descrição auxiliar.
    /// </summary>
    public String Descricao { get; set; }

    /// <summary>
    ///     Dados auxiliares 1.
    /// </summary>
    public String DadosAux1 { get; set; }

    /// <summary>
    ///     Dados auxiliares 2.
    /// </summary>
    public String DadosAux2 { get; set; }

    /// <summary>
    ///     Dados auxiliares 3.
    /// </summary>
    public String DadosAux3 { get; set; }

    /// <summary>
    ///     Dados auxiliares 3.
    /// </summary>
    public String DadosAux4 { get; set; }

    /// <summary>
    ///     Dados auxiliares 3.
    /// </summary>
    public String DadosAux5 { get; set; }

    /// <summary>
    ///     Dados auxiliares 3.
    /// </summary>
    public String DadosAux6 { get; set; }

    /// <summary>
    ///     Dados auxiliares 3.
    /// </summary>
    public String DadosAux7 { get; set; }

    /// <summary>
    ///     Dados auxiliares 3.
    /// </summary>
    public String DadosAux8 { get; set; }
}

/// <summary>
///     Representa um objeto de orçamento.
/// </summary>
internal class Orcamento
{
    public int Id;
    /// <summary>
    ///     Código/nome do orçamento.
    /// </summary>
    public String CodOrca;
    public String Cliente;
    public String Contato;
    public String Cidade;
    /// <summary>
    ///     Estado.
    /// </summary>
    public String UF;
    public String Pais;
    public int EstadoOrc;
    /// <summary>
    ///     Externo (clietes) ou interno (outras unidades).
    /// </summary>
    public int TipoVis;
    /// <summary>
    ///     Tipo de orçamento cadastrado pelo usuário.
    /// </summary>
    public int IdTipoOrcamento;
    /// <summary>
    ///     Nome do tipo de orçamento cadastrado pelo usuário.
    /// </summary>
    public String TipoOrcamento;
    public int IdTipoPag;
    public String NomePag;
    /// <summary>
    ///     Tipo de origem que teve o orçamento (em inteiro)
    /// </summary>
    public int OrcOrigem;

    /// <summary>
    ///     Tipo de origem que teve o orçamento (em forma de texto)
    /// </summary>
    public String NomeOrcOrigem
    {
        get
        {
            switch (OrcOrigem)
            {
                case 1: return "Orçamento simples";
                case 2: return "Orçamento detalhado";
                case 3: return "Orçamento técnico";
                default: return "Orçamento simples";
            }
        }
    }

    /// <summary>
    ///     ID da moeda em que foi feito a cotação.
    /// </summary>
    public int IdMoeda;
    /// <summary>
    ///     Nome da moeda
    /// </summary>
    public String NomeMoeda;
    /// <summary>
    ///     Valor da cotação.
    /// </summary>
    public double ValorMoeda;
    public Boolean PossuiDesconto;
    public double ValorDesconto;
    public Boolean FreteIncluso;
    public double ValorFrete;
    /// <summary>
    ///     0 = Nada, 1 = Markup, 2 = Margem de contribuição(valor em percentual), 
    /// </summary>
    public int TipoVenda;
    /// <summary>
    ///     Nome do tipo de venda com base no valor da variável 'TipoVenda'
    /// </summary>
    public String NomeTipoVenda
    {
        get
        {
            switch (TipoVenda)
            {
                case 0: return "Nenhum";
                case 1: return "Markup";
                case 2: return "Margem de contribuição";
                default: return "Nenhum";
            }
        }

    }
    /// <summary>
    ///     Um objeto contendo dados do markup selecionado pelo usuário.
    /// </summary>
    public Markup DadosMarkup;
    /// <summary>
    ///     Valor a ser somado na venda (porcentagem de soma ou markup).
    /// </summary>
    public double ValorSomarVenda;
    public double ValorTotal;
    public double ValorTotalComDesconto;
    public String Vendedor;
    public String Orcamentista;
    /// <summary>
    ///     Data que está sendo feito a cotação no orçamento
    /// </summary>
    public DateTime DtCotacao;
    /// <summary>
    ///     Data de envio do orçamento para o cliente.
    /// </summary>
    public DateTime DtEmissao;
    /// <summary>
    ///     Data de validade do orçamento.
    /// </summary>
    public DateTime DtValidade;
    /// <summary>
    ///     Data estipulada para entregar os itens do orçamento produzidos.
    /// </summary>
    public DateTime DtPrevEntrega;
    public int Versao;
    public Boolean Ativo;
    public String UsuarioCriacao;
    public DateTime DtCriacao;
    public String UsuarioUltMod;
    public DateTime DtUltimaMod;
}

/// <summary>
///     Representa um objeto de imposto.
/// </summary>
internal class Imposto
{
    public int Id;
    public string Nome;
    public string Descricao;
    public int Prioridade;
    public int TipoImposto;
    public double ValorImposto;
    public int TipoCobranca;
    public bool TemValidade;
    public DateTime ValidoAte;
    public bool CtrlVersao;
    public string Versao;

    /// <summary>
    ///     Data de criação do imposto.
    /// </summary>
    public DateTime DtCrt;

    /// <summary>
    ///     Usuário responsável pela criação.
    /// </summary>
    public string UsrCrt;

    /// <summary>
    ///     Data da última modificação.
    /// </summary>
    public DateTime DtUltMod;

    /// <summary>
    ///     Usuário responsável pela última modificação.
    /// </summary>
    public string UsrUltMod;
}

/// <summary>
///     Representa um objeto de markup.
/// </summary>
internal class Markup
{
    public int Id;
    public String Nome;
    public double MargemLucro;
    public double MarkupUp;
    public double MarkupDown;

    /// <summary>
    /// Multiplicador em valor.
    /// </summary>
    public double Mult;

    /// <summary>
    /// Multiplicador em valor percentual.
    /// </summary>
    public double MultPer;

    public Boolean Ativo;
    public Boolean Visivel;
    public DateTime DtCrt;
    public String UsrCrty;
}

/// <summary>
///     Representa um objeto de estoque/inventário de ferramentas.
/// </summary>
internal class FerramentaEstoque
{
    /// <summary>
    ///     Chave primária da tabela.
    /// </summary>
    public String Id;

    /// <summary>
    ///     Chave primária da ferramenta.
    /// </summary>
    public String ToolId;

    /// <summary>
    ///     Nome da ferramenta.
    /// </summary>
    public String NomeTool;

    /// <summary>
    ///     Tipo de ferramenta (mill, turn, hole, etc).
    /// </summary>
    public String TipoTool;

    /// <summary>
    ///     Fresa de topo, etc.
    /// </summary>
    public String SubTipoTool;

    /// <summary>
    ///     mm | inch
    /// </summary>
    public String UnidadeMedida;

    /// <summary>
    ///     Tipo de gestão de vida útil
    /// </summary>
    public String TipoGestaoVidaUtil;

    /// <summary>
    ///     Tempo da vida util
    /// </summary>
    public String TempoVidaUtil;

    /// <summary>
    ///     1 - Tem validade | 0 - Não tem validade
    /// </summary>
    public String TemValidade;
    public String DataValidade;
    public String Fornecedor;
    public String CustoUnitario;

    /// <summary>
    ///     Tempo (em dias) para receber a ferramenta do fornecedor.
    /// </summary>
    public String TempoRecebimento;
    public String QuantidadeEstoque;
    public String EstoqueMinimo;
    public String ParaVenda;
    public String ParaFabricacao;
    public String ParaUsoInterno;

    /// <summary>
    ///     Disponível, sem estoque, estoque zerado.
    /// </summary>
    public String Estado;

    /// <summary>
    ///     Unidade da empresa em que se encontra o estoque.
    /// </summary>
    public String UnidadeOrganizacional;
    public String Armazem;
    public String DadosAux;
    public String DtCriacao;
    public String UsuarioCriacao;
    public String DtUltimaMod;
    public String UsuarioUltimaMod;
}

/// <summary>
///     Representa uma máquina
/// </summary>
internal class Maquina
{
    public int IdMqn { get; set; }

    public String NomeMqn { get; set; }

    public int MqnAtiva { get; set; }

    /// <summary>
    ///     Arquivo mcp/tcp
    /// </summary>
    public String ArqPos { get; set; }

    /// <summary>
    ///     Arquivo CGD
    /// </summary>
    public String ArqCgd { get; set; }

    public String CaminhoCnc { get; set; }

    /// <summary>
    ///     0 Torneamento
    ///     1 Fresamento
    ///     2 Aditiva
    /// </summary>
    public int Ambiente { get; set; }

    public String VersaoPosEc { get; set; }

    //public String Operador { get; set; }

    /// <summary>
    ///     XZ
    ///     XYZ
    ///     XZ C
    ///     XZ CY
    ///     XYZ A B C (etc)
    /// </summary>
    public String Tecnologia { get; set; }

    public Double CustoHoraMqn { get; set; }

    public Double CustoHoraHomem { get; set; }

    public String Descricao { get; set; }

    /// <summary>
    ///     Unidade organizacional que o centro de trabalho está cadastrado.
    /// </summary>
    public String Unidade { get; set; }

    public String DadosAux1 { get; set; }

    public String DadosAux2 { get; set; }

    public String DadosAux3 { get; set; }
}

/// <summary>
///     Representa um material do banco de dados do Edgecam.
/// </summary>
internal class Material
{
    /// <summary>
    ///     Id (chave primária) do material
    /// </summary>
    public String Id { get; set; }

    /// <summary>
    ///     Nome do material
    /// </summary>
    public String NomeMaterial { get; set; }

    /// <summary>
    ///     Descrição do material
    /// </summary>
    public String DescricaoMaterial { get; set; }
}

/// <summary>
///     Representa um trabalho do banco de dados do Edgecam.
/// </summary>
internal class TrabalhoEdgecam
{
    /// <summary>
    ///     Chave primária de cada trabalho
    /// </summary>
    public int IdJob { get; set; }

    /// <summary>
    ///     Descrição/nome do trabalho
    /// </summary>
    public String Descricao { get; set; }

    public String Comentario { get; set; }

    /// <summary>
    ///     Argumento agrupador
    /// </summary>
    public String Familia { get; set; }
    public String Sequencia { get; set; }
    
    /// <summary>
    ///     Job_Location no banco de dados (nome da máquina)
    /// </summary>
    public String PostoTrabalho { get; set; }

    public String Cliente { get; set; }

    /// <summary>
    ///     Programador da peça.
    /// </summary>
    public String Usuario { get; set; }
    public String Material { get; set; }

    /// <summary>
    ///     Define o estado do trabalho.
    ///     0 - Criado
    ///     1 - Iniciado
    ///     2 - Aguardando aprovação
    ///     3 - Não aprovado
    ///     4 - Aprovado (Concluído)
    ///     5 - Cancelado
    /// </summary>
    public String Status { get; set; }

    /// <summary>
    ///     Contém o caminho completo do arquivo PPF
    /// </summary>
    public String CaminhoArqPpf { get; set; }

    /// <summary>
    ///     Contém apenas o nome do arquivo PPF.
    /// </summary>
    public String NomeArqPpf 
    {
        get 
        {
            return CaminhoArqPpf.Substring(CaminhoArqPpf.LastIndexOf("\\") + 1);
        }
        set
        {
            NomeArqPpf = value;
        }
    }

    /// <summary>
    ///     Contém o caminho completo do arquivo CAD
    /// </summary>
    public String CaminhoArqCad { get; set; }

    /// <summary>
    ///     Contém apenas o nome do arquivo CAD.
    /// </summary>
    public String NomeArqCad 
    {
        get
        {
            return CaminhoArqCad.Substring(CaminhoArqCad.LastIndexOf("\\") + 1);
        }
        set
        {
            NomeArqCad = value;
        }
    }

    /// <summary>
    ///     Contém o caminho completo do arquivo CNC.
    /// </summary>
    public String CaminhoArqCnc { get; set; }

    /// <summary>
    ///     Contém o nome do arquivo CNC.
    /// </summary>
    public String NomeArqCnc
    {
        get
        {
            return CaminhoArqCnc.Substring(CaminhoArqCnc.LastIndexOf("\\") + 1);
        }
        set
        {
            NomeArqCnc = value;
        }
    }

    /// <summary>
    ///     false - Não selecionar um kit pré parametrizado
    ///     true  - Utilizar um kit existente (parametrizado)
    /// </summary>
    public String PreSelecaoKits { get; set; }

    /// <summary>
    ///     Contém a revisão do trabalho (texto)
    /// </summary>
    public String RevisaoJob { get; set; }

    /// <summary>
    ///     Tempo de usinagem do trabalho
    /// </summary>
    public String TempoDeCiclo { get; set; }

    /// <summary>
    ///     Título das notas gerais
    /// </summary>
    public String JobNotesSubject { get; set; }

    /// <summary>
    ///     Notas gerais do trabalho
    /// </summary>
    public String JobNotes { get; set; }

    /// <summary>
    ///     Arquivo da nota geral do trabalho
    /// </summary>
    public String JobNotesFile { get; set; }

    /// <summary>
    ///     Título das notas de fixações
    /// </summary>
    public String FixtureNotesSubject { get; set; }

    /// <summary>
    ///     Notas gerais da fixação
    /// </summary>
    public String FixturesNotes { get; set; }

    /// <summary>
    ///     Arquivo da nota de fixação do trabalho
    /// </summary>
    public String FixturesNotesFile { get; set; }

    /// <summary>
    ///     Título das notas dos brutos
    /// </summary>
    public String StockNotesSubject { get; set; }

    /// <summary>
    ///     Notas gerais do bruto
    /// </summary>
    public String StockNotes { get; set; }

    /// <summary>
    ///     Arquivo da nota de bruto do trabalho
    /// </summary>
    public String StockNotesFile { get; set; }

    /// <summary>
    ///     True  - Visível ao usuário
    ///     False - Invisível ao usuário.
    /// </summary>
    public String TrabalhoVisivel { get; set; }

    /// <summary>
    ///     Verifica posições de ferramentas na torre duplicadas
    ///     0 - Não verifica
    ///     1 - Verifica
    /// </summary>
    public String TurretWarning { get; set; }

    /// <summary>
    ///     Data de criação do trabalho.
    /// </summary>
    public String DtCriacao { get; set; }

    /// <summary>
    ///     Data da última modificação do trabalho.
    /// </summary>
    public String DtModificacao { get; set; }

    /// <summary>
    ///     Valor da peça para fora da placa
    /// </summary>
    public String PartStickOut { get; set; }

    /// <summary>
    ///     program id 1 do Edgecam
    /// </summary>
    public String ProgramId1 { get; set; }

    /// <summary>
    ///     program id 1 do Edgecam
    /// </summary>
    public String ProgramId2 { get; set; }

    /// <summary>
    ///     Contém o valor do trabalho pai desse trabalho.
    /// </summary>
    public String IdJobParente { get; set; }
}

/// <summary>
///     Representa um cliente no banco de dados do Edgecam Manager.
/// </summary>
internal class Cliente
{
    /// <summary>
    ///     Chave primária da tabela.
    /// </summary>
    public String Id { get; set; }
    /// <summary>
    ///     Nome da empresa do cliente (deve ser único)
    /// </summary>
    public String NomeEmpresa { get; set; }
    public String Setor { get; set; }
    public String Endereco { get; set; }
    public String Cep { get; set; }
    public String Cidade { get; set; }
    public String Estado { get; set; }
    public String Pais { get; set; }
    public String Cnpj { get; set; }
    /// <summary>
    ///     Apelido do cliente.
    /// </summary>
    public String NomeInterno { get; set; }
    public String Telefone { get; set; }
    public String Email { get; set; }
    /// <summary>
    ///     0 - Não
    ///     1 - Sim
    /// </summary>
    public Boolean IsCliente { get; set; }
    /// <summary>
    ///     0 - Não
    ///     1 - Sim
    /// </summary>
    public Boolean IsFornecedor { get; set; }
    /// <summary>
    ///     0 - Não
    ///     1 - Sim
    /// </summary>
    public Boolean IsDistribuidor { get; set; }
    /// <summary>
    ///     Data de criação do registro do cliente
    /// </summary>
    public DateTime DtCriacao { get; set; }
    /// <summary>
    ///     Data da última modificação do cliente
    /// </summary>
    public DateTime DtUltimaMod { get; set; }
    /// <summary>
    ///     Usuário responsável pela última modificação.
    /// </summary>
    public String UsuarioUltimaMod { get; set; }
}

/// <summary>
///     Representa uma peça no banco de dados do Edgecam Manager.
/// </summary>
internal class Artigo
{
    /// <summary>
    ///     Chave primária de cada artigo/item/peça/montagem/conjunto
    /// </summary>
    public int id { get; set; }
    public String NomePeca { get; set; }
    public String CaminhoPeca { get; set; }
    public String Desenho { get; set; }
    public String Revisao { get; set; }
    public String Comprimento { get; set; }
    public String Largura { get; set; }
    public String Diametro { get; set; }
    public String Peso { get; set; }
    public String Material { get; set; }
    public String CentroTrabalho { get; set; }
    /// <summary>
    ///     Ambiente, onde:
    ///     1 – Fresamento
    ///     2 – Torneamento
    ///     3 – Aditiva
    ///     4 – Wire (Erosão a fio)
    /// </summary>
    public int Ambiente { get; set; }
    /// <summary>
    ///     0 – Não é parte de um conjunto
    ///     1 – É um conjunto
    /// </summary>
    public Boolean IsConjunto { get; set; }
    /// <summary>
    ///     Esse valor será preenchido com 1 quando o item for filho de alguém.
    ///     0 – Não é pai (filho)
    ///     1 – É pai
    /// </summary>
    public Boolean IsPai { get; set; }
    /// <summary>
    ///     Nome do pai. Caso esteja vazio, significa que ele mesmo é o pai
    /// </summary>
    public String NomePai { get; set; }
    /// <summary>
    ///     Nível do componente na estrutura do produto.
    /// </summary>
    public String Nivel { get; set; }
    /// <summary>
    ///     0 – Inativo
    ///     1 – Ativo
    /// </summary>
    public Boolean Ativo { get; set; }
    /// <summary>
    ///     1 – Fabricação
    ///     2 – Venda
    ///     3 – Estoque
    ///     4 – Terceirização
    /// </summary>
    public int Tipo { get; set; }
    /// <summary>
    ///     0 – Não tem preço
    ///     1 – Tem preço fixo
    /// </summary>
    public Boolean TemPrecoFixo { get; set; }
    /// <summary>
    ///     Valor do preço fixo do conjunto/peça
    /// </summary>
    public String ValorPrecoFixo { get; set; }
    public String Descricao { get; set; }
    public String DA1 { get; set; }
    public String DA2 { get; set; }
    public String DA3 { get; set; }
    public String DA4 { get; set; }
    public String DA5 { get; set; }
    public String DA6 { get; set; }
    public String DA7 { get; set; }
    public String DA8 { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime DtUltimaMod { get; set; }
    public String DtValidade { get; set; }
    public String UsuarioUltimaMod { get; set; }
}

/// <summary>
///     Representa uma referência automática cadastrada no banco de dados
/// do Edgecam Manager.
/// </summary>
internal class ReferenciaAutomatica
{
    public int Id { get; set; }
    public String NomeTabela { get; set; }
    public String NomeColuna { get; set; }
    public String Prefixo { get; set; }
    public int Incremento { get; set; }
    public int ValorInicial { get; set; }
    public int ValorFinal { get; set; }
    public int ValorContadorAtual { get; set; }
    public Boolean InserirZeros { get; set; }
    public Boolean ZerosAEsquerda { get; set; }
    public Boolean ZerosADireita { get; set; }
    public int NumZerosInserir { get; set; }
    public DateTime DtUltMod { get; set; }
}

/// <summary>
///     Representa uma família do Edgecam (da tabela TS_JOB)
/// </summary>
internal class Familia
{
    public String NomeFamilia { get; set; }
}

/// <summary>
///     Representa uma campo para importação.
/// </summary>
internal class CampoImportar
{
    public String Id;
    public String NomeCampo;
    public String NomeImportar;
    public Boolean Ativo;
    public Boolean AceitaNulos;
    public String ValorPadrao;
}

/// <summary>
///     Representa uma campo para exportação.
/// </summary>
internal class CampoExportar
{
    public String Id;
    public String NomeCampo;
    public String NomeImportar;
    public Boolean Ativo;
    public Boolean AceitaNulos;
    public String ValorPadrao;
}

/// <summary>
///     Representa um método de pagamento utilizado nos orçamentos.
/// </summary>
internal class MetodoPagamento
{
    /// <summary>
    ///     Contém o identificado do registro.
    /// </summary>
    public String Id;
    /// <summary>
    ///     Nome do método de pagamento.
    /// </summary>
    public String Nome;
    /// <summary>
    ///     Se está ou não ativo.
    /// </summary>
    public String Ativo;
    /// <summary>
    ///     Disponível para todos os orçamentos
    /// </summary>
    public String DisponivelTodosOrcamentos;
    /// <summary>
    ///     0 = Disponível para todos.
    /// </summary>
    public String TipoOrcamentoDisponivel;
}

/// <summary>
///     Representa um método de pagamento utilizado nos orçamentos.
/// </summary>
internal class TipoOrcamento
{
    /// <summary>
    ///     Contém o identificado do registro.
    /// </summary>
    public String Id;
    /// <summary>
    ///     Nome do método de pagamento.
    /// </summary>
    public String Nome;
    /// <summary>
    ///     Descrição.
    /// </summary>
    public String Descricao;
    /// <summary>
    ///     Tipo de categoria..
    /// </summary>
    public String Categoria;
    /// <summary>
    ///     Se está ou não ativo.
    /// </summary>
    public Boolean Ativo;
    /// <summary>
    ///     Disponível para todos os orçamentos
    /// </summary>
    public DateTime DtCrt;
    /// <summary>
    ///     0 = Disponível para todos.
    /// </summary>
    public String UsrCrt;
}

/// <summary>
///     Representa uma unidade organizacional.
/// </summary>
internal class UnidadeOrganizacional
{
    /// <summary>
    ///     Indentificador único da tabela.
    /// </summary>
    public int id;
    /// <summary>
    ///     Nome da unidade
    /// </summary>
    public String Unidade;
    public String Cidade;
    public String EstadoUF;
    public String Pais;
    /// <summary>
    ///     1 = Ativo, 0 = Inativo
    /// </summary>
    public Boolean Ativo;
    /// <summary>
    ///     Data de criação
    /// </summary>
    public DateTime DtCriacao;
    /// <summary>
    ///     Usuário que criou
    /// </summary>
    public String UsuarioCrt;
    /// <summary>
    ///     Data de criação.
    /// </summary>
    public DateTime DtCrt;
    /// <summary>
    ///     Usuário que fez a última modificação.
    /// </summary>
    public String UsuarioUltMod;
    /// <summary>
    ///     Data de última modificação.
    /// </summary>
    public DateTime DtUltMod;
}

/// <summary>
///     Representa um contato de algum cliente.
/// </summary>
internal class ContatoCliente
{
    public String id;
    public String Nome;
    public String Sobrenome;
    public String Setor;
    public String Cliente;
    public String Cidade;
    public String Estado;
    public String Pais;
}

/// <summary>
///     Representa uma cidade, estado ou país.
/// </summary>
internal class Cidade
{
    public String id;
    public String NomeCidade;
    public String Estado;
    public String Pais;
    public String UF;
}

internal class Produto
{
    public String id;
    public String Nome;
    public Double CustoUnitario;
    public Double CustoTotal;
    public Double DescontoPadrao;
    public int Quantidade;
    public int Tipo;
    public String Observacao;
    /// <summary>
    ///     1 = Ativo, 0 = Inativo
    /// </summary>
    public Boolean Ativo;
    /// <summary>
    ///     Usuário que criou
    /// </summary>
    public String UsuarioCrt;
    /// <summary>
    ///     Data de criação
    /// </summary>
    public DateTime DtCriacao;
    /// <summary>
    ///     Usuário que fez a última modificação.
    /// </summary>
    public String UsuarioUltMod;
    /// <summary>
    ///     Data de última modificação.
    /// </summary>
    public DateTime DtUltMod;
}

#endregion

#region Listas de objetos

/// <summary>
///     Representa uma lista de máquinas
/// </summary>
internal class ListaMachines : BindingList<Maquina>
{
    /// <summary>
    ///     Propriedade que possuí o id exclusivo de uma determinada máquina.
    /// </summary>
    public int _idMqn { get; set; }

    /// <summary>
    ///     Popula um objeto 'BindingList<T>'
    /// </summary>
    /// <param name="NomeMqn">Nome da máquina</param>
    /// <param name="Ambiente">
    ///                         0 Torneamento
    ///                         1 Fresamento
    ///                         2 Aditivas
    /// </param>
    public ListaMachines(String NomeMqn, int Ambiente)
    {
        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(CriaFiltroSql(NomeMqn, Ambiente));
        CarregaDt(dt);
    }

    /// <summary>
    ///     Consulta os dados de uma máquina com base em um nome da máquina.
    /// </summary>
    /// <param name="NomeMqn"></param>
    public ListaMachines(String NomeMqn)
    {
        Dictionary<String, object> dic = new Dictionary<string, object>();
        dic.Add("@MqnName", NomeMqn);

        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ID_MAQUINA, dic);
        CarregaIdExclusivo(dt);
    }

    /// <summary>
    ///     Consulta todas as máquinas cadastradas no banco intermediário sem
    /// clausulas (parâmetros de busca).
    /// </summary>
    public ListaMachines()
    {
        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_MAQUINAS);
        CarregaDt(dt);
    }

    public String CriaFiltroSql(String NomeMqn, int Ambiente)
    {
        String strRet = Consultas_EcMgr.CONSULTA_MAQUINAS;

        if (NomeMqn != "") strRet += string.Format(" and NomeMaquina like '%{0}%'", NomeMqn);

        if (Ambiente != -1) strRet += string.Format(" and Ambiente = '{0}'", Ambiente);

        return strRet;
    }

    private void CarregaDt(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            var q = dt.AsEnumerable().Select(r => new Maquina
            {
                IdMqn = Convert.ToInt16(r["id"]),
                NomeMqn = r["Centro de trabalho"].ToString(),
                MqnAtiva = Convert.ToInt16(r["MqnAtiva_Db"]),
                ArqPos = r["CaminhoPosCompilado"].ToString(),
                ArqCgd = r["CaminhoPosCgd"].ToString(),
                CaminhoCnc = r["CaminhoCnc"].ToString(),
                Ambiente = Convert.ToInt16(r["Ambiente_Db"]),
                VersaoPosEc = r["VersaoEcPos"].ToString(),
                //Operador = r["OperadorMqn"].ToString(),
                Tecnologia = r["Tecnologia"].ToString(),
                CustoHoraMqn = Convert.ToDouble(r["CustoHoraMqn"]),
                CustoHoraHomem = Convert.ToDouble(r["CustoHoraHomem"]),
                Descricao = r["Descricao"].ToString(),
                Unidade = r["Unidade"].ToString(),
                DadosAux1 = r["DadosAux1"].ToString(),
                DadosAux2 = r["DadosAux2"].ToString(),
                DadosAux3 = r["DadosAux3"].ToString()
            });

            for (int x = 0; x < q.Count(); x++)
            {
                Add(q.ToArray()[x]);
            }
        }
    }

    /// <summary>
    ///     Carrega o ID exclusivo de uma máquina na propriedade para ser acessada externamente.
    /// </summary>
    /// <param name="dt"></param>
    private void CarregaIdExclusivo(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            _idMqn = dt.AsEnumerable().Select(r => Convert.ToInt16(r["id"].ToString())).FirstOrDefault();
        }
    }
}

/// <summary>
///     Representa uma lista de materiais do Edgecam.
/// </summary>
internal class ListaMaterais : BindingList<Material>
{
    public ListaMaterais()
    {
        DataTable dt = Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_MATERIAIS);
        CarregaDt(dt);
    }

    private void CarregaDt(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            var q = dt.AsEnumerable().Select(r => new Material
            {
                Id                  = r["MAT_MATERIAL_ID"].ToString(),
                NomeMaterial        = r["MAT_MATERIAL_DESCRIPTION"].ToString(),
                DescricaoMaterial   = r["MAT_COMMENT"].ToString(),
            });

            for (int x = 0; x < q.Count(); x++)
            {
                Add(q.ToArray()[x]);
            }
        }
    }
}

/// <summary>
///     Representa uma lista de clientes do Edgecam Manager.
/// </summary>
internal class ListaClientes : BindingList<Cliente>
{
    public ListaClientes()
    {
        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_CLIENTES);
        CarregaDt(dt);
    }

    public void CarregaDt(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            var q = dt.AsEnumerable().Select(r => new Cliente
            {
                Id               = r["id"].ToString(),
                NomeEmpresa      = r["NomeEmpresa"].ToString(),
                Setor            = r["Setor"].ToString(),
                Endereco         = r["Endereco"].ToString(),
                Cep              = r["Cep"].ToString(),
                Cidade           = r["Cidade"].ToString(),
                Estado           = r["Estado"].ToString(),
                Pais             = r["Pais"].ToString(),
                Cnpj             = r["CNPJ"].ToString(),
                NomeInterno      = r["NomeInterno"].ToString(),
                Telefone         = r["Telefone"].ToString(),
                Email            = r["Email"].ToString(),
                IsCliente        = Convert.ToBoolean(r["IsCliente"].ToString()),
                IsFornecedor     = Convert.ToBoolean(r["IsFornecedor"].ToString()),
                IsDistribuidor   = Convert.ToBoolean(r["IsDistribuidor"].ToString()),
                DtCriacao        = Convert.ToDateTime(r["DtCriacao"].ToString()),
                DtUltimaMod      = Convert.ToDateTime(r["DtUltimaMod"].ToString()),
                UsuarioUltimaMod = r["UsuarioUltimaMod"].ToString()
            });

            for (int x = 0; x < q.Count(); x++)
            {
                Add(q.ToArray()[x]);
            }
        }
    }
}

/// <summary>
///     Representa uma lista de peças
/// </summary>
internal class ListaArtigos : BindingList<Artigo>
{
    public ListaArtigos()
    {
        DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ARTIGOS);
        CarregaDt(dt);
    }

    private void CarregaDt(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            var q = dt.AsEnumerable().Select(r => new Artigo 
            {
                id = Convert.ToInt32(r["id"]),
                NomePeca = r["NomePeca"].ToString(),
                CaminhoPeca = r["CaminhoPeca"].ToString(),
                Desenho = r["Desenho"].ToString(),
                Revisao = r["Revisao"].ToString(),
                Comprimento = r["Comprimento"].ToString(),
                Largura = r["Largura"].ToString(),
                Diametro = r["Diametro"].ToString(),
                Peso = r["Peso"].ToString(),
                Material = r["Material"].ToString(),
                CentroTrabalho = r["CentroTrabalho"].ToString(),
                Ambiente = Convert.ToInt32(r["Ambiente"]),
                IsConjunto = Convert.ToBoolean(r["EConjunto"]),
                IsPai = Convert.ToBoolean(r["EPai"]),
                NomePai = r["NomeDoPai"].ToString(),
                Nivel = r["Nivel"].ToString(),
                Ativo = Convert.ToBoolean(r["Ativo"]),
                Tipo = Convert.ToInt32(r["Tipo"]),
                TemPrecoFixo = Convert.ToBoolean(r["TemPrecoFixo"]),
                ValorPrecoFixo = r["ValorPrecoFixo"].ToString(),
                Descricao = r["Descrição"].ToString(),
                DA1 = r["DadosAux1"].ToString(),
                DA2 = r["DadosAux2"].ToString(),
                DA3 = r["DadosAux3"].ToString(),
                DA4 = r["DadosAux4"].ToString(),
                DA5 = r["DadosAux5"].ToString(),
                DA6 = r["DadosAux6"].ToString(),
                DA7 = r["DadosAux7"].ToString(),
                DA8 = r["DadosAux8"].ToString(),
                DtCriacao = Convert.ToDateTime(r["DtCriacao"]),
                DtUltimaMod = Convert.ToDateTime(r["DtUltimaMod"]),
                DtValidade = r["DtValidade"].ToString(),
                UsuarioUltimaMod = r["UsuarioUltimaMod"].ToString()
            });

            for (int x = 0; x < q.Count(); x++)
            {
                Add(q.ToArray()[x]);
            }
        }
    }
}

/// <summary>
///     Representa uma lista de famílias.
/// </summary>
internal class ListaFamilia : BindingList<Familia>
{
    /// <summary>
    ///     Instancia o objeto da classe e em conjunto consulta as familías do Edgecam.
    /// </summary>
    public ListaFamilia()
    {
        CarregaDt(Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CONSULTA_FAMILIAS));
    }

    private void CarregaDt(DataTable Dt)
    {
        if (Dt.Rows.Count > 0)
        {
            var q = Dt.AsEnumerable().Select(r => new Familia { NomeFamilia = r["JOB_FAMILY"].ToString() });

            for (int x = 0; x < q.Count(); x++)
            {
                Add(q.ToArray()[x]);
            }
        }
    }
}

/// <summary>
///     Representa uma lista de métodos de pagamento para orçamentos.
/// </summary>
internal class ListaMetodosPay : BindingList<MetodoPagamento>
{
    /// <summary>
    ///     Instancia o objeto da classe e em conjunto consulta as familías do Edgecam.
    /// </summary>
    public ListaMetodosPay()
    {
        CarregaDt(Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TIPOS_PAGAMENTO));
    }

    private void CarregaDt(DataTable Dt)
    {
        if (Dt.Rows.Count > 0)
        {
            var q = Dt.AsEnumerable().Select(r => new MetodoPagamento 
            {
                Id = r["id"].ToString(),
                Nome = r["NomePagamento"].ToString(),
                Ativo = r["Ativo"].ToString(),
                DisponivelTodosOrcamentos = r["DisponivelTodosOrcamentos"].ToString(),
                TipoOrcamentoDisponivel = r["TipoOrcamento"].ToString()
            });

            for (int x = 0; x < q.Count(); x++) this.Add(q.ToArray()[x]);
        }
    }
}

internal class ListQuotesType : BindingList<TipoOrcamento>
{
    /// <summary>
    ///     Instancia o objeto da classe e em conjunto consulta as familías do Edgecam.
    /// </summary>
    public ListQuotesType()
    {
        this.CarregaDt(Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TIPOS_ORCAMENTOS));
    }

    private void CarregaDt(DataTable Dt)
    {
        if (Dt.Rows.Count > 0)
        {
            var q = Dt.AsEnumerable().Select(r => new TipoOrcamento
            {
                Id = r["id"].ToString(),
                Nome = r["Nome"].ToString(),
                Descricao = r["Descricao"].ToString(),
                Categoria = r["Categoria"].ToString(),
                Ativo = Convert.ToBoolean(r["Ativo"].ToString()),
                DtCrt = Convert.ToDateTime(r["DtCriacao"].ToString()),
                UsrCrt = r["UsuarioResp"].ToString()
            });

            for (int x = 0; x < q.Count(); x++) this.Add(q.ToArray()[x]);
        }
    }
}

/// <summary>
///     Representa uma lista das unidades organizacionais.
/// </summary>
internal class ListaUnidades : BindingList<UnidadeOrganizacional>
{
    /// <summary>
    ///     Consulta a lista de unidades organizacionais.
    /// </summary>
    /// <param name="ConsultarTudo">True para consultar tudo (inclusive as inativas), false para consultar apenas os nomes.</param>
    public ListaUnidades(Boolean ConsultarTudo = false)
    {
        if (!ConsultarTudo)
            CarregaDt(Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_UNIDADES_ORGANIZACIONAIS), ConsultarTudo);
        else CarregaDt(Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONTULTA_TODAS_UNIDADES_ORGANIZACIONAIS), ConsultarTudo);
    }

    private void CarregaDt(DataTable Dt, Boolean CarregarTudo = false)
    {
        if (Dt.Rows.Count > 0)
        {
            //  Se for true esse parâmetro, significa que o sistema vai
            //carregar todas as propriedades das unidades organizacionais,
            //não apenas os nomes.
            if (CarregarTudo)
            {
                var q = Dt.AsEnumerable().Select(r => new UnidadeOrganizacional
                {
                    id = Convert.ToInt16(r["id"].ToString()),
                    Unidade = r["Unidade"].ToString(),
                    Cidade = r["Cidade"].ToString(),
                    EstadoUF = r["UF"].ToString(),
                    Pais = r["Pais"].ToString(),
                    Ativo = Convert.ToBoolean(r["Ativo"].ToString()),
                    DtCriacao = Convert.ToDateTime(r["DtCriacao"].ToString()),
                    UsuarioCrt = r["UsuarioCriacao"].ToString(),
                    DtUltMod = Convert.ToDateTime(r["DtUltimaMod"].ToString()),
                    UsuarioUltMod = r["UsuarioUltimaMod"].ToString()
                });

                for (int x = 0; x < q.Count(); x++) this.Add(q.ToArray()[x]);
            }
            else
            {
                var q = Dt.AsEnumerable().Select(r => new UnidadeOrganizacional { Unidade = r["Unidade"].ToString() });
                for (int x = 0; x < q.Count(); x++) this.Add(q.ToArray()[x]);
            }            
        }
    }
}

#endregion



/// <summary>
///     Representa um arquivo CNC gerado pelo Edgecam.
/// </summary>
internal class Cnc
{
    /// <summary>
    ///     Nome/Descrição do trabalho
    /// </summary>
    public String NomeTrabalho { get; set; }

    /// <summary>
    ///     Caminho do arquivo CNC
    /// </summary>
    public String CaminhoCnc { get; set; }

    /// <summary>
    ///     Última data de modificação do trabalho, isso quer dizer,
    /// a última data que foi gerado um CNC (geralmente é isso).
    /// </summary>
    public Nullable<DateTime> DtModificacao { get; set; }
}

/// <summary>
///     Representa uma lista de códigos CNC's do Edgecam.
/// </summary>
internal class ListaCncs : BindingList<Cnc>
{
    public ListaCncs(String Job, String Cnc, String Data)
    {
        Sql sql = new Sql(Objects.CfgAtual._EcStringConnectionSql);
        DataTable dt = sql.ExecutaSql(CriaFiltroSql(Job, Cnc, Data));
        CarregaDt(dt);
    }

    private String CriaFiltroSql(String Job, String Cnc, String Dt)
    {
        String strRet = Consultas_Ec.CONSULTA_CNCS;

        if (Job != "") strRet += String.Format(" AND T1.JOB_JOB_DESCRIPTION LIKE %{0}%", Job);
        if (Cnc != "") strRet += String.Format(" AND REVERSE(SUBSTRING(REVERSE(T1.JOB_NC_FILE ), 1, CHARINDEX('\', REVERSE(T1.JOB_NC_FILE ), 1) -1)) like '%{0}%'", Cnc);
        if (Dt != "")  strRet += String.Format(" AND T1.JOB_MODIFIED = '{0}'", Dt);

        return strRet;
    }

    private void CarregaDt(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            var q = dt.AsEnumerable().Select(r => new Cnc
            {
                NomeTrabalho    = r["JOB_JOB_DESCRIPTION"].ToString(),
                CaminhoCnc      = r["JOB_NC_FILE"].ToString(),
                DtModificacao   = Convert.ToDateTime(r["JOB_MODIFIED"].ToString())
            });

            foreach (Cnc c in q)
            {
                Add(c);
            }
        }
    }
}