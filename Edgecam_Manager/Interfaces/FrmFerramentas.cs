using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImagedComboBox;
using Edgecam_Manager;
using Infragistics.Win.UltraWinGrid;

namespace Edgecam_Manager
{
    internal partial class FrmFerramentas : Form
    {

        #region Variáveis da classe

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados do grid de ferramentas de fresamento.
        /// </summary>
        private Exporter mExporter_mill;

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados do grid de ferramentas de torneamento.
        /// </summary>
        private Exporter mExporter_turn;

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados do grid de ferramentas de furação.
        /// </summary>
        private Exporter mExporter_hole;

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados do grid de ferramentas de apalpador.
        /// </summary>
        private Exporter mExporter_probe;

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados do grid de ferramentas aditivas.
        /// </summary>
        private Exporter mExporter_additive;

        #endregion

        #region Instância dos objetos da classe

        public FrmFerramentas()
        {
            InitializeComponent();
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que faz a carga dos ícones na interface.
        /// </summary>
        private void InicializaValoresDefault()
        {
            //Objects.DefineColorThemeInterface(this);

            //Remove a ordenação anterior que o usuário pode ter feito (fica salvo internamente no sistema).
            UltraGridOptions udgv1 = new UltraGridOptions(udgv_Mill, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                 Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                 Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                 Imagens_NewLookInterface.remover_deletar,
                                                                                 Imagens_NewLookInterface.agrupamento_16);
            UltraGridOptions udgv2 = new UltraGridOptions(udgv_Turn, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                 Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                 Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                 Imagens_NewLookInterface.remover_deletar,
                                                                                 Imagens_NewLookInterface.agrupamento_16);
            UltraGridOptions udgv3 = new UltraGridOptions(udgv_Hole, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                 Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                 Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                 Imagens_NewLookInterface.remover_deletar,
                                                                                 Imagens_NewLookInterface.agrupamento_16);
            UltraGridOptions udgv4 = new UltraGridOptions(udgv_Probe, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                  Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                  Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                  Imagens_NewLookInterface.remover_deletar,
                                                                                  Imagens_NewLookInterface.agrupamento_16);
            UltraGridOptions udgv5 = new UltraGridOptions(udgv_Additive, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                     Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                     Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                     Imagens_NewLookInterface.remover_deletar,
                                                                                     Imagens_NewLookInterface.agrupamento_16);
            
            mExporter_mill = new Exporter(udgv_Mill, btnExportarMill, "Ferramentas de fresamento");
            mExporter_turn = new Exporter(udgv_Turn, btnExportarTurn, "Ferramentas de torneamento");
            mExporter_hole = new Exporter(udgv_Hole, btnExportarHole, "Ferramentas de furação");
            mExporter_probe = new Exporter(udgv_Probe, btnExportarProbe, "Ferramentas de apalpador");
            mExporter_additive = new Exporter(udgv_Additive, btnExportarAdditive, "Ferramentas aditivas");

            #region Aba Fresamento

            icbxMill_Tipo.Items.Add(new ComboBoxItem("(Todos)"));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa", Imagens_Edgecam.Mill_0));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa de cantos arredondados", Imagens_Edgecam.Mill_1));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Esférica", Imagens_Edgecam.Mill_2));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa de topo", Imagens_Edgecam.Mill_3));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa em ângulo", Imagens_Edgecam.Mill_4));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Faceamento", Imagens_Edgecam.Mill_5));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa de abrir canais", Imagens_Edgecam.Mill_6));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa pirulito", Imagens_Edgecam.Mill_7));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Ferramenta de rosca", Imagens_Edgecam.Mill_8));
            icbxMill_Tipo.Items.Add(new ComboBoxItem("Fresa Escalonada", Imagens_Edgecam.Mill_9));
            icbxMill_Tipo.SelectedIndex = 0;

            cbxMill_Tecnologia.SelectedIndex = 0;
            cbxMill_Unidade.SelectedIndex = 0;
            txtMill_Diam.Text = "0.0";

            #endregion

            #region Aba Torneamento

            icbxTurn_Tipo.Items.Add(new ComboBoxItem("(Todos)"));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Torneamento externo", Imagens_Edgecam.Turn_0));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Torneamento interno", Imagens_Edgecam.Turn_1));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Canal interno", Imagens_Edgecam.Turn_2));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Canal externo", Imagens_Edgecam.Turn_3));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Sangramento interno", Imagens_Edgecam.Turn_4));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Sangramento externo", Imagens_Edgecam.Turn_5));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Rosqueamento interno", Imagens_Edgecam.Turn_6));
            icbxTurn_Tipo.Items.Add(new ComboBoxItem("Rosqueamento externo", Imagens_Edgecam.Turn_7));
            icbxTurn_Tipo.SelectedIndex = 0;

            cbxTurn_Tecnologia.SelectedIndex = 0;
            cbxTurn_Unidade.SelectedIndex = 0;
            cbxTurn_Symbol.SelectedIndex = 0;

            icbxTurn_Lado.Items.Add(new ComboBoxItem("(Todos)"));
            icbxTurn_Lado.Items.Add(new ComboBoxItem("Lado direito", Imagens_Edgecam.side_right));
            icbxTurn_Lado.Items.Add(new ComboBoxItem("Lado esquerdo", Imagens_Edgecam.side_left));
            icbxTurn_Lado.Items.Add(new ComboBoxItem("Neutra (centro)", Imagens_Edgecam.side_center));
            icbxTurn_Lado.SelectedIndex = 0;

            #endregion

            #region Aba Furação

            icbxHole_Tipo.Items.Add(new ComboBoxItem("(Todos)"));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Furação", Imagens_Edgecam.Hole_0));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Alargador", Imagens_Edgecam.Hole_1));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Macho para roscar", Imagens_Edgecam.Hole_2));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Barra de mandrilhar", Imagens_Edgecam.Hole_3));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Escariador", Imagens_Edgecam.Hole_4));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Broca de centro", Imagens_Edgecam.Hole_5));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Broca espada", Imagens_Edgecam.Hole_6));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Mandrilhamento traseiro", Imagens_Edgecam.Hole_7));
            icbxHole_Tipo.Items.Add(new ComboBoxItem("Broca escalonada", Imagens_Edgecam.Hole_8));
            icbxHole_Tipo.SelectedIndex = 0;

            cbxHole_Tecnologia.SelectedIndex = 0;
            cbxHole_Unidade.SelectedIndex = 0;
            txtHole_Diam.Text = "0.0";

            icbxHole_Lado.Items.Add(new ComboBoxItem("(Todos)"));
            icbxHole_Lado.Items.Add(new ComboBoxItem("Lado direito", Imagens_Edgecam.side_right));
            icbxHole_Lado.Items.Add(new ComboBoxItem("Lado esquerdo", Imagens_Edgecam.side_left));
            icbxHole_Lado.SelectedIndex = 0;

            #endregion

            #region Aba Apalpador

            icbxProbe_Tipo.Items.Add(new ComboBoxItem("(Todos)"));
            icbxProbe_Tipo.Items.Add(new ComboBoxItem("Apalpador", Imagens_Edgecam.Probe_01));
            icbxProbe_Tipo.SelectedIndex = 0;

            icbxProbe_Defasagem.Items.Add(new ComboBoxItem("(Todos)"));
            icbxProbe_Defasagem.Items.Add(new ComboBoxItem("Ponto primário", Imagens_Edgecam.probe_primary));
            icbxProbe_Defasagem.Items.Add(new ComboBoxItem("Ponto secundário", Imagens_Edgecam.probe_secondary));
            icbxProbe_Defasagem.Items.Add(new ComboBoxItem("Não definido (nenhum)", Imagens_Edgecam.probe_none));
            icbxProbe_Defasagem.SelectedIndex = 0;

            cbxProbe_Unidade.SelectedIndex = 0;
            txtProbe_Diam.Text = "0.0";

            #endregion

            #region Aba Aditiva

            icbxAdditive_Tipo.Items.Add(new ComboBoxItem("(Todos)"));
            icbxAdditive_Tipo.Items.Add(new ComboBoxItem("Deposição de pó", Imagens_Edgecam.Additive_0));
            icbxAdditive_Tipo.Items.Add(new ComboBoxItem("Deposição de fio (wire)", Imagens_Edgecam.Additive_1));
            icbxAdditive_Tipo.Items.Add(new ComboBoxItem("Metalização", Imagens_Edgecam.Additive_2));
            icbxAdditive_Tipo.Items.Add(new ComboBoxItem("Extrusão", Imagens_Edgecam.Additive_3));
            icbxAdditive_Tipo.SelectedIndex = 0;

            cbxAdditive_Unidade.SelectedIndex = 0;
            txtAdditive_Diam.Text = "0.0";

            #endregion

        }

        #endregion

        #region Ações com o botão direito sobre as grades de dados (todas as grades de dados)

        /// <summary>
        ///     Inicia a busca dos trabalhos em que a ferramenta foi utilizada.
        /// </summary>
        /// <param name="GradeDeDadosAtiva">Grade de dados que teve origem a ação/evento.</param>
        private void RastreiaFerramenta(UltraGrid GradeDeDadosAtiva)
        {
            var q = GradeDeDadosAtiva.Selected.Rows.Count == 0 ? GradeDeDadosAtiva.Rows[0] : GradeDeDadosAtiva.Selected.Rows[0];

            if (q != null)
            {
                FrmFerramentas_Rastreio frm = new FrmFerramentas_Rastreio(q.Cells["id"].OriginalValue.ToString(), q.Cells["Descrição"].OriginalValue.ToString());
                if (frm._FerramentaUsada)
                {
                    frm.ShowDialog();
                }
            }
        }

        /// <summary>
        ///     Método que adiciona a ferramenta no inventário.
        /// </summary>
        /// <param name="GradeDeDadosAtiva">Grade de dados que teve origem a ação/evento.</param>
        private void AdicionaFerramentaInventario(UltraGrid GradeDeDadosAtiva)
        {
            var q = GradeDeDadosAtiva.Selected.Rows.Count == 0 ? GradeDeDadosAtiva.Rows[0] : GradeDeDadosAtiva.Selected.Rows[0];

            if (q != null)
            {
                String ambiente = "";

                switch (GradeDeDadosAtiva.Name)
                {
                    case "udgv_Mill": ambiente = "FRESAMENTO"; break;
                    case "udgv_Turn": ambiente = "TORNEAMENTO"; break;
                    case "udgv_Hole": ambiente = "FURAÇÃO"; break;
                    case "udgv_Probe": ambiente = "APALPADOR"; break;
                    case "udgv_Additive": ambiente = "ADITIVA"; break;
                }


                FrmInventarios_New_Tool frm = new FrmInventarios_New_Tool(q.Cells["id"].OriginalValue.ToString(), ambiente,
                                                                          q.Cells["Tipo_Db"].OriginalValue.ToString(),
                                                                          q.Cells["Descrição"].OriginalValue.ToString(),
                                                                          q.Cells["Unidade"].OriginalValue.ToString());
                frm.ShowDialog();
            }
        }

        /// <summary>
        ///     Método que apresenta as opções com o botão direito do mouse
        /// na grade de dados.
        /// </summary>
        /// <param name="GradeDeDadosAtiva">Grade de dados que teve origem a ação/evento.</param>
        private void AbreComandos_GradeDeDados(UltraGrid GradeDeDadosAtiva)
        {
            if (GradeDeDadosAtiva.Rows.Count > 0)
            {
                //  Se o grid tiver agrupamentos, pode ser que o usuário deseja apenas remover o agrupamento.
                //Eu deixei desabilitado os controles dessa interface, previne o erro no sistema.
                if (UltraGridOptions.GridPossuiAgrupamento(GradeDeDadosAtiva))
                {
                    tsm_AddInven.Enabled = false;
                    tsm_Rastrear.Enabled = false;
                    tsm_CheckInven.Enabled = false;
                }
                else
                {
                    var q = GradeDeDadosAtiva.Selected.Rows.Count == 0 ? GradeDeDadosAtiva.Rows[0] : GradeDeDadosAtiva.Selected.Rows[0];

                    tsm_Rastrear.Enabled = true;
                    tsm_CheckInven.Enabled = true;

                    //Se a ferramenta não exise, eu permito adiciona-lá.
                    if (!ExisteFerramentaInventario(q.Cells["id"].OriginalValue.ToString()))
                        tsm_AddInven.Enabled = true;
                    else tsm_AddInven.Enabled = false;
                }
            }
            else
            {
                tsm_AddInven.Enabled = false;
                tsm_Rastrear.Enabled = false;
                tsm_CheckInven.Enabled = false;
            }
        }

        /// <summary>
        ///     Método que verifica se a ferramenta já não foi adicionada no inventário
        /// para ser gerenciada.
        /// </summary>
        /// <param name="IdTool">Id da ferramenta</param>
        /// <returns>True ela ainda já foi adicionada, false para o contrário.</returns>
        private Boolean ExisteFerramentaInventario(String IdTool)
        {
            try
            {
                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.VERIFICA_SE_EXISTE_FERRAMENTA_INVENTARIO, new Dictionary<string, object>() { { "@IDTOOL", IdTool } });

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        /// <summary>
        ///     Métdo que consulta o estoque de ferramenta atual.
        /// </summary>
        /// <param name="GradeDeDadosAtiva">Grade de dados que deu origem à chamada do método.</param>
        private void ConsultaInventario_Tool(UltraGrid GradeDeDadosAtiva)
        {
            var q = GradeDeDadosAtiva.Selected.Rows.Count == 0 ? GradeDeDadosAtiva.Rows[0] : GradeDeDadosAtiva.Selected.Rows[0];

            DataTable dt = SQLQueries.Consulta_InventarioFerramentas(q.Cells["id"].OriginalValue.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                FerramentaEstoque t = new FerramentaEstoque();

                t.Id = dt.Rows[0]["id"].ToString();
                t.ToolId = dt.Rows[0]["Id da ferramenta"].ToString();
                t.NomeTool = dt.Rows[0]["Nome da ferramenta"].ToString();
                t.TipoTool = dt.Rows[0]["Tipo_Db"].ToString();
                t.SubTipoTool = dt.Rows[0]["SubTipo_Db"].ToString();
                t.UnidadeMedida = dt.Rows[0]["Unidade_Db"].ToString();
                t.TipoGestaoVidaUtil = dt.Rows[0]["TipoGestao_Db"].ToString();
                t.TempoVidaUtil = dt.Rows[0]["Tempo de vida útil"].ToString();
                t.TemValidade = dt.Rows[0]["Possui validade"].ToString();
                t.DataValidade = dt.Rows[0]["Data de validade"].ToString();
                t.Fornecedor = dt.Rows[0]["Fornecedor"].ToString();
                t.CustoUnitario = dt.Rows[0]["Custo unitário"].ToString();
                t.TempoRecebimento = dt.Rows[0]["Tempo para recebimento"].ToString();
                t.QuantidadeEstoque = dt.Rows[0]["Quantidade em estoque"].ToString();
                t.EstoqueMinimo = dt.Rows[0]["Estoque mínimo"].ToString();
                t.ParaVenda = dt.Rows[0]["Permitir venda"].ToString();
                t.ParaFabricacao = dt.Rows[0]["Permitir fabricação"].ToString();
                t.ParaUsoInterno = dt.Rows[0]["Permitir uso interno"].ToString();
                t.Estado = dt.Rows[0]["Estado_Db"].ToString();
                t.UnidadeOrganizacional = dt.Rows[0]["Unidade organizacional"].ToString();
                t.Armazem = dt.Rows[0]["Armazém"].ToString();
                t.DadosAux = dt.Rows[0]["Dados adicionais"].ToString();
                t.DtCriacao = dt.Rows[0]["DtCriacao"].ToString();
                t.UsuarioCriacao = dt.Rows[0]["UsuarioCriacao"].ToString();
                t.DtUltimaMod = dt.Rows[0]["DtUltimaMod"].ToString();
                t.UsuarioUltimaMod = dt.Rows[0]["UsuarioUltimaMod"].ToString();

                Objects.ImplementaNovoFormTela(new FrmInventarios_EstTool(t));
            }
        }

        /// <summary>
        ///     Verifica se há ferramentas na interface de fresamento.
        /// </summary>
        private void cms_Opening(object sender, CancelEventArgs e)
        {
            String nome = "";

            try
            {
                nome = ((UltraGrid)(((ContextMenuStrip)(sender)).SourceControl)).Name;

                AbreComandos_GradeDeDados((UltraGrid)(((ContextMenuStrip)(sender)).SourceControl));
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar abrir as opções do direito do mouse na grade de dados",
                                           "FrmFerramentas", "cms_Opening", "Grade de dados: " + nome, "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm_Rastrear_Click(object sender, EventArgs e)
        {
            try
            {
                RastreiaFerramenta((UltraGrid)((ContextMenuStrip)((sender as ToolStripMenuItem).Owner)).SourceControl);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao rastrear as ferramentas", "FrmFerramentas", "tsmMill_Rastrear_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm_AddInven_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionaFerramentaInventario((UltraGrid)((ContextMenuStrip)((sender as ToolStripMenuItem).Owner)).SourceControl);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar a ferramenta no estoque", "FrmFerramentas", "tsmMill_AddInven_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsm_CheckInven_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaInventario_Tool((UltraGrid)((ContextMenuStrip)((sender as ToolStripMenuItem).Owner)).SourceControl);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar estoque da ferramenta", "FrmFerramentas", "tsm_CheckInven_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Fresamento'

        private void PesquisaFerramentas_Fresamento()
        {
            //Monitora o tempo que o usuário está gastando para programar a peça.
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            Objects.LimpaOrdenacaoColunasGrid(udgv_Mill);
            udgv_Mill.DataSource = SQLQueries.ConsultaFerramentas_Mill(txtMill_Desc.Text, icbxMill_Tipo.SelectedIndex - 1, txtMill_Diam.Text, cbxMill_Unidade.SelectedIndex - 1, cbxMill_Tecnologia.SelectedIndex);

            if (udgv_Mill.Rows.Count > 0)
            {
                lblMill_Qtde.Text = String.Format("Quantidade de ferramentas encontradas: {0}", udgv_Mill.Rows.Count.ToString());
                lblMill_Qtde.Visible = true;
            }
            else lblMill_Qtde.Visible = false;

            //sw.Stop();
            //String tempo = sw.Elapsed.Milliseconds.ToString();
        }

        private void txtMill_Diam_TextChanged(object sender, EventArgs e)
        {
            txtMill_Diam.Text = CustomStrings.DeixaSomenteDecimais(txtMill_Diam.Text);
        }

        private void btnPesquisa_Mill_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisaFerramentas_Fresamento();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar as ferramentas de fresamento", "FrmFerramentas", "btnPesquisa_Mill_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Abre o 'Context menu strip' e permite o usuário exportar os dados
        /// da grade de dados em arquivos definidos pelo mesmo.
        /// </summary>
        private void btnExportarMill_Click(object sender, EventArgs e)
        {
            mExporter_mill.MostrarCms();
        }


        private void txtMill_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnPesquisa_Mill_Click(new object(), new EventArgs());
        }

        #endregion

        #region Aba 'Torneamento'

        private void PesquisaFerramentas_Torneamento()
        {
            Objects.LimpaOrdenacaoColunasGrid(udgv_Turn);
            udgv_Turn.DataSource = SQLQueries.ConsultaFerramentas_Turn(txtTurn_Desc.Text, icbxTurn_Tipo.SelectedIndex - 1, cbxTurn_Symbol.SelectedIndex - 1, cbxTurn_Unidade.SelectedIndex - 1, cbxTurn_Tecnologia.SelectedIndex, icbxTurn_Lado.SelectedIndex - 1);

            if (udgv_Turn.Rows.Count > 0)
            {
                lblTurn_Qtde.Text = String.Format("Quantidade de ferramentas encontradas: {0}", udgv_Turn.Rows.Count.ToString());
                lblTurn_Qtde.Visible = true;
            }
            else lblTurn_Qtde.Visible = false;
        }

        private void btnPesquisa_Turn_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisaFerramentas_Torneamento();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar as ferramentas de torneamento", "FrmFerramentas", "btnPesquisa_Turn_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Abre o 'Context menu strip' e permite o usuário exportar os dados
        /// da grade de dados em arquivos definidos pelo mesmo.
        /// </summary>
        private void btnExportarTurn_Click(object sender, EventArgs e)
        {
            mExporter_turn.MostrarCms();
        }

        private void txtTurn_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnPesquisa_Turn_Click(new object(), new EventArgs());
        }

        #endregion

        #region Aba 'Furação'

        private void PesquisaFerramentas_Furacao()
        {
            Objects.LimpaOrdenacaoColunasGrid(udgv_Hole);
            udgv_Hole.DataSource = SQLQueries.ConsultaFerramentas_Hole(txtHole_Desc.Text, icbxHole_Tipo.SelectedIndex - 1, txtHole_Diam.Text, cbxHole_Unidade.SelectedIndex - 1, cbxHole_Tecnologia.SelectedIndex, icbxHole_Lado.SelectedIndex - 1);

            if (udgv_Hole.Rows.Count > 0)
            {
                lblHole_Qtde.Text = String.Format("Quantidade de ferramentas encontradas: {0}", udgv_Hole.Rows.Count.ToString());
                lblHole_Qtde.Visible = true;
            }
            else lblHole_Qtde.Visible = false;
        }

        private void txtHole_Diam_TextChanged(object sender, EventArgs e)
        {
            txtHole_Diam.Text = CustomStrings.DeixaSomenteDecimais(txtHole_Diam.Text);
        }

        private void btnPesquisa_Hole_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisaFerramentas_Furacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar as ferramentas de furação", "FrmFerramentas", "btnHole_Pes_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Abre o 'Context menu strip' e permite o usuário exportar os dados
        /// da grade de dados em arquivos definidos pelo mesmo.
        /// </summary>
        private void btnExportarHole_Click(object sender, EventArgs e)
        {
            mExporter_hole.MostrarCms();
        }

        private void txtHole_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnPesquisa_Hole_Click(new object(), new EventArgs());
        }

        #endregion

        #region Aba 'Apalpadores'

        private void PesquisaFerramentas_Apalpador()
        {
            Objects.LimpaOrdenacaoColunasGrid(udgv_Probe);
            udgv_Probe.DataSource = SQLQueries.ConsultaFerramentas_Probe(txtProbe_Desc.Text, icbxProbe_Tipo.SelectedIndex - 1, txtProbe_Diam.Text, cbxProbe_Unidade.SelectedIndex - 1, icbxProbe_Defasagem.SelectedIndex - 1);

            if (udgv_Probe.Rows.Count > 0)
            {
                lblProbe_Qtde.Text = String.Format("Quantidade de ferramentas encontradas: {0}", udgv_Probe.Rows.Count.ToString());
                lblProbe_Qtde.Visible = true;
            }
            else lblProbe_Qtde.Visible = false;
        }

        private void txtProbe_Diam_TextChanged(object sender, EventArgs e)
        {
            txtProbe_Diam.Text = CustomStrings.DeixaSomenteDecimais(txtProbe_Diam.Text);
        }

        private void btnPesquisa_Probe_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisaFerramentas_Apalpador();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar as ferramentas de apalpador", "FrmFerramentas", "btnPesquisa_Probe_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnExportarProbe_Click(object sender, EventArgs e)
        {
            mExporter_probe.MostrarCms();
        }

        private void txtProbe_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnPesquisa_Probe_Click(new object(), new EventArgs());
        }

        #endregion

        #region Aba 'Aditiva'

        private void PesquisaFerramentas_Aditivas()
        {
            Objects.LimpaOrdenacaoColunasGrid(udgv_Additive);
            udgv_Additive.DataSource = SQLQueries.ConsultaFerramentas_Aditive(txtAdditive_Desc.Text, icbxAdditive_Tipo.SelectedIndex - 1, txtAdditive_Diam.Text, cbxAdditive_Unidade.SelectedIndex - 1);

            if (udgv_Additive.Rows.Count > 0)
            {
                lblAdditive_Qtde.Text = String.Format("Quantidade de ferramentas encontradas: {0}", udgv_Additive.Rows.Count.ToString());
                lblAdditive_Qtde.Visible = true;
            }
            else lblAdditive_Qtde.Visible = false;
        }

        private void txtAdditive_Diam_TextChanged(object sender, EventArgs e)
        {
            txtAdditive_Diam.Text = CustomStrings.DeixaSomenteDecimais(txtAdditive_Diam.Text);
        }

        private void btnPesquisa_Additive_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisaFerramentas_Aditivas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar as ferramentas aditivas", "FrmFerramentas", "btnPesquisa_Additive_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnExportarAdditive_Click(object sender, EventArgs e)
        {
            mExporter_additive.MostrarCms();
        }

        private void txtAdditive_Desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnPesquisa_Additive_Click(new object(), new EventArgs());
        }

        #endregion

    }
}