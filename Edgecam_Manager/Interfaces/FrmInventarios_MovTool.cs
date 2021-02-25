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
    internal partial class FrmInventarios_MovTool : Form
    {

        #region Variáveis globais

        private e_TipoMovEstoque mTipoMoviEstoque;
        private int mToolId;
        private Boolean mSalvouMovimento = false;
        private String mNewQtde = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///     Propriedade que determina se o usuário cancelou ou salvo o movimento de inventário.
        /// </summary>
        public Boolean _SalvouMovimento
        {
            get
            {
                return mSalvouMovimento;
            }
        }

        /// <summary>
        ///     Contém a nova quantidade de ferramentas no estoque.
        /// </summary>
        public String _NewQtdeEstoque
        {
            get
            {
                return mNewQtde;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmInventarios_MovTool(e_TipoMovEstoque TipoMoviEstoque, int ToolId)
        {
            InitializeComponent();

            mTipoMoviEstoque = TipoMoviEstoque;
            mToolId = ToolId;

            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        private void InicializaValoresDefault()
        {
            switch (mTipoMoviEstoque)
            {
                case e_TipoMovEstoque.Entrada: label12.Text += " a dar entrada"; break;
                case e_TipoMovEstoque.Saida: label12.Text += " a dar baixa"; break;
                case e_TipoMovEstoque.Transferencia: label12.Text += " a transferir"; break;
                case e_TipoMovEstoque.Outro: label12.Text += " a movimentar"; break;
            }

            cbFornecedores.Items.Add("<Selecione>");
            cbFornecedores.Items.AddRange(ConsultaFornecedores().ToArray());
            cbFornecedores.SelectedIndex = 0;

            cbUnidadeEmpresa.Items.Add("<Selecione>");
            cbUnidadeEmpresa.Items.AddRange(Objects.LstUnidOrg.Select(x => x.Unidade).ToArray());
            cbUnidadeEmpresa.SelectedIndex = 0;

            cbArmazem.Items.Add("<Selecione>");
            cbArmazem.Items.AddRange(ConsultaArmazens().ToArray());
            cbArmazem.SelectedIndex = 0;
        }


        /// <summary>
        ///     Consulta os fornecedores.
        /// </summary>
        /// <returns>Lista de string</returns>
        private List<String> ConsultaFornecedores()
        {
            try
            {
                return Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_FORNECEDORES).AsEnumerable().Select(x => x.ItemArray[0].ToString()).ToList();
            }
            catch { return new List<String>() { "<Selecione>" }; }
        }

        /// <summary>
        ///     Consulta os armazéns das unidades.
        /// </summary>
        /// <returns>Lista de string</returns>
        private List<String> ConsultaArmazens()
        {
            try
            {
                return Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ARMAZENS).AsEnumerable().Select(x => x.ItemArray[0].ToString()).ToList();
            }
            catch { return new List<String>() { "<Selecione>" }; }
        }

        /// <summary>
        ///     Método que valida se todos os campos foram preenchidos.
        /// </summary>
        /// <returns>True caso todos os campos foram preenchidos devidamente.</returns>
        private Boolean CamposObrigatoriosPreenchidos()
        {
            int emptyFields = 0;
            try
            {
                emptyFields += String.IsNullOrEmpty(txtQuantidade.Text) ? 1 : 0;
                emptyFields += String.IsNullOrEmpty(txtMotivo.Text) ? 1 : 0;
                emptyFields += String.IsNullOrEmpty(txtQuantidade.Text) ? 1 : 0;

                if (emptyFields == 0) return true;
                else return false;
            }
            catch { return false; }
        }

        private void SalvaMovimento()
        {
            /*
             *  Dionei Beilke dos Santos
             *      Adicionado a condição abaixo para movimentos, pois na consulta sempre irá
             * realizar um movimento de estoque de entrada (soma), e nunca de saída (subtração).
             */
            String atualizaQtdeEstoque = Consultas_EcMgr.ATUALIZA_QTDE_ESTOQUE_TOOL;

            if (CamposObrigatoriosPreenchidos())
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@TOOLID", mToolId);

                switch (mTipoMoviEstoque)
                {
                    case e_TipoMovEstoque.Entrada:
                        dic.Add("@INFO", "Entrada de inventário de quantidade " + txtQuantidade.Text);
                        dic.Add("@ACAO", 1);
                        break;
                    case e_TipoMovEstoque.Saida:
                        dic.Add("@INFO", "Saída de inventário de quantidade " + txtQuantidade.Text);
                        dic.Add("@ACAO", 2);
                        atualizaQtdeEstoque = atualizaQtdeEstoque.Replace("+", "-");
                        break;
                    case e_TipoMovEstoque.Transferencia: 
                        dic.Add("@INFO", "Transferência entre unidades de inventário de quantidade " + txtQuantidade.Text);
                        dic.Add("@ACAO", 3);
                        atualizaQtdeEstoque = atualizaQtdeEstoque.Replace("+", "-");
                        break;
                    case e_TipoMovEstoque.Outro:
                        dic.Add("@INFO", "Movimento de inventário de quantidade " + txtQuantidade.Text);
                        dic.Add("@ACAO", 4);
                        break;
                }

                
                dic.Add("@USR", Objects.UsuarioAtual.Login);                
                dic.Add("@MOTIVO", txtMotivo.Text);
                dic.Add("@QTDE", Convert.ToInt16(txtQuantidade.Text));
                dic.Add("@FOR", !String.IsNullOrEmpty(cbFornecedores.Text) && cbFornecedores.Text.ToUpper() != "<SELECIONE>" ? cbFornecedores.Text : DBNull.Value.ToString());
                dic.Add("@UNI", !String.IsNullOrEmpty(cbUnidadeEmpresa.Text) && cbUnidadeEmpresa.Text.ToUpper() != "<SELECIONE>" ? cbUnidadeEmpresa.Text : DBNull.Value.ToString());
                dic.Add("@ARM", !String.IsNullOrEmpty(cbArmazem.Text) && cbArmazem.Text.ToUpper() != "<SELECIONE>" ? cbArmazem.Text : DBNull.Value.ToString());
                dic.Add("@LOTE", txtLote.Text);
                dic.Add("@DES", DBNull.Value.ToString());

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_MOV_EST_INVENTARIO_TOOL, dic);

                //Já faço a consulta da nova quantidade em estoque.
                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(atualizaQtdeEstoque, 
                                                    new Dictionary<string, object>() 
                                                    { 
                                                        { "@ID", mToolId }, 
                                                        { "@NEWQTDE", Convert.ToInt16(txtQuantidade.Text) } 
                                                    });

                mNewQtde = dt.Rows[0]["QuantidadeEstoque"].ToString();

                mSalvouMovimento = true;
                this.Close();
                GC.Collect();
            }
            else MessageBox.Show("Verifique se os campos 'Quantidade' e/ou 'Motivo' estão vazios.", "Campos não preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Eventos


        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            txtQuantidade.Text = CustomStrings.DeixaSomenteNumeros(txtQuantidade.Text);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            mSalvouMovimento = false;
            this.Close();
            GC.Collect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaMovimento();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar movimento no estoque de ferramentas", "FrmInventario_MovTool", 
                                           "btnSave_Click", mTipoMoviEstoque.ToString(), "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

    }
}