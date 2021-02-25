/***********************************************************************************************************
 * 
 * 
 *                  SkaSyntaxHighlighterRichTextBox - Classe que implementa destaque de texto no richtextbox
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Cria destaque de texto no richtextbox
 *      Version:    1.1
 *      Date:       30/01/2019, at 06:16 PM
 *      Note:       <None>
 *      Updates:    Update - 14/09/2018 - 06:16 PM - Lançado a primeira versão. - V1.0 Lançada
 *                  Update - 30/01/2019 - 01:54 PM - Adicionado a propriedade de configuração 'NormalTextColor'
 *                      na classe 'SyntaxSettings' - V1.1 Lançada
 * 
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;


public class SkaSyntaxHighlighterRichTextBox : System.Windows.Forms.RichTextBox
{
    private SyntaxSettings m_settings = new SyntaxSettings();
    private static bool m_bPaint = true;
    private string m_strLine = "";
    private int m_nContentLength = 0;
    private int m_nLineLength = 0;
    private int m_nLineStart = 0;
    private int m_nLineEnd = 0;
    private string m_strKeywords = "";
    private int m_nCurSelection = 0;

    private ListBox mVariablesList;
    private Point mReferencePoint;

    /// <summary>
    /// The settings.
    /// </summary>
    public SyntaxSettings Settings
    {
        get { return m_settings; }
    }

    public SkaSyntaxHighlighterRichTextBox()
    {
        InicializaVariavies();
    }

    /// <summary>
    /// WndProc
    /// </summary>
    /// <param name="m"></param>
    protected override void WndProc(ref System.Windows.Forms.Message m)
    {
        if (m.Msg == 0x00f)
        {
            if (m_bPaint)
                base.WndProc(ref m);
            else
                m.Result = IntPtr.Zero;
        }
        else
            base.WndProc(ref m);
    }

    private void InicializaVariavies()
    {
        mVariablesList = new ListBox();
        mVariablesList.Size = new Size(250, 100);
        mVariablesList.ForeColor = Color.Green;

        //Eventos
        mVariablesList.KeyDown += SyntaxKeyDown;

        //Here u can    //populate from some DataSource..
        mVariablesList.Items.Add("@COMPRIMENTO@");
        mVariablesList.Items.Add("@LARGURA@");
        mVariablesList.Items.Add("@DIMENSÃO@");
        mVariablesList.Items.Add("@TESTE@");
        mVariablesList.Items.Add("@AAAAA@");
        mVariablesList.Items.Add("@1424324@");
        mVariablesList.Items.Add("@HAHAHAHA@");
        mVariablesList.Items.Add("@EHUEHUEHEHU@");
    }

    private void AdicionaVariavel()
    {
        //Esconde a list box
        //mVariablesList.Location = referencePoint;
        mVariablesList.Hide();

        String selectedItem = mVariablesList.SelectedItem.ToString();

        if (this.Text[this.Text.Length - 1].ToString().ToUpper().Trim() == "@")
        {
            //Substitui o último @ pela variável
            this.Text = this.Text.Remove(this.Text.LastIndexOf('@'), 1) + selectedItem;

            //Esconde a lista de variáveis
            mVariablesList.Hide();

            //Define o foco no richTextBox
            this.Focus();

            //Seta o cursor do mouse para a última posição do rich text box.
            this.Select(this.Text.Length, this.Text.Length);
        }
    }

    protected void SyntaxKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) AdicionaVariavel();
        else if (e.KeyCode == Keys.Escape) mVariablesList.Hide();
    }
    /// <summary>
    /// OnTextChanged
    /// </summary>
    /// <param name="e"></param>
    protected override void OnTextChanged(EventArgs e)
    {
        //Aqui serve para adicionar as variáveis.
        mReferencePoint = this.Cursor.HotSpot;
        if (!String.IsNullOrEmpty(this.Text))
        {
            String lastChar = this.Text[this.Text.Length - 1].ToString().ToUpper().Trim();

            if (lastChar == "@")
            {
                //apresenta o intellisense.
                mVariablesList.Location = mReferencePoint;
                mVariablesList.SelectedIndex = 0;
                mVariablesList.Show();
                mVariablesList.Parent = this;
                mVariablesList.Focus();
            }
            else
            {
                mVariablesList.Location = mReferencePoint;
                //lb.Size = new Size(250, 100);
                mVariablesList.Hide();
            }
        }

        // Calculate shit here.
        m_nContentLength = this.TextLength;

        int nCurrentSelectionStart = SelectionStart;
        int nCurrentSelectionLength = SelectionLength;

        m_bPaint = false;

        // Find the start of the current line.
        m_nLineStart = nCurrentSelectionStart;
        while ((m_nLineStart > 0) && (Text[m_nLineStart - 1] != '\n'))
            m_nLineStart--;
        // Find the end of the current line.
        m_nLineEnd = nCurrentSelectionStart;
        while ((m_nLineEnd < Text.Length) && (Text[m_nLineEnd] != '\n'))
            m_nLineEnd++;
        // Calculate the length of the line.
        m_nLineLength = m_nLineEnd - m_nLineStart;
        // Get the current line.
        m_strLine = Text.Substring(m_nLineStart, m_nLineLength);

        // Process this line.
        //ProcessLine();
        ProcessAllLines();

        m_bPaint = true;

    }
    /// <summary>
    /// Process a line.
    /// </summary>
    private void ProcessLine()
    {
        // Save the position and make the whole line black
        int nPosition = SelectionStart;
        SelectionStart = m_nLineStart;
        SelectionLength = m_nLineLength;
        //SelectionColor = Color.Black;
        SelectionColor = Settings.NormalTextColor;

        // Process the keywords
        ProcessRegex(m_strKeywords, Settings.KeywordColor);
        // Process numbers
        if (Settings.EnableIntegers)
            ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", Settings.IntegerColor);
        // Process strings
        if (Settings.EnableStrings)
            ProcessRegex("\"[^\"\\\\\\r\\n]*(?:\\\\.[^\"\\\\\\r\\n]*)*\"", Settings.StringColor);
        // Process comments
        if (Settings.EnableComments && !string.IsNullOrEmpty(Settings.Comment))
            ProcessRegex(Settings.Comment + ".*$", Settings.CommentColor);

        SelectionStart = nPosition;
        SelectionLength = 0;
        //SelectionColor = Color.Black;
        SelectionColor = Settings.NormalTextColor;

        m_nCurSelection = nPosition;
    }
    /// <summary>
    /// Process a regular expression.
    /// </summary>
    /// <param name="strRegex">The regular expression.</param>
    /// <param name="color">The color.</param>
    private void ProcessRegex(string strRegex, Color color)
    {
        Regex regKeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Match regMatch;

        for (regMatch = regKeywords.Match(m_strLine); regMatch.Success; regMatch = regMatch.NextMatch())
        {
            // Process the words
            int nStart = m_nLineStart + regMatch.Index;
            int nLenght = regMatch.Length;
            SelectionStart = nStart;
            SelectionLength = nLenght;
            SelectionColor = color;
        }
    }
    /// <summary>
    /// Compiles the keywords as a regular expression.
    /// </summary>
    public void CompileKeywords()
    {
        for (int i = 0; i < Settings.Keywords.Count; i++)
        {
            string strKeyword = Settings.Keywords[i];

            if (i == Settings.Keywords.Count - 1)
                m_strKeywords += "\\b" + strKeyword + "\\b";
            else
                m_strKeywords += "\\b" + strKeyword + "\\b|";
        }
    }

    public void ProcessAllLines()
    {
        m_bPaint = false;

        int nStartPos = 0;
        int i = 0;
        int nOriginalPos = SelectionStart;
        while (i < Lines.Length)
        {
            m_strLine = Lines[i];
            m_nLineStart = nStartPos;
            m_nLineEnd = m_nLineStart + m_strLine.Length;

            ProcessLine();
            i++;

            nStartPos += m_strLine.Length + 1;
        }

        m_bPaint = true;
    }
}

/// <summary>
/// Class to store syntax objects in.
/// </summary>
public class SyntaxList
{
    public List<string> m_rgList = new List<string>();
    public Color m_color = new Color();
}

/// <summary>
/// Settings for the keywords and colors.
/// </summary>
public class SyntaxSettings
{
    SyntaxList m_rgKeywords = new SyntaxList();
    string m_strComment = "";
    Color m_colorComment = Color.Green;
    Color m_colorString = Color.Gray;
    Color m_colorInteger = Color.Red;
    bool m_bEnableComments = true;
    bool m_bEnableIntegers = true;
    bool m_bEnableStrings = true;
    Color m_normalText = Color.Black;

    #region Properties
    /// <summary>
    /// A list containing all keywords.
    /// </summary>
    public List<string> Keywords
    {
        get { return m_rgKeywords.m_rgList; }
    }
    /// <summary>
    /// The color of keywords.
    /// </summary>
    public Color KeywordColor
    {
        get { return m_rgKeywords.m_color; }
        set { m_rgKeywords.m_color = value; }
    }
    /// <summary>
    /// A string containing the comment identifier.
    /// </summary>
    public string Comment
    {
        get { return m_strComment; }
        set { m_strComment = value; }
    }
    /// <summary>
    /// The color of comments.
    /// </summary>
    public Color CommentColor
    {
        get { return m_colorComment; }
        set { m_colorComment = value; }
    }
    /// <summary>
    /// Enables processing of comments if set to true.
    /// </summary>
    public bool EnableComments
    {
        get { return m_bEnableComments; }
        set { m_bEnableComments = value; }
    }
    /// <summary>
    /// Enables processing of integers if set to true.
    /// </summary>
    public bool EnableIntegers
    {
        get { return m_bEnableIntegers; }
        set { m_bEnableIntegers = value; }
    }
    /// <summary>
    /// Enables processing of strings if set to true.
    /// </summary>
    public bool EnableStrings
    {
        get { return m_bEnableStrings; }
        set { m_bEnableStrings = value; }
    }
    /// <summary>
    /// The color of strings.
    /// </summary>
    public Color StringColor
    {
        get { return m_colorString; }
        set { m_colorString = value; }
    }
    /// <summary>
    /// The color of integers.
    /// </summary>
    public Color IntegerColor
    {
        get { return m_colorInteger; }
        set { m_colorInteger = value; }
    }
    /// <summary>
    /// The color of normal text.
    /// </summary>
    public Color NormalTextColor
    {
        get { return m_normalText; }
        set { m_normalText = value; }
    }

    #endregion
}