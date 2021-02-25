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
    internal partial class FrmConfig_RefAuto : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     DataTabe que contém os dados das tabelas e colunas para mostrar ao usuário.
        /// </summary>
        private DataTable mDtDadosTabela;

        /// <summary>
        ///     Variável que determina se o formulário já foi carregado. Criado para tratar
        /// erros nos eventos 'SelectedIndexChanged' das combo boxes.
        /// </summary>
        private Boolean mJaCarregouTela = false;

        /// <summary>
        ///     Contém uma referência que o cliente deseja modificar.
        /// </summary>
        private ReferenciaAutomatica mRefAuto;

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto para criar uma nova referência.
        /// </summary>
        public FrmConfig_RefAuto()
        {
            InitializeComponent();
            InicializaValoresDefault();
        }

        /// <summary>
        ///     Instância o objeto para modificar uma referência existente.
        /// </summary>
        /// <param name="SkaReferencia">Objeto contendo a referência</param>
        public FrmConfig_RefAuto(ReferenciaAutomatica SkaReferencia)
        {
            InitializeComponent();
            //InicializaValoresDefault();

            //BLoqueia alguns controles que não podem ser modificados.
            cbTabelas.Enabled = false;
            cbColunas.Enabled = false;
            txtValorAtual.Enabled = false;

            mRefAuto = SkaReferencia;

            CarregaReferenciaAutoEdicao();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que inicializa os valores padrões da interface.
        /// </summary>
        private void InicializaValoresDefault()
        {

            //Carrega as tabelas em um objeto da classe para não ficar executando múltiplas consultas.
            mDtDadosTabela = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TABELAS);

            //Carrega as tabelas na lista
            if (mRefAuto == null)
                cbTabelas.Items.Add("<Selecione>");
                cbTabelas.Items.AddRange(mDtDadosTabela.AsDataView().ToTable(true, "TABLE_NAME").AsEnumerable().Select(r => r.ItemArray[0].ToString()).OrderBy(u => u).ToArray());
                cbTabelas.SelectedIndex = 0;

            //Carrega as colunas na lista.
            if (mRefAuto == null)
                cbColunas.Items.Add("<Selecione>");
                cbColunas.SelectedIndex = 0;

            //Inicialização dos componentes.
            cbxZerosEsquerda.Enabled = false;
            cbxZerosDireita.Enabled = false;
            txtNumZeros.Enabled = false;
            //txtValorAtual.Enabled = false;
        }

        /// <summary>
        ///     Método que valida as configurações antes de salvar.
        /// </summary>
        /// <returns>True caso estiver inválido (um campo obrigatório inválido).</returns>
        private Boolean ValidaAntesDeSalvar()
        {
            int ret = 0;

            if (mRefAuto == null)
                ret += cbTabelas.SelectedIndex >= 1 ? 0 : + 1;

            if (mRefAuto == null)
                ret += cbColunas.SelectedIndex >= 1 ? 0 : + 1;

            ret += txtPrefixo.Text != "" ? 0 : + 1;
            ret += txtIncremento.Text != "" ? 0 : + 1;
            ret += txtValorInicial.Text != "" ? 0 : + 1;
            ret += txtValorFinal.Text != "" ? 0 : + 1;
            ret += txtValorAtual.Text != "" ? 0 : + 1;
            //Precisa ser obrigatoriamente maior que um o valor de zeros à inserir.
            ret += txtNumZeros.Text != "" && Convert.ToInt32(txtNumZeros.Text) > 0 ? 0 : +1;

            return ret > 0;
        }

        /// <summary>
        ///     Salva a referência automática (nova ou existente) no banco de dados do Edgecam manager.
        /// </summary>
        private void SalvaReferenciaAuto()
        {
            Cursor = Cursors.WaitCursor;

            if (!ValidaAntesDeSalvar())
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (mRefAuto != null) dic.Add("@TBL", cbTabelas.Text.ToString());
                else dic.Add("@TBL", cbTabelas.SelectedItem.ToString());

                if (mRefAuto != null) dic.Add("@CLM", cbColunas.Text.ToString());
                else dic.Add("@CLM", cbColunas.SelectedItem.ToString());

                dic.Add("@PRE", txtPrefixo.Text);
                dic.Add("@INC", Convert.ToInt32(txtIncremento.Text));
                dic.Add("@VI", Convert.ToInt32(txtValorInicial.Text));
                dic.Add("@VF", Convert.ToInt32(txtValorFinal.Text));
                dic.Add("@VA", Convert.ToInt32(txtValorAtual.Text));
                dic.Add("@INSZEROS", cbxCompletarZeros.Checked);
                dic.Add("@ESQ", cbxZerosEsquerda.Checked);
                dic.Add("@DIR", cbxZerosDireita.Checked);
                dic.Add("@NUMZEROS", Convert.ToInt32(txtNumZeros.Text));
                dic.Add("@DT", DateTime.Now);

                //Significa que o usuário está auterando uma referência automática.
                if (mRefAuto != null)
                {
                    dic.Add("@ID", mRefAuto.Id);
                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_REFERENCIA_AUTOMATICA, dic);

                    Cursor = Cursors.Arrow;
                    Messages.Msg069();

                    btnVoltar_Click(new object(), new EventArgs());
                }
                else
                {
                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVA_REFERENCIA_AUTOMATICA, dic);

                    Cursor = Cursors.Arrow;
                    Messages.Msg068();

                    btnVoltar_Click(new object(), new EventArgs());
                }
            }
            else
            {
                Cursor = Cursors.Arrow;
                Messages.Msg070();
            }
        }

        /// <summary>
        ///     Método que carrega uma referência existente para edição.
        /// </summary>
        private void CarregaReferenciaAutoEdicao()
        {
            if (mRefAuto != null)
            {
                cbTabelas.Items.Add(mRefAuto.NomeTabela);
                cbTabelas.SelectedIndex = 0;

                cbColunas.Items.Add(mRefAuto.NomeColuna);
                cbColunas.SelectedIndex = 0;

                txtPrefixo.Text = mRefAuto.Prefixo;                
                txtIncremento.Text = mRefAuto.Incremento.ToString();
                txtValorInicial.Text = mRefAuto.ValorInicial.ToString();
                txtValorFinal.Text = mRefAuto.ValorFinal.ToString();
                txtValorAtual.Text = mRefAuto.ValorContadorAtual.ToString();
                cbxCompletarZeros.Checked = mRefAuto.InserirZeros;
                cbxZerosEsquerda.Checked = mRefAuto.ZerosAEsquerda;
                cbxZerosDireita.Checked = mRefAuto.ZerosADireita;
                txtNumZeros.Text = mRefAuto.NumZerosInserir.ToString();
            }
        }

        #endregion

        #region Eventos para remoção de caracteres em campos do tipo texto

        private void txtIncremento_TextChanged(object sender, EventArgs e)
        {
            txtIncremento.Text = CustomStrings.DeixaSomenteNumeros(txtIncremento.Text);
        }

        private void txtValorInicial_TextChanged(object sender, EventArgs e)
        {
            txtValorInicial.Text = CustomStrings.DeixaSomenteNumeros(txtValorInicial.Text);
        }

        private void txtValorFinal_TextChanged(object sender, EventArgs e)
        {
            txtValorFinal.Text = CustomStrings.DeixaSomenteNumeros(txtValorFinal.Text);
        }

        private void txtValorAtual_TextChanged(object sender, EventArgs e)
        {
            txtValorAtual.Text = CustomStrings.DeixaSomenteNumeros(txtValorAtual.Text);
        }

        private void txtNumZeros_TextChanged(object sender, EventArgs e)
        {
            txtNumZeros.Text = CustomStrings.DeixaSomenteNumeros(txtNumZeros.Text);
        }

        #endregion

        #region Eventos

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaReferenciaAuto();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao salvar referência automática", "FrmConfig_RefAuto", "Criar/Atualizar", "Exceção não tratada",
                                           "Consultas_EcMgr.ATUALIZA_REFERENCIA_AUTOMATICA/CADASTRA_NOVA_REFERENCIA_AUTOMATICA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        private void cbTabelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mJaCarregouTela)
                return;
            if (cbTabelas.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione primeiro uma tabela", "Ação não permitida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                cbColunas.Items.Clear();
                cbColunas.Items.Add("<Selecione>");
                cbColunas.Items.AddRange(mDtDadosTabela.AsEnumerable().Where(x => x.ItemArray[0].ToString().ToUpper() == cbTabelas.SelectedItem.ToString().ToUpper()).Select(y => y.ItemArray[1].ToString()).OrderBy(u => u).ToArray());
                cbColunas.SelectedIndex = 0;
            }
        }

        private void FrmConfig_RefAuto_Shown(object sender, EventArgs e)
        {
            mJaCarregouTela = true;
        }

        private void cbxCompletarZeros_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxCompletarZeros.Checked)
            {
                cbxZerosEsquerda.Enabled = true;
                cbxZerosDireita.Enabled = true;
                txtNumZeros.Enabled = true;
            }
            else
            {
                cbxZerosEsquerda.Enabled = false;
                cbxZerosDireita.Enabled = false;
                txtNumZeros.Enabled = false;
            }
        }

        private void cbxZerosEsquerda_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxZerosEsquerda.Checked)
                cbxZerosDireita.Enabled = false;
            else cbxZerosDireita.Enabled = true;
        }

        private void cbxZerosDireita_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxZerosDireita.Checked)
                cbxZerosEsquerda.Enabled = false;
            else cbxZerosEsquerda.Enabled = true;
        }

        #endregion

    }
}