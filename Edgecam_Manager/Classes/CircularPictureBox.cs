/***********************************************************************************************************
 * 
 * 
 *                  CircularPictureBox - Controle que cria uma caixa de imagem redonda /circular
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Controle que cria uma caixa de imagem redonda /circular
 *      Version:    1.0
 *      Date:       14/03/2019, at 03:52 PM
 *      Note:       Link de ajuda: https://www.youtube.com/watch?v=U0pEEGUKNKc
 *      History:    Update - 14/03/2019 - 03:52 PM - Primeira versão da classe - V1.0 Lançada
 * 
 * 
 * 
 * 
 **********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CircularPictureBox
{
    public class CircularPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            GraphicsPath g = new GraphicsPath();
            g.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(g);
            base.OnPaint(pe);
        }
    }
}