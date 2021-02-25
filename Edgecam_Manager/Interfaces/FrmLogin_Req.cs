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
    public partial class FrmLogin_Req : Form
    {

        #region Variáveis globais (públicas e privadas) e/ou estáticas

        //Dados para conexão com o banco de dados do Edgecam
        private String mEcServer;
        private String mEcDb;
        private String mEcUser;
        private String mEcPass;
        //Dados para conexão com o banco de dados intermediário.
        private String mAuxServer;
        private String mAuxDb;
        private String mAuxUser;
        private String mAuxPass;
        //Dados para conexão com o banco de dados de log.
        private String mLogServer;
        private String mLogDb;
        private String mLogUser;
        private String mLogPass;
        //Contém o idioma definido pelo usuário.
        private System.Globalization.CultureInfo mIdioma;

        /// <summary>
        ///     True para caso o usuário tenha definido que queira
        /// se manter conectado.
        /// </summary>
        private Boolean mManterUserConectado;

        //Instanciação dos objetos das classes.
        private CustomXmlConfig mXml;

        /// <summary>
        ///     Contém o objeto que dá acesso ao formulário main.
        /// </summary>
        //public FrmMain mFrmMain;

        #endregion

        #region Instâncias da classe

        public FrmLogin_Req()
        {
            this.InitializeComponent();
            this.LoadConfig();
            this.GetUnities();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Load system configuration.
        /// </summary>
        private void LoadConfig()
        {
            mXml = new CustomXmlConfig("Config.xml");

            mEcServer   = mXml.StrXmlSimpleConfigValue("SERVER_EC");
            mEcDb       = mXml.StrXmlSimpleConfigValue("BANCO_EC");
            mEcUser     = mXml.StrXmlSimpleConfigValue("USER_EC");
            mEcPass     = mXml.StrXmlSimpleConfigValue("PASS_EC");

            mAuxServer  = mXml.StrXmlSimpleConfigValue("SERVER_AUX");
            mAuxDb      = mXml.StrXmlSimpleConfigValue("BANCO_AUX");
            mAuxUser    = mXml.StrXmlSimpleConfigValue("USER_AUX");
            mAuxPass    = mXml.StrXmlSimpleConfigValue("PASS_AUX");

            mLogServer  = mXml.StrXmlSimpleConfigValue("SERVER_LOG");
            mLogDb      = mXml.StrXmlSimpleConfigValue("BANCO_LOG");
            mLogUser    = mXml.StrXmlSimpleConfigValue("USER_LOG");
            mLogPass    = mXml.StrXmlSimpleConfigValue("PASS_LOG");

            String EcStrCnn  = Sql.CriaStringConexao(mEcServer, mEcDb, mEcUser, mEcPass);
            String AuxStrCnn = Sql.CriaStringConexao(mAuxServer, mAuxDb, mAuxUser, mAuxPass);
            String LogStrCnn = Sql.CriaStringConexao(mLogServer, mLogDb, mLogUser, mLogPass);

            //Cria um objeto estático, de maneira que possa 'reutilizar' os objetos em toda a interface.
            Objects.CfgAtual = new Config(mEcServer, mEcDb, mEcUser, mEcPass, EcStrCnn, mAuxServer, mAuxDb, mAuxUser, mAuxPass, AuxStrCnn, mIdioma, "default");

            //Cria os objetos estáticos para cada banco de dados.
            Objects.CnnBancoEc      = new Sql(EcStrCnn);
            Objects.CnnBancoEcMgr   = new Sql(AuxStrCnn);
            Objects.CnnBancoLog     = new Sql(LogStrCnn);
        }

        private void GetUnities()
        {
            DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_UNIDADES_ORGANIZACIONAIS);
            cbUnidade.Items.Add("<Selecione>");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow r in dt.Rows) cbUnidade.Items.Add(r[0].ToString());
            }

            cbUnidade.SelectedIndex = 0;
        }

        private Boolean IsFieldsFilled()
        {
            if (String.IsNullOrEmpty(txtNome.Text)) return false;
            else if (String.IsNullOrEmpty(txtSobrenome.Text)) return false;
            else if (String.IsNullOrEmpty(txtArea.Text)) return false;
            else if (String.IsNullOrEmpty(txtGestor.Text)) return false;
            else if (String.IsNullOrEmpty(txtEmail.Text)) return false;
            else if (cbUnidade.SelectedIndex <= 0 && String.IsNullOrEmpty(cbUnidade.Text) && cbUnidade.Text.ToUpper().Trim() == "<SELECIONE>") return false;
            else return true;
        }

        #endregion

        #region Eventos

        private void btnReturn_Click(object sender, EventArgs e)
        {
            bool isBool = false;

            try
            {
                Boolean.TryParse(sender.ToString(), out isBool);
            }
            catch { isBool = false; }

            if (isBool)
            {
                this.Close();
                GC.Collect();
            }
            else if (MessageBox.Show("Deseja mesmo sair sem salvar?", "Dados não salvos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.Close();
                GC.Collect();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.IsFieldsFilled())
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@NOME", txtNome.Text);
                dic.Add("@SOBREN", txtSobrenome.Text);
                dic.Add("@AREA", txtArea.Text);
                dic.Add("@GESTOR", txtGestor.Text);
                dic.Add("@EMAIL", txtEmail.Text);
                dic.Add("@RAMAL", txtRamal.Text);
                dic.Add("@UNID", cbUnidade.SelectedIndex > 0 ? cbUnidade.SelectedItem.ToString() : cbUnidade.Text);

                //Register new request and query them
                DataTable dt = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_SOLI_NEW_USER, dic);

                if (dt == null || dt.Rows.Count == 0) throw new Exception("Não foi possível inserir a solicitação de criação de novos usuários.");

                //Send a notification to admin
                dic = new Dictionary<string, object>();
                dic.Add("@ASSUNTO", "Inspecionar nova solicitação de login");
                dic.Add("@INSTRU", $"Validar solicitação de novo login do sistema para o registro de id '{dt.Rows[0]["id"].ToString()}'");
                dic.Add("@TIPO", 5);
                dic.Add("@PRIORIDADE", 3);
                dic.Add("@DTINICIO", DateTime.Today);
                dic.Add("@DTFIM", DateTime.Today.AddDays(10));
                dic.Add("@DTCRIACAO", DateTime.Now);
                dic.Add("@USR", "SYSADM");
                dic.Add("@USRSOLICITANTE", "SYSADM");

                //Create a new task to admin
                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVA_TAREFA, dic);

                MessageBox.Show("Seu login foi solicitado ao administrador do sistema e você será notificado quando houver novidades.", 
                                "Login solicitado com êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.btnReturn_Click(true, new EventArgs());
            }
            else
            {
                MessageBox.Show("Alguns campos não foram preenchidos. Todos os campos são obrigatórios.", "Campos vazios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        #endregion

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}