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
    internal partial class FrmDadosAuxiliares_New : Form
    {

        #region Variáveis da classe

        /// <summary>
        ///     Contém o id do dado auxiliar recém criado.
        /// </summary>
        private String mIdDadoAuxiliar = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Propriedade que contém o id do dado auxiliar recém criado.
        /// </summary>
        public String _IdDadoAuxiliar
        {
            get
            {
                return mIdDadoAuxiliar;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmDadosAuxiliares_New()
        {
            InitializeComponent();
            //Objects.DefineColorThemeInterface(this);
        }

        public FrmDadosAuxiliares_New(List<String> LstNomesPecas)
        {
            InitializeComponent();
            //Objects.DefineColorThemeInterface(this);
        }

        #endregion

        #region Métodos

        private void SalvaNovoDadoAuxiliar()
        {
            if (!String.IsNullOrEmpty(txtDescricao.Text))
            {
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_DADO_AUXILIAR, new Dictionary<string, object>() { { "@DADOAUX", txtDescricao.Text }, { "@USR", Objects.UsuarioAtual.Login } });
                DataTable tmp = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ULTIMO_ID_DADO_AUXILIAR);

                if (tmp != null && tmp.Rows.Count > 0)
                {
                    mIdDadoAuxiliar = tmp.Rows[0]["id"].ToString();
                }

                MessageBox.Show("Dado auxiliar cadastrado com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReturn_Click(new object(), new EventArgs());
            }
            else MessageBox.Show("Você deve preencher o campo do dado auxiliar obrigatoriamente para salvar.", "Dado auxiliar não informado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Eventos

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaNovoDadoAuxiliar();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar novo dado auxiliar", "FrmDadosAuxiliares_New", "btnSalvar_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_DADO_AUXILIAR", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos para definir o foco

        private void txtDescricao_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtDescricao);
        }

        private void txtDescricao_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtDescricao);
        }

        #endregion

    }
}