using Edgecam_Manager_PackAndGo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    internal partial class FrmMain2 : Form
    {
        #region Variáveis Globais

        /// <summary>
        ///     Contém o valor atual da barra de progresso.
        /// </summary>
        private int mValorAtualBarraProgresso = 0;

        /// <summary>
        ///     Contém o texto da barra de progresso atual.
        /// </summary>
        private String mTextoBarraProgresso = "";

        private FrmTarefas mFrmTarefas;
        private FrmOrdens mFrmOrdens;
        private FrmOrcamentos mFrmOrcamentos;
        private FrmInventarios mFrmInventario;
        private FrmTrabalhos mTrabalhos;
        private FrmFerramentas mFrmFerramentas;
        private FrmMaquinas mFrmMaquinas;
        private FrmUserData mFrmUserData;
        private FrmConfig mFrmCfg;
        private FrmSobre mFrmSobre;
        private FrmCalendario mFrmCalendar;

        private System.Diagnostics.Stopwatch mTemporizador = new System.Diagnostics.Stopwatch();

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém o valor atual da barra de progresso.
        /// </summary>
        public int _AtualValorBarraProgresso
        {
            get
            {
                return mValorAtualBarraProgresso;
            }
        }

        /// <summary>
        ///     Contém o texto da barra de progresso atual.
        /// </summary>
        public String _TextoBarraProgresso
        {
            get
            {
                return mTextoBarraProgresso;
            }
        }

        #endregion

        #region Enumeradores privados

        /// <summary>
        ///     Enumerador que determina qual interface será instânciada.
        /// </summary>
        private enum e_Interface
        {
            FrmTarefas,
            FrmOrdens,
            FrmOrcamentos,
            FrmInventario,
            FrmTrabalhos,
            FrmUserData,
            FrmConfig,
            FrmFerramentas,
            FrmSobre,
            FrmMaquinas,
            FrmCriptomoedas,
            FrmSyneco,
            FrmCalendar
        }

        #endregion

        #region Delegates

        //  Cada delegate será chamado por um método, onde, os parâmetros que tiver no delegate, também deverão
        //existir no método, e o método será responsável por definir os valores nos controles informados.
        //  O delegate é basicamente um evento que chama um método durante uma execução, ou seja, 
        //enquanto o 'form' executa as ações, o delegate executa outras ao mesmo tempo deixando a aplicação mais rápida,
        //dessa forma, você consegue entregar a respansividade da interface ao usuário.

        /// <summary>
        ///     Delegate para invocação (execução de multiplas tarefas enquanto a interface é carregada).
        /// </summary>
        delegate void Delegate_CarregaInterface();
        /// <summary>
        ///     Delegate que seta os valores da barra de progresso da interface main.
        /// </summary>
        /// <param name="Valor">Valor (porcentagem) que a barra deverá andar (valor final é 100)</param>
        /// <param name="Msg">Mensagem para apresentar ao usuário do que está sendo carregado.</param>
        delegate void Delegate_SetaProgressoPb(int Valor, String Msg);

        #endregion

        #region Delegate Methods

        /// <summary>
        ///     Evento do delegate que criando e carregando as interfaces para o usuário.
        /// </summary>
        private void DelEvent_CarregaInterface()
        {
            Delegate_CarregaInterface d = new Delegate_CarregaInterface(ChamaEventosNormais_CarregarForms);
            Invoca(this, d);
        }

        /// <summary>
        ///     Método do delegate que seta o progresso da barra.
        /// </summary>
        /// <param name="Valor"></param>
        /// <param name="Msg"></param>
        private void SetaProgressoPb(int Valor, String Msg)
        {
            //pb_Progresso.Value = Valor;
            //pb_Texto.Text = Msg;
            Application.DoEvents();
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmMain2()
        {
            this.mTemporizador.Start();

            this.InitializeComponent();

            /*
             *  Dionei Beilke dos Santos
             *  Se habilitar alguma linha abaixo, deve obrigatoriamente re-definir o AutoHideDelay,
             * porquê ele volta para o valor default (5000), apresentando 'uma certa lentidão'.
             */

            //Infragistics.Win.AppStyling.StyleManager.Load(@"C:\Users\Public\Documents\Infragistics\2017.2\Windows Forms\AppStylist for Windows Forms\Styles\VS2013 - Dark.isl");
            //Infragistics.Win.AppStyling.StyleManager.Load(@"C:\Users\Public\Documents\Infragistics\2017.2\Windows Forms\AppStylist for Windows Forms\Styles\Metro.isl");
            //Infragistics.Win.AppStyling.StyleManager.Load(@"C:\Users\Public\Documents\Infragistics\2017.2\Windows Forms\AppStylist for Windows Forms\Styles\Office2013 - Dark Gray.isl");

            if (Objects.CfgAtual._Theme.ToUpper().Trim() != "PADRÃO")
            {
                String pathTheme = System.IO.Path.Combine(Application.StartupPath, String.Format("Themes\\{0}", Objects.CfgAtual._Theme));

                if (System.IO.File.Exists(pathTheme))
                {
                    Infragistics.Win.AppStyling.StyleManager.Load(pathTheme);
                }
            }

            this.ultraDockManager1.AutoHideDelay = 50;
            this.ultraToolbarsManager1.BeginUpdate();

            // Disable alpha-blending which may increase performance.
            this.ultraToolbarsManager1.AlphaBlendMode = Infragistics.Win.AlphaBlendMode.Disabled;

            Objects.BuscaVersaoBanco();

            //this.Text += String.Format(" - {0}", Objects.Versao.VersaoSistema);
            this.ultraToolbarsManager1.Ribbon.Caption += String.Format(" - {0}", Objects.Versao.VersaoSistema);

            //http://help.infragistics.com/Help/Doc/WinForms/2012.2/CLR4.0/html/WinToolbarsManager_Customize_Areas_of_Application_Menu_2010_and_File_Menu_Button.html
            this.ultraToolbarsManager1.Ribbon.FileMenuButtonCaption = "Arquivo";

            this.CarregaObjetosEstaticos();

            //Seta o valor máximo da barra de progresso.
            this.pb_Progresso.Maximum = 100;

            //lblNomeUsuario.Text = String.Format("{0} {1}", Objects.sUsuarioAtual.Nome, Objects.sUsuarioAtual.SobreNome);
            //Objects.DefineColorThemeInterface(this);
            this.DefineTextosInterface();

            this.udgvItensNaoSalvos.Visible = false;
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que consulta o banco de dados intermediário e cria
        /// objetos estáticos na classe 'Objects'.
        /// </summary>
        /// <remarks>Não alterar esses objetos nem mesmo a instância dos mesmos.</remarks>
        private void CarregaObjetosEstaticos()
        {
            Application.DoEvents();

            //Objects.LstUsuarios   = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_LISTA_USUARIOS).AsEnumerable().Select(x => 
            //                        (new CustomEncrypt(CustomEncrypt.CryptProvider.TripleDES, Edgecam_Manager_Random.Random.e_h01x_h20xfffS4A)).DecryptString(x["Login"].ToString())).ToList();
            Objects.LoadUsersList();
            Objects.LstMaquinas   = new ListaMachines();
            Objects.LstMateriais  = new ListaMaterais();
            Objects.LstClientes   = new ListaClientes();
            Objects.LstFamilias   = new ListaFamilia();
            Objects.LstPagamentos = new ListaMetodosPay();
            Objects.LstUnidOrg    = new ListaUnidades(true);

            //Objects.CnnBancoEcMgr.ExecutaSql("UPDATE Relatorios set ConteudoRelatorio = @IMG", new Dictionary<string, object>() { { "@IMG", System.IO.File.ReadAllText(@"C:\Users\ANAKIN\Desktop\SamplesQuotes\Report_Quote_2.mrt") } });            

            Objects.LstTiposOrcamentos = new ListQuotesType();

            Objects.LoadUsersFilter();
        }

        /// <summary>
        ///     De acordo com o idioma definido, é alterado o texto dos controles da interface.
        /// </summary>
        private void DefineTextosInterface()
        {
            //De todas os assemblys
            //https://www.infragistics.com/help/winforms/win-assembly-resource-strings

            //Somente do UltraWinToolbars
            //https://www.infragistics.com/help/winforms/wintoolbarsmanager-resource-strings

            //Customização do ribbon
            //https://www.infragistics.com/help/winforms/winribboncustomizationprovider-using-the-ribbon-customization-dialog

            //Instância o objeto
            Infragistics.Shared.ResourceCustomizer rc = Infragistics.Win.UltraWinToolbars.Resources.Customizer;
            rc.SetCustomizedString("RibbonDisplayOptions", "Opções de visualização");

            //Altera o texto da primeira seção
            rc.SetCustomizedString("RibbonDisplayOptions_AutoHide_Title", "Ocultar a faixa de opções automaticamente");
            rc.SetCustomizedString("RibbonDisplayOptions_AutoHide_Description", "Ocultar a faixa de opções.<br/>Clique na parte superior do aplicativo para mostrá-la.");

            //Altera o texto da segunda seção
            rc.SetCustomizedString("RibbonDisplayOptions_TabsOnly_Title", "Mostrar guias");
            rc.SetCustomizedString("RibbonDisplayOptions_TabsOnly_Description", "Mostrar somente as guias da faixa de opções.<br/>Clique em uma guia para mostrar os comandos.");

            //Altera o texto da terceira seção
            rc.SetCustomizedString("RibbonDisplayOptions_Full_Title", "Mostrar guias e comandos");
            rc.SetCustomizedString("RibbonDisplayOptions_Full_Description", "Mostrar todas guias da faixa de opções e comandos o tempo todo");

            //Altera o texto dos controles do direito do mouse sobre a interface/guia
            rc.SetCustomizedString("AddToQuickAccessToolbar", "Adicionar a barra de acesso rápido");
            rc.SetCustomizedString("RemoveFromQuickAccessToolbar", "Remover da barra de acesso rápido");
            rc.SetCustomizedString("QuickAccessToolbarAboveRibbon", "Mostrar barra de acesso rápido acima da guia");
            rc.SetCustomizedString("QuickAccessToolbarBelowRibbon", "Mostrar barra de acesso rápido abaixo da guia");

            //Barra de acesso rápido
            rc.SetCustomizedString("CustomizeQAT", "Customizar barra de acesso rápido");
            rc.SetCustomizedString("QuickAccessToolbarAboveRibbonCustomize", "Mostrar acima da guia");
            rc.SetCustomizedString("QuickAccessToolbarBelowRibbonCustomize", "Mostrar abaixo da guia");
            rc.SetCustomizedString("CustomizeTheRibbon", "Customizar as guias");
            rc.SetCustomizedString("MinimizeRibbon", "Minimizar guias");
        }

        /// <summary>
        ///     Faz com que um determinado controle invoque um delegate
        /// </summary>
        /// <param name="Controle">Controle que irá invocar um delegate (Formulário no caso)</param>
        /// <param name="Del">Delegate</param>
        /// <param name="Param1">Lista de objetos contendo parâmetros (pode ser valores, texto, etc)</param>
        /// <param name="Param2">Lista de objetos contendo parâmetros (pode ser valores, texto, etc)</param>
        public void Invoca(Control Controle, Delegate Del, Object Param1 = null, Object Param2 = null)
        {
            List<Object> p = new List<object>();

            mValorAtualBarraProgresso = Convert.ToInt16(Param1);
            mTextoBarraProgresso = Param2 == null ? "Inicializando" : Param2.ToString();


            if (Param1 != null)
            {
                //Se o primeiro parâmetro não estiver vazio, verifico osegundo.
                if (Param2 != null)
                {
                    p.Add(Param1);
                    p.Add(Param2);
                    Controle.Invoke(Del, p.ToArray());
                }
                //Caso o lstParam2 estiver vazio, adiciono apenas o primeiro
                else
                {
                    p.Add(Param1);
                    Controle.Invoke(Del, p);
                }
            }
            else
            {
                Controle.Invoke(Del);
            }
        }

        /// <summary>
        ///     Inicializa a instânia de todas as interfaces através dos eventos de clicks dos
        /// botões da interface
        /// </summary>
        private void ChamaEventosNormais_CarregarForms()
        {
            Delegate_SetaProgressoPb d = new Delegate_SetaProgressoPb(SetaProgressoPb);

            //Carrega a tela de inicio (FrmTarefas)
            Invoca(statusStrip, d, 5, "Carregando interface inicial");
            ChamaTela_Inicio();

            Invoca(statusStrip, d, 10, "Carregando ordens");
            ChamaTela_OrdensProducao();

            Invoca(statusStrip, d, 15, "Carregando os orçamentos");
            ChamaTela_Orcamentos();

            Invoca(statusStrip, d, 25, "Carregando informações do usuário");
            ChamaTela_Usuario();

            Invoca(statusStrip, d, 30, "Carregando os trabalhos");
            ChamaTela_Trabalhos();

            Invoca(statusStrip, d, 45, "Carregando as ferramentas");
            ChamaTela_Ferramentas();

            Invoca(statusStrip, d, 40, "Carregando as máquinas");
            ChamaTela_Maquinas();

            //submenuFabrica_Click        (new object(), new EventArgs());
            //Invoca(statusStrip, d, 45, "Carregando a fábrica");

            //submenuExportJobs_Click     (new object(), new EventArgs());
            //Invoca(statusStrip, d, 50, "Carregando folha de processo");

            //submenuNcEditor_Click       (new object(), new EventArgs());
            //Invoca(statusStrip, d, 55, "Carregando os arquivos cncs");

            //submenuReports_Click        (new object(), new EventArgs());
            //Invoca(statusStrip, d, 60, "Carregando os relatórios");

            //Menu suspenso superior
            //submenuConfiguracoes_Click  (new object(), new EventArgs());
            //Invoca(statusStrip, d, 65, "Carregando as configurações");

            //Removido, pois não há mais a necessidade de carregar as configurações ao inicializar o sistema.
            //submenuUsuario_Click        (new object(), new EventArgs());
            
            Invoca(statusStrip, d, 70, "Carregando calendário");
            ChamaTela_Calendario();

            //Traz para frente o formulário de tarefas.
            mFrmTarefas.BringToFront();

            Invoca(statusStrip, d, 75, "Carregando notificações");
            Invoca(statusStrip, d, 80, "Carregando a interface");
            Invoca(statusStrip, d, 85, "Atualizando informações");
            Invoca(statusStrip, d, 90, "Verificando dados do Edgecam");
            Invoca(statusStrip, d, 95, "Atualizando dados do Edgecam");
            Invoca(statusStrip, d, 98, "Concluindo carga das interfaces");
            Invoca(statusStrip, d, 100, "Concluído");

            //Escondo o 'statusStrip' pois já carreguei a interface.
            statusStrip.Visible = false;
        }

        /// <summary>
        ///     Implementa um formulário (instancia o objeto) e adiciona aos controles do form pai,
        /// salvando os objetos que sempre são utilizados (como por exemplo, as interfaces de consulta
        /// de dados).
        /// </summary>
        /// <param name="Frm">Formulário à ser instânciado</param>
        /// <param name="TipoInterface">Enumerador contendo qual interface deverá ser implementada</param>
        /// <param name="SalvarObjeto">Determina se deve ou não manter o cache salvo (dados no grid por exemplo)</param>
        private void ImplementaInterface(Form Frm, e_Interface TipoInterface, Boolean SalvarObjeto = false)
        {
            if (Frm == null)
            {
                Frm = InstanciaNovoForm(TipoInterface);

                Frm.MdiParent = ParentForm;

                Frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Frm.TopLevel = false;
                Frm.ControlBox = false;
                Frm.MaximizeBox = false;
                Frm.MinimizeBox = false;
                Frm.ShowIcon = false;
                Frm.Dock = DockStyle.Fill;

                Controls.Add(Frm);

                Frm.Show();
                Frm.BringToFront();
            }
            else if (!Frm.Visible)
            {
                Frm.Show();
            }
            else Frm.BringToFront();

            if (SalvarObjeto) SalvaInstanciaFormExistente(TipoInterface, Frm);
        }

        /// <summary>
        ///     De acordo com o parâmetro, devolve um tipo de objeto de form.
        /// </summary>
        /// <param name="TipoInterface">Enumerador contendo qual interface deverá ser implementada</param>
        /// <returns>Retorna um objeto de um form</returns>
        private Form InstanciaNovoForm(e_Interface TipoInterface)
        {
            switch (TipoInterface)
            {
                case e_Interface.FrmTarefas: return new FrmTarefas();
                case e_Interface.FrmOrdens: return new FrmOrdens();
                case e_Interface.FrmOrcamentos: return new FrmOrcamentos();
                case e_Interface.FrmInventario: return new FrmInventarios();
                case e_Interface.FrmTrabalhos: return new FrmTrabalhos();
                case e_Interface.FrmMaquinas: return new FrmMaquinas();
                case e_Interface.FrmUserData: return new FrmUserData();
                case e_Interface.FrmConfig: return new FrmConfig();
                case e_Interface.FrmFerramentas: return new FrmFerramentas();
                case e_Interface.FrmSobre: return new FrmSobre();
                case e_Interface.FrmCriptomoedas: return new FrmOrcamentos_Criptomoedas();
                case e_Interface.FrmSyneco: return new FrmSyneco();
                case e_Interface.FrmCalendar: return new FrmCalendario();
                default: return null;
            }
        }

        /// <summary>
        ///     Pega a instância do objeto a atribuí a uma variável global, de maneira que,
        /// ela possa ser chamada posteriormente
        /// </summary>
        /// <param name="TipoInterface">Enumerador contendo qual interface deverá ser implementada</param>
        /// <param name="Frm">Objeto instanciado</param>
        private void SalvaInstanciaFormExistente(e_Interface TipoInterface, Form Frm)
        {
            switch (TipoInterface)
            {
                case e_Interface.FrmTarefas: mFrmTarefas = (FrmTarefas)Frm; break;
                case e_Interface.FrmOrdens: mFrmOrdens = (FrmOrdens)Frm; break;
                case e_Interface.FrmOrcamentos: mFrmOrcamentos = (FrmOrcamentos)Frm; break;
                case e_Interface.FrmInventario: mFrmInventario = (FrmInventarios)Frm; break;
                case e_Interface.FrmTrabalhos: mTrabalhos = (FrmTrabalhos)Frm; break;
                case e_Interface.FrmMaquinas: mFrmMaquinas = (FrmMaquinas)Frm; break;
                case e_Interface.FrmUserData: mFrmUserData = (FrmUserData)Frm; break;
                case e_Interface.FrmConfig: mFrmCfg = (FrmConfig)Frm; break;
                case e_Interface.FrmFerramentas: mFrmFerramentas = (FrmFerramentas)Frm; break;
                case e_Interface.FrmSobre: mFrmSobre = (FrmSobre)Frm; break;
                case e_Interface.FrmCriptomoedas: Objects.Formulario_Criptomoedas = (FrmOrcamentos_Criptomoedas)Frm; break;
                case e_Interface.FrmSyneco: Objects.Formulario_Syneco = (FrmSyneco)Frm; break;
                case e_Interface.FrmCalendar: mFrmCalendar = (FrmCalendario)Frm; break;
            }
        }

        /// <summary>
        ///     Método que verifica as atualizações do sistema
        /// </summary>
        private void VerificaAtualizacoes()
        {
            Edgecam_Manager_AutoUpdate.FrmUpdate frm = new Edgecam_Manager_AutoUpdate.FrmUpdate("Edgecam Manager 2019 R2 - Beta 3", "Edgecam Manager 2019 R2 - Beta 3", Application.ProductVersion,
                                          Assembly.GetExecutingAssembly(), new Uri("http://10.10.10.147/update.xml"), "31d74b1beef722271187af872fea4c23");

            if (frm._TemNovaVersao)
            {
                if (MessageBox.Show("Você tem uma nova versão disponível. Deseja obter maiores informações?", "Nova versão disponível", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    frm.ShowDialog();
                }
            }
            else MessageBox.Show("Você já possuí a última versão do software");
        }

#endregion

#region Métodos para chamar as interfaces

        private void ChamaTela_Inicio()
        {
            ImplementaInterface(mFrmTarefas, e_Interface.FrmTarefas, true);
        }

        private void ChamaTela_NovaTarefa()
        {
            Objects.ImplementaNovoFormTela(new FrmTarefas_New(), true);
        }

        private void ChamaTela_TarefaConcluidas()
        {

        }

        private void ChamaTela_OrdensProducao()
        {
            ImplementaInterface(mFrmOrdens, e_Interface.FrmOrdens, true);
        }

        private void ChamaTela_NovaOrdem()
        {
            Objects.ImplementaNovoFormTela(new FrmOrdens_New(), true);
        }

        private void ChamaTela_Orcamentos()
        {
            ImplementaInterface(mFrmOrcamentos, e_Interface.FrmOrcamentos, true);
        }

        private void ChamaTela_NovoOrcamento()
        {
            Objects.ImplementaNovoFormTela(new FrmOrcamentos_NewDet(), true);
        }

        private void ChamaTela_Criptomoedas()
        {
            ImplementaInterface(Objects.Formulario_Criptomoedas, e_Interface.FrmCriptomoedas, true);
        }

        private void ChamaTela_Inventario()
        {
            ImplementaInterface(mFrmInventario, e_Interface.FrmInventario, true);
        }

        private void ChamaTela_Trabalhos()
        {
            ImplementaInterface(mTrabalhos, e_Interface.FrmTrabalhos, true);
        }

        private void ChamaTela_Ferramentas()
        {
            ImplementaInterface(mFrmFerramentas, e_Interface.FrmFerramentas, true);
        }

        private void ChamaTela_Maquinas()
        {
            ImplementaInterface(mFrmMaquinas, e_Interface.FrmMaquinas, true);
        }

        private void ChamaTela_Usuario()
        {
            mFrmUserData = new FrmUserData();

            //mFrmUserData.Location = new Point(this.Width - mFrmUserData.Width, 52);

            mFrmUserData.MdiParent = this.ParentForm;

            mFrmUserData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mFrmUserData.TopLevel = false;
            mFrmUserData.ControlBox = false;
            mFrmUserData.MaximizeBox = false;
            mFrmUserData.MinimizeBox = false;
            mFrmUserData.ShowIcon = false;
            mFrmUserData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));

            mFrmUserData.KeyPreview = true;

            panel1.Controls.Add(mFrmUserData);

            mFrmUserData.Show();
            mFrmUserData.BringToFront();
        }

        private void ChamaTela_AlterarSenha()
        {
            FrmLogin_NewPass frm = new FrmLogin_NewPass(Objects.UsuarioAtual.Login.ToUpper());
            frm.ShowDialog();
        }

        private void ChamaTela_Exportar()
        {
            FrmExportacao frm = new FrmExportacao(FrmExportacao.e_SkaModuloExportar.Usuario);
            frm.ShowDialog();
        }

        private void ChamaTela_Syneco()
        {
            ImplementaInterface(Objects.Formulario_Syneco, e_Interface.FrmSyneco, true);
        }

        private void ChamaTela_Configuracoes()
        {
            ImplementaInterface(mFrmCfg, e_Interface.FrmConfig, false);
        }

        private void ChamaTela_Calendario()
        {
            ImplementaInterface(mFrmCalendar, e_Interface.FrmCalendar, true);
        }

        private void ChamaTela_Sobre()
        {
            FrmSobre frm = new FrmSobre();
            frm.ShowDialog();
        }

        /// <summary>
        ///     NÃO ESTÁ SENDO UTILIZADO
        /// </summary>
        private void ChamaTela_CheckUpdates()
        {
            try
            {
                VerificaAtualizacoes();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar/obter as atualizações", "FrmMain", "verificarAtualizaçõesToolStripMenuItem_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

#endregion

#region Eventos

        /// <summary>check 
        ///     Já carrega todos os controles na interface (problemas de responsividade).
        /// </summary>
        private void FrmMain2_Shown(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(DelEvent_CarregaInterface), 0);
            t.Start();

            do
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }
            while (t.IsAlive && statusStrip.Visible);

            this.ultraToolbarsManager1.EndUpdate();

            mTemporizador.Stop();
            String tempo = mTemporizador.Elapsed.ToString();

            //MessageBox.Show(String.Format("O tempo de carga do sistema foi de: '{0}'", tempo), "Tempo gasto pelo processo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        ///     Evento dos cliques que o usuário clicou na interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Home":    // ButtonTool
                    ChamaTela_Inicio();
                    break;

                case "Alterar senha":    // ButtonTool
                    FrmLogin_NewPass frm = new FrmLogin_NewPass(Objects.UsuarioAtual.Login.ToUpper());
                    frm.ShowDialog();
                    break;

                case "Logout":    // ButtonTool
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    //ECMGR-359
                    Objects.CnnBancoEcMgr.ExecutaSql($"update Usuarios set IsLogged = 0 where id = {Objects.UsuarioAtual.Id}");

                    //'Esconde' essa interface
                    Visible = false;
                    ShowInTaskbar = false;
                    Opacity = 0;
                    Hide();

                    break;

                case "Sair":    // ButtonTool
                    //ECMGR-359
                    Objects.CnnBancoEcMgr.ExecutaSql($"update Usuarios set IsLogged = 0 where id = {Objects.UsuarioAtual.Id}");

                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                    GC.Collect();
                    break;

                case "Inicio":    // ButtonTool
                    ChamaTela_Inicio();
                    break;

                case "Nova tarefa":    // ButtonTool
                    ChamaTela_NovaTarefa();
                    break;

                case "Tarefas concluídas":    // ButtonTool
                    ChamaTela_TarefaConcluidas();
                    break;

                case "Consultar ordens":    // ButtonTool
                    ChamaTela_OrdensProducao();
                    break;

                case "Nova ordem de produção":    // ButtonTool
                    ChamaTela_NovaOrdem();
                    break;

                case "Consultar orçamentos":    // ButtonTool
                    ChamaTela_Orcamentos();
                    break;

                case "Novo orçamento":    // ButtonTool
                    ChamaTela_NovoOrcamento();
                    break;

                case "Criptomoedas":    // ButtonTool
                    ChamaTela_Criptomoedas();
                    break;

                case "Inventários":    // ButtonTool
                    ChamaTela_Inventario();
                    break;

                case "Trabalhos":    // ButtonTool
                    ChamaTela_Trabalhos();
                    break;

                case "Ferramentas":    // ButtonTool
                    ChamaTela_Ferramentas();
                    break;

                case "Máquinas":    // ButtonTool
                    ChamaTela_Maquinas();
                    break;

                case "Relatórios":    // ButtonTool
                    // Place code here
                    break;

                case "Configurações":    // ButtonTool
                    ChamaTela_Configuracoes();
                    break;

                case "Importar":    // ButtonTool
                    // Place code here
                    break;

                case "Exportar":    // ButtonTool
                    ChamaTela_Exportar();
                    break;

                case "Calculadora":    // ButtonTool
                    System.Diagnostics.Process.Start("calc.exe");
                    break;

                case "Calculadora para tempos":    // ButtonTool
                    // Place code here
                    break;

                case "Calendário":    // ButtonTool
                    ChamaTela_Calendario();
                    break;

                case "Syneco":
                    ChamaTela_Syneco();
                    break;

                case "Sobre":    // ButtonTool
                    ChamaTela_Sobre();
                    break;
            }
        }

        /// <summary>
        ///     Evento de clique sobre os painéis.
        /// </summary>
        private void ultraDockManager1_PaneActivate(object sender, Infragistics.Win.UltraWinDock.ControlPaneEventArgs e)
        {
            switch (e.Pane.Text.ToUpper().Trim())
            {
                case "ITENS NÃO SALVOS": Objects.AtualizaListaItensNaoSalvos(ref udgvItensNaoSalvos); break;
                default: break;
            }
        }

        /// <summary>
        ///     Evento da grade de dados que permite trazer para frente um determinado
        /// item que ainda não foi salvo.
        /// </summary>
        private void udgvItensNaoSalvos_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR")
                return;
            else
            {
                Objects.ChamaTelaPendenteInterface(udgvItensNaoSalvos.Rows[e.Cell.Row.Index].Cells["HashCode"].OriginalValue.ToString());
            }
        }

        private void FrmMain2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Significa que o usuário quer encerrar a interface.
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            //ECMGR-359
            Objects.CnnBancoEcMgr.ExecutaSql($"update Usuarios set IsLogged = 0 where id = {Objects.UsuarioAtual.Id}");
        }

        /// <summary>
        ///     Evento que limpa o texto da caixa de 'pesquisa rápida' caso o usuário somente clicar na caixa de texto.
        /// </summary>
        private void ultraToolbarsManager1_BeforeToolEnterEditMode(object sender, Infragistics.Win.UltraWinToolbars.BeforeToolEnterEditModeEventArgs e)
        {
            if (e.Tool.Key == "Pesquisa rápida")
            {
                String currentText = ((Infragistics.Win.UltraWinToolbars.TextBoxTool)(e.Tool)).Text;

                if (!String.IsNullOrEmpty(currentText) && currentText == "Pesquisa rápida")
                {
                    ((Infragistics.Win.UltraWinToolbars.TextBoxTool)(e.Tool)).Text = "";
                }
            }
            else return;
        }

        /// <summary>
        ///     Evento que define o texto da caixa de 'pesquisa rápida' caso o usuário somente digitar na caixa de texto.
        /// </summary>
        private void ultraToolbarsManager1_AfterToolExitEditMode(object sender, Infragistics.Win.UltraWinToolbars.AfterToolExitEditModeEventArgs e)
        {
            if (e.Tool.Key == "Pesquisa rápida")
            {
                String currentText = ((Infragistics.Win.UltraWinToolbars.TextBoxTool)(e.Tool)).Text;

                if (String.IsNullOrEmpty(currentText))
                {
                    ((Infragistics.Win.UltraWinToolbars.TextBoxTool)(e.Tool)).Text = "Pesquisa rápida";
                }
            }
            else return;
        }

#endregion

    }
}