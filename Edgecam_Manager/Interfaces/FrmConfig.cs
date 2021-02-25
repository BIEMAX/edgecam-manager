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

using System.IO;
using System.ServiceProcess;

namespace Edgecam_Manager
{
    internal partial class FrmConfig : Form
    {

        #region Variáveis globais

        /// <summary>
        ///     DataTable que contém os dados das tabelas e colunas das 
        /// referências automáticas.
        /// </summary>
        private DataTable mDt_Referencias;

        /// <summary>
        ///     Contém a tabela selecionada pelo usuário na aba 'Personalização'.
        /// Isto serve para recarregar as colunas da tabela em caso de edição/
        /// adição de uma nova coluna.
        /// </summary>
        private String mTabelaSelecionada_Personalizacao;

        /// <summary>
        ///     Contém uma flag para saber se o usuário clicou sobre a combo box
        /// para trocar o thema. Se sim, eu aviso ele que só surtira efeito caso
        /// ele reinicie o sistema.
        /// </summary>
        private int mIndiceTemaAnterior = 0;

        #endregion

        #region Instância dos objetos da classe

        public FrmConfig()
        {
            InitializeComponent();

            InicializaControles();
        }

        #endregion

        #region Métodos gerais

        /// <summary>
        ///     Método que inicializa os controles (valores iniciais e etc..).
        /// </summary>
        private void InicializaControles()
        {
            #region Definições da aba 'Notificações'

            cbNotificar.Checked = false;
            label57.Visible = false;
            mtxtTempo.Visible = false;
            label17.Visible = false;
            mtxtTempoAnimacao.Visible = false;
            btnIniciarServico.Visible = false;
            lblStatus.Visible = false;
            cbOrdens.Visible = false;
            cbOrdensErro.Visible = false;
            cbOrcamentos.Visible = false;
            cbInventario.Visible = false;
            cbEstoque.Visible = false;
            cbOrdensAtrasadas.Visible = false;            

            #endregion

            #region Definições da aba 'Configurações regionais'

            icbxCultura.Items.Add(new ComboBoxItem("Português", Properties.Resources.br));
            icbxCultura.Items.Add(new ComboBoxItem("Espanhol", Properties.Resources.es));
            icbxCultura.Items.Add(new ComboBoxItem("Inglês", Properties.Resources.us));
            icbxCultura.SelectedIndex = 0;

            //icbxThemes.Items.Add(new ComboBoxItem("Tema claro", Properties.Resources.theme_white));
            //icbxThemes.Items.Add(new ComboBoxItem("Tema escuro", Properties.Resources.theme_black));
            //icbxThemes.SelectedIndex = 0;

            icbxThemes.Items.Add(new ComboBoxItem("<Padrão>"));

            try
            {
                String[] files = Directory.GetFiles(Path.Combine(Application.StartupPath, "Themes"), "*.isl");

                if (files != null && files.Count() > 0)
                {
                    foreach (string f in files)
                    {
                        icbxThemes.Items.Add(new ComboBoxItem(f.Substring(f.LastIndexOf("\\") + 1)));
                    }
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(false, "Erro ao tentar carregar os themas na configuração", "FrmConfig", 
                "InicializaControles, definições da aba 'configurações gerais'", "", "", e_TipoErroEx.Erro, ex);
            }

            icbxThemes.SelectedIndex = 0;

            #endregion

            #region Definições da aba 'Registro de logs'

            icbxLogs.Items.Add(new ComboBoxItem("(Todos)"));
            icbxLogs.Items.Add(new ComboBoxItem("Erro", Properties.Resources.ex_error));
            icbxLogs.Items.Add(new ComboBoxItem("Aviso", Properties.Resources.ex_alert));
            icbxLogs.Items.Add(new ComboBoxItem("Informação", Properties.Resources.ex_information));
            icbxLogs.Items.Add(new ComboBoxItem("Notificação", Properties.Resources.ex_notification));
            icbxLogs.SelectedIndex = 0;

            dtErro.DateTime = DateTime.Now;

            #endregion

            RemoveControlesInterface(tbPaginaInicial);

            //Objects.DefineColorThemeInterface(this);

            tbPaginaInicial.BackColor = System.Drawing.SystemColors.ControlLightLight;
        }

        /// <summary>
        ///     Método que carrega a licença do cliente e os módulos pertencentes à ela.
        /// </summary>
        private void CarregaLicenca()
        {
            //TODO: TERMINAR AQUI A PARTE DE CARGA DA LICENÇA.

            //SkaLic c = new SkaLic();
            //txt_Li_Customer.Text            = c.GetSingleConfigValue(SkaLic.e_h01x.h02xfffS4A);
            //mtb_Li_Serial.Text              = c.GetSingleConfigValue(SkaLic.e_h01x.h01xfffS4A);
            ////txt_Li_MaintenanceExpiry.Text   = c.GetSingleConfigValue(SkaLic.e_h01x.SkaMaintenanceExpiry);
            //txt_Li_LicenseExpiry.Text       = c.GetSingleConfigValue(SkaLic.e_h01x.h03xfffS4A);
            //txt_Li_EcMgrVersion.Text        = c.GetSingleConfigValue(SkaLic.e_h01x.h04xfffS4A);
            //txt_Li_EcLicense.Text           = c.GetSingleConfigValue(SkaLic.e_h01x.h05xfffS4A);
            //txt_Li_LockCode.Text            = c.GetSingleConfigValue(SkaLic.e_h01x.h10xfffS4A);

            //if (c.GetSingleConfigValue(SkaLic.e_h01x.h06xfffS4A) == "0")
            //    txt_Li_LicenseType.Text = "Local";
            //else txt_Li_LicenseType.Text = "Em rede";

            ////Carrega os módulos
            //string[] modulos = c.GetSingleConfigValue(SkaLic.e_h01x.h14xfffS4A).Split(';');

            //foreach (string s in modulos)
            //{
            //    //if (s.ToUpper() == "ORDERS")
            //    //    cb_Module_Order.Checked = true;

            //    //if (s.ToUpper() == "TRACK_PRODUCTION")
            //    //    cb_Module_FindOrder.Checked = true;

            //    //if (s.ToUpper() == "JOBS")
            //    //    cb_Module_Jobs.Checked = true;

            //    //if (s.ToUpper() == "SHOP_FLOOR")
            //    //    cb_Module_ShopFloor.Checked = true;

            //    //if (s.ToUpper() == "TOOLING_MGR")
            //    //    cb_Module_Tool.Checked = true;

            //    //if (s.ToUpper() == "MACHINE_MGR")
            //    //    cb_Module_Machine.Checked = true;

            //    //if (s.ToUpper() == "EXPORT_JOBS")
            //    //    cb_Module_ExportJobs.Checked = true;

            //    //if (s.ToUpper() == "CNCS")
            //    //    cb_Module_Report.Checked = true;

            //    //if (s.ToUpper() == "REPORTS")
            //    //    cb_Module_Cncs.Checked = true;
            //}
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
        ///     Carrega as extensões não permitidas pelo sistema importador automático Edgecam
        /// </summary>
        private void CarregaExtensoesBloqueadas()
        {
            //dgv_BlockExtensions.DataSource = new SkaLstLockedEx(e_Extensoes.Bloqueados);
            //dgv_BlockExtensions.AutoGenerateColumns = false;
        }

        /// <summary>
        ///     Carrega as extensões permitidas pelo sistema importador automático Edgecam
        /// </summary>
        private void CarregaExtensoesPermitidas()
        {
            //dgv_UnlockedExtensions.DataSource = new SkaLstLockedEx(e_Extensoes.Permitidos);
            //dgv_UnlockedExtensions.AutoGenerateColumns = false;
        }

        /// <summary>
        ///     Carrega os modelos que devem ser ignorados.
        /// </summary>
        private void CarregaModelosIgnorados()
        {
            //dgv_IgnoredModels.DataSource = new SkaLstIgnoredFiles();
            //dgv_IgnoredModels.AutoGenerateColumns = false;
        }

        #endregion

        #region Eventos e métodos para atualizar a interface

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void ueb_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            switch (e.Item.Text.ToUpper().Trim())
            {
                case "NOTIFICAÇÕES":                    Config_Notificacoes(); break;
                case "CONFIGURAÇÕES GERAIS":            Config_Gerais(); break;
                case "REFERÊNCIAS AUTOMÁTICAS":         Config_ReferenciasAutomaticas(); break;
                case "PERSONALIZAÇÃO":                  Config_Personalizacao(); break;
                case "REGISTRO DE LOGS":                Config_Logs(); break;
                case "LICENÇA E MÓDULOS":               Config_Licenca(); break;
                case "CONEXÕES COM OS BANCO DE DADOS":  Config_Bancos(); break;
                case "CONEXÃO COM O SOLIDWORKS PDM":    Config_PDM(); break;
                case "CONEXÃO COM A NUVEM":             Config_Nuvem(); break;
                case "IMPORTAÇÃO DE ORDENS":            Config_ImportaOrdens(); break;
                case "EXPORTAÇÃO DE ORDENS":            Config_ExportaOrdens(); break;
                case "ENCERRAMENTO DE ORDENS":          Config_EncerraOrdens(); break;
                case "ROTAS DE PRODUÇÃO":               Config_Rotas(); break;
                case "AUTO PROGRAMAÇÃO":                Config_AutoProg(); break;
                case "DADOS AUXILIARES":                Config_Dados(); break;
                case "ARQUIVOS AUXILIARES":             Config_Arquivos(); break;
                case "EXTENSÕES PERMITIDAS":            Config_ExtensoesPermitidas(); break;
                case "EXTENSÕES BLOQUEADAS":            Config_ExtensoesBloqueadas(); break;
                case "ARQUIVOS A SEREM IGNORADOS":      Config_ArquivosIgnorar(); break;
                case "IMPOSTOS":                        Config_Impostos(); break;
                case "TARIFAS":                         Config_Tarifas(); break;
                case "MÉTODOS DE PAGAMENTO":            Config_Metodos(); break;
                case "CUSTOS ADICIONAIS":               Config_Custos(); break;
                case "CENTROS DE TRABALHO DE CORTE":    Config_CentrosCorte(); break;
                case "CENTROS DE TRABALHO DE NÃO CORTE": Config_CentrosNaoCorte(); break;
                case "MONTAGEM DAS FERRAMENTAS":        Config_Tools(); break;
                case "CÓPIA DE SEGURANÇA":              Config_Backup(); break;
                case "USUÁRIOS":                        Config_Usuarios(); break;
                case "UNIDADES ORGANIZACIONAIS":        Config_Unidades(); break;
                case "GRUPOS":                          Config_Grupos(); break;
                case "PERMISSÕES":                      Config_Permissoes(); break;
            }
        }

        private void Config_Notificacoes()
        {
            RemoveControlesInterface(tbNotificacoes);
            //  Depois de mostrar a aba notificações, preciso verificar se já
            //existe uma configuração previamente imposta.
            CarregaConfig_Notificacoes();
            VerificaEstadoServico();
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Gerais()
        {
            RemoveControlesInterface(tbCfgGerais);
            CarregaConfig_Regional();
            CarregaConfig_ThemaInterface();
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_ReferenciasAutomaticas()
        {
            RemoveControlesInterface(tbReferenciasAutomaticas);
            CarregaTabelas_Referencias();
            DesabilitaControles_Referencias();
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Personalizacao()
        {
            RemoveControlesInterface(tbPersonalizacao);
            DesabilitaControles_Personalizacao();
            Objects.LimpaOrdenacaoColunasGrid(udgv_Colunas);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Logs()
        {
            RemoveControlesInterface(tbLogs);
            DesabilitaControles_Logs();
            Objects.LimpaOrdenacaoColunasGrid(udgv_Logs);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Licenca()
        {
            RemoveControlesInterface(tbLicenca);
        }

        private void Config_Bancos()
        {
            RemoveControlesInterface(tbConexoes);
            CarregaConfig_ConexoesBancosDados();
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_PDM()
        {
            RemoveControlesInterface(tbPDM);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Nuvem()
        {
            RemoveControlesInterface(tbNuvem);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_ImportaOrdens()
        {
            RemoveControlesInterface(tbImportar);
            DesabilitaControles_Importacao();
            Objects.LimpaOrdenacaoColunasGrid(udgv_Importacao);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_ExportaOrdens()
        {
            RemoveControlesInterface(tbExportar);
            DesabilitaControles_Exportacao();
            Objects.LimpaOrdenacaoColunasGrid(udgv_Exportacao);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_EncerraOrdens()
        {
            RemoveControlesInterface(tbEncerrar);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Rotas()
        {
            RemoveControlesInterface(tbRotas);
        }

        private void Config_AutoProg()
        {
            RemoveControlesInterface(tbAuto);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_Dados()
        {
            RemoveControlesInterface(tbDadosAux);
        }

        private void Config_Arquivos()
        {
            RemoveControlesInterface(tbArquivosAux);
        }

        private void Config_ExtensoesPermitidas()
        {
            RemoveControlesInterface(tbExtAllow);
        }

        private void Config_ExtensoesBloqueadas()
        {
            RemoveControlesInterface(tbExtBlq);
        }

        private void Config_ArquivosIgnorar()
        {
            RemoveControlesInterface(tbArqIgn);
        }

        private void Config_Impostos()
        {
            RemoveControlesInterface(tbImpostos);
        }

        private void Config_Tarifas()
        {
            RemoveControlesInterface(tbTarifas);
        }

        private void Config_Metodos()
        {
            RemoveControlesInterface(tbMetodos);
        }

        private void Config_Custos()
        {
            RemoveControlesInterface(tbCustos);
        }

        private void Config_CentrosCorte()
        {
            RemoveControlesInterface(tbCentrosCorte);
            //Objects.DefineColorThemeInterface(this);
        }

        private void Config_CentrosNaoCorte()
        {
            RemoveControlesInterface(tbCentrosNaoCorte);
        }

        private void Config_Tools()
        {
            RemoveControlesInterface(tbToolsMqn);
        }

        private void Config_Backup()
        {
            RemoveControlesInterface(tbBckp);
        }

        private void Config_Usuarios()
        {
            RemoveControlesInterface(tbUsuarios);
        }

        private void Config_Unidades()
        {
            RemoveControlesInterface(tbUnidades);
        }

        private void Config_Grupos()
        {
            RemoveControlesInterface(tbGrupos);
        }

        private void Config_Permissoes()
        {
            RemoveControlesInterface(tbPermissoes);
        }

        #endregion

        #region Aba 'Notificações'

        /// <summary>
        ///     Salva/Atualiza as configurações no banco de dados para o usuário corrente.
        /// </summary>
        /// <remarks>Configuração imposta por usuário.</remarks>
        private void SalvaConfig_Notificacoes()
        {
            //Essa parte serve para verificar se o usuário já não cadastrou previamente uma configuração.
            int idRegistroAntigo = 0;

            //Aqui eu monto o dicionário para inserir/atualizar os registros.
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@USUARIO", Objects.UsuarioAtual.Login);

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_CONFIGURACAO_NOTIFICACOES_USUARIO_ATUAL, dic);

            if (dt.Rows.Count > 0)
            {
                idRegistroAntigo = Convert.ToInt32(dt.AsEnumerable().Select(r => r["id"].ToString()).FirstOrDefault());

                dic = new Dictionary<string, object>();
                dic.Add("@MOSTRAR", cbNotificar.Checked);

                //Obtem o tempo de notificação.
                TimeSpan tmp = TimeSpan.Parse(String.IsNullOrEmpty(mtxtTempo.Text) ? "00:00:00" : mtxtTempo.Text);
                dic.Add("@TEMPO", tmp.TotalMinutes);

                //Obtem o tempo de animação.
                tmp = TimeSpan.Parse(String.IsNullOrEmpty(mtxtTempoAnimacao.Text) ? "00:00:00" : mtxtTempoAnimacao.Text);
                dic.Add("@TEMPO_ANIMACAO", tmp.TotalMinutes);
                dic.Add("@ORDENS", cbOrdens.Checked);
                dic.Add("@ORDENS_ERRO", cbOrdensErro.Checked);
                dic.Add("@ORCAMENTOS", cbOrcamentos.Checked);
                dic.Add("@INVENTARIO", cbInventario.Checked);
                dic.Add("@ESTOQUE", cbEstoque.Checked);
                dic.Add("@ORDENS_ATRASADAS", cbOrdensAtrasadas.Checked);
                dic.Add("@DATA_ULTMOD", DateTime.Now);
                dic.Add("@ID", idRegistroAntigo);

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_CONFIGURACAO_NOTIFICACOES, dic);
                MessageBox.Show("Configuração atualizada com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //Aqui eu monto o dicionário para inserir/atualizar os registros.
                dic = new Dictionary<string, object>();
                dic.Add("@MOSTRAR", cbNotificar.Checked);

                //Obtem o tempo de notificação.
                TimeSpan tmp = TimeSpan.Parse(String.IsNullOrEmpty(mtxtTempo.Text) ? "00:00:00" : mtxtTempo.Text);
                dic.Add("@TEMPO", tmp.TotalMinutes);

                //Obtem o tempo de animação.
                tmp = TimeSpan.Parse(String.IsNullOrEmpty(mtxtTempoAnimacao.Text) ? "00:00:00" : mtxtTempoAnimacao.Text);
                dic.Add("@TEMPO_ANIMACAO", tmp.TotalMinutes);
                dic.Add("@ORDENS", cbOrdens.Checked);
                dic.Add("@ORDENS_ERRO", cbOrdensErro.Checked);
                dic.Add("@ORCAMENTOS", cbOrcamentos.Checked);
                dic.Add("@INVENTARIO", cbInventario.Checked);
                dic.Add("@ESTOQUE", cbEstoque.Checked);
                dic.Add("@ORDENS_ATRASADAS", cbOrdensAtrasadas.Checked);
                dic.Add("@USUARIO", Objects.UsuarioAtual.Login);
                dic.Add("@DATA_CRIACAO", DateTime.Now);
                dic.Add("@DATA_ULTMOD", DateTime.Now);

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.SALVA_CONFIGURACOES_NOTIFICACOES, dic);
                MessageBox.Show("Configuração salva com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            RemoveControlesInterface(tbPaginaInicial);
        }

        /// <summary>
        ///     Método que carrega as configurações impostas pelo usuário caso existam.
        /// </summary>
        private void CarregaConfig_Notificacoes()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@USUARIO", Objects.UsuarioAtual.Login);

            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.EXISTE_CONFIGURACAO_NOTIFICACOES_USUARIO_ATUAL, dic);

            if (dt.Rows.Count > 0)
            {
                cbNotificar.Checked = Convert.ToBoolean(dt.Rows[0]["MostrarNotificacoes"].ToString());
                mtxtTempo.Text = TimeSpan.FromMinutes(Convert.ToDouble(dt.Rows[0]["Tempo"].ToString())).ToString();
                cbNotificar.Text = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["TempoAnimacao"].ToString())).ToString().Substring(3);
                cbOrdens.Checked = Convert.ToBoolean(dt.Rows[0]["NotifyNovasOrdens"].ToString());
                cbOrdensErro.Checked = Convert.ToBoolean(dt.Rows[0]["NotifyNovasOrdensErro"].ToString());
                cbOrcamentos.Checked = Convert.ToBoolean(dt.Rows[0]["NotifyNovosOrçamentos"].ToString());
                cbInventario.Checked = Convert.ToBoolean(dt.Rows[0]["NotifyInventario"].ToString());
                cbEstoque.Checked = Convert.ToBoolean(dt.Rows[0]["NotifyEstoqueMinimo"].ToString());
                cbOrdensAtrasadas.Checked = Convert.ToBoolean(dt.Rows[0]["NotifyOrdensAtrasadas"].ToString());
            }
        }

        /// <summary>
        ///     Verifica se o serviço background já está em execução e define o estado para
        /// o usuário.
        /// </summary>
        private void VerificaEstadoServico()
        {
            try
            {
                //Caso o serviço já estiver em execução, apenas inicio mudo o estado.
                if (WindowsServiceMgr.IsServiceRunning("Edgecam Manager Background service"))
                {
                    lblStatus.Text = "Iniciado";
                    lblStatus.BackColor = Color.Green;
                    btnIniciarServico.Text = "Parar";
                }
                else
                {
                    lblStatus.Text = "Parado";
                    lblStatus.BackColor = Color.Red;
                    btnIniciarServico.Text = "Iniciar";
                }
            }
            catch
            {
                lblStatus.Text = "Parado";
                lblStatus.BackColor = Color.Red;
                btnIniciarServico.Text = "Iniciar";
            }

        }

        /// <summary>
        ///     Inicia o serviço para monitoramento de notifcações.
        /// </summary>
        private void IniciaBackgroundService()
        {
            //Help link: https://stackoverflow.com/questions/255056/install-a-net-windows-service-without-installutil-exe
            #if !DEBUG
                String dirServico = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "");
            #else
            //String dirServico = @"C:\@Backup\@@@@@@@backup\Projects\@Edgecam_Manager - 2018 R2 - beta 2\Edgecam_Manager_BackgroundService\bin\Release\Edgecam_Manager_BackgroundService.exe";
                String dirServico = @"C:\Temp\Edgecam_Manager_BackgroundService.exe";
            #endif

            Cursor = Cursors.WaitCursor;

            if (lblStatus.Text.ToUpper() == "INICIADO")
            {
                WindowsServiceMgr.StopService("Edgecam Manager Background service");

                lblStatus.Text = "Parado";
                lblStatus.BackColor = Color.Red;
                btnIniciarServico.Text = "Iniciar";
            }
            //Se não estiver iniciado, o usuário quer iniciar!!
            else
            {
                if (File.Exists(dirServico))
                {
                    if (!WindowsServiceMgr.ServiceIsInstalled("Edgecam Manager Background service"))
                    {
                        WindowsServiceMgr.InstallAndStart("Edgecam Manager Background service", "Edgecam Manager Background service", dirServico);

                        lblStatus.Text = "Iniciado";
                        lblStatus.BackColor = Color.Green;
                        btnIniciarServico.Text = "Parar";
                    }
                    else
                    {
                        WindowsServiceMgr.InstallAndStart("Edgecam Manager Background service", "Edgecam Manager Background service", dirServico);

                        lblStatus.Text = "Iniciado";
                        lblStatus.BackColor = Color.Green;
                        btnIniciarServico.Text = "Parar";
                    }
                }
                else throw new FileNotFoundException("Serviço não foi localizado");
            }

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///     Apresente/esconde as configurações para o usuário realizar.
        /// </summary>
        private void cbNotificar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNotificar.Checked)
            {
                label57.Visible = true;

                mtxtTempo.Visible = true;
                mtxtTempo.Text = "00:01:00";

                label17.Visible = true;
                
                mtxtTempoAnimacao.Visible = true;
                mtxtTempoAnimacao.Text = "00:05";

                btnIniciarServico.Visible = true;
                lblStatus.Visible = true;
                cbOrdens.Visible = true;
                cbOrdensErro.Visible = true;
                cbOrcamentos.Visible = true;
                cbInventario.Visible = true;
                cbEstoque.Visible = true;
                cbOrdensAtrasadas.Visible = true;
            }
            else
            {
                label57.Visible = false;
                mtxtTempo.Visible = false;
                label17.Visible = false;
                mtxtTempoAnimacao.Visible = false;
                btnIniciarServico.Visible = false;
                lblStatus.Visible = false;
                cbOrdens.Visible = false;
                cbOrdensErro.Visible = false;
                cbOrcamentos.Visible = false;
                cbInventario.Visible = false;
                cbEstoque.Visible = false;
                cbOrdensAtrasadas.Visible = false;   
            }
        }

        /// <summary>
        ///     Inicia o serviço.
        /// </summary>
        private void btnIniciarServico_Click(object sender, EventArgs e)
        {
            try
            {
                IniciaBackgroundService();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao iniciar serviço", "FrmConfig_AbaNotificações", "btnIniciarServico_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Salva as configurações no banco de dados por usuário.
        /// </summary>
        private void btnSalvaNotificacoes_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaConfig_Notificacoes();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao salvar configurações", "FrmConfig_AbaNotificacoes", "btnSalvaNotificacoes_Click", "<None>", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Configurações gerais'

        /// <summary>
        ///     Salva a configuração no arquivo de configuração XML.
        /// </summary>
        private void SalvaConfig_Gerais()
        {
            CustomXmlConfig xml = new CustomXmlConfig("SkaConfig.xml");

            if (icbxCultura.SelectedIndex == 0) xml.UpdateXmlConfigValue("IDIOMA", "pt-BR");
            if (icbxCultura.SelectedIndex == 1) xml.UpdateXmlConfigValue("IDIOMA", "es");
            if (icbxCultura.SelectedIndex == 2) xml.UpdateXmlConfigValue("IDIOMA", "en-US");

            //O usuário pode escolher em ficar com o padrão (o atual dev).
            xml.UpdateXmlConfigValue("THEME", icbxThemes.SelectedItem.ToString());                

            MessageBox.Show("Configuração salva com êxito", "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RemoveControlesInterface(tbPaginaInicial);
        }

        /// <summary>
        ///     Carrega configuração caso ela exista.
        /// </summary>
        private void CarregaConfig_Regional()
        {
            CustomXmlConfig xml = new CustomXmlConfig("SkaConfig.xml");

            String cfg = xml.StrXmlSimpleConfigValue("IDIOMA");

            if (!String.IsNullOrEmpty(cfg.ToUpper().Trim()))
            {
                icbxCultura.SelectedItem = cfg;
            }
            else icbxCultura.SelectedIndex = 0;
        }

        /// <summary>
        ///     Carrega as configurações da interface.
        /// </summary>
        private void CarregaConfig_ThemaInterface()
        {
            CustomXmlConfig xml = new CustomXmlConfig("SkaConfig.xml");

            String cfg = xml.StrXmlSimpleConfigValue("THEME");

            if (!String.IsNullOrEmpty(cfg.ToUpper().Trim()))
            {
                for (int x = 0; x < icbxThemes.Items.ItemsBase.Count; x++)
                {
                    if (cfg.ToUpper().Trim() == icbxThemes.Items[x].ToString().ToUpper().Trim())
                    {
                        mIndiceTemaAnterior = x;
                        icbxThemes.SelectedIndex  = x;
                        break;
                    }
                }
            }
            else icbxThemes.SelectedIndex = 0;
        }

        private void icbxThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (icbxThemes.SelectedIndex != mIndiceTemaAnterior) label26.Visible = true;
            else label26.Visible = false;
        }

        private void btnSalvaCfgRegional_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaConfig_Gerais();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao salvar configurações", "FrmConfig_AbaNotificacoes", "btnSalvaCfgRegional_Click", "<None>", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Referências automáticas'

        /// <summary>
        ///     Método que carrega as tabelas e suas colunas do banco de dados do manager.
        /// </summary>
        private void CarregaTabelas_Referencias()
        {
            mDt_Referencias = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_TABELAS);

            cbTabelas.Items.Clear();
            cbTabelas.Items.Add("<Selecione>");
            cbTabelas.Items.AddRange(mDt_Referencias.AsDataView().ToTable(true, "TABLE_NAME").AsEnumerable().Select(r => r.ItemArray[0].ToString()).OrderBy(u => u).ToArray());
            cbTabelas.SelectedIndex = 0;
        }

        /// <summary>
        ///     Desabilita os controles de edição e deletar das referências automáticas.
        /// </summary>
        private void DesabilitaControles_Referencias()
        {
            btnEdit_RefAuto.Enabled = false;
            btnDelete_RefAuto.Enabled = false;
        }

        /// <summary>
        ///     Habilita os controles de edição e deletar das referências automáticas.
        /// </summary>
        private void HabilitaControles_Referencias()
        {
            btnEdit_RefAuto.Enabled = true;
            btnDelete_RefAuto.Enabled = true;
        }

        /// <summary>
        ///     Método que carrega as Referências automáticas salvas no banco de dados.
        /// </summary>
        private void Consulta_ReferenciasAutomaticas()
        {
            String sql = Consultas_EcMgr.CONSULTA_TODAS_REFERENCIAS_AUTOMATICAS;

            if (cbTabelas.SelectedIndex > 0 && cbTabelas.Text != "")
                sql += String.Format(" WHERE id != 0 AND Tabela = '{0}'", cbTabelas.Text);

            if (cbColunas.SelectedIndex > 0 && cbColunas.Text != "")
                sql += String.Format(" AND Coluna = '{0}'", cbColunas.Text);

            udgv_Referencias.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(sql);

            if (udgv_Referencias.Rows.Count > 0)
                HabilitaControles_Referencias();
            else DesabilitaControles_Referencias();
        }

        /// <summary>
        ///     Habilita a edição da referência automática existente.
        /// </summary>
        private void EditaReferenciaAutomatica()
        {
            ReferenciaAutomatica r = new ReferenciaAutomatica();
            
            if (udgv_Referencias.Selected.Rows.Count > 1)
                Messages.Msg007();
            else if (udgv_Referencias.Rows.Count > 0)
            {
                var q = udgv_Referencias.Selected.Rows.Count == 0 ? udgv_Referencias.Rows[0] : udgv_Referencias.Selected.Rows[0];

                r.Id = Convert.ToInt32(q.Cells["id"].OriginalValue.ToString());
                r.NomeTabela = q.Cells["Tabela"].OriginalValue.ToString();
                r.NomeColuna = q.Cells["Coluna"].OriginalValue.ToString();
                r.Prefixo = q.Cells["Prefixo"].OriginalValue.ToString();
                r.Incremento = Convert.ToInt32(q.Cells["Incremento"].OriginalValue.ToString());
                r.ValorInicial = Convert.ToInt32(q.Cells["ValorInicial"].OriginalValue.ToString());
                r.ValorFinal = Convert.ToInt32(q.Cells["ValorFinal"].OriginalValue.ToString());
                r.ValorContadorAtual = Convert.ToInt32(q.Cells["Valor do contador atual"].OriginalValue.ToString());
                r.InserirZeros = Convert.ToBoolean(q.Cells["InserirZeros"].OriginalValue.ToString());
                r.ZerosAEsquerda = Convert.ToBoolean(q.Cells["ZerosAesquerda"].OriginalValue.ToString());
                r.ZerosADireita = Convert.ToBoolean(q.Cells["ZerosAdireita"].OriginalValue.ToString());
                r.NumZerosInserir = Convert.ToInt32(q.Cells["NumZerosInserir"].OriginalValue.ToString());
                r.DtUltMod = Convert.ToDateTime(q.Cells["UltDataMod"].OriginalValue.ToString());

                FrmConfig_RefAuto frm = new FrmConfig_RefAuto(r);
                frm.ShowDialog();

                //Recarrega as referências automáticas.
                Consulta_ReferenciasAutomaticas();
            }
        }

        /// <summary>
        ///     Deleta uma referência selecionada pelo usuário
        /// </summary>
        private void DeletaReferenciaAutomatica()
        {
            if (udgv_Referencias.Rows.Count > 0)
            {
                if (MessageBox.Show("Você deseja realmente excluir a referência automática selecionada?", "Remover referência", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    var q = udgv_Referencias.Selected.Rows.Count == 0 ? udgv_Referencias.Rows[0] : udgv_Referencias.Selected.Rows[0];

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_REFERENCIA_AUTOMATICA, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });

                    Objects.CadastraNovoLog(false, String.Format("Remoção de referência automática de id '{0}'", q.Cells["id"].OriginalValue.ToString()), "FrmConfig_AbaNotificacoes", "Deletar", "DeletaReferenciaAutomatica", "Consultas_EcMgr.DELETA_REFERENCIA_AUTOMATICA", e_TipoErroEx.Informacao);

                    MessageBox.Show("Exclusão da referência concluída com êxito", "Êxito ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Consulta_ReferenciasAutomaticas();
                }
            }
        }

        private void btnPesquisar_ConfigAuto_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_ReferenciasAutomaticas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao pesquisar referências automáticas", "FrmConfig_AbaCfgAutomaticas", "btnPesquisar_ConfigAuto_Click", "<None>", "Consultas_EcMgr.CONSULTA_TODAS_REFERENCIAS_AUTOMATICAS", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnNew_RefAuto_Click(object sender, EventArgs e)
        {
            try
            {
                FrmConfig_RefAuto frm = new FrmConfig_RefAuto();
                frm.ShowDialog();

                //Recarrega as referências automáticas.
                Consulta_ReferenciasAutomaticas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar referências automáticas", "FrmConfig_AbaCfgAutomaticas", "btnNew_RefAuto_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVA_REFERENCIA_AUTOMATICA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_RefAuto_Click(object sender, EventArgs e)
        {
            try
            {
                EditaReferenciaAutomatica();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao editar referências automáticas", "FrmConfig_AbaCfgAutomaticas", "btnEdit_RefAuto_Click", "<None>", "Consultas_EcMgr.ATUALIZA_REFERENCIA_AUTOMATICA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDelete_RefAuto_Click(object sender, EventArgs e)
        {
            try
            {
                DeletaReferenciaAutomatica();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao deletar referências automática", "FrmConfig_AbaCfgAutomaticas", "btnDelete_RefAuto_Click", "<None>", "Consultas_EcMgr.DELETA_REFERENCIA_AUTOMATICA", e_TipoErroEx.Erro, ex);
            }
        }

        private void cbTabelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTabelas.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione primeiro uma tabela", "Ação não permitida", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                cbColunas.Items.Clear();
                cbColunas.Items.Add("<Selecione>");
                cbColunas.Items.AddRange(mDt_Referencias.AsEnumerable().Where(x => x.ItemArray[0].ToString().ToUpper() == cbTabelas.SelectedItem.ToString().ToUpper()).Select(y => y.ItemArray[1].ToString()).OrderBy(u => u).ToArray());
                cbColunas.SelectedIndex = 0;
            }
        }

        #endregion

        #region Aba 'Personalização

        /// <summary>
        ///     Desabilita os controles da interface de personalização (deletar coluna).
        /// </summary>
        private void DesabilitaControles_Personalizacao()
        {
            btnEdit_Column.Enabled = false;
            btnDelete_Column.Enabled = false;
        }

        /// <summary>
        ///     Habilita os controles da interface de personalização (deletar coluna).
        /// </summary>
        private void HabilitaControles_Personalizacao()
        {
            btnEdit_Column.Enabled = true;
            btnDelete_Column.Enabled = true;
        }

        /// <summary>
        ///     Método responsável por abrir a interface para seleção de uma determinada tabela.
        /// </summary>
        private void SelecionaTabela()
        {
            FrmConfig_SelecionaTabela frm = new FrmConfig_SelecionaTabela();
            frm.ShowDialog();

            mTabelaSelecionada_Personalizacao = frm._TabelaSelecionada;

            if (!String.IsNullOrEmpty(mTabelaSelecionada_Personalizacao))
            {
                CarregaColunasFromTabela(mTabelaSelecionada_Personalizacao);
                btnSelecionaTabela.Text = "Alterar tabela";
            }
        }

        /// <summary>
        ///     Carrega as colunas do banco de dados na interface para o usuário.
        /// </summary>
        /// <param name="NomeTabela">Nome da tabela</param>
        private void CarregaColunasFromTabela(String NomeTabela)
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_NOMES_COLUNAS_PERSONALIZAR, new Dictionary<string, object> { { "@NOME_TABELA", NomeTabela } });

            // Cria as colunas de armazenamento dos ícones da interface.
            DataColumn clImg = new DataColumn("Criado", typeof(System.Drawing.Image));
            dt.Columns.Add(clImg);

            // Adiciona os ícones na tuplas da table (ícones da prioridade)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Ícones de visibilidade
                if (dt.Rows[i]["Criado por"].ToString().ToUpper() == "SISTEMA") dt.Rows[i]["Criado"] = Edgecam_Manager.Properties.Resources.system;
                else dt.Rows[i]["Criado"] = Edgecam_Manager.Imagens_NewLookInterface.usuario_azul;
            }

            udgv_Colunas.DataSource = dt;
        }

        /// <summary>
        ///     Método que adiciona uma nova coluna no banco de dados.
        /// </summary>
        private void AdicionaNovaColunaTabela()
        {
            FrmConfig_AdicionaColuna frm = new FrmConfig_AdicionaColuna(mTabelaSelecionada_Personalizacao);
            frm.ShowDialog();

            Objects.CadastraNovoLog(false, String.Format("Usuário '{0}' adicionou a coluna '{1}' na tabela '{2}'", Objects.UsuarioAtual.Login, frm._NomeColunaPersonalizado, mTabelaSelecionada_Personalizacao),
                                       "FrmConfig_AbaPersonalização", "AdicionaNovaColunaTabela", "<None>", "", e_TipoErroEx.Informacao);

            CarregaColunasFromTabela(mTabelaSelecionada_Personalizacao);
        }

        /// <summary>
        ///     Edita uma coluna existente, criada previamente pelo usuário.
        /// </summary>
        private void EditaColuna()
        {
            var q = udgv_Colunas.Selected.Rows.Count == 0 ? udgv_Colunas.Rows[0] : udgv_Colunas.Selected.Rows[0];

            if (q != null && q.Cells["Criado por"].OriginalValue.ToString().ToUpper() == "USUÁRIO")
            {
                FrmConfig_AdicionaColuna frm = new FrmConfig_AdicionaColuna(mTabelaSelecionada_Personalizacao, q.Cells["Coluna"].OriginalValue.ToString(), q.Cells["Tipo"].OriginalValue.ToString());
                frm.ShowDialog();

                CarregaColunasFromTabela(mTabelaSelecionada_Personalizacao);
            }
        }

        /// <summary>
        ///     Deleta uma coluna que foi criado pelo usuário.
        /// </summary>
        private void DeletaColuna()
        {
            var q = udgv_Colunas.Selected.Rows.Count == 0 ? udgv_Colunas.Rows[0] : udgv_Colunas.Selected.Rows[0];

            if (q != null && q.Cells["Criado por"].OriginalValue.ToString().ToUpper() == "USUÁRIO" && MessageBox.Show("Você deseja mesmo deletar a coluna selecionada?", "Remover coluna", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_CONSTRAINT_COLUNA.Replace("@TABELA", mTabelaSelecionada_Personalizacao).Replace("@COLUNA", q.Cells["Coluna"].OriginalValue.ToString()));
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_COLUNA.Replace("@TABELA", mTabelaSelecionada_Personalizacao).Replace("@COLUNA", q.Cells["Coluna"].OriginalValue.ToString()));

                Objects.CadastraNovoLog(false, String.Format("Usuário removeu da tabela '{0}', a coluna '{1}'", mTabelaSelecionada_Personalizacao, q.Cells["Coluna"].OriginalValue.ToString()), "FrmConfig_AbaPersonalizacao", "btnDelete_Column_Click", "<None>", "Consultas_EcMgr.DELETA_COLUNA", e_TipoErroEx.Aviso);

                MessageBox.Show("Remoção da coluna concluído", "Êxito ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregaColunasFromTabela(mTabelaSelecionada_Personalizacao);
            }
        }

        /// <summary>
        ///     Método que verifica se o usuário pode editar a coluna selecionada.
        /// </summary>
        private void VerificaCriadorColuna()
        {
            var q = udgv_Colunas.Selected.Rows.Count == 0 ? udgv_Colunas.Rows[0] : udgv_Colunas.Selected.Rows[0];

            if (q.Cells["Criado por"].OriginalValue.ToString().ToUpper() == "SISTEMA")
                DesabilitaControles_Personalizacao();
            else if (q.Cells["Criado por"].OriginalValue.ToString().ToUpper() == "USUÁRIO")
                HabilitaControles_Personalizacao();
        }

        /// <summary>
        ///     Abre o formulário para selecioar uma tabela para edição.
        /// </summary>
        private void btnSelecionaTabela_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionaTabela();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao selecionar tabela", "FrmConfig_AbaPersonalizacao", "btnSelecionaTabela_Click", "<None>", "Consultas_EcMgr.CONSULTA_NOMES_TABELAS_PERSONALIZAR", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Ao clicar com o direito sobre a grade de dados da aba 'Personalização',
        /// valida se a célula que o usuário está tentando deletar é editável (não é
        /// do sistema).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cms_Personalizacao_Opening(object sender, CancelEventArgs e)
        {
            VerificaCriadorColuna();
        }

        private void btnNew_Column_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionaNovaColunaTabela();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar coluna", "FrmConfig_AbaPersonalizacao", "btnNew_Column_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVA_COLUNA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_Column_Click(object sender, EventArgs e)
        {
            try
            {
                EditaColuna();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar coluna", "FrmConfig_AbaPersonalizacao", "btnNew_Column_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVA_COLUNA", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDelete_Column_Click(object sender, EventArgs e)
        {
            try
            {
                DeletaColuna();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao deletar coluna", "FrmConfig_AbaPersonalizacao", "btnDelete_Column_Click", "<None>", "Consultas_EcMgr.DELETA_COLUNA", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Registro de logs'

        /// <summary>
        ///     Desabilita os controles da interface de logs.
        /// </summary>
        private void DesabilitaControles_Logs()
        {
            dtErro.Enabled = false;
        }

        private void Consulta_Logs()
        {
            udgv_Logs.DataSource = SQLQueries.Consulta_Logs(icbxLogs.SelectedIndex - 1, cbxUsarData.Checked ? dtErro.DateTime.ToString("yyyy-MM-dd") : "");
        }

        private void VisualizaDetalhes_Logs(Infragistics.Win.UltraWinGrid.UltraGridRow LinhaSelecionada)
        {
            if (LinhaSelecionada != null)
            {
                FrmConfig_ViewLog frm = new FrmConfig_ViewLog(LinhaSelecionada.Cells["id"].OriginalValue.ToString());
                frm.ShowDialog();
            }
        }

        private void btnPesquisar_Logs_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_Logs();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar logs", "FrmConfig_AbaLogs", "btnPesquisar_Logs_Click", "<None>", "Consultas_Log.CONSULTA_LOGS", e_TipoErroEx.Erro, ex);
            }
        }

        private void udgv_Logs_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            //Link de ajuda:
            //http://help.infragistics.com/Help/Doc/WinForms/2012.1/CLR2.0/html/Infragistics2.Win.UltraWinGrid.v12.1~Infragistics.Win.UltraWinGrid.UltraGrid~DoubleClickRow_EV.html
            try
            {
                if (udgv_Logs.Rows.Count <= 0) return;
                VisualizaDetalhes_Logs(e.Row);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao tentar visualizar detalhes do log", "FrmConfig_AbaLogs", "udgv_Logs_DoubleClick", "<None>", "Consultas_Log.CONSULTA_LOG_POR_ID", e_TipoErroEx.Erro, ex);
            }
        }

        private void cbxUsarData_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUsarData.Checked)
                dtErro.Enabled = true;
            else dtErro.Enabled = false;
        }

        #endregion

        #region Aba 'Conexões com os bancos de dados'

        private void CarregaConfig_ConexoesBancosDados()
        {
            CustomXmlConfig xml = new CustomXmlConfig("SkaConfig.xml");
            txt_Cn_SqlEc.Text = xml.StrXmlSimpleConfigValue("SERVER_EC");
            txt_Cn_DbEc.Text = xml.StrXmlSimpleConfigValue("BANCO_EC");
            txt_Cn_EcUser.Text = xml.StrXmlSimpleConfigValue("USER_EC");
            txt_Cn_EcPass.Text = xml.StrXmlSimpleConfigValue("PASS_EC");

            txt_Cn_SqlEcMgr.Text = xml.StrXmlSimpleConfigValue("SERVER_AUX");
            txt_Cn_DbEcMgr.Text = xml.StrXmlSimpleConfigValue("BANCO_AUX");
            txt_Cn_EcMgrUser.Text = xml.StrXmlSimpleConfigValue("USER_AUX");
            txt_Cn_EcMgrPass.Text = xml.StrXmlSimpleConfigValue("PASS_AUX");

            txt_Cn_SqlEcMgrLog.Text = xml.StrXmlSimpleConfigValue("SERVER_LOG");
            txt_Cn_DbEcMgrLog.Text = xml.StrXmlSimpleConfigValue("BANCO_LOG");
            txt_Cn_EcMgrLogUser.Text = xml.StrXmlSimpleConfigValue("USER_LOG");
            txt_Cn_EcMgrLogPass.Text = xml.StrXmlSimpleConfigValue("PASS_LOG");
        }

        private void SalvaConfig_BancosDeDados()
        {
            CustomXmlConfig xml = new CustomXmlConfig("SkaConfig.xml");
            xml.UpdateXmlConfigValue("SERVER_EC", txt_Cn_SqlEc.Text);
            xml.UpdateXmlConfigValue("BANCO_EC", txt_Cn_DbEc.Text);
            xml.UpdateXmlConfigValue("USER_EC", txt_Cn_EcUser.Text);
            xml.UpdateXmlConfigValue("PASS_EC", txt_Cn_EcPass.Text);

            xml.UpdateXmlConfigValue("SERVER_AUX", txt_Cn_SqlEcMgr.Text);
            xml.UpdateXmlConfigValue("BANCO_AUX", txt_Cn_DbEcMgr.Text);
            xml.UpdateXmlConfigValue("USER_AUX", txt_Cn_EcMgrUser.Text);
            xml.UpdateXmlConfigValue("PASS_AUX", txt_Cn_EcMgrPass.Text);

            xml.UpdateXmlConfigValue("SERVER_LOG", txt_Cn_SqlEcMgrLog.Text);
            xml.UpdateXmlConfigValue("BANCO_LOG", txt_Cn_DbEcMgrLog.Text);
            xml.UpdateXmlConfigValue("USER_LOG", txt_Cn_EcMgrLogUser.Text);
            xml.UpdateXmlConfigValue("PASS_LOG", txt_Cn_EcMgrLogPass.Text);

            xml.UpdateXmlConfigValue("MANTER_CONECTADO", "FALSE");

            Objects.CadastraNovoLog(false, String.Format("Usuário '{0}' alterou as configurações dos bancos de dados", Objects.UsuarioAtual.Login), "FrmConfig_AbaBancos", "btnSalvaCfgBancos_Click", "<None>", "<None>", e_TipoErroEx.Informacao);
            MessageBox.Show("Reinicie o sistema para as configurações serem aplicadas", "Configuração salva com êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_Test_Ec_Click(object sender, EventArgs e)
        {
            try
            {
                Sql s = new Sql(txt_Cn_SqlEc.Text, txt_Cn_DbEc.Text, txt_Cn_EcUser.Text, txt_Cn_EcPass.Text);
                s.ConectaSQL();
                s.DesconectaSQL();
                MessageBox.Show("Conexão estabelecida com êxito em relação ao banco de dados do Edgecam.",
                                "Conexão concluída com êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Não foi possível se conectar ao banco de dados do Edgecam",
                                "Erro na conexão com o banco de dados.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_Test_EcMgr_Click(object sender, EventArgs e)
        {
            try
            {
                Sql s = new Sql(txt_Cn_SqlEcMgr.Text, txt_Cn_DbEcMgr.Text, txt_Cn_EcMgrUser.Text, txt_Cn_EcMgrPass.Text);
                s.ConectaSQL();
                s.DesconectaSQL();
                MessageBox.Show("Conexão estabelecida com êxito em relação ao banco de dados do Edgecam Manager.",
                                "Conexão concluída com êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Não foi possível se conectar ao banco de dados do Edgecam Manager",
                                "Erro na conexão com o banco de dados.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_Test_EcMgrLog_Click(object sender, EventArgs e)
        {
            try
            {
                Sql s = new Sql(txt_Cn_SqlEcMgrLog.Text, txt_Cn_DbEcMgrLog.Text, txt_Cn_EcMgrLogUser.Text, txt_Cn_EcMgrLogPass.Text);
                s.ConectaSQL();
                s.DesconectaSQL();
                MessageBox.Show("Conexão estabelecida com êxito em relação ao banco de dados de logs do Edgecam Manager.",
                                "Conexão concluída com êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Não foi possível se conectar ao banco de dados do Edgecam Manager",
                                "Erro na conexão com o banco de dados.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSalvaCfgBancos_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaConfig_BancosDeDados();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao salvar configurações dos bancos de dados", "FrmConfig_AbaConexõesBancos", "btnSalvaCfgBancos_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Importação de ordens de produção'

        /// <summary>
        ///     Desabilita os controles da interface de importação de ordens de produção.
        /// </summary>
        private void DesabilitaControles_Importacao()
        {
            btnEdit_Field.Enabled = false;
            btnDelete_Field.Enabled = false;
            btnInactivate_Field.Enabled = false;
            btnActivate_Field.Enabled = false;
            btnGerarExemploXmlImporter.Enabled = false;
        }

        /// <summary>
        ///     Habilita os controles da interface de importação de ordens de produção.
        /// </summary>
        private void HabilitaControles_Importacao()
        {
            btnEdit_Field.Enabled = true;
            btnDelete_Field.Enabled = true;
            btnInactivate_Field.Enabled = true;
            btnActivate_Field.Enabled = true;
            btnGerarExemploXmlImporter.Enabled = true;
        }

        /// <summary>
        ///     Consulta todos os campos de importação de ordens de produção.
        /// </summary>
        private void Consulta_CamposImportacao()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_CAMPOS_IMPORTAR);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Cria as colunas de armazenamento dos ícones da interface.
                DataColumn clImg = new DataColumn("Ativo", typeof(System.Drawing.Image));
                dt.Columns.Add(clImg);

                // Adiciona os ícones na tuplas da table (ícones da prioridade)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Erro = 0
                    //Aviso = 1
                    //Informacao = 2
                    //Notificacao = 3

                    //Ícones de visibilidade
                    if (dt.Rows[i]["Ativo_Db"].ToString().ToUpper() == "TRUE") dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                    else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
                }

                udgv_Importacao.DataSource = dt;
            }

            if (udgv_Importacao.Rows.Count > 0)
                HabilitaControles_Importacao();
            else DesabilitaControles_Importacao();
        }

        /// <summary>
        ///     Método que abre um formulário para o usuário adicionar um novo campo para importação.
        /// </summary>
        private void AdicionaCampoImportacao()
        {
            FrmConfig_CamposImportar frm = new FrmConfig_CamposImportar();
            frm.ShowDialog();

            btnPesquisar_Importacao_Click(new object(), new EventArgs());
        }

        /// <summary>
        ///     Edita um determinado campo designado para importação.
        /// </summary>
        private void EditaCampoImportacao()
        {
            var q = udgv_Importacao.Selected.Rows.Count == 0 ? udgv_Importacao.Rows[0] : udgv_Importacao.Selected.Rows[0];

            if (q != null)
            {
                CampoImportar c = new CampoImportar() 
                {
                    Id              = q.Cells["id"].OriginalValue.ToString(),                    
                    NomeCampo       = q.Cells["Nome do campo"].OriginalValue.ToString(),
                    NomeImportar    = q.Cells["Nome do elemento para importação"].OriginalValue.ToString(),
                    Ativo           = Convert.ToBoolean(q.Cells["Ativo_Db"].OriginalValue.ToString()),
                    AceitaNulos     = Convert.ToBoolean(q.Cells["AceitarValoresVazios"].OriginalValue.ToString()),
                    ValorPadrao     = q.Cells["ValorPadrao"].OriginalValue.ToString()
                };

                FrmConfig_CamposImportar frm = new FrmConfig_CamposImportar(c);
                frm.ShowDialog();

                btnPesquisar_Importacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Deleta o campo para importação das ordens de produção.
        /// </summary>
        private void DeletaCampoImportacao()
        {
            if (MessageBox.Show("Você deseja mesmo deletar o campo selecionada?", "Remover campo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                var q = udgv_Importacao.Selected.Rows.Count == 0 ? udgv_Importacao.Rows[0] : udgv_Importacao.Selected.Rows[0];

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_CAMPO_IMPORTACAO, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                Objects.CadastraNovoLog(false, String.Format("Usuário removeu o campo '{0}'", q.Cells["Nome do campo"].OriginalValue.ToString()), "FrmConfig_AbaImportacao", "btnDelete_Field_Click", "<None>", "Consultas_EcMgr.DELETA_CAMPO_IMPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Remoção do campo concluído", "Êxito ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnPesquisar_Importacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Inativa um campo de importação.
        /// </summary>
        private void InativaCampoImportacao()
        {
            if (MessageBox.Show("Você deseja mesmo inativar o campo selecionado?", "Inativar campo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                var q = udgv_Importacao.Selected.Rows.Count == 0 ? udgv_Importacao.Rows[0] : udgv_Importacao.Selected.Rows[0];

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.INATIVA_CAMPO_IMPORTACAO, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                Objects.CadastraNovoLog(false, String.Format("Usuário inativou o campo '{0}'", q.Cells["Nome do campo"].OriginalValue.ToString()), "FrmConfig_AbaImportacao", "btnInactivate_Field_Click", "<None>", "Consultas_EcMgr.INATIVA_CAMPO_IMPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Inativação do campo concluído", "Êxito ao inativar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnPesquisar_Importacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Ativa um campo de importação.
        /// </summary>
        private void AtivaCampoImportacao()
        {
            if (MessageBox.Show("Você deseja mesmo ativar o campo selecionado?", "Ativar campo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                var q = udgv_Importacao.Selected.Rows.Count == 0 ? udgv_Importacao.Rows[0] : udgv_Importacao.Selected.Rows[0];

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATIVA_CAMPO_IMPORTACAO, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                Objects.CadastraNovoLog(false, String.Format("Usuário ativou o campo '{0}'", q.Cells["Nome do campo"].OriginalValue.ToString()), "FrmConfig_AbaImportacao", "btnActivate_Field_Click", "<None>", "Consultas_EcMgr.ATIVA_CAMPO_IMPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Ativação do campo concluído", "Êxito ao ativar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnPesquisar_Importacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Método que gera a partir do ultra grid de campos de importação para ordens
        /// de produção, um exemplo de XML.
        /// </summary>
        private void GeraXmlExemploImportacao()
        {
            if (udgv_Importacao.Rows.Count > 0)
            {
                List<LstElemXml> lstElementos = new List<LstElemXml>();

                for (int x = 0; x < udgv_Importacao.Rows.Count; x++)
                {
                    //Só exporta para o XML campos determinados como ativos.
                    if (udgv_Importacao.Rows[x].Cells["Ativo_Db"].OriginalValue.ToString().ToUpper() == "FALSE") continue;

                    var q = udgv_Importacao.Rows[x].Cells["Nome do elemento para importação"].OriginalValue.ToString().ToUpper();
                    lstElementos.Add(new LstElemXml() { NomeElemento = q.ToString(), ValorElemento = "Default" });
                }

                CustomXml xml = new CustomXml(CustomXml.e_SkaLocalSalvamento.AreaDeTrabalho, "ImportExample", "Import");
                xml.AdicionaElementosXml("ITEM", lstElementos);

                if (xml._ArquivoSalvo)
                    MessageBox.Show(String.Format("Arquivo exemplar salvo em '{0}'", xml._LocalArqXml), "Êxito ao salvar arquivo exemplar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Não foi possível gerar o arquivo xml exemplar", "Erro desconhecido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        ///     Método que verifica se o usuário pode editar o campo, inativar
        /// ou ativar determinado campo.
        /// </summary>
        private void VerificaCampoImportar()
        {
            if (udgv_Importacao.Rows.Count > 0)
            {
                HabilitaControles_Importacao();

                var q = udgv_Importacao.Selected.Rows.Count == 0 ? udgv_Importacao.Rows[0] : udgv_Importacao.Selected.Rows[0];

                if (q.Cells["Ativo_Db"].OriginalValue.ToString().ToUpper() == "TRUE")
                {
                    btnEdit_Field.Enabled = true;
                    btnDelete_Field.Enabled = true;
                    btnInactivate_Field.Enabled = true;
                    btnActivate_Field.Enabled = false;
                }
                else
                {
                    btnEdit_Field.Enabled = true;
                    btnDelete_Field.Enabled = true;
                    btnInactivate_Field.Enabled = false;
                    btnActivate_Field.Enabled = true;
                }
            }
            else DesabilitaControles_Importacao();
        }

        private void btnPesquisar_Importacao_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_CamposImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar campos para importação", "FrmConfig_AbaImportacao", "btnPesquisar_Importacao_Click", "<None>", "Consultas_EcMgr.CONSULTA_CAMPOS_IMPORTAR", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnAdd_Field_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionaCampoImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar campo para importação", "FrmConfig_AbaImportacao", "btnAddField_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_CAMPO_IMPORTAR", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_Field_Click(object sender, EventArgs e)
        {
            try
            {
                EditaCampoImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao editar campo para importação", "FrmConfig_AbaImportacao", "btnEdit_Field_Click", "<None>", "Consultas_EcMgr.ATUALIZA_CAMPO_IMPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDelete_Field_Click(object sender, EventArgs e)
        {
            try
            {
                DeletaCampoImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao deletar campo para importação", "FrmConfig_AbaImportacao", "btnDelete_Field_Click", "<None>", "Consultas_EcMgr.DELETA_CAMPO_IMPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnInactivate_Field_Click(object sender, EventArgs e)
        {
            try
            {
                InativaCampoImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao inativar campo para importação", "FrmConfig_AbaImportacao", "btnInactivate_Field_Click", "<None>", "Consultas_EcMgr.INATIVA_CAMPO_IMPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnActivate_Field_Click(object sender, EventArgs e)
        {
            try
            {
                AtivaCampoImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao ativar campo para importação", "FrmConfig_AbaImportacao", "btnActivate_Field_Click", "<None>", "Consultas_EcMgr.ATIVA_CAMPO_IMPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnGerarExemploXmlImporter_Click(object sender, EventArgs e)
        {
            try
            {
                GeraXmlExemploImportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao gerar XML exemplar para importação de ordens de produção", "FrmConfig_AbaImportacao", "btnGerarExemploXmlImporter_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void cms_Importacao_Opening(object sender, CancelEventArgs e)
        {
            VerificaCampoImportar();
        }

        #endregion

        #region Aba 'Exportação de ordens de produção'

        /// <summary>
        ///     Desabilita os controles da interface de exportação de ordens de produção.
        /// </summary>
        private void DesabilitaControles_Exportacao()
        {
            btnEdit_FieldEx.Enabled = false;
            btnDelete_FieldEx.Enabled = false;
            btnInactivate_FieldEx.Enabled = false;
            btnActivate_FieldEx.Enabled = false;
            btnGerarExemploXmlExporter.Enabled = false;
        }

        /// <summary>
        ///     Habilita os controles da interface de importação de ordens de produção.
        /// </summary>
        private void HabilitaControles_Exportacao()
        {
            btnEdit_FieldEx.Enabled = true;
            btnDelete_FieldEx.Enabled = true;
            btnInactivate_FieldEx.Enabled = true;
            btnActivate_FieldEx.Enabled = true;
            btnGerarExemploXmlExporter.Enabled = true;
        }

        /// <summary>
        ///     Consulta todos os campos de exportação de ordens de produção.
        /// </summary>
        private void Consulta_CamposExportacao()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_CAMPOS_EXPORTAR);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Cria as colunas de armazenamento dos ícones da interface.
                DataColumn clImg = new DataColumn("Ativo", typeof(System.Drawing.Image));
                dt.Columns.Add(clImg);

                // Adiciona os ícones na tuplas da table (ícones da prioridade)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Ícones de visibilidade
                    if (dt.Rows[i]["Ativo_Db"].ToString().ToUpper() == "TRUE") dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Green;
                    else dt.Rows[i]["Ativo"] = Edgecam_Manager.Properties.Resources.Red;
                }

                udgv_Exportacao.DataSource = dt;
            }

            if (udgv_Exportacao.Rows.Count > 0)
                HabilitaControles_Exportacao();
            else DesabilitaControles_Exportacao();
        }

        /// <summary>
        ///     Método que abre um formulário para o usuário adicionar um novo campo para exportação.
        /// </summary>
        private void AdicionaCampoExportacao()
        {
            FrmConfig_CamposExportar frm = new FrmConfig_CamposExportar();
            frm.ShowDialog();

            btnPesquisar_Exportacao_Click(new object(), new EventArgs());
        }

        /// <summary>
        ///     Edita um determinado campo designado para exportação.
        /// </summary>
        private void EditaCampoExportacao()
        {
            var q = udgv_Exportacao.Selected.Rows.Count == 0 ? udgv_Exportacao.Rows[0] : udgv_Exportacao.Selected.Rows[0];

            if (q != null)
            {
                CampoExportar c = new CampoExportar()
                {
                    Id = q.Cells["id"].OriginalValue.ToString(),
                    NomeCampo = q.Cells["Nome do campo"].OriginalValue.ToString(),
                    NomeImportar = q.Cells["Nome do elemento para importação"].OriginalValue.ToString(),
                    Ativo = Convert.ToBoolean(q.Cells["Ativo_Db"].OriginalValue.ToString()),
                    AceitaNulos = Convert.ToBoolean(q.Cells["AceitarValoresVazios"].OriginalValue.ToString()),
                    ValorPadrao = q.Cells["ValorPadrao"].OriginalValue.ToString()
                };

                FrmConfig_CamposExportar frm = new FrmConfig_CamposExportar(c);
                frm.ShowDialog();

                btnPesquisar_Importacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Deleta o campo para exportação das ordens de produção.
        /// </summary>
        private void DeletaCampoExportacao()
        {
            if (MessageBox.Show("Você deseja mesmo deletar o campo selecionada?", "Remover campo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                var q = udgv_Exportacao.Selected.Rows.Count == 0 ? udgv_Exportacao.Rows[0] : udgv_Exportacao.Selected.Rows[0];

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.DELETA_CAMPO_EXPORTACAO, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                Objects.CadastraNovoLog(false, String.Format("Usuário removeu o campo '{0}'", q.Cells["Nome do campo"].OriginalValue.ToString()), "FrmConfig_AbaExportacao", "btnDelete_FieldEx_Click", "<None>", "Consultas_EcMgr.DELETA_CAMPO_EXPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Remoção do campo concluído", "Êxito ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnPesquisar_Exportacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Inativa um campo de exportação.
        /// </summary>
        private void InativaCampoExportacao()
        {
            if (MessageBox.Show("Você deseja mesmo inativar o campo selecionado?", "Inativar campo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                var q = udgv_Exportacao.Selected.Rows.Count == 0 ? udgv_Exportacao.Rows[0] : udgv_Exportacao.Selected.Rows[0];

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.INATIVA_CAMPO_EXPORTACAO, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                Objects.CadastraNovoLog(false, String.Format("Usuário inativou o campo '{0}'", q.Cells["Nome do campo"].OriginalValue.ToString()), "FrmConfig_AbaExportacao", "btnInactivate_FieldEx_Click", "<None>", "Consultas_EcMgr.INATIVA_CAMPO_EXPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Inativação do campo concluído", "Êxito ao inativar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnPesquisar_Exportacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Ativa um campo de exportação.
        /// </summary>
        private void AtivaCampoExportacao()
        {
            if (MessageBox.Show("Você deseja mesmo ativar o campo selecionado?", "Ativar campo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                var q = udgv_Exportacao.Selected.Rows.Count == 0 ? udgv_Exportacao.Rows[0] : udgv_Exportacao.Selected.Rows[0];

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATIVA_CAMPO_EXPORTACAO, new Dictionary<string, object>() { { "@ID", q.Cells["id"].OriginalValue.ToString() } });
                Objects.CadastraNovoLog(false, String.Format("Usuário ativou o campo '{0}'", q.Cells["Nome do campo"].OriginalValue.ToString()), "FrmConfig_AbaExportacao", "btnActivate_FieldEx_Click", "<None>", "Consultas_EcMgr.ATIVA_CAMPO_EXPORTACAO", e_TipoErroEx.Informacao);

                MessageBox.Show("Ativação do campo concluído", "Êxito ao ativar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnPesquisar_Exportacao_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Método que gera a partir do ultra grid de campos de exportação para ordens
        /// de produção, um exemplo de XML.
        /// </summary>
        private void GeraXmlExemploExportacao()
        {
            if (udgv_Exportacao.Rows.Count > 0)
            {
                List<LstElemXml> lstElementos = new List<LstElemXml>();

                for (int x = 0; x < udgv_Exportacao.Rows.Count; x++)
                {
                    //Só exporta para o XML campos determinados como ativos.
                    if (udgv_Exportacao.Rows[x].Cells["Ativo_Db"].OriginalValue.ToString().ToUpper() == "FALSE") continue;

                    var q = udgv_Exportacao.Rows[x].Cells["Nome do elemento para exportação"].OriginalValue.ToString().ToUpper();
                    lstElementos.Add(new LstElemXml() { NomeElemento = q.ToString(), ValorElemento = "Default" });
                }

                CustomXml xml = new CustomXml(CustomXml.e_SkaLocalSalvamento.AreaDeTrabalho, "ExportExample", "Export");
                xml.AdicionaElementosXml("ITEM", lstElementos);
                
                if (xml._ArquivoSalvo)
                    MessageBox.Show(String.Format("Arquivo exemplar salvo em '{0}'", xml._LocalArqXml), "Êxito ao salvar arquivo exemplar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Não foi possível gerar o arquivo xml exemplar", "Erro desconhecido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        ///     Método que verifica se o usuário pode editar o campo, inativar
        /// ou ativar determinado campo de exportação.
        /// </summary>
        private void VerificaCampoExportar()
        {
            if (udgv_Exportacao.Rows.Count > 0)
            {
                HabilitaControles_Exportacao();

                var q = udgv_Exportacao.Selected.Rows.Count == 0 ? udgv_Exportacao.Rows[0] : udgv_Exportacao.Selected.Rows[0];

                if (q.Cells["Ativo_Db"].OriginalValue.ToString().ToUpper() == "TRUE")
                {
                    btnEdit_FieldEx.Enabled = true;
                    btnDelete_FieldEx.Enabled = true;
                    btnInactivate_FieldEx.Enabled = true;
                    btnActivate_FieldEx.Enabled = false;
                }
                else
                {
                    btnEdit_FieldEx.Enabled = true;
                    btnDelete_FieldEx.Enabled = true;
                    btnInactivate_FieldEx.Enabled = false;
                    btnActivate_FieldEx.Enabled = true;
                }
            }
            else DesabilitaControles_Exportacao();
        }

        private void cms_Exportacao_Opening(object sender, CancelEventArgs e)
        {
            VerificaCampoExportar();
        }

        private void btnPesquisar_Exportacao_Click(object sender, EventArgs e)
        {
            try
            {
                Consulta_CamposExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar campos para exportação", "FrmConfig_AbaExportacao", "btnPesquisar_Exportacao_Click", "<None>", "Consultas_EcMgr.CONSULTA_CAMPOS_ExPORTAR", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnGerarExemploXmlExporter_Click(object sender, EventArgs e)
        {
            try
            {
                GeraXmlExemploExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao gerar XML exemplar para exportação de ordens de produção", "FrmConfig_AbaExportacao", "btnGerarExemploXmlExporter_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnAddFieldEx_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionaCampoExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar campo para exportação", "FrmConfig_AbaExportacao", "btnAddFieldEx_Click", "<None>", "Consultas_EcMgr.CADASTRA_NOVO_CAMPO_EXPORTAR", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnEdit_FieldEx_Click(object sender, EventArgs e)
        {
            try
            {
                EditaCampoExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao editar campo para exportação", "FrmConfig_AbaExportacao", "btnEdit_FieldEx_Click", "<None>", "Consultas_EcMgr.ATUALIZA_CAMPO_EXPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnDelete_FieldEx_Click(object sender, EventArgs e)
        {
            try
            {
                DeletaCampoExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao deletar campo para Exportação", "FrmConfig_AbaExportacao", "btnDelete_FieldEx_Click", "<None>", "Consultas_EcMgr.DELETA_CAMPO_EXPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnInactivate_FieldEx_Click(object sender, EventArgs e)
        {
            try
            {
                InativaCampoExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao ativar campo para exportação", "FrmConfig_AbaExportacao", "btnInactivate_FieldEx_Click", "<None>", "Consultas_EcMgr.ATIVA_CAMPO_EXPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnActivate_FieldEx_Click(object sender, EventArgs e)
        {
            try
            {
                AtivaCampoExportacao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao inativar campo para exportação", "FrmConfig_AbaExportacao", "btnActivate_FieldEx_Click", "<None>", "Consultas_EcMgr.INATIVA_CAMPO_EXPORTACAO", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Aba 'Encerramento de ordens de produção'

        /// <summary>
        ///     Desabilita os controles da interface de encerramento de ordens de produção.
        /// </summary>
        private void DesabilitaControles_Encerramento()
        {

        }

        /// <summary>
        ///     Habilita os controles da interface de encerramento de ordens de produção.
        /// </summary>
        private void HabilitaControles_Encerramento()
        {

        }

        #endregion

        #region Aba 'Auto programação de ordens de produção'

        private void btnPesquisar_Macros_Click(object sender, EventArgs e)
        {

        }

        private void btnNewMacro_Click(object sender, EventArgs e)
        {
            Edgecam_Manager_MacroDev.FrmMain frm = new Edgecam_Manager_MacroDev.FrmMain(Edgecam_Manager_MacroDev.FrmMain.e_SkaTheme.Escuro);
            frm.ShowDialog();
            //frm.Show();
        }

        #endregion

        #region Aba 'Centros de trabalho'



        #endregion

        /// <summary>
        ///     Evento genérico que retorna para a interface inicial.
        /// </summary>
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            RemoveControlesInterface(tbPaginaInicial);
        }

        /// <summary>
        ///     Evento genérico que, ao clicar, salva as configurações de todas as abas no banco de dados.
        /// </summary>
        /// <remarks>Não exite o botão btn_Salvar, ele é apenas virtual (só existe na minha mente e na de deus)</remarks>
        /// <remarks>Utilizado para TODOS os botões com o ícone do diskete para salvar</remarks>
        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
            GC.Collect();
        }
    }
}