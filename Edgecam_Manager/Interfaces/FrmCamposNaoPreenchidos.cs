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
    public partial class FrmCamposNaoPreenchidos : Form
    {
        private List<String> mLstCampos = new List<String>();

        public FrmCamposNaoPreenchidos(List<String> LstCampos)
        {
            InitializeComponent();
            mLstCampos = LstCampos;
            InicializaInterface();
        }

        private void InicializaInterface()
        {
            if (mLstCampos != null && mLstCampos.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Campo", typeof(String)));

                for (int x = 0; x < mLstCampos.Count; x++)
                {
                    dt.Rows.Add(mLstCampos[x].ToString());
                }

                udgv.DataSource = dt;
            }
            else
            {
                this.Close();
                GC.Collect();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }
    }
}
