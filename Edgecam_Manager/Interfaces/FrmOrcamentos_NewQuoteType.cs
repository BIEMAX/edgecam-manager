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
    internal partial class FrmOrcamentos_NewQuoteType : Form
    {

        #region Global variables

        private TipoOrcamento mTipo;

        #endregion

        #region Properties

        public TipoOrcamento _Tipo
        {
            get { return mTipo; }
        }

        #endregion

        public FrmOrcamentos_NewQuoteType()
        {
            InitializeComponent();
        }

        #region Methods

        private Boolean HasEmptyFields()
        {
            if (String.IsNullOrEmpty(txtNome.Text)) return true;
            //else if (String.IsNullOrEmpty(txtDesc.Text)) return true;
            else return false;
        }

        private void SalvaNovoTipo()
        {
            if (this.HasEmptyFields())
            {
                MessageBox.Show("Alguns campos não foram preenchidos. Por favor, revise os campos e tente novamente.", 
                                "Campos não preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                Dictionary<String, Object> d = new Dictionary<string, object>();
                d.Add("@NOME", txtNome.Text);
                d.Add("@DESC", String.IsNullOrEmpty(txtDesc.Text) ? DBNull.Value : (Object)txtDesc.Text);
                d.Add("@CAT", DBNull.Value);
                d.Add("@USR", Objects.UsuarioAtual.Login);

                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_TIPO_ORCAMENTO, d);

                MessageBox.Show("Novo tipo de orçamento cadastrado com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (dt != null && dt.Rows.Count > 0)
                {
                    mTipo = new TipoOrcamento()
                    {
                        Id = dt.Rows[0]["id"].ToString(),
                        Nome = dt.Rows[0]["Nome"].ToString(),
                        Descricao = dt.Rows[0]["Descricao"].ToString(),
                        Categoria = dt.Rows[0]["Categoria"].ToString(),
                        Ativo = Convert.ToBoolean(dt.Rows[0]["Ativo"].ToString()),
                        DtCrt = Convert.ToDateTime(dt.Rows[0]["DtCriacao"].ToString()),
                        UsrCrt = dt.Rows[0]["UsuarioResp"].ToString()
                    };
                }

                this.ubtnSair_Click(new object(), new EventArgs());
            }
        }

        #endregion

        #region Events

        private void ubtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void ubtnSalvar_Click(object sender, EventArgs e)
        {
            this.SalvaNovoTipo();
        }

        #endregion
    }
}