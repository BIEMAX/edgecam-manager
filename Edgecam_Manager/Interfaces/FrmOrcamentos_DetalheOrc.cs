using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    internal partial class FrmOrcamentos_DetalheOrc : Form
    {
        #region Variáveis globais

        private Orcamento mOrc;
        private DataTable mItens;

        /// <summary>
        ///     DataTable contendo os dados que serão exibidos na tela.
        /// </summary>
        private DataTable mDadosCustosCalculados;

        /// <summary>
        ///     Contém o valor final do orçamento sem desconto.
        /// </summary>
        private double mValorFinal;

        /// <summary>
        ///     Contém o valor final do orçamento com desconto.
        /// </summary>
        private double mValorFinalComDesconto;

        /// <summary>
        ///     Contém o ID do orçamento.
        /// </summary>
        private String mIdOrcamento;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém o valor final do orçamento sem desconto.
        /// </summary>
        public double _ValorFinal
        {
            get { return mValorFinal; }
        }

        /// <summary>
        ///     Contém o valor final do orçamento com desconto.
        /// </summary>
        public double _ValorFinalComDesconto
        {
            get { return mValorFinalComDesconto; }
        }

        /// <summary>
        ///     Contém os dados calculados dos custos dos orçamentos.
        /// </summary>
        public DataTable _DadosCustosCalculados
        {
            get { return mDadosCustosCalculados; }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o formulário e consulta os dados de a partir de um orçamento existente.
        /// </summary>
        /// <param name="IdOrcamento">Id do orçamento a ser verificado.</param>
        public FrmOrcamentos_DetalheOrc(String IdOrcamento)
        {
            this.mIdOrcamento = IdOrcamento;
            this.InitializeComponent();
            this.CreateColumns();
            udgv.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_CUSTOS_DETALHADOS_ORCAMENTO, new Dictionary<string, object>() { { "@ID", mIdOrcamento } });

            if(udgv.DataSource == null && udgv.Rows.Count == 0)
            {
                MessageBox.Show("Não foram localizados custos detalhados referente à esse orçamento. Contate o administrador do sistema.", 
                                "Não há custos detalhados", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                GC.Collect();
            }
        }

        /// <summary>
        ///     Instância o formulário.
        /// </summary>
        /// <param name="OrcObject">Objeto contendo os dados do orçamento (somente custos)</param>
        /// <param name="Itens">DataTable contendo os itens selecionados para o orçamento.</param>
        public FrmOrcamentos_DetalheOrc(Orcamento OrcObject, DataTable Itens)
        {
            this.mOrc = OrcObject;
            this.mItens = Itens;
            this.InitializeComponent();
            this.LoadDefaultValues();
        }

        #endregion

        #region Métodos

        private void LoadDefaultValues()
        {
            this.CreateColumns();
            this.Text += $" - Orçamento '{mOrc.CodOrca}'";

            int qtde = 0;
            double valor = 0.0;

            foreach(DataRow r in mItens.Rows)
            {
                //armazeno temporariamente para calculo (quantidade * custo)
                int q = Convert.ToInt16(r["Quantidade"].ToString());
                double v = Convert.ToDouble(r["Custo unitário (R$)"].ToString()) * q;

                //Armazena os valores finais
                qtde += q;
                valor += v;                
            }
            String estadoOrigem = Objects.LstUnidOrg.Where(x => x.Unidade.ToString().ToUpper() == Objects.UsuarioAtual.UnidadeOrg.ToUpper()).Select(y => y.EstadoUF).FirstOrDefault();

            //ICMS aplicável apenas no brasil
            DataTable t = null;
            if (mOrc.Pais.ToUpper().Trim() == "BRASIL" || mOrc.Pais.ToUpper().Trim() == "BRAZIL")
            {
                Dictionary<String, object> d = new Dictionary<string, object>()
                {
                    { "@EO", estadoOrigem },
                    { "@ED", mOrc.UF }
                };
                t = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ICMS_ESTADUAIS, d);
            }

            mDadosCustosCalculados.Rows.Add("Itens em orçamentos", "Valor (R$)", $"R$ {valor}");
            mDadosCustosCalculados.Rows.Add("Frete", "Valor (R$)", mOrc.FreteIncluso ? $"R$ {mOrc.ValorFrete}" : "R$ 0.0");
            mDadosCustosCalculados.Rows.Add($"Moeda cotada: {mOrc.NomeMoeda}", "Valor (R$)", !String.IsNullOrEmpty(mOrc.NomeMoeda) ? $"R$ {mOrc.ValorMoeda}" : "R$0.0");

            mDadosCustosCalculados.Rows.Add($"ICMS de '{estadoOrigem}' para '{mOrc.UF}'", "Percentual", $"{t.Rows[0]["Valor"].ToString()}%");
            mDadosCustosCalculados.Rows.Add($"Tipo de cálculo: {mOrc.NomeTipoVenda}", "Percentual", mOrc.TipoVenda == 2 ? $"{mOrc.DadosMarkup.MarkupUp}%" : (mOrc.TipoVenda == 1 ? $"{mOrc.ValorSomarVenda}%" : "0%"));
            mDadosCustosCalculados.Rows.Add("Desconto", "Percentual", mOrc.PossuiDesconto ? $"{mOrc.ValorDesconto}%" : "0%");

            mDadosCustosCalculados.Rows.Add("\t\t\t\t-", "\t-", "-");
            mDadosCustosCalculados.Rows.Add("Valor total", "Valor (R$)", String.Format("{0:C}", this.CalcTotalValue()));
            mDadosCustosCalculados.Rows.Add("Valor total com disconto", "Valor (R$)", String.Format("{0:C}", this.CalcTotalValueWithDiscount()));

            udgv.DataSource = mDadosCustosCalculados;
        }

        private void CreateColumns()
        {
            mDadosCustosCalculados = new DataTable();
            mDadosCustosCalculados.Columns.Add(new DataColumn("Descrição do item", typeof(String)));
            mDadosCustosCalculados.Columns.Add(new DataColumn("Tipo", typeof(String)));
            mDadosCustosCalculados.Columns.Add(new DataColumn("Valor", typeof(String)));
        }
        
        /// <summary>
        ///     Método que soma todos os valores para calcular o valor total (sem desconto)>
        /// </summary>
        /// <returns></returns>
        private Double CalcTotalValue()
        {
            double v = 0.0;

            //Primeiro calcula os valores e multiplicadores.
            foreach(DataRow r in mDadosCustosCalculados.Rows)
            {
                if (r["Descrição do item"].ToString().Contains("-")) break;
                else
                {
                    if (r["Tipo"].ToString() == "Percentual") continue;
                    else v += Convert.ToDouble(r["Valor"].ToString().Replace("R$", "").Trim());
                }
            }

            //Depois calcula os percentuais.
            foreach (DataRow r in mDadosCustosCalculados.Rows)
            {
                if (r["Descrição do item"].ToString().Contains("-")) break;
                else
                {
                    if (r["Tipo"].ToString() == "Valor (R$)") continue;
                    else
                    {
                        if (r["Descrição do item"].ToString() == "Desconto") continue;
                        else v += (v * Convert.ToDouble(r["Valor"].ToString().Replace("%", ""))) / 100;
                    }
                }
            }

            this.mValorFinal = v;

            return v;
        }

        /// <summary>
        ///     Método que soma todos os valores para calcular o valor total (com desconto)>
        /// </summary>
        /// <returns></returns>
        private Double CalcTotalValueWithDiscount()
        {
            double v = mValorFinal;

            foreach(DataRow r in mDadosCustosCalculados.Rows)
            {
                if (r["Tipo"].ToString() == "Valor (R$)" || r["Descrição do item"].ToString() != "Desconto") continue;
                else
                {
                    v -= (v * Convert.ToDouble(r["Valor"].ToString().Replace("%", ""))) / 100;
                    break;
                }
            }

            this.mValorFinalComDesconto = v;

            return v;
        }
        
        #endregion

        #region Eventos

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion
    }
}
