using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Edgecam_Manager.Idiomas;

namespace Edgecam_Manager
{
    internal partial class FrmLogin_UserCnns : Form
    {

        CustomXmlConfig mXml;

        public static Boolean sSalvouArq = false;

        #region Métodos

        public FrmLogin_UserCnns()
        {
            InitializeComponent();

            //DefineLanguage();

            //Objects.DefineColorThemeInterface(this);
        }

        ///// <summary>
        /////     Método que troca o idioma do sistema para o idioma definido pelo usuário.
        ///// </summary>
        //private void DefineLanguage()
        //{
        //    //Por padrão já está tudo em português
        //    if (Objects.CfgAtual._Idioma.Name.ToUpper() == "PT-BR") { }
        //    if (Objects.CfgAtual._Idioma.Name.ToUpper() == "EN-US")
        //    {
        //        Text = en_US.FrmUserConfigCnns_Title;

        //        groupBox1.Text = en_US.FrmUserConfigCnns_Gp_Ec;
        //        label9.Text = en_US.FrmUserConfigCnns_EcSrv;
        //        label11.Text = en_US.FrmUserConfigCnns_EcDb;
        //        label13.Text = en_US.FrmUserConfigCnns_EcUs;
        //        label12.Text = en_US.FrmUserConfigCnns_EcPs;
        //        btn_Test_Ec.Text = en_US.FrmUserConfigCnns_TestCnn;

        //        groupBox2.Text = en_US.FrmUserConfigCnns_Gp_Ax;
        //        label16.Text = en_US.FrmUserConfigCnns_MgSrv;
        //        label15.Text = en_US.FrmUserConfigCnns_MgDb;
        //        label10.Text = en_US.FrmUserConfigCnns_EcUs;
        //        label14.Text = en_US.FrmUserConfigCnns_EcPs;
        //        btn_Test_EcMgr.Text = en_US.FrmUserConfigCnns_TestCnn;

        //        btnReturn.Text = en_US.FrmUserConfigCnns_Return;
        //        btnSave.Text = en_US.FrmUserConfigCnns_Save;
        //    }
        //}

        /// <summary>
        ///     Método que valida se todos os campos 'obrigatórios' estão devidamente preenchidos.
        /// </summary>
        /// <remarks>Não faz nenhum teste de conexão para verificar a autenticidade das informações.</remarks>
        private void ValidaSalvaConfiguracao()
        {
            int fieldError = 0;

            //Conexão com o banco de dados do Edgecam
            try
            {
                if (txt_Cn_SqlEc.Text == "") fieldError++;
            }
            catch { fieldError++; }

            try
            {
                if (txt_Cn_DbEc.Text == "") fieldError++;
            }
            catch { fieldError++; }

            try
            {
                if (txt_Cn_EcUser.Text == "") fieldError++;
            }
            catch { fieldError++; }

            //Conexão com o banco de dados auxiliar
            try
            {
                if (txt_Cn_SqlEcMgr.Text == "") fieldError++;
            }
            catch { fieldError++; }

            try
            {
                if (txt_Cn_DbEcMgr.Text == "") fieldError++;
            }
            catch { fieldError++; }

            try
            {
                if (txt_Cn_EcMgrUser.Text == "") fieldError++;
            }
            catch { fieldError++; }

            //Conexão com o banco de dados auxiliar de logs (exclusivamente)
            try
            {
                if (txt_Cn_SqlEcMgrLog.Text == "") fieldError++;
            }
            catch { fieldError++; }

            try
            {
                if (txt_Cn_DbEcMgrLog.Text == "") fieldError++;
            }
            catch { fieldError++; }

            try
            {
                if (txt_Cn_EcMgrLogUser.Text == "") fieldError++;
            }
            catch { fieldError++; }

            if (fieldError == 0)
            {
                if (CriaArquivoXml())
                {
                    sSalvouArq = true;
                    Messages.Msg003();

                    Close();
                    GC.Collect();
                }

            }
            else
            {
                Messages.Msg004();            
            }
        }

