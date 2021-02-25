/***********************************************************************************************************
 * 
 * 
 *             UltraGridOptions - Classe com objetos e controles para atuar junto com um ultragrid
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Habilitar comandos com o botão direito do mouse sobre UltraGrid's
 *      Version:    2.0
 *      Date:       22/11/2018, at 22:12 PM AM
 *      Note:       <None>
 *      History:    Update - 22/11/2018 - 11:27 PM - Criado a primeira versão - V1.0 Lançada.
 *                  Update - 01/02/2019 - 11:12 AM - Adicionado a opção de ordenar as colunas de forma
 *                      ascendente, descendente e remover a ordenação - V1.1 Lançada
 *                  Update - 01/02/2019 - 11:20 AM - Adicionado o nome da coluna nos controles
 *                      para mostrar ao usuário qual coluna será ordenado - V1.2 Lançada
 *                  Update - 04/02/2019 - 07:46 PM - Adicionado o método 'LimpaOrdenacaoColunasGrid' - V1.3 Lançada.
 *                  Update - 22/03/2019 - 07:05 AM - Tornado o método 'GridPossuiAgrupamento' público - V1.4 Lançada.
 *                  Update - 25/03/2019 - 06:21 AM - Adicionado a opção para agrupar por coluna - V1.5 Lançada.
 *                  Update - 03/04/2019 - 11:27 AM - Resolvido um problema no método 'udgv_MouseUp', onde,
 *                      quando havia um agrupamento com mais de uma coluna, o CMS para remover o agrupador
 *                      não aparecia corretamente - V1.6 Lançada.
 *                  Update - 03/04/2019 - 12:15 AM - Adicionado a possibilidade de agrupar por colunas em
 *                      até dois níveis de agrupamento - V1.7 Lançada.
 *                  Update - 03/04/2019 - 05:09 PM - Roolback na parte de permitir mais de um agrupamento
 *                      por coluna, pois não mostrava mais os CSM de opções - V1.8 Lançada.
 *                  Update - 17/05/2019 - 10:58 AM - Adicionado as variáveis 'mOrdenarCrescente' e 'mOrdenarDecrescente' - V1.9 Lançada.]
 *                  Update - 17/05/2019 - 11:38 AM - Adicionado as variáveis 'mSelecionarColunas', 
 *                      'mRemoverOrdenacaoColunas' e 'mAgruparColunas' - V2.0 Lançada.
 *                  
 * 
***********************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;


/// <summary>
///     Classe que permite realizar executar alguns comandos com o botão
/// direito do mouse no 'UltraGrid'.
/// </summary>
public class UltraGridOptions
{

    #region Variáveis globais

    /// <summary>
    ///     Objeto contendo a grade de dados.
    /// </summary>
    private UltraGrid mUdgv;

    /// <summary>
    ///     Contém um objeto da coluna que o usuário deseja ordenar.
    /// </summary>
    private UltraGridColumn mColunaOrdenar;

    /// <summary>
    ///     Contém o ícone para seleção de colunas.
    /// </summary>
    private Image mImgSelecionarColunas;

    /// <summary>
    ///     Contém o ícone para order o grid por ordem crescente.
    /// </summary>
    private Image mImgOrdenarCrescente;

    /// <summary>
    ///     Contém o ícone para order o grid por ordem decrescente.
    /// </summary>
    private Image mImgOrdenarDecrescente;

    /// <summary>
    ///     Contém o ícone para remover a ordenação do grid.
    /// </summary>
    private Image mImgRemoverOrdenacaoColunas;

    /// <summary>
    ///     Contém o ícone para agrupamento das colunas.
    /// </summary>
    private Image mImgAgruparColunas;

    #endregion

    #region Context Menu's Strip's

    /// <summary>
    ///     Menu suspenso para seleção de colunas.
    /// </summary>
    private ContextMenuStrip cms_EscolherColunas;

    /// <summary>
    ///     Menu suspenso para remover o agrupamento realizado por colunas.
    /// </summary>
    private ContextMenuStrip cms_RemoveGroupByBox;

    /// <summary>
    ///     Menu suspenso para expandir/recolher as colunas agrupadas.
    /// </summary>
    private ContextMenuStrip cms_Expandir;

    #endregion

    #region Botões (ToolStripMenuItem)

    /// <summary>
    ///     ToolStripMenuItem para escolher as colunas
    /// </summary>
    private ToolStripMenuItem btnEscolherColunas;

    /// <summary>
    ///     ToolStripMenuItem para remover o agrupamento das colunas.
    /// </summary>
    private ToolStripMenuItem btnRemoveGroup;

    /// <summary>
    ///     ToolStripMenuItem para expandir as tuplas da interface.
    /// </summary>
    private ToolStripMenuItem btnExpandirTodos;

    /// <summary>
    ///     ToolStripMenuItem para recolher as tuplas da interface.
    /// </summary>
    private ToolStripMenuItem btnRecolherTodos;

    /// <summary>
    ///     ToolStripMenuItem para agrupar por colunas.
    /// </summary>
    private ToolStripMenuItem btnAgruparColuna;

    /// <summary>
    ///     ToolStripMenuItem para ordenar os dados da interface como ascendente
    /// </summary>
    private ToolStripMenuItem btnAscendente;

    /// <summary>
    ///     ToolStripMenuItem para ordenar os dados da interface como descendente
    /// </summary>
    private ToolStripMenuItem btnDescendente;

    /// <summary>
    ///     ToolStripMenuItem para remover a ordenação
    /// </summary>
    private ToolStripMenuItem btnRemoverOrdenacao;

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instância o objeto da classe, o qual adiciona ao Grid de dados,
    /// menus suspensos (Context Menu's Strip's) e eventos de botões.
    /// </summary>
    /// <param name="Udgv">Ultra Data Grid View</param>
    /// <param name="RemoverOrdenacao">True para remover a ordenação das colunas.</param>
    /// <param name="SelColunas">Ícone para seleção de colunas</param>
    /// <param name="OrdenarCrescente">Ícone para ordenar os dados crescentes</param>
    /// <param name="OrdenarDecrescente">Ícone para ordenar os dados decrescentes</param>
    /// <param name="RemoverOrdenacaoColunas">Ícone para remover a ordenação</paramparam>
    /// <param name="AgruparColunas">Ícone para agrupar pela coluna.</param>
    public UltraGridOptions(UltraGrid Udgv, Boolean RemoverOrdenacao = false, Image SelColunas = null, 
                               Image OrdenarCrescente = null, Image OrdenarDecrescente = null, 
                               Image RemoverOrdenacaoColunas = null, Image AgruparColunas = null) 
    {
        mUdgv = Udgv;

        if (RemoverOrdenacao) LimpaOrdenacaoColunasGrid();

        mImgSelecionarColunas = SelColunas;
        mImgOrdenarCrescente = OrdenarCrescente;
        mImgOrdenarDecrescente = OrdenarDecrescente;
        mImgRemoverOrdenacaoColunas = RemoverOrdenacaoColunas;
        mImgAgruparColunas = AgruparColunas;

        mUdgv.MouseUp += new System.Windows.Forms.MouseEventHandler(udgv_MouseUp);
        AdicionaControlesCms();
    }

    #endregion

    #region Métodos

    /// <summary>
    ///     Método responsável por limpar a ordenação das colunas de todos os UltraGridsView
    /// </summary>
    private void LimpaOrdenacaoColunasGrid()
    {
        mUdgv.DisplayLayout.Bands[0].SortedColumns.Clear();
        mUdgv.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
    }

    /// <summary>
    ///     Inicia a adição dos controles (botões) nos CMS (Context Menu's Strip's).
    /// </summary>
    private void AdicionaControlesCms()
    {
        AdicionaControles_Escolher();
        AdicionaControles_Remover();
        AdicionaControles_Expandir();
    }

    /// <summary>
    ///     Adiciona os controles no CMS 'cms_EscolherColunas'
    /// </summary>
    private void AdicionaControles_Escolher()
    {
        //Instância o ToolStripMenuItem para escolher as colunas
        btnEscolherColunas = new ToolStripMenuItem();
        btnEscolherColunas.Name = "btnEscolherColunas";

        if (mImgSelecionarColunas != null)
            btnEscolherColunas.Image = mImgSelecionarColunas;

        btnEscolherColunas.Size = new System.Drawing.Size(162, 22);
        btnEscolherColunas.Text = "Escolher colunas";
        btnEscolherColunas.Click += new System.EventHandler(btnEscolherColunas_Click);


        //Instância o ToolStripMenuItem para ordenar de forma ascendente a coluna
        btnAscendente = new ToolStripMenuItem();
        btnAscendente.Name = "btnAscendente";
        btnAscendente.Size = new System.Drawing.Size(162, 22);

        if (mImgOrdenarCrescente != null)
            btnAscendente.Image = mImgOrdenarCrescente;

        btnAscendente.Text = "Ordenar coluna ascendente";
        btnAscendente.Click += new System.EventHandler(btnAscendente_Click);


        //Instância o ToolStripMenuItem para ordenar de forma descendente a coluna
        btnDescendente = new ToolStripMenuItem();
        btnDescendente.Name = "btnDescendente";

        if (mImgOrdenarDecrescente != null)
            btnDescendente.Image = mImgOrdenarDecrescente;

        btnDescendente.Size = new System.Drawing.Size(162, 22);
        btnDescendente.Text = "Ordenar coluna descendente";
        btnDescendente.Click += new System.EventHandler(btnDescendente_Click);


        //Instância o ToolStripMenuItem para remover a ordenação
        btnRemoverOrdenacao = new ToolStripMenuItem();
        btnRemoverOrdenacao.Name = "btnRemoverOrdenacao";

        if (mImgRemoverOrdenacaoColunas != null)
            btnRemoverOrdenacao.Image = mImgRemoverOrdenacaoColunas;

        btnRemoverOrdenacao.Size = new System.Drawing.Size(162, 22);
        btnRemoverOrdenacao.Text = "Remover ordenação";
        btnRemoverOrdenacao.Click += new System.EventHandler(btnRemoverOrdenacao_Click);

        btnAgruparColuna = new ToolStripMenuItem();
        btnAgruparColuna.Name = "btnAgruparColuna";

        if (mImgAgruparColunas != null)
            btnAgruparColuna.Image = mImgAgruparColunas;

        btnAgruparColuna.Size = new Size(162, 22);
        btnAgruparColuna.Text = "Agrupar por coluna";
        btnAgruparColuna.Click += new System.EventHandler(btnAgruparColuna_Click);


        //Instância o CMS e adiciona os controles (ToolStripMenuItem)
        cms_EscolherColunas = new ContextMenuStrip();
        cms_EscolherColunas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnEscolherColunas,
                                                                                      btnAgruparColuna,
                                                                                      btnAscendente, 
                                                                                      btnDescendente, 
                                                                                      btnRemoverOrdenacao
                                                                                    });
        cms_EscolherColunas.Name = "cms_EscolherColunas";
        cms_EscolherColunas.Size = new System.Drawing.Size(163, 26);
    }

    /// <summary>
    ///     Adiciona os controles no CMS 'cms_RemoveGroupByBox'
    /// </summary>
    private void AdicionaControles_Remover()
    {
        //Instância o ToolStripMenuItem
        btnRemoveGroup = new ToolStripMenuItem();
        btnRemoveGroup.Name = "btnRemoveGroup";
        btnRemoveGroup.Size = new System.Drawing.Size(179, 22);
        btnRemoveGroup.Text = "Remover agrupador";
        btnRemoveGroup.Click += new System.EventHandler(btnRemoveGroup_Click);

        //Instância o CMS e adiciona o controle (ToolStripMenuItem)
        cms_RemoveGroupByBox = new ContextMenuStrip();
        cms_RemoveGroupByBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnRemoveGroup });
        cms_RemoveGroupByBox.Name = "cms_RemoveGroupByBox";
        cms_RemoveGroupByBox.Size = new System.Drawing.Size(180, 26);
    }

    /// <summary>
    ///     Adiciona os controles no CMS 'cms_Expandir'
    /// </summary>
    private void AdicionaControles_Expandir()
    {
        //Instância o ToolStripMenuItem
        btnExpandirTodos = new ToolStripMenuItem();
        btnExpandirTodos.Name = "btnExpandirTodos";
        btnExpandirTodos.Size = new System.Drawing.Size(153, 22);
        btnExpandirTodos.Text = "Expandir todos";
        btnExpandirTodos.Click += new System.EventHandler(btnExpandirTodos_Click);

        btnRecolherTodos = new ToolStripMenuItem();
        btnRecolherTodos.Name = "btnRecolherTodos";
        btnRecolherTodos.Size = new System.Drawing.Size(153, 22);
        btnRecolherTodos.Text = "Recolher todos";
        btnRecolherTodos.Click += new System.EventHandler(btnRecolherTodos_Click);

        //Instância o CMS e adiciona o controle (ToolStripMenuItem)
        cms_Expandir = new ContextMenuStrip();
        cms_Expandir.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnExpandirTodos, btnRecolherTodos });
        cms_Expandir.Name = "cms_Expandir";
        cms_Expandir.Size = new System.Drawing.Size(154, 48);
    }

    #endregion

    #region Eventos

    /// <summary>
    ///     Evento que identifica aonde na área gráfica o usuário clicou para apresentar
    /// os menus suspensos.
    /// </summary>
    private void udgv_MouseUp(object sender, MouseEventArgs e)
    {
        //Variáveis utilizadas dentro das condições.
        int xMax = 0, yMax = 0;

        //Link de ajuda:
        //https://www.infragistics.com/community/forums/f/ultimate-ui-for-windows-forms/13204/ultragrid-groupbybox-height

        //Obtenho esse 'gridElement' para saber o tamanho do groupbybox, para determinar
        //até que ponto do click na área gráfico eu posso mostrar determinado menu suspenso.
        UIElement gridElement = mUdgv.DisplayLayout.UIElement;
        if (gridElement != null)
        {
            UIElement groupByBoxUIElement = gridElement.GetDescendant(typeof(GroupByBoxUIElement));
            if (groupByBoxUIElement != null)
            {
                xMax = groupByBoxUIElement.Rect.Width;
                yMax = groupByBoxUIElement.Rect.Height;
            }
        }

        //Caso o grid tenha um agrupamento, mostro o menu suspenso para expandir todos os agrupamentos (não mexer).
        if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Y >= 0 && e.X >= 0 && e.X <= 10)
        {
            //Link de ajuda:
            //http://help.infragistics.com/Help/Doc/WinForms/2013.1/CLR4.0/html/WinGrid_Expand_All_Rows_in_WinGrid.html
            //if (udgv.DisplayLayout.Bands[0].ColumnFilters.Count > 0) 

            if (GridPossuiAgrupamento(mUdgv)) cms_Expandir.Show(mUdgv, new Point(e.X, e.Y));
        }
        //Caso o usuário clicar sobre o agrupador de colunas, exibe o Context Menu Strip para limpar o agrupamento.
        else if (e.Button == System.Windows.Forms.MouseButtons.Right && e.X <= xMax && e.Y <= yMax)
        {
            //mUdgv.DisplayLayout.GroupByBox.Appearance.
            cms_RemoveGroupByBox.Show(mUdgv, new Point(e.X, e.Y));
        }
        //Caso o usuário clicar sobre as colunas, exibe o Context Menu Strip para escolher as colunas.
        else if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Y >= 35 && e.Y <= 55)
        {
            //Obtém um objeto da coluna
            UIElement ue = ((UltraGrid)sender).DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
            mColunaOrdenar = (UltraGridColumn)ue.GetContext(typeof(UltraGridColumn), true);

            //Adiciona o nome da coluna no controle 'btnAscendente/btnDescendente'
            if (mColunaOrdenar != null)
            {
                btnAgruparColuna.Text = String.Format("Agrupar pela coluna '{0}'", mColunaOrdenar.Key);
                btnAscendente.Text = String.Format("Ordenar coluna '{0}' ascendente", mColunaOrdenar.Key);
                btnDescendente.Text = String.Format("Ordenar coluna '{0}' descendente", mColunaOrdenar.Key);
            }

            cms_EscolherColunas.Show(mUdgv, new Point(e.X, e.Y));
        }
    }

    /// <summary>
    ///     Remove o agrupamento feito por colunas (realizado pelo usuário).    
    /// </summary>
    private void btnRemoveGroup_Click(object sender, EventArgs e)
    {
        //Link de ajuda
        //http://help.infragistics.com/Help/Doc/WinForms/2012.2/CLR4.0/html/Infragistics4.Win.UltraWinGrid.v12.2~Infragistics.Win.UltraWinGrid.UltraGridBand~ClearGroupByColumns.html

        mUdgv.DisplayLayout.Bands[0].ClearGroupByColumns();
    }

    /// <summary>
    ///     Evento que permite selecionar as colunas.
    /// </summary>
    private void btnEscolherColunas_Click(object sender, EventArgs e)
    {
        try
        {
            Objects.EscolheColunasGrid(mUdgv);
        }
        catch (Exception ex)
        {
            Objects.CadastraNovoLog(true, "Erro ao tentar selecionas as colunas", "FrmSelColunasGrid", "escolherColunasToolStripMenuItem_Click", "<None>", "<None>", e_TipoErroEx.Erro, ex);
        }
    }

    /// <summary>
    ///     Evento que permite agrupar dados pela coluna selecionada pelo usuário.
    /// </summary>
    private void btnAgruparColuna_Click(object sender, EventArgs e)
    {
        if (mColunaOrdenar != null)
        {
            //mColunaOrdenar.GroupByEvaluator = new MyGroupByEvaluator();
            //mColunaOrdenar.HiddenWhenGroupBy = DefaultableBoolean.False;

            mUdgv.DisplayLayout.Bands[0].SortedColumns.Add(mColunaOrdenar, false, true);
        }
    }

    /// <summary>
    ///     Evento que expande todas as linhas conforme agrupamentos realizados pelo usuário.
    /// </summary>
    private void btnExpandirTodos_Click(object sender, EventArgs e)
    {
        mUdgv.Rows.ExpandAll(true);
    }

    /// <summary>
    ///     Evento que recolhe todas as linhas conforme agrupamentos realizados pelo usuário.
    /// </summary>
    private void btnRecolherTodos_Click(object sender, EventArgs e)
    {
        mUdgv.Rows.CollapseAll(true);
    }

    /// <summary>
    ///     Evento que ordena os dados do grid de forma ascendente.
    /// </summary>
    private void btnAscendente_Click(object sender, EventArgs e)
    {
        if (mColunaOrdenar != null)
        {
            mUdgv.DisplayLayout.Bands[0].Columns[mColunaOrdenar.Key].SortIndicator = SortIndicator.Ascending;
            mUdgv.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
        }
    }

    /// <summary>
    ///     Evento que ordena os dados do grid de forma descendente.
    /// </summary>
    private void btnDescendente_Click(object sender, EventArgs e)
    {
        if (mColunaOrdenar != null)
        {
            mUdgv.DisplayLayout.Bands[0].Columns[mColunaOrdenar.Key].SortIndicator = SortIndicator.Descending;
            mUdgv.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
        }
    }

    /// <summary>
    ///     Evento que remove a ordenação geral das colunas.
    /// </summary>
    private void btnRemoverOrdenacao_Click(object sender, EventArgs e)
    {
        mUdgv.DisplayLayout.Bands[0].SortedColumns.Clear();
        mUdgv.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
    }
    
    #endregion

    #region Métodos estáticos

    /// <summary>
    ///     Método responsável por verificar se há alguma coluna na seção 'GroupByBox'.
    /// </summary>
    /// <param name="UltraGrid">UltraDataGridView que terá a ordenação das colunas limpa.</param>
    /// <remarks>Link de ajuda: http://help.infragistics.com/Help/Doc/WinForms/2011.2/CLR2.0/html/Infragistics2.Win.UltraWinGrid.v11.2~Infragistics.Win.UltraWinGrid.UltraGridColumn~IsGroupByColumn.html</remarks>
    /// <returns>True caso houve alguma coluna na seção 'GroupByBox'</returns>
    public static Boolean GridPossuiAgrupamento(Infragistics.Win.UltraWinGrid.UltraGrid UltraGrid)
    {
        try
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand band = UltraGrid.DisplayLayout.Bands[0];

            for (int i = 0; i < band.Columns.Count; i++)
            {
                ////Adicionei a condição para ser mais rápido, onde, irei considerar apenas as colunas visíveis.
                //if (!band.Columns[i].Hidden) continue;

                if (band.Columns[i].IsGroupByColumn)
                    return true;
            }
            return false;
        }
        catch { return false; }
    }

    #endregion
}

/// <summary>
/// 
/// </summary>
/// <remarks>
///     Essa classe permite ordenarmos/agruparmos as colunas por imagens.
///     
///     Link 1: http://help.infragistics.com/Help/Doc/WinForms/2015.2/CLR4.0/html/WinGrid_Display_Cell_Image_in_GroupBy_Header_Whats_New_2005_3.html
///     Link 2: http://help.infragistics.com/Help/Doc/WinForms/2015.2/CLR4.0/html/Infragistics4.Win.UltraWinGrid.v15.2~Infragistics.Win.UltraWinGrid.UltraGridColumn~GroupByEvaluator.html
/// </remarks>
internal class MyGroupByEvaluator : Infragistics.Win.UltraWinGrid.IGroupByEvaluator
{
    public object GetGroupByValue(UltraGridGroupByRow groupbyRow, UltraGridRow row)
    {

        string val = "";

        // Get the default value from the groupbyRow.
        if (groupbyRow.Value == null)
            val = "";
        else
        {
            if (groupbyRow.Value.GetType().ToString() == "System.Drawing.Bitmap")
            {
                //val = Infragistics.Win.FormattedLinkLabel.FormattedLinkEditor.EncodeImage((Image)groupbyRow.Value);
                val = groupbyRow.ToolTipText.ToString();
            }
            else val = groupbyRow.Value.ToString();
        }

        // If it is longer than 2 characters truncate it.
        //if (val.Length > 2)
        //    val = val.Substring(0, 2);

        // Convert the string to uppercase for display in the group-by row's description.
        return val.ToUpper();

    }

    public bool DoesGroupContainRow(UltraGridGroupByRow groupbyRow, UltraGridRow row)
    {

        // Get the related cell's value as a string.
        string cellValue = row.Cells[groupbyRow.Column].Value.ToString();

        // If it is longer than 2 characters truncate it.
        if (cellValue.Length > 2)
            cellValue = cellValue.Substring(0, 2);

        // Do a case insensitive compare.
        return 0 == string.Compare(groupbyRow.Value.ToString(), cellValue, true);
    }

}