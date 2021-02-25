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
    internal partial class FrmConfig_CamposExportar : Form
    {
        #region Variáveis globais

        private CampoExportar mCampo;

        #endregion

        #region Instância dos objetos da classe

        public FrmConfig_CamposExportar()
        {
            InitializeComponent();

            InicializaValoresDefault();
        }

        public FrmConfig_CamposExportar(CampoExportar Campo)
        {
            InitializeComponent();

            mCampo = Campo;

            InicializaValoresDefault();

            CarregaConfiguracoes();
        }

        #endregion

        #region Métodos

        private void InicializaValoresDefault()
        {
            cbColunas.Items.Add("<Selecione>");

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_CAMPOS_CONFIGURADOS_EXPORTACAO);
            String sql = Consultas_EcMgr.CONSULTA_COLUNAS_TABELA_ORDENS;

            if (dt != null && dt.Rows.Count > 0)
            {
                sql += String.Format(" and COLUMN_NAME not in ({0})", dt.AsEnumerable().Select(x => "'" + x.ItemArray[0].ToString() + "'").Aggregate((oldValue, newValue) => string.Format("{0}, {1}", oldValue, newValue))).ToString();
            }

            cbColunas.Items.AddRange(Objects.CnnBancoEcMgr.ExecutaSql(sql).AsEnumerable().Select(r => r.ItemArray[0].ToString()).ToArray());
            cbColunas.SelectedIndex = 0;

            label2.Visible = false;
            txtValorPadrao.Visible = false;
        }

        /// <summary>
        ///     Carrega as configurações de uma coluna existente para editá-las.
        /// </summary>
        private void CarregaConfiguracoes()
        {
            if (mCampo != null)
            {
                cbColunas.SelectedText = mCampo.NomeCampo;
                txtElementoXml.Text = mCampo.NomeImportar;
                cbxAceitarNull.Checked = mCampo.AceitaNulos;

                if (cbxAceitarNull.Checked)
                    txtValorPadrao.Text = mCampo.ValorPadrao;

                cbColunas.Enabled = false;
            }
        }

        private void SalvaColunaExportar()
        {
            if (mCampo != null)
            {
                //Realiza um update no banco de dados intermediário
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@NOME_XML", txtElementoXml.Text);
                dic.Add("@ACEITAR_NULL", cbxAceitarNull.Checked ? true : false);
                dic.Add("@DEFAULT", cbxAceitarNull.Checked ? txtValorPadrao.Text : "");
                dic.Add("@DTM", DateTime.Now);
                dic.Add("@ID", mCampo.Id);

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_CAMPO_EXPORTACAO, dic);
                Objects.CadastraNovoLog(false, String.Format("Campo '{0}' para exportação foi atualizado", mCampo.NomeCampo), "FrmConfig_CamposExportar", "btnSalva_Click", "Atualização de campos", "Consultas_EcMgr.ATUALIZA_CAMPO_EXPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Campo atualizado com êxito", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnVoltar_Click(new object(), new EventArgs());
            }
            else
            {
                if (ValidaAntesDeSalvar())
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("@NOME_DB", cbColunas.SelectedItem.ToString());
                    dic.Add("@NOME_XML", txtElementoXml.Text);
                    dic.Add("@ATIVO", "1");
                    dic.Add("@ACEITAR_NULL", cbxAceitarNull.Checked ? true : false);
                    dic.Add("@DEFAULT", cbxAceitarNull.Checked ? txtValorPadrao.Text : "");
                    dic.Add("@USR", Objects.UsuarioAtual.Login);
                    dic.Add("@DTC", DateTime.Now);
                    dic.Add("@DTM", DateTime.Now);

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_CAMPO_EXPORTAR, dic);
                    Objects.CadastraNovoLog(false, String.Format("Usuário '{0}' adicionou o campo '{1}' para exportação de ordens", Objects.UsuarioAtual.Login, cbColunas.SelectedItem.ToString()), "FrmConfig_CamposImportar", "btnSalva_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_CAMPO_IMPORTAR", e_TipoErroEx.Aviso);

                    MessageBox.Show("Campo adicionado com êxito", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnVoltar_Click(new object(), new EventArgs());
                }
                else MessageBox.Show("Alguns campos não foram preenchidos devidamente, favor, revisar.", "Campos não preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private Boolean ValidaAntesDeSalvar()
        {
            int ret = 0;

            ret += cbColunas.SelectedIndex > 0 ? 0 : 1;

            //É opcional
            //if (cbxAceitarNull.Checked)
            //    ret += String.IsNullOrEmpty(txtValorPadrao.Text) ? 1 : 0;

            if (ret == 0) return true; else return false;
        }

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

        #endregion

        #region Eventos

        private void cbxAceitarNull_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAceitarNull.Checked)
            {
                label2.Visible = true;
                txtValorPadrao.Visible = true;
            }
            else
            {
                label2.Visible = false;
                txtValorPadrao.Visible = false;
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaColunaExportar();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar campo para exportação", "FrmConfig_CamposExportar", "btnSalva_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_CAMPO_EXPORTAR", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos para determinar focus dos controles

        private void txtElementoXml_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtElementoXml);
        }

        private void txtElementoXml_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtElementoXml.BackColor = Color.White;
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

        private void cbColunas_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(cbColunas);
        }

        private void cbColunas_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            cbColunas.BackColor = Color.White;
        }

        #endregion
    }
}
