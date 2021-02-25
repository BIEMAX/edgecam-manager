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
    public partial class FrmOrcamentos_CustosNew : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     Contém o id do custo adicional recém criado.
        /// </summary>
        private String mIdCustoAdicional = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Propriedade que contém o id do custo adicional recém criado.
        /// </summary>
        public String _IdCustoAdicional
        {
            get
            {
                return mIdCustoAdicional;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmOrcamentos_CustosNew()
        {
            InitializeComponent();
            //Objects.DefineColorThemeInterface(this);
            txtCusto.Text = "0.0";
            txtQtde.Text = "1";
        }

        #endregion

        #region Métodos

        private void SalvaNovoCustoAdicional()
        {
            if (!String.IsNullOrEmpty(txtDescricao.Text))
            {
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_CUSTO_ADICIONAL, new Dictionary<string, object>() 
                { 
                    { "@DESC", txtDescricao.Text }, 
                    { "@CUSTO", String.IsNullOrEmpty(txtCusto.Text) == true ? "0.0" : txtCusto.Text.Replace(",", ".") },
                    { "@QTDE", String.IsNullOrEmpty(txtQtde.Text) == true ? "1" : txtQtde.Text },
                    { "@USR", Objects.UsuarioAtual.Login } 
                });

                DataTable tmp = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ULTIMO_ID_CUSTO_ADICIONAL);

                if (tmp != null && tmp.Rows.Count > 0)
                {
                    mIdCustoAdicional = tmp.Rows[0]["id"].ToString();
                    MessageBox.Show("Dado auxiliar cadastrado com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReturn_Click(new object(), new EventArgs());
                }
            }
            else MessageBox.Show("Você deve preencher o campo do dado auxiliar obrigatoriamente para salvar.", "Dado auxiliar não informado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Evento para permitir apenas decimais na caixa de texto
        /// </summary>
        private void txtCusto_TextChanged(object sender, EventArgs e)
        {
            txtCusto.Text = CustomStrings.DeixaSomenteDecimais(txtCusto.Text);
        }

        /// <summary>
        ///     Evento para permitir apenas inteiros na caixa de texto
        /// </summary>
        private void txtQtde_TextChanged(object sender, EventArgs e)
        {
            txtQtde.Text = CustomStrings.DeixaSomenteNumeros(txtQtde.Text);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaNovoCustoAdicional();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar um novo custo adicional", "FrmOrcamentos_CustosNew", "btnSalvar_Click", "", "", e_TipoErroEx.Erro, ex);
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
            Objects.RemoveSelecaoCaixa(txtDescricao, true);
        }

        private void txtCusto_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtCusto);
        }

        private void txtCusto_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtCusto, true);
        }

        private void txtQtde_Enter(object sender, EventArgs e)
        {
            Objects.SetaSelecaoCaixa(txtQtde);
        }

        private void txtQtde_Leave(object sender, EventArgs e)
        {
            Objects.RemoveSelecaoCaixa(txtQtde, true);
        }

        #endregion
    }
}