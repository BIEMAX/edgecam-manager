/*****************************************************************************************************************************
 * 
 * 
 *                  Exporter - Classe com métodos específicos para exportação de dados em arquivos
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe para exportação de dados em arquivos
 *      Version:    1.2
 *      Date:       28/08/2018, at 04:15 PM
 *      Note:       <None>
 *      Updates:    Update - 28/08/2018 - 04:15 PM - Primeira versão da classe - V1.0 Lançada
 *                  Update - 05/02/2019 - 04:15 PM - Adicionado um context menu strip dentro da classe que
 *                      permite a exportação diretamente dentro do sistema - V1.1 Lançada
 *                  Update - 21/03/2019 - 05:17 PM - Resolvido um problema com o datatable null - V1.2 Lançada.
 *      
 *****************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;

/// <summary>
///     Classe que permite a exportação da grade de dados em arquivos definidos pelo usuário.
/// </summary>
public class Exporter
{

    #region Variáveis da classe (privadas/protegidas)

    private e_SkaTipoGrade mTipoGrade;
    private DataTable mDt;
    private Control mControlAddCms;
    private String mNomeModulo;
    
    /// <summary>
    ///     Contém um objeto do tipo 'UltraGrid'.
    /// </summary>
    private Infragistics.Win.UltraWinGrid.UltraGrid mUdgv;

    /// <summary>
    ///     Contém um objeto do tipo 'DataGrid'.
    /// </summary>
    private DataGridView mDtgv;

    private List<String> mNomesColunas;

    /// <summary>
    ///     Contém as opções para exportação dos dados.
    /// </summary>
    private ContextMenuStrip mCms;

    //Botões
    private ToolStripMenuItem tsm_Pdf;
    private ToolStripMenuItem tsm_Docx;
    private ToolStripMenuItem tsm_Doc;
    private ToolStripMenuItem tsm_Xlsx;
    private ToolStripMenuItem tsm_Xls;
    private ToolStripMenuItem tsm_Xml;
    private ToolStripMenuItem tsm_Txt;
    private ToolStripMenuItem tsm_Csv;
    private ToolStripMenuItem tsm_Png;
    private ToolStripMenuItem tsm_Jpg;
    private ToolStripMenuItem tsm_Bmp;

    #endregion

    #region Enumeradores

    /// <summary>
    ///     Define qual instância o programador usou.
    /// </summary>
    private enum e_SkaTipoGrade
    {
        DataGridView, UltraGrid
    }

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instancia o objeto da classe informando que os dados serão exportados através de um
    /// UltraGrid.
    /// </summary>
    /// <param name="Udgv">UltraGrid que é será exportado os dados.</param>
    /// <param name="ControleAdicionarCms">Controle (Botão, combobox) que será adicionado o 'context menu strip' para exportação dos dados.</param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    public Exporter(Infragistics.Win.UltraWinGrid.UltraGrid Udgv, Control ControleAdicionarCms, String NomeModulo) 
    {
        mTipoGrade = e_SkaTipoGrade.UltraGrid;
        mUdgv = Udgv;
        mDt = mUdgv.DataSource as DataTable;
        mControlAddCms = ControleAdicionarCms;
        mNomeModulo = NomeModulo;
        InicializaInstanciasOpcoesCms();
    }

    /// <summary>
    ///     Instancia o objeto da classe informando que os dados serão exportados através de um
    /// DataGridView.
    /// </summary>
    /// <param name="Dtgv">DataGridView que é será exportado os dados.</param>
    /// <param name="ControleAdicionarCms">Controle (Botão, combobox) que será adicionado o 'context menu strip' para exportação dos dados.</param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    public Exporter(DataGridView Dtgv, Control ControleAdicionarCms, String NomeModulo)
    {
        mTipoGrade = e_SkaTipoGrade.DataGridView;
        mDtgv = Dtgv;
        mDt = mDtgv.DataSource as DataTable;
        mControlAddCms = ControleAdicionarCms;
        mNomeModulo = NomeModulo;
        InicializaInstanciasOpcoesCms();
    }

    ~Exporter() { }

    #endregion

    #region Métodos privados

    /// <summary>
    ///     Método que instancia os controles do tipo 'ToolStripMenuItem' e 
    /// os adicionam no 'mCms'.
    /// </summary>
    private void InicializaInstanciasOpcoesCms()
    {
        // tsm_Pdf
        tsm_Pdf = new ToolStripMenuItem();
        tsm_Pdf.Image = global::Edgecam_Manager.Properties.Resources.pdf;
        tsm_Pdf.Name = "tsm_Pdf";
        tsm_Pdf.Size = new System.Drawing.Size(102, 22);
        tsm_Pdf.Text = "Pdf";
        tsm_Pdf.Click += new System.EventHandler(tsm_Pdf_Click);
 
        // tsm_Docx
        tsm_Docx = new ToolStripMenuItem();
        tsm_Docx.Image = global::Edgecam_Manager.Properties.Resources.docx;
        tsm_Docx.Name = "tsm_Docx";
        tsm_Docx.Size = new System.Drawing.Size(102, 22);
        tsm_Docx.Text = "Docx";
        tsm_Docx.Click += new System.EventHandler(tsm_Docx_Click);

        // tsm_Doc
        tsm_Doc = new ToolStripMenuItem();
        tsm_Doc.Image = global::Edgecam_Manager.Properties.Resources.doc;
        tsm_Doc.Name = "tsm_Doc";
        tsm_Doc.Size = new System.Drawing.Size(102, 22);
        tsm_Doc.Text = "Doc";
        tsm_Doc.Click += new System.EventHandler(tsm_Doc_Click);

        // tsm_Xlsx
        tsm_Xlsx = new ToolStripMenuItem();
        tsm_Xlsx.Image = global::Edgecam_Manager.Properties.Resources.xlsx;
        tsm_Xlsx.Name = "tsm_Xlsx";
        tsm_Xlsx.Size = new System.Drawing.Size(102, 22);
        tsm_Xlsx.Text = "Xlsx";
        tsm_Xlsx.Click += new System.EventHandler(tsm_Xlsx_Click);

        // tsm_Xls
        tsm_Xls = new ToolStripMenuItem();
        tsm_Xls.Image = global::Edgecam_Manager.Properties.Resources.xls;
        tsm_Xls.Name = "tsm_Xls";
        tsm_Xls.Size = new System.Drawing.Size(102, 22);
        tsm_Xls.Text = "Xls";
        tsm_Xls.Click += new System.EventHandler(tsm_Xls_Click);

        // tsm_Xml
        tsm_Xml = new ToolStripMenuItem();
        tsm_Xml.Image = global::Edgecam_Manager.Properties.Resources.xml;
        tsm_Xml.Name = "tsm_Xml";
        tsm_Xml.Size = new System.Drawing.Size(102, 22);
        tsm_Xml.Text = "Xml";
        tsm_Xml.Click += new System.EventHandler(tsm_Xml_Click);
         
        // tsm_Txt
        tsm_Txt = new ToolStripMenuItem();
        tsm_Txt.Image = global::Edgecam_Manager.Properties.Resources.txt;
        tsm_Txt.Name = "tsm_Txt";
        tsm_Txt.Size = new System.Drawing.Size(102, 22);
        tsm_Txt.Text = "Texto";
        tsm_Txt.Click += new System.EventHandler(tsm_Txt_Click);
        
        // tsm_Csv
        tsm_Csv = new ToolStripMenuItem();
        tsm_Csv.Image = global::Edgecam_Manager.Properties.Resources.csv;
        tsm_Csv.Name = "tsm_Csv";
        tsm_Csv.Size = new System.Drawing.Size(102, 22);
        tsm_Csv.Text = "Csv";
        tsm_Csv.Click += new System.EventHandler(tsm_Csv_Click);
         
        // tsm_Png
        tsm_Png = new ToolStripMenuItem(); 
        tsm_Png.Image = global::Edgecam_Manager.Properties.Resources.png;
        tsm_Png.Name = "tsm_Png";
        tsm_Png.Size = new System.Drawing.Size(102, 22);
        tsm_Png.Text = "Png";
        tsm_Png.Click += new System.EventHandler(tsm_Png_Click);
         
        // tsm_Jpg
        tsm_Jpg = new ToolStripMenuItem(); 
        tsm_Jpg.Image = global::Edgecam_Manager.Properties.Resources.jpg;
        tsm_Jpg.Name = "tsm_Jpg";
        tsm_Jpg.Size = new System.Drawing.Size(102, 22);
        tsm_Jpg.Text = "Jpg";
        tsm_Jpg.Click += new System.EventHandler(tsm_Jpg_Click);
        
        // tsm_Bmp
        tsm_Bmp = new ToolStripMenuItem(); 
        tsm_Bmp.Image = global::Edgecam_Manager.Properties.Resources.bmp;
        tsm_Bmp.Name = "tsm_Bmp";
        tsm_Bmp.Size = new System.Drawing.Size(102, 22);
        tsm_Bmp.Text = "Bmp";
        tsm_Bmp.Click += new System.EventHandler(tsm_Bmp_Click);

        // cmsExportar
        mCms = new ContextMenuStrip();
        mCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsm_Pdf, tsm_Docx, tsm_Doc, tsm_Xlsx, tsm_Xls, tsm_Xml, tsm_Txt, tsm_Csv, tsm_Png, tsm_Jpg, tsm_Bmp });
        mCms.Name = "cmsExporter";
        mCms.Size = new System.Drawing.Size(103, 246);
    }

    /// <summary>
    ///     Método responsável por obter o nome das colunas para exportar aos arquivos de texto.
    /// </summary>
    /// <returns>Lista de string contendo o nome de todas as colunas do 'DataTable'</returns>
    private List<String> ObtemNomesColunas()
    {
        //Dictionary<string, object> ret = new Dictionary<string, object>();

        //for (int x = 0; x < mDt.Columns.Count; x++)
        //{
        //    //Forma que encontrei de obter o nome das colunas.
        //    ret.Add(x.ToString(), mDt.Columns[x].ColumnName.ToString());
        //}

        if (mDt != null && mDt.Rows.Count > 0)
        {
            return mDt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray().ToList();
        }

        return null;
    }

    /// <summary>
    ///     Remove as colunas do tipo 'Image' para geração de arquivos XML's principalmente.
    /// </summary>
    private void RemoveColunasTipoImagem()
    {
        //Utilizei essa função para remover as tabelas de imagens, pois estava dando exceção.
        //{Name = "Image" FullName = "System.Drawing.Image"}
        List<String> dtColunasImages = mDt.Columns.Cast<DataColumn>().Where(x => x.DataType.Name.ToUpper() == "IMAGE").Select(y => y.ColumnName).ToArray().ToList();
        for (int x = 0; x < dtColunasImages.Count; x++)
        {
            mDt.Columns.Remove(dtColunasImages[x].ToString());
        }
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo PDF com todas as suas colunas e dados.
    /// </summary>
    /// <remarks>Não adiciona colunas do tipo 'Image' (Fullname="System.Drawing.Image")</remarks>
    private void ExportaEmPdf()
    {
        mNomesColunas = ObtemNomesColunas();

        RemoveColunasTipoImagem();

        if (mDt.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.pdf", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None);

            Document dc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 1f, 1f, 1.5f, 1.5f);
            //dc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

            PdfWriter pdfw = PdfWriter.GetInstance(dc, fs);
            dc.Open();

            //Cabeçalho (Título)
            BaseFont bfh = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bfh, 16, 1, iTextSharp.text.Color.GRAY);
            
            Paragraph p = new Paragraph();
            p.Alignment = Element.ALIGN_CENTER;
            p.Add(new Chunk(mNomeModulo, f));
            
            dc.Add(p);

            //Adiciona uma linha como 'quebra de página' visível.
            p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.Color.BLACK, Element.ALIGN_LEFT, 1)));
            dc.Add(p);

            //Adiciona uma quebra de linha para iniciar a inserção dos dados.
            dc.Add(new Chunk("\n", f));

            //Cria uma tabela para inserir os dados.
            PdfPTable table = new PdfPTable(mNomesColunas.Count - 1);
            table.WidthPercentage = 95;

            //Cabeçalho da tabela (nome das colunas)
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntColumnHeader = new iTextSharp.text.Font(btnColumnHeader, 6, 1, iTextSharp.text.Color.WHITE);

            for (int i = 0; i < mDt.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = iTextSharp.text.Color.GRAY;
                cell.AddElement(new Chunk(mDt.Columns[i].ColumnName.ToUpper(), fntColumnHeader));

                table.AddCell(cell);
            }

            //Alimenta os dados na planilha
            iTextSharp.text.Font fontTinyItalic = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.ITALIC, iTextSharp.text.Color.BLACK);
            PdfPCell theCell = null;

            for (int i = 0; i < mDt.Rows.Count; i++)
            {
                for (int j = 0; j < mDt.Columns.Count; j++)
                {
                    //table.AddCell(mDt.Rows[i][j].ToString());
                    theCell = new PdfPCell(new Paragraph(mDt.Rows[i][j].ToString(), fontTinyItalic));
                    table.AddCell(theCell);
                }
            }

            dc.Add(table);
            dc.Close();
            pdfw.Close();
            fs.Close();

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo PDF.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo DOCX com todas as suas colunas e dados.
    /// </summary
    /// <remarks>Não adiciona colunas do tipo 'Image' (Fullname="System.Drawing.Image")</remarks>
    private void ExportaEmDocx()
    {
        mNomesColunas = ObtemNomesColunas();

        RemoveColunasTipoImagem();

        if (mDt.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.docx", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            //Instancio o objeto para criar uma nova planilha apenas.
            Word word = new Word();
            word.CriaWord(file, false, false);

            //Encerro os processos, pois precisei reinstanciar o objeto para inserir os dados no excel.
            word.MataProcessosWord();
            word.FecharWord(true);

            //Reinstancio o objeto para inserir os dados.
            word = new Word(file, true);

            //Insere os valores das tuplas
            word.InsereDados_Documento(mDt, true);
            
            //Não precisa da linha abaixo, pois ele já salva dentro do método acima.
            //word.SalvarWord();

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo DOCX.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo DOC com todas as suas colunas e dados.
    /// </summary>
    /// <remarks>Não adiciona colunas do tipo 'Image' (Fullname="System.Drawing.Image")</remarks>
    private void ExportaEmDoc()
    {
        mNomesColunas = ObtemNomesColunas();

        RemoveColunasTipoImagem();

        if (mDt.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.doc", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            //Instancio o objeto para criar uma nova planilha apenas.
            Word word = new Word();
            word.CriaWord(file, false, false);

            //Encerro os processos, pois precisei reinstanciar o objeto para inserir os dados no excel.
            word.MataProcessosWord();
            word.FecharWord(true);

            //Reinstancio o objeto para inserir os dados.
            word = new Word(file, true);

            //Insere os valores das tuplas
            word.InsereDados_Documento(mDt, true);

            //Não precisa da linha abaixo, pois ele já salva dentro do método acima.
            //word.SalvarWord();

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo DOC.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo XLSX com todas as suas colunas e dados.
    /// </summary>
    private void ExportaEmXlsx()
    {
        mNomesColunas = ObtemNomesColunas();

        if (mDt.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.xlsx", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            //Instancio o objeto para criar uma nova planilha apenas.
            Excel excel = new Excel();
            excel.CriaExcel(file, false, true);

            //Encerro os processos, pois precisei reinstanciar o objeto para inserir os dados no excel.
            //excel.MataProcessosExcel();
            Excel.MataProcessosExcel_EmExecucao();
            excel.FecharExcel(true);

            //Reinstancio o objeto para inserir os dados.
            excel = new Excel(file, 1, true);

            //Insere o nome das colunas na planilha de dados do Excel.
            for (int x = 1; x < mNomesColunas.Count; x++)
            {
                excel.InsereDados_Planilha(1, x, mNomesColunas[x - 1].ToString(), Excel.e_TipoFormatacaoValor.texto);
            }

            //Insere os valores das tuplas
            excel.InsereDados_Planilha(2, 1, mDt);
            excel.SalvarExcel();

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo XLSX.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo XLS com todas as suas colunas e dados.
    /// </summary>
    /// <param name="Dt">DataTable contendo os dados. Caso for uma DataSource de um Grid, fazer assim: UltraDataGridView.DataSource as DataTable</param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaEmXls()
    {
        mNomesColunas = ObtemNomesColunas();

        if (mDt.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.xls", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            //Instancio o objeto para criar uma nova planilha apenas.
            Excel excel = new Excel();
            excel.CriaExcel(file, false, true);

            //Encerro os processos, pois precisei reinstanciar o objeto para inserir os dados no excel.
            //excel.MataProcessosExcel();
            Excel.MataProcessosExcel_EmExecucao();
            excel.FecharExcel(true);

            //Reinstancio o objeto para inserir os dados.
            excel = new Excel(file, 1, true);

            //Insere o nome das colunas na planilha de dados do Excel.
            for (int x = 1; x < mNomesColunas.Count; x++)
            {
                excel.InsereDados_Planilha(1, x, mNomesColunas[x - 1].ToString(), Excel.e_TipoFormatacaoValor.texto);
            }

            //Insere os valores das tuplas
            excel.InsereDados_Planilha(2, 1, mDt);
            excel.SalvarExcel();

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo XLS.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo XML com todas as suas colunas e dados.
    /// </summary>
    /// <param name="Dt">DataTable contendo os dados. Caso for uma DataSource de um Grid, fazer assim: UltraDataGridView.DataSource as DataTable</param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    /// <remarks>Não adiciona colunas do tipo 'Image' (Fullname="System.Drawing.Image")</remarks>
    private void ExportaEmXml()
    {
        mNomesColunas = ObtemNomesColunas();

        if (mDt.Rows.Count > 0)
        {
            RemoveColunasTipoImagem();

            DataSet ds = new DataSet();
            ds.Tables.Add(mDt);

            String content = ds.GetXml();

            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.xml", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            File.WriteAllText(file, content);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'",file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo XML.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo de TEXTO com todas as suas colunas e dados.
    /// </summary>
    /// <param name="Dt">DataTable contendo os dados. Caso for uma DataSource de um Grid, fazer assim: UltraDataGridView.DataSource as DataTable</param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaEmTxt()
    {
        mNomesColunas = ObtemNomesColunas();

        if (mDt.Rows.Count > 0)
        {
            String content = "";

            //Exporta as colunas da grade de dados
            for (int x = 0; x < mNomesColunas.Count; x++)
            {
                content += String.Format("{0}-{1}\t\t", x.ToString(), mNomesColunas[x].ToString());
            }

            //Adicinha uma nova linha
            content += "\n";

            //Exporta as tuplas do datatable advindo de um datasource.
            for (int x = 0; x < mDt.Rows.Count; x++)
            {
                //Exporta a linha
                for (int y = 0; y < mDt.Rows[x].ItemArray.Count(); y++)
                {
                    content += String.Format("{0}\t\t", mDt.Rows[x].ItemArray[y].ToString());
                }
                content += "\n";
            }

            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.txt", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            File.WriteAllText(file, content);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo de TEXTO.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataTable em um arquivo CSV com todas as suas colunas e dados.
    /// </summary>
    /// <param name="Dt">DataTable contendo os dados. Caso for uma DataSource de um Grid, fazer assim: UltraDataGridView.DataSource as DataTable</param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaEmCsv()
    {
        mNomesColunas = ObtemNomesColunas();

        if (mDt.Rows.Count > 0)
        {
            String content = "";

            //Exporta as colunas da grade de dados
            for (int x = 0; x < mNomesColunas.Count; x++)
            {
                content += String.Format("{0}-{1};", x.ToString(), mNomesColunas[x].ToString());
            }

            //Adicinha uma nova linha
            content += "\n";

            //Exporta as tuplas do datatable advindo de um datasource.
            for (int x = 0; x < mDt.Rows.Count; x++)
            {
                //Exporta a linha
                for (int y = 0; y < mDt.Rows[x].ItemArray.Count(); y++)
                {
                    content += String.Format("{0};", mDt.Rows[x].ItemArray[y].ToString());
                }
                content += "\n";
            }

            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.csv", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            File.WriteAllText(file, content);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em um arquivo de CSV.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataGridView em um arquivo PNG com todas as suas colunas e dados, com base
    /// na dimensão do grid.
    /// </summary>
    /// <param name="Dtgv">DataGridView devidamente populado, pois será nele </param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaDataGridViewEmPng()
    {
        //mDt = mDtgv.DataSource as DataTable;
        //mNomeModulo = NomeModulo;
        mNomesColunas = ObtemNomesColunas();

        if (mDtgv.RowCount > 0)
        {
            //Calcula as dimensões com base nos dados do GridDeDados.
            int tmpHeigth = (mDtgv.RowCount * mDtgv.RowTemplate.Height) * 2;
            int tmpWidht = mDtgv.ColumnCount * mDtgv.Columns[0].Width;

            Bitmap bmp = new Bitmap(tmpWidht, tmpHeigth);
            mDtgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, tmpWidht, tmpHeigth));

            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.png", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));
            bmp.Save(file);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em uma imagem do tipo PNG.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma UltraWinGrid.UltraGrid em um arquivo PNG com todas as suas colunas e dados, com base
    /// na dimensão do grid.
    /// </summary>
    /// <param name="Udgv">UltraWinGrid.UltraGrid devidamente populado, pois será nele </param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaUltraGridEmPng()
    {
        //mDt = Udgv.DataSource as DataTable;
        //mNomeModulo = NomeModulo;
        mNomesColunas = ObtemNomesColunas();

        if (mUdgv.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.png", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            Bitmap bmp = new Bitmap(mUdgv.Width, mUdgv.Height);
            mUdgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, mUdgv.Width, mUdgv.Height));
            bmp.Save(file);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em uma imagem do tipo PNG.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataGridView em um arquivo JPG com todas as suas colunas e dados, com base
    /// na dimensão do grid.
    /// </summary>
    /// <param name="Dtgv">DataGridView devidamente populado, pois será nele </param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaDataGridViewEmJpg()
    {
        //mDt = Dtgv.DataSource as DataTable;
        //mNomeModulo = NomeModulo;
        mNomesColunas = ObtemNomesColunas();

        if (mDtgv.RowCount > 0)
        {
            //Calcula as dimensões com base nos dados do GridDeDados.
            int tmpHeigth = (mDtgv.RowCount * mDtgv.RowTemplate.Height) * 2;
            int tmpWidht = mDtgv.ColumnCount * mDtgv.Columns[0].Width;

            Bitmap bmp = new Bitmap(tmpWidht, tmpHeigth);
            mDtgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, tmpWidht, tmpHeigth));

            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.jpg", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));
            bmp.Save(file);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em uma imagem do tipo JPG.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma UltraWinGrid.UltraGrid em um arquivo JPG com todas as suas colunas e dados, com base
    /// na dimensão do grid.
    /// </summary>
    /// <param name="Udgv">UltraWinGrid.UltraGrid devidamente populado, pois será nele </param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaUltraGridEmJpg()
    {
        //mDt = Udgv.DataSource as DataTable;
        //mNomeModulo = NomeModulo;
        mNomesColunas = ObtemNomesColunas();

        if (mUdgv.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.jpg", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            Bitmap bmp = new Bitmap(mUdgv.Width, mUdgv.Height);
            mUdgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, mUdgv.Width, mUdgv.Height));
            bmp.Save(file);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em uma imagem do tipo JPG.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma DataGridView em um arquivo BMP com todas as suas colunas e dados, com base
    /// na dimensão do grid.
    /// </summary>
    /// <param name="Dtgv">DataGridView devidamente populado, pois será nele </param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaDataGridViewEmBmp() 
    {
        //mDt = Dtgv.DataSource as DataTable;
        //mNomeModulo = NomeModulo;
        mNomesColunas = ObtemNomesColunas();

        if (mDtgv.RowCount > 0)
        {
            //Calcula as dimensões com base nos dados do GridDeDados.
            int tmpHeigth = (mDtgv.RowCount * mDtgv.RowTemplate.Height) * 2;
            int tmpWidht = mDtgv.ColumnCount * mDtgv.Columns[0].Width;

            Bitmap bmp = new Bitmap(tmpWidht, tmpHeigth);
            mDtgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, tmpWidht, tmpHeigth));

            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.bmp", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));
            bmp.Save(file);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em uma imagem do tipo BMP.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    /// <summary>
    ///     Método que exporta uma UltraWinGrid.UltraGrid em um arquivo BMP com todas as suas colunas e dados, com base
    /// na dimensão do grid.
    /// </summary>
    /// <param name="Udgv">UltraWinGrid.UltraGrid devidamente populado, pois será nele </param>
    /// <param name="NomeModulo">Nome do módulo que está exportando o arquivo. Isso serve para o nome do arquivo de saída.</param>
    private void ExportaUltraGridEmBmp()
    {
        //mDt = Udgv.DataSource as DataTable;
        //mNomeModulo = NomeModulo;
        mNomesColunas = ObtemNomesColunas();

        if (mUdgv.Rows.Count > 0)
        {
            String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}_{1}.bmp", mNomeModulo, DateTime.Now.ToString("dd.MM.yy.hh.mm.ss.ff")));

            Bitmap bmp = new Bitmap(mUdgv.Width, mUdgv.Height);
            mUdgv.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, mUdgv.Width, mUdgv.Height));
            bmp.Save(file);

            MessageBox.Show(String.Format("Arquivo salvo em '{0}'", file), "Êxito ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else MessageBox.Show("Não há dados para exportar em uma imagem do tipo BMP.", "Não foi possível gerar o arquivo solicitado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void BloqueiaAcoesExportar()
    {
        tsm_Pdf.Enabled = false;
        tsm_Docx.Enabled = false;
        tsm_Doc.Enabled = false;
        tsm_Xlsx.Enabled = false;
        tsm_Xls.Enabled = false;
        tsm_Xml.Enabled = false;
        tsm_Txt.Enabled = false;
        tsm_Csv.Enabled = false;
        tsm_Png.Enabled = false;
        tsm_Jpg.Enabled = false;
        tsm_Bmp.Enabled = false;
    }

    private void DesbloqueiaAcoesExportar()
    {
        tsm_Pdf.Enabled = true;
        tsm_Docx.Enabled = true;
        tsm_Doc.Enabled = true;
        tsm_Xlsx.Enabled = true;
        tsm_Xls.Enabled = true;
        tsm_Xml.Enabled = true;
        tsm_Txt.Enabled = true;
        tsm_Csv.Enabled = true;
        tsm_Png.Enabled = true;
        tsm_Jpg.Enabled = true;
        tsm_Bmp.Enabled = true;
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    ///     Método que apresenta o 'context menu strip' no controle informado na instancia
    /// da classe.
    /// </summary>
    public void MostrarCms()
    {
        switch (mTipoGrade)
        {
            case e_SkaTipoGrade.DataGridView: 
                if (mDtgv.Rows.Count == 0)
                    BloqueiaAcoesExportar();
                else DesbloqueiaAcoesExportar(); 
                break;

            case e_SkaTipoGrade.UltraGrid:
                if (mUdgv.Rows.Count == 0)
                    BloqueiaAcoesExportar();
                else DesbloqueiaAcoesExportar();

                break;
        }

        mCms.Show(mControlAddCms, new Point(0, mControlAddCms.Height));
    }

    #endregion

    #region Eventos

    private void tsm_Pdf_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmPdf();
    }

    private void tsm_Docx_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmDocx();
    }

    private void tsm_Doc_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmDoc();
    }

    private void tsm_Xlsx_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmXlsx();
    }

    private void tsm_Xls_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmXls();
    }

    private void tsm_Xml_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmXml();
    }

    private void tsm_Txt_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmTxt();
    }

    private void tsm_Csv_Click(object sender, EventArgs e)
    {
        //  Condição adicionada para verificar qual grade de dados (DataGridView
        //ou UltraDataGridView) está com os dados preenchidos.
        if (mDtgv != null)
            mDt = mDtgv.DataSource as DataTable;
        else mDt = mUdgv.DataSource as DataTable;

        this.ExportaEmCsv();
    }

    private void tsm_Png_Click(object sender, EventArgs e)
    {
        if (mDtgv != null)
            this.ExportaDataGridViewEmPng();
        else this.ExportaUltraGridEmPng();
    }

    private void tsm_Jpg_Click(object sender, EventArgs e)
    {
        if (mDtgv != null)
            this.ExportaDataGridViewEmJpg();
        else this.ExportaUltraGridEmJpg();
    }

    private void tsm_Bmp_Click(object sender, EventArgs e)
    {
        if (mDtgv != null)
            this.ExportaDataGridViewEmBmp();
        else this.ExportaUltraGridEmBmp();
    }

    #endregion

}