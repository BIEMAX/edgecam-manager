using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ImagedComboBox;

namespace Edgecam_Manager
{
    internal partial class FrmTrabalhos : Form
    {

        #region Variáveis globais

        private Exporter mExporter;

        #endregion

        #region Instância dos objetos da classe

        public FrmTrabalhos()
        {
            InitializeComponent();
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método responsável por fazer a carga dos valores pré-definidos.
        /// </summary>
        private void InicializaValoresDefault()
        {
            //Carrega os estados
            icbxEstado.Items.Add(new ComboBoxItem("(Todos)"));
            icbxEstado.Items.Add(new ComboBoxItem("Criado", Properties.Resources.White));                              //0
            icbxEstado.Items.Add(new ComboBoxItem("Iniciado", Properties.Resources.Green));                            //1
            icbxEstado.Items.Add(new ComboBoxItem("Aguardando aprovação", Properties.Resources.PartiallyReleased));    //2
            icbxEstado.Items.Add(new ComboBoxItem("Não aprovado (revisar)", Properties.Resources.Orange));             //3
            icbxEstado.Items.Add(new ComboBoxItem("Aprovado (concluído)", Properties.Resources.Global));               //4
            icbxEstado.Items.Add(new ComboBoxItem("Cancelado", Properties.Resources.Cancel));                          //5
            icbxEstado.SelectedIndex = 0;
            
            //Adiciona as máquinas na lista.
            cbMaquinas.Items.Add("(Todos)");
            if (Objects.LstMaquinas != null)
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            else
            {
                Objects.LstMaquinas = new ListaMachines();
                cbMaquinas.Items.AddRange(Objects.LstMaquinas.Where(x => x.NomeMqn != "").Select(x => x.NomeMqn).ToArray());
            }
            cbMaquinas.SelectedIndex = 0;
            
            //Define o texto dos controles
            txtTrabalho.Text    = "(Todos)";
            txtCliente.Text     = "(Todos)";
            txtProgramador.Text = "(Todos)";
            txtMaterial.Text    = "(Todos)";

            mExporter = new Exporter(udgv, btnExportar, "Trabalhos");
            UltraGridOptions ultraOptions = new UltraGridOptions(udgv, true, Imagens_NewLookInterface.escolher_editar_coluna_16,
                                                                                   Imagens_NewLookInterface.ordenar_crescente_16,
                                                                                   Imagens_NewLookInterface.ordenar_decrescente_16,
                                                                                   Imagens_NewLookInterface.remover_deletar,
                                                                                   Imagens_NewLookInterface.agrupamento_16);

            //Desabilita controles
            btnExportar.Enabled = false;
        }

        /// <summary>
        ///     Método que consulta os trabalhos.
        /// </summary>
        private void ConsultaTrabalhos()
        {
            udgv.DataSource = SQLQueries.Consulta_Trabalhos("", txtTrabalho.Text, txtCliente.Text, icbxEstado.SelectedIndex - 1, cbMaquinas.SelectedItem.ToString(), txtProgramador.Text, txtMaterial.Text, true);

            if (udgv.Rows.Count > 0)
            {
                btnExportar.Enabled = true;
            }
            else
            {
                btnExportar.Enabled = false;
            }
        }

        /// <summary>
        ///     Apresenta a interface para cadastro de um novo trabalho.
        /// </summary>
        private void NovoTrabalho()
        {
            Objects.ImplementaNovoFormTela(new FrmTrabalhos_New());
        }

        /// <summary>
        ///     Método que visualiza os dados do trabalho selecionado.
        /// </summary>
        private void VisualizaTrabalho()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                TrabalhoEdgecam j = new TrabalhoEdgecam();
                j.IdJob                 = Convert.ToInt16(q.Cells["id"].OriginalValue.ToString());
                j.Descricao             = q.Cells["Nome do trabalho"].OriginalValue.ToString();
                j.Comentario            = q.Cells["Comentário"].OriginalValue.ToString();
                j.Familia               = q.Cells["Familía"].OriginalValue.ToString();
                j.Sequencia             = q.Cells["Sequência"].OriginalValue.ToString();
                j.PostoTrabalho         = q.Cells["Máquina"].OriginalValue.ToString();
                j.Cliente               = q.Cells["Cliente"].OriginalValue.ToString();
                j.Usuario               = q.Cells["Programador"].OriginalValue.ToString();
                j.Material              = q.Cells["Material"].OriginalValue.ToString();
                j.Status                = q.Cells["Status_Db"].OriginalValue.ToString();
                j.CaminhoArqPpf         = q.Cells["Arquivo PPF"].OriginalValue.ToString();
                j.CaminhoArqCad         = q.Cells["Arquivo CAD"].OriginalValue.ToString();
                j.CaminhoArqCnc         = q.Cells["Arquivo CNC"].OriginalValue.ToString();
                j.PreSelecaoKits        = q.Cells["Pré-carga de ferramentas"].OriginalValue.ToString();
                j.RevisaoJob            = q.Cells["Revisão"].OriginalValue.ToString();
                j.TempoDeCiclo          = q.Cells["Tempo de usinagem"].OriginalValue.ToString();
                j.JobNotesSubject       = q.Cells["JOB_NOTES_SUBJECT"].OriginalValue.ToString();
                j.JobNotes              = q.Cells["JOB_NOTES"].OriginalValue.ToString();
                j.JobNotesFile          = q.Cells["JOB_NOTES_FILE"].OriginalValue.ToString();
                j.FixtureNotesSubject   = q.Cells["JOB_FIXTURE_NOTES_SUBJECT"].OriginalValue.ToString();
                j.FixturesNotes         = q.Cells["JOB_FIXTURE_NOTES"].OriginalValue.ToString();
                j.FixturesNotesFile     = q.Cells["JOB_FIXTURE_NOTES_FILE"].OriginalValue.ToString();
                j.StockNotesSubject     = q.Cells["JOB_STOCK_NOTES_SUBJECT"].OriginalValue.ToString();
                j.StockNotes            = q.Cells["JOB_STOCK_NOTES"].OriginalValue.ToString();
                j.StockNotesFile        = q.Cells["JOB_STOCK_NOTES_FILE"].OriginalValue.ToString();
                j.TrabalhoVisivel       = q.Cells["Visibilidaed_Db"].OriginalValue.ToString();
                //j.TurretWarning = q.Cells[""].OriginalValue.ToString();
                j.DtCriacao             = q.Cells["Data de criação"].OriginalValue.ToString();
                j.DtModificacao         = q.Cells["Data da última modificação"].OriginalValue.ToString();
                j.PartStickOut          = q.Cells["JOB_STICKOUT"].OriginalValue.ToString();
                //j.ProgramId1 = q.Cells[""].OriginalValue.ToString();
                //j.ProgramId2 = q.Cells[""].OriginalValue.ToString();

                Objects.ImplementaNovoFormTela(new FrmTrabalhos_New(j, true));
            }
        }

