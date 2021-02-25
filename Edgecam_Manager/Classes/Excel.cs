/***********************************************************************************************************
 * 
 * 
 *                  Excel - Classe com métodos específicos para exportação de dados em uma Planilha
 *                                                  de dados do Excel.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe para adicionar dados em uma planilha de dados do Excel.
 *      Version:    3.8
 *      Date:       28/05/2017, at 08:07 PM
 *      Note:       <None>
 *      History:    Update      - 23/08/2017 - 2:27 PM - Concluído as atribuições dos valores nas
 *                                          propriedades e adicionado comentários nas propriedas - V1.1 Lançada.
 *                  Update      - 21/03/2018 - 4:50 PM - Adicionado novos métodos para excluir os valores 
 *                                          das células (entre intervalo também) - V1.2 Lançada.
 *                  Update      - 22/03/2018 - 5:26 PM - Adicionado um método para consultar a planilha de dados
 *                                          do excel (como um banco de dados) - V1.3 Lançada.
 *                  Update      - 25/07/2018 - 09:33 AM - Adicionado um novo parâmetro no método 'InsereDados_Planilha',
 *                                          pois o mesmo estava inserindo incorretamente os valores quando eram definidos
 *                                          como porcentagem - V1.4 Lançada.
 *                  Update      - 25/07/2018 - 10:11 AM - Adicionado uma nova instância do método 'InsereDados_Planilha',
 *                                          onde, possamos agora informar o nome da colun - V1.5 Lançada.
 *                  Update      - 13/08/2018 - 10:20 AM - Adicionado a propriedade '_WorkSheetAtiva' - V1.6 Lançada.
 *                  Update      - 14/08/2018 - 09:39 AM - Corrigido um problema no método 'AbrirExcel' - V1.7 Lançada.
 *                  Update      - 30/08/2018 - 01:06 PM - Adicionar uma nova instância do método 'Excel' - V1.8 Lançada.
 *                  Update      - 30/08/2018 - 01:06 PM - Adicionado o método 'CriaExcel' - V1.9 Lançada.
 *                  Update      - 30/08/2018 - 01:06 PM - Adicionado o método 'MataProcessosExcel' - V2.0 Lançada.
 *                  Update      - 30/08/2018 - 01:06 PM - Adicionado o método 'SalvarExcelComo' - V2.1 Lançada.
 *                  Update      - 30/08/2018 - 01:06 PM - Adicionado o método 'MaximizeExcel' - V2.2 Lançada.
 *                  Update      - 30/08/2018 - 01:06 PM - Adicionado o método 'EscondeExcel' e o método 'ApresentaExcel' - V2.3 Lançada.
 *                  Update      - 30/08/2018 - 02:26 PM - Adicionado uma nova instância do método 'InsereDados_Planilha' - V2.4 Lançada.
 *                  Update      - 31/08/2018 - 08:28 PM - Corrigido alguns problemas no método 'CriarExcel' - V2.5 Lançada.
 *                  Update      - 03/08/2018 - 03:58 PM - Adicionado um novo case (enumerador) no método 'InsereDados_Planilha' - V2.6 Lançada.
 *                  Update      - 07/11/2018 - 10:03 AM - Adicionado uma nova condição nas instâncias dos objetos - V2.7 Lançada.
 *                  Update      - 07/11/2018 - 11:13 AM - Adicionado o método 'CopiaRangeCelulas' - V2.8 Lançada.
 *                  Update      - 21/05/2019 - 05:48 PM - Adicionado o método 'InsereImagem_Planilha' - V2.9 Lançada.
 *                  Update      - 10/06/2019 - 09:07 AM - Corrigido um problema de formatação de data no método 'InsereDados_Planilha' - V3.0 Lançada
 *                  Update      - 30/07/2019 - 08:38 AM - Corrigido um problem no método 'FecharExcel' - V3.1 Lançada.
 *                  Update      - 30/07/2019 - 08:46 AM - Alterado a visibilidade do método 'MataProcessosExcel' para static - V3.2 Lançada.
 *                  Update      - 10/09/2019 - 05:47 PM - Adicionado o método 'ImprimiPlanilha' - V3.3 Lançada.
 *                  Update      - 10/09/2019 - 05:47 PM - Renomeado o método 'MataProcessosExcel' para 'MataProcessosExcel_EmExecucao' - V3.4 Lançada.
 *                  Update      - 10/09/2019 - 05:47 PM - Adicionado o método 'MataProcessosExcel_Pendurados' - V3.5 Lançada.
 *                  Update      - 11/09/2019 - 03:43 PM - Adicionado uma nova instância do método 'SalvarExcelComo' - V3.6 Lançada.
 *                  Update      - 11/09/2019 - 04:17 PM - Adicionado o método 'IsExcelOpened' - V3.7 Lançada.
 *                  Update      - 13/09/2019 - 10:04 PM - Adicionado o parâmetro 'ExportarNomeColunas' no método - V3.8 Lançada.
 * 
 * 
 * 
 * 
 **********************************************************************************************************/


