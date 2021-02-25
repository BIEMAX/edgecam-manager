using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ImagedComboBox;
using System.Reflection;
using Edgecam_Manager.Idiomas;

namespace Edgecam_Manager
{
    internal partial class FrmTarefas_New : Form
    {

        #region Enumeradores privados

        /// <summary>
        ///     Enumerador que possuí o tipo de comando, para definir como será o método
        /// de salvamento da tarefa (update/insert)
        /// </summary>
        private enum e_Comando
        {
            /// <summary>
            ///     Nova tarefa (Insert)
            /// </summary>
            Novo,
            /// <summary>
            ///     Editar tarefa (update)
            /// </summary>
            Editar,
            /// <summary>
            ///     Visualizar tarefa e uma possível edição (update).
            /// </summary>
            Visualizar
        }

        #endregion

        #region Variáveis globais

        /// <summary>
        ///     Variável global que contém o número de controles adicionados (usuários).
        /// </summary>
        private int mContadorControles;

        /// <summary>
        ///     Variável global que contém o valor do Y para adicionar um novo botão
        /// e uma nova PictureBox na interface.
        /// </summary>
        private int mNovaPosicaoYBtnPic = 372;

        /// <summary>
        ///     Variável global que contém o valor do Y para adicionar um novo labelna interface.
        /// </summary>
        private int mNovaPosicaoYLabel = 381;

        /// <summary>
        ///     Contém uma lista dos usuários para criar a tarefa e impedir que um mesmo usuário
        /// seja adicionado mais de uma vez.
        /// </summary>
        private List<String> mLstUsuariosSelec;

        /// <summary>
        ///     Contém um objeto com a tarefa que o usuário pretende editar ou visualizar.
        /// </summary>
        private Tarefa mTarefaUsuario;

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que instancia o objeto, utilizado apenas quando se criar uma nova tarefa.
        /// </summary>
        public FrmTarefas_New()
        {
            InitializeComponent();
            InicializaValoresDefault();
            btnEdit.Visible = false;
            btnCancelar.Visible = false;

            //Objects.DefineColorThemeInterface(this);
        }

        /// <summary>
        ///     Método que instancia o objeto, utilizando apenas quando se quer editar/visualizar
        /// uma tarefa.
        /// </summary>
        /// <param name="Tarefa">Objeto contendo a tarefa à ser editada/visualizada</param>
        /// <param name="DesabilitarControles">True para desabilitar os controles (apenas visualização)</param>
        public FrmTarefas_New(Tarefa Tarefa, Boolean DesabilitarControles)
        {
            InitializeComponent();
            InicializaValoresDefault();
            mTarefaUsuario = Tarefa;

            CarregaTarefa();

            if (DesabilitarControles)
            {
                DesabilitaControlesInterface();
            }
            else btnEdit.Visible = false;

            //Objects.DefineColorThemeInterface(this);
        }

        /// <summary>
        ///     Inicializa os valores 'padrões' na interface (controles de filtros por exemplo).
        /// </summary>
        private void InicializaValoresDefault()
        {
            //ComboBox de prioridades
            cbxPrioridade.Items.Add(new ComboBoxItem("Baixa", Properties.Resources.p_baixa));
            cbxPrioridade.Items.Add(new ComboBoxItem("Normal (Médio)", Properties.Resources.p_normal));
            cbxPrioridade.Items.Add(new ComboBoxItem("Alta", Properties.Resources.p_alta));
            cbxPrioridade.SelectedIndex = 1;//Sempre selecionado médio

            //ComboBox de tipo de tarefa à ser executada
            cbxTipoTarefa.Items.Add(new ComboBoxItem("<Selecione>"));
            cbxTipoTarefa.Items.Add(new ComboBoxItem("Notificação", Properties.Resources.Notification));
            cbxTipoTarefa.Items.Add(new ComboBoxItem("Chamada telefônica", Imagens_NewLookInterface.telefone_ligacao_16));
            cbxTipoTarefa.Items.Add(new ComboBoxItem("E-mail", Properties.Resources.Email));
            cbxTipoTarefa.Items.Add(new ComboBoxItem("Reunião", Properties.Resources.Meeting));
            cbxTipoTarefa.Items.Add(new ComboBoxItem("Tarefa", Edgecam_Manager.Imagens_NewLookInterface.tarefa_16));
            cbxTipoTarefa.SelectedIndex = 0;//Sempre selecionado médio
            
            CarregaUsuariosComboBox();

            mLstUsuariosSelec = new List<string>();

            /*
             *  ECMGR-197
             *  08/11/2018
             */
            dtDataInicio.DateTime = DateTime.Now;
            dtDataFim.DateTime = DateTime.Now;
        }

