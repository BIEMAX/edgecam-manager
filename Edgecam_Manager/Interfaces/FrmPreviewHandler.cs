using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    internal partial class FrmPreviewHandler : Form
    {

        #region Variáveis da classe

        private PreviewHandler mHandler;

        /// <summary>
        ///     Contém o caminho do arquivo à ser pré-visualizado.
        /// </summary>
        private String mArquivo;

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto e abre um arquivo informado como parâmetro.
        /// </summary>
        /// <param name="Arquivo"></param>
        public FrmPreviewHandler(String Arquivo)
        {
            InitializeComponent();

            this.Text = "Pré-visualização de arquivos";

            if (File.Exists(Arquivo))
            {
                mArquivo = Arquivo;

                AdicionarPreviewHandlerForm();
            }
            else throw new FileNotFoundException("Arquivo não foi encontrado ou não é válido");
        }

        /// <summary>
        ///     Instância o objeto e abre o dialog para pesquisar o(um) arquivo.
        /// </summary>
        public FrmPreviewHandler()
        {
            InitializeComponent();

            this.Text = "Pré-visualização de arquivos";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mArquivo = openFileDialog.FileName;

                AdicionarPreviewHandlerForm();
            }
            else
            {
                FechaForm();
            }
        }

        /// <summary>
        ///     Instância do objeto que já inicia a pré-visualização de um arquivo
        /// informado por parâmetro.
        /// </summary>
        /// <param name="Arquivo">Caminho do arquivo completo a ser visualizado</param>
        //public FrmPreviewHandler(String Arquivo)
        //{
        //    InitializeComponent();

        //    mArquivo = Arquivo;
        //    this.Text = String.Format("Pré-visualização do arquivo '{0}'", mArquivo);

        //    AdicionarPreviewHandlerForm();
        //}

        #endregion

        #region Métodos

        /// <summary>
        ///     Método que adiciona ao 'Panel1' do formulário, o preview handler.
        /// </summary>
        private void AdicionarPreviewHandlerForm()
        {
            Cursor = Cursors.WaitCursor;

            mHandler = new PreviewHandler();

            panel1.Controls.Add(mHandler);

            mHandler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            mHandler.Size = new Size(panel1.Size.Width, panel1.Size.Height);

            //Abre o arquivo de controle de peça no eDrawings 
            mHandler.Open(mArquivo);

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///     Fecha o formulário e limpa o cache do form do sistema.
        /// </summary>
        private void FechaForm()
        {
            this.Close();
            GC.Collect();
        }

        #endregion

    }
}