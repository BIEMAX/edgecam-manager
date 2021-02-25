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
    public partial class FrmOrcamentos_NewSim : Form
    {
        //Posições
        //12; 12
        //12; 229
        //12; 384
        //12; 592

        #region Global variables

        /// <summary>
        ///     Contains an object with quote data.
        /// </summary>
        private Orcamento mQuote;

        /// <summary>
        ///     DataTable contendo os produtos para orçamento.
        /// </summary>
        private DataTable mProdutos = new DataTable();

        /// <summary>
        ///     Contém um objeto que contém o markup selecionado ou criado pelo usuário.
        /// </summary>
        private Markup mMkpSelecionado;

        /// <summary>
        ///     Lista de campos não preenchidos pelo usuário.
        /// </summary>
        private List<String> mLstCampos = null;

        /// <summary>
        ///     Variável que contém o item da lista do tipo de orçamento selecionado pelo usuário.
        /// </summary>
        private String mSelectedQuoteType;

        /// <summary>
        ///     Valor final sem desconto.
        /// </summary>
        private Double mMvalorFinal;

        /// <summary>
        ///     Valor final com desconto.
        /// </summary>
        private Double mValorFinalCDesconto;

        /// <summary>
        ///     Detalhes dos calculos dos orçamentos.
        /// </summary>
        private DataTable mDetalhesOrc;

        /// <summary>
        ///     Contém um objeto do orçamento para visualização ou edição.
        /// </summary>
        private Orcamento mOrc;

        #endregion

        #region Class instances 

        /// <summary>
        ///     Instancia um novo objeto para edição ou visualização de um orçamento.
        /// </summary>
        /// <param name="Ordem">Objeto contendo os dados da ordem</param>
        /// <param name="DesabilitarControles">True caso seja visulização, false para edição.</param>
        internal FrmOrcamentos_NewSim(Orcamento Orcamento, Boolean DesabilitarControles)
        {
            this.InitializeComponent();
            this.mOrc = Orcamento;
            this.LoadDefaultValues();            
            this.CarregaOrcamento();

            if (DesabilitarControles) this.DesabilitaControles();

            //De qualquer maneira, esse campo nunca será editável.
            txtOrca.Enabled = false;
        }

        public FrmOrcamentos_NewSim()
        {
            InitializeComponent();
            LoadDefaultValues();
        }

        internal FrmOrcamentos_NewSim(Orcamento Orc)
        {
            this.InitializeComponent();

            this.mQuote = Orc;
            this.LoadDefaultValues();
        }

        #endregion

        #region Methods

        private void LoadDefaultValues()
        {
            //Set quotes date.
            label1.Text = String.Format("Orçamento - {0}", DateTime.Now.ToString("dd/MM/yyyy"));

            //Users list.
            cbOrcamentista.Items.Add("<Selecione>");
            if (Objects.LstUsuarios != null && Objects.LstUsuarios.Count > 0) cbOrcamentista.Items.AddRange(Objects.LstUsuarios.ToArray());
            cbOrcamentista.SelectedIndex = 0;

            //Quotes date.
            dtCotacao.DateTime = DateTime.Today;
            dtValidade.DateTime = DateTime.Today;
            dtPrevEntrega.DateTime = (DateTime.Today.AddDays(7.0));

            //Set the requested user.
            cbVendedor.Items.Add("<Selecione>");
            if (Objects.LstUsuarios != null && Objects.LstUsuarios.Count > 0) cbVendedor.Items.AddRange(Objects.LstUsuarios.ToArray());
            cbVendedor.SelectedIndex = 0;

            //Load quotes type
            cbTipoOrc.Items.Add("<Selecione>");
            if(Objects.LstTiposOrcamentos != null && Objects.LstTiposOrcamentos.Count > 0) cbTipoOrc.Items.AddRange(Objects.LstTiposOrcamentos.Where(x => x.Ativo == true).Select(x => x.Nome).ToArray());
            cbTipoOrc.SelectedIndex = 0;

            // If is null, it's a new quote.
            if (mQuote == null)
            {
                lblVersao.Text = "Primeira versão";
                txtVersao.Text = "1";
            }

            //Verifica se existe 'ref auto para OP'
            if (Objects.ExisteReferenciaAutomatica("Orcamentos", "Orcamento"))
                btnRefAuto_Orc.Visible = true;
            else btnRefAuto_Orc.Visible = false;

            //Objects.AddHistoricoGrid(udgv_Historico, $"");

            //Usuario responsavel
            label10.Enabled = false;

            //Desconto
            label14.Enabled = false;
            txtDesconto.Enabled = false;
            label31.Visible = false;

            //frete
            label18.Enabled = false;
            txtFrete.Enabled = false;
            label29.Visible = false;

            //Mpeda
            label16.Enabled = false;
            txtMoeda.Enabled = false;
            label21.Visible = false;
            btnMoeda.Enabled = false;

            //Cotação
            label17.Enabled = false;
            txtCotacaoMoeda.Enabled = false;
            label22.Visible = false;
            btnCotacaoMoeda.Enabled = false;

            //Tipo venda
            cbTipoVenda.SelectedIndex = 0;

            label19.Enabled = false;
            txtMarkup.Enabled = false;
            label28.Visible = false;
            btnMarkup.Visible = false;
            btnNewMarkup.Visible = false;

            this.AddColumnsDataTable();
            this.LoadPaymentMethods();
        }

        private void CarregaOrcamento()
        {
            //Dados gerais
            txtOrca.Text = mOrc.CodOrca;
            String orcType = Objects.LstTiposOrcamentos.Where(x => x.Id == mOrc.IdTipoOrcamento.ToString()).Select(y => y.Nome).FirstOrDefault();
            cbTipoOrc.SelectedIndex = cbTipoOrc.FindStringExact(orcType);
            btnNewTipoOrc.Enabled = false;
            cbxDefinirUser.Checked = String.IsNullOrEmpty(mOrc.Orcamentista) ? false : true;
            cbOrcamentista.SelectedItem = mOrc.Orcamentista;
            cbVendedor.SelectedItem = mOrc.Vendedor;
            dtCotacao.DateTime = mOrc.DtCotacao;
            dtValidade.DateTime = mOrc.DtValidade;
            dtPrevEntrega.DateTime = mOrc.DtPrevEntrega;

            //Cliente
            txtCliente.Text = mOrc.Cliente;
            txtContato.Text = mOrc.Contato;
            txtCidade.Text = mOrc.Cidade;
            txtEstado.Text = mOrc.UF;
            txtPais.Text = mOrc.Pais;

            //Itens
            udgvItens.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ITENS_ORCAMENTOS, new Dictionary<string, object>() { { "@ID", mOrc.Id } });

            //Pagamento
            cbMetodos.SelectedItem = mOrc.NomePag;

            cbxDesconto.Checked = String.IsNullOrEmpty(mOrc.ValorDesconto.ToString()) ? false : true;
            txtDesconto.Text = mOrc.ValorDesconto.ToString();

            cbxFrete.Checked = String.IsNullOrEmpty(mOrc.ValorFrete.ToString()) ? false : true;
            txtFrete.Text = mOrc.ValorFrete.ToString();

            cbxUsarMoeda.Checked = String.IsNullOrEmpty(mOrc.NomeMoeda.ToString()) ? false : true;
            txtMoeda.Text = mOrc.NomeMoeda;
            btnMoeda.Visible = String.IsNullOrEmpty(mOrc.NomeMoeda.ToString()) ? false : true;
            txtCotacaoMoeda.Text = mOrc.ValorMoeda.ToString();
            btnCotacaoMoeda.Visible = String.IsNullOrEmpty(mOrc.NomeMoeda.ToString()) ? false : true;

            cbTipoVenda.SelectedItem = mOrc.NomeTipoVenda;
            txtMarkup.Text = mOrc.ValorSomarVenda.ToString();
            btnNewMarkup.Visible = false;
            btnMarkup.Visible = false;

            txtValor.Text = mOrc.ValorTotal.ToString();
            btnCalc.Visible = false;
            btnDetalhes.Visible = false;

            udgv_Historico.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_HISTORICO_ORCAMENTOS, new Dictionary<string, object>() { { "@ID", mOrc.Id } });
        }

        /// <summary>
        ///     Desabilita os controles.
        /// </summary>
        private void DesabilitaControles()
        {
            //Dados gerais
            txtOrca.Enabled = false;
            btnRefAuto_Orc.Visible = false;
            cbTipoOrc.Enabled = false;
            btnNewTipoOrc.Enabled = false;
            cbxDefinirUser.Enabled = false;
            cbOrcamentista.Enabled = false;
            cbVendedor.Enabled = false;
            dtCotacao.Enabled = false;
            dtValidade.Enabled = false;
            dtPrevEntrega.Enabled = false;

            //Esconde os labels
            label23.Visible = false;
            label35.Visible = false;
            label30.Visible = false;
            label34.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label32.Visible = false;
            label26.Visible = false;
            btnSelCliente.Visible = false;
            label27.Visible = false;
            btnSelContato.Visible = false;
            label36.Visible = false;
            btnSelCidade.Visible = false;
            label37.Visible = false;
            btnSelEstado.Visible = false;
            label38.Visible = false;
            btnSelPais.Visible = false;

            //Cliente
            txtCliente.Enabled = false;
            txtContato.Enabled = false;
            txtCidade.Enabled = false;
            txtEstado.Enabled = false;
            txtPais.Enabled = false;

            //Items a orçar.
            btnAddIt.Enabled = false;
            udgvItens.Enabled = false;

            //Pagamento
            cbMetodos.Enabled = false;

            cbxDesconto.Enabled = false;
            txtDesconto.Enabled = false;

            cbxFrete.Enabled = false;
            txtFrete.Enabled = false;

            cbxUsarMoeda.Enabled = false;
            txtMoeda.Enabled = false;
            btnMoeda.Enabled = false;
            txtCotacaoMoeda.Enabled = false;
            btnCotacaoMoeda.Enabled = false;

            cbTipoVenda.Enabled = false;
            txtMarkup.Enabled = false;
            btnNewMarkup.Enabled = false;
            btnMarkup.Enabled = false;
            
            txtValor.Enabled = false;
            btnCalc.Enabled = false;
            btnDetalhes.Enabled = false;

            //Esconde labels
            label31.Visible = false;
            label29.Visible = false;
            label21.Visible = false;
            label28.Visible = false;
            label22.Visible = false;

            ubtnSalvar.Visible = false;
        }

        /// <summary>
        ///     Habilita os controles da interface.
        /// </summary>
        private void HabilitaControles()
        {
            //Dados gerais
            txtOrca.Enabled = true;
            btnRefAuto_Orc.Enabled = true;
            cbTipoOrc.Enabled = true;
            btnNewTipoOrc.Enabled = true;
            cbxDefinirUser.Enabled = true;
            cbOrcamentista.Enabled = true;
            cbVendedor.Enabled = true;
            dtCotacao.Enabled = true;
            dtValidade.Enabled = true;
            dtPrevEntrega.Enabled = true;

            //Esconde os labels
            label23.Visible = true;
            label35.Visible = true;
            label30.Visible = true;
            label34.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            label32.Visible = true;
            label26.Visible = true;
            btnSelCliente.Visible = true;
            label27.Visible = true;
            btnSelContato.Visible = true;
            label36.Visible = true;
            btnSelCidade.Visible = true;
            label37.Visible = true;
            btnSelEstado.Visible = true;
            label38.Visible = true;
            btnSelPais.Visible = true;

            //Cliente
            txtCliente.Enabled = true;
            txtContato.Enabled = true;
            txtCidade.Enabled = true;
            txtEstado.Enabled = true;
            txtPais.Enabled = true;

            //Items a orçar.
            btnAddIt.Enabled = true;
            udgvItens.Enabled = true;

            //Pagamento
            cbMetodos.Enabled = true;

            cbxDesconto.Enabled = true;
            txtDesconto.Enabled = true;

            cbxFrete.Enabled = true;
            txtFrete.Enabled = true;

            cbxUsarMoeda.Enabled = true;
            txtMoeda.Enabled = true;
            btnMoeda.Enabled = true;
            txtCotacaoMoeda.Enabled = true;
            btnCotacaoMoeda.Enabled = true;

            cbTipoVenda.Enabled = true;
            txtMarkup.Enabled = true;
            btnNewMarkup.Enabled = true;
            btnMarkup.Enabled = true;

            txtValor.Enabled = true;
            btnCalc.Enabled = true;
            btnDetalhes.Enabled = true;

            //Mostra labels
            label31.Visible = true;
            label29.Visible = true;
            label21.Visible = true;
            label28.Visible = true;
            label22.Visible = true;

            ubtnSalvar.Visible = true;
        }

        private void AddColumnsDataTable()
        {
            mProdutos.Columns.Add(new DataColumn("Item", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("Custo unitário (R$)", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("Custo total (R$)", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("Quantidade", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("CaminhoDoItem", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("Tempo do processo", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("Tempo total", typeof(String)));
            mProdutos.Columns.Add(new DataColumn("IdTipoItensOrcamento", typeof(String)));
            //mProdutos.Columns.Add(new DataColumn("", typeof(String)));
            //mProdutos.Columns.Add(new DataColumn("", typeof(String)));
            //mProdutos.Columns.Add(new DataColumn("", typeof(String)));
            //mProdutos.Columns.Add(new DataColumn("", typeof(String)));
        }

        private void LoadPaymentMethods()
        {
            cbMetodos.Items.Add("<Selecione>");

            if (Objects.LstPagamentos == null) Objects.LstPagamentos = new ListaMetodosPay();

            cbMetodos.Items.AddRange((Objects.LstPagamentos = new ListaMetodosPay()).Select(x => x.Nome).ToArray());
            cbMetodos.SelectedIndex = 0;
        }
        
        private void ConsultaMoedas()
        {
            FrmMoedas_Seleciona frm = new FrmMoedas_Seleciona();
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._MoedaSelecionada)) txtMoeda.Text = frm._MoedaSelecionada; 
        }

        private void ConsultaCotacaoDiariaMoeda()
        {
            FrmCotacoesDiarias_Seleciona frm = new FrmCotacoesDiarias_Seleciona(txtMoeda.Text);
            frm.ShowDialog();

            if (!String.IsNullOrEmpty(frm._CotacaoSelecionada))
            {
                txtCotacaoMoeda.Text = CustomStrings.DeixaSomenteDecimais(frm._CotacaoSelecionada);
                txtMoeda.Text = frm._MoedaSelecionada;
            }
        }

        /// <summary>
        ///     Seleciona uma ou mais peças e adiciona dentro da grade 'itens para orçar'
        /// </summary>
        private void SelecionaPeca()
        {
            //Lista de arquivos selecionados.
            List<String> f = new List<string>();

            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.Title = "Arquivos para orçamentos";
            o.Filter = "Todos os arquivos (*.*)|*.*";

            foreach (EdgecamExtensions e in Edgecam.LstExtensoesValidasSolidos())
                o.Filter += String.Format("|{0}(*.{1})|*.{1}", e.Extensao, e.Descricao);

            if (o.ShowDialog() == DialogResult.OK)
            {
                //  Array com o nome & extensão de todos os arquivos selecionados pelo usuário.
                String[] arqSelecionados = o.SafeFileNames;
                String dir = o.FileName.Substring(0, o.FileName.LastIndexOf("\\"));

                foreach (string s in arqSelecionados) f.Add(System.IO.Path.Combine(dir, s));
            }

            if (f != null && f.Count > 0)
            {
                for (int x = 0; x < f.Count; x++)
                {
                    //Nome do arquivo, Custo unitário, custo total, Quantidade, CaminhoCompleto, tempoProcesso, tempoTotal, IdTipoItensOrcamento
                    mProdutos.Rows.Add(f[x].Substring(f[x].LastIndexOf("\\") + 1), "0", "0", "1", f[x], "00:00:00", "00:00:00", "1");
                }
                udgvItens.DataSource = mProdutos;
            }
        }

        /// <summary>
        ///     Valida se todos os campos obrigatórios foram preenchidos.
        /// </summary>
        /// <returns>True caso todos algum campo obrigatório não tenha sido preenchido corretamente.</returns>
        private Boolean HasAnyEmptyField()
        {
            mLstCampos = new List<string>();

            if (String.IsNullOrEmpty(txtOrca.Text)) mLstCampos.Add("Nome/código do orçamento");
            if (String.IsNullOrEmpty(dtCotacao.DateTime.ToShortDateString())) mLstCampos.Add("Data de cotação");
            if (String.IsNullOrEmpty(dtValidade.DateTime.ToShortDateString())) mLstCampos.Add("Data de validade");
            if (cbVendedor.SelectedIndex == 0) mLstCampos.Add("Vendedor");
            if (cbxDefinirUser.Checked)
            {
                if (cbOrcamentista.SelectedIndex == 0) mLstCampos.Add("Orçamentista");
            }
            if (String.IsNullOrEmpty(dtPrevEntrega.DateTime.ToShortDateString())) mLstCampos.Add("Data prevista para entrega");
            if (String.IsNullOrEmpty(txtCliente.Text)) mLstCampos.Add("Cliente");
            if (String.IsNullOrEmpty(txtContato.Text)) mLstCampos.Add("Contato");
            if (String.IsNullOrEmpty(txtCidade.Text)) mLstCampos.Add("Cidade");
            if (String.IsNullOrEmpty(txtEstado.Text)) mLstCampos.Add("Estado");
            if (String.IsNullOrEmpty(txtPais.Text)) mLstCampos.Add("País");
            if (udgvItens.Rows.Count == 0) mLstCampos.Add("Itens a orçar");
            if (cbMetodos.SelectedIndex == 0) mLstCampos.Add("Tipo de pagamento");
            if (cbxDesconto.Checked)
            {
                if(String.IsNullOrEmpty(txtDesconto.Text)) mLstCampos.Add("Valor do desconto");
            }
            if (cbxFrete.Checked)
            {
                if (String.IsNullOrEmpty(txtFrete.Text)) mLstCampos.Add("Valor do frete");
            }
            if (cbxUsarMoeda.Checked)
            {
                if (String.IsNullOrEmpty(txtMoeda.Text)) mLstCampos.Add("Moeda");
                if (String.IsNullOrEmpty(txtCotacaoMoeda.Text)) mLstCampos.Add("Cotação da moeda");
            }
            if (cbTipoVenda.SelectedIndex > 0)
            {
                if (String.IsNullOrEmpty(txtMarkup.Text)) mLstCampos.Add("Valor para cálculo (markup/margem)");
            }

            //Se tiver algum item na lista, significa que há campos obrigatórios não preenchidos.
            return mLstCampos.Count > 0 ? true : false;
        }

        private void CreateNewQuoteType()
        {
            FrmOrcamentos_NewQuoteType f = new FrmOrcamentos_NewQuoteType();
            f.ShowDialog();

            //Como o usuário cadastrou um novo tipo de orçamento, preciso recarregar a lista estática e a lista do usuário na interface.
            cbTipoOrc.Items.Clear();
            cbTipoOrc.Items.Add("<Selecione>");
            Objects.LstTiposOrcamentos = new ListQuotesType();
            cbTipoOrc.Items.AddRange(Objects.LstTiposOrcamentos.Where(x => x.Ativo == true).Select(x => x.Nome).ToArray());

            //Seta a seleção da lista com o item recém cadastrado.
            if (f._Tipo != null) cbTipoOrc.SelectedItem = f._Tipo.Nome;
            else cbTipoOrc.SelectedItem = mSelectedQuoteType;
        }

        private void CreateNewMarkup()
        {
            FrmOrcamentos_MarkupSeleciona f = new FrmOrcamentos_MarkupSeleciona();
            f.ShowDialog();

            if (f._MarkupSelecionado != null)
            {
                mMkpSelecionado = f._MarkupSelecionado;
                txtMarkup.Text = mMkpSelecionado.MarkupUp.ToString();
            }
        }

        /// <summary>
        ///     Mostra os valores mais detalhados do orçamento atual.
        /// </summary>
        private void ShowQuoteDetailed(Boolean ShowInterface = true)
        {
            if (!this.HasAnyEmptyField())
            {
                Orcamento o = new Orcamento()
                {
                    CodOrca = txtOrca.Text,
                    Cidade = txtCidade.Text,
                    UF = txtEstado.Text,
                    Pais = txtPais.Text,
                    PossuiDesconto = cbxDesconto.Checked,
                    ValorDesconto = String.IsNullOrEmpty(txtDesconto.Text) ? 0.0 : Convert.ToDouble(txtDesconto.Text),
                    FreteIncluso = cbxFrete.Checked,
                    ValorFrete = String.IsNullOrEmpty(txtFrete.Text) ? 0.0 : Convert.ToDouble(txtFrete.Text),
                    NomeMoeda = cbxUsarMoeda.Checked ? txtMoeda.Text : "",
                    ValorMoeda = cbxUsarMoeda.Checked && !String.IsNullOrEmpty(txtMoeda.Text) ? Convert.ToDouble(txtCotacaoMoeda.Text) : 0.0,
                    TipoVenda = cbTipoVenda.SelectedIndex,
                    ValorSomarVenda = cbTipoVenda.SelectedIndex > 0 ? Convert.ToDouble(txtMarkup.Text) : 0.0,
                    DadosMarkup = mMkpSelecionado
                };

                FrmOrcamentos_DetalheOrc f = new FrmOrcamentos_DetalheOrc(o, (DataTable)udgvItens.DataSource);

                if(ShowInterface) f.ShowDialog();

                //Salvo os valores para não precisar recalcular novamente (performance issue)
                mMvalorFinal = f._ValorFinal;
                mValorFinalCDesconto = f._ValorFinalComDesconto;
                mDetalhesOrc = f._DadosCustosCalculados;

                txtValor.Text = String.Format("{0:C}", f._ValorFinal);
            }
            else
            {
                if (MessageBox.Show("Alguns campos não foram preenchidos, esses campos são fundamentais para visualizar os detalhes do orçamento. Deseja visualizar os campos?", 
                                    "Campos não preenchidos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    FrmCamposNaoPreenchidos frm = new FrmCamposNaoPreenchidos(mLstCampos);
                    frm.ShowDialog();
                }
            }
        }

        /// <summary>
        ///     Método que verifica se o cliente e o contato existem, caso não,
        /// cadastra eles no banco de dados.
        /// </summary>
        private void CheckCustomerContact()
        {
            //Verifica se o cliente existe.
            Dictionary<String, Object> d = new Dictionary<string, object>();
            if (!Objects.ExisteValorBanco("Clientes", "NomeEmpresa", txtCliente.Text.ToString().Trim()))
            {
                d.Add("@NOME", txtCliente.Text);
                d.Add("@S", DBNull.Value);
                d.Add("@E", DBNull.Value);
                d.Add("@C", DBNull.Value);
                d.Add("@CIDADE", txtCidade.Text);
                d.Add("@UF", txtEstado.Text);
                d.Add("@PAIS", txtPais.Text);
                d.Add("@CNPJ", DBNull.Value);
                d.Add("@NI", DBNull.Value);
                d.Add("@T", DBNull.Value);
                d.Add("@EMAIL", DBNull.Value);
                d.Add("@ISCLIENTE", "1");
                d.Add("@ISFORNECEDOR", "0");
                d.Add("@ISDISTRIBUIDOR", "0");
                d.Add("@USR", Objects.UsuarioAtual.Login);

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_CLIENTE, d);
            }

            //Verifica se o contato existe.
            d = new Dictionary<string, object>();
            if (!Objects.ExisteValorBanco("ContatosClientes", "Nome", txtContato.Text.ToString().Trim()))
            {
                d.Add("@N", txtContato.Text.Substring(0, txtContato.Text.IndexOf(" ")));
                d.Add("@S", txtContato.Text.Substring(txtContato.Text.IndexOf(" ") + 1));
                d.Add("@SETOR", DBNull.Value);
                d.Add("@EMAIL", DBNull.Value);
                d.Add("@TELCOM", DBNull.Value);
                d.Add("@TELCEL", DBNull.Value);
                d.Add("@CLT", txtCliente.Text);
                d.Add("@CIDADE", txtCidade.Text);
                d.Add("@ESTADO", txtEstado.Text);
                d.Add("@PAIS", txtPais.Text);
                d.Add("@USR", Objects.UsuarioAtual.Login);

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_CONTATO_CLIENTE, d);
            }
        }

        private void SaveQuote()
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

            if (Objects.ExisteValorBanco("Orcamentos", "Nome", txtOrca.Text.ToString().Trim()))
            {
                MessageBox.Show($"Já existe uma markup de nome '{txtOrca.Text}'. Escolha um novo nome antes de salvar.", "Referência já existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (!HasAnyEmptyField())
            {
                CheckCustomerContact();

                Dictionary<String, object> d = new Dictionary<string, object>();
                d.Add("@ORC", txtOrca.Text);
                d.Add("@CLT", txtCliente.Text);
                d.Add("@CONT", txtContato.Text);
                d.Add("@CIDADE", txtCidade.Text);
                d.Add("@UF", txtEstado.Text);
                d.Add("@PAIS", txtPais.Text);
                d.Add("@EST", 1);
                d.Add("@VISI", 1);
                d.Add("@IDTIPO", Objects.LstTiposOrcamentos.Where(x => x.Nome.ToUpper().Trim() == cbTipoOrc.SelectedItem.ToString().ToUpper().Trim()).Select(y => y.Id).FirstOrDefault());
                d.Add("@TIPOORC", cbTipoOrc.SelectedItem.ToString());
                d.Add("@IDPAG", Objects.LstPagamentos.Where(x => x.Nome.ToUpper().Trim() == cbMetodos.SelectedItem.ToString().ToUpper().Trim()).Select(y => y.Id.ToString()).FirstOrDefault());
                d.Add("@NOMEPAG", cbMetodos.SelectedItem.ToString());
                //d.Add("@ORCORG", "Orçamento simples");
                d.Add("@ORCORG", 1);
                d.Add("@IDMOEDA", 0);
                d.Add("@MOEDA", txtMoeda.Text);
                d.Add("@VALORMOEDA", Convert.ToDouble(txtCotacaoMoeda.Text));

                //Custos
                d.Add("@HASD", cbxDesconto.Checked);
                d.Add("@VALORDES", Convert.ToDouble(txtDesconto.Text));
                d.Add("@HASF", cbxFrete.Checked);
                d.Add("@VALORFRETE", Convert.ToDouble(txtFrete.Text));
                d.Add("@TIPOVENDA", cbTipoVenda.SelectedIndex);
                d.Add("@VALORSOMAR", Convert.ToDouble(txtMarkup.Text));

                //Caso o usuário tenha se esquecido de recalcular o valor final, eu recalculo automaticamente
                if(mMvalorFinal == 0) this.ShowQuoteDetailed(false);
                d.Add("@VALORTOTAL", mMvalorFinal);
                d.Add("@VALORDESC", mValorFinalCDesconto);
                d.Add("@VENDEDOR", cbVendedor.SelectedItem);
                d.Add("@ORCAMENTISTA", cbOrcamentista.SelectedItem.ToString().ToUpper() == "<SELECIONE>" ? Objects.UsuarioAtual.Login : cbOrcamentista.SelectedItem);
                d.Add("@DTCOTACAO", dtCotacao.DateTime);
                d.Add("@DTEMISS", DBNull.Value);
                d.Add("@DTVALI", dtValidade.DateTime);
                d.Add("@DTPREVENTREGA", dtPrevEntrega.DateTime);
                d.Add("@VER", Convert.ToInt16(txtVersao.Text));
                d.Add("@USR", Objects.UsuarioAtual.Login);

                //Aqui contém o ID do orçamento recém criado.
                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVO_ORCAMENTO, d);

                foreach(var r in udgvItens.Rows)
                {
                    d = new Dictionary<string, object>();
                    d.Add("@IDORC", dt.Rows[0][0].ToString());
                    d.Add("@IDPROD", "");
                    d.Add("@IDTIPO", Convert.ToInt16(r.Cells["IdTipoItensOrcamento"].Value.ToString()));
                    d.Add("@NOME", r.Cells["Item"].Value.ToString());
                    d.Add("@DESCITEM", DBNull.Value);
                    d.Add("@CAMIWIN", r.Cells["CaminhoDoItem"].Value.ToString());
                    d.Add("@GEO", DBNull.Value);
                    d.Add("@DESC", DBNull.Value);
                    d.Add("@CUSUNI", Convert.ToDouble(r.Cells["Custo unitário (R$)"].Value.ToString()));
                    d.Add("@CUSTOT", Convert.ToDouble(r.Cells["Custo total (R$)"].Value.ToString()));
                    d.Add("@DESCONTO", DBNull.Value);
                    d.Add("@QTDE", Convert.ToInt16(r.Cells["Quantidade"].Value.ToString()));
                    d.Add("@OBS", DBNull.Value);
                    d.Add("@TEMPOU", r.Cells["Tempo do processo"].Value.ToString());
                    d.Add("@TEMPOT", r.Cells["Tempo total"].Value.ToString());
                    d.Add("@USR", Objects.UsuarioAtual.Login);

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_ITENS_ORCAMENTO, d);
                }

                //Adiciona histórico
                d = new Dictionary<string, object>();
                d.Add("@ID", Convert.ToInt16(dt.Rows[0][0].ToString()));
                d.Add("@VERSAO", Convert.ToInt16(txtVersao.Text));
                d.Add("@INFO", $"Orçamento de id '{dt.Rows[0][0].ToString()}' criado.");
                d.Add("@USR", Objects.UsuarioAtual.Login);
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ADICIONA_HISTORICO_ORCAMENTOS, d);

                //Salva custos detalhados
                for (int x = 0; x < mDetalhesOrc.Rows.Count; x++)
                {
                    d = new Dictionary<string, object>();
                    d.Add("@ID", Convert.ToInt16(dt.Rows[0][0].ToString()));
                    d.Add("@DESC", mDetalhesOrc.Rows[x]["Descrição do item"].ToString());
                    d.Add("@TIPO", mDetalhesOrc.Rows[x]["Tipo"].ToString());
                    d.Add("@VALOR", mDetalhesOrc.Rows[x]["Valor"].ToString());
                    d.Add("@VER", txtVersao.Text);
                    d.Add("@USR", Objects.UsuarioAtual.Login);
                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_CUSTOS_DETALHADOS_ORCAMENTOS, d);
                }

                MessageBox.Show("Orçamento cadastrado com êxito.", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ubtnSair_Click(new object(), new EventArgs());
            }
            else
            {
                if (MessageBox.Show("Alguns campos não foram preenchidos. Deseja visualizar?", "Campos não preenchidos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    FrmCamposNaoPreenchidos frm = new FrmCamposNaoPreenchidos(mLstCampos);
                    frm.ShowDialog();
                }
            }
        }

        #endregion

        #region Events

        private void btnRefAuto_Orc_Click(object sender, EventArgs e)
        {
            try
            {
                txtOrca.Text = Objects.BuscaNovaReferenciaAutomatica("Orcamentos", "Orcamento");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao obter nova referência automática", "FrmOrcamentos_NewSim", "btnRefAuto_Orc_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNewTipoOrc_Click(object sender, EventArgs e)
        {
            try
            {
                this.CreateNewQuoteType();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar novo tipo de orçamento", "FrmOrcamentos_NewSim", "btnNewTipoOrc_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que serve para salvar o tipo de orçamento selecionado previamente, 
        /// pois a lista reiniciar e era perdido o item selecionado.
        /// </summary>
        private void cbTipoOrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mSelectedQuoteType = cbTipoOrc.SelectedItem.ToString();
        }

        private void cbxDefinirUser_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDefinirUser.Checked)
            {
                label30.Visible = true;
                label10.Enabled = true;
                cbOrcamentista.Enabled = true;
            }
            else
            {
                label30.Visible = false;
                label10.Enabled = false;
                cbOrcamentista.SelectedIndex = 0;
                cbOrcamentista.Enabled = false;
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
                    txtCidade.Text = frm._ClienteSelecionado.Cidade;
                    txtEstado.Text = frm._ClienteSelecionado.Estado;
                    txtPais.Text = frm._ClienteSelecionado.Pais;

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

                txtCliente.Text = "";
                txtCidade.Text = "";
                txtEstado.Text = "";
                txtPais.Text = "";

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
                FrmCidades_Seleciona frm = new FrmCidades_Seleciona();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtCidade.Text = frm._CidadeSelecionada.NomeCidade;
                    txtEstado.Text = frm._CidadeSelecionada.UF;
                    txtPais.Text = frm._CidadeSelecionada.Pais;

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
                txtCidade.Enabled = true;
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

        private void btnAddIt_Click(object sender, EventArgs e)
        {
            cms.Show(btnAddIt, new Point(0, btnAddIt.Height));
        }

        /// <summary>
        ///     Evento que calcula os custos e tempos totais de cada iten em orçamento.
        /// </summary>
        private void udgvItens_AfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e)
        {
            try
            {
                e.Row.Cells["Tempo total"].Value = CustomTimes.MultiplyTimes(e.Row.Cells["Tempo do processo"].Value.ToString(), Convert.ToInt16(e.Row.Cells["Quantidade"].Value.ToString()));
                e.Row.Cells["Custo total (R$)"].Value = Convert.ToDouble(e.Row.Cells["Custo unitário (R$)"].Value.ToString()) * Convert.ToInt16(e.Row.Cells["Quantidade"].Value.ToString());

                //Prova real.
                //var t = CustomTimes.SumTimes("01:45:00", "01:45:00", "01:45:00", "01:45:00", "01:45:00", "01:45:00", "01:45:00", "01:45:00", "01:45:00", "01:45:00");
                //int x = 0;
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao calcular os valores dos itens em orçamentos", "FrmOrcamentos_NewSim", "udgvItens_AfterRowUpdate", "", "", e_TipoErroEx.Erro, ex);
            }
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

        private void cbxUsarMoeda_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarMoeda.Checked)
            {
                //Mpeda
                label16.Enabled = true;
                txtMoeda.Enabled = true;
                label21.Visible = true;
                btnMoeda.Enabled = true;

                //Cotação
                label17.Enabled = true;
                txtCotacaoMoeda.Enabled = true;
                label22.Visible = true;
                btnCotacaoMoeda.Enabled = true;
            }
            else
            {
                //Mpeda
                label16.Enabled = false;
                txtMoeda.Enabled = false;
                label21.Visible = false;
                btnMoeda.Enabled = false;

                //Cotação
                label17.Enabled = false;
                txtCotacaoMoeda.Enabled = false;
                label22.Visible = false;
                btnCotacaoMoeda.Enabled = false;
            }
        }

        private void cbxDesconto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDesconto.Checked)
            {
                label14.Enabled = true;
                txtDesconto.Enabled = true;
                label31.Visible = true;
            }
            else
            {
                label14.Enabled = false;
                txtDesconto.Enabled = false;
                label31.Visible = false;
            }
        }

        private void cbxFrete_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxFrete.Checked)
            {
                label18.Enabled = true;
                txtFrete.Enabled = true;
                label29.Visible = true;
            }
            else
            {
                label18.Enabled = false;
                txtFrete.Enabled = false;
                label29.Visible = false;
            }
        }

        private void cbTipoVenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbTipoVenda.SelectedIndex)
            {
                case 0:
                    label19.Enabled = false;
                    txtMarkup.Enabled = false;
                    label28.Visible = false;
                    btnMarkup.Visible = false;
                    btnNewMarkup.Visible = false;
                    break;
                case 1:
                    label19.Enabled = true;
                    txtMarkup.Enabled = true;
                    label28.Visible = true;
                    btnMarkup.Visible = true;
                    btnNewMarkup.Visible = true;
                    break;
                case 2:
                    label19.Enabled = true;
                    txtMarkup.Enabled = true;
                    label28.Visible = true;
                    btnMarkup.Visible = false;
                    btnNewMarkup.Visible = false;
                    break;
            }
        }

        private void btnNewMarkup_Click(object sender, EventArgs e)
        {
            try
            {
                FrmOrcamentos_MarkupNew f = new FrmOrcamentos_MarkupNew();
                f.ShowDialog();

                if(f._Markup != null)
                {
                    mMkpSelecionado = f._Markup;
                    txtMarkup.Text = mMkpSelecionado.MarkupUp.ToString();
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao criar um novo markup", "FrmOrcamentos_NewSim/FrmOrcamentos_MarkupNew", "btnNewMarkup_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnMarkup_Click(object sender, EventArgs e)
        {
            try
            {
                this.CreateNewMarkup();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao criar um novo markup", "FrmOrcamentos_NewSim/FrmOrcamentos_MarkupNew", "btnNewMarkup_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowQuoteDetailed(false);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao recalcular os detalhes do orçamento", "FrmOrcamentos_NewSim", "btnCalc_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDetalhes_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowQuoteDetailed();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao calcular os detalhes do orçamento", "FrmOrcamentos_NewSim", "btnDetalhes_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void ubtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();

            //Remove o controle para permitir o sistema consultar os dados.
            Objects.FechaTelaPendenteInterface(this.GetHashCode().ToString());
            Objects.FormularioPrincipal.Controls.Remove(this);
        }

        private void ubtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveQuote();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar novo orçamento", "FrmOrcamentos_NewSim", "btnSave_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Context menu stip over the button.

        private void tsmPeca_Click(object sender, EventArgs e)
        {
            try
            {
               this.SelecionaPeca();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar peça no orçamento", "FrmOrcamentos_NewSim", "tsmPeca_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmTool_Click(object sender, EventArgs e)
        {

        }

        private void tsmProd_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProdutos_Seleciona f = new FrmProdutos_Seleciona();
                f.ShowDialog();

                if (f._Produto != null)
                {
                    //                  Nome,           Custo unitário,           Custo total ,          Quantidade,           Caminho, Tempo unitario, Tempo total, IdTipoItensOrcamento
                    mProdutos.Rows.Add(f._Produto.Nome, f._Produto.CustoUnitario, f._Produto.CustoTotal, f._Produto.Quantidade, "", "00:00:00", "00:00:00", f._Produto.Tipo);
                    udgvItens.DataSource = mProdutos;
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar itens no orçamento", "FrmOrcamentos_NewSim", "tsmProd_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmItem_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}