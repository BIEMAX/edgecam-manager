﻿using System;
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
    internal partial class FrmMoedas_Seleciona : Form
    {
        #region Variáveis globais

        private String mMoeda;

        #endregion

        #region Propriedades

        /// <summary>
        ///     Contém um objeto selecionado pelo cliente.
        /// </summary>
        public String _MoedaSelecionada
        {
            get
            {
                return mMoeda;
            }
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmMoedas_Seleciona()
        {
            InitializeComponent();

            ConsultaMoedas();
        }

        #endregion

        #region Métodos

        private void ConsultaMoedas()
        {
            udgv.DataSource = SQLQueries.Consulta_Moedas(txtMoeda.Text, true);
        }

        #endregion

        #region Eventos

        private void udgv_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            //Enquanto o usuário não clicar sobre o hiperlink, eu não faço nada.
            if (e.Cell.Column.ToString().ToUpper() != "SELECIONAR")
            {
                return;
            }
            else
            {
                //mMoeda = new SkaCidade();
                //mMoeda.id      = udgv.Rows[e.Cell.Row.Index].Cells["id"].OriginalValue.ToString();
                //mMoeda.Cidade  = udgv.Rows[e.Cell.Row.Index].Cells["Cidade"].OriginalValue.ToString();
                //mMoeda.Estado  = udgv.Rows[e.Cell.Row.Index].Cells["Estado"].OriginalValue.ToString();
                //mMoeda.Pais    = udgv.Rows[e.Cell.Row.Index].Cells["País"].OriginalValue.ToString();

                mMoeda = udgv.Rows[e.Cell.Row.Index].Cells["Moeda"].OriginalValue.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                this.Close();
                GC.Collect();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaMoedas();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar moedas", "FrmMoedas_Seleciona", "btnPesquisar_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Evento que, ao digitar o enter, pesquisa.
        /// </summary>
        private void txtMoeda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPesquisar_Click(new object(), new EventArgs());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
            GC.Collect();
        }

        #endregion
    }
}