        /// <summary>
        ///     Método que permite o usuário editar os dados do trabalho.
        /// </summary>
        private void EditaTrabalho()
        {
            if (udgv.Selected.Rows.Count > 1)
                Messages.Msg015();
            else if (udgv.Rows.Count > 0)
            {
                var q = udgv.Selected.Rows.Count == 0 ? udgv.Rows[0] : udgv.Selected.Rows[0];

                TrabalhoEdgecam j = new TrabalhoEdgecam();
                j.IdJob                 = Convert.ToInt16(q.Cells["id"].OriginalValue.ToString());
                j.Descricao             = q.Cells["Nome do trabalho"].OriginalValue.ToString();
                j.Comentario            = q.Cells["Comentário"].OriginalValue.ToString();
                j.Familia               = q.Cells["Familía"].OriginalValue.ToString();
                j.Sequencia             = q.Cells["Sequência"].OriginalValue.ToString();
                j.PostoTrabalho         = q.Cells["Máquina"].OriginalValue.ToString();
                j.Cliente               = q.Cells["Cliente"].OriginalValue.ToString();
                j.Usuario               = q.Cells["Programador"].OriginalValue.ToString();
                j.Material              = q.Cells["Material"].OriginalValue.ToString();
                j.Status                = q.Cells["Status_Db"].OriginalValue.ToString();
                j.CaminhoArqPpf         = q.Cells["Arquivo PPF"].OriginalValue.ToString();
                j.CaminhoArqCad         = q.Cells["Arquivo CAD"].OriginalValue.ToString();
                j.CaminhoArqCnc         = q.Cells["Arquivo CNC"].OriginalValue.ToString();
                j.PreSelecaoKits        = q.Cells["Pré-carga de ferramentas"].OriginalValue.ToString();
                j.RevisaoJob            = q.Cells["Revisão"].OriginalValue.ToString();
                j.TempoDeCiclo          = q.Cells["Tempo de usinagem"].OriginalValue.ToString();
                j.JobNotesSubject       = q.Cells["JOB_NOTES_SUBJECT"].OriginalValue.ToString();
                j.JobNotes              = q.Cells["JOB_NOTES"].OriginalValue.ToString();
                j.JobNotesFile          = q.Cells["JOB_NOTES_FILE"].OriginalValue.ToString();
                j.FixtureNotesSubject   = q.Cells["JOB_FIXTURE_NOTES_SUBJECT"].OriginalValue.ToString();
                j.FixturesNotes         = q.Cells["JOB_FIXTURE_NOTES"].OriginalValue.ToString();
                j.FixturesNotesFile     = q.Cells["JOB_FIXTURE_NOTES_FILE"].OriginalValue.ToString();
                j.StockNotesSubject     = q.Cells["JOB_STOCK_NOTES_SUBJECT"].OriginalValue.ToString();
                j.StockNotes            = q.Cells["JOB_STOCK_NOTES"].OriginalValue.ToString();
                j.StockNotesFile        = q.Cells["JOB_STOCK_NOTES_FILE"].OriginalValue.ToString();
                j.TrabalhoVisivel       = q.Cells["Visibilidaed_Db"].OriginalValue.ToString();
                //j.TurretWarning = q.Cells[""].OriginalValue.ToString();
                j.DtCriacao             = q.Cells["Data de criação"].OriginalValue.ToString();
                j.DtModificacao         = q.Cells["Data da última modificação"].OriginalValue.ToString();
                j.PartStickOut          = q.Cells["JOB_STICKOUT"].OriginalValue.ToString();
                //j.ProgramId1 = q.Cells[""].OriginalValue.ToString();
                //j.ProgramId2 = q.Cells[""].OriginalValue.ToString();

                Objects.ImplementaNovoFormTela(new FrmTrabalhos_New(j, false));
            }
        }

