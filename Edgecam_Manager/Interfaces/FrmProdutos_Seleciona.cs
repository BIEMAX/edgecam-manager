using ImagedComboBox;
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
    internal partial class FrmProdutos_Seleciona : Form
    {

        #region Variáveis globais

        private Produto mProduto = null;

        #endregion

        #region Propriedades

        public Produto _Produto { get { return mProduto; } }

        #endregion

        #region Class instances

        public FrmProdutos_Seleciona()
        {
            this.InitializeComponent();
            this.LoadDefaultValues();
            this.btnPesquisar_Click(new object(), new EventArgs());
        }

        #endregion

        #region Methods
        
        private void LoadDefaultValues()
        {
            icbxTipos.Items.Add(new ComboBoxItem("<Selecione>"));
            this.GetProductsTypes();
            icbxTipos.SelectedIndex = 0;
        }

        private void GetProductsTypes()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql("Select Nome from TiposProdutos where Ativo = 1");

            if(dt != null && dt.Rows.Count > 0)
            {
                for(int x = 0; x < dt.Rows.Count; x++)
                {
                    icbxTipos.Items.Add(new ComboBoxItem(dt.Rows[x]["Nome"].ToString()));
                }
            }
        }

        #endregion

        #region Events

        private void udgv_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR") return;
            else
            {
                mProduto = new Produto();
                mProduto.id             = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();
                mProduto.Nome           = udgv.Rows[e.Cell.Row.Index].Cells["Nome"].OriginalValue.ToString();
                mProduto.CustoUnitario  = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["Custo unitário"].OriginalValue.ToString());
                mProduto.CustoTotal     = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["Custo total"].OriginalValue.ToString());
                mProduto.DescontoPadrao = Convert.ToDouble(udgv.Rows[e.Cell.Row.Index].Cells["Desconto padrão"].OriginalValue.ToString());
                mProduto.Quantidade     = Convert.ToInt16(udgv.Rows[e.Cell.Row.Index].Cells["Quantidade"].OriginalValue.ToString());
                mProduto.Tipo           = Convert.ToInt16(udgv.Rows[e.Cell.Row.Index].Cells["Tipo_Db"].OriginalValue.ToString());
                mProduto.Observacao     = udgv.Rows[e.Cell.Row.Index].Cells["Observação"].OriginalValue.ToString();
                mProduto.Ativo          = Convert.ToBoolean(udgv.Rows[e.Cell.Row.Index].Cells["Ativo_Db"].OriginalValue.ToString());
                mProduto.UsuarioCrt     = udgv.Rows[e.Cell.Row.Index].Cells["Cadastrado por"].OriginalValue.ToString();
                mProduto.DtCriacao      = Convert.ToDateTime(udgv.Rows[e.Cell.Row.Index].Cells["Data de cadastro"].OriginalValue.ToString());
                mProduto.UsuarioUltMod  = udgv.Rows[e.Cell.Row.Index].Cells["Última alteração por"].OriginalValue.ToString();
                mProduto.DtUltMod       = Convert.ToDateTime(udgv.Rows[e.Cell.Row.Index].Cells["Data de última mod."].OriginalValue.ToString());

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            udgv.DataSource = SQLQueries.Consulta_ProdutosOrcamentos(txtItem.Text, txtObs.Text, icbxTipos.SelectedItem.ToString());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            mProduto = null;

            this.Close();
            GC.Collect();
        }

        #endregion
    }
}