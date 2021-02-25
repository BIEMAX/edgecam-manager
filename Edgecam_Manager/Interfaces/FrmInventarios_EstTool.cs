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
    internal partial class FrmInventarios_EstTool : Form
    {
        #region Variáveis globais/da classe

        /// <summary>
        ///     Objeto contendo os dados de estoque de uma ferramenta
        /// </summary>
        private FerramentaEstoque mTool;

        #endregion

        #region Propriedades

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância a interface.
        /// </summary>
        /// <param name="Ferramenta">Objeto contendo os dados de estoque de uma ferramenta.</param>
        public FrmInventarios_EstTool(FerramentaEstoque Ferramenta)
        {
            InitializeComponent();
            mTool = Ferramenta;
            InicializaValoresDefault();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que carrega as propriedades da ferramenta na interface,
        /// carrega o histórico e os movimentos do armazém.
        /// </summary>
        private void InicializaValoresDefault()
        {
            CarregaEstoqueToolInterface();
            ConsultaMovimentosEstoque();
            ConsultaHistoricoEstoque();
        }

        /// <summary>
        ///     Método que carrega os dados do estoque da ferramenta na interface.
        /// </summary>
        private void CarregaEstoqueToolInterface()
        {
            txtIdTool.Text          = mTool.Id;
            txtNomeFerramenta.Text  = mTool.NomeTool;
            txtAmbiente.Text        = DefineAmbienteFerramenta();
            txtUnidadeMedida.Text   = mTool.UnidadeMedida == "0" ? "Polegadas" : "Milímetros";
            txtCusto.Text           = String.Format("{0:C}", Convert.ToDouble(mTool.CustoUnitario));
            cbxTemValidade.Checked  = mTool.TemValidade == "0" ? false : true;
            dtValidade.DateTime     = mTool.TemValidade == "1" ? Convert.ToDateTime(mTool.DataValidade) : DateTime.Now;
            txtFornecedor.Text      = mTool.Fornecedor;
            txtTempoEntrega.Text    = String.Format("{0} dias", mTool.TempoRecebimento);

            icbxSubTipo.Items.Add(DefineFerramenta());
            icbxSubTipo.SelectedIndex = 0;

            //Sempre atualizar o estado primeiro e depois apresentar na interface.
            AtualizaStatusEstoque();

            switch (mTool.Estado)
            {
                case "0": 
                    ulblStatus.Text = "Estoque zerado - Quantidade em estoque: " + mTool.QuantidadeEstoque; 
                    ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.White; 
                    break;
                case "1": 
                    ulblStatus.Text = "Estoque disponível - Quantidade em estoque: " + mTool.QuantidadeEstoque; 
                    ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.Green; 
                    break;
                case "2":
                    ulblStatus.Text = "Estoque abaixo do estoque mínimo - Quantidade em estoque: " + mTool.QuantidadeEstoque;
                    ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.Red; 
                    break;
                case "3":
                    ulblStatus.Text = "Estoque em recontagem - Quantidade em estoque: " + mTool.QuantidadeEstoque; 
                    ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.Orange; 
                    break;
            }
        }

        /// <summary>
        ///     Método que atualiza o estado do estoque da ferramenta no banco de dados.
        /// </summary>
        private void AtualizaStatusEstoque()
        {
            int qtdeEstoque = Convert.ToInt16(mTool.QuantidadeEstoque);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@ID", mTool.Id);

            if (qtdeEstoque == 0)
            {
                dic.Add("@ESTADO", 0);
                ulblStatus.Text = "Estoque zerado - Quantidade em estoque: " + mTool.QuantidadeEstoque;
                ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.White;
                mTool.Estado = "0";
            }
            else if (qtdeEstoque >= Convert.ToInt16(mTool.EstoqueMinimo))
            {
                dic.Add("@ESTADO", 1);
                ulblStatus.Text = "Estoque disponível - Quantidade em estoque: " + mTool.QuantidadeEstoque;
                ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.Green;
                mTool.Estado = "1";
            }
            else if (qtdeEstoque < Convert.ToInt16(mTool.EstoqueMinimo))
            {
                dic.Add("@ESTADO", 2);
                ulblStatus.Text = "Estoque abaixo do estoque mínimo - Quantidade em estoque: " + mTool.QuantidadeEstoque;
                ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.Red;
                mTool.Estado = "2";
            }

            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_ESTADO_ESTOQUE_TOOL, dic);
        }

        /// <summary>
        ///     Método que carrega o tipo da ferramenta em forma de texto para o usuário.
        /// </summary>
        /// <returns>String contendo o ambiente da ferramenta.</returns>
        private String DefineAmbienteFerramenta()
        {
            switch (mTool.TipoTool)
            {
                case "0": return "Fresamento";
                case "1": return "Torneamento";
                case "2": return "Furação";
                case "3": return "Apalpador";
                case "4": return "Aditiva";
                default: return "";
            }
        }

        /// <summary>
        ///     Método que devolve um objeto do tipo ImageComboBoxItem
        /// de acordo com o tipo e sub tipo da ferramenta.
        /// </summary>
        /// <returns>Objeto contendo um texto e uma imagem</returns>
        private ComboBoxItem DefineFerramenta()
        {
            if (mTool.TipoTool.ToUpper().Trim() == "0")
            {
                switch (mTool.SubTipoTool)
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
            else if (mTool.TipoTool.ToUpper().Trim() == "1")
            {
                switch (mTool.SubTipoTool)
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
            else if (mTool.TipoTool.ToUpper().Trim() == "2")
            {
                switch (mTool.SubTipoTool)
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
            else if (mTool.TipoTool.ToUpper().Trim() == "3")
            {
                return new ComboBoxItem("Apalpador", Imagens_Edgecam.Probe_01);
            }
            else
            {
                switch (mTool.SubTipoTool)
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

        /// <summary>
        ///     Consulta os movimentos no estoque.
        /// </summary>
        private void ConsultaMovimentosEstoque()
        {
            udgv_Movimentos.DataSource = SQLQueries.Consulta_MovEstFerramentas(mTool.ToolId, "", true);
        }

        /// <summary>
        ///     Método que consulta o histórico de modificações desse inventário.
        /// </summary>
        private void ConsultaHistoricoEstoque()
        {
            udgv_Historico.DataSource = Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CONSULTA_HISTORICO_INVENTARIO_TOOL, new Dictionary<string, object>() { { "@IDTOOL", mTool.ToolId } });
        }

        #endregion

        #region Eventos

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Eventos do CMS

        /// <summary>
        ///     Método que chama um form para realizar movimentos no inventário.
        /// </summary>
        /// <param name="TipoMovEstoque">Tipo de movimento de estoque à ser realizado.</param>
        private void IniciaMovimento_Inventario(e_TipoMovEstoque TipoMovEstoque)
        {
            FrmInventarios_MovTool frm = null;

            switch (TipoMovEstoque)
            {
                case e_TipoMovEstoque.Entrada: frm = new FrmInventarios_MovTool(e_TipoMovEstoque.Entrada, Convert.ToInt16(mTool.ToolId)); break;
                case e_TipoMovEstoque.Saida: frm = new FrmInventarios_MovTool(e_TipoMovEstoque.Saida, Convert.ToInt16(mTool.ToolId)); break;
                case e_TipoMovEstoque.Transferencia: frm = new FrmInventarios_MovTool(e_TipoMovEstoque.Transferencia, Convert.ToInt16(mTool.ToolId)); break;
                case e_TipoMovEstoque.Outro: frm = new FrmInventarios_MovTool(e_TipoMovEstoque.Outro, Convert.ToInt16(mTool.ToolId)); break;
            }

            frm.ShowDialog();

            if (frm._SalvouMovimento)
            {
                ConsultaHistoricoEstoque();
                ConsultaMovimentosEstoque();

                //Aqui contém a nova quantidade em estoque.
                mTool.QuantidadeEstoque = frm._NewQtdeEstoque;

                //Atualizo o estoque e a interface consequentemente.
                AtualizaStatusEstoque();
            }
        }

        private void tsmEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                IniciaMovimento_Inventario(e_TipoMovEstoque.Entrada);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar movimento no estoque de ferramentas", "FrmInventarios_EstTool",
                                           "btnSave_Click", e_TipoMovEstoque.Entrada.ToString(), "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmSaida_Click(object sender, EventArgs e)
        {
            try
            {
                IniciaMovimento_Inventario(e_TipoMovEstoque.Saida);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar movimento no estoque de ferramentas", "FrmInventarios_EstTool",
                                           "btnSave_Click", e_TipoMovEstoque.Saida.ToString(), "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmReiniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Você está prestes a reiniciar o inventário dessa ferramenta. Deseja prosseguir?", "Reinicialização de inventário", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    Dictionary<String, Object> dic = new Dictionary<string, object>();
                    dic.Add("@ID", mTool.Id);
                    dic.Add("@TOOLID", mTool.ToolId);
                    dic.Add("@INFO", "Estoque reiniciado/zerado");
                    dic.Add("@USR", Objects.UsuarioAtual.Login);

                    dic.Add("@ACAO", 4);
                    dic.Add("@MOTIVO", "Estoque reiniciado pelo usuário.");
                    dic.Add("@QTDE", 0);
                    dic.Add("@FOR", DBNull.Value.ToString());
                    dic.Add("@UNI", Objects.UsuarioAtual.UnidadeOrg);
                    dic.Add("@ARM", DBNull.Value.ToString());
                    dic.Add("@LOTE", DBNull.Value.ToString());
                    dic.Add("@DES", DBNull.Value.ToString());

                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.REINICIA_ESTOQUE_TOOL, dic);
                    Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_MOV_EST_INVENTARIO_TOOL, dic);

                    //Atualiza na interface para o usuário.
                    ulblStatus.Text = "Estoque zerado"; 
                    ulblStatus.Appearance.Image = Edgecam_Manager.Properties.Resources.White;

                    //Atualiza as grades de dados.
                    ConsultaHistoricoEstoque();
                    ConsultaMovimentosEstoque();

                    //MessageBox.Show("Estoque reiniciado com êxito", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao reiniciar estoque", "FrmInventario_EstTool", "tsmReiniciar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmTransferência_Click(object sender, EventArgs e)
        {
            try
            {
                IniciaMovimento_Inventario(e_TipoMovEstoque.Transferencia);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar movimento no estoque de ferramentas", "FrmInventarios_EstTool",
                                           "btnSave_Click", e_TipoMovEstoque.Transferencia.ToString(), "", e_TipoErroEx.Erro, ex);
            }
        }

        private void tsmOutro_Click(object sender, EventArgs e)
        {
            try
            {
                IniciaMovimento_Inventario(e_TipoMovEstoque.Outro);
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar movimento no estoque de ferramentas", "FrmInventarios_EstTool",
                                           "btnSave_Click", e_TipoMovEstoque.Outro.ToString(), "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion

    }
}