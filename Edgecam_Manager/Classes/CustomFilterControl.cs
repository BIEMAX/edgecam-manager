using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Edgecam_Manager
{
    public partial class CustomFilterControl : UserControl
    {
        #region Variáveis privadas/globais

        /// <summary>
        ///     Group box containing all the fields to filter.
        /// </summary>
        private UltraGroupBox mGroupBox;
        
        private String mModule;
        private String mInterface;

        #endregion

        #region Propriedades

        public UltraGroupBox _GroupBox { set { mGroupBox = value; } }
        public String _Module { set { mModule = value; } }
        public String _Interface { set { mInterface = value; } }

        #endregion

        #region Instância dos objetos da classe

        public CustomFilterControl()
        {
            this.InitializeComponent();

            this.cbxFilters.Items.Add("<Selecione>");
            this.cbxFilters.SelectedIndex = 0;
            this.cbxFilters.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        }

        private CustomFilterControl(UltraGroupBox GroupBox, Object Module, Object Interface)
        {
            this.InitializeComponent();
            this.mModule     = (String)Module;
            this.mInterface  = (String)Module;
            this.mGroupBox   = GroupBox;

            this.cbxFilters.Items.Add("<Selecione>");
            this.cbxFilters.SelectedIndex = 0;
            this.cbxFilters.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
        }

        #endregion

        #region Métodos

        #endregion

        #region Eventos

        private void cbxFilters_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxFilters.SelectedIndex == 0) this.ubtnResetFilter_Click(new object(), new EventArgs());
                else if (cbxFilters.SelectedItem != null) Objects.LoadFilterValuesInControls(mGroupBox, cbxFilters.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao carregar o filtro desejado", "CustomFilterControl", "cbFilters_ValueChanged", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        /// <summary>
        ///     Before dropdown event, we load all filtes (olds and news) in the list
        /// to user choice.
        /// </summary>
        private void cbxFilters_BeforeDropDown(object sender, CancelEventArgs e)
        {
            try
            {
                Objects.LoadFilterNameListInComboBox(ref cbxFilters, mInterface, mModule);
                //cbxFilters.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao carregar os filtros das tarefas", "CustomFilterControl", "cbFilters_BeforeDropDown", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void ubtnSaveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                FrmFiltros_New f = new FrmFiltros_New(mInterface, mModule, Objects.CreateFilterFromControls(mGroupBox));
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao salvar o filtro", "CustomFilterControl", "ubtnSaveFilter_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void ubtnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                cbxFilters.SelectedIndex = 0;

                if(mGroupBox != null && mGroupBox.Controls.Count > 0)
                {
                    foreach(Control c in mGroupBox.Controls)
                    {
                        if (c.GetType().ToString() == "System.Windows.Forms.TextBox") ((TextBox)c).Text = "(Todos)";
                        if (c.GetType().ToString() == "System.Windows.Forms.CheckBox") ((CheckBox)c).Checked = false;
                        else if (c.GetType().ToString() == "ImagedComboBox.ImagedComboBox") ((ImagedComboBox.ImagedComboBox)c).SelectedIndex = 0;
                        else if (c.GetType().ToString() == "ComboBox") ((CheckBox)c).Checked = false;
                        else if (c.GetType().ToString() == "Infragistics.Win.UltraWinEditors.UltraDateTimeEditor")
                        {
                            ((UltraDateTimeEditor)c).Enabled = false;
                            ((UltraDateTimeEditor)c).DateTime = DateTime.Today;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao resetar o filtro", "CustomFilterControl", "ubtnResetFilter_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        private void ubtnFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmFiltros frm = new FrmFiltros();
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Objects.CadastraNovoLog(true, "Erro ao consultar a lista de filtros das tarefas", "CustomFilterControl", "ubtnFiltros_Click", "", "", e_TipoErroEx.Erro, ex);
            }
        }

        #endregion
    }
}