        /// <summary>
        ///     Método que carrega a lista de usuários em um objeto estático e os carrega
        /// na combo box da interface.
        /// </summary>
        private void CarregaUsuariosComboBox()
        {
            cbxUsuarios.Items.Add("<Selecione>");            

            if (Objects.LstUsuarios == null)
            {
                Objects.LoadUsersList();
                cbxUsuarios.Items.AddRange(Objects.LstUsuarios.ToArray());
            }
            else if (Objects.LstUsuarios != null && Objects.LstUsuarios.Count > 0)
            {
                //  Caso esse lista já tenha sido carregada anteriormente, apenas jogo os itens na combobox.
                cbxUsuarios.Items.AddRange(Objects.LstUsuarios.ToArray());
            }

            cbxUsuarios.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que cria novos comandos na interface para o usuário, onde,
        /// adiciona via programação, um novo PictureBox, Label e Button com os
        /// usuários adicionados pelo usuário do sistema.
        /// </summary>
        private void AdicionaNovoUsuario()
        {
            //Botão para remover o usuário.
            Button b = new Button();
            b.ForeColor = System.Drawing.Color.SteelBlue;
            //p.Image = global::Edgecam_Manager.Properties.Resources.Delete;
            b.Image = Imagens_NewLookInterface.remover_deletar;
            b.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            b.Location = new System.Drawing.Point(187, mNovaPosicaoYBtnPic);
            b.Name = "btnRemoveUsr" + mContadorControles;
            b.Size = new System.Drawing.Size(111, 31);
            b.TabIndex = 72;
            b.Text = "Remover usuário";
            b.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            b.UseVisualStyleBackColor = true;
            b.Visible = true;
            b.Click += new EventHandler(btnRemoveUsr_Click);
            ugbDados.Controls.Add(b);

            //Label contendo o nome do usuário.
            Label l = new Label();
            l.AutoSize = true;
            l.Location = new System.Drawing.Point(60, mNovaPosicaoYLabel);
            l.Name = "lblNomeUsuario" + mContadorControles;
            l.Size = new System.Drawing.Size(35, 13);
            l.TabIndex = 71;
            l.Text = cbxUsuarios.SelectedItem.ToString();
            l.Visible = true;
            ugbDados.Controls.Add(l);

            //Imagem
            PictureBox pc = new PictureBox();
            //pc.Image = global::Edgecam_Manager.Properties.Resources.user_default_icon;
            pc.Image = Imagens_NewLookInterface.usuario_azul;
            pc.InitialImage = null;
            pc.Location = new System.Drawing.Point(18, mNovaPosicaoYBtnPic);
            pc.Name = "pictureBox" + mContadorControles;
            pc.Size = new System.Drawing.Size(36, 31);
            pc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pc.TabIndex = 70;
            pc.TabStop = false;
            pc.Visible = true;
            ugbDados.Controls.Add(pc);
        }

        /// <summary>
        ///     Método que valida se todos os campos 'obrigatórios' estão devidamente preenchidos.
        /// </summary>
        /// <returns>True caso estiver tudo OK e false para caso contenha erros.</returns>
        /// <remarks>Não faz nenhum teste de conexão para verificar a autenticidade das informações.</remarks>
        private Boolean ValidaDadosAntesSalvar()
        {
            int erros = 0;

            if (String.IsNullOrEmpty(txtAssunto.Text))
                erros++;

            //Esse campo deixa de ser obrigatório a partir da 2019 R2 B4
            //if (String.IsNullOrEmpty(rtxtInstrucao.Text))
            //    erros++;

            if (cbxTipoTarefa.SelectedIndex == 0)
                erros++;

            //Removi essas linhas, pois irei utilizar esse método para quando o usuário edita a tarefa.
            //if (!lblNomeUsuario.Visible)
            //    erros++;

            if (erros > 0)
                return false;
            else return true;
        }

        /// <summary>
        ///     Método que salva uma nova tarefa no banco de dados.
        /// </summary>
        private void SalvaNovaTarefa()
        {
            //Caso for diferente de nulo, o usuário abriu a edição desta tarefa, logo, executa-se um update no banco.
            if (mTarefaUsuario != null)
            {
                if (ValidaDadosAntesSalvar())
                {
                    Dictionary<String, Object> dic = new Dictionary<String, Object>();
                    dic.Add("@ASSUNTO", txtAssunto.Text);
                    dic.Add("@INSTRU", rtxtInstrucao.Text);
                    dic.Add("@TIPO", cbxTipoTarefa.SelectedIndex);
                    dic.Add("@PRIORIDADE", cbxPrioridade.SelectedIndex + 1);//O índice começa em 0, mas o banco aceita 1 para cima.
                    dic.Add("@DTINICIO", dtDataInicio.DateTime.ToString("yyyy-MM-dd"));
                    dic.Add("@DTFIM", dtDataFim.DateTime.ToString("yyyy-MM-dd"));
                    dic.Add("@DTULTMOD", DateTime.Now);//Última modificação
                    dic.Add("@USRULTMOD", Objects.UsuarioAtual.Login);//Usuário responsável pela última modificação.
                    dic.Add("@ID", mTarefaUsuario.IdTarefa);

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_TAREFA, dic);

                    Messages.Msg011();
                    ubtnSair_Click(new object(), new EventArgs());
                }
                else Messages.Msg013();
            }
            //Caso chegar até aqui, significa que o usuário está criando uma nova tarefa.
            else
            {
                if (ValidaDadosAntesSalvar() && lblNomeUsuario.Visible)
                {
                    foreach (String usuario in mLstUsuariosSelec)
                    {
                        Dictionary<String, Object> dic = new Dictionary<String, Object>();
                        dic.Add("@ASSUNTO", txtAssunto.Text);
                        dic.Add("@INSTRU", rtxtInstrucao.Text);
                        dic.Add("@TIPO", cbxTipoTarefa.SelectedIndex);
                        dic.Add("@PRIORIDADE", cbxPrioridade.SelectedIndex + 1);//O índice começa em 0, mas o banco aceita 1 para cima.
                        dic.Add("@DTINICIO", dtDataInicio.DateTime.ToString("yyyy-MM-dd"));
                        dic.Add("@DTCRIACAO", DateTime.Now);
                        dic.Add("@DTFIM", dtDataFim.DateTime.ToString("yyyy-MM-dd"));
                        dic.Add("@USR", usuario);
                        dic.Add("@USRSOLICITANTE", Objects.UsuarioAtual.Login);

                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVA_TAREFA, dic);
                    }

                    Messages.Msg012();
                    ubtnSair_Click(new object(), new EventArgs());
                }
                else Messages.Msg013();
            }
        }