        /// <summary>
        ///     Método que deleta o trabalho selecionado.
        /// </summary>
        private void DeletaTrabalho()
        {
            if (MessageBox.Show("Você está prestes a deletar o trabalho. Tem certeza?", "Deletar trabalho selecionado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                if (udgv.Selected.Rows.Count == 1)
                {
                    Dictionary<String, object> dic = new Dictionary<string, object>();
                    dic.Add("@JOBID", udgv.Selected.Rows[0].Cells["id"].OriginalValue.ToString());
                    dic.Add("@JOBNAME", udgv.Selected.Rows[0].Cells["Nome do trabalho"].OriginalValue.ToString());

                    Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.DELETA_TRABALHOS_E_VINCULOS, dic);

                    //Salva a informação no banco de dados que o usuário 'realizou alguma tarefa', para manter o histórico do que o idiota está fazendo.
                    Objects.CadastraNovoLog(false, "Foi deletado um trabalho", "FrmTrabalhos", "btnDelete_Click",
                                               String.Format("Usuário deletou o trabalho de id '{0}', nome '{1}'", udgv.Selected.Rows[0].Cells["id"].ToString(), udgv.Selected.Rows[0].Cells["Nome do trabalho"].ToString()),
                                               "", e_TipoErroEx.Informacao);

                    btnPesquisar_Click(new object(), new EventArgs());
                }
            }
        }

