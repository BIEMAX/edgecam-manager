/***********************************************************************************************************
 * 
 * 
 *                  CustomTimes - Classe com métodos específicos para soma, subtração de tempos.
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe com métodos específicos para soma, subtração de tempos.
 *      Version:    2.7
 *      Date:       13/03/2019, at 02:36 PM
 *      Note:       <None>
 *      History:    Update - 13/03/2019 - 02:36 PM - Primeira versão da classe - V1.0 Lançada
 *                  Update - 13/03/2019 - 04:51 PM - Adicionado o método 'SomaTempo' - V1.1 Lançada.
 *                  Update - 13/03/2019 - 04:59 PM - Adicionado o método 'Semana_ISO8601' - V1.2 Lançada.
 *                  Update - 05/04/2019 - 01:23 PM - Adicionado o método 'GetLastDayMonth' - V1.3 Lançada
 *                  Update - 03/05/2019 - 04:15 PM - Adicionado o método 'GetDayName' - V1.4 Lançada.
 *                  Update - 01/07/2019 - 02:03 PM - Adicionado o método 'GetFirstDay_CurrentMonth' - V1.5 Lançada.
 *                  Update - 01/07/2019 - 02:03 PM - Adicionado o método 'GetLastDay_CurrentMonth' - V1.6 Lançada.
 *                  Update - 17/07/2019 - 10:25 AM - Adicionado o método 'GetDayName' - V1.7 Lançado.
 *                  Update - 17/07/2019 - 10:38 AM - Adicionado o método 'GetFirstDay_CurrentWeek' - V1.8 Lançada.
 *                  Update - 17/07/2019 - 10:38 AM - Adicionado o método 'GetLastDay_CurrentWeek' - V1.9 Lançada.
 *                  Update - 17/07/2019 - 01:52 PM - Adicionado um novo construtor no método 'GetFirstDay_CurrentWeek' - V2.0 Lançada.
 *                  Update - 17/07/2019 - 02:05 PM - Adicionado um novo construtor no método 'GetLastDay_CurrentWeek' - V2.1 Lançada.
 *                  Update - 18/07/2019 - 05:35 PM - Corrigido um problema no método 'GetFirstDay_CurrentWeek' - V2.2 Lançada.
 *                  Update - 29/07/2019 - 11:51 AM - Alterado o nome do método 'SomaTempo' para 'SumTimes' - V2.3 Lançada
 *                  Update - 29/07/2019 - 02:20 PM - Adicionado o método 'SecondsToMinutes' - V2.4 Lançada.
 *                  Update - 29/07/2019 - 02:20 PM - Adicionado o método 'SecondsToHours' - V2.5 Lançada.
 *                  Update - 06/08/2019 - 10:04 AM - Adicionado o método 'CompareDates' - V2.6 Lançada.
 *                  Update - 22/04/2020 - 12:30 AM - Adicionado o método 'MultiplyTimes' - V2.7 Lançada.
 * 
 * 
 **********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     Classe que possuí funções voltadas para a atuação com datas e tempos.
/// </summary>
public class CustomTimes
{
    #region Métodos estáticos

    /// <summary>
    ///     Obtém o datetime do primeiro dia do mês atual.
    /// </summary>
    /// <returns>Objeto contendo a data</returns>
    public static DateTime GetFirstDay_CurrentMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }

    /// <summary>
    ///     Obtém o datetime do último dia do mês atual.
    /// </summary>
    /// <returns>Objeto contendo a data</returns>
    public static DateTime GetLastDay_CurrentMonth()
    {
        return new DateTime(DateTime.Now.Year, DateTime.Now.Month, GetLastDayMonth());
    }

    /// <summary>
    ///     Método que obtém o primeiro dia (DateTime) da semana, considerando
    /// o dia atual.
    /// </summary>
    /// <param name="Day">Dia da semana para ser definido como início 
    /// (domingo ou segunda, por exemplo)</param>
    /// <returns>DateTime contendo o dia da semana.</returns>
    public static DateTime GetFirstDay_CurrentWeek(DayOfWeek Day = DayOfWeek.Monday)
    {
        DateTime dt = DateTime.Now;

        //Tive que adicionar o '-1', pois a segunda é sempre zero, domingo é 7
        return dt.AddDays((int)Day - (int)dt.DayOfWeek);
    }

    /// <summary>
    ///     Método que obtém o primeiro dia (DateTime) da semana, considerando
    /// uma data informada por parâmetro.
    /// </summary>
    /// <param name="Date">Data a ser considerada para o cálculo.</param>
    /// <param name="Day">Dia da semana para ser definido como início 
    /// (domingo ou segunda, por exemplo)</param>
    /// <returns>DateTime contendo o dia da semana.</returns>
    public static DateTime GetFirstDay_CurrentWeek(DateTime Date, DayOfWeek Day = DayOfWeek.Monday)
    {
        //Tive que adicionar o '-1', pois a segunda é sempre zero, domingo é 7
        return Date.AddDays((-(int)Day) - 1);
    }

    /// <summary>
    ///     Método que obtém o último dia (DateTime) da semana, considerando
    /// o dia atual.
    /// </summary>
    /// <param name="Day">Dia da semana para ser definido como término 
    /// (sexta ou domingo, por exemplo)</param>
    /// <returns>DateTime contendo o dia da semana.</returns>
    public static DateTime GetLastDay_CurrentWeek(DayOfWeek Day = DayOfWeek.Friday)
    {
        DateTime dt = DateTime.Now;

        //Resumo do cálculo: pego o dia da sexta (da semana atual) - os dias dessa semana.
        return dt.AddDays((int)Day - (int)dt.DayOfWeek);
    }

    /// <summary>
    ///     Método que obtém o último dia (DateTime) da semana, considerando
    /// uma data informada por parâmetro.
    /// </summary>
    /// <param name="Date">Data a ser considerada para o cálculo.</param>
    /// <param name="Day">Dia da semana para ser definido como término 
    /// (sexta ou domingo, por exemplo)</param>
    /// <returns>DateTime contendo o dia da semana.</returns>
    public static DateTime GetLastDay_CurrentWeek(DateTime Date, DayOfWeek Day = DayOfWeek.Friday)
    {
        //Resumo do cálculo: pego o dia da sexta (da semana atual) - os dias dessa semana.
        return Date.AddDays((int)Day - (int)Date.DayOfWeek);
    }

    /// <summary>
    ///     Método que obtem o último dia do mês. Se não passar nada como parâmetro, obtém
    /// as informações do DateTime.Now.
    /// </summary>
    /// <param name="Year">Ano desejado</param>
    /// <param name="Month">Mês (numérico)</param>
    /// <returns>Inteiro contendo o dia.</returns>
    public static int GetLastDayMonth(int Year = 0, int Month = 0)
    {
        int y = 0, m = 0;

        y = Year == 0 ? DateTime.Now.Year : Year;
        m = Month == 0 ? DateTime.Now.Month : Month;

        return new DateTime(y, m, 1).AddMonths(1).AddTicks(-1).Day;
    }

    /// <summary>
    ///     Método que obtém o nome do dia em portguês com base em uma data.
    /// </summary>
    /// <param name="Date">Data</param>
    /// <returns>String contendo o nome do dia.</returns>
    public static String GetDayName(DateTime Date)
    {
        return Date.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
    }

    /// <summary>
    ///     Método responsável por devolver o dia da semana em texto.
    /// </summary>
    /// <param name="Day">Número do dia</param>
    /// <returns>String contendo o nome do dia.</returns>
    public static String GetDayName(int Day)
    {
        switch (Day)
        {
            case 0: return "Domingo";
            case 1: return "Segunda-feira";
            case 2: return "Terça-feira";
            case 3: return "Quarta-feira";
            case 4: return "Quinta-feira";
            case 5: return "Sexta-feira";
            case 6: return "Sábado";
            default: return "";
        }
    }

    /// <summary>
    ///     Calcula o número da semana, onde cai uma determinada data, de acordo com a data fornecida.
    /// </summary>
    /// <param name="DiaAtual">Dia atual à ser calculado o número da semana</param>
    /// <remarks>
    ///     Segundo a norma ISO 8601, a semana 1 de um ano é aquela onde cai a primeira quinta-feira, sendo que
    /// a semana começa na segunda-feira!
    /// Assim, para calcular a semana de uma data, é necessário:
    /// - Achar a quinta-feira que cai na mesma semana dessa data (pode ser antes ou depois da data)
    /// - Calcular o número de quintas-feiras ocorridas desde o dia 01/Janeiro do mesmo ano dessa quinta-feira
    /// Note que essa quinta-feira pode cair no ano anterior à data, ou no próximo ano. 
    /// Ex: 01/01/2011 (sábado) pertence à semana 52 de 2010, pois a quinta-feira mais próxima é 30/12/2010
    /// Ex2: 29/12/2008 (segunda) pertence à semana 01 de 2009, pois a quinta da semana dele é 01/01/2009
    /// </remarks>
    /// <returns>Número do tipo inteiro contendo a semana.</returns>
    public static int Semana_ISO8601(DateTime DiaAtual)
    {
        // dia da semana -> segunda = 1, terça = 2 até domingo = 7
        int dayOfWeek = (int)DiaAtual.DayOfWeek != 0 ? (int)DiaAtual.DayOfWeek : 7;

        // qual é a quinta-feira que caiu nessa semana?
        DateTime NearestThu = DiaAtual.AddDays(4 - dayOfWeek);

        // qual é o ano dessa quinta-feira
        int year = NearestThu.Year;

        // dia 01/01 do ano dessa quinta-feira
        DateTime Jan1 = new DateTime(year, 1, 1);

        // diferença entra a quinta-feira e o dia 1/1 do seu ano
        TimeSpan ts = NearestThu.Subtract(Jan1);

        // contagem de quintas-feiras
        return Convert.ToInt16((System.Math.Floor(Convert.ToDouble(ts.Days / 7))) + 1);
    }

    /// <summary>
    ///     Método responsável por somar uma lista de tempos e devolver o resultado da soma
    /// de todos eles em forma de texto (hh:mm:ss)
    /// </summary>
    /// <param name="Tempos">Array de tempos, respeitando o seguinte formato: hh:mm:ss</param>
    /// <returns>Tempo total, resultante da soma de todos os parâmetros informados.</returns>
    public static String SumTimes(params String[] Tempos)
    {
        try
        {
            if (Tempos.Count() == 0)
                return "00:00:00";
            else if (Tempos.Count() == 1)
                return Tempos[0];
            else
            {
                String tmpTempoTotal = "00:00:00";

                for (int x = 0; x < Tempos.Count(); x++)
                {
                    String tmp = Tempos[x];

                    if (!String.IsNullOrEmpty(tmp))
                    {
                        int h1 = 0, m1 = 0, s1 = 0, h2 = 0, m2 = 0, s2 = 0;

                        //Aqui contém o tempo final (tmpTempoTotal)
                        h1 = Convert.ToInt16(tmpTempoTotal.Split(new char[] { ':' })[0]);
                        m1 = Convert.ToInt16(tmpTempoTotal.Split(new char[] { ':' })[1]);
                        s1 = Convert.ToInt16(tmpTempoTotal.Split(new char[] { ':' })[2]);

                        //Contém o tempo na lista de 'Tempos' (parâmetro)
                        h2 = Convert.ToInt16(tmp.Split(new char[] { ':' })[0]);
                        m2 = Convert.ToInt16(tmp.Split(new char[] { ':' })[1]);
                        s2 = Convert.ToInt16(tmp.Split(new char[] { ':' })[2]);

                        //Variáveis temporárias
                        int tmpHoras = 0, tmpMinutos = 0, tmpSegundos = 0;

                        //Soma os segundos
                        tmpSegundos = s1 + s2;

                        //Soma os minutos
                        if (tmpSegundos > 60)
                        {
                            tmpMinutos = (m1 + m2) + 1;
                            tmpSegundos = (tmpSegundos - 60);
                        }
                        else tmpMinutos = (m1 + m2);

                        //Soma as horas
                        if (tmpMinutos > 60)
                        {
                            tmpHoras = (h1 + h2) + 1;
                            tmpMinutos = (tmpMinutos - 60);
                        }
                        else tmpHoras = (h1 + h2);

                        tmpTempoTotal = tmpHoras.ToString().Length == 1 ? "0" + tmpHoras.ToString() + ":" : tmpHoras.ToString() + ":";
                        tmpTempoTotal += tmpMinutos.ToString().Length == 1 ? "0" + tmpMinutos.ToString() + ":" : tmpMinutos.ToString() + ":";
                        tmpTempoTotal += tmpSegundos.ToString().Length == 1 ? "0" + tmpSegundos.ToString() : tmpSegundos.ToString();
                    }
                }

                return tmpTempoTotal;
            }
        }
        catch { return "00:00:00"; }
    }

    /// <summary>
    ///     Método responsável por somar uma lista de tempos e devolver o resultado da soma
    /// de todos eles em forma de texto (hh:mm:ss)
    /// </summary>
    /// <param name="Tempo">Tempo, respeitando o seguinte formato: hh:mm:ss</param>
    /// <returns>Tempo total, resultante da soma de todos os parâmetros informados.</returns>
    public static String MultiplyTimes(String Tempo, int Qtde)
    {
        try
        {
            if (String.IsNullOrEmpty(Tempo))
                return "00:00:00";
            else
            {
                TimeSpan t = TimeSpan.Parse(Tempo);

                return TimeSpan.FromSeconds(t.TotalSeconds * Qtde).ToString();
            }
        }
        catch { return Tempo; }
    }

    /// <summary>
    ///     Método responsável por converter um valor em segundos (float/double) em minutos (já formatado).
    /// </summary>
    /// <param name="Seconds">Valor em segundos</param>
    /// <returns>String contendo o seguinte formado: 'hh:mm:ss'</returns>
    public static String SecondsToMinutes(Double Seconds)
    {
        //Temporário
        Double t = Seconds / 3600;

        return TimeSpan.FromHours(t).ToString(@"hh\:mm\:ss");
    }

    /// <summary>
    ///     Método responsável por converter um valor em segundos (float/double) em horas (já formatado).
    /// </summary>
    /// <param name="Seconds">Valor em segundos</param>
    /// <returns>String contendo o seguinte formado: 'hh:mm:ss'</returns>
    public static String SecondsToHours(Double Seconds)
    {
        //Temporário
        Double t = Seconds / 3600;

        return TimeSpan.FromHours(t).ToString(@"hh\:mm\:ss");
    }

    /// <summary>
    ///     Método que compara duas datas e verifica se a primeira data é maior que a segunda.
    /// </summary>
    /// <param name="Date1">Data a ser verificada se é maior que a segunda</param>
    /// <param name="Date2">Data a ser considerada como parâmetro de maioridade</param>
    /// <returns>True caso a primeira data seja maior</returns>
    public static Boolean CompareDates(DateTime Date1, DateTime Date2)
    {
        try
        {
            int value = DateTime.Compare(Date1.Date, Date2.Date);

            if (value > 0) return true;
            else return false;
        }
        catch { return false; }
    }

    #endregion
}