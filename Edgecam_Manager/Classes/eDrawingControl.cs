/***********************************************************************************************************
 * 
 * 
 *                  eDrawingControl - Controle que dá acesso ao eDrawings
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Controle que dá acesso ao eDrawings.
 *      Version:    1.0
 *      Date:       19/11/2018, at 02:31 PM
 *      Note:       
 *      History:    Update - 19/11/2018 - 02:31 PM - Primeira versão da classe - V1.0 Lançada
 * 
 * 
 * 
 * 
 **********************************************************************************************************/

 //TODO: Precisa descomentar a parte para visualzação dos arquivos 3D.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using interop.EmodelView;
using System.Runtime.InteropServices;

[System.Windows.Forms.AxHost.ClsidAttribute("{22945A69-1191-4DCF-9E6F-409BDE94D101}")]
[System.ComponentModel.DesignTimeVisibleAttribute(false)]
public partial class eDrawingControl : AxHost
{


    #region Variáveis globais

    //private EModelViewControl mOcx;
    public int mEnableFeaturesVal = 0;

    /// <summary>
    ///     True para abrir o modo full (EMVEnableFeatures.eMVFullUI)
    /// </summary>
    private Boolean mAbrirModoFull;

    /// <summary>
    ///     True para sempre mostrar mensagens de erro como marca d'água.
    /// </summary>
    private Boolean mMostrarMsgAviso;

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instância o objeto que permite acessar a visualização 3D do eDrawing.
    /// </summary>
    /// <param name="AbrirModoFull">True para mostrar o modo FullUI, false para o modo simplificado.</param>
    /// <param name="MostrarErros">True para mostrar os erros como uma marca d'água.</param>
    public eDrawingControl(Boolean AbrirModoFull = false, Boolean MostrarErros = false)
        : base("{22945A69-1191-4DCF-9E6F-409BDE94D101}")
    {
        mAbrirModoFull = AbrirModoFull;
        mMostrarMsgAviso = MostrarErros;
    }

    #endregion

    protected override void AttachInterfaces()
    {
        try
        {
            //this.mOcx = (EModelViewControl)this.GetOcx();
            ////this.ocx.EnableFeatures = EnableFeaturesVal;
            //this.mOcx.EnableFeatures = (int)EMVEnableFeatures.eMVFullUI;

            //mOcx.FullUI = mAbrirModoFull ? (int)EMVEnableFeatures.eMVFullUI : (int)EMVEnableFeatures.eMVSimplifiedUI;
            //mOcx.AlwaysShowWarningWatermark = mMostrarMsgAviso ? -1 : 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Error loading Edrawings");
            //MessageBox.Show("Installed version of eDrawings does not match referenced version.  This will be corrected at runtime", "version mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    public void ReleaseControl()
    {

        //Marshal.ReleaseComObject(this.mOcx);
        //this.mOcx = null;
        //GC.Collect();
        //GC.WaitForPendingFinalizers();
        //GC.Collect();
        //GC.WaitForPendingFinalizers();
    }

    //public EModelViewControl eDrawingsControl
    //{
    //    get
    //    {
    //        return this.mOcx;
    //    }
    //}

}