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
    internal partial class FrmConfig_ViewLog : Form
    {

        #region Variáveis globais

        private String mIdLog;
        private String mXmlTemp = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "XmlErroEcMgr.xml");

        #endregion

        #region Instância dos objetos da classe

        public FrmConfig_ViewLog(String IdLog)
        {
            InitializeComponent();

            mIdLog = IdLog;

            //Sempre deletar o arquivo que por algum motivo, não foi excluído previamente.
            DeletaXmlTemp();

            ConsultaDetalhesLog();
        }

        #endregion

        #region Métodos

        private void ConsultaDetalhesLog()
        {
            DataTable dt = Objects.CnnBancoLog.ExecutaSql(Consultas_Log.CONSULTA_LOG_POR_ID, new Dictionary<string, object>() { { "@ID", mIdLog } });

            if (dt != null && dt.Rows.Count > 0)
            {
                Text += String.Format("Detalhes do log de id '{0}'", mIdLog);

                txtAcao.Text        = dt.Rows[0]["Acao"].ToString();
                txtDt.Text          = dt.Rows[0]["DtHoraErro"].ToString();
                txtInferface.Text   = dt.Rows[0]["Interface"].ToString();
                txtMsgAux.Text      = dt.Rows[0]["MsgAuxiliar"].ToString();
                txtTitulo.Text      = dt.Rows[0]["TituloErro"].ToString();
                txtUser.Text        = dt.Rows[0]["Usuario"].ToString();

                CriaXmlTemp(dt.Rows[0]["ExStackTrace"].ToString());

                wb.Navigate(mXmlTemp);
            }
        }

        private void CriaXmlTemp(String ConteudoArq)
        {
            if (!String.IsNullOrEmpty(ConteudoArq))
            {
                System.IO.File.AppendAllText(mXmlTemp, ConteudoArq);
            }
        }

        private void DeletaXmlTemp()
        {
            if (System.IO.File.Exists(mXmlTemp))
                System.IO.File.Delete(mXmlTemp);
        }

        #endregion

        #region Eventos

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DeletaXmlTemp();
            Close();
            GC.Collect();
        }

        private void FrmConfig_ViewLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnVoltar_Click(new object(), new EventArgs());
            }
        }

        #endregion

    }
}
