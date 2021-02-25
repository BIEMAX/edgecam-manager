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
    internal partial class FrmInventarios : Form
    {

        #region Variáveis globais

        public Exporter mExporter_Tools;

        #endregion

        #region Instância dos objetos da classe

        public FrmInventarios()
        {
            InitializeComponent();
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        private void InicializaValoresDefault()
        {
            //Objects.DefineColorThemeInterface(this);
            UltraGridOptions udgv1 = new UltraGridOptions(udgv_Tools, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                  Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                  Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                  Imagens_NewLookInterface.remover_deletar,
                                                                                  Imagens_NewLookInterface.agrupamento_16);
            mExporter_Tools = new Exporter(udgv_Tools, btnExportarTools, "Inventário de ferramentas");

            icbxTools_Tipo.Items.Add(new ComboBoxItem("(Todos)"));
            icbxTools_Tipo.Items.Add(new ComboBoxItem("Fresamento", Imagens_Edgecam.Mill_0));
            icbxTools_Tipo.Items.Add(new ComboBoxItem("Torneamento", Imagens_Edgecam.Turn_0));
            icbxTools_Tipo.Items.Add(new ComboBoxItem("Furação", Imagens_Edgecam.Hole_0));
            icbxTools_Tipo.Items.Add(new ComboBoxItem("Apalpador", Imagens_Edgecam.Probe_01));
            icbxTools_Tipo.Items.Add(new ComboBoxItem("Aditiva", Imagens_Edgecam.Additive_0));
            icbxTools_Tipo.SelectedIndex = 0;

            cbTools_Unidade.SelectedIndex = 0;

            icbxTools_Subtipo.Items.Add(new ComboBoxItem("Todos"));
            icbxTools_Subtipo.SelectedIndex = 0;
            icbxTools_Subtipo.Enabled = false;

            //Falta consultar as empresas ainda
            Consulta_UnidadesOrganizacionais();

            icbxTools_Estado.Items.Add(new ComboBoxItem("(Todos)"));
            icbxTools_Estado.Items.Add(new ComboBoxItem("Estoque zerado", Edgecam_Manager.Properties.Resources.White));
            icbxTools_Estado.Items.Add(new ComboBoxItem("Estoque disponível", Edgecam_Manager.Properties.Resources.Green));
            icbxTools_Estado.Items.Add(new ComboBoxItem("Estoque abaixo do estoque mínimo", Edgecam_Manager.Properties.Resources.Red));
            icbxTools_Estado.Items.Add(new ComboBoxItem("Estoque em recontagem", Edgecam_Manager.Properties.Resources.Orange));
            icbxTools_Estado.SelectedIndex = 0;

            //Fiz isso para resolver um bug do infragistics. Aparentemente, só foi resolvido na versão 10.1.
            //https://www.infragistics.com/community/forums/f/ultimate-ui-for-windows-forms/41560/code-generation-for-property-image-failed

            //Artigos
            ueb.Groups[0].Items[0].Settings.AppearancesSmall.Appearance.Image = Properties.Resources.part;
            ueb.Groups[0].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Brutos
            ueb.Groups[1].Items[0].Settings.AppearancesSmall.Appearance.Image = Edgecam_Manager.Imagens_NewLookInterface.circulo_tubo;
            ueb.Groups[1].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Fixações
            ueb.Groups[2].Items[0].Settings.AppearancesSmall.Appearance.Image = Properties.Resources.fixacao;
            ueb.Groups[2].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Consumíveis
            ueb.Groups[3].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.caixa;
            ueb.Groups[3].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Ferramentas
            ueb.Groups[4].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.ferramentas2;
            ueb.Groups[4].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;
        }

        /// <summary>
        ///     Consulta as unidades organizacionais do banco de dados do manager.
        /// </summary>
        private void Consulta_UnidadesOrganizacionais()
        {
            cbTools_Empresa.Items.Add("(Todos)");

            try
            {
                cbTools_Empresa.Items.AddRange(Objects.LstUnidOrg.Select(x => x.Unidade).ToArray());
            }
            catch { }

            cbTools_Empresa.SelectedIndex = 0;
        }

        /// <summary>
        ///     Método que remove todas as abas do 'tabControl1' exceto a que foi informada
        /// por parâmetro, e caso esta não esteja interface (tela), a mesma é adicionada.
        /// </summary>
        /// <param name="ControleNaoRemover">Controle à ser removido/Adicionado na interface</param>
        private void RemoveControlesInterface(TabPage ControleNaoRemover)
        {
            if (!tabControl1.TabPages.Contains(ControleNaoRemover)) tabControl1.TabPages.Add(ControleNaoRemover);

            foreach (TabPage p in tabControl1.TabPages)
            {
                if (p != ControleNaoRemover)
                {
                    tabControl1.TabPages.Remove(p);
                }
            }
        }

        /// <summary>
        ///     Método que atualiza o subtipo das ferramentas de acordo com o tipo selecionado.
        /// </summary>
        private void AtualizaIndicesSubTipoFerramenta()
        {
            switch (icbxTools_Tipo.SelectedIndex)
            {
                case 0:
                    icbxTools_Subtipo.Items.ItemsBase.Clear();
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("(Todos)"));
                    icbxTools_Subtipo.SelectedIndex = 0;
                    icbxTools_Subtipo.Enabled = false;
                    break;
                case 1:
                    icbxTools_Subtipo.Items.ItemsBase.Clear();
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("(Todos)"));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa", Imagens_Edgecam.Mill_0));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa de cantos arredondados", Imagens_Edgecam.Mill_1));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Esférica", Imagens_Edgecam.Mill_2));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa de topo", Imagens_Edgecam.Mill_3));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa em ângulo", Imagens_Edgecam.Mill_4));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Faceamento", Imagens_Edgecam.Mill_5));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa de abrir canais", Imagens_Edgecam.Mill_6));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa pirulito", Imagens_Edgecam.Mill_7));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Ferramenta de rosca", Imagens_Edgecam.Mill_8));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Fresa Escalonada", Imagens_Edgecam.Mill_9));
                    icbxTools_Subtipo.SelectedIndex = 0;
                    icbxTools_Subtipo.Enabled = true;
                    break;
                case 2:
                    icbxTools_Subtipo.Items.ItemsBase.Clear();
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("(Todos)"));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Torneamento externo", Imagens_Edgecam.Turn_0));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Torneamento interno", Imagens_Edgecam.Turn_1));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Canal interno", Imagens_Edgecam.Turn_2));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Canal externo", Imagens_Edgecam.Turn_3));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Sangramento interno", Imagens_Edgecam.Turn_4));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Sangramento externo", Imagens_Edgecam.Turn_5));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Rosqueamento interno", Imagens_Edgecam.Turn_6));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Rosqueamento externo", Imagens_Edgecam.Turn_7));
                    icbxTools_Subtipo.SelectedIndex = 0;
                    icbxTools_Subtipo.Enabled = true;
                    break;
                case 3:
                    icbxTools_Subtipo.Items.ItemsBase.Clear();
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("(Todos)"));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Furação", Imagens_Edgecam.Hole_0));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Alargador", Imagens_Edgecam.Hole_1));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Macho para roscar", Imagens_Edgecam.Hole_2));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Barra de mandrilhar", Imagens_Edgecam.Hole_3));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Escariador", Imagens_Edgecam.Hole_4));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Broca de centro", Imagens_Edgecam.Hole_5));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Broca espada", Imagens_Edgecam.Hole_6));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Mandrilhamento traseiro", Imagens_Edgecam.Hole_7));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Broca escalonada", Imagens_Edgecam.Hole_8));
                    icbxTools_Subtipo.SelectedIndex = 0;
                    break;
                case 4:
                    icbxTools_Subtipo.Items.ItemsBase.Clear();
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("(Todos)"));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Apalpador", Imagens_Edgecam.Probe_01));
                    icbxTools_Subtipo.SelectedIndex = 0;
                    icbxTools_Subtipo.Enabled = true;
                    break;
                case 5:
                    icbxTools_Subtipo.Items.ItemsBase.Clear();
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("(Todos)"));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Deposição de pó", Imagens_Edgecam.Additive_0));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Deposição de fio (wire)", Imagens_Edgecam.Additive_1));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Metalização", Imagens_Edgecam.Additive_2));
                    icbxTools_Subtipo.Items.Add(new ComboBoxItem("Extrusão", Imagens_Edgecam.Additive_3));
                    icbxTools_Subtipo.SelectedIndex = 0;
                    icbxTools_Subtipo.Enabled = true;
                    break;
            }
        }

        private void AcessaAba_Artigos()
        {
            try
            {
                RemoveControlesInterface(tb_Pecas);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover aba 'Artigos'", "FrmInventario", "linklblArtigos_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void AcessaAba_Brutos()
        {
            try
            {
                RemoveControlesInterface(tb_Brutos);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover aba 'Brutos'", "FrmInventario", "linklblBrutos_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void AcessaAba_Fixacoes()
        {
            try
            {
                RemoveControlesInterface(tb_Fixacoes);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover aba 'Fixações'", "FrmInventario", "linklblFixacoes_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void AcessaAba_Consumiveis()
        {
            try
            {
                RemoveControlesInterface(tb_Consumiveis);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover aba 'Consumíveis'", "FrmInventario", "linklblConsumiveis_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void AcessaAba_Insertos()
        {
            try
            {
                RemoveControlesInterface(tb_Insertos);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover aba 'Insertos'", "FrmInventario", "linklblInsertos_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void AcessaAba_Ferramentas()
        {
            try
            {
                RemoveControlesInterface(tb_Ferramamentas);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao remover aba 'Ferramentas'", "FrmInventario", "linklblFerramentas_LinkClicked", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos gerais

        /// <summary>
        ///     Evento que serve apenas para deixa visível a aba principal (artigos).
        /// As demais abas são removidas.
        /// </summary>
        private void FrmInventario_Load(object sender, EventArgs e)
        {
            RemoveControlesInterface(tb_Pecas);
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

        private void ueb_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            switch (e.Item.Text.ToUpper().Trim())
            {
                case "CONSULTAR O ESTOQUE DE ARTIGOS": AcessaAba_Artigos(); break;
                case "ADICIONAR NOVO ARTIGO AO ESTOQUE": break; //Não faz nada ainda, pq ainda não está pronto.

                case "CONSULTAR O ESTOQUE DE BRUTOS": AcessaAba_Brutos(); break;
                case "ADICIONAR NOVO BRUTO AO ESTOQUE": break;

                case "CONSULTAR O ESTOQUE DE FIXAÇÕES": AcessaAba_Fixacoes(); break;
                case "ADICIONAR NOVA FIXAÇÃO AO ESTOQUE": break;

                case "CONSULTAR O ESTOQUE DE CONSUMÍVEIS": AcessaAba_Consumiveis(); break;
                case "ADICIONAR NOVO CONSUMÍVEL AO ESTOQUE": break;

                case "CONSULTAR O ESTOQUE DE FERRAMENTAS": AcessaAba_Ferramentas(); break;
                case "ADICIONAR NOVA FERRAMENTA AO ESTOQUE": break;
            }
        }

        #endregion

        #region Aba 'Artigos'

        private void tsm_Adicionar_Click(object sender, EventArgs e)
        {
            FrmPeca_Importa frm = new FrmPeca_Importa();
            frm.ShowDialog();
        }

        #endregion

        #region Aba 'Ferramentas'

        private void PesquisaInventario_Ferramentas()
        {
            udgv_Tools.DataSource = SQLQueries.Consulta_InventarioFerramentas("", txtTools_Desc.Text, cbTools_Unidade.SelectedIndex - 1, icbxTools_Tipo.SelectedIndex - 1, 
                                                                              icbxTools_Subtipo.SelectedIndex - 1, cbTools_Empresa.SelectedItem.ToString(), 
                                                                              icbxTools_Estado.SelectedIndex - 1);
        }

        /// <summary>
        ///     Método responsável por consultar o estoque da ferramenta atual, histórico,
        /// movimentos de entrada e saída.
        /// </summary>
        private void ConsultaEstoque()
        {
            if (udgv_Tools.Rows.Count > 0)
            {
                var q = udgv_Tools.Selected.Rows.Count == 0 ? udgv_Tools.Rows[0] : udgv_Tools.Selected.Rows[0];

                FerramentaEstoque t = new FerramentaEstoque();
                t.Id                    = q.Cells["id"].OriginalValue.ToString();
                t.ToolId                = q.Cells["Id da ferramenta"].OriginalValue.ToString();
                t.NomeTool              = q.Cells["Nome da ferramenta"].OriginalValue.ToString();
                t.TipoTool              = q.Cells["Tipo_Db"].OriginalValue.ToString();
                t.SubTipoTool           = q.Cells["SubTipo_Db"].OriginalValue.ToString();
                t.UnidadeMedida         = q.Cells["Unidade_Db"].OriginalValue.ToString();
                t.TipoGestaoVidaUtil    = q.Cells["TipoGestao_Db"].OriginalValue.ToString();
                t.TempoVidaUtil         = q.Cells["Tempo de vida útil"].OriginalValue.ToString();
                t.TemValidade           = q.Cells["Possui validade"].OriginalValue.ToString();
                t.DataValidade          = q.Cells["Data de validade"].OriginalValue.ToString();
                t.Fornecedor            = q.Cells["Fornecedor"].OriginalValue.ToString();
                t.CustoUnitario         = q.Cells["Custo unitário"].OriginalValue.ToString();
                t.TempoRecebimento      = q.Cells["Tempo para recebimento"].OriginalValue.ToString();
                t.QuantidadeEstoque     = q.Cells["Quantidade em estoque"].OriginalValue.ToString();
                t.EstoqueMinimo         = q.Cells["Estoque mínimo"].OriginalValue.ToString();
                t.ParaVenda             = q.Cells["Permitir venda"].OriginalValue.ToString();
                t.ParaFabricacao        = q.Cells["Permitir fabricação"].OriginalValue.ToString();
                t.ParaUsoInterno        = q.Cells["Permitir uso interno"].OriginalValue.ToString();
                t.Estado                = q.Cells["Estado_Db"].OriginalValue.ToString();
                t.UnidadeOrganizacional = q.Cells["Unidade organizacional"].OriginalValue.ToString();
                t.Armazem               = q.Cells["Armazém"].OriginalValue.ToString();
                t.DadosAux              = q.Cells["Dados adicionais"].OriginalValue.ToString();
                t.DtCriacao             = q.Cells["DtCriacao"].OriginalValue.ToString();
                t.UsuarioCriacao        = q.Cells["UsuarioCriacao"].OriginalValue.ToString();
                t.DtUltimaMod           = q.Cells["DtUltimaMod"].OriginalValue.ToString();
                t.UsuarioUltimaMod      = q.Cells["UsuarioUltimaMod"].OriginalValue.ToString();

                Objects.ImplementaNovoFormTela(new FrmInventarios_EstTool(t));
            }
        }

        private void icbxTools_Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AtualizaIndicesSubTipoFerramenta();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao determinar tipo de ferramenta", "FrmInventario_AbaFerramentas", "icbxTools_Tipo_SelectedIndexChanged", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnPesquisa_Tools_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisaInventario_Ferramentas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar o inventário de ferramentas", "FrmInventario_AbaFerramentas", "btnPesquisa_Tools_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnExportarTools_Click(object sender, EventArgs e)
        {
            mExporter_Tools.MostrarCms();
        }

        private void cms_Ferramentas_Opening(object sender, CancelEventArgs e)
        {
            if (udgv_Tools.Rows.Count > 0)
            {
                tsmVerificarEstoque.Enabled = true;
            }
            else
            {
                tsmVerificarEstoque.Enabled = false;
            }
        }

        private void tsmVerificarEstoque_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaEstoque();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar estoque", "FrmInventarios_AbaFerramentas", "tsmVerificarEstoque_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}