        /// <summary>
        ///     Método que cancela o trabalho e as ordens de produção atribuídas à ela.
        /// </summary>
        private void CancelaTrabalho()
        {
            if (MessageBox.Show("Você está prestes a cancelar o trabalho e as ordens de produção vinculadas. Tem certeza?", "Cancelar trabalho selecionado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                if (udgv.Selected.Rows.Count == 1)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("@JOBNAME", udgv.Selected.Rows[0].Cells["Nome do trabalho"].OriginalValue.ToString());
                    dic.Add("@INFO", String.Format("Usuário está cancelou a ordem através do trabalho de nome '{0}'", udgv.Selected.Rows[0].Cells["Nome do trabalho"].OriginalValue.ToString()));
                    dic.Add("@USR", Objects.UsuarioAtual.Login);

                    Objects.CnnBancoEc.ExecutaSql(Consultas_Ec.CANCELA_TRABALHO, dic);
                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CANCELA_ORDEM_DE_PRODUCAO_POR_TRABALHO, dic);

                    //Salva a informação no banco de dados que o usuário 'realizou alguma tarefa', para manter o histórico do que o idiota está fazendo.
                    Objects.CadastraNovoLog(false, "Foi cancelado um trabalho", "FrmTrabalhos", "btnDelete_Click",
                                               String.Format("Usuário cancelou o trabalho de id '{0}', nome '{1}'", udgv.Selected.Rows[0].Cells["id"].ToString(), udgv.Selected.Rows[0].Cells["Nome do trabalho"].ToString()),
                                               "", e_TipoErroEx.Informacao);

                    btnPesquisar_Click(new object(), new EventArgs());
                }
            }
        }

        #endregion

        #region Eventos

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ////Monitora o tempo que o usuário está gastando para programar a peça.
                //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                //sw.Start();

                ConsultaTrabalhos();

                //sw.Stop();
                //String tempo = sw.Elapsed.Milliseconds.ToString();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar trabalhos", "FrmTrabalhos", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                NovoTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar um novo trabalho", "FrmTrabalhos", "btnNew_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EditaTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao editar o trabalho", "FrmTrabalhos", "btnEdit_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeletaTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao deletar o trabalho", "FrmTrabalhos", "btnDelete_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                VisualizaTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar visualizar detalhes do trabalho", "FrmTrabalhos", "btnViewDetails_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CancelaTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar cancelar o trabalho", "FrmTrabalhos", "btnCancelar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            mExporter.MostrarCms();
        }

        private void cms_Opening(object sender, CancelEventArgs e)
        {
            if (udgv.Rows.Count > 0)
            {
                btnNew.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnViewDetails.Enabled = true;

                //Se o trabalho já estiver cancelado, não permito "cancelar" uma segunda vez.
                if (udgv.Selected.Rows[0].Cells["Status_Db"].OriginalValue.ToString() == "5")
                {
                    btnCancelar.Enabled = false;
                    btnComplete.Enabled = false;
                }
                else
                {
                    btnCancelar.Enabled = true;
                    btnComplete.Enabled = true;
                }
            }
            else
            {
                btnNew.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnComplete.Enabled = false;
                btnViewDetails.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }

        private void udgv_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                VisualizaTrabalho();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar visualizar detalhes do trabalho", "FrmTrabalhos", "udgv_DoubleClickRow", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos de foco

        private void txtTrabalho_Enter(object sender, EventArgs e)
        {
            txtTrabalho.Text = "";
        }

        private void txtTrabalho_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTrabalho.Text)) txtTrabalho.Text = "(Todos)";
        }

        private void txtCliente_Enter(object sender, EventArgs e)
        {
            txtCliente.Text = "";
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCliente.Text)) txtCliente.Text = "(Todos)";
        }

        private void txtProgramador_Enter(object sender, EventArgs e)
        {
            txtProgramador.Text = "";
        }

        private void txtProgramador_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtProgramador.Text)) txtProgramador.Text = "(Todos)";
        }

        private void txtMaterial_Enter(object sender, EventArgs e)
        {
            txtMaterial.Text = "";
        }

        private void txtMaterial_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaterial.Text)) txtMaterial.Text = "(Todos)";
        }

        #endregion
    }
}