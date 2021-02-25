using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;
using ImagedComboBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    internal partial class FrmOrcamentos_NewDet : Form
    {

        #region Variáveis globais

        private Orcamento mOrcamento;
        private Utilities mUtil = new Utilities();

        /// <summary>
        ///     Variável que determina se posso ou não tornar visível o botão de referência
        /// automatica para trabalhos de orçamentos
        /// </summary>
        private Boolean mPodeHabilitarRefAutoJob = false;

        /// <summary>
        ///     Método booleano que serve para impedir que o sistema
        /// faça alguns cálculos durante a sua inicialização.
        /// </summary>
        private Boolean mJaCarregouInterface = false;

        #endregion

        #region Enumeradores

        private enum e_SkaColunas
        {
            NomeRota,
            Operacao,
            OrdemExecucao,
            TempoEstimado,
            Custo
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instancia um novo objeto para criação de um novo orçamento.
        /// </summary>
        public FrmOrcamentos_NewDet()
        {
            InitializeComponent();
            InicializaValoresDefault();
            RemoveAbasInterface();
        }

        /// <summary>
        ///     Instancia um novo objeto para edição ou visualização de um orçamento.
        /// </summary>
        /// <param name="Orcamento">Objeto contendo os dados do orçamento</param>
        /// <param name="DesabilitarControles">True caso seja visulização, false para edição.</param>
        public FrmOrcamentos_NewDet(Orcamento Orcamento, Boolean DesabilitarControles)
        {
            InitializeComponent();
            mOrcamento = Orcamento;
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos gerais

        /// <summary>
        ///     Método que inicializa a construção da interface e de alguns controles
        /// para pesquisas e/ou criação de dados.
        /// </summary>
        private void InicializaValoresDefault()
        {
            if (mOrcamento == null) tabControl1.TabPages.Remove(tbEdgecam);

            label1.Text = String.Format("Orçamento - {0}", DateTime.Now.ToString("dd/MM/yyyy"));

            //Habilita os controles para referências automáticas.
            ValidaReferenciaAutomatica();

            //Remove a ordenação anterior que o usuário pode ter feito (fica salvo internamente no sistema).
            //UltraGridOptions udgv_orc = new UltraGridOptions(udgv, true);
            //Objects.DefineColorThemeInterface(this);

            icbxTiposOrc.Items.Add(new ComboBoxItem("<Selecione>"));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Estoque", Edgecam_Manager.Imagens_NewLookInterface.produto_inventario));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Ferramentas", Edgecam_Manager.Imagens_NewLookInterface.ferramentas));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Serviços (terceirização)", Edgecam_Manager.Imagens_NewLookInterface.servico_manutencao_16));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Serviços de usinagem", Edgecam_Manager.Imagens_NewLookInterface.servico_suporte));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Produção em lote", Edgecam_Manager.Imagens_NewLookInterface.producao_fabrica));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Cotação", Edgecam_Manager.Imagens_NewLookInterface.cambio_moeda_cotacao));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Implantação", Edgecam_Manager.Imagens_NewLookInterface.instalacao_software));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Consultoria", Edgecam_Manager.Imagens_NewLookInterface.consultoria_perguntar));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Compra", Imagens_NewLookInterface.compras_carrinho_16));
            icbxTiposOrc.Items.Add(new ComboBoxItem("Outros", Properties.Resources.help));
            icbxTiposOrc.SelectedIndex = 0;

            //Métodos de pagamento
            cbMetodos.Items.Add("<Selecione>");
            cbMetodos.Items.AddRange((Objects.LstPagamentos = new ListaMetodosPay()).Select(x => x.Nome).ToArray());
            cbMetodos.SelectedIndex = 0;

            //Lista de usuários.
            cbUserResp.Items.Add("<Selecione>");
            if (Objects.LstUsuarios != null && Objects.LstUsuarios.Count > 0) cbUserResp.Items.AddRange(Objects.LstUsuarios.ToArray());
            cbUserResp.SelectedIndex = 0;

            //Define o usuário solicitante
            //txtUserReq.Text = Objects.UsuarioAtual.Login == "X154812A85SD4DSDS5A1A1S8A" ? "API" : Objects.UsuarioAtual.Login;
            txtUserReq.Text = Objects.UsuarioAtual.Login;

            //Carrega as unidades organizacionais.
            cbUnidadeOrg.Items.Add("<Selecione>");
            if (Objects.LstUnidOrg != null && Objects.LstUnidOrg.Count > 0) cbUnidadeOrg.Items.AddRange(Objects.LstUnidOrg.Select(x => x.Unidade).ToArray());
            cbUnidadeOrg.SelectedIndex = 0;

            //Define o tipo de visibilidade (padrão é externo)
            icbxVisi.Items.Add(new ComboBoxItem("Externo", Properties.Resources.external_link));
            icbxVisi.Items.Add(new ComboBoxItem("Interno", Properties.Resources.internal_link));
            icbxVisi.SelectedIndex = 0;

            dtEntrega.DateTime = DateTime.Today;
            dtValidade.DateTime = DateTime.Today;

            //Se for nulo, significa que é um novo orçamento.
            if (mOrcamento == null)
            {
                lblVersao.Text = "Primeira versão";
                txtVersao.Text = "1";
            }

            //Definições da aba Matéria prima
            cbTipoBruto.SelectedIndex = 0;
            txtPrecoKg.Text     = "0";
            txtDensidade.Text   = "0";
            txtComprimento.Text = "0";
            txtLargura.Text     = "0";
            txtAltura.Text      = "0";
            txtQuilos.Text      = "0";
            txtCustoEstimado.Text = "0";

        }

        /// <summary>
        ///     Método que remove algumas abas quando se trata de um novo orçamento.
        /// Isso se deve ao de quê, essas abas só são populadas após iniciar o
        /// orçamento, abrindo as peças no edgecam por exemplo ou modificar o
        /// orçamento para ter um histórico.
        /// </summary>
        private void RemoveAbasInterface()
        {
            tabControl1.TabPages.Remove(tbHistorico);
            tabControl1.TabPages.Remove(tbEdgecam);
            tabControl1.TabPages.Remove(tbTrabalhos);
        }

        /// <summary>
        ///     Método que verifica se existe referência automática para 'Ordem de produção'
        /// e 'trabalhos' do Edgecam. Caso não existir, esconde os botões para utilizar
        /// as referências automáticas do sistema.
        /// </summary>
        private void ValidaReferenciaAutomatica()
        {

            //Verifica se existe 'ref auto para OP'
            if (Objects.ExisteReferenciaAutomatica("Orcamentos", "Orcamento"))
                btnRefAuto_Orc.Visible = true;
            else btnRefAuto_Orc.Visible = false;

            //Verifica se existe 'ref auto para trabalhos'
            if (Objects.ExisteReferenciaAutomatica("Orcamentos", "NomeTrabalhoEdgecam"))
            {
                mPodeHabilitarRefAutoJob = true;
                btnRefAuto_JobOrc.Visible = true;
            }
            else
            {
                mPodeHabilitarRefAutoJob = false;
                btnRefAuto_JobOrc.Visible = false;
            }
        }

        #endregion

        #region Eventos gerais

        private void FrmOrcamentos_New_Shown(object sender, EventArgs e)
        {
            mJaCarregouInterface = true;
        }

        #endregion

        #region Aba 'Geral'

        private void btnRefAuto_Orc_Click(object sender, EventArgs e)
        {
            try
            {
                txtOrdem.Text = Objects.BuscaNovaReferenciaAutomatica("Orcamentos", "Orcamento");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao obter nova referência automática", "FrmOrcamentos_New", "btnRefAuto_Orc_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void cbxDefinirUser_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDefinirUser.Checked)
            {
                label30.Visible = true;
                cbUserResp.Enabled = true;
            }
            else
            {
                label30.Visible = false;
                cbUserResp.SelectedIndex = 0;
                cbUserResp.Enabled = false;
            }
        }
        
        /// <summary>
        ///     Seleciona o cliente.
        /// </summary>
        private void btnSelCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FrmClientes_Seleciona frm = new FrmClientes_Seleciona();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtCliente.Text = frm._ClienteSelecionado.NomeEmpresa;
                    txtCidade.Text  = frm._ClienteSelecionado.Cidade;
                    txtEstado.Text  = frm._ClienteSelecionado.Estado;
                    txtPais.Text    = frm._ClienteSelecionado.Pais;

                    //Após o usuário selecionar, eu bloqueio os demais controles.
                    //txtCliente.Enabled = false;
                    txtCidade.Enabled = false;
                    txtEstado.Enabled = false;
                    txtPais.Enabled = false;

                    //btnSelCliente.Enabled = false;
                    btnSelCidade.Enabled = false;
                    btnSelEstado.Enabled = false;
                    btnSelPais.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os clientes", "FrmOrcamentos_New", "btnSelCliente_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Caso seja limpado o texto do campo, ele libera a seleção de outros comandos.
        /// </summary>
        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCliente.Text))
            {
                //Após o usuário selecionar, eu bloqueio os demais controles.
                //txtCliente.Enabled = false;
                txtCidade.Enabled = true;
                txtEstado.Enabled = true;
                txtPais.Enabled = true;

                //btnSelCliente.Enabled = false;
                btnSelCidade.Enabled = true;
                btnSelEstado.Enabled = true;
                btnSelPais.Enabled = true;
            }
        }
        
        /// <summary>
        ///     Seleciona o contato.
        /// </summary>
        private void btnSelContato_Click(object sender, EventArgs e)
        {
            try
            {
                FrmContatos_Seleciona frm = new FrmContatos_Seleciona();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtContato.Text = frm._ContatoSelecionado.Nome;
                    txtCliente.Text = frm._ContatoSelecionado.Cliente;
                    txtCidade.Text = frm._ContatoSelecionado.Cidade;
                    txtEstado.Text = frm._ContatoSelecionado.Estado;
                    txtPais.Text = frm._ContatoSelecionado.Pais;

                    //Após o usuário selecionar, eu bloqueio os demais controles.
                    txtCliente.Enabled = false;
                    txtCidade.Enabled = false;
                    txtEstado.Enabled = false;
                    txtPais.Enabled = false;

                    btnSelCliente.Enabled = false;
                    btnSelCidade.Enabled = false;
                    btnSelEstado.Enabled = false;
                    btnSelPais.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os contatos", "FrmOrcamentos_New", "btnSelContato_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Caso seja limpado o texto do campo, ele libera a seleção de outros comandos.
        /// </summary>
        private void txtContato_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtContato.Text))
            {
                //Após o usuário selecionar, eu bloqueio os demais controles.
                txtCliente.Enabled = true;
                txtCidade.Enabled = true;
                txtEstado.Enabled = true;
                txtPais.Enabled = true;

                btnSelCliente.Enabled = true;
                btnSelCidade.Enabled = true;
                btnSelEstado.Enabled = true;
                btnSelPais.Enabled = true;
            }
        }

        /// <summary>
        ///     Seleciona uma cidade
        /// </summary>
        private void btnSelCidade_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEstado_Seleciona frm = new FrmEstado_Seleciona();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtCidade.Text = frm._EstadoSelecionada.NomeCidade;
                    txtEstado.Text = frm._EstadoSelecionada.Estado;
                    txtPais.Text = frm._EstadoSelecionada.Pais;

                    //Após o usuário selecionar, eu bloqueio os demais controles.
                    //txtCliente.Enabled = false;
                    //txtCidade.Enabled = false;
                    txtEstado.Enabled = false;
                    txtPais.Enabled = false;

                    //btnSelCliente.Enabled = false;
                    //btnSelCidade.Enabled = false;
                    btnSelEstado.Enabled = false;
                    btnSelPais.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar as cidades", "FrmOrcamentos_New", "btnSelCidade_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Caso seja limpado o texto do campo, ele libera a seleção de outros comandos.
        /// </summary>
        private void txtCidade_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCidade.Text))
            {
                //Após o usuário selecionar, eu bloqueio os demais controles.
                //txtCliente.Enabled = true;
                //txtCidade.Enabled = true;
                txtEstado.Enabled = true;
                txtPais.Enabled = true;

                //btnSelCliente.Enabled = true;
                btnSelCidade.Enabled = true;
                btnSelEstado.Enabled = true;
                btnSelPais.Enabled = true;
            }
        }

        /// <summary>
        ///     Seleciona um estado.
        /// </summary>
        private void btnSelEstado_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEstado_Seleciona frm = new FrmEstado_Seleciona();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //txtContato.Text = frm._EstadoSelecionada.Nome;
                    //txtCliente.Text = frm._EstadoSelecionada.Cliente;
                    //txtCidade.Text = frm._EstadoSelecionada.Cidade;
                    txtEstado.Text = String.IsNullOrEmpty(frm._EstadoSelecionada.UF) ? frm._EstadoSelecionada.Estado : frm._EstadoSelecionada.UF;
                    txtPais.Text = frm._EstadoSelecionada.Pais;

                    //Após o usuário selecionar, eu bloqueio os demais controles.
                    //txtCliente.Enabled = false;
                    //txtCidade.Enabled = false;
                    //txtEstado.Enabled = false;
                    txtPais.Enabled = false;

                    //btnSelCliente.Enabled = false;
                    //btnSelCidade.Enabled = false;
                    //btnSelEstado.Enabled = false;
                    btnSelPais.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os estados", "FrmOrcamentos_New", "btnSelEstado_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Caso seja limpado o texto do campo, ele libera a seleção de outros comandos.
        /// </summary>
        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCidade.Text))
            {
                //Após o usuário selecionar, eu bloqueio os demais controles.
                //txtCliente.Enabled = true;
                //txtCidade.Enabled = true;
                //txtEstado.Enabled = true;
                txtPais.Enabled = true;

                //btnSelCliente.Enabled = true;
                //btnSelCidade.Enabled = true;
                //btnSelEstado.Enabled = true;
                btnSelPais.Enabled = true;
            }
        }

        /// <summary>
        ///     Seleciona um país.
        /// </summary>
        private void btnSelPais_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPais_Seleciona frm = new FrmPais_Seleciona();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //txtContato.Text = frm._ContatoSelecionado.Nome;
                    //txtCliente.Text = frm._ContatoSelecionado.Cliente;
                    //txtCidade.Text = frm._ContatoSelecionado.Cidade;
                    //txtEstado.Text = frm._ContatoSelecionado.Estado;
                    txtPais.Text = frm._PaisSelecionado.Pais;

                    ////Após o usuário selecionar, eu bloqueio os demais controles.
                    //txtCliente.Enabled = false;
                    //txtCidade.Enabled = false;
                    //txtEstado.Enabled = false;
                    //txtPais.Enabled = false;

                    //btnSelCliente.Enabled = false;
                    //btnSelCidade.Enabled = false;
                    //btnSelEstado.Enabled = false;
                    //btnSelPais.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os contatos", "FrmOrcamentos_New", "btnSelContato_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }
        
        /// <summary>
        ///     De acordo com o tipo de orçamento escolhido pelo usuário, é apresentado
        /// a opção de criar um trabalho de cotação.
        /// </summary>
        private void icbxTiposOrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (icbxTiposOrc.SelectedIndex)
            {
                case 0:
                    cbxCriarTrabalho.Visible = false;
                    label13.Visible = false;
                    txtJob.Visible = false;
                    btnRefAuto_JobOrc.Visible = false;
                    break;

                case 3:
                    cbxCriarTrabalho.Visible = true;
                    //label13.Visible = true;
                    //txtJob.Visible = true;
                    //btnRefAuto_JobOrc.Visible = true;
                    break;

                case 4:
                    cbxCriarTrabalho.Visible = true;
                    //label13.Visible = true;
                    //txtJob.Visible = true;
                    //btnRefAuto_JobOrc.Visible = true;
                    break;

                case 5:
                    cbxCriarTrabalho.Visible = true;
                    //label13.Visible = true;
                    //txtJob.Visible = true;
                    //btnRefAuto_JobOrc.Visible = true;
                    break;

                case 8:
                    cbxCriarTrabalho.Visible = true;
                    //label13.Visible = true;
                    //txtJob.Visible = true;
                    //btnRefAuto_JobOrc.Visible = true;
                    break;

                default:
                    cbxCriarTrabalho.Visible = false;
                    label13.Visible = false;
                    txtJob.Visible = false;
                    btnRefAuto_JobOrc.Visible = false;
                    break;
            }
        }

        private void icbxVisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (icbxVisi.SelectedIndex == 1)
                cbUnidadeOrg.Enabled = true;
            else
            {
                cbUnidadeOrg.SelectedIndex = 0;
                cbUnidadeOrg.Enabled = false;
            }
        }

        private void cbxCriarTrabalho_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxCriarTrabalho.Checked)
            {
                label31.Visible = true;
                label13.Visible = true;
                txtJob.Visible = true;

                if (mPodeHabilitarRefAutoJob) btnRefAuto_JobOrc.Visible = true;
                else btnRefAuto_JobOrc.Visible = false;
            }
            else
            {
                label31.Visible = false;
                label13.Visible = false;
                txtJob.Visible = false;
                btnRefAuto_JobOrc.Visible = false;
            }
        }

        private void btnRefAuto_JobOrc_Click(object sender, EventArgs e)
        {
            try
            {
                txtJob.Text = Objects.BuscaNovaReferenciaAutomatica("Orcamentos", "NomeTrabalhoEdgecam");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao obter nova referência automática", "FrmOrcamentos_New", "btnRefAuto_JobOrc_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();

            //Remove o controle para permitir o sistema consultar os dados.
            Objects.FechaTelaPendenteInterface(this.GetHashCode().ToString());
            Objects.FormularioPrincipal.Controls.Remove(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 *  Precisa verificar antes de salvar:
                 *  1-Contato existe?
                 *      1.1-Se o contato não existe, é OBRIGATÓRIO o preenchimento do campo 'cliente'
                 *      1.2-Se o campo 'cliente' foi informado, verificar se existe o contato cadastrado para esse cliente
                 *      1.3-Se o contato e o cliente não existirem ainda, preciso cadastrá-los dentro do meu sistema.
                 *  2-Usuário existe e cliente existe
                 *  3-Verificar se deseja salvar as peças no banco de dados.
                 */

            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar novo orçamento", "FrmOrcamentos_New", "btnSave_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Itens à orçar'

        private void SelecionaPeca()
        {
            List<String> files = mUtil.BuscaArquivos("Arquivos para orçamentos");

            if (files.Count > 0)
            {
                DataTable dt;
                if (udgvItens.Rows.Count > 0) dt = udgvItens.DataSource as DataTable;
                else
                {
                    dt = new DataTable();

                    dt.Columns.Add("Item", typeof(string));
                    dt.Columns.Add("Custo padrão (R$)", typeof(string));
                    dt.Columns.Add("Quantidade", typeof(int));
                    dt.Columns.Add("CaminhoDoItem", typeof(string));
                }

                for (int x = 0; x < files.Count; x++)
                {
                    //Nome do arquivo, Custo, Quantidade, CaminhoCompleto
                    dt.Rows.Add(files[x].Substring(files[x].LastIndexOf("\\") + 1), "0", "1", files[x]);
                }

                udgvItens.DataSource = dt;
            }
        }

        private void btnAddDet_Click(object sender, EventArgs e)
        {
            cms.Show(btnAddDet, new Point(0, btnAddDet.Height));
        }

        private void tsmPeca_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionaPeca();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar peça", "FrmOrcamentos_New_AbaItens", "tsmPeca_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmFerramenta_Click(object sender, EventArgs e)
        {

        }

        private void tsmEstoque_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Aba 'Material'

        private void CalculaPesoEmQuilos_MateriaPrima()
        {
            try
            {
                Double d = Double.Parse(txtDensidade.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                Double c = Double.Parse(txtComprimento.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                Double l = Double.Parse(txtLargura.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                Double a = Double.Parse(txtAltura.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                float v = float.Parse(txtPrecoKg.Text.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);

                //Double result = ((d * c * l * a) / 100000000);
                Double result = Math.CalculaPesoPeca(c, l, a, d);

                txtQuilos.Text = String.Format("{0} Kg", result);

                txtCustoEstimado.Text = String.Format("{0:C}", (result * v));
            }
            catch { txtQuilos.Text = "0"; }
        }

        private void Consulta_Materiais()
        {
            FrmMaterial_Seleciona frm = new FrmMaterial_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._MaterialSelecionado))
            {
                txtMaterial.Text = frm._MaterialSelecionado;
                txtDensidade.Text = frm._DensidadeSelecionado;
            }
        }

        private void Consulta_PrecoQuiloMaterial()
        {
            FrmCustoMaterial_Seleciona frm = new FrmCustoMaterial_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._MaterialSelecionado))
            {
                txtMaterial.Text = frm._MaterialSelecionado;
                txtDensidade.Text = frm._DensidadeSelecionado;
                txtPrecoKg.Text = frm._PrecoSelecionado;
            }
        }

        private void cbTipoBruto_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTipoBruto.SelectedIndex)
            {
                //<Selecione>
                case 0: 
                    pictureBox1.Image = null;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    break;
                //Bloco
                case 1: 
                    pictureBox1.Image = Properties.Resources.bloco;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    break;
                //Barra
                case 2: 
                    pictureBox1.Image = Properties.Resources.barra; 
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    break;
                //Tubo
                case 3: 
                    pictureBox1.Image = Properties.Resources.tubo_furo;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    break;
                //Barra sextavada
                case 4: 
                    pictureBox1.Image = Properties.Resources.barra_sextavada; 
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    break;
            }
        }

        private void btnBuscaMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_Materiais();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os materiais para orçamentos", "FrmOrcamentos_New/AbaMaterial", "btnBuscaMaterial_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnBuscaPrecoKg_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_PrecoQuiloMaterial();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar os materiais para orçamentos", "FrmOrcamentos_New/AbaMaterial", "btnBuscaMaterial_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void txtPrecoKg_TextChanged(object sender, EventArgs e)
        {
            if (!mJaCarregouInterface) return;
            txtPrecoKg.Text = CustomStrings.DeixaSomenteDecimais(txtPrecoKg.Text);
            CalculaPesoEmQuilos_MateriaPrima();
        }

        private void txtDensidade_TextChanged(object sender, EventArgs e)
        {
            if (!mJaCarregouInterface) return;
            txtDensidade.Text = CustomStrings.DeixaSomenteDecimais(txtDensidade.Text);
            CalculaPesoEmQuilos_MateriaPrima();
        }

        private void txtComprimento_TextChanged(object sender, EventArgs e)
        {
            if (!mJaCarregouInterface) return;
            txtComprimento.Text = CustomStrings.DeixaSomenteDecimais(txtComprimento.Text);
            CalculaPesoEmQuilos_MateriaPrima();
        }

        private void txtLargura_TextChanged(object sender, EventArgs e)
        {
            if (!mJaCarregouInterface) return;
            txtLargura.Text = CustomStrings.DeixaSomenteDecimais(txtLargura.Text);
            CalculaPesoEmQuilos_MateriaPrima();
        }

        private void txtAltura_TextChanged(object sender, EventArgs e)
        {
            if (!mJaCarregouInterface) return;
            txtAltura.Text = CustomStrings.DeixaSomenteDecimais(txtAltura.Text);
            CalculaPesoEmQuilos_MateriaPrima();
        }

        #endregion

        #region Aba 'Custos adicionais'

        /// <summary>
        ///     Abre a interface para o usuário cadastrar um novo custo adicional e já o carrega na interface.
        /// </summary>
        private void CadastraNovoCustoAdicional()
        {
            FrmOrcamentos_CustosNew frm = new FrmOrcamentos_CustosNew();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdCustoAdicional))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgvCustos.DataSource;

                if (dt == null)
                {
                    //Somente assim o sistema não gerou exceção.
                    udgvCustos.DataSource = SQLQueries.Consulta_CustosAdicionais(frm._IdCustoAdicional, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_DadosAuxiliares(frm._IdCustoAdicional, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgvCustos.DataSource = dt;
                }
            }
        }

        /// <summary>
        ///     Abre a interface que contém uma série de dados auxiliares previamente cadastrados.
        /// </summary>
        private void PermiteSelecaoCustoAdicional()
        {
            FrmOrcamentos_CustosSeleciona frm = new FrmOrcamentos_CustosSeleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdCustoAdicionalSelecionado))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgvCustos.DataSource;

                if (dt == null)
                {
                    //Somente assim o sistema não gerou exceção.
                    udgvCustos.DataSource = SQLQueries.Consulta_CustosAdicionais(frm._IdCustoAdicionalSelecionado, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_CustosAdicionais(frm._IdCustoAdicionalSelecionado, "", SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgvCustos.DataSource = dt;
                }
            }
        }

        private void btnNewCusto_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovoCustoAdicional();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar um novo custo adicional de produção", "FrnOrcamentosNew_AbaCustosAdicionais", "btnNewCusto_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnAddCusto_Click(object sender, EventArgs e)
        {
            try
            {
                PermiteSelecaoCustoAdicional();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar custos adicionais de produção", "FrnOrcamentosNew_AbaCustosAdicionais", "btnSelCustos_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Rotas de produção'

        private void ConsultaRotasProducao()
        {
            FrmOrcamentos_RotasSeleciona frm = new FrmOrcamentos_RotasSeleciona();
            frm.ShowDialog();

            if (frm._RotaSelecionada != null)
            {
                DataTable dt = SQLQueries.Consulta_RotasProducao(false, frm._RotaSelecionada);

                if (dt.Rows.Count > 0)
                {
                    //Aqui contém o nome das rotas de produção (pais)
                    DataTable lstNomesRotas = dt.AsDataView().ToTable(true, "NomeRota");

                    for (int y = 0; y < lstNomesRotas.Rows.Count; y++)
                    {
                        //Recebe os nós do UltraTreeView para a partir dele, ir adicionados os próximos 'nodos' (filhos).
                        UltraTreeNode n = utv.Nodes.Add();

                        n.Cells[(int)e_SkaColunas.NomeRota].Value = lstNomesRotas.Rows[y]["NomeRota"].ToString();

                        DataTable lstFilhos = dt.Select(String.Format("NomeRota = '{0}'", lstNomesRotas.Rows[y]["NomeRota"].ToString())).CopyToDataTable();

                        for (int z = 0; z < lstFilhos.Rows.Count; z++)
                        {
                            //Recebe os nós já adicionados no pai.
                            UltraTreeNode noFilho = n.Nodes.Add();

                            noFilho.Cells[(int)e_SkaColunas.Operacao].Value = lstFilhos.Rows[z]["Operacao"].ToString();
                            noFilho.Cells[(int)e_SkaColunas.OrdemExecucao].Value = lstFilhos.Rows[z]["OrdemExecucao"].ToString();
                            noFilho.Cells[(int)e_SkaColunas.TempoEstimado].Value = lstFilhos.Rows[z]["TempoEstimado"].ToString();
                            noFilho.Cells[(int)e_SkaColunas.Custo].Value = lstFilhos.Rows[z]["Custo"].ToString();

                            n.Nodes.Add(noFilho);
                        }

                        utv.Nodes.Add(n);
                    }

                    utv.ExpandAll();
                }
            }
        }

        private void btnAddRota_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaRotasProducao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar rota de produção", "FrmOrcamentos_NewAbaRotas", "btnAddRota_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Fatura'

        private void ConsultaMoedas()
        {
            FrmMoedas_Seleciona frm = new FrmMoedas_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._MoedaSelecionada))
            {
                txtMoeda.Text = frm._MoedaSelecionada;
            }
        }

        private void ConsultaCotacaoDiariaMoeda()
        {
            FrmCotacoesDiarias_Seleciona frm = new FrmCotacoesDiarias_Seleciona(txtMoeda.Text);
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._CotacaoSelecionada))
            {
                txtCotacaoMoeda.Text = frm._CotacaoSelecionada;
                txtMoeda.Text = frm._MoedaSelecionada;
            }
        }

        private void CadastraNovaTarifa()
        {
            FrmTarifas_New frm = new FrmTarifas_New();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdNovaTarifa))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Tarifas.DataSource;

                if (dt == null)
                {
                    udgv_Tarifas.DataSource = SQLQueries.Consulta_Tarifas(frm._IdNovaTarifa, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_Tarifas(frm._IdNovaTarifa, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Tarifas.DataSource = dt;
                }
            }
        }

        private void ConsultaTarifas()
        {
            FrmTarifas_Seleciona frm = new FrmTarifas_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._TarifaSelecionada))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Tarifas.DataSource;

                if (dt == null)
                {
                    udgv_Tarifas.DataSource = SQLQueries.Consulta_Tarifas(frm._TarifaSelecionada, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_Tarifas(frm._TarifaSelecionada, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Tarifas.DataSource = dt;
                }
            }
        }

        private void CadastraNovoImposto()
        {
            FrmImpostos_New frm = new FrmImpostos_New();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._IdNovoImposto))
            {
                //Esse datatable contém o que já foi adicionado à grade de dados.
                DataTable dt = (DataTable)udgv_Impostos.DataSource;

                if (dt == null)
                {
                    udgv_Impostos.DataSource = SQLQueries.Consulta_Impostos(frm._IdNovoImposto, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
                }
                else
                {
                    dt.Merge(SQLQueries.Consulta_Impostos(frm._IdNovoImposto, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
                    udgv_Impostos.DataSource = dt;
                }
            }
        }

        private void ConsultaImpostos()
        {
            //FrmImpostos_Seleciona frm = new FrmImpostos_Seleciona();
            //frm.ShowDialog();

            //if (!String.IsNullOrEmpty(frm._ImpostoSelecionado))
            //{
            //    //Esse datatable contém o que já foi adicionado à grade de dados.
            //    DataTable dt = (DataTable)udgv_Impostos.DataSource;

            //    if (dt == null)
            //    {
            //        udgv_Impostos.DataSource = SQLQueries.Consulta_Impostos(frm._ImpostoSelecionado, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true);
            //    }
            //    else
            //    {
            //        dt.Merge(SQLQueries.Consulta_Impostos(frm._ImpostoSelecionado, "", null, SQLQueries.e_SkaTipoConsultaDados.ConsultarPorId, true), false, MissingSchemaAction.Add);
            //        udgv_Impostos.DataSource = dt;
            //    }
            //}
        }

        private void txtCotacaoMoeda_TextChanged(object sender, EventArgs e)
        {
            txtCotacaoMoeda.Text = CustomStrings.DeixaSomenteDecimais(txtCotacaoMoeda.Text);
        }

        private void btnMoeda_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaMoedas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar moedas", "FrmOrcamentos_New_AbaFatura", "btnMoeda_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnCotacaoMoeda_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaCotacaoDiariaMoeda();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar cotações diárias", "FrmOrcamentos_New_AbaFatura", "btnCotacaoMoeda_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNewTarifa_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovaTarifa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar nova tarifa", "FrmOrcamentos_New_AbaFatura", "btnNewTarifa_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnSelTarifa_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaTarifas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar tarifa", "FrmOrcamentos_New_AbaFatura", "btnSelTarifa_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNewImposto_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovoImposto();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar novo imposto", "FrmOrcamentos_New_Aba_Fatura", "btnNewImposto_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnSelImposto_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaImpostos();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar impostos", "FrmOrcamentos_New_Aba_Fatura", "btnSelImposto_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            txtDesconto.Text = CustomStrings.DeixaSomenteDecimais(txtDesconto.Text);
        }

        private void txtValorFinal_TextChanged(object sender, EventArgs e)
        {
            txtValorFinal.Text = CustomStrings.DeixaSomenteDecimais(txtValorFinal.Text);
        }

        private void btnHisCalculo_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        #endregion

        #region Aba 'Histórico'

        #endregion

    }
}