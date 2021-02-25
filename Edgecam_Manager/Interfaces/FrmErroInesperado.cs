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
    internal partial class FrmErroInesperado : Form
    {

        /// <summary>
        ///     Contém o comentário do usuário.
        /// </summary>
        public String _ComentarioUsuario { get; set; }

        /// <summary>
        ///     Apenas  inicializa uma nova instância.
        /// </summary>
        public FrmErroInesperado(String TituloErro)
        {
            InitializeComponent();

            Text = TituloErro;

            groupBox1.Text = "Ops... Parece que encontramos um erro";
        }

        /// <summary>
        ///     Botão que salva o comentário do usuário em uma propriedade da classe.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            _ComentarioUsuario = String.IsNullOrEmpty(richTextBox1.Text) == true ? "" : richTextBox1.Text;

            Close();
            GC.Collect();
        }

        /// <summary>
        ///     Botão que salva o comentário do usuário em uma propriedade da classe.
        /// </summary>
        private void btnSair_Click(object sender, EventArgs e)
        {
            _ComentarioUsuario = String.IsNullOrEmpty(richTextBox1.Text) == true ? "" : richTextBox1.Text;

            Close();
            GC.Collect();
        }

        /// <summary>
        ///     Botão que salva o comentário do usuário em uma propriedade da classe.
        /// </summary>
        private void FrmErroInesperado_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ComentarioUsuario = String.IsNullOrEmpty(richTextBox1.Text) == true ? "" : richTextBox1.Text;

            Close();
            GC.Collect();
        }
    }
}
