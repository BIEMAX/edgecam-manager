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
    internal partial class FrmLogin_NewPass : Form
    {

        #region Métodos

        /// <summary>
        ///     Apenas inicializa uma nova instância do objeto.
        /// </summary>
        public FrmLogin_NewPass()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Inicializa uma nova instância a partir da interface,
        /// onde, já carrega o usuário e liberar apenas o campo
        /// senha para ser modificado.
        /// </summary>
        /// <param name="CurrentUser"></param>
        public FrmLogin_NewPass(String CurrentUser)
        {
            InitializeComponent();

            txtUser.Text = CurrentUser;
            txtUser.Enabled = false;
        }
        
        /// <summary>
        ///     Método que atualiza a senha do usuário atual.
        /// </summary>
        private void AtualizaSenha()
        {
            Dictionary<String, object> dic = new Dictionary<string,object>();
            dic.Add("@USER", txtUser.Text);
            dic.Add("@PASS", txtSenha.Text);
            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_SENHA_USUARIO, dic);
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Ação que tenta realizar a troca (atualização) de senha do usuário.
        /// </summary>
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                AtualizaSenha();
                Messages.Msg002();
                btnReturn_Click(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar trocar senha", "FrmLogin_NewPass", "AtualizaSenha", "<None>",
                                           "Consultas_EcMgr.ATUALIZA_SENHA_USUARIO", e_TipoErroEx.Erro, ex);
            }
            
        }

        /// <summary>
        ///     Ação que fecha saí da interface atual.
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        #endregion

    }
}
