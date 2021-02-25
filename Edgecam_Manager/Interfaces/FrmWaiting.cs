using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Edgecam_Manager
{
    internal partial class FrmWaiting : Form
    {
        private String mDirPeca;
        private String mOpId;

        //public FrmWaiting()
        //{
        //    InitializeComponent();
        //}

        public FrmWaiting(String DirPeca, String OpId)
        {
            InitializeComponent();
            
            this.mDirPeca = DirPeca;
            this.mOpId = OpId;

            //Não pode ser chamado por aqui, caso contrário, a tela de LOADING não
            //AbrePecaEdgecam();
        }

        private void AbrePecaEdgecam()
        {
            //Ao abrir a peça, preciso atualizar o status da ordem de produção no banco de dados.
            Objects.CnnBancoEcMgr.ExecutaSql(Consultas_EcMgr.ATUALIZA_ORDEM_USUARIO_TRABALHANDO,
                                                new Dictionary<string, object> { 
                                                            { "@USER", Objects.UsuarioAtual.Login }, 
                                                            { "@ID", mOpId } 
                                                        });

            Edgecam ec = new Edgecam(true);

            //Obtém um nome e caminho temporário
            String tmpDir = String.Format("{0}.js", Path.GetTempFileName());

            if (!String.IsNullOrEmpty(Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "AbrePeca.js")))
            {
                //Le o conteúdo do arquivo JS
                String conteudo = File.ReadAllText("AbrePeca.js");

                File.WriteAllText(tmpDir, conteudo.Replace("@CAMINHOPECA@", mDirPeca.Replace("\\", "\\\\")));
            }

            //Abre o edgecam e espera ele fechar (Exited).
            ec.AbrirEdgecam(true, tmpDir);

            if (File.Exists(tmpDir))
                File.Delete(tmpDir);

            this.Close();
        }

        private void FrmWaiting_Shown(object sender, EventArgs e)
        {
            AbrePecaEdgecam();
        }
    }
}
