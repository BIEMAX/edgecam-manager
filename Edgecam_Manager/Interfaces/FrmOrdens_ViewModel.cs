using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using interop.EmodelView;
//using eDrawings.Interop.EModelViewControl;
//using Interop.EmodelView;
//using EModelView.x64;

namespace Edgecam_Manager
{
    public partial class FrmOrdens_ViewModel : Form
    {
        #region Variáveis globais

        /// <summary>
        ///     Contém o caminho do modelo sólido para visualização 3D.
        /// </summary>
        private String mCaminhoSolido;

        /// <summary>
        ///     Contém apenas o nome do modelo sólido.
        /// </summary>
        private String mNomeSolido;

        /// <summary>
        ///     Contém o controle do EDrawings
        /// </summary>
        //private EModelViewControl mEmvControl = null;

        /// <summary>
        ///     Contém a base hexadecimal do eDrawings
        /// </summary>
        private eDrawingControl mHostContainer = null;

        #endregion

        #region Métodos

        public FrmOrdens_ViewModel(String CaminhoSolido)
        {
            Cursor = Cursors.WaitCursor;

            InitializeComponent();

            mCaminhoSolido = CaminhoSolido;

            ValidaSolido();
        }

        /// <summary>
        ///     Válida se o modelo sólido é válido.
        /// </summary>
        private void ValidaSolido()
        {
            try
            {
                if (!String.IsNullOrEmpty(mCaminhoSolido))
                {
                    mNomeSolido = mCaminhoSolido.TrimStart().TrimEnd().Substring(mCaminhoSolido.LastIndexOf("\\") + 1);

                    Text += String.Format(" - Peça '{0}'", mNomeSolido);
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao carregar visualização 3D", "FrmOrdens_ViewModel", "ValidaSolido", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

        private void FrmOrdens_ViewModel_Load(object sender, EventArgs e)
        {
            //Instância o objeto
            //mHostContainer = new eDrawingControl(true, true);
            mHostContainer = new eDrawingControl(false, false);
            panel1.Controls.Add(mHostContainer);

            //Configurações do controle
            //mEmvControl = (EModelViewControl)mHostContainer.GetOcx();

            //Link de ajuda:
            //http://help.solidworks.com/2016/english/api/emodelapi/edrawings.interop.emodelviewcontrol~edrawings.interop.emodelviewcontrol.emvenablefeatures.html
            //mEmvControl.EnableFeatures = (int)EMVEnableFeatures.eMVFullUI;// +
                                         //(int)EMVEnableFeatures.eMVDisableMeasure +
                                         //(int)EMVEnableFeatures.eMVDisableMenuSave +
                                         //(int)EMVEnableFeatures.eMVSeparateMarkup +
                                         //(int)EMVEnableFeatures.eMVEnableSilentMode +
                                         //(int)EMVEnableFeatures.eMVSimplifiedUI +
                                         //(int)EMVEnableFeatures.eMVDisableSNLCheckout +
                                         //(int)EMVEnableFeatures.eMVReadOnly +
                                         //(int)EMVEnableFeatures.eMVSmallToolbarButtons + 
                                         //(int)EMVEnableFeatures.eMVSuppressRMBMenu + 
                                         //(int)EMVEnableFeatures.eMVSuppressSavePrompt +
                                         //(int)EMVEnableFeatures.eMVSuppressStatusBar + 
                                         //(int)EMVEnableFeatures.eMVSuppressTabs +
                                         //(int)EMVEnableFeatures.eMVSupressMenuBar +
                                         //(int)EMVEnableFeatures.eMVZoomInFunctionality;
                                         //0;
            //mEmvControl.ShowAllTooltips();

            //mEmvControl.FullUI = true ? (int)EMVEnableFeatures.eMVFullUI : (int)EMVEnableFeatures.eMVSimplifiedUI;
            //mEmvControl.AlwaysShowWarningWatermark = false ? -1 : 0;

            //Define a posição do controle no forme 
            //mHostContainer.Left = 52; 
            //mHostContainer.Top = 48;

            //Define do controle no form 
            mHostContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            mHostContainer.Size = new Size(panel1.Size.Width, panel1.Size.Height); 

            //Abre o arquivo de controle de peça no eDrawings 
            //mEmvControl.OpenDoc(mCaminhoSolido, false, false, false, "");

            Cursor = Cursors.Arrow;
        }
    }
}
