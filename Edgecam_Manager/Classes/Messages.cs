using System;
using System.Windows.Forms;

using Edgecam_Manager.Idiomas;
using System.Collections.Generic;

/// <summary>
///     Classe com as mensagens do sistema, de maneira estática.
/// </summary>
internal class Messages
{

    #region Variáveis globais

    private static String mIdioma;

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o idioma definido pelo usuário.
    /// </summary>
    public String _Idioma
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

    #endregion

    #region Dicionário PT-BR

    /// <summary>
    ///     Dicionário que contém messagens da interface em português.
    /// </summary>
    public Dictionary<int, String> InterfaceMessagesPTBR = new Dictionary<int, string>()
    {
        [0] = "",
        [1] = "",
        [2] = "",
        [3] = "",
        [4] = "",
        [5] = ""
    };

    #endregion

    #region Instância dos objetos

    /// <summary>
    ///     Instância o objeto da classe, passando como parâmetro o idioma das mensagens à serem exibidas
    /// na interface para o usuário.
    /// </summary>
    /// <param name="Idioma"></param>
    public Messages(String Idioma)
    {
        if (String.IsNullOrEmpty(Idioma))
        {
            _Idioma = "EN-US";
        }
        else if (Idioma.ToUpper() != "PT-BR" && Idioma.ToUpper() != "EN-US" && Idioma.ToUpper() != "ES-ES" && Idioma.ToUpper() != "FR-FR")
        {
            _Idioma = "EN-US";
        }
        else _Idioma = Idioma;
    }

    #endregion

    #region Mensagens - Formulário 'FrmLogin'

    /// <summary>
    ///     Mostra uma mensagem informando que não foi possível de encontrar um arquivo de configuração.
    /// </summary>
    public static void Msg001()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show(en_US.FrmLogin_Arquivo_config_nao_existe, en_US.FrmLogin_Configuracao_nao_localizada, MessageBoxButtons.OK, MessageBoxIcon.Error);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmLogin_Arquivo_config_nao_existe, pt_BR.FrmLogin_Configuracao_nao_localizada, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    #endregion

    #region Mensagens - Formulário 'FrmLogin_NewPass'

    /// <summary>
    ///     Apresenta uma mensagem para o usuário informando que a senha foi trocada com êxito.
    /// </summary>
    public static void Msg002()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmLogin_NewPass_SenhaAtualizada, pt_BR.FrmLogin_NewPass_Sucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion

    #region Mensagens - Formulário 'FrmLogin_UserCnns'

