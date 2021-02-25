using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;

namespace Edgecam_Manager_AutoUpdate
{
    public partial class FrmUpdate : Form, IAutoUpdatable
    {

        #region Variaveis Globais

        private String mAppName = "";
        private String mAppId = "";
        private String mAppVersion = "";
        private Assembly mAssembly = null;
        private Uri mUri = null;
        private String mHash = "";

        private Boolean mTemNovaVersao;

        private AutoUpdateXml mAutoUpdate;

        #endregion

        #region Propriedades

        public Boolean _TemNovaVersao
        {
            get
            {
                return mTemNovaVersao;
            }
        }

        #endregion

        #region Implementação do IAutoUpdatable

        /// <summary>
        ///     Contém o nome da aplicação.
        /// </summary>
        String IAutoUpdatable.ApplicationName
        {
            get
            {
                return mAppName;
            }
        }

        /// <summary>
        ///     Contém o ID da aplicação (nesse caso, aconselha-se a utilizar o mesmo nome da aplicação).
        /// </summary>
        String IAutoUpdatable.ApplicationID
        {
            get
            {
                return mAppId;
            }
        }
        /// <summary>
        ///     Contém a versão da aplicação.
        /// </summary>
        String IAutoUpdatable.ApplicationVersion
        {
            get
            {
                return mAppVersion;
            }
        }
        /// <summary>
        ///     Contém o objeto do assembly (assembly em execução).
        /// </summary>
        Assembly IAutoUpdatable.ApplicationAssembly
        {
            get
            {
                return mAssembly;
            }
        }
        /// <summary>
        ///     Contém o ícone da aplicação.
        /// </summary>
        Icon IAutoUpdatable.ApplicationIcon
        {
            get
            {
                return this.Icon;
            }
        }
        /// <summary>
        ///     Localização do arquivo 'update.xml' no servidor.
        /// </summary>
        Uri IAutoUpdatable.UpdateXmlLocation
        {
            get
            {
                return mUri;
            }
        }
        /// <summary>
        ///     O contexto do programa.
        ///     Para formulários do windows, utilizar o 'this'.
        ///     Console apps, referência a library 'System.Windows.Forms' e retorne nulo.
        /// </summary>
        Form IAutoUpdatable.Context
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region Instância de objetos da classe

        public FrmUpdate(String AppName, String AppId, String AppVersion, Assembly Assembly, Uri UpdateXmlLocation, String Hash)
        {
            InitializeComponent();

            mAppName = AppName;
            mAppId = AppId;
            mAppVersion = AppVersion;
            mAssembly = Assembly;
            mUri = UpdateXmlLocation;
            mHash = Hash;

            mTemNovaVersao = VerificaVersao();
        }

        #endregion

        #region Métodos

        private Boolean VerificaVersao()
        {
            if (AutoUpdateXml.ExistsOnServer(mUri))
            {
                //Carrega o XML do servidor
                mAutoUpdate = AutoUpdateXml.Parse(mUri, mAppId);

                //Se retornar true, significa que o cliente não tem a última versão. 
                if (mAutoUpdate.IsNewerThan(Version.Parse(mAppVersion)))
                    return true;
                else return false;
            }
            return false;
        }

        private void CarregaTarefasDesenvolvimento()
        {
            try
            {
                String[] devs = mAutoUpdate._Description.Split('\n');

                foreach (String dev in devs)
                {
                    if (dev != "" && dev != "\r")
                    {
                        String strTmp = dev.Replace("\n", "").Replace("\t", "").Replace("\r", "");

                        dgv.Rows.Add(strTmp.Split(new char[] { '|' })[0], strTmp.Split(new char[] { '|' })[1]);
                    }
                    else continue;
                }
            }
            catch
            {
                //TODO: Caso não conseguir interpretar as atualizações, como vou apresentar
                //o erro para o usuário?????? (ESTUDAR ISSO URGENTE)
            }
        }

        #endregion

        #region Eventos

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AutoUpdateSharp s = new AutoUpdateSharp(this);
            s.DoUpdate();
            //FrmDownloading frm = new FrmDownloading(mAutoUpdate._Uri, mAutoUpdate._Md5);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion

        private void FrmUpdate_Shown(object sender, EventArgs e)
        {
            txtVersaoAtual.Text = mAppVersion;
            txtVersaoDisponivel.Text = mAutoUpdate._Version.ToString();

            CarregaTarefasDesenvolvimento();
        }
    }
}