using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CircularPictureBox;

namespace Edgecam_Manager
{
    internal partial class FrmUserData : Form
    {

        #region Variáveis da classe

        /// <summary>
        ///     Botão que identifica se o usuário iniciou a edição e por algum
        /// motivo não quis prosseguir.
        /// </summary>
        private Boolean mIniciouEdicao = false;

        #endregion

        #region Propriedades

        #endregion

        #region Instância dos objetos da classe

        public FrmUserData()
        {
            InitializeComponent();

            CarregaDadosUsuario();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que irá carregar os dados nos 'controllers' da interface.
        /// </summary>
        private void CarregaDadosUsuario()
        {
            if (Objects.UsuarioAtual != null)
            {
                lblUsuario.Text = String.IsNullOrEmpty(Objects.UsuarioAtual.Nome) == true ? Objects.UsuarioAtual.Login : String.Format("{0} {1}", Objects.UsuarioAtual.Nome, Objects.UsuarioAtual.SobreNome);

                if (Objects.UsuarioAtual.Imagem != null)
                {
                    //Usuário tem imagem
                    pic_ImgUser.Image = new Utilities().ConverteByteArrayEmImagem(Objects.UsuarioAtual.Imagem);
                    linkRemoverFoto.Enabled = true;
                }
                else
                {
                    //usuário ainda não tem imagem
                    pic_ImgUser.Image = Imagens_NewLookInterface.usuario_azul;
                    linkRemoverFoto.Enabled = false;
                }

                NotificationBadge.AddBadgeTo(btnTasks, ConsultaTarefasNaoLidas());

                txtEmail.Text = Objects.UsuarioAtual.Email;
                txtCargo.Text = Objects.UsuarioAtual.Cargo;
                txtCompany.Text = Objects.UsuarioAtual.UnidadeOrg;
                txtGestor.Text = Objects.UsuarioAtual.Gestor;

                if (Objects.UsuarioAtual.ExpirarSenha == 1)
                {
                    cbxExpirarSenha.Checked = true;
                    lblDataExpirarSenha.Visible = true;
                    lblDataExpirarSenha.Text = String.Format("Data de expiração da senha: {0:dd/MM/yyyy}", Objects.UsuarioAtual.DtExpiracaoSenha.ToString());
                }
                else
                {
                    cbxExpirarSenha.Checked = false;
                    lblDataExpirarSenha.Visible = false;
                }
            }
        }

        /// <summary>
        ///     Método que executa uma consulta no banco de dados e verifica
        /// quantas tarefas não lidas o usuário possui.
        /// </summary>
        /// <returns>String contendo a quantidade de tarefas</returns>
        private String ConsultaTarefasNaoLidas()
        {
            try
            {
                return Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TAREFAS_NAO_LIDAS,
                    new Dictionary<string, object>() { { "@USUARIO", Objects.UsuarioAtual.Login } }).AsEnumerable().Select(r => r.ItemArray[0]).FirstOrDefault().ToString();
            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        ///     Método que salva/Atualiza as configurações do usuário no banco de dados.
        /// </summary>
        private void SalvaConfiguracoes()
        {
            Dictionary<String, Object> dic = new Dictionary<string, object>();
            dic.Add("@USREMAIL", txtEmail.Text);
            dic.Add("@POSITION", txtCargo.Text);
            dic.Add("@UNI", txtCompany.Text);
            dic.Add("@BOSS", txtGestor.Text);
            dic.Add("@ID", Objects.UsuarioAtual.Id);

            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_CADASTRO_USUARIO, dic);

            MessageBox.Show("Dados atualizados com êxito", "Sucesso ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            mIniciouEdicao = false;

            BloqueiaControles();
        }

        /// <summary>
        ///     Método que fecha a interface do usuário.
        /// </summary>
        private void FechaInterfaceUsuario()
        {
            if (mIniciouEdicao)
            {
                if (MessageBox.Show("Uma edição está pendente de salvamento. Deseja realmente sair?", "Dados não salvos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                else
                {
                    this.Close();
                    GC.Collect();
                }
            }
            else
            {
                this.Close();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Bloqueia todos os controles da interface para edição.
        /// </summary>
        private void BloqueiaControles()
        {
            //txtLogin.Enabled = true;
            txtEmail.Enabled = false;
            txtCargo.Enabled = false;
            txtCompany.Enabled = false;
            txtGestor.Enabled = false;

            ////usuário poderá remover a hora que quiser as fotos
            //linkRemoverFoto.Enabled = true;
            //linkTrocarFoto.Enabled = true;

            cbxExpirarSenha.Enabled = false;
            btnSalvar.Enabled = false;
            btnEdit.Enabled = true;
            btnCancelar.Visible = false;
        }

        /// <summary>
        ///     Desbloqueia todos os controles da interface para o usuário
        /// poder editá-las ou atualizá-las.
        /// </summary>
        private void DesbloqueiaControles()
        {
            //txtLogin.Enabled = true;
            txtEmail.Enabled = true;
            txtCargo.Enabled = true;
            txtCompany.Enabled = true;
            txtGestor.Enabled = true;

            ////usuário poderá remover a hora que quiser as fotos
            //linkRemoverFoto.Enabled = true;
            //linkTrocarFoto.Enabled = true;

            cbxExpirarSenha.Enabled = false;
            btnSalvar.Enabled = true;

            //Desabilito o botão de edição, pois o usuário já deve ter clicado ao menos uma vez.
            btnEdit.Enabled = false;
            btnCancelar.Visible = true;
        }

        /// <summary>
        ///     Atualiza a foto do usuário.
        /// </summary>
        private void TrocarFoto()
        {
            Utilities u = new Utilities();
            String imgUsr = u.BuscaArquivo("Arquivos de imagem (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg");

            if (File.Exists(imgUsr))
            {
                Objects.UsuarioAtual.Imagem = u.ConverteImagemEmByteArray(imgUsr);

                //Objects.CnnBancoEcMgr.ExecutaSql("UPDATE UnidadeCentral set LogoEmpresa = @IMG", new Dictionary<string, object>() { { "@IMG", Objects.UsuarioAtual.Imagem } });
                //Objects.CnnBancoEcMgr.ExecutaSql("UPDATE Relatorios set ConteudoRelatorio = @IMG", new Dictionary<string, object>() { { "@IMG", File.ReadAllText(@"C:\Users\ANAKIN\Desktop\SamplesQuotes\Report_Quote_2.mrt") } });

                //Irá inserir o imagem no banco de dados e a carregar na interface.
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_FOTO_USUARIO, new Dictionary<string, object>() { { "@ID", Objects.UsuarioAtual.Id }, { "@IMG", Objects.UsuarioAtual.Imagem } });

                pic_ImgUser.Image = u.ConverteByteArrayEmImagem(Objects.UsuarioAtual.Imagem);
                pic_ImgUser.Refresh();

                linkRemoverFoto.Enabled = true;

                MessageBox.Show("Imagem atualizada com êxito", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Arquivo ilegível ou não localizado", "Erro ao ler arquivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        /// <summary>
        ///     Método que remove a foto do usuário.
        /// </summary>
        private void RemoverFoto()
        {
            if (MessageBox.Show("Uma vez removida a foto, não será possível de restaurá-la. Desseja prosseguir?", "Remoção de foto", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.REMOVE_FOTO_USUARIO, new Dictionary<string, object>() { { "@ID", Objects.UsuarioAtual.Id } });

                MessageBox.Show("Êxito ao remover a foto de perfil", "Sucesso ao remover", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Seta a imagem do objeto como nulo e carrega a imagem padrão.
                Objects.UsuarioAtual.Imagem = null;

                pic_ImgUser.Image = Imagens_NewLookInterface.usuario_azul;
                pic_ImgUser.Refresh();

                linkRemoverFoto.Enabled = false;
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Permite a edição
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                mIniciouEdicao = true;
                DesbloqueiaControles();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar editar as configurações do usuário", "FrmUserData", "btnEdit_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Cancela a edição.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                mIniciouEdicao = false;
                BloqueiaControles();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar cancelar a edição", "FrmUserData", "btnCancelar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Fecha a interface para configurar a interface atual do usuário.
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                FechaInterfaceUsuario();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar fechar a interface", "FrmUserData", "btnReturn_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Salva as configurações realizadas pelo usuário.
        /// </summary>
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaConfiguracoes();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao atualizar dados do usuário", "FrmUserData", "btnSalvar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Remove a foto de perfil do usuário.
        /// </summary>
        private void linkRemoverFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RemoverFoto();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar remover a foto de perfil", "FrmUserData", "linkRemoverFoto_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Troca a foto de perfil do usuário.
        /// </summary>
        private void linkTrocarFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                TrocarFoto();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar trocar a foto de perfil", "FrmUserData", "linkTrocarFoto_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}