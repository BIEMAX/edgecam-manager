using ImagedComboBox;
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
    public partial class FrmInventarios_New_Tool : Form
    {
        #region Variáveis globais/da classe

        private String mToolId;
        private String mAmbiente;
        private String mSubTipo;
        private String mNomeTool;
        private String mUnidade;

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto que permite o usuário cadastrar uma ferramenta
        /// que ainda não existe no edgecam.
        /// </summary>
        public FrmInventarios_New_Tool()
        {
            InitializeComponent();
            InicializaValoresDefault(false);
        }

        /// <summary>
        ///     Instância o objeto e carrega uma ferramente à ser adicionada na interface.
        /// </summary>
        /// <param name="ToolId">Id da ferramenta</param>
        /// <param name="Ambiente">Ambiente (fresa, turn, hole, etc)</param>
        /// <param name="SubTipo">Sub tipo da ferramenta</param>
        /// <param name="NomeTool">Nome/descrição da ferramenta</param>
        /// <param name="Unidade">Unidade de medida da ferramenta.</param>
        public FrmInventarios_New_Tool(String ToolId, String Ambiente, String SubTipo, String NomeTool, String Unidade)
        {
            InitializeComponent();

            mToolId = ToolId;
            mAmbiente = Ambiente;
            mSubTipo = SubTipo;
            mNomeTool = NomeTool;
            mUnidade = Unidade;

            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Inicializa os valores default da interface.
        /// </summary>
        /// <param name="BloquearControles">True cao esteja carregando uma ferramenta do EC.</param>
        private void InicializaValoresDefault(Boolean BloquearControles = true)
        {
            //Objects.DefineColorThemeInterface(this);

            this.Text = String.Format("Adicionar ferramenta '{0}' no inventário", mNomeTool);

            txtIdTool.Text = mToolId;
            txtNomeFerramenta.Text = mNomeTool;
            
            cbAmbiente.Items.Add(mAmbiente);
            cbAmbiente.SelectedIndex = 0;

            icbxSubTipo.Items.Add(DefineFerramenta());
            icbxSubTipo.SelectedIndex = 0;

            cbUnidadeMedida.Items.Add(mUnidade);
            cbUnidadeMedida.SelectedIndex = 0;

            //desativa os controles.
            cbTipoGestao.Enabled = false;
            txtTempo.Enabled = false;

            dtValidade.Enabled = false;
            //Adicionei esse valor para sempre puxar a data de entrega do dia atual.
            dtValidade.DateTime = DateTime.Now;

            //Seta valores padrão
            cbTipoGestao.SelectedIndex = 0;
            txtTempo.Text = "0";
            txtCusto.Text = "0";
            txtTempoEntrega.Text = "0";
            txtEstoqueMinimo.Text = "0";
            txtQuantidade.Text = "0";

            cbFornecedores.Items.Add("<Selecione>");
            cbFornecedores.Items.AddRange(ConsultaFornecedores().ToArray());
            cbFornecedores.SelectedIndex = 0;

            cbUnidadeEmpresa.Items.Add("<Selecione>");
            cbUnidadeEmpresa.Items.AddRange(Objects.LstUnidOrg.Select(x => x.Unidade).ToArray());
            cbUnidadeEmpresa.SelectedIndex = 0;

            cbArmazem.Items.Add("<Selecione>");
            cbArmazem.Items.AddRange(ConsultaArmazens().ToArray());
            cbArmazem.SelectedIndex = 0;

            if (BloquearControles) BloqueiaControles();
        }

        /// <summary>
        ///     Consulta os fornecedores.
        /// </summary>
        /// <returns>Lista de string</returns>
        private List<String> ConsultaFornecedores()
        {
            try
            {
                return Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_FORNECEDORES).AsEnumerable().Select(x => x.ItemArray[0].ToString()).ToList();
            }
            catch { return new List<String>() { "<Selecione>" }; }
        }

        /// <summary>
        ///     Consulta os armazéns das unidades.
        /// </summary>
        /// <returns>Lista de string</returns>
        private List<String> ConsultaArmazens()
        {
            try
            {
                return Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_ARMAZENS).AsEnumerable().Select(x => x.ItemArray[0].ToString()).ToList();
            }
            catch { return new List<String>() { "<Selecione>" }; }
        }

        /// <summary>
        ///     Método que valida se o usuário preencheu os campos obrigatórios.
        /// </summary>
        /// <returns>True significa que está tudo certo.</returns>
        private Boolean ValidaConfiguracoes()
        {
            int ret = 0;

            if (cbxGerenciar.Checked)
                ret += String.IsNullOrEmpty(txtTempo.Text) == true ? +1 : 0;

            ret += String.IsNullOrEmpty(txtCusto.Text) == true ? +1 : 0;
            ret += String.IsNullOrEmpty(txtEstoqueMinimo.Text) == true ? +1 : 0;
            ret += String.IsNullOrEmpty(txtQuantidade.Text) == true ? +1 : 0;

            return ret <= 0 ? true : false;
        }

        /// <summary>
        ///     Define o tipo de ferramenta.
        /// </summary>
        /// <returns>Retorna um número inteiro</returns>
        private int DefineTipoFerramenta()
        {
            switch (mAmbiente.ToUpper().Trim())
            {
                case "FRESAMENTO": return 0;
                case "TORNEAMENTO": return 1;
                case "FURAÇÃO": return 2;
                case "APALPADOR": return 3;
                case "ADITIVA": return 4;
                default: return 0;
            }
        }

        /// <summary>
        ///     Determina qual a situação do estoque atual.
        /// </summary>
        /// <returns>Número inteiro que representa a situação do estoque</returns>
        private int DefineTipoEstoque()
        {
            int qtde = Convert.ToInt16(txtQuantidade.Text);
            int qtdeMin = Convert.ToInt16(txtEstoqueMinimo.Text);

            //Se a quantidade em estoque e mínima forem zero, "estoque indisponível" (não tem)
            if (qtde == 0 && qtdeMin == 0)
                return 0;
            //Se a quantidade em estoque foi maior/igual que a quantidade mínima, "estoque disponível"
            else if (qtde >= qtdeMin)
                return 1;
            else if (qtde < qtdeMin)
                return 2;
            //Retorna tipo "recontagem"
            else return 3;
        }

        private void SalvarInventario()
        {
            if (ValidaConfiguracoes())
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@ID", mToolId);
                dic.Add("@DEC", mNomeTool);
                dic.Add("@TP", DefineTipoFerramenta());
                dic.Add("@SBTP", Convert.ToInt16(mSubTipo));
                dic.Add("@UN", mUnidade.ToUpper() == "POLEGADAS" ? 0 : 1);
                dic.Add("@TPGESTAO", cbxGerenciar.Checked);
                dic.Add("@TMP", cbxGerenciar.Checked ? cbTipoGestao.SelectedIndex : 0);
                dic.Add("@HASVAL", cbxTemValidade.Checked);
                dic.Add("@DTVAL", cbxTemValidade.Checked == true ? dtValidade.DateTime.Date.ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd"));
                dic.Add("@PROVIDER", cbFornecedores.Text);
                dic.Add("@CUSTO", Convert.ToDouble(txtCusto.Text));
                dic.Add("@TMP_RECEIVE", Convert.ToInt16(txtTempoEntrega.Text));
                dic.Add("@QTDE_EST", Convert.ToInt16(txtQuantidade.Text));
                dic.Add("@EST_MIN", Convert.ToInt16(txtEstoqueMinimo.Text));
                dic.Add("@FORSALE", cbxForSale.Checked);
                dic.Add("@FORPRODUCTION", cbxForProduction.Checked);
                dic.Add("@FORINTERN", cbxForIntern.Checked);
                dic.Add("@ESTADO", DefineTipoEstoque());
                dic.Add("@UNIORG", cbUnidadeEmpresa.Text);
                dic.Add("@ARM", String.IsNullOrEmpty(cbArmazem.Text) && cbArmazem.Text.ToUpper() != "<SELECIONE>" ? "" : cbArmazem.Text);
                dic.Add("@AUX", "");
                dic.Add("@USRCRT", Objects.UsuarioAtual.Login);
                dic.Add("@USRMOD", Objects.UsuarioAtual.Login);

                //Parâmetro para adicionar na tabela de histórico
                dic.Add("@INFO", String.Format("Cadastrado a ferramenta de id '{0}' no inventário", mToolId));

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_INVENTARIO_FERRAMENTAS, dic);
                MessageBox.Show("Cadastro de inventário concluída", "Sucesso ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnReturn_Click(new object(), new EventArgs());
            }
        }

        /// <summary>
        ///     Desativa a edição de controles em relação à ferramenta.
        /// </summary>
        private void BloqueiaControles()
        {
            txtIdTool.Enabled = false;
            txtNomeFerramenta.Enabled = false;
            cbAmbiente.Enabled = false;
            icbxSubTipo.Enabled = false;
            cbUnidadeMedida.Enabled = false;
        }

        /// <summary>
        ///     Ativa a edição de controles em relação à ferramenta.
        /// </summary>
        private void DesbloqueiaControles()
        {
            txtIdTool.Enabled = true;
            txtNomeFerramenta.Enabled = true;
            cbAmbiente.Enabled = true;
            icbxSubTipo.Enabled = true;
            cbUnidadeMedida.Enabled = true;
        }

        /// <summary>
        ///     Método que devolve um objeto do tipo ImageComboBoxItem
        /// de acordo com o tipo e sub tipo da ferramenta.
        /// </summary>
        /// <returns>Objeto contendo um texto e uma imagem</returns>
        private ComboBoxItem DefineFerramenta()
        {
            if (mAmbiente.ToUpper().Trim() == "FRESAMENTO")
            {
                switch(mSubTipo)
                {
                    case "0": return new ComboBoxItem("Fresa", Imagens_Edgecam.Mill_0);
                    case "1": return new ComboBoxItem("Fresa de cantos arredondados", Imagens_Edgecam.Mill_1);
                    case "2": return new ComboBoxItem("Esférica", Imagens_Edgecam.Mill_2);
                    case "3": return new ComboBoxItem("Fresa de topo", Imagens_Edgecam.Mill_3);
                    case "4": return new ComboBoxItem("Fresa em ângulo", Imagens_Edgecam.Mill_4);
                    case "5": return new ComboBoxItem("Faceamento", Imagens_Edgecam.Mill_5);
                    case "6": return new ComboBoxItem("Fresa de abrir canais", Imagens_Edgecam.Mill_6);
                    case "7": return new ComboBoxItem("Fresa pirulito", Imagens_Edgecam.Mill_7);
                    case "8": return new ComboBoxItem("Ferramenta de rosca", Imagens_Edgecam.Mill_8);
                    case "9": return new ComboBoxItem("Fresa Escalonada", Imagens_Edgecam.Mill_9);
                }
            }
            else if (mAmbiente.ToUpper().Trim() == "TORNEAMENTO") 
            {
                switch (mSubTipo)
                {
                    case "0": return new ComboBoxItem("Torneamento externo", Imagens_Edgecam.Turn_0);
                    case "1": return new ComboBoxItem("Torneamento interno", Imagens_Edgecam.Turn_1);
                    case "2": return new ComboBoxItem("Canal interno", Imagens_Edgecam.Turn_2);
                    case "3": return new ComboBoxItem("Canal externo", Imagens_Edgecam.Turn_3);
                    case "4": return new ComboBoxItem("Sangramento interno", Imagens_Edgecam.Turn_4);
                    case "5": return new ComboBoxItem("Sangramento externo", Imagens_Edgecam.Turn_5);
                    case "6": return new ComboBoxItem("Rosqueamento interno", Imagens_Edgecam.Turn_6);
                    case "7": return new ComboBoxItem("Rosqueamento externo", Imagens_Edgecam.Turn_7);
                }
            }
            else if (mAmbiente.ToUpper().Trim() == "FURAÇÃO") 
            {
                switch (mSubTipo)
                {
                    case "0": return new ComboBoxItem("Furação", Imagens_Edgecam.Hole_0);
                    case "1": return new ComboBoxItem("Alargador", Imagens_Edgecam.Hole_1);
                    case "2": return new ComboBoxItem("Macho para roscar", Imagens_Edgecam.Hole_2);
                    case "3": return new ComboBoxItem("Barra de mandrilhar", Imagens_Edgecam.Hole_3);
                    case "4": return new ComboBoxItem("Escariador", Imagens_Edgecam.Hole_4);
                    case "5": return new ComboBoxItem("Broca de centro", Imagens_Edgecam.Hole_5);
                    case "6": return new ComboBoxItem("Broca espada", Imagens_Edgecam.Hole_6);
                    case "7": return new ComboBoxItem("Mandrilhamento traseiro", Imagens_Edgecam.Hole_7);
                    case "8": return new ComboBoxItem("Broca escalonada", Imagens_Edgecam.Hole_8);
                }
            }
            else if (mAmbiente.ToUpper().Trim() == "APALPADOR") 
            {
                return new ComboBoxItem("Apalpador", Imagens_Edgecam.Probe_01);
            }
            else
            {
                switch (mSubTipo)
                {
                    case "0": return new ComboBoxItem("Deposição de pó", Imagens_Edgecam.Additive_0);
                    case "1": return new ComboBoxItem("Deposição de fio (wire)", Imagens_Edgecam.Additive_1);
                    case "2": return new ComboBoxItem("Metalização", Imagens_Edgecam.Additive_2);
                    case "3": return new ComboBoxItem("Extrusão", Imagens_Edgecam.Additive_3);
                }
            }

            //Só para não dar erro no método, mas será impossível chegar aqui
            return new ComboBoxItem("Apalpador", Imagens_Edgecam.Probe_01);
        }

        #endregion

        #region Eventos

        private void cbxGerenciar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxGerenciar.Checked)
            {
                label6.Enabled = true;
                cbTipoGestao.Enabled = true;
                label7.Enabled = true;
                txtTempo.Enabled = true;
            }
            else
            {
                label6.Enabled = false;
                cbTipoGestao.Enabled = false;
                label7.Enabled = false;
                txtTempo.Enabled = false;
            }
        }

        private void txtTempo_TextChanged(object sender, EventArgs e)
        {
            txtTempo.Text = CustomStrings.DeixaSomenteNumeros(txtTempo.Text);
        }

        private void cbxTemValidade_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTemValidade.Checked)
                dtValidade.Enabled = true;
            else dtValidade.Enabled = false;
        }

        private void txtCusto_TextChanged(object sender, EventArgs e)
        {
            txtCusto.Text = CustomStrings.DeixaSomenteDecimais(txtCusto.Text);
        }

        private void txtTempoEntrega_TextChanged(object sender, EventArgs e)
        {
            txtTempoEntrega.Text = CustomStrings.DeixaSomenteNumeros(txtTempoEntrega.Text);
        }

        private void txtEstoqueMinimo_TextChanged(object sender, EventArgs e)
        {
            txtEstoqueMinimo.Text = CustomStrings.DeixaSomenteNumeros(txtEstoqueMinimo.Text);
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            txtQuantidade.Text = CustomStrings.DeixaSomenteNumeros(txtQuantidade.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarInventario();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao adicionar novo item ao inventário", "FrmInventarios_new_tool", "btnSave_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion

    }
}