        /// <summary>
        ///     Método responsável por carregar a tarefa na interface do usuário na janela.
        /// </summary>
        /// <remarks>Como o usuário está editando a sua tarefa e não a dos outros,
        /// eu 'escondo' os botões dos usuários.</remarks>
        private void CarregaTarefa()
        {
            txtAssunto.Text             = mTarefaUsuario.Assunto;
            cbxPrioridade.SelectedIndex = Convert.ToInt16(mTarefaUsuario.Prioridade) - 1;//Precisa ser menos para dar certo na interface.
            dtDataInicio.DateTime       = Convert.ToDateTime(mTarefaUsuario.DtInicio);
            dtDataFim.DateTime          = Convert.ToDateTime(mTarefaUsuario.DtLimitePrazo);
            cbxTipoTarefa.SelectedIndex = Convert.ToInt16(mTarefaUsuario.TipoTarefa);
            rtxtInstrucao.Text          = mTarefaUsuario.Instrucoes;

            MarcaTarefaComoLido();

            //Esconde os controles de adicionar ou remover usuários.
            label6.Visible          = false;
            cbxUsuarios.Visible     = false;
            btnAddUsr.Visible       = false;
            pictureBox.Visible      = false;
            lblNomeUsuario.Visible  = false;
            btnRemoveUsr.Visible    = false;
        }