using Microsoft.Office.Interop.Excel;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
//Adicionar a seguinte referência: Microsoft.Office.Interop.Excel 12.0.0.0 (12.0.4518.1014)
using ExcelApp = Microsoft.Office.Interop.Excel.Application;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Excel
{

    #region Variáveis globais

    /// <summary>
    ///     Contém o caminho da planilha de dados do Excel.
    /// </summary>
    protected string mCaminhoArqExcel;

    /// <summary>
    ///     Contém a String de conexão para planilha de dados do Excel XLSX (caso necessário utilizar).
    /// O caminho da planilha deve substituir a string '@PATH_EXCEL' (entre apóstrofos).
    /// </summary>
    protected string mStrCnn_xlsx = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=@PATH_EXCEL;Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";

    /// <summary>
    ///     Contém a String de conexão para planilha de dados do Excel XLS (caso necessário utilizar).
    /// O caminho da planilha deve substituir a string '@PATH_EXCEL' (entre apóstrofos).
    /// </summary>
    protected string mStrCnn_xls = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=@PATH_EXCEL;Extended Properties=\"Excel 8.0;HDR=Yes\";";

    /// <summary>
    ///     Define se deve ou não mostrar a planilha de dados do Excel.
    /// </summary>
    protected bool mMostrarExcel;

    /// <summary>
    ///     Número da dataSheet (página/aba) da planilha de dados.
    /// </summary>
    protected int mNumPagPlanilha;

    protected ExcelApp mExcelApp;
    protected _Workbook mWorkBook;
    protected _Worksheet mWorkSheet;

    /// <summary>
    ///     Contém o nome de todas as guias em ordem da planilha de dados do Excel instânciada.
    /// </summary>
    protected List<String> mLstWorkSheetsNames = new List<string>();

    /// <summary>
    ///     Contém o nome da WorkSheet (Plan) ativa na planilha de dados do Excel.
    /// </summary>
    protected String mWorkSheetAtiva;

    #endregion

    #region Enumeradores públicos

    /// <summary>
    ///     Enumerador que é utilizado para formatar o valor no momento que insere os dados
    /// na células da planilha.
    /// </summary>
    public enum e_TipoFormatacaoValor
    {
        /// <summary>
        ///     Formata o valor float/double para '{0:0,00}%'
        /// </summary>
        porcentagem = 0,
        /// <summary>
        ///     Somente insere os valores inteiros
        /// </summary>
        inteiro = 1,
        /// <summary>
        ///     Somente insere os valores de textos
        /// </summary>
        texto = 2,
        /// <summary>
        ///     Somente formata os valores das datas
        /// </summary>
        data = 3,
        /// <summary>
        ///     Valor real/double/float
        /// </summary>
        flutuante = 4
    }

    /// <summary>
    ///     Enumerador que é utilizado para salvar a planilha de dados do excel com
    /// um formato específico.
    /// </summary>
    public enum e_ExtensaoSalvar
    {
        /// <summary>
        ///     Formato PDF
        /// </summary>
        pdf = 0,
        /// <summary>
        ///     Documento XPS
        /// </summary>
        xps = 1
    }

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o caminho da planilha de dados do Excel.
    /// </summary>
    public String _CaminhoArqExcel
    {
        get
        {
            return mCaminhoArqExcel.ToUpper();
        }
        set
        {
            mCaminhoArqExcel = value;
        }
    }

    /// <summary>
    ///     Contém o número da planilha de dados (Plan1, Plan2, etc....).
    /// </summary>
    public int _NumPagPlanilha
    {
        get
        {
            return mNumPagPlanilha;
        }
        set
        {
            mNumPagPlanilha = value;
        }
    }

    /// <summary>
    ///     Variável que define se deve mostrar o Excel durante a adição
    /// dos dados ou não.
    /// </summary>
    public Boolean _MostrarExcel
    {
        get
        {
            return mMostrarExcel;
        }
        set
        {
            mMostrarExcel = value;
        }
    }

    /// <summary>
    ///     Propriedade somente leitura que contém o nome de todas as guias
    /// contidas na planilha instânciada.
    /// </summary>
    public List<String> _LstNomeGuiasPlanilha
    {
        get
        {
            return mLstWorkSheetsNames;
        }
    }

    /// <summary>
    ///     Contém o nome da WorkSheet (Plan) ativa na planilha de dados do Excel.
    /// </summary>
    public String _WorkSheetAtiva
    {
        get
        {
            return mWorkSheetAtiva;
        }
        set
        {
            mWorkSheetAtiva = value;
        }
    }

    #endregion

    #region Instancia dos objetos

    /// <summary>
    ///     Instância a classe para criar uma nova planilha de dados do Excel.
    /// </summary>
    /// <remarks>Essa instância serve para acessar o método 'CriaExcel'</remarks>
    public Excel()
    {
        mExcelApp = new Microsoft.Office.Interop.Excel.Application();
        mExcelApp.Visible = _MostrarExcel;
    }

    /// <summary>
    ///     Instância a classe e abre a planilha de dados na primeira
    /// página (Plan1 por exemplo).
    /// </summary>
    /// <param name="ArqExcel">Caminho completo da planilha de dados do Excel</param>
    public Excel(string ArqExcel)
    {
        try
        {
            _CaminhoArqExcel = ArqExcel.ToUpper();
            _NumPagPlanilha = 1;
            _MostrarExcel = true;

            mExcelApp = new Microsoft.Office.Interop.Excel.Application();
            mExcelApp.Visible = _MostrarExcel;

            AbrirExcel();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar abrir planilha de dados do Excel", ex);
        }

    }

    /// <summary>
    ///     Instância a classe, abre a planilha de dados de acordo
    /// com WorkSheet (Plan1, por exemplo) específicada.
    /// </summary>
    /// <param name="ArqExcel">Caminho completo da planilha de dados do Excel</param>
    /// <param name="WorkSheet">Número da 'worksheet'/'planilha' que deverá abrir.</param>
    public Excel(string ArqExcel, int WorkSheet)
    {
        if (WorkSheet <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da planilha deve ser orbigatoriamente maior que zero (iniciar em 1)");

        try
        {
            _CaminhoArqExcel = ArqExcel.ToUpper();
            _NumPagPlanilha = WorkSheet;
            _MostrarExcel = true;

            mExcelApp = new Microsoft.Office.Interop.Excel.Application();
            mExcelApp.Visible = _MostrarExcel;

            AbrirExcel();
        }
        catch { }
    }

    /// <summary>
    ///     Instância a classe, abre a planilha de dados de acordo
    /// com WorkSheet (Plan1, por exemplo) específicada e determina se a mesma
    /// deve ou não ser apresentada.
    /// </summary>
    /// <param name="ArqExcel">Caminho completo da planilha de dados do Excel</param>
    /// <param name="WorkSheet">Número da 'worksheet' que deverá abrir (deve informar um número maior que zero (0)).</param>
    /// <param name="MostrarExcel">Valor booleano, onde, 'true' mostra a planilha de dados.</param>
    public Excel(string ArqExcel, int WorkSheet, bool MostrarExcel)
    {
        if (WorkSheet <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da planilha deve ser orbigatoriamente maior que zero (iniciar em 1)");

        try
        {
            _CaminhoArqExcel = ArqExcel.ToUpper();
            _NumPagPlanilha = WorkSheet;
            _MostrarExcel = MostrarExcel;

            mExcelApp = new Microsoft.Office.Interop.Excel.Application();
            mExcelApp.Visible = _MostrarExcel;

            AbrirExcel();
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Erro ao tentar abrir a planilha '{0}'.", ArqExcel.Substring(ArqExcel.LastIndexOf("\\") + 1), ex));
        }
    }

    /// <summary>
    ///     Método destrutor da classe.
    /// </summary>
    ~Excel() { }

    #endregion

    #region Métodos públicos

    /// <summary>
    ///     Método privado que abre a planilha de dados do excel.
    /// </summary>
    /// <param name="ArqExcel">Caminho completo da planilha de dados do Excel.</param>
    public void AbrirExcel()
    {
        if (mExcelApp == null)
        {
            throw new Exception("Não há instalações válidas do Microsoft Office Excel");
        }
        else if (!System.IO.File.Exists(mCaminhoArqExcel))
        {
            throw new System.IO.FileNotFoundException(String.Format("Não foi possível encontrar o arquivo '{0}'.", mCaminhoArqExcel));
        }
        else
        {
            mWorkBook = (Workbook)mExcelApp.Workbooks.Open(mCaminhoArqExcel, Type.Missing, (object)false, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value);
            //Abre a visualização da planilha designada (define a mesma como ativa).
            mWorkSheet = (_Worksheet)mWorkBook.Worksheets.get_Item(mNumPagPlanilha);
            mWorkSheet.Activate();

            _WorkSheetAtiva = mWorkSheet.Application.ActiveSheet.Name;

            for (int i = 1; i <= mWorkBook.Worksheets.Count; i++)
            {
                _Worksheet temp = (_Worksheet)mWorkBook.Worksheets.get_Item(i);

                mLstWorkSheetsNames.Add(temp.Name);
            }
        }
    }

    /// <summary>
    ///     Método responsável por criar um novo arquivo de planilha do excel (XLSX ou XLS).
    /// </summary>
    /// <param name="CaminhoArq">String contendo o caminho + nome + extensão da planilha;</param>
    /// <param name="FecharExcelAoCriar">Valor booleando, onde, 'true' encerra os objetos instanciados</param>
    /// <param name="MostrarExcel"><Valor booleano, onde, 'true' mostra a planilha de dados./param>
    public void CriaExcel(String CaminhoArq, Boolean FecharExcelAoCriar, Boolean MostrarExcel)
    {
        if (mExcelApp == null)
            throw new NotImplementedException("Excel não está propriamente instalado");

        if (String.IsNullOrEmpty(CaminhoArq))
            throw new NotImplementedException("Caminho do arquivo não existe");

        if (System.IO.File.Exists(CaminhoArq))
            throw new System.IO.FileLoadException(String.Format("O arquivo informado '{0}' já existe e não pode ser substituído", CaminhoArq));

        if (!CaminhoArq.ToUpper().StartsWith("\\") && !CaminhoArq.ToUpper().StartsWith("C") && !CaminhoArq.ToUpper().EndsWith(".XLSX") && !CaminhoArq.ToUpper().EndsWith(".XLS"))
            throw new ArgumentOutOfRangeException("Caminho informado não está no formato correto (não é unidade de rede nem unidade local ou não possuí a extensão 'XLSX' ou 'XLS'");

        object misValue = System.Reflection.Missing.Value;

        mWorkBook = mExcelApp.Workbooks.Add(misValue);
        mWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)mWorkBook.Worksheets.get_Item(1);

        //mWorkSheet.Cells[1, 1] = "ID";
        //mWorkSheet.Cells[1, 2] = "Name";
        //mWorkSheet.Cells[2, 1] = "1";
        //mWorkSheet.Cells[2, 2] = "One";
        //mWorkSheet.Cells[3, 1] = "2";
        //mWorkSheet.Cells[3, 2] = "Two";

        //Here saving the file in xlsx
        mWorkBook.SaveAs(CaminhoArq, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
        misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

        if (FecharExcelAoCriar)
        {
            mWorkBook.Close(true, misValue, misValue);
            mExcelApp.Quit();

            Marshal.ReleaseComObject(mWorkSheet);
            Marshal.ReleaseComObject(mWorkBook);
            Marshal.ReleaseComObject(mExcelApp);
        }

        _CaminhoArqExcel = CaminhoArq.ToUpper();
        _NumPagPlanilha = 1;
        _MostrarExcel = MostrarExcel;

        if (_MostrarExcel)
            AbrirExcel();
    }

    /// <summary>
    ///     Método que transforma um número inteiro para o nome da coluna
    /// correspondente no Excel.
    /// </summary>
    /// <param name="ColumnNumber">Número da coluna. Ex.: 1, 558, 18</param>
    /// <returns>String contendo o nome da coluna.</returns>
    public String GetExcelColumnName(int ColumnNumber)
    {
        int dividend = ColumnNumber;
        String ColumnName = String.Empty;

        int modulo;

        while (dividend > 0)
        {
            modulo = (dividend - 1) % 26;
            ColumnName = Convert.ToChar(65 + modulo).ToString() + ColumnName;
            dividend = (int)((dividend - modulo) / 26);
        }

        return ColumnName;
    }

    /// <summary>
    ///     Método que transforma um texto contendo nome da coluna do excel (A1, C45),
    /// para um número inteiro.
    /// </summary>
    /// <param name="ColumnName">Texto contendo o nome da coluna.</param>
    /// <returns>Inteiro de 32 bits com o número correspondente à coluna.</returns>
    public Int32 GetExcelColumnNameNumber(string ColumnName)
    {
        //Adicionei essa linha abaixo somente para dar um upper case e remover possíveis espaços em branco.
        String nameColumn = ColumnName.ToUpper().Trim();

        int ColumnNumber = 0;
        int pow = 1;

        for (int i = nameColumn.Length - 1; i >= 0; i--)
        {
            ColumnNumber += (nameColumn[i] - 'A' + 1) * pow;
            pow *= 26;

        }
        return ColumnNumber;
    }

    /// <summary>
    ///     Método que insere dados na planilha de dados do Excel.
    /// </summary>
    /// <param name="Linha">Linha que terá valores inseridos (número inicial igual ou maior que 1)</param>
    /// <param name="Coluna">Número da coluna (número inicial igual ou maior que 1)</param>
    /// <param name="Valor">Valor à ser inserido</param>
    /// <param name="Formato">Tipo de formatação que deve ser realizado no momento de inserir os dados</param>
    /// <remarks>Precisa-se verificar a formatação de datas para as planilhas.</remarks>
    public void InsereDados_Planilha(int Linha, int Coluna, object Valor, e_TipoFormatacaoValor Formato)
    {
        if (Linha <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da linha deve ser orbigatoriamente maior que zero (iniciar em 1)");

        if (Coluna <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da Coluna deve ser orbigatoriamente maior que zero (iniciar em 1)");

        try
        {
            switch (Formato)
            {
                case e_TipoFormatacaoValor.porcentagem:
                    mWorkSheet.Cells[Linha, Coluna] = String.Format("{0:0,00}%", Convert.ToDouble(Valor.ToString()));
                    break;

                case e_TipoFormatacaoValor.inteiro:
                    mWorkSheet.Cells[Linha, Coluna] = Valor.ToString().ToUpper();
                    break;

                case e_TipoFormatacaoValor.texto:
                    mWorkSheet.Cells[Linha, Coluna] = Valor.ToString().ToUpper();
                    break;

                case e_TipoFormatacaoValor.data:
                    //mWorkSheet.Cells[Linha, Coluna] = Convert.ToDateTime(Valor.ToString().ToUpper()).ToOADate();
                    mWorkSheet.Cells[Linha, Coluna] = Valor.ToString().ToUpper();
                    break;

                case e_TipoFormatacaoValor.flutuante:
                    mWorkSheet.Cells[Linha, Coluna] = Convert.ToDouble(Valor.ToString());
                    break;

                default: break;
            }
        }
        catch { }
    }

    /// <summary>
    ///     Método que insere dados na planilha de dados do Excel.
    /// </summary>
    /// <param name="Linha">Linha que terá valores inseridos (número inicial igual ou maior que 1)</param>
    /// <param name="NomeColuna">Nome da coluna que será inserido valores (Ex.: A, B, AB...)</param>
    /// <param name="Valor">Valor à ser inserido</param>
    /// <param name="Formato">Tipo de formatação que deve ser realizado no momento de inserir os dados</param>
    /// <remarks>Precisa-se verificar a formatação de datas para as planilhas.</remarks>
    public void InsereDados_Planilha(int Linha, String NomeColuna, object Valor, e_TipoFormatacaoValor Formato)
    {
        if (Linha <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da linha deve ser orbigatoriamente maior que zero (iniciar em 1)");

        try
        {
            switch (Formato)
            {
                case e_TipoFormatacaoValor.porcentagem:
                    mWorkSheet.Cells[Linha, GetExcelColumnNameNumber(NomeColuna)] = String.Format("{0:0,00}%", Convert.ToDouble(Valor.ToString()));
                    break;

                case e_TipoFormatacaoValor.inteiro:
                    mWorkSheet.Cells[Linha, GetExcelColumnNameNumber(NomeColuna)] = Valor.ToString().ToUpper();
                    break;

                case e_TipoFormatacaoValor.texto:
                    mWorkSheet.Cells[Linha, GetExcelColumnNameNumber(NomeColuna)] = Valor.ToString().ToUpper();
                    break;

                case e_TipoFormatacaoValor.data:
                    mWorkSheet.Cells[Linha, GetExcelColumnNameNumber(NomeColuna)] = Valor.ToString().ToUpper();
                    break;

                case e_TipoFormatacaoValor.flutuante:
                    mWorkSheet.Cells[Linha, GetExcelColumnNameNumber(NomeColuna)] = Convert.ToDouble(Valor.ToString());
                    break;

                default: break;
            }

        }
        catch { }
    }

    /// <summary>
    ///     Método responsável por inserir uma 'DataTable' em uma planilha de dados do excel.
    /// </summary>
    /// <param name="LinhaInicial">Linha inicial que começara à receber os valores (número inicial igual ou maior que 1)</param>
    /// <param name="ColunaInicial">Coluna inicial que começara à receber os valores (número inicial igual ou maior que 1)</param>
    /// <param name="DtDados">DataTable contendo os valores à serem inseridos</param>
    /// <param name="ExportarNomeColunas">True para exportar o nome das colunas para o Excel, na linha e coluna indicados</param>
    public void InsereDados_Planilha(int LinhaInicial, int ColunaInicial, System.Data.DataTable DtDados, Boolean ExportarNomeColunas = false)
    {
        if (LinhaInicial <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da linha deve ser orbigatoriamente maior que zero (iniciar em 1)");

        if (ColunaInicial <= 0)
            throw new ArgumentOutOfRangeException("O valor inicial da Coluna deve ser orbigatoriamente maior que zero (iniciar em 1)");

        //  Armazeno temporariamente os valores nas variaveis, pois elas serão as mandatórias
        //por inserir os dados na planilha de dados do Excel.
        int tmpLinha = LinhaInicial, tmpColunm = ColunaInicial, tmpColunmNames = ColunaInicial;

        if (ExportarNomeColunas)
        {
            foreach (System.Data.DataColumn c in DtDados.Columns)
            {
                mWorkSheet.Cells[tmpLinha, tmpColunmNames] = c.ColumnName.ToString();
                tmpColunmNames++;
            }
        }

        if (DtDados != null && DtDados.Rows.Count > 0)
        {
            //Se for pra exportar as colunas, a linha tem que ser incrementada uma vez.
            if (ExportarNomeColunas)
            {
                tmpLinha++;
            }

            for (int i = 0; i < DtDados.Rows.Count; i++)
            {
                for (int j = 0; j < DtDados.Columns.Count; j++)
                {
                    //As variáveis 'i' e 'j' servem para obter os dados do DataTable
                    mWorkSheet.Cells[tmpLinha, tmpColunm] = DtDados.Rows[i][j].ToString();
                    tmpColunm++;
                }
                //Reinicio a contagem das colunas
                tmpColunm = ColunaInicial;
                tmpLinha++;
            }
        }
    }

    /// <summary>
    ///     Método que insere uma imagem em uma planilha de dados do excel.
    /// </summary>
    /// <param name="CaminhoImagem">Caminho físico da imagem</param>
    /// <param name="PosicaoInicial_X">Posição inicial em X na planilha (dada em pontos)</param>
    /// <param name="PosicaoInicial_Y">Posição inicial em Y na planilha (dada em pontos)</param>
    /// <param name="Largura">Largura da imagem (padrão é '-1' para manter o tamanho original da imagem)</param>
    /// <param name="Altura">Altura da imagem (padrão é '-1' para manter o tamanho original da imagem)</param>
    public void InsereImagem_Planilha(String CaminhoImagem, float PosicaoInicial_X = 50, float PosicaoInicial_Y = 50, float Largura = -1, float Altura = -1)
    {
        if (!System.IO.File.Exists(CaminhoImagem))
            throw new System.IO.FileNotFoundException(String.Format("Imagem '{0}' não foi encontrada/não existe.", CaminhoImagem));
        else
        {
            //mWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";

            //Adicionar como a 'COM Object' 'Microsoft Office <versão> Object Library' como referência no projeto.
            mWorkSheet.Shapes.AddPicture(CaminhoImagem, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, PosicaoInicial_X, PosicaoInicial_Y, Largura, Altura);

            mWorkBook.Save();

            this.ApresentaExcel();
        }
    }

    /// <summary>
    ///     Método que copia um range de células para um novo range, mantendo fórmulas, formatação, cores de fundo,
    /// valores, etc.
    ///     O método irá concatenar as variáveis: CelulaInicial, CelulaFinal e NumeroLinhaCopiar para montar uma
    /// String contendo o range de células: Ex.: CelulaInicial + NumeroLinhaCopiar : CelulaFinal + NumeroLinhaCopiar = AB22:AZ22
    /// </summary>
    /// <param name="CelulaInicial">Nome dá célula inicial (apenas a(s) letra(s)). Ex.: A, AB, AZ, EG..</param>
    /// <param name="CelulaFinal">Nome dá célula inicial (apenas a(s) letra(s)). Ex.: A, AB, AZ, EG..</param>
    /// <param name="NumeroLinhaCopiar">Número da linha que deve ser copiada juntamente com o range de células</param>
    /// <param name="NumeroLinhaColar">Número da linha que deverá ser o destino da cópia do range (preferencialmente
    /// uma linha vazia, pois será sobrescrito qualquer conteúdo).</param>
    /// <returns>True caso tenha êxito ao copiar, false para o contrário.</returns>
    public Boolean CopiaRangeCelulas(String CelulaInicial, String CelulaFinal, int NumeroLinhaCopiar, int NumeroLinhaColar)
    {
        #region Exemplo

        /*
            ExcelApp excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;

            String caminhoExcel = @"C:\Temp\03 - Orçamento.xlsx";

            _Workbook mWorkBook = (Workbook)excel.Workbooks.Open(caminhoExcel, Type.Missing, (object)false, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value);
            //Abre a visualização da planilha designada (define a mesma como ativa).
            _Worksheet mWorkSheet = (_Worksheet)mWorkBook.Worksheets.get_Item(1);
            mWorkSheet.Activate();

            //Aqui copia a linha
            mWorkSheet.Range["A21:BA21"].Copy(mWorkSheet.Range["A22:BA22"]);
            mWorkSheet.Range["A22:BA22"].Copy(mWorkSheet.Range["A23:BA23"]);
            mWorkSheet.Range["A23:BA23"].Copy(mWorkSheet.Range["A24:BA24"]);
            mWorkSheet.Range["A24:BA24"].Copy(mWorkSheet.Range["A25:BA25"]);

            //Aqui insere uma linha em braco
            mWorkSheet.Range["A24:BA24"].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            mWorkSheet.Range["A25:BA25"].Insert(Excel.XlInsertShiftDirection.xlShiftDown);
         */

        #endregion

        try
        {
            if (String.IsNullOrEmpty(CelulaInicial)) throw new ArgumentOutOfRangeException("Celula inicial não pode estar vazia");
            if (String.IsNullOrEmpty(CelulaFinal)) throw new ArgumentOutOfRangeException("Celula de final não pode estar vazia");
            if (NumeroLinhaCopiar <= 0) throw new ArgumentOutOfRangeException("Número da linha à ser copiada tem que ser maior que zero (0)");
            if (NumeroLinhaColar <= 0) throw new ArgumentOutOfRangeException("Número da linha à ser colada (destino) tem que ser maior que zero (0)");

            if (mWorkSheet != null)
            {
                String celulaOrigem = String.Format("{0}{2}:{1}{2}", CelulaInicial, CelulaFinal, NumeroLinhaCopiar);
                String celulaDestino = String.Format("{0}{2}:{1}{2}", CelulaInicial, CelulaFinal, NumeroLinhaColar);
                mWorkSheet.Range[celulaOrigem].Copy(mWorkSheet.Range[celulaDestino]);

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Método que deleta uma tupla (linha) da planilha (remove a linha inteira da planilha).
    /// </summary>
    /// <param name="Linha">Número da linha à ser deletada (número inicial igual ou maior que 1).</param>
    /// <exception cref="Exception">Ocorre quando não conseguir modificar o valor da célula
    /// para 'vazio'. Pode significar que a planilha já está em uso.</exception>
    public void DeletaTupla_Planilha(int Linha)
    {
        try
        {
            mWorkSheet.Rows[Linha].Delete((object)Missing.Value);
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Erro ao tentar deletar tupla (linha) '{0}'", Linha), ex);
        }
    }

    /// <summary>
    ///     Método que deleta o valor de uma determinada célula da planilha.
    /// </summary>
    /// <param name="Linha">Linha da planilha (número inicial igual ou maior que 1)</param>
    /// <param name="Coluna">Coluna da planilha (número inicial igual ou maior que 1)</param>
    /// <exception cref="Exception">Ocorre quando não conseguir modificar o valor da célula
    /// para 'vazio'. Pode significar que a planilha já está em uso.</exception>
    public void DeletaCelula_Planilha(int Linha, int Coluna)
    {
        try
        {
            mWorkSheet.Cells[Linha, Coluna] = "";
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Erro ao tentar deletar a célula '{0}'", GetExcelColumnName(Coluna) + Linha), ex);
        }
    }

    /// <summary>
    ///     Método que deleta os valores de células de uma linha, a partir de uma coluna inicial e uma final (intervalo/range).
    /// </summary>
    /// <param name="Linha">Linha da planilha (número inicial igual ou maior que 1)</param>
    /// <param name="ColunaInicial">Coluna inicial (número inicial igual ou maior que 1)</param>
    /// <param name="ColunaFinal">Coluna final (número inicial igual ou maior que 1)</param>
    public void DeletaRange_Celulas(int Linha, int ColunaInicial, int ColunaFinal)
    {
        try
        {
            if (ColunaFinal > ColunaInicial)
            {
                int cont = ColunaInicial;

                while (cont <= ColunaFinal)
                {
                    mWorkSheet.Cells[Linha, cont] = "";
                    cont++;
                }
            }
            else throw new InvalidOperationException("Número da coluna final deve ser maior que o da coluna inicial.");
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Erro ao tentar deletar as colunas da linha '{0}'", Linha), ex);
        }
    }

    /// <summary>
    ///     Método que executa umqa consulta na planilha de dados do Excel instânciada no objeto.
    /// </summary>
    /// <param name="Query">String contendo a consulta.</param>
    /// <returns>DataTable com todos os dados obtidos na consulta.</returns>
    /// <example>Exemplo de consulta: SELECT * FROM [NomeDaGuia$] | SELECT * FROM [Plan1$]</example>
    /// <remarks>Cuidar com as consultas realizadas.</remarks>
    public System.Data.DataTable ConsultaPlanilha(String Query)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        string cnn = "";
        OleDbConnection dbCnn = null;

        try
        {
            if (mCaminhoArqExcel.Trim().ToUpper().EndsWith(".XLSX"))
                cnn = mStrCnn_xlsx.Replace("@PATH_EXCEL", mCaminhoArqExcel);
            else if (mCaminhoArqExcel.Trim().EndsWith(".XLS"))
                cnn = mStrCnn_xls.Replace("@PATH_EXCEL", mCaminhoArqExcel);
            else
            {
                throw new System.IO.FileLoadException("");
            }

            dbCnn = new OleDbConnection(cnn);

            OleDbDataAdapter adapter = new OleDbDataAdapter();

            OleDbCommand cmd = new OleDbCommand(Query, dbCnn);

            adapter.SelectCommand = cmd;

            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper() == "O provedor 'Microsoft.ACE.OLEDB.12.0' não está registrado na máquina local.".ToUpper())
            {
                throw new Exception("Não foi possível estabelecer uma conexão com a planilha de dados do Excel. Tente instalar o componente '2007 Office System Driver: Data Connectivity Components' da Microsoft", ex);
            }
            else throw new Exception("Erro ao tentar consultar a planilha de dados do Excel. " + ex.Message, ex);
        }
        finally
        {
            if (dbCnn != null && dbCnn.State == System.Data.ConnectionState.Open)
            {
                dbCnn.Dispose();

                //Limpa as pools
                OleDbConnection.ReleaseObjectPool();
            }

            dbCnn.Dispose();
        }

        return dt;
    }

    /// <summary>
    ///     Método que salva a planilha de dados do Excel.
    /// </summary>
    public void SalvarExcel()
    {
        try
        {
            mWorkBook.Save();
        }
        catch { }
    }

    /// <summary>
    ///     Método para salvar a planilha de dados do excel em um caminho 
    /// ou com um nome diferente (salva na extensão XLSX).
    /// </summary>
    /// <param name="CaminhoArq">Novo caminho + nome + extensão do arquivo</param>
    public void SalvarExcelComo(String CaminhoArq)
    {
        try
        {
            mWorkBook.SaveAs(CaminhoArq);
        }
        catch { }
    }

    /// <summary>
    ///     Método para exportar a planilha de dados do excel em um formato
    /// específico, como PDF e XPS.
    /// </summary>
    /// <param name="CaminhoArq">Caminho do arquivo (com ou sem a extensão do arquivo).</param>
    /// <param name="Extensao">Extensão a ser exportado.</param>
    public void SalvarExcelComo(String CaminhoArq, e_ExtensaoSalvar Extensao)
    {
        //https://social.msdn.microsoft.com/Forums/en-US/cc380742-dd71-4818-9125-0e1c77717ada/saving-excel-workbook-as-pdf-exportasfixedformat-throws-8220value-does-not-fall-within-the?forum=exceldev
        try
        {
            if (System.IO.File.Exists(CaminhoArq))
                System.IO.File.Delete(CaminhoArq);
        }
        catch (Exception ex)
        {
            throw new System.IO.IOException(String.Format("Não foi possível deletar o arquivo '{0}'", CaminhoArq), ex);
        }

        try
        {
            switch (Extensao)
            {
                case e_ExtensaoSalvar.pdf: mWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, CaminhoArq.EndsWith(".pdf") ? CaminhoArq : CaminhoArq + ".pdf"); break;
                case e_ExtensaoSalvar.xps: mWorkBook.ExportAsFixedFormat(XlFixedFormatType.xlTypeXPS, CaminhoArq.EndsWith(".xps") ? CaminhoArq : CaminhoArq + ".xps"); break;
                default: break;
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao exportar a planilha de dados do Excel para o formato desejado.", ex);
        }
    }

    /// <summary>
    ///     Método responsável por maximizar a planilha de dados do excel.
    /// </summary>
    public void MaximizeExcel()
    {
        try
        {
            mExcelApp.WindowState = XlWindowState.xlMaximized;
        }
        catch { }
    }

    /// <summary>
    ///     Método responsável por esconder o excel.
    /// </summary>
    public void EscondeExcel()
    {
        try
        {
            mExcelApp.Visible = false;
        }
        catch { }
    }

    /// <summary>
    ///     Método responsável por apresentar o excel.
    /// </summary>
    public void ApresentaExcel()
    {
        try
        {
            mExcelApp.Visible = true;
        }
        catch { }
    }

    /// <summary>
    ///     Método que fecha a planilha de dados do Excel e, salva ou não
    /// a planilha de dados.
    /// </summary>
    /// <param name="MostrarExcel"<Valor booleano, onde, 'true' salva a planilha de dados.</param>
    public void FecharExcel(bool SalvarArq = true)
    {
        try
        {
            mWorkBook.Close(SalvarArq, "", "");
        }
        catch { }
        try
        {
            mExcelApp.Workbooks.Close();
            mExcelApp.Quit();
            Marshal.ReleaseComObject(mExcelApp);
        }
        catch { }
    }

    /// <summary>
    ///     Método que verifica se por algum motivo, o usuário fechou a planilha
    /// (ActiveSheet is null?) ativa/aberta nesse objeto da classe (aplicação at run time).
    /// </summary>
    /// <returns>True caso a planilha ainda esteja aberta, false para o contrário
    /// (nesse caso, não será possível visualizar a planilha novamente).</returns>
    public Boolean IsExcelOpened()
    {
        return mExcelApp.ActiveSheet != null ? true : false;
    }

    /// <summary>
    ///     Método responsável por imprimir o excel na impressora padrão.
    /// </summary>
    /// <param name="DefaultPrinter">True para utilizar a impressora padrão, false para utilizar uma em específico</param>
    /// <param name="DefaultPrinterName">Nome da impressora desejada.</param>
    public void ImprimiPlanilha(Boolean DefaultPrinter = true, String DefaultPrinterName = "")
    {
        //Contém o nome da impressora padrão.
        string defaultPrinter = "";

        if (DefaultPrinter)
        {
            System.Drawing.Printing.PrinterSettings printerName = new System.Drawing.Printing.PrinterSettings();
            defaultPrinter = printerName.PrinterName;
        }
        else
        {
            if (!String.IsNullOrEmpty(DefaultPrinterName))
                defaultPrinter = DefaultPrinterName;
            else throw new ArgumentNullException("Nome da impressora está nulo ou vazio");
        }

        //Contém uma lista das impressoras instaladas na máquina.
        var printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
        //Indice da impressora ativa.
        int printerIndex = 0;

        //Aqui será validado, qual impressora está ativa através do nome.
        foreach (String s in printers)
        {
            if (s.Equals(defaultPrinter))
                break;
            printerIndex++;
        }

        //Imprime.
        mWorkBook.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, printers[printerIndex], Type.Missing, Type.Missing, Type.Missing);

    }

    #endregion

    #region Métodos estáticos (públicos)

    /// <summary>
    ///     Método responsável que mata TODOS os processos do excel que
    /// estão em execução na sessão atual do windows (com o usuário corrente).
    /// </summary>
    public static void MataProcessosExcel_EmExecucao()
    {
        var processes = from p in Process.GetProcessesByName("EXCEL") select p;

        foreach (var process in processes)
        {
            process.Kill();
        }
    }

    /// <summary>
    ///     Método responsável que mata todos os processos do excel que ficaram
    /// pendurados no windows (todos os processos que estiverem com nenhum
    /// arquivo aberto no excel, será encerrado o processo).
    /// </summary>
    public static void MataProcessosExcel_Pendurados()
    {
        var processes = from p in Process.GetProcessesByName("EXCEL") select p;

        foreach (var process in processes)
        {
            //  Se o processo não tiver nada no nome/título da janela, mata ele, 
            //pois é somente processo que foi aberto e não foi matado pela aplicação.
            if (String.IsNullOrEmpty(process.MainWindowTitle))
            {
                process.Kill();
            }
        }
    }

    #endregion

}