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
    public partial class FrmFiltros_New : Form
    {
        #region Variáveis globais

        private String mInterface;
        private String mModulo;
        private String mFields;

        #endregion

        #region Instância dos objetos da classe

        private FrmFiltros_New()
        {
            InitializeComponent();
        }

        public FrmFiltros_New(String Interface, String Modulo, String Fields)
        {
            this.InitializeComponent();
            this.mInterface = Interface;
            this.mModulo = Modulo;
            this.mFields = Fields;
        }

        #endregion

        #region Métodos

        private void SaveNewFilter()
        {
            Dictionary<String, Object> dic = new Dictionary<string, object>();
            dic.Add("@INTER", mInterface);
            dic.Add("@MOD", mModulo);
            dic.Add("@USR", Objects.UsuarioAtual.Login);
            dic.Add("@ID", Objects.UsuarioAtual.Id);
            dic.Add("@NAME", txtNomeFiltro.Text);
            //If checked, means that this filter is public
            dic.Add("@PRIV", cbxPrivate.Checked ? false : true);
            dic.Add("@FILTERS", mFields);

            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_FILTRO_INTERFACE, dic);

            MessageBox.Show("Filtro cadastrado com êxito.", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Objects.LoadUsersFilter();

            this.btnReturn_Click(true, new EventArgs());
        }

        #endregion

        #region Eventos

        private void btnReturn_Click(object sender, EventArgs e)
        {
            bool isBool = false;

            try
            {
                Boolean.TryParse(sender.ToString(), out isBool);
            }
            catch { isBool = false; }

            //If is bool, means that the user saved the filter, so, I can close this form.
            if (isBool)
            {
                this.Close();
                GC.Collect();
            }
            else if (MessageBox.Show("Deseja mesmo sair sem salvar?", "Dados não salvos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.Close();
                GC.Collect();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNomeFiltro.Text.Contains("-") || txtNomeFiltro.Text.Contains("<") || txtNomeFiltro.Text.Contains(">") || txtNomeFiltro.Text.Contains("[") || txtNomeFiltro.Text.Contains("]"))
                MessageBox.Show("O nome do filtro não pode conter caracteres como '-', '<' e '>'.", "Nome do filtro inválido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (!String.IsNullOrEmpty(txtNomeFiltro.Text)) this.SaveNewFilter();
            else MessageBox.Show("Você precisa obrigatóriamente informar um nome para seu filtro personalizado",
                                 "Nome do filtro não informado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion
    }
}