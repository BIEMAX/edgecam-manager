/***********************************************************************************************************
 * 
 * 
 *                  Word - Classe com métodos específicos para exportação de dados em um documento
 *                                                  de dados do Word.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe para adicionar dados em um documento do word.
 *      Version:    1.1
 *      Date:       31/08/2018, at 07:50 PM
 *      Note:       <None>
 *      History:    Update      - 31/08/2018 - 07:50 PM - Primeira versão da classe - V1.0 Lançada.
 *                  Update      - 29/02/2019 - 09:47 AM - Corrigido um problema no método 'FecharWord' - V1.1 Lançada.
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

using WordApp = Microsoft.Office.Interop.Word.Application;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class Word
{

    #region Variáveis globais

    protected WordApp mWord;
    protected _Document mDoc;
    protected Table mTable;
    protected String mCaminhoArqWord;
    protected Boolean mMostrarWord;

    #endregion

    #region Propriedades

    public String _CaminhoArqWord
    {
        get
        {
            return mCaminhoArqWord;
        }
        set
        {
            mCaminhoArqWord = value;
        }
    }

    public Boolean _MostrarWord
    {
        get
        {
            return mMostrarWord;
        }
        set
        {
            mMostrarWord = value;
        }
    }

    #endregion

    #region Instancia dos objetos

    /// <summary>
    ///     Instância a classe para criar um novo documento do word.
    /// </summary>
    /// <remarks>Essa instância serve para acessar o método 'CriaWord'</remarks>
    public Word() 
    {
        _MostrarWord = true;

        mWord = new WordApp();
        mWord.Visible = _MostrarWord;
    }

    /// <summary>
    ///     Instância a classe e abre um documento do word na primeira página.
    /// </summary>
    /// <param name="ArqWord">Caminho completo do documento do word.</param>
    public Word(String ArqWord) 
    {
        try
        {
            _CaminhoArqWord = ArqWord.ToUpper();
            _MostrarWord = true;

            mWord = new WordApp();
            mWord.Visible = _MostrarWord;

            AbrirWord();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao tentar abrir o documento do Word", ex);
        }
    }

    /// <summary>
    ///     Instância a classe, abre um documento do word na primeira página
    /// e determina se a mesma deve ou não ser apresentada.
    /// </summary>
    /// <param name="ArqWord">Caminho completo do documento do word</param>
    /// <param name="MostrarWord">Valor booleano, onde, 'true' mostra o documento do word.</param>
    public Word(String ArqWord, Boolean MostrarWord) 
    {
        try
        {
            _CaminhoArqWord = ArqWord.ToUpper();
            _MostrarWord = true;

            mWord = new WordApp();
            mWord.Visible = _MostrarWord;

            AbrirWord();
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Erro ao tentar abrir o documento '{0}'.", ArqWord.Substring(ArqWord.LastIndexOf("\\") + 1), ex));
        }
    }

    ~Word() { }

    #endregion

    #region Métodos

    /// <summary>
    ///     Método responsável por abrir o documento do word e exibi-lo na tela
    /// para o usuário corrente.
    /// </summary>
    public void AbrirWord()
    {
        if (mWord == null)
        {
            throw new Exception("Não há instalações válidas do Microsoft Office Word");
        }
        else if (!System.IO.File.Exists(mCaminhoArqWord))
        {
            throw new System.IO.FileNotFoundException(String.Format("Não foi possível encontrar o arquivo '{0}'.", mCaminhoArqWord));
        }
        else
        {
            mDoc = (Document)mWord.Documents.Open(mCaminhoArqWord, Type.Missing, (object)false, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value, (object)Missing.Value);
            //Abre a visualização da planilha designada (define a mesma como ativa).
            mWord.Activate();
        }
    }

    /// <summary>
    ///     Método responsável por criar um novo documento do Word (DOCX ou DOC).
    /// </summary>
    /// <param name="CaminhoArq">String contendo o caminho + nome + extensão do documento</param>
    /// <param name="FecharWordAoCriar">Valor booleando, onde, 'true' encerra os objetos instanciados</param>
    /// <param name="MostrarWord"><Valor booleano, onde, 'true' mostra o documento/param>
    public void CriaWord(String CaminhoArq, Boolean FecharWordAoCriar, Boolean MostrarWord)
    {
        if (mWord == null)
            throw new NotImplementedException("Word não está propriamente instalado");

        if (String.IsNullOrEmpty(CaminhoArq))
            throw new NotImplementedException("Caminho do arquivo não existe");

        if (System.IO.File.Exists(CaminhoArq))
            throw new System.IO.FileLoadException(String.Format("O arquivo informado '{0}' já existe e não pode ser substituído", CaminhoArq));

        if (!CaminhoArq.ToUpper().StartsWith("\\") && !CaminhoArq.ToUpper().StartsWith("C") && !CaminhoArq.ToUpper().EndsWith(".DOCX") && !CaminhoArq.ToUpper().EndsWith(".DOC"))
            throw new ArgumentOutOfRangeException("Caminho informado não está no formato correto (não é unidade de rede nem unidade local ou não possuí a extensão 'DOCX' ou 'DOC'");

        object misValue = System.Reflection.Missing.Value;

        //Precisei adicionar essa condição pois estava sempre mostrando o Word (I don't know why!!)
        mWord.Visible = MostrarWord;

        mDoc = mWord.Documents.Add(ref misValue, ref misValue, ref misValue, ref misValue);

        mDoc.SaveAs2(CaminhoArq);

        if (FecharWordAoCriar)
        {
            mDoc.Close(true, misValue, misValue);
            //mWord.Quit(ref misValue, ref misValue, ref misValue);

            Marshal.ReleaseComObject(mWord);
            Marshal.ReleaseComObject(mDoc);
        }

        _CaminhoArqWord = CaminhoArq.ToUpper();
        _MostrarWord = MostrarWord;

        if (_MostrarWord)
            AbrirWord();
    }

    /// <summary>
    ///     Método responsável por inserir uma 'DataTable' em um documento do Word.
    /// </summary>
    /// <param name="DtDados">DataTable contendo os valores à serem inseridos</param>
    /// <param name="ApresentarDoc">'True' para apresentar o documento do Word ao término.</param>    
    public void InsereDados_Documento(System.Data.DataTable DtDados, Boolean ApresentarDoc)
    {
        object missVal = System.Reflection.Missing.Value;

        if (DtDados != null && DtDados.Rows.Count > 0)
        {
            //mDoc = mWord.Documents.Add(_CaminhoArqWord, ref missVal, ref missVal, ref missVal);
            
            mTable = mDoc.Tables.Add(mWord.ActiveDocument.Range(), DtDados.Rows.Count, DtDados.Columns.Count, ref missVal, ref missVal);
            mTable.Range.ParagraphFormat.SpaceAfter = 7;

            for (int i = 0; i < DtDados.Rows.Count; i++)
            {
                for (int j = 0; j < DtDados.Columns.Count; j++)
                {
                    mTable.Cell(i, j).Range.Text = DtDados.Rows[i][j].ToString();
                }
            }

            //Gerava exceção com essas linhas
            //mTable.Rows[1].Range.Font.Bold = 1;
            //mTable.Rows[1].Range.Font.Italic = 1;
            //mTable.Borders.Shadow = true;

            if (ApresentarDoc)
                ApresentaWord();

            //Gerava exceção com essa linha
            // SaveAs2(mCaminhoArqWord);

            mDoc.Save();
        }
    }

    /// <summary>
    ///     Método responsável que mata todos os processos do word que ficaram presos no windows.
    /// </summary>
    public void MataProcessosWord()
    {
        var processes = from p in Process.GetProcessesByName("WORD") select p;

        foreach (var process in processes)
        {
            process.Kill();
        }
    }

    /// <summary>
    ///     Método que salva o documento do word.
    /// </summary>
    public void SalvarWord()
    {
        try
        {
            mWord.Documents.Save();
        }
        catch { }
    }

    /// <summary>
    ///     Método para salvar o documento do word em um caminho ou com um nome diferente.
    /// </summary>
    /// <param name="CaminhoArq">Novo caminho + nome + extensão do arquivo</param>
    private void SalvarWordComo(String CaminhoArq)
    {
        try
        {
            //TODO: PRECISO VER COMO SALVAR COMO O DOCUMENTO EM UM NOVO LOCAL.
            //mWord.Documents.Save(CaminhoArq);
        }
        catch { }
    }

    /// <summary>
    ///     Método responsável por maximizar a planilha de dados do excel.
    /// </summary>
    public void MaximizeWord()
    {
        try
        {
            mWord.WindowState = WdWindowState.wdWindowStateMaximize;
        }
        catch { }
    }

    /// <summary>
    ///     Método responsável por esconder o word.
    /// </summary>
    public void EscondeWord()
    {
        try
        {
            mWord.Visible = false;
        }
        catch { }
    }

    /// <summary>
    ///     Método responsável por apresentar o word.
    /// </summary>
    public void ApresentaWord()
    {
        try
        {
            mWord.Visible = true;
        }
        catch { }
    }

    /// <summary>
    ///     Método que fecha a planilha de dados do Excel e, salva ou não
    /// a planilha de dados.
    /// </summary>
    /// <param name="MostrarExcel"><Valor booleano, onde, 'true' salva a planilha de dados./param>
    public void FecharWord(Boolean SalvarArq)
    {
        try
        {
            mDoc.Close(true, "", "");
        }
        catch { }
        try
        {
            mWord.Documents.Close();
            //mWord.Quit();
            Marshal.ReleaseComObject(mWord);
        }
        catch { }
    }

    #endregion
}