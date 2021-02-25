/***********************************************************************************************************
 * 
 * 
 *                              MathicalTextBox - Caixa de texto que suporta fórmulas matemáticas
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Caixa de texto que suporta fórmulas matemáticas
 *      Version:    1.6
 *      Date:       01/04/2020, at 03:19 PM
 *      Note:       <None>
 *      History:	Update - 01/04/2020 - 03:19 AM - Criado a primeira versão - V1.0 Lançada
 * 					Update - 01/04/2020 - 02:29 PM - Adicionado lógicas para permitir apenas números e símbolos matemáticos - V1.1 Lançada
 * 					Update - 02/04/2020 - 09:41 PM - Adicionado uma troca de cor da text box caso tenha uma fórmula. - V1.2 Lançada
 * 					Update - 02/04/2020 - 11:39 PM - Resolvido o problema de não trocar a cor da TextBox corretamente. - V1.3 Lançada
 * 					Update - 07/04/2020 - 11:39 PM - Resolvido o problema do campo texto ficar vazio. - V1.4 Lançada
 * 					Update - 09/04/2020 - 08:55 PM - Resolvido o problema de não aceitar valores negativos. - V1.5 Lançada
 * 					Update - 20/04/2020 - 11:56 PM - Resolvido o problema de não salvar valores com vírgula. - V1.6 Lançada
 * 
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Edgecam_Manager
{
    public partial class MathicalTextBox : TextBox
    {

        #region  DDL Import and consts

        const int WM_NCPAINT = 0x85;
        const uint RDW_INVALIDATE = 0x1;
        const uint RDW_IUPDATENOW = 0x100;
        const uint RDW_FRAME = 0x400;

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprc, IntPtr hrgn, uint flags);

        #endregion

        #region Global variables

        /// <summary>
        ///     Default color.
        /// </summary>
        private Color mBorderColorWithFormula = Color.Cyan;

        ///// <summary>
        /////     Default color.
        ///// </summary>
        //private Color mBorderColorNoFormula = Color.Empty;

        /// <summary>
        ///     Original text entered in text box
        /// </summary>
        private String mValueWithFormula = "";

        /// <summary>
        ///     Value calculated/evalueted with formula.
        /// </summary>
        private String mValueCalculated = "";

        /// <summary>
        ///     Object to solved math expressions.
        /// </summary>
        private Eval3.Evaluator mEv = new Eval3.Evaluator(Eval3.eParserSyntax.cSharp, false);

        /// <summary>
        ///     True if already calculated.
        /// </summary>
        private Boolean mAlreadyCalculated;

        #endregion

        #region Properties

        [Description("Color of text box rectangle if have a formula. If the field doesn't have a formula, the default value will 'Color.Empty'.")]
        public Color BorderColorWithFormula
        {
            get { return mBorderColorWithFormula; }
            set
            {
                mBorderColorWithFormula = value;
                RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
            }
        }

        //[Description("Color of text box rectangle angle if they didn't have a formula")]
        //internal Color BorderColorNoFormula
        //{
        //    get { return mBorderColorNoFormula; }
        //    set
        //    {
        //        mBorderColorNoFormula = value;
        //        RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
        //    }
        //}

        /// <summary>
        ///     Original text entered in text box
        /// </summary>
        public String _ValueWithFormula { get { return mValueWithFormula; } }

        /// <summary>
        ///     Value calculated/evalueted with formula.
        /// </summary>
        public String _ValueCalculated { get { return mValueCalculated; } }

        #endregion

        #region Class Instances

        #endregion

        #region Methods

        /// <summary>
        ///     Remove characters and allow only numbers and math symbols.
        /// </summary>
        /// <param name="Str">String to check</param>
        /// <returns>String purified</returns>
        public void AllowOnlyNumbers()
        {
            // this.Text = Regex.Replace(this.Text, "[^0-9.,+*+/-^%]", "");
            this.Text = Regex.Replace(this.Text, "[^0-9.,](?!\\+)(?:\"((?:\\\"|[^\"])*)\"?)", "");
        }

        /// <summary>
        ///     Check the text inside the control and apply evaluation (if has a valid formula).
        /// </summary>
        private void EvaluatedMathFormula()
        {
            if (!String.IsNullOrEmpty(this.Text))
            {
                this.AllowOnlyNumbers();

                if (this.HasFormula())
                {
                    mBorderColorWithFormula = Color.Cyan;
                    this.mValueWithFormula = this.Text;
                    this.mValueCalculated = mEv.Parse(mValueWithFormula).value.ToString();
                    this.Text = mValueCalculated;
                    this.ChangeBorderColor();
                    this.mAlreadyCalculated = true;
                }                    
                else
                {
                    mBorderColorWithFormula = Color.Empty;
                    this.Text = mValueWithFormula = this.Text;//Means that doesn't have any formula
                    this.mAlreadyCalculated = true;
                }
            }
        }

        /// <summary>
        ///     Check if the field have a mathical expression.
        /// </summary>
        /// <returns>True if has a formula in text</returns>
        private Boolean HasFormula()
        {
            if (!String.IsNullOrEmpty(this.Text))
            {
                if (this.Text.Contains("+")) return true;
                if (this.Text.Contains("-")) return true;
                if (this.Text.Contains("*")) return true;
                if (this.Text.Contains("/")) return true;
                if (this.Text.Contains("%")) return true;
                if (this.Text.Contains("^")) return true;

                //Se chegar aqui, significa que não tem fórmula no texto (valor puro).
                return false;
            }
            else return false;
        }

        /// <summary>
        ///     Change the border color of text box based on property.
        /// </summary>
        private void ChangeBorderColor()
        {
            this.BorderColorWithFormula = mBorderColorWithFormula;
            RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
        }

        #endregion

        #region Events

        /// <summary>
        ///     Redraw the text box rectangle
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCPAINT && BorderColorWithFormula != Color.Transparent && BorderStyle == BorderStyle.Fixed3D)
            {
                var hdc = GetWindowDC(this.Handle);
                using (var g = Graphics.FromHdcInternal(hdc))
                using (var p = new Pen(BorderColorWithFormula, 3))//Aqui altera a cor do retângulo da text box e o tamanho da borda.
                    g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));//Aqui desenha o retângulo na tela.
                ReleaseDC(this.Handle, hdc);
            }
        }

        /// <summary>
        ///     When the text box change the size, redraw the rectangle wiht new colors.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, RDW_FRAME | RDW_IUPDATENOW | RDW_INVALIDATE);
        }

        /// <summary>
        ///     When get the focus, show the original value or formula.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.Text))
            {
                this.mValueWithFormula = this.Text;
                this.mAlreadyCalculated = false;
            }
        }

        /// <summary>
        ///     When lost the focus, show the value of mathical expression.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            if (!this.mAlreadyCalculated)
            {
                this.EvaluatedMathFormula();
                //this.Text = mValueCalculated;
            }
            //else this.Text = mValueCalculated;
        }

        /// <summary>
        ///     When click inside the text box, show the original value.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            this.Text = mValueWithFormula;
        }

        /// <summary>
        ///     If enter key pressed, evaluete the formula (if exists)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.EvaluatedMathFormula();
        }

        #endregion
    }
}