        private Boolean CriaArquivoXml()
        {
            try
            {
                mXml = new CustomXmlConfig("Config.xml");
                mXml.CreateNewXmlFileConfig("ConfiguracoesGerais");

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("SERVER_EC", txt_Cn_SqlEc.Text);
                dic.Add("BANCO_EC", txt_Cn_DbEc.Text);
                dic.Add("USER_EC", txt_Cn_EcUser.Text);
                dic.Add("PASS_EC", txt_Cn_EcPass.Text != "" ? txt_Cn_EcPass.Text : "");

                dic.Add("SERVER_AUX", txt_Cn_SqlEcMgr.Text);
                dic.Add("BANCO_AUX", txt_Cn_DbEcMgr.Text);
                dic.Add("USER_AUX", txt_Cn_EcMgrUser.Text);
                dic.Add("PASS_AUX", txt_Cn_EcMgrPass.Text != "" ? txt_Cn_EcMgrPass.Text : "");

                dic.Add("SERVER_LOG", txt_Cn_SqlEcMgrLog.Text);
                dic.Add("BANCO_LOG", txt_Cn_DbEcMgrLog.Text);
                dic.Add("USER_LOG", txt_Cn_EcMgrLogUser.Text);
                dic.Add("PASS_LOG", txt_Cn_EcMgrLogPass.Text != "" ? txt_Cn_EcMgrLogPass.Text : "");

                /*
                 *  Dionei Beilke dos Santos
                 *  09/11/2018
                 *  Adicionado novas configuracoes
                 */
                dic.Add("IDIOMA", "pt-BR");
                dic.Add("THEME", "LIGHT");

                mXml.AddXmlParameters("ConfiguracoesGerais", dic);

                //User XML
                mXml = new CustomXmlConfig("ConfigCurrentUser.xml");
                mXml.CreateNewXmlFileConfig("ConfiguracoesGerais");

                dic = new Dictionary<string, object>();
                dic.Add("MANTER_CONECTADO", "false");
                dic.Add("USUARIO", "L04eWNDJERU=");
                dic.Add("SENHA", "");

                mXml.AddXmlParameters("ConfiguracoesGerais", dic);

                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Método que limpa a seleção de todos os controles da interface (background color = yellow e o errorProvider)
        /// e seta a seleção em um único controle na interface, destacando para o usuário.
        /// </summary>
        /// <param name="Crtl">Controle que deverá ser setado como conrtole ativo.</param>
        private void SetaSelecaoCaixa(Control Crtl)
        {
            foreach (Control c in Controls)
            {
                foreach (Control cc in c.Controls)
                {
                    if (cc is TextBox && cc.Name.ToUpper() != Crtl.Name.ToUpper())
                        cc.BackColor = Color.White;
                }

                alert.Clear();
                Crtl.BackColor = Color.Yellow;

                if (Objects.CfgAtual._Idioma.Name.ToUpper() == "PT-BR")
                {
                    alert.SetError(Crtl, "Insira um dado válido");
                }
                if (Objects.CfgAtual._Idioma.Name.ToUpper() == "EN-US")
                {
                    alert.SetError(Crtl, en_US.FrmUserConfigCnns_DadoValido);
                }
            }
        }

        #endregion        

        #region Eventos

        /// <summary>
        ///     Evento de carga do usuário que define o foco para a primeira text box.
        /// </summary>
        private void FrmUserConfigCnns_Load(object sender, EventArgs e)
        {
            txt_Cn_SqlEc_Enter(new object(), new EventArgs());
        }

        /// <summary>
        ///     Testa a conectividade com o banco de dados do Edgecam.
        /// </summary>
        private void btn_Test_Ec_Click(object sender, EventArgs e)
        {
            try
            {
                Sql s = new Sql(txt_Cn_SqlEc.Text, txt_Cn_DbEc.Text, txt_Cn_EcUser.Text, txt_Cn_EcPass.Text);
                if (s.TestarConexaoSql())
                    Messages.Msg005();
                else Messages.Msg006();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(false, "Erro ao testar conexão", "FrmLogin_UserCnns", "TestarConexaoSql", "Exceção não tratada em teste de conectividade com o servidor do EC",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Testa a conectividade com o banco de dados do Edgecam Manager
        /// </summary>
        private void btn_Test_EcMgr_Click(object sender, EventArgs e)
        {
            try
            {
                Sql s = new Sql(txt_Cn_SqlEcMgr.Text, txt_Cn_DbEcMgr.Text, txt_Cn_EcMgrUser.Text, txt_Cn_EcMgrPass.Text);
                if (s.TestarConexaoSql())
                    Messages.Msg005();
                else Messages.Msg006();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(false, "Erro ao testar conexão", "FrmLogin_UserCnns", "TestarConexaoSql", "Exceção não tratada em teste de conectividade com o servidor do EC MGR",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }            
        }

        /// <summary>
        ///     Testa a conectividade com o banco de dados de logs do Edgecam Manager
        /// </summary>
        private void btn_Test_EcMgrLog_Click(object sender, EventArgs e)
        {
            try
            {
                Sql s = new Sql(txt_Cn_SqlEcMgrLog.Text, txt_Cn_DbEcMgrLog.Text, txt_Cn_EcMgrLogUser.Text, txt_Cn_EcMgrLogPass.Text);
                if (s.TestarConexaoSql())
                    Messages.Msg005();
                else Messages.Msg006();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(false, "Erro ao testar conexão", "FrmLogin_UserCnns", "TestarConexaoSql", "Exceção não tratada em teste de conectividade com o servidor do EC MGR LOG",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }   
        }

        /// <summary>
        ///     Saí da interface atual.
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        /// <summary>
        ///     Salva as configurações impostas pelo usuário.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidaSalvaConfiguracao();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao criar arquivo de configuração", "FrmLogin_UserCnns", "ValidaDadosAntesSalvar", "Exceção não tratada ao salvar nova config",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        #region Eventos para definir o foco

        private void txt_Cn_SqlEc_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_SqlEc);
        }

        private void txt_Cn_DbEc_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_DbEc);
        }

        private void txt_Cn_EcUser_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_EcUser);
        }

        private void txt_Cn_EcPass_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_EcPass);
        }

        private void txt_Cn_SqlEcMgr_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_SqlEcMgr);
        }

        private void txt_Cn_DbEcMgr_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_DbEcMgr);
        }

        private void txt_Cn_EcMgrUser_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_EcMgrUser);
        }

        private void txt_Cn_EcMgrPass_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_EcMgrPass);
        }

        private void txt_Cn_SqlEcMgrLog_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_SqlEcMgrLog);
        }

        private void txt_Cn_DbEcMgrLog_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_DbEcMgrLog);
        }

        private void txt_Cn_EcMgrLogUser_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_EcMgrLogUser);
        }

        private void txt_Cn_EcMgrLogPass_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txt_Cn_EcMgrLogPass);
        }

        /// <summary>
        ///     Evento para limpar tudo
        /// </summary>
        private void txt_Cn_EcMgrLogPass_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txt_Cn_EcMgrPass.BackColor = Color.White;
        }

        #endregion
    }
}
