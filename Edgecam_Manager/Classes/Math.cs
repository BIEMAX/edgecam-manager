/***********************************************************************************************************
 * 
 * 
 *                  Math - Classe com cálculos matemáticos
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe com cálculos matemáticos
 *      Version:    1.4
 *      Date:       22/04/2019, at 11:25 AM
 *      Note:       <None>
 *      History:    Update - 22/04/2019 - 11:25 AM - Criado a primeira versão - V1.0 Lançada.
 *                  Update - 22/04/2019 - 11:29 AM - Adicionado o método 'CalculaAreaPeca' - V1.1 Lançada.
 *                  Update - 22/04/2019 - 11:39 AM - Adicionado o método 'CalculaPesoPeca' - V1.2 Lançada.
 *                  Update - 29/04/2019 - 11:21 AM - Adicionado o método 'FormataValor_EmQuilos' - V1.3 Lançada.
 *                  Update - 29/04/2019 - 11:25 AM - Alterado o fato de cálculo do método 'CalculaPesoPeca' - V1.4 lançada
 * 
 * 
 * 
 * 
 **********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     Classe com operações matemáticas.
/// </summary>
public class Math
{

    #region Métodos estáticos

    /// <summary>
    ///     Método que obtém o valor em percentual de determinado número/valor.
    /// </summary>
    /// <param name="ValorTotal">Valor correspondente aos 100%</param>
    /// <param name="ValorObterPercentual">Valor a ser descoberto seu percentual</param>
    /// <param name="ArredondarValor">True para arredondar o valor final para apenas duas casas decimais após a vírgula.</param>
    /// <returns>Valor considerando o seu percentual.</returns>
    public static Double ObtemPercentualValor(Double ValorTotal, Double ValorObterPercentual, Boolean ArredondarValor = false)
    {
        if (!ArredondarValor)
            return (ValorObterPercentual * 100) / ValorTotal;
        else return System.Math.Round((ValorObterPercentual * 100) / ValorTotal, 2, MidpointRounding.ToEven);
    }

    /// <summary>
    ///     Método que cálcula a área de uma determinada peça.
    /// </summary>
    /// <param name="Comprimento">Comprimento da peça</param>
    /// <param name="Largura">Largura da peça</param>
    /// <returns>Valor da área</returns>
    /// <remarks>O cálculo de área será realizado com base nos valores, não irá considerar se os valores
    /// estão em metros ou milímetros</remarks>
    public static Double CalculaAreaPeca(Double Comprimento, Double Largura)
    {
        return Comprimento * Largura;
    }

    /// <summary>
    ///     Método responsável por determinar o peso teórico de uma peça.
    /// </summary>
    /// <param name="Comprimento">Comprimento da peça</param>
    /// <param name="Largura">Largura da peça</param>
    /// <param name="Espessura">Espessura da peça</param>
    /// <param name="DensidadeMat">Densidade do material</param>
    /// <returns>Valor do peso téorico da peça</returns>
    public static Double CalculaPesoPeca(Double Comprimento, Double Largura, Double Espessura, Double DensidadeMat)
    {
        return (Comprimento * Largura * Espessura * DensidadeMat) / 1000000;
    }

    /// <summary>
    ///     Método que converte um valor Double para valor em quilos (string).
    /// </summary>
    /// <param name="Valor"></param>
    /// <returns>String contendo o valor já formatado</returns>
    public static string FormataValor_EmQuilos(Double Valor)
    {
        if (Valor > 999999999 || Valor < -999999999) return Valor.ToString("0,,,.###Kg", CultureInfo.InvariantCulture);
        else if (Valor > 999999 || Valor < -999999) return Valor.ToString("0,,.##Kg", CultureInfo.InvariantCulture);
        else if (Valor > 999 || Valor < -999) return Valor.ToString("0,.#Kg", CultureInfo.InvariantCulture);
        else return Valor.ToString(CultureInfo.InvariantCulture);
    }

    #endregion
}