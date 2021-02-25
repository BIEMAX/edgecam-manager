using Infragistics.Win.UltraWinExplorerBar;
using ImagedComboBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    public partial class FrmOrcamentos : Form
    {
        #region Variáveis globais

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados da grade dos dados.
        /// </summary>
        private Exporter mExporter;

        /// <summary>
        ///     Variável que informa se o sistema possuí acesso à internet ou não.
        /// </summary>
        private Boolean mPossuiConexaoInternet = false;

        #endregion

        #region Instância dos objetos da classe

        public FrmOrcamentos()
        {
            InitializeComponent();

            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que inicializa a construção da interface e de alguns controles
        /// para pesquisas e/ou criação de dados.
        /// </summary>
        private void InicializaValoresDefault()
        {
            lblDiaAtual.Text += " - " + DateTime.Now.ToString("dd/MM/yyyy");

            //Verifica se o sistema possuí conexão com a internet.
            mPossuiConexaoInternet = Utilities.CheckInternetConnection();

            if (!mPossuiConexaoInternet)
                lblNoInternetCnn.Visible = true;
            else lblNoInternetCnn.Visible = false;

            CriaEstruturaCotacoesMoedas();

            //Remove a ordenação anterior que o usuário pode ter feito (fica salvo internamente no sistema).
            UltraGridOptions udgv_orc = new UltraGridOptions(udgv, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                         Imagens_NewLookInterface.ordenar_crescente_16,
                                                                         Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                         Imagens_NewLookInterface.remover_deletar,
                                                                         Imagens_NewLookInterface.agrupamento_16);
            //Objects.DefineColorThemeInterface(this);

            //Carrega as listas nas combo boxes
            icbxEstado.Items.Add(new ComboBoxItem("(Todos)"));
            icbxEstado.Items.Add(new ComboBoxItem("Pendente de planejamento", Properties.Resources.White));
            icbxEstado.Items.Add(new ComboBoxItem("Em andamento (Iniciado)", Properties.Resources.Green));
            icbxEstado.Items.Add(new ComboBoxItem("Enviado para o cliente", Properties.Resources.Orange));
            icbxEstado.Items.Add(new ComboBoxItem("Aprovado", Properties.Resources.Global));
            icbxEstado.Items.Add(new ComboBoxItem("Cancelado", Properties.Resources.Cancel));
            icbxEstado.Items.Add(new ComboBoxItem("Atrasado", Properties.Resources.Red));
            icbxEstado.SelectedIndex = 0;//Só trago as ordens pendentes de planejamento quando o usuário for consultar.

            icbxTiposOrc.Items.Add(new ComboBoxItem("(Todos)"));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Estoque", Edgecam_Manager.Imagens_NewLookInterface.produto_inventario));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Ferramentas", Edgecam_Manager.Imagens_NewLookInterface.ferramentas));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Serviços (terceirização)", Edgecam_Manager.Imagens_NewLookInterface.servico_manutencao_16));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Serviços de usinagem", Edgecam_Manager.Imagens_NewLookInterface.servico_suporte));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Produção em lote", Edgecam_Manager.Imagens_NewLookInterface.producao_fabrica));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Cotação", Edgecam_Manager.Imagens_NewLookInterface.cambio_moeda_cotacao));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Implantação", Edgecam_Manager.Imagens_NewLookInterface.instalacao_software));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Consultoria", Edgecam_Manager.Imagens_NewLookInterface.consultoria_perguntar));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Desenvolvimento de protótipos", Imagens_NewLookInterface.development_prototype_16));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Compra", Imagens_NewLookInterface.compras_carrinho_16));
            //icbxTiposOrc.Items.Add(new ComboBoxItem("Outros", Properties.Resources.help));

            foreach (TipoOrcamento t in Objects.LstTiposOrcamentos.OrderBy(x => x.Id)) icbxTiposOrc.Items.Add(new ComboBoxItem(t.Nome));
            icbxTiposOrc.SelectedIndex = 0;

            //Desabilita os botões da interface
            btnEdit.Enabled = false;
            btnStart.Enabled = false;
            btnCancelar.Enabled = false;
            btnView.Enabled = false;

            //Desativa o controle de data
            dtEmissao.Enabled = false;

            //Carrega lista de clientes
            cbClientes.Items.Add("(Todos)");
            if (Objects.LstClientes != null)
            {
                cbClientes.Items.AddRange(Objects.LstClientes.Where(x => x.NomeEmpresa != "").Select(x => x.NomeEmpresa).ToArray());
            }
            cbClientes.SelectedIndex = 0;

            mExporter = new Exporter(udgv, btnExportar, "Orçamentos");

            this.BloqueiaAcoesDireitoMouse();

            //Filters
            customFilterControl1._GroupBox = ugbFiltros;
            customFilterControl1._Interface = "Orcamentos";
            customFilterControl1._Module = "Orcamentos";

            //Reports
            customReportControl1._Interface = nameof(FrmOrcamentos);
            customReportControl1._Module = "Orcamentos";
            customReportControl1._UltraGrid = udgv;
        }

        /// <summary>
        ///     Método que bloqueia algumas ações do grid (Editar, visualizar, etc), tudo
        ///  o que precisar de dados para trabalhar, ele bloqueia.
        /// </summary>
        private void BloqueiaAcoesDireitoMouse()
        {
            btnEdit.Enabled = false;
            btnStart.Enabled = false;
            btnCancelar.Enabled = false;
            btnView.Enabled = false;
        }

        /// <summary>
        ///     Método que desbloqueia algumas ações do grid (Editar, visualizar, etc), tudo
        ///  o que precisar de dados para trabalhar, ele desbloqueia.
        /// </summary>
        private void DesbloqueiaAcoesDireitoMouse()
        {
            btnEdit.Enabled = true;
            btnStart.Enabled = true;
            btnCancelar.Enabled = true;
            btnView.Enabled = true;
        }

        /// <summary>
        ///     Método que cria a estrutura dentro do 'UltraExploreBar'.
        /// </summary>
        private void CriaEstruturaCotacoesMoedas()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_COTACOES_ATIVAS);

            if (dt != null && dt.Rows.Count > 0)
            {
                String hojeAtt = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

                //Adiciona a opção do usuário verificar o preço das criptomoedas.
                UltraExplorerBarGroup lstGrupoCriptmoeda = new UltraExplorerBarGroup();
                lstGrupoCriptmoeda.Settings.ShowExpansionIndicator = Infragistics.Win.DefaultableBoolean.False;
                lstGrupoCriptmoeda.Text = "Criptomoedas";

                //Se não tiver internet, eu não permito que o item fique ativo.
                if (!mPossuiConexaoInternet) lstGrupoCriptmoeda.Enabled = false;

                ueb.Groups.Add(lstGrupoCriptmoeda);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    //Apenas Salvo o nome da moeda para facilitar.
                    String nomeMoeda = dt.Rows[x]["MoedaPesquisa"].ToString();

                    //Esse irá conter uma lista de items (agrupadores) à serem adicionados na interface.
                    UltraExplorerBarGroup lstGrupos = new UltraExplorerBarGroup();
                    lstGrupos.Text = nomeMoeda;

                    //Contém o valor da cotação atual (do dia)
                    UltraExplorerBarItem i = new UltraExplorerBarItem();

                    //Adicionei essa variável, pois se a moeda já foi pesquisada, eu trago do banco o valor
                    //e a data/hora da atualização.
                    String cotacaoHoje = BuscaCotacaoMoeda(nomeMoeda);
                    String dtAtt = cotacaoHoje.Contains("|") ? cotacaoHoje.Split(new char[] { '|' })[1] : "";

                    i.Text = cotacaoHoje.Contains("|") ? cotacaoHoje.Split(new char[] { '|' })[0] : cotacaoHoje;
                    i.Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.dinheiro_16;
                    lstGrupos.Items.Add(i);

                    //Contém a hora da consulta do valor da cotação.
                    UltraExplorerBarItem i2 = new UltraExplorerBarItem();
                    i2.Text = String.Format("Última atualização: '{0}'", !String.IsNullOrEmpty(dtAtt) ? dtAtt : hojeAtt);
                    i2.Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.relogio_despertador_16;
                    lstGrupos.Items.Add(i2);

                    //Contém a opção para consultar todo o histórico.
                    UltraExplorerBarItem i3 = new UltraExplorerBarItem();
                    i3.Text = String.Format("Histórico da moeda '{0}'", nomeMoeda);
                    i3.Settings.AppearancesSmall.Appearance.Image = Edgecam_Manager.Properties.Resources.table;
                    i3.Settings.AppearancesSmall.Appearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                    i3.Settings.AppearancesSmall.Appearance.ForeColor = Color.Blue;
                    lstGrupos.Items.Add(i3);

                    //Contém a opção para consultar todo o histórico.
                    UltraExplorerBarItem i4 = new UltraExplorerBarItem();
                    i4.Text = String.Format("Atualizar o valor da cotação da moeda '{0}'", nomeMoeda);
                    i4.Settings.AppearancesSmall.Appearance.Image = Edgecam_Manager.Properties.Resources.refresh;
                    i4.Settings.AppearancesSmall.Appearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
                    i4.Settings.AppearancesSmall.Appearance.ForeColor = Color.Blue;
                    lstGrupos.Items.Add(i4);

                    ueb.Groups.Add(lstGrupos);
                }
            }
        }

        /// <summary>
        ///     Método que busca as cotações ativas no banco de dados intermediário
        /// e as adiciona na interface para o usuário.
        /// </summary>
        /// <param name="Moeda">Nome da moeda a ser pesquisada.</param>
        private String BuscaCotacaoMoeda(String Moeda)
        {
            try
            {
                System.Net.WebClient client;

                //  Primeiro verifica se já foi pesquisada a cotação do dia que está armazenada
                //no banco de dados, caso ela exista, retorna ela.
                String cotacaoSalvaBanco = JaConsultaramCotacaoAtual(Moeda);

                if (!String.IsNullOrEmpty(cotacaoSalvaBanco))
                    return cotacaoSalvaBanco;
                else
                {
                    //O sistema só consulta/obtém a cotação atual se tiver internet.
                    if (mPossuiConexaoInternet)
                    {
                        client = new System.Net.WebClient();
                        System.IO.Stream data = client.OpenRead(String.Format("https://www.google.com.br/search?client=opera&q={0}+hoje&sourceid=opera&ie=UTF-8&oe=UTF-8", Moeda));
                        //System.IO.Stream data = client.OpenRead(String.Format("https://dolarhoje.com/{0}", Moeda.ToUpper().Contains("DÓLAR") ? "" : Moeda.Replace(" ", "") + "-hoje"));
                        System.IO.StreamReader reader = new System.IO.StreamReader(data);
                        string s = reader.ReadToEnd();

                        data.Close();
                        reader.Close();

                        //Contém o valor da cotação do dia de hoje de acordo com o google.
                        //String valorCotacaoAtual = CustomStrings.GetTextBetween(s, "<div class=\"J7UKTe\">", "</div>");
                        String valorCotacaoAtual = CustomStrings.GetTextBetween(s, "<div class=\"BNeawe iBp4i AP7Wnd\">", "</div></div></div></div></div><div class=").Replace(".", ",");
                        //String valorCotacaoAtual = CustomStrings.GetTextBetween(s, "<input type=\"text\" id=\"nacional\" value=\"", "\"/></span><span class=\"optional\">").Replace(".", ",");

                        //Criei essa condição, pois em determinadas situações, o sistema
                        //não consegue obter o valor da moeda do dia (por alguma razão
                        //desconhecida).
                        if (String.IsNullOrEmpty(valorCotacaoAtual))
                        {
                            valorCotacaoAtual = String.Format("1 {0} = ", Moeda);
                            valorCotacaoAtual += CustomStrings.GetTextBetween(s, "<div><div><span><div class=", "</div></span></div><div><span><div").Split(new char[] { '>' })[1].Replace(".", ",");
                        }

                        valorCotacaoAtual = String.Format("1 {0} = {1} Real Brasileiro", Moeda, valorCotacaoAtual.ToUpper().Replace("REAL BRASILEIRO", "").Trim());

                        SalvaCotacaoDiaria(Moeda, valorCotacaoAtual);
                        return valorCotacaoAtual;
                    }
                    else
                    {
                        //Se a primeira moeda já não tiver internet, então já informo o usuário.
                        if (!mPossuiConexaoInternet)
                        {
                            String ultValorSalvo = ConsultaUltimaCotacaoAtual(Moeda);

                            //Altera a formatação para o usuário estar ciente do problema
                            lblDiaAtual.Text = String.Format("Cotações do dia {0}", Convert.ToDateTime(ultValorSalvo.Split(new char[] { '|' })[1]).ToString("dd/MM/yyyy"));
                            lblDiaAtual.ForeColor = Color.Red;

                            return ultValorSalvo;
                        }
                        else
                        {
                            return ConsultaUltimaCotacaoAtual(Moeda);
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        ///     Verifica no banco de dados se já existe uma cotação atual do dia.
        /// </summary>
        /// <param name="Moeda">Moeda/Cotação à ser verificada.</param>
        /// <returns>String contendo o valor do banco ou vazio caso o valor ainda não exista.</returns>
        private String JaConsultaramCotacaoAtual(String Moeda)
        {
            try
            {
                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_COTACAO_DE_HOJE, new Dictionary<string, object>() { { "@MOEDA", Moeda }, { "@DT", DateTime.Now.ToString("yyyy-MM-dd") } });

                if (dt != null && dt.Rows.Count > 0)
                {
                    return String.Format("{0}|{1}", dt.Rows[0]["ValorAtual"].ToString(), dt.Rows[0]["DtConsulta"].ToString());
                }
                else return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        ///     Método que consulta a última cotação salva no banco de dados do manager com
        /// base na data, ou seja, o valor da última data salva.
        /// </summary>
        /// <param name="Moeda">Moeda/Cotação à ser verificada.</param>
        /// <returns>String contendo o valor do banco ou vazio caso o valor ainda não exita.</returns>
        private String ConsultaUltimaCotacaoAtual(String Moeda)
        {
            try
            {
                return Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ULTIMA_DATA_COTACAO_MOEDA, 
                       new Dictionary<string,object>() { { "@MOEDA", Moeda}}).AsEnumerable().Select(r => String.Format("{0}|{1}", r.ItemArray[0].ToString(), r.ItemArray[1].ToString())).FirstOrDefault();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        ///     Salva a cotação do dia no banco de dados.
        /// </summary>
        /// <param name="Moeda">Nome da cotação/moeda.</param>
        /// <param name="Valor">Valor da cotação/moeda do dia de hoje.</param>
        private void SalvaCotacaoDiaria(String Moeda, String Valor)
        {
            if (!String.IsNullOrEmpty(Moeda) && !String.IsNullOrEmpty(Valor))
            {
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.SALVA_COTACAO_DE_HOJE, new Dictionary<string, object>() 
                { 
                    { "@MOEDA", Moeda },
                    { "@VALOR", Valor },
                    { "@DT", DateTime.Now },
                    { "@USR", Objects.UsuarioAtual.Login }
                });
            }
        }

        /// <summary>
        ///     Método que limpa a cotação de uma moeda especificada no dia de hoje (caso exista)
        /// e já consulta o novo valor da moeda.
        /// </summary>
        /// <param name="Moeda">MOeda da cotação</param>
        /// <returns>String contendo o valor da cotação e a hora da atualização.</returns>
        private String AtualizaCotacaoBanco(String Moeda)
        {
            try
            {
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_COTACAO_DIA.Replace("@DT", DateTime.Now.ToString("yyyy-MM-dd")).Replace("@MOEDA", Moeda));
                return BuscaCotacaoMoeda(Moeda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Método que consulta os orçamentos atuais do banco de dados intermediário.
        /// </summary>
        private void QueryQuotes()
        {
            Cursor = Cursors.WaitCursor;

            //ECMGR-250
            Objects.LimpaOrdenacaoColunasGrid(udgv);

            udgv.DataSource = SQLQueries.Consulta_Orcamentos(txtOrcamento.Text, cbClientes.Text, cbxUsarData.Checked == true ? dtEmissao.DateTime.ToString("yyyy-MM-dd") : "", icbxEstado.SelectedIndex, icbxTiposOrc.SelectedItem.ToString());

            //Habilita os botões de editar e excluir.
            if (udgv.Rows.Count > 0)
            {
                DesbloqueiaAcoesDireitoMouse();
                udgv.ActiveRow = udgv.ActiveRowScrollRegion.FirstRow;
            }
            else BloqueiaAcoesDireitoMouse();

            Cursor = Cursors.Arrow;
        }

        private void NewSimpleQuote()
        {
            FrmOrcamentos_NewSim frm;
            Objects.ImplementaNovoFormTela(frm = new FrmOrcamentos_NewSim(), true);

            Objects.SetaUltimaTuplaSelecionada(udgv);
        }

        /// <summary>
        ///     Método que cria um novo orçamento
        /// </summary>
        private void NewDetailedQuote()
        {
            FrmOrcamentos_NewDet frm;
            Objects.ImplementaNovoFormTela(frm = new FrmOrcamentos_NewDet(), true);

            ////  Fico dentro do loop enquanto o formulário principal ainda tiver
            ////uma instância do formulário de nova interação.
            //do
            //{
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();
            //}
            //while (Objects.FormularioPrincipal.Controls.Contains(frm));

            //Consulta as ordens de produção novamente para trazer a ordem recém criada.
            //btnPesquisar_Click(new object(), new EventArgs());

            Objects.SetaUltimaTuplaSelecionada(udgv);
        }

        /// <summary>
        ///     Método responsável por editar um orçamento.
        /// </summary>
        /// <param name="View">True caso seja apenas para visualização.</param>
        private void EditQuote(Boolean ViewQuote)
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                Orcamento o = new Orcamento();

                o.Id = Convert.ToInt16(q.Cells["id"].OriginalValue.ToString());
                o.CodOrca = q.Cells["Orçamento"].OriginalValue.ToString();
                o.Cliente = q.Cells["Cliente"].OriginalValue.ToString();
                o.Contato = q.Cells["Contato"].OriginalValue.ToString();
                o.Cidade = q.Cells["Cidade"].OriginalValue.ToString();
                o.UF = q.Cells["Unidade da federação"].OriginalValue.ToString();
                o.Pais = q.Cells["País"].OriginalValue.ToString();
                o.TipoVis = Convert.ToInt16(q.Cells["Visibilidade_db"].OriginalValue.ToString());
                o.IdTipoOrcamento = Convert.ToInt16(q.Cells["IdTipoOrcamento"].OriginalValue.ToString());
                o.TipoOrcamento = q.Cells["Tipo de orçamento"].OriginalValue.ToString();
                o.IdTipoPag = Convert.ToInt16(q.Cells["IdTipoPag"].OriginalValue.ToString());
                o.NomePag = q.Cells["Forma de pagamento"].OriginalValue.ToString();
                o.OrcOrigem = Convert.ToInt16(q.Cells["OrcOrigem_db"].OriginalValue.ToString());
                o.IdMoeda = Convert.ToInt16(q.Cells["IdMoeda"].OriginalValue.ToString());
                o.NomeMoeda = q.Cells["Moeda utilizada"].OriginalValue.ToString();
                o.ValorMoeda = Convert.ToDouble(q.Cells["Valor da cotação"].OriginalValue.ToString());
                o.PossuiDesconto = Convert.ToBoolean(q.Cells["Tem desconto"].OriginalValue.ToString());
                o.ValorDesconto = Convert.ToDouble(q.Cells["Valor de desconto"].OriginalValue.ToString());
                o.FreteIncluso = Convert.ToBoolean(q.Cells["Frete incluso"].OriginalValue.ToString());
                o.ValorFrete = Convert.ToDouble(q.Cells["Valor frete"].OriginalValue.ToString());
                o.TipoVenda = Convert.ToInt16(q.Cells["TipoVenda"].OriginalValue.ToString());                
                o.ValorSomarVenda = Convert.ToDouble(q.Cells["Margem de lucro"].OriginalValue.ToString());
                o.ValorTotal = Convert.ToDouble(q.Cells["Valor total"].OriginalValue.ToString());
                o.ValorTotalComDesconto = Convert.ToDouble(q.Cells["Valor com desconto"].OriginalValue.ToString());
                o.Vendedor = q.Cells["Solicitante/Vendedor"].OriginalValue.ToString();
                o.Orcamentista = q.Cells["Responsável"].OriginalValue.ToString();
                o.DtCotacao = !String.IsNullOrEmpty(q.Cells["Data de cotação"].OriginalValue.ToString()) ? Convert.ToDateTime(q.Cells["Data de cotação"].OriginalValue.ToString()) : new DateTime(0001, 01, 01);
                o.DtEmissao = !String.IsNullOrEmpty(q.Cells["Data de emissão"].OriginalValue.ToString()) ? Convert.ToDateTime(q.Cells["Data de emissão"].OriginalValue.ToString()) : new DateTime(0001, 01, 01);
                o.DtValidade = !String.IsNullOrEmpty(q.Cells["Valido até"].OriginalValue.ToString()) ? Convert.ToDateTime(q.Cells["Valido até"].OriginalValue.ToString()) : new DateTime(0001, 01, 01);
                o.DtPrevEntrega = !String.IsNullOrEmpty(q.Cells["Data prevista para entrega"].OriginalValue.ToString()) ? Convert.ToDateTime(q.Cells["Data prevista para entrega"].OriginalValue.ToString()) : new DateTime(0001, 01, 01);
                o.Versao = Convert.ToInt16(q.Cells["Versão"].OriginalValue.ToString());
                o.Ativo = Convert.ToBoolean(q.Cells["Ativo_db"].OriginalValue.ToString());
                o.UsuarioCriacao = q.Cells["Cadastrado por"].OriginalValue.ToString();
                o.DtCriacao = Convert.ToDateTime(q.Cells["Data de cadastro"].OriginalValue.ToString());
                o.UsuarioUltMod = q.Cells["Ultima alteração por"].OriginalValue.ToString();
                o.DtUltimaMod = Convert.ToDateTime(q.Cells["Data de última mod."].OriginalValue.ToString());

                FrmOrcamentos_NewSim frm;
                Objects.ImplementaNovoFormTela(frm = new FrmOrcamentos_NewSim(o, ViewQuote));

                //  Fico dentro do loop enquanto o formulário principal ainda tiver
                //uma instância do formulário de nova interação.
                do
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
                while (Objects.FormularioPrincipal.Controls.Contains(frm));

                //Consulta as ordens de produção novamente para trazer a ordem recém criada.
                btnPesquisar_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Cancela uma ordem de produção.
        /// </summary>
        private void CancelaOrcamento()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                if (MessageBox.Show("Deseja realmente cancelar o orçamento?", "Cancelar orçamento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    FrmCancelar f = new FrmCancelar("orçamento", q.Cells["Orçamento"].OriginalValue.ToString());
                    f.ShowDialog();

                    if (!String.IsNullOrEmpty(f._Razao))
                    {
                        Dictionary<String, Object> d = new Dictionary<string, object>();
                        d.Add("@ID", q.Cells["id"].OriginalValue.ToString());
                        d.Add("@VERSAO", q.Cells["Versão"].OriginalValue.ToString());
                        d.Add("@INFO", $"Usuário cancelou o orçamento pela seguinte razão/motivo: '{f._Razao}'");
                        d.Add("@USR", Objects.UsuarioAtual.Login);
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ADICIONA_HISTORICO_ORCAMENTOS, d);
                        Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CANCELA_ORCAMENTO_POR_ID, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });

                        MessageBox.Show("Orçamento cancelado com êxito", "Orçamento cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Objects.CadastraNovoLog(false, $"Usuário '{Objects.UsuarioAtual.Login}' cancelou o orçamento '{q.Cells["id"].OriginalValue.ToString()}'", "FrmOrcamentos", "btnCancelar_Click", "", "", e_TipoErroEx.Informacao);

                        btnPesquisar_Click(new object(), new EventArgs());
                    }
                }
                else return;
            }
        }

        private void ConsultaHistoricoCotacoes(String Moeda)
        {
            FrmOrcamentos_HistoricoCotacoes frm = new FrmOrcamentos_HistoricoCotacoes(Moeda);
            frm.ShowDialog();
        }

        /// <summary>
        ///     Método que executa uma ação de acordo com o item na árvore selecionado.
        /// </summary>
        /// <param name="ItemClicado">Item que o usuário clicou.</param>
        /// <param name="GrpParente"></param>
        private void ExecutaAcaoDaArvore(String ItemClicado, UltraExplorerBarGroup GrpParente = null)
        {
            if (ItemClicado == "CRIPTOMOEDAS")
                new FrmOrcamentos_Criptomoedas().ShowDialog();
            else if (ItemClicado.Contains("HISTÓRICO"))
            {
                String moeda = ItemClicado.Replace("HISTÓRICO DA MOEDA '", "").Replace("'", "");
                FrmOrcamentos_HistoricoCotacoes frm = new FrmOrcamentos_HistoricoCotacoes(moeda);
                frm.ShowDialog();
            }
            else if (ItemClicado.Contains("ATUALIZAR"))
            {
                String moeda = ItemClicado.Replace("ATUALIZAR O VALOR DA COTAÇÃO DA MOEDA '", "").Replace("'", "").ToLower();

                String valorAtt = AtualizaCotacaoBanco(moeda);

                GrpParente.Items[0].Text = valorAtt;
                GrpParente.Items[1].Text = String.Format("Última atualização: '{0}'", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

                //Atualiza o ultra explore bar (graficamente para o usuário).
                ueb.Refresh();
            }
        }

        #endregion

        #region Eventos

        private void txtOrcamento_Click(object sender, EventArgs e)
        {
            txtOrcamento.Text = "";
        }

        private void btnExpandePainel_Click(object sender, EventArgs e)
        {
            //False siginifica que ele está visível, true para invisível.
            if (!splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = true;
                btnExpandePainel.Image = Properties.Resources.arrow_direita;
            }
            //Se estiver invisível, mostra ele na inteface.
            else if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
                btnExpandePainel.Image = Properties.Resources.arrow_esquerda;
            }
        }

        private void cbxUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarData.Checked) dtEmissao.Enabled = true;
            else dtEmissao.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                this.QueryQuotes();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar contular os orçamentos", "FrmOrcamentos", "btnPesquisar_Click", "", "Consultas_EcMgr.CONSULTA_ORÇAMENTOS", e_TipoErroEx.Erro, ex);
            }
            finally { Cursor = Cursors.Arrow; }
        }

        private void udgv_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                this.EditQuote(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao visualizar o orçamento", "FrmOrcamentos", "udgv_DoubleClickRow", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void cms_Opening(object sender, CancelEventArgs e)
        {
            if (udgv.Rows != null && udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                switch (q.Cells["EstadoOrcamento_db"].Value.ToString())
                {
                    case "1":
                        btnNewSim.Enabled = true;
                        btnNewDet.Enabled = true;
                        btnNewTec.Enabled = true;
                        btnEdit.Enabled = true;
                        btnStart.Enabled = true;
                        btnEnviarClt.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnView.Enabled = true;
                        btnViewDetailedCosts.Enabled = true;
                        break;
                    case "2":
                        btnNewSim.Enabled = true;
                        btnNewDet.Enabled = true;
                        btnNewTec.Enabled = true;
                        btnEdit.Enabled = true;
                        btnStart.Enabled = false;
                        btnEnviarClt.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnView.Enabled = true;
                        btnViewDetailedCosts.Enabled = true;
                        break;
                    case "3":
                        btnNewSim.Enabled = true;
                        btnNewDet.Enabled = true;
                        btnNewTec.Enabled = true;
                        btnEdit.Enabled = false;
                        btnStart.Enabled = false;
                        btnEnviarClt.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnView.Enabled = true;
                        btnViewDetailedCosts.Enabled = true;
                        break;
                    case "4":
                        btnNewSim.Enabled = true;
                        btnNewDet.Enabled = true;
                        btnNewTec.Enabled = true;
                        btnEdit.Enabled = false;
                        btnStart.Enabled = false;
                        btnEnviarClt.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnView.Enabled = true;
                        btnViewDetailedCosts.Enabled = true;
                        break;
                    case "5":
                        btnNewSim.Enabled = true;
                        btnNewDet.Enabled = true;
                        btnNewTec.Enabled = true;
                        btnEdit.Enabled = false;
                        btnStart.Enabled = false;
                        btnEnviarClt.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnView.Enabled = true;
                        btnViewDetailedCosts.Enabled = true;
                        break;
                    case "6":
                        btnNewSim.Enabled = true;
                        btnNewDet.Enabled = true;
                        btnNewTec.Enabled = true;
                        btnEdit.Enabled = false;
                        btnStart.Enabled = false;
                        btnEnviarClt.Enabled = false;
                        btnCancelar.Enabled = true;
                        btnView.Enabled = true;
                        btnViewDetailedCosts.Enabled = true;
                        break;
                    default: break;
                }
            }
            else
            {
                btnNewSim.Enabled = true;
                btnNewDet.Enabled = true;
                btnNewTec.Enabled = true;
                btnEdit.Enabled = false;
                btnStart.Enabled = false;
                btnEnviarClt.Enabled = false;
                btnCancelar.Enabled = false;
                btnView.Enabled = false;
                btnViewDetailedCosts.Enabled = false;
            }
        }

        private void btnNewSim_Click(object sender, EventArgs e)
        {
            try
            {
                this.NewSimpleQuote();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar um novo orçamento", "FrmOrcamentos", "btnNew_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNewDet_Click(object sender, EventArgs e)
        {
            try
            {
                this.NewDetailedQuote();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar um novo orçamento", "FrmOrcamentos", "btnNew_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNewTec_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.EditQuote(false);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao edtiar o orçamento", "FrmOrcamentos", "btn_Edit_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void btnEnviarClt_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelaOrcamento();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar cancelar uma ordem", "FrmOrdens", "btnDelete_Click", "<None>", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                EditQuote(true);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao visualizar o orçamento", "FrmOrcamentos", "btnView_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnViewDetailedCosts_Click(object sender, EventArgs e)
        {
            try
            {
                if (udgv.Selected.Rows.Count > 1)
                    Messages.Msg015();
                else if (udgv.Rows.Count > 0)
                {
                    var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                    FrmOrcamentos_DetalheOrc f = new FrmOrcamentos_DetalheOrc(q.Cells["id"].OriginalValue.ToString());
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar custos detalhados do orçamento", "FrmOrcamentos", "btnViewDetailedCosts_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Quando o usuário clicar sobre a árvore, dependendo da opção, eu executo
        /// uma determinada ação.
        /// </summary>
        private void ueb_GroupClick(object sender, GroupEventArgs e)
        {
            try
            {
                //if(e.Group.Text.ToUpper())
                ExecutaAcaoDaArvore(e.Group.Text.ToUpper());
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, String.Format("Erro ao executar a ação '{0}'", e.Group.Text), "FrmOrcamentos", "ueb_GroupClick", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Quando o usuário clicar sobre a árvore, dependendo da opção, eu executo
        /// uma determinada ação.
        /// </summary>
        private void ueb_ItemClick(object sender, ItemEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ExecutaAcaoDaArvore(e.Item.Text.ToString().ToUpper(), e.Item.Group);
                Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, String.Format("Erro ao executar a ação '{0}'", e.Item.Text), "FrmOrcamentos", "ueb_ItemClick", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Abre o 'Context menu strip' e permite o usuário exportar os dados
        /// da grade de dados em arquivos definidos pelo mesmo.
        /// </summary>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            mExporter.MostrarCms();
        }

        /// <summary>
        ///     Evento sobre o linked label que permite o usuário consultar
        /// o histórico da moeda desejada.
        /// </summary>
        private void lblLinkedHistoricoCotacao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ConsultaHistoricoCotacoes(((LinkLabel)sender).Text.Replace("Ver histórico de cotações de ", "").Trim());
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar histórico de cotações", "FrmOrcamentos", "lblLinkedHistoricoCotacao_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        private void FrmOrcamentos_Load(object sender, EventArgs e)
        {

        }
    }
}