    /// <summary>
    ///     Mensagem que informa que foi criado um arquivo de configuração XML com êxito.
    /// </summary>
    public static void Msg003()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show(en_US.FrmUserConfigCnns_SalvouCfg, en_US.FrmUserConfigCnns_Concluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmUser_ConfigCnns_SalvouCfg, pt_BR.FrmUser_ConfigCnns_Concluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Mensagem que informa o usuário que houve um erro ao tentar salvar a configuração.
    /// </summary>
    public static void Msg004()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show(en_US.FrmUserConfigCnns_ErroSalvarCfg, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmUser_ConfigCnns_ErroSalvarCfg, pt_BR.FrmUser_ConfigCnns_Erro, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Mensagem que informa que o teste de conexão foi estabelecido com êxito.
    /// </summary>
    public static void Msg005()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show(en_US.FrmUserConfigCnns_CnnOk, en_US.FrmUserConfigCnns_Concluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmUser_ConfigCnns_CnnOk, pt_BR.FrmUser_ConfigCnns_Concluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
          
    /// <summary>
    ///     Mensagem que informa o usuário de que não foi possível se conectar ao banco de dados.
    /// </summary>
    public static void Msg006()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show(en_US.FrmUserConfigCnns_CnnNotOk, en_US.FrmUserConfigCnns_NaoConcluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmUser_ConfigCnns_CnnNotOk, pt_BR.FrmUser_ConfigCnns_NaoConcluido, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }          

    #endregion

    #region Mensagens - Formulário 'FrmTarefas'

    /// <summary>
    ///     Informa que não é possível editar multiplas tarefas ao mesmo tempo.
    /// </summary>
    public static void Msg007()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_EditarMultiplasTarefas, pt_BR.FrmTarefas_OperacaoAbortada, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Informa que não é possível visualizar multiplas tarefas ao mesmo tempo.
    /// </summary>
    public static void Msg008()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_ViewMultiplasTarefas, pt_BR.FrmTarefas_OperacaoAbortada, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Informa que a tarefa foi concluída com êxito.
    /// </summary>
    public static void Msg009()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_TarefaConcluida, pt_BR.FrmTarefas_AvisoTarefaConcluida, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Pergunta se o usuário realmente deseja deletar a tarefa selecionada.
    /// </summary>
    public static DialogResult Msg010()
    {
        if (mIdioma.ToUpper() == "EN-US")
            return MessageBox.Show("", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            return MessageBox.Show(pt_BR.FrmTarefas_TarefaDeletada, pt_BR.FrmTarefas_AvisoTarefaDeletada, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        return DialogResult.Cancel;
    }

    #endregion

    #region Mensagens - Formulário 'FrmTarefas_New'

    /// <summary>
    ///     Informa que atualizou a tarefa com êxito.
    /// </summary>
    public static void Msg011()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_New_AtualizaTarefa, pt_BR.FrmTarefas_New_Concluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Informa que foi salvo a tarefa com êxito  .
    /// </summary>
    public static void Msg012()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_New_SalvaNovaTarefa, pt_BR.FrmTarefas_New_Concluido, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Informa que alguns campos não foram preenchidos adequadamente.
    /// </summary>
    public static void Msg013()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_New_CamposNaoPreen, pt_BR.FrmTarefas_New_AvisoCamposNaoPreen, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Informa que um determinado usuário já foi selecionado para criação de uma tarefa.
    /// </summary>
    public static void Msg014()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmTarefas_New_UsrJaAdd, pt_BR.FrmTarefas_New_AvisoUsrJaAdd, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    #endregion

    #region Mensagens - Formulário 'FrmOrdens'

    /// <summary>
    ///     Informa que não é possível editar múltiplas mensagens.
    /// </summary>
    public static void Msg015()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmOrdens_EditarMultiplasOrdens, pt_BR.FrmOrdens_OperacaoAbortada, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    #endregion

    #region Mensagens - Formulário 'FrmOrdens_New'

    /// <summary>
    ///     Informa que o cadastro de uma nova ordem de produção foi executada com êxito.
    /// </summary>
    public static void Msg016()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmOrdens_New_OrdemCriada, pt_BR.FrmOrdens_New_AvisoOrdemCriada, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Informa que o usuário não preencheu todos os campos devidamente para criação de ordens de produção.
    /// </summary>
    public static void Msg017()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmOrdens_New_VerificaCampos, pt_BR.FrmOrdens_New_AvisoVerificaCampos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    #endregion

    #region Mensagens - Formulário 'FrmConfig_RefAuto'

    /// <summary>
    ///     Informa que foi salvo com êxito a referência automática.
    /// </summary>
    public static void Msg068()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmConfig_RefAuto_ReferenciaCriada, pt_BR.FrmConfig_RefAuto_AvisoReferenciaCriada, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Informa que foi atualizado com êxito a referência automática.
    /// </summary>
    public static void Msg069()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmConfig_RefAuto_ReferenciaAtualizada, pt_BR.FrmConfig_RefAuto_AvisoReferenciaAtualizada, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Informa que nem todos os campos foram preenchidos corretamente.
    /// </summary>
    public static void Msg070()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show(pt_BR.FrmConfig_RefAuto_VerificaCampos, pt_BR.FrmConfig_RefAuto_AvisoVerificaCampos, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public static void Msg9999()
    {
        if (mIdioma.ToUpper() == "EN-US")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }


    /// <summary>
    ///     
    /// </summary>
    public static DialogResult Msg10000()
    {
        if (mIdioma.ToUpper() == "EN-US")
            return MessageBox.Show("", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        if (mIdioma.ToUpper() == "PT-BR")
            return MessageBox.Show(pt_BR.FrmTarefas_TarefaDeletada, pt_BR.FrmTarefas_AvisoTarefaDeletada, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        return DialogResult.Cancel;
    }

}