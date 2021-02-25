/***********************************************************************************************************
 * 
 * 
 *                  NotificationBadge - Controle que cria uma label dentro de um controle
 *                          com uma quantidade de itens pendentes
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Controle que cria uma label dentro de um controle com uma quantidade de itens pendentes
 *      Version:    1.0
 *      Date:       15/03/2019, at 05:44 AM
 *      Note:       Link de ajuda: https://stackoverflow.com/questions/29756038/add-a-badge-to-a-c-sharp-winforms-control
 *      History:    Update - 15/03/2019 - 05:44 AM - Primeira versão da classe - V1.0 Lançada
 * 
 * 
 * 
 * 
 **********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

static class NotificationBadge
{
    private static List<Control> controls = new List<Control>();

    /// <summary>
    ///     Adiciona uma caixa de texto do tipo label em um controle, com um texto.
    /// </summary>
    /// <param name="Control">Controle (botão, panel, group box, etc)</param>
    /// <param name="Text">Texto à ser adicionado</param>
    /// <returns>Retorna true caso tenha conseguido adicionar a caixa de notificação</returns>
    static public bool AddBadgeTo(Control Control, string Text)
    {
        if (controls.Contains(Control)) return false;

        SkaBadge badge = new SkaBadge();
        badge.AutoSize = true;
        badge.Text = Text;
        badge.BackColor = Color.Transparent;
        controls.Add(Control);
        Control.Controls.Add(badge);
        SetPosition(badge, Control);

        return true;
    }

    static public bool RemoveBadgeFrom(Control ctl)
    {
        SkaBadge badge = GetBadge(ctl);
        if (badge != null)
        {
            ctl.Controls.Remove(badge);
            controls.Remove(ctl);
            return true;
        }
        else return false;
    }

    static public void SetBadgeText(Control ctl, string newText)
    {
        SkaBadge badge = GetBadge(ctl);
        if (badge != null)
        {
            badge.Text = newText;
            SetPosition(badge, ctl);
        }
    }

    static public string GetBadgeText(Control ctl)
    {
        SkaBadge badge = GetBadge(ctl);
        if (badge != null) return badge.Text;
        return "";
    }

    static private void SetPosition(SkaBadge badge, Control ctl)
    {
        //badge.Location = new Point(ctl.Width - badge.Width - 5, ctl.Height - badge.Height - 5);
        //Deixa no centro do controle (em caso de botões)
        badge.Location = new Point(ctl.Width - badge.Width - 5, (ctl.Height / 2) - (badge.Height / 2));
    }

    static public void SetClickAction(Control ctl, Action<Control> action)
    {
        SkaBadge badge = GetBadge(ctl);
        if (badge != null) badge.ClickEvent = action;
    }

    static SkaBadge GetBadge(Control ctl)
    {
        for (int c = 0; c < ctl.Controls.Count; c++)
            if (ctl.Controls[c] is SkaBadge) return ctl.Controls[c] as SkaBadge;
        return null;
    }


    class SkaBadge : Label
    {
        // Member hides inherited member; missing new keyword
        #pragma warning disable CS0108
        Color BackColor = Color.SkyBlue;
        Color ForeColor = Color.White;

        Font font = new Font("Sans Serif", 8f);

        public Action<Control> ClickEvent;

        public SkaBadge() { }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(BackColor), this.ClientRectangle);
            e.Graphics.DrawString(Text, font, new SolidBrush(ForeColor), 3, 1);
        }

        //protected override void OnClick(EventArgs e)
        //{
        //    ClickEvent(this);
        //}

    }
}