        /// <summary>
        ///     Método que, ao receber uma tarefa para visualizar, é determinada como lida
        /// no banco de dados.
        /// </summary>
        private void MarcaTarefaComoLido()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@ID", mTarefaUsuario.IdTarefa);
            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.MARCA_TAREFA_COMO_LIDA, dic);
            Objects.CadastraNovoLog(false, String.Format("Usuário completou a tarefa de id {0}", mTarefaUsuario.IdTarefa), "FrmTarefas", "MarcaTarefaComoLido", "", "", e_TipoErroEx.Informacao);
        }

        /// <summary>
        ///     Habilita os controles da interface para edição.
        /// </summary>
        private void HabilitaControlesInterface()
        {
            //Desabilita a edição dos controles
            txtAssunto.Enabled      = true;
            cbxPrioridade.Enabled   = true;
            dtDataInicio.Enabled    = true;
            dtDataFim.Enabled       = true;
            cbxTipoTarefa.Enabled   = true;
            rtxtInstrucao.Enabled   = true;
            ubtnSalvar.Visible      = true;
            btnCancelar.Visible     = true;
        }

        /// <summary>
        ///     Desabilita os controles da interface para edição.
        /// </summary>
        private void DesabilitaControlesInterface()
        {
            //Desabilita a edição dos controles
            txtAssunto.Enabled      = false;
            cbxPrioridade.Enabled   = false;
            dtDataInicio.Enabled    = false;
            dtDataFim.Enabled       = false;
            cbxTipoTarefa.Enabled   = false;
            rtxtInstrucao.Enabled   = false;

            ubtnSalvar.Visible      = false;

            label13.Visible         = false;
            btnCancelar.Visible     = false;
            
            //Habilita o botão, caso o usuário deseja re-editar.
            btnEdit.Enabled         = true;
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Evento que saí da interface atual.
        /// </summary>
        private void ubtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();

            //Remove o controle para permitir o sistema consultar os dados.
            Objects.FechaTelaPendenteInterface(this.GetHashCode().ToString());
            Objects.FormularioPrincipal.Controls.Remove(this);
        }

        /// <summary>
        ///     Botão que cria ou atualiza uma tarefa. Isso depende de como é instanciado o objeto 'FrmTarefas_New'.
        /// </summary>
        private void ubtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaNovaTarefa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao Criar/atualizar a tarefa", "FrmTarefa_New", "btnSave_Click", "Exceção não tratada em uma tentativa de criar/atualizar a tarefa",
                                           "Consultas_EcMgr.CADASTRA_NOVA_TAREFA/ATUALIZA_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Adiciona um usuário na lista de usuários responsáveis por essa tarefa.
        /// </summary>
        private void btnAddUsr_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxUsuarios.SelectedItem.ToString().ToUpper().Trim() == "<SELECIONE>")
                {
                    MessageBox.Show("Por favor, selecione um usuário válido", "Usuário inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //  Primeiro válido se o usuário já não foi adicionado.
                if (mLstUsuariosSelec.Where(x => x.ToUpper().Trim() == cbxUsuarios.SelectedItem.ToString().ToUpper()).Count() > 0)
                {
                    Messages.Msg014();
                    return;
                }

                //  Como é o primeiro usuário, apenas habilito a visualização dos recursos.
                if (!pictureBox.Visible && !lblNomeUsuario.Visible && !btnRemoveUsr.Visible)
                {
                    pictureBox.Visible = true;
                    lblNomeUsuario.Visible = true;
                    btnRemoveUsr.Visible = true;

                    lblNomeUsuario.Text = cbxUsuarios.SelectedItem.ToString();

                    mLstUsuariosSelec.Add(cbxUsuarios.SelectedItem.ToString());
                }
                //  Caso não for o primeiro usuário, adiciona novos elementos na interface.
                else
                {
                    mNovaPosicaoYBtnPic += 51;
                    mNovaPosicaoYLabel += 51;
                    mContadorControles++;
                    AdicionaNovoUsuario();
                    mLstUsuariosSelec.Add(cbxUsuarios.SelectedItem.ToString());
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar adicionar usuário", "FrmTarefas_New", "btnAddUsr_Click", "Exceção não tratada durante adição de usuário",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Remove o usuário que foi adicionado pelo usuário anteriormente.
        /// </summary>
        private void btnRemoveUsr_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtém o nome do controle para remover seus filhos da interface.
                Button btnClicked = sender as Button;
                String nomeCtrl = btnClicked.Name;
                //Variável temporária que contém o indice dos controles adicionados na interface.
                int indiceCtrl = 0;

                //  Verifico se ele ele clicou no botão default que só está invisível ou em outro criado
                //via aplicação.
                if (int.TryParse(nomeCtrl.ToUpper().Replace("BTNREMOVEUSR", ""), out indiceCtrl))
                {
                    //Remove o usuário da lista de usuários já adicionados.
                    mLstUsuariosSelec.Remove(ugbDados.Controls["lblNomeUsuario" + indiceCtrl].Text);

                    //Remove botão
                    ugbDados.Controls.Remove(ugbDados.Controls[nomeCtrl]);
                    //Remove imagem
                    ugbDados.Controls.Remove(ugbDados.Controls["pictureBox" + indiceCtrl]);
                    //Remove label
                    ugbDados.Controls.Remove(ugbDados.Controls["lblNomeUsuario" + indiceCtrl]);

                    mNovaPosicaoYBtnPic -= 51;
                    mNovaPosicaoYLabel -= 51;
                }
                else
                {
                    pictureBox.Visible = false;
                    lblNomeUsuario.Visible = false;
                    btnRemoveUsr.Visible = false;
                    mLstUsuariosSelec.Remove(lblNomeUsuario.Text);
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar remover usuário", "FrmTarefas_New", "btnRemoveUsr_Click", "Exceção não tratada durante remoção de usuário",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Habilita a edição dos controles na interface para o usuário.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            HabilitaControlesInterface();
            btnEdit.Enabled = false;
        }

        /// <summary>
        ///     Desabilita (cancela) a edição dos controles na interface para o usuário.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DesabilitaControlesInterface();
        }

        #endregion

        #region Eventos para definir e remover o foco

        private void txtAssunto_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtAssunto);
        }

        private void txtAssunto_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtAssunto);
        }

        private void cbxPrioridade_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbxPrioridade);
        }

        private void cbxPrioridade_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbxPrioridade);
        }

        private void dtDataInicio_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(dtDataInicio);
        }

        private void dtDataInicio_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(dtDataInicio);
        }

        private void dtDataFim_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(dtDataFim);
        }

        private void dtDataFim_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(dtDataFim);
        }

        private void cbxTipoTarefa_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbxTipoTarefa);
        }

        private void cbxTipoTarefa_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbxTipoTarefa);
        }

        private void rtxtInstrucao_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(rtxtInstrucao);
        }

        private void rtxtInstrucao_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(rtxtInstrucao);
        }

        private void cbxUsuarios_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(cbxUsuarios);
        }

        private void cbxUsuarios_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(cbxUsuarios);
        }

        #endregion

    }
}
