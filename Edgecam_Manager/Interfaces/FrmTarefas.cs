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
using Edgecam_Manager.Idiomas;

namespace Edgecam_Manager
{
    internal partial class FrmTarefas : Form
    {
        #region Variáveis da classe

        /// <summary>
        ///     Objeto que dá acesso à exportação dos dados da grade dos dados.
        /// </summary>
        private Exporter mExporter;

        #endregion

        #region Instância dos objetos da classe

        public FrmTarefas()
        {
            InitializeComponent();
            InicializaValoresDefault();
            //Objects.DefineColorThemeInterface(this);
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Inicializa os valores 'padrões' na interface (controles de filtros por exemplo).
        /// </summary>
        private void InicializaValoresDefault()
        {
            cbxPrioridade.Items.Add(new ComboBoxItem("(Todos)"));
            cbxPrioridade.Items.Add(new ComboBoxItem("Baixa", Properties.Resources.p_baixa));
            cbxPrioridade.Items.Add(new ComboBoxItem("Normal (Médio)", Properties.Resources.p_normal));
            cbxPrioridade.Items.Add(new ComboBoxItem("Alta", Properties.Resources.p_alta));
            cbxPrioridade.SelectedIndex = 0;

            cbUsarData.Checked = false;
            dtData.Enabled = false;
            dtData.DateTime = DateTime.Now;

            //Remove a ordenação anterior que o usuário pode ter feito (fica salvo internamente no sistema).
            UltraGridOptions udgv_op = new UltraGridOptions(udgv, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                              Imagens_NewLookInterface.ordenar_crescente_16,
                                                                              Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                              Imagens_NewLookInterface.remover_deletar,
                                                                              Imagens_NewLookInterface.agrupamento_16);
            //Objects.DefineColorThemeInterface(this);
            mExporter = new Exporter(udgv, btnExportar, "Tarefas");

            //Fiz isso para resolver um bug do infragistics. Aparentemente, só foi resolvido na versão 10.1.
            //https://www.infragistics.com/community/forums/f/ultimate-ui-for-windows-forms/41560/code-generation-for-property-image-failed

            //Tarefas
            ueb.Groups[0].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.tarefa;
            ueb.Groups[0].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Ordens
            ueb.Groups[1].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.producao_fabrica;
            ueb.Groups[1].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Orçamentos
            ueb.Groups[2].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.moedas_cotacao;
            ueb.Groups[2].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Inventário
            ueb.Groups[3].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.produto_inventario;
            ueb.Groups[3].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Trabalhos
            ueb.Groups[4].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.trabalhos_pasta;
            ueb.Groups[4].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;
            ueb.Groups[4].Items[2].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Ferramentas
            ueb.Groups[5].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.ferramentas2;
            ueb.Groups[5].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.ferramentas2;
            ueb.Groups[5].Items[2].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.ferramentas2;

            //Máquinas
            ueb.Groups[6].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.maquinas;
            ueb.Groups[6].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Vendas
            ueb.Groups[7].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.vendas_pedidos;
            ueb.Groups[7].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;
            ueb.Groups[7].Items[2].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.novo;

            //Relatórios
            ueb.Groups[8].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.relatorios;

            //Outros
            ueb.Groups[9].Items[0].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.xml_azul;
            ueb.Groups[9].Items[1].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.servico_suporte;
            ueb.Groups[9].Items[2].Settings.AppearancesSmall.Appearance.Image = Imagens_NewLookInterface.web;

            customFilterControl1._GroupBox = ugbFiltros;
            customFilterControl1._Interface = "Tarefas";
            customFilterControl1._Module = "Tarefas";
        }

        /// <summary>
        ///     Método que bloqueia algumas ações do grid (Editar, visualizar, etc), tudo
        ///  o que precisar de dados para trabalhar, ele bloqueia.
        /// </summary>
        private void BloqueiaAcoesDireitoMouse()
        {
            //Bloqueia os botoões de editar, excluir, completar e visualizar.
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnComplete.Enabled = false;
            btnViewDetails.Enabled = false;

            //Bloqueia o botão de exportação
            btnExportar.Enabled = false;
        }

        /// <summary>
        ///     Método que desbloqueia algumas ações do grid (Editar, visualizar, etc), tudo
        ///  o que precisar de dados para trabalhar, ele desbloqueia.
        /// </summary>
        private void DesbloqueiaAcoesDireitoMouse()
        {
            //Desloqueia os botoões de editar, excluir, completar e visualizar.
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnComplete.Enabled = true;
            btnViewDetails.Enabled = true;

            //Bloqueia o botão de exportação
            btnExportar.Enabled = true;
        }

        /// <summary>
        ///     Método que verifica se o usuário possuí alguma tarefa atrasada e muda a cor da linha para vermelho.
        /// </summary>
        private void DestacaItensAtrasados()
        {
            for (int x = 0; x < udgv.Rows.Count; x++)
            {
                // 0 = mesma data  |  -1 = A segunda data é a maior |  1 = A primeira data é a menor
                if (DateTime.Compare(Convert.ToDateTime(udgv.Rows[x].Cells["Início"].Value.ToString()), Convert.ToDateTime(DateTime.Now.ToShortDateString())) == -1)
                {
                    udgv.Rows[x].Appearance.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        ///     Método responsável por (re)consultar as tarefas do banco auxiliar do sistema.
        /// </summary>
        private void ConsultaTarefas()
        {
            //Serve para monitorar o tempo de consulta no banco de dados.
            //System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            //s.Start();

            //udgv.DataSource = SkaConsultasTables.Consulta_Tarefas(txtAssunto.Text, cbxPrioridade.SelectedIndex, cbUsarData.Checked == true ? dtData.DateTime.ToShortDateString() : "");

            //s.Stop();
            //MessageBox.Show("Tempo de consulta em mili segundos: " + s.ElapsedMilliseconds);

            Cursor = Cursors.WaitCursor;

            //ECMGR-250
            Objects.LimpaOrdenacaoColunasGrid(udgv);

            udgv.DataSource = SQLQueries.Consulta_Tarefas(txtAssunto.Text, cbxPrioridade.SelectedIndex, cbUsarData.Checked == true ? dtData.DateTime.ToString("yyyy-MM-dd") : "");

            //Habilita os botões de editar e excluir.
            if (udgv.Rows.Count > 0)
            {
                DesbloqueiaAcoesDireitoMouse();
                DestacaItensAtrasados();
                udgv.ActiveRow = udgv.ActiveRowScrollRegion.FirstRow;

                //for (int x = 0; x < udgv.Rows.Count; x++)
                //{
                //    udgv.Rows[x].Cells["Prioridade"].ToolTipText = DefineTextoPrioridade(udgv.Rows[x].Cells["PrioridadeTarefa"].Value.ToString());
                //}
            }
            else BloqueiaAcoesDireitoMouse();

            Cursor = Cursors.Arrow;
        }

        //private string DefineTextoPrioridade(String NivelPrioridade)
        //{
        //    switch (NivelPrioridade)
        //    {
        //        case "1": return "Baixo";
        //        case "2": return "Médio";
        //        case "3": return "Alto";
        //        default: return "Médio";
        //    }
        //}

        /// <summary>
        ///     Abre interface para criar uma nova tarefa.
        /// </summary>
        private void NovaTarefa()
        {
            FrmTarefas_New frm;
            Objects.ImplementaNovoFormTela(frm = new FrmTarefas_New(), true);

            ////  Fico dentro do loop enquanto o formulário principal ainda tiver
            ////uma instância do formulário de nova interação.
            //do
            //{
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();
            //}
            //while (Objects.FormularioPrincipal.Controls.Contains(frm));

            //Consulta as ordens de produção novamente para trazer a ordem recém criada.
            ubtnPesquisar_Click(new object(), new EventArgs());

            Objects.SetaUltimaTuplaSelecionada(udgv);
        }

        /// <summary>
        ///     Método responsável por editar a tarefa.
        /// </summary>
        private void EditaTarefa()
        {
            /*
             *  Dionei Beilke dos Santos
             *  06/07/2018, at 12:20 AM
             *  ECMGR-137
             *  Tive que adicionar as condições abaixo, pois quando se havia apenas uma
             * row no DataGrid, gerava exceção dizendo que não havia sido selecionado
             * nenhuma linha para edição (literalmente precisa-se selecionar uma linha
             * para funcionar).
             */
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg007();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                Tarefa t = new Tarefa();
                t.IdTarefa              = q.Cells["id"].OriginalValue.ToString();
                t.Assunto               = q.Cells["Assunto"].OriginalValue.ToString();
                t.Instrucoes            = q.Cells["Descrição"].OriginalValue.ToString();
                t.TipoTarefa            = q.Cells["TipoTarefa"].OriginalValue.ToString();
                t.Prioridade            = q.Cells["PrioridadeTarefa"].OriginalValue.ToString();
                t.DtInicio              = q.Cells["Início"].OriginalValue.ToString();
                t.DtLimitePrazo         = q.Cells["DtLimite"].OriginalValue.ToString();
                t.DtCriacao             = q.Cells["Requerida"].OriginalValue.ToString();
                t.DtUltimaMod           = q.Cells["DtUltimaMod"].OriginalValue.ToString();
                t.UsuarioUltimaMod      = q.Cells["UsuarioUltimaMod"].OriginalValue.ToString();
                t.UsuarioDesignado      = q.Cells["Designado"].OriginalValue.ToString();
                t.UsuarioSolicitante    = q.Cells["Solicitante"].OriginalValue.ToString();
                t.ExecucaoRecursiva     = q.Cells["ExecucaoRecursiva"].OriginalValue.ToString();
                t.PeriodoExecucao       = q.Cells["Periodo"].OriginalValue.ToString();
                t.DtPeriodoInicio       = q.Cells["DtPeriodoInicio"].OriginalValue.ToString();
                t.DtNextPeriodo         = q.Cells["DtPeriodoNext"].OriginalValue.ToString();
                t.Lido                  = q.Cells["Lido"].OriginalValue.ToString();
                t.Completo              = q.Cells["Completo"].OriginalValue.ToString();

                FrmTarefas_New frm;
                Objects.ImplementaNovoFormTela(frm = new FrmTarefas_New(t, false));

                //  Fico dentro do loop enquanto o formulário principal ainda tiver
                //uma instância do formulário de nova interação.
                do
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
                while (Objects.FormularioPrincipal.Controls.Contains(frm));

                //Consulta as ordens de produção novamente para trazer a ordem recém criada.
                ubtnPesquisar_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Visualiza os detalhes de uma tarefa existente.
        /// </summary>
        private void VisualizaTarefa()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg008();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                Tarefa t = new Tarefa();
                t.IdTarefa              = q.Cells["id"].OriginalValue.ToString();
                t.Assunto               = q.Cells["Assunto"].OriginalValue.ToString();
                t.Instrucoes            = q.Cells["Descrição"].OriginalValue.ToString();
                t.TipoTarefa            = q.Cells["TipoTarefa"].OriginalValue.ToString();
                t.Prioridade            = q.Cells["PrioridadeTarefa"].OriginalValue.ToString();
                t.DtInicio              = q.Cells["Início"].OriginalValue.ToString();
                t.DtLimitePrazo         = q.Cells["DtLimite"].OriginalValue.ToString();
                t.DtCriacao             = q.Cells["Requerida"].OriginalValue.ToString();
                t.DtUltimaMod           = q.Cells["DtUltimaMod"].OriginalValue.ToString();
                t.UsuarioUltimaMod      = q.Cells["UsuarioUltimaMod"].OriginalValue.ToString();
                t.UsuarioDesignado      = q.Cells["Designado"].OriginalValue.ToString();
                t.UsuarioSolicitante    = q.Cells["Solicitante"].OriginalValue.ToString();
                t.ExecucaoRecursiva     = q.Cells["ExecucaoRecursiva"].OriginalValue.ToString();
                t.PeriodoExecucao       = q.Cells["Periodo"].OriginalValue.ToString();
                t.DtPeriodoInicio       = q.Cells["DtPeriodoInicio"].OriginalValue.ToString();
                t.DtNextPeriodo         = q.Cells["DtPeriodoNext"].OriginalValue.ToString();
                t.Lido                  = q.Cells["Lido"].OriginalValue.ToString();
                t.Completo              = q.Cells["Completo"].OriginalValue.ToString();

                Objects.ImplementaNovoFormTela(new FrmTarefas_New(t, true));
            }
        }

        /// <summary>
        ///     Concluí a tarefa do usuário.
        /// </summary>
        private void ConcluiTarefa()
        {
            var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

            if (q != null)
            {
                Dictionary<String, object> dic = new Dictionary<string, object>();
                dic.Add("@IDTASK", q.Cells["id"].OriginalValue.ToString());

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONCLUI_TAREFA, dic);

                //Salva a informação no banco de dados que o usuário 'realizou alguma tarefa', para manter o histórico do que o idiota está fazendo.
                Objects.CadastraNovoLog(false, "Não houve erros ao concluir a tarefa", "FrmTarefa", "Concluir tarefa", String.Format("Usuário concluiu uma tarefa de id '{0}'", q.Cells["id"].OriginalValue.ToString()),
                                           "Consultas_EcMgr.CONCLUI_TAREFA", e_TipoErroEx.Informacao);

                ubtnPesquisar_Click(new object(), new EventArgs());

                Messages.Msg009();
            }
        }

        /// <summary>
        ///     Método que deleta uma tarefa selecionado pelo usuário.
        /// </summary>
        private void DeletaTarefa()
        {
            //if (MessageBox.Show("Deseja realmente realizar essa ação? Uma vez deletado, não poderá recuperar-se a informação!", "Deletar tarefa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            if (Messages.Msg010() == System.Windows.Forms.DialogResult.Yes)
            {
                if (udgv.Selected.Rows.Count == 1)
                {
                    Dictionary<String, object> dic = new Dictionary<string, object>();
                    dic.Add("@IDTASK", udgv.Selected.Rows[0].Cells["id"].OriginalValue.ToString());

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_TAREFA, dic);

                    //Salva a informação no banco de dados que o usuário 'realizou alguma tarefa', para manter o histórico do que o idiota está fazendo.
                    Objects.CadastraNovoLog(false, "Não houve erros ao deletar a tarefa", "FrmTarefa", "Deletar tarefa", String.Format("Usuário deletou uma tarefa de id '{0}'", udgv.Selected.Rows[0].Cells["id"].ToString()),
                                               "Consultas_EcMgr.DELETA_TAREFA", e_TipoErroEx.Informacao);

                    ubtnPesquisar_Click(new object(), new EventArgs());
                }
            }
        }

        #endregion

        #region Eventos dos controles da interface

        private void ubtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaTarefas();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Objects.CadastraNovoLog(true, "Erro ao tentar consultar as tarefas", "FrmTarefa", "ConsultaTarefas", "", "Consultas_EcMgr.CONSULTA_TAREFAS_USUARIO_LOGADO", e_TipoErroEx.Erro, ex);
            }
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

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void cbUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUsarData.Checked)
                dtData.Enabled = true;
            else dtData.Enabled = false;
        }

        /// <summary>
        ///     Método que verifica se há dados na grid para habilitar comandos com o botão
        /// direito do mouse.
        /// </summary>
        private void cms_Opening(object sender, CancelEventArgs e)
        {
            if (udgv.Rows.Count > 0) DesbloqueiaAcoesDireitoMouse();
            else BloqueiaAcoesDireitoMouse();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                NovaTarefa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar criar uma nova tarefa", "FrmTarefa", "NovaTarefa", "Exceção não tratada em uma tentativa de criar uma tarefa",
                                           "Consultas_EcMgr.CADASTRA_NOVA_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EditaTarefa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar editar a tarefa", "FrmTarefa", "EditaTarefa", "Exceção não tratada em uma tentativa de editar uma tarefa",
                                           "Consultas_EcMgr.ATUALIZA_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                ConcluiTarefa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao concluir tarefa", "FrmTarefas", "ConcluiTarefa", "", "Consultas_EcMgr.CONCLUI_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeletaTarefa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar deletar a tarefa", "FrmTarefa", "Deletar tarefa", "Exceção não tratada em uma tentativa de remover uma tarefa",
                                           "Consultas_EcMgr.DELETA_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                VisualizaTarefa();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar visualizar a tarefa", "FrmTarefa", "VisualizaTarefa", "Exceção não tratada em uma tentativa de visualizar uma tarefa",
                                           "Consultas_EcMgr.DELETA_TAREFA", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que permite visualizar a tarefa com um duplo click na linha/tupla/row.
        /// </summary>
        private void udgv_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            VisualizaTarefa();
        }

        /// <summary>
        ///     Abre o 'Context menu strip' e permite o usuário exportar os dados
        /// da grade de dados em arquivos definidos pelo mesmo.
        /// </summary>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            mExporter.MostrarCms();
        }

        #endregion

        #region Eventos de foco

        private void txtAssunto_Enter(object sender, EventArgs e)
        {
            txtAssunto.Text = "";
        }

        private void txtAssunto_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtAssunto.Text)) txtAssunto.Text = "(Todos)";
        }

        #endregion

        #region Eventos para chamar as telas por meio dos atalhos

        private void ueb_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            switch (e.Item.Text.ToUpper())
            {
                case "TODAS AS TAREFAS": 
                    ubtnPesquisar_Click(new object(), new EventArgs()); break;
                case "NOVA TAREFA": 
                    btnNew_Click(new object(), new EventArgs()); break;
                case "TODAS AS ORDENS DE PRODUÇÃO": 
                    Objects.ImplementaNovoFormTela(new FrmOrdens()); break;
                case "NOVA ORDEM DE PRODUÇÃO": 
                    Objects.ImplementaNovoFormTela(new FrmOrdens_New()); break;
                case "TODOS OS ORÇAMENTOS": 
                    Objects.ImplementaNovoFormTela(new FrmOrcamentos()); break;
                case "NOVO ORÇAMENTO": 
                    Objects.ImplementaNovoFormTela(new FrmOrcamentos_NewDet()); break;
                case "TODO O ESTOQUE": 
                    Objects.ImplementaNovoFormTela(new FrmInventarios()); break;
            }
        }

        #endregion
    }
}