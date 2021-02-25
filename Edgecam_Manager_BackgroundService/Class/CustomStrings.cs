/***********************************************************************************************************
 * 
 * 
 *                              CustomStrings - Utilidades para Strings
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe com funções específicas para textos (strings)
 *      Version:    2.2
 *      Date:       27/06/2017, at 03:19 PM
 *      Note:       <None>
 *      History:	Update - 28/06/2017 - 08:06 AM - Implementado um novo método chamado 'RemoveSpecialCharacters' - V1.1 Lançada
 * 					Update - 02/08/2017 - 05:29 PM - Implementado um novo método chamado 'IsNull' - V1.2 Lançada
 *					Update - 27/10/2017 - 07:50 AM - Implementado um novo método chamado 'RemoveCharsCharacters' - V1.3 Lançada
 *					Update - 21/08/2018 - 08:05 AM - Modificado todos os métodos para público - V1.4 Lançada
 *					Update - 04/02/2019 - 03:15 PM - Modificado todos os métodos para static public - V1.5 Lançada
 *					Update - 04/02/2019 - 03:19 PM - Adicionado o método 'GetTextBetween' - V1.6 Lançada.
 *					Update - 13/02/2019 - 11:48 AM - Refatorado o método de 'RemoveCharsCharacters' para 'DeixaSomenteNumeros' - V1.7 Lançada.
 *					Update - 13/02/2019 - 12:53 AM - Adicionado o método 'DeixaSomenteDecimais' - V1.8 Lançada.
 *					Update - 02/04/2019 - 12:15 AM - Adicionado o método 'RandomNumber' - V1.9 Lançada.
 *					Update - 02/04/2019 - 12:31 AM - Adicionado o método 'RandomString' - V2.0 Lançada.
 *					Update - 02/04/2019 - 12:59 AM - Adicionado o método 'RandomPassword' - V2.1 Lançada.
 *					Update - 10/06/2019 - 04:40 PM - Corrigido um problema no método 'GetTextBetween' - V2.2 Lançada.
 * 
 * 
***********************************************************************************************************/

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class CustomStrings
{

    //public CustomStrings()
    //{

    //}

    //~CustomStrings() { }


    /// <summary>
    ///     Método booleano que compara duas strings.
    /// </summary>
    /// <param name="str1">Primeira string a ser comparada</param>
    /// <param name="str2">Segunda string a ser comparada</param>
    /// <returns>'True' caso as duas strings forem iguais, 'false' caso não forem.</returns>
    public static Boolean IsStringsIguais(String str1, String str2)
    {
        if (!String.IsNullOrEmpty(str1) && String.IsNullOrEmpty(str2))
        {
            char[] sep = { '\t', '\n', '\r' };
            if (str1.Split(sep, StringSplitOptions.RemoveEmptyEntries).ToString().ToUpper() == str2.Split(sep, StringSplitOptions.RemoveEmptyEntries).ToString().ToUpper())
            {
                return true;
            }
            return false;
        }
        else
        {
            throw new Exception("Valor passado como parâmetro na função de comparação de strings não pode ser vazio");
        }
    }

    /// <summary>
    ///     Implementa um case-insensitive ("sensível à caixa das letras" ou "sensível a maiúsculas e minúsculas")
    /// que substituí texto/caractere de uma string por um dado informado.
    /// </summary>
    /// <param name="StrOriginal">String original que terá seu texto substituído.</param>
    /// <param name="StrProcurar">Texto que deve ser procurado na string original.</param>
    /// <param name="StrSubstituto">Texto substituto pelo 'texto de procura'.</param>
    /// <returns>String original com sua chave de busca substituída.</returns>
    /// <remarks>Adicionar a referência 'Microsoft.VisualBasic'</remarks>
    public static String SubstituiTextoEmString(String StrOriginal, String StrProcurar, String StrSubstituto)
    {
        return Microsoft.VisualBasic.Strings.Replace(StrOriginal, StrProcurar, StrSubstituto, 1, -1, CompareMethod.Text);
    }

    /// <summary>
    ///     Método booleano que verifica se a peça possui a codificação correta para ser programada, ou seja,
    /// a peça deve possuir uma letra + 4 à 6 dígitos + uma letra opicional. Ex.: D1234A, D123456, D12345
    /// </summary>
    /// <param name="CodImutavel">Representa uma expressão regular imutável, ou seja, um padrão de nomenclatura. Ex.: Regex r = new Regex("[A-Z]{1}[0-9]{4,6}[A-Z]?");</param>
    /// <param name="CodValidar">Código que deve ser validado</param>
    /// <param name="TemExtensao">Caso o codigo é o nome de uma peça que possua extensão, deve ser setado como false.</param>
    /// <returns></returns>
    public static Boolean IsStringCodigoValido(System.Text.RegularExpressions.Regex CodImutavel, String CodValidar, bool TemExtensao)
    {
        String codigo = null;

        if (TemExtensao)
            codigo = System.IO.Path.GetFileNameWithoutExtension(CodValidar);
        else
        {
            if (!CodImutavel.Match(codigo).Success)
                return false;
        }
        return true;
    }

    /// <summary>
    ///     Recebe um nome e diz se ele é válido, ou seja, não contem caracteres especiais/prioibidos.
    /// </summary>
    /// <param name="StrTexto">Nome à ser validado</param>
    /// <returns>True caso não possuir nenhum caracter inválido</returns>
    public static Boolean PossuiCaracteresEspeciais(String StrTexto)
    {

        char[] charsInvalidos = System.IO.Path.GetInvalidFileNameChars();
        var q = charsInvalidos.Where(c => StrTexto.Contains(c));

        return q.Count() == 0;

    }

    /// <summary>
    ///     Método que remove os espaços em branco de uma string e a retorna de maneira limpa.
    /// </summary>
    /// <param name="StrTexto">String contendo o texto.</param>
    /// <returns>String purificada (sem espaços)</returns>
    public static String RemoveEspacosEmBranco(String StrTexto)
    {
        return String.Join("", StrTexto.Split(default(String[]), StringSplitOptions.RemoveEmptyEntries));
    }

    /// <summary>
    ///     Remove qualquer caractere especial em uma string e a devolve purificada.
    /// </summary>
    /// <param name="Str">String à ser removido caracteres especiais</param>
    /// <returns>String purificada</returns>
    public static String RemoveSpecialCharacters(String Str)
    {
        return Regex.Replace(Str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
    }

    /// <summary>
    ///     Remove caracteres e deixa somente números no texto.
    /// </summary>
    /// <param name="Str">String à ser removido os caracteres especiais</param>
    /// <returns>String purificada</returns>
    public static String DeixaSomenteNumeros(String Str)
    {
        return Regex.Replace(Str, "[^0-9 ]", "");
    }

    /// <summary>
    ///     Remove os caracteres e deixa somente números decimais (com vírgulas ou pontos).
    /// </summary>
    /// <param name="Str">String à ser removido os caracteres especiais</param>
    /// <returns>String purificada</returns>
    public static String DeixaSomenteDecimais(String Str)
    {
        //return Regex.Replace(Str, "[^0-9]+.?[^0-9,]*", "");
        return Regex.Replace(Str, "[^0-9.,]", "");

    }

    /// <summary>
    ///     Método que valida se uma string é null ou está vazia.
    /// </summary>
    /// <param name="Texto">Texto à ser verificado sua autenticidades</param>
    /// <param name="DefaultValue">Valor para retornar, caso à string esteja vazia.</param>
    /// <returns>String contendo o seu valor (caso não estiver nulo) ou o valor definido como padrão.-</returns>
    public static String IsNull(String Texto, String DefaultValue)
    {
        if (String.IsNullOrEmpty(Texto) || Texto == "" || Texto == null)
        {
            return DefaultValue.ToUpper();
        }
        else return Texto;
    }

    /// <summary>
    ///     Método que procura um texto em uma String de sua escolha (pode ser string relativamente
    /// grandes, o desempenho será só irá aumentar).
    /// </summary>
    /// <param name="Content">Conteúdo da String, onde será buscado os valores</param>
    /// <param name="StrStart">Parâmetro de inicio à ser procurado</param>
    /// <param name="StrEnd">Parâmetro de término (limite)</param>
    /// <returns>String contendo o texto entre os limites impostos.</returns>
    public static String GetTextBetween(String Content, String StrStart, String StrEnd)
    {
        int Start, End;
        if (Content.Contains(StrStart) && Content.Contains(StrEnd))
        {
            Start = Content.IndexOf(StrStart, 0) + StrStart.Length;
            End = Content.IndexOf(StrEnd, Start);
            //return Content.Substring(Start, End - Start);
            //Dá um replace no texto.
            return Content.Substring(Start, End - Start).Replace(StrStart, "").Replace(StrEnd, "").Replace("<div>", "");
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    ///     Método que obtém um número aleatório definido entre um intervalo.
    /// </summary>
    /// <param name="Min">Número inicial (mínimo)</param>
    /// <param name="Max">Número final (máximo)</param>
    /// <returns>String contendo o número aleatório.</returns>
    public static String RandomNumber(int Min = 0, int Max = 100)
    {
        Random r = new Random();
        return r.Next(Min, Max).ToString();
    }

    /// <summary>
    ///     Método que obtém um texto (string) aleatório definido em um tamanho.
    /// </summary>
    /// <param name="Size">Tamanho do texto (quantidade de caracteres)</param>
    /// <param name="LowerCase">True para devolver o texto em caixa baixa.</param>
    /// <returns>String contendo o texto aleatório.</returns>
    public static String RandomString(int Size, Boolean LowerCase)
    {
        StringBuilder b = new StringBuilder();
        Random r = new Random();
        char ch;

        for (int i = 0; i < Size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(System.Math.Floor(26 * r.NextDouble() + 65)));
            b.Append(ch);
        }

        if (LowerCase) return b.ToString().ToLower();
        return b.ToString();
    }

    /// <summary>
    ///     Método que gera uma senha/código aleatoriamente.
    /// </summary>
    /// <param name="QtdeCod">Quantidade de códigos que vai conter na devolução.</param>
    /// <param name="IncludeSep">True para incluir o caractere '-' no código.</param>
    /// <returns>String contendo o texto aleatório.</returns>
    public static String RandomPassword(int Size = 4, int QtdeCod = 1, Boolean IncludeSep = false)
    {
        char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[Size];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }

        //Declara o objeto.
        StringBuilder s = new StringBuilder(Size);

        if (QtdeCod <= 1)
        {
            foreach (byte b in data)
            {
                s.Append(chars[b % (chars.Length)]);
            }
        }
        else if (QtdeCod > 1)
        {
            for (int x = 0; x < QtdeCod; x++)
            {
                //Adicionei a condição abaixo para não ficar um '-' no final do texto.
                if (IncludeSep && x != 0 && x + 1 <= QtdeCod) s.Append("-");

                s.Append(RandomPassword(4, 1, false));
            }
        }

        return s.ToString();
    }

}