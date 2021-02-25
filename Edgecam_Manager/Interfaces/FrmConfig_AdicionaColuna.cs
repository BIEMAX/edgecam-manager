using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    internal partial class FrmConfig_AdicionaColuna : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     Contém o nome da coluna definido pelo usuário.
        /// </summary>
        private String mNomeColuna = "";

        /// <summary>
        ///     Contém a forma de como será salvo a coluna, se é
        /// uma nova coluna ou uma existente.
        /// </summary>
        private e_SkaFormaAbrir mFormaSalvamento;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém o nome da coluna personalizada que o usuário deseja cadastrar. 
        /// </summary>
        public String _NomeColunaPersonalizado
        {
            get
            {
                return mNomeColuna;
            }
        }

        #endregion

        #region Enumeradores

        /// <summary>
        ///     Enumerador que contém a instrução de como será o salvamento
        /// desta interface (insert or update).
        /// </summary>
        private enum e_SkaFormaAbrir
        {
            Novo,
            Edicao
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto que permite cadastrar uma nova coluna em uma determinada tabela
        /// do banco de dados do Edgecam Manager.
        /// </summary>
        /// <param name="NomeTabela">Nome da tabela que será adicionado uma nova coluna.</param>
        public FrmConfig_AdicionaColuna(String NomeTabela)
        {
            InitializeComponent();

            txtNomeTabela.Text = NomeTabela;
            cbTipos.SelectedIndex = 0;

            mFormaSalvamento = e_SkaFormaAbrir.Novo;
        }

        /// <summary>
        ///     Instância o objeto que permite cadastrar uma nova coluna em uma determinada tabela
        /// do banco de dados do Edgecam Manager.
        /// </summary>
        /// <param name="NomeTabela">Nome da tabela que será adicionado uma nova coluna.</param>
        /// <param name="NomeColuna">Nome da coluna</param>
        /// <param name="TipoDado">Tipo de dado</param>
        /// <param name="ValorPadrao">Valor padrão da coluna</param>
        public FrmConfig_AdicionaColuna(String NomeTabela, String NomeColuna, String TipoDado, String ValorPadrao = "")
        {
            InitializeComponent();

            txtNomeTabela.Text = NomeTabela;

            mNomeColuna = NomeColuna.Replace("USR_", "");
            txtNomeColuna.Text = mNomeColuna;

            cbTipos.SelectedIndex = DefineTipoDadoColuna(TipoDado);
            txtValorPadrao.Text = ValorPadrao;

            txtNomeTabela.Enabled = false;
            txtValorPadrao.Enabled = false;

            mFormaSalvamento = e_SkaFormaAbrir.Edicao;
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que limpa a seleção de todos os controles da interface (background color = yellow e o errorProvider)
        /// e seta a seleção em um único controle na interface, destacando para o usuário.
        /// </summary>
        /// <param name="Ctrl">Controle que deverá ser setado como conrtole ativo.</param>
        private void SetaSelecaoCaixa(Control Ctrl)
        {
            alert.Clear();
            Ctrl.BackColor = Color.Yellow;
            alert.SetError(Ctrl, "Insira um dado válido");
        }

        /// <summary>
        ///     Salva a coluna personalizada pelo usuário.
        /// </summary>
        private void SalvaColuna()
        {
            if (ValidaAntesSalvar() && mFormaSalvamento == e_SkaFormaAbrir.Novo)
            {
                Objects.CnnBancoEcMgr.ExecutaSql(MontaConsultaSql_NovaColuna());

                MessageBox.Show("Adição da coluna concluído com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ValidaAntesSalvar() && mFormaSalvamento == e_SkaFormaAbrir.Edicao)
            {
                //Precisa remover o 'CONSTRAINT' da coluna, para depois renomear ela e atualizar os dados
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_CONSTRAINT_COLUNA.Replace("@TABELA", txtNomeTabela.Text).Replace("@COLUNA", mNomeColuna));

                //Aqui é renomeado a coluna para o novo nome (caso tenha acontecido)
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.RENOMEIA_COLUNA.Replace("@TABELA", txtNomeTabela.Text).Replace("@COLUNA", String.Format("USR_{0}", mNomeColuna)).Replace("@NOVA_COLUNA", String.Format("USR_{0}", string.Join("", txtNomeColuna.Text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)))));

                //Aqui é alterado o tipo de dado da coluna
                mNomeColuna = String.Format("USR_{0}", string.Join("", txtNomeColuna.Text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)));
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_COLUNA.Replace("@TABELA", txtNomeTabela.Text).Replace("@COLUNA", mNomeColuna).Replace("@TIPO", TipoDadoColuna()));

                MessageBox.Show("Atualização da coluna concluído com êxito", "Êxito ao atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///     Método booleano que valida os dados e verifica se a coluna já não existe no banco
        /// de dados do Edgecam Manager para prosseguir com a alteração na tabela.
        /// </summary>
        /// <returns>'True' significa que está tudo preenchido corretamente e a 
        /// coluna não existe ainda.</returns>
        private Boolean ValidaAntesSalvar()
        {
            try
            {
                if (!String.IsNullOrEmpty(txtNomeColuna.Text))
                    if (cbTipos.SelectedIndex > 0)
                        if (Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_COLUNA, new Dictionary<string, object>() { { "@TABELA", txtNomeTabela.Text }, { "@COLUNA", txtNomeColuna.Text } }).Rows.Count == 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Insira um nome diferente para coluna", "Coluna já existe no banco de dados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                //Se chegar aqui, significa que algum dado não foi preenchido.
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Com base na seleção do usuário, o mesmo define o tipo de dado que
        /// será criado a coluna no banco de dados do Edgecam Manager.
        /// </summary>
        /// <returns></returns>
        private String TipoDadoColuna()
        {
            switch (cbTipos.SelectedIndex)
            {
                case 1: return "int";
                case 2: return "nvarchar(max)";
                case 3: return "char(1)";
                case 4: return "date";
                case 5: return "datetime";
                case 6: return "float";
                case 7: return "varbinary";
                case 8: return "bit";
                default: return "";
            }
        }

        /// <summary>
        ///     Define o item selecionado na combo box de acordo com o tipo de dado.
        /// </summary>
        /// <param name="TipoDado">tipo de dado.</param>
        /// <returns>Inteiro correspondente ao indice na lista.</returns>
        private int DefineTipoDadoColuna(String TipoDado)
        {
            switch (TipoDado.ToLower())
            {
                case "inteiro": return 1;
                case "texto": return 2;
                case "caractere": return 3;
                case "data": return 4;
                case "data e hora": return 5;
                case "real": return 6;
                case "imagem": return 7;
                case "verdadeiro ou falso": return 8;
                default: return 0;
            }
        }

        /// <summary>
        ///     Método responsável por montar a consulta SQL que irá adicionar a coluna.
        /// </summary>
        /// <returns>String formata para execução no SQL.</returns>
        private String MontaConsultaSql_NovaColuna()
        {
            mNomeColuna = String.Format("USR_{0}", string.Join("", txtNomeColuna.Text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)));
            return Consultas_EcMgr.CADASTRA_NOVA_COLUNA.Replace("@TABELA", txtNomeTabela.Text).Replace("@COLUNA", String.Format("USR_{0}", string.Join("", txtNomeColuna.Text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)))).Replace("@TIPO", TipoDadoColuna()).Replace("@VALOR_DEFAULT", String.IsNullOrEmpty(txtValorPadrao.Text) ? "N''" : txtValorPadrao.Text);
        }

        #endregion

        #region Eventos

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaColuna();
                btnVoltar_Click(new object(), new EventArgs());//Fecha a interface atual.
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar coluna no banco de dados", "FrmConfig_AdicionaColuna", "btnSalvar_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVA_COLUNA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        #endregion

        #region Eventos para definir e remover o foco

        private void txtNomeColuna_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtNomeColuna);
        }

        private void txtNomeColuna_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtNomeColuna.BackColor = Color.White;
        }

        private void txtValorPadrao_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtValorPadrao);
        }

        private void txtValorPadrao_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtValorPadrao.BackColor = Color.White;
        }

        #endregion

    }
}