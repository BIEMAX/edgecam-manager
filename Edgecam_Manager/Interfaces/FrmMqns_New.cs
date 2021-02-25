using Edgecam_Manager.Idiomas;
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
    internal partial class FrmMqns_New : Form
    {

        #region Global Variables

        private Utilities mUtil;

        #endregion

        #region Propriedades
                
        #endregion

        #region Instância dos objetos da classe

        public FrmMqns_New()
        {
            InitializeComponent();

            InicializaValoresIniciais();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Inicializa os valores da interface.
        /// </summary>
        private void InicializaValoresIniciais()
        {
            //Inicializa as instâncias
            mUtil = new Utilities();

            //Inicializa valores nas combo boxes.
            cbTecnologia.Items.AddRange(new string[] { "<Selecione>", "2 Eixos", "3 Eixos", "4 Eixos", "5 Eixos", "6 Eixos" });
            cbTecnologia.SelectedIndex = 0;

            //Define os controles como invisiveis.
            btnAddFerramentas.Visible = false;
            udgvFerramentas.Visible = false;

            btnAddEmpregados.Visible = false;
            udgvEmpregados.Visible = false;

            //Define os controles como setados.
            rdbtnAtivo.Checked = true;
            rdbtnTorno.Checked = true;

        }

        /// <summary>
        ///     Método que limpa a seleção de todos os controles da interface (background color = yellow e o errorProvider)
        /// e seta a seleção em um único controle na interface, destacando para o usuário.
        /// </summary>
        /// <param name="Ctrl">Controle que deverá ser setado como conrtole ativo.</param>
        private void SetaSelecaoCaixa(Control Ctrl)
        {
            alert.Clear();
            Ctrl.BackColor = Color.Yellow;

            if (Objects.CfgAtual._Idioma.Name.ToUpper() == "PT-BR")
            {
                alert.SetError(Ctrl, "Insira um dado válido");
            }
            if (Objects.CfgAtual._Idioma.Name.ToUpper() == "EN-US")
            {
                alert.SetError(Ctrl, en_US.FrmUserConfigCnns_DadoValido);
            }
        }

        private void CadastraNovaMaquina()
        {
            if (ValidaAntesSalvar())
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("@NOME", "");
                dic.Add("@ATIVO", "");
                dic.Add("@MCP", "");
                dic.Add("@CGD", "");
                dic.Add("@CNC", "");
                dic.Add("@ENV", "");
                dic.Add("@VER", "");
                dic.Add("@EIXO", "");
                dic.Add("@HORAMQN", "");
                dic.Add("@HORAHM", "");
                dic.Add("@DESC", "");
                dic.Add("@UNI", "");
                dic.Add("@DA1", "");
                dic.Add("@DA2", "");
                dic.Add("@DA3", "");

                Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.CADASTRA_NOVA_MAQUINA, dic);

            }
            else { }
        }

        /// <summary>
        ///     Valida dados antes de salvar
        /// </summary>
        /// <returns>True caso não esteja algum campo devidamente preenchido.</returns>
        private Boolean ValidaAntesSalvar()
        {
            int ret = 0;

            ret = String.IsNullOrEmpty(txtNomeMqn.Text) ? +1 : 0;
            ret = String.IsNullOrEmpty(txtUnidade.Text) ? +1 : 0;
            ret = String.IsNullOrEmpty(txtDirCgd.Text) ? +1 : 0;
            ret = String.IsNullOrEmpty(txtDirCp.Text) ? +1 : 0;
            ret = String.IsNullOrEmpty(txtDirCnc.Text) ? +1 : 0;
            ret = String.IsNullOrEmpty(txtPosVer.Text) ? +1 : 0;
            ret = cbTecnologia.SelectedIndex == 0 || cbTecnologia.SelectedIndex == -1 ? +1 : 0;
            ret = String.IsNullOrEmpty(txtHMqn.Text) ? +1 : 0;
            ret = String.IsNullOrEmpty(txtHMan.Text) ? +1 : 0;

            if (cbxAddFerramentas.Checked)
                ret = udgvFerramentas.Rows.Count == 0 ? +1 : 0;

            if (cbxAddEmpregados.Checked)
                ret = udgvEmpregados.Rows.Count == 0 ? +1 : 0;

            return ret > 0;            
        }

        ///// <summary>
        /////     Cadastra a nova máquina no banco de dados auxiliar da integração e retorna
        ///// como true caso a máquina tenha sido cadastrada com êxito.
        ///// </summary>
        //private void CadastraNewMachine()
        //{
        //    Sql sql = null;

        //    try
        //    {
        //        //Cadastra a máquina
        //        CadastraNewMachine();

        //        sql = new Sql(Objects.sCfgAtual._AuxStringConnectionSql, true);

        //        sql.ExecutaQueryTransacao(Consultas_EcMgr.CADASTRA_NOVA_MAQUINA, MontaDic_NewMqn());

        //        sql.ComitaTransacao();

        //        //TODO: CONSULTAR ID DA MÁQUINA EM SEQUÊNCIA
        //    }
        //    catch (Exception ex)
        //    {
        //        sql.RoolbackTransacao();
        //        throw ex;
        //    }
        //}

        ///// <summary>
        /////     Cria um dicionário de dados contendo os parâmetros e os valores para um cadastro de uma nova máquina.
        ///// </summary>
        ///// <returns>Dicionário de dados contendo os parâmetros e valores para cadastrar uma máquina</returns>
        //private Dictionary<String, Object> MontaDic_NewMqn()
        //{
        //    Dictionary<String, object> dic = new Dictionary<string, object>();

        //    dic.Add("@refMqn", txtRef.Text);

        //    if (radioButton6.Checked) dic.Add("@isActive", 1);
        //    //if (radioButton5.Checked) dic.Add("@isActive", 0);
        //    else dic.Add("@isActive", 0);

        //    dic.Add("@dirCp", txtDirCp.Text);
        //    dic.Add("@dirCgd", txtDirCgd.Text);
        //    dic.Add("@dirCnc", txtDirCnc.Text);

        //    //Ambiente que a máquina pertence
        //    if (radioButton1.Checked) dic.Add("@environment", 0);//Torneamento
        //    if (radioButton2.Checked) dic.Add("@environment", 1);//Fresamento
        //    if (radioButton3.Checked) dic.Add("@environment", 2);//Aditiva

        //    dic.Add("@versionPost", txtPosVer.Text);
        //    dic.Add("@progr", txtProgrammer.Text);
        //    dic.Add("@operator", txtOperator.Text);
        //    dic.Add("@axis", cbTecnologia.SelectedItem.ToString());
        //    dic.Add("@costHourMqn", txtHMqn.Text);
        //    dic.Add("@costHourMan", txtHMan.Text);
        //    dic.Add("@desc", txtDescri.Text);
        //    dic.Add("@unit", txtUnit.Text);
        //    dic.Add("@aux1", txtAux1.Text);
        //    dic.Add("@aux2", txtAux2.Text);
        //    dic.Add("@aux3", txtAux3.Text);

        //    return dic;
        //}

        ///// <summary>
        /////     Cadastra as ferramentas adicionadas na interace e retorna
        ///// como true caso a máquina tenha sido cadastrada com êxito.
        ///// </summary>
        //private void CadastraNewMagazine()
        //{
        //    Sql sql = null;

        //    //Signfica que o usuário deseja cadastrar o magazine, em seguida, valido se há informações à serem inseridas.
        //    if (cbNewMgz.Checked)
        //    {
        //        if (dgvTools.Rows.Count == 0) return;

        //        try
        //        {
        //            //Obtém o identificador único da máquina recém cadastrada.
        //            SkaListaMachines m = new SkaListaMachines(txtRef.Text);
        //            int mqnId = m._idMqn;

        //            sql = new Sql(Objects.sCfgAtual._AuxStringConnectionSql, true);

        //            foreach (DataGridViewRow r in dgvTools.Rows)
        //            {
        //                SkaMachineTools t = (SkaMachineTools)r.DataBoundItem;
        //                //CRIAR UM MÉTODO PARA CRIAR O FILTRO SQL PARA CADASTRAR A NOVA FERRAMENTA.
        //                sql.ExecutaQueryTransacao(Consultas_EcMgr.CADASTRA_NOVA_FERRAMENTA, ParametrosNovaTool(t));
        //            }

        //            sql.ComitaTransacao();
        //        }
        //        catch (Exception ex)
        //        {
        //            sql.RoolbackTransacao();
        //            throw ex;
        //        }
        //    }
        //}

        ///// <summary>
        /////     Recebe um objeto do tipo 'SkaMachineTools' e cria uma dicionário de dados para inserir está nova ferramenta
        ///// no banco de dados.
        ///// </summary>
        ///// <returns></returns>
        //private Dictionary<String, object> ParametrosNovaTool(SkaMachineTools t)
        //{
        //    Dictionary<String, object> dic = new Dictionary<string, object>();

        //    dic.Add("@idMqn", t.IdMqn);
        //    dic.Add("@name", t.NomeTool);
        //    dic.Add("@position", t.PosicaoTool);
        //    dic.Add("@preset", t.PresetTool);
        //    dic.Add("@diam", t.DiametroTool);
        //    dic.Add("@coolant", t.TipoCoolant);
        //    dic.Add("@insert", t.InsertoTool);
        //    dic.Add("@envi", t.AmbienteTool);
        //    dic.Add("@type", t.TipoTool);

        //    return dic;
        //}

        ///// <summary>
        /////     Método que cadastra uma nova máquina no banco de dados e valida
        ///// qual parte do cadastro falhou.
        ///// </summary>
        //private void CriaNovoCadastroBanco()
        //{
        //    try
        //    {
        //        CadastraNewMachine();
        //        CadastraNewMagazine();

        //        MessageBox.Show("Cadastro concluído", "Cadastrado com êxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        //Fecha a interface.
        //        btnVoltar_Click(new object(), new EventArgs());
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: Implementar aqui a custom message box?!! (Acredito que sim, pois será ela quem irá salvar os dados no meu banco de dados auxiliar).
        //    }
        //}

        ///// <summary>
        /////     Define o ambiente para cadastrar em uma ferramenta.
        ///// </summary>
        //private int DefineAmbienteTool()
        //{
        //    //É torneamento
        //    if (radioButton1.Checked)
        //        return 0;

        //    if (radioButton2.Checked)
        //        return 1;

        //    if (radioButton3.Checked)
        //        return 2;

        //    return 0;
        //}

        #endregion

        #region Eventos

        private void btnProcuraCgd_Click(object sender, EventArgs e)
        {
            try
            {
                txtDirCgd.Text = mUtil.BuscaArquivo("cgd", "Pós-processador Edgecam");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao procurar arquivo CGD", "FrmMqns_New", "btnProcuraCgd_Click", "Exceção não tratada",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnProcuraCp_Click(object sender, EventArgs e)
        {
            try
            {
                txtDirCp.Text = mUtil.BuscaArquivo("?cp", "Pós-processador Edgecam");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao procurar arquivo ?CP", "FrmMqns_New", "btnProcuraCp_Click", "Exceção não tratada",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnProcuraCnc_Click(object sender, EventArgs e)
        {
            try
            {
                txtDirCnc.Text = mUtil.BuscaDiretorio("Diretório para armazenamento dos arquivos CNC's da máquina");
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao procurar arquivo CNC", "FrmMqns_New", "btnProcuraCnc_Click", "Exceção não tratada",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void cbxAddFerramentas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAddFerramentas.Checked)
            {
                btnAddFerramentas.Visible = true;
                udgvFerramentas.Visible = true;
            }
            else
            {
                btnAddFerramentas.Visible = false;
                udgvFerramentas.Visible = false;
            }
        }

        private void cbxAddEmpregados_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAddEmpregados.Checked)
            {
                btnAddEmpregados.Visible = true;
                udgvEmpregados.Visible = true;
            }
            else
            {
                btnAddEmpregados.Visible = false;
                udgvEmpregados.Visible = false;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                CadastraNovaMaquina();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao cadastrar nova máquina", "FrmMqns_New", "btnCadastrar_Click", "Exceção não tratada",
                                           "<None>", e_TipoErroEx.Erro, ex);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            GC.Collect();
        }

        #endregion

        #region Eventos para definir e remover o foco

        private void txtNomeMqn_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtNomeMqn);
        }

        private void txtNomeMqn_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtNomeMqn.BackColor = Color.White;
        }

        private void txtUnidade_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtUnidade);
        }

        private void txtUnidade_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtUnidade.BackColor = Color.White;
        }

        private void txtDirCgd_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtDirCgd);
        }

        private void txtDirCgd_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtDirCgd.BackColor = Color.White;
        }

        private void txtDirCp_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtDirCp);
        }

        private void txtDirCp_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtDirCp.BackColor = Color.White;
        }

        private void txtDirCnc_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtDirCnc);
        }

        private void txtDirCnc_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtDirCnc.BackColor = Color.White;
        }

        private void txtPosVer_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtPosVer);
        }

        private void txtPosVer_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtPosVer.BackColor = Color.White;
        }

        private void cbTecnologia_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(cbTecnologia);
        }

        private void cbTecnologia_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            cbTecnologia.BackColor = Color.White;
        }

        private void txtHMqn_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtHMqn);
        }

        private void txtHMqn_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtHMqn.BackColor = Color.White;
        }

        private void txtHMan_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtHMan);
        }

        private void txtHMan_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtHMan.BackColor = Color.White;
        }

        private void rtbDescricao_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(rtbDescricao);
        }

        private void rtbDescricao_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            rtbDescricao.BackColor = Color.White;
        }

        private void txtAux1_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtAux1);
        }

        private void txtAux1_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtAux1.BackColor = Color.White;
        }

        private void txtAux2_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtAux2);
        }

        private void txtAux2_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtAux2.BackColor = Color.White;
        }

        private void txtAux3_Enter(object sender, EventArgs e)
        {
            SetaSelecaoCaixa(txtAux3);
        }

        private void txtAux3_Leave(object sender, EventArgs e)
        {
            alert.Clear();
            txtAux3.BackColor = Color.White;
        }

        #endregion

    }
}