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

namespace Edgecam_Manager_MacroDev
{
    public partial class FrmMain : Form
    {
        #region Variáveis globais

        private e_SkaTheme mTheme;
        private String mArquivo;
        private e_SkaModoSalvar mModoSalvar;

        #endregion

        #region Enumeradores

        /// <summary>
        ///     Define a cor de fundo do sistema (branca ou escura).
        /// </summary>
        public enum e_SkaTheme
        {
            Claro,
            Escuro
        }

        /// <summary>
        ///     Define o tipo de salvaento do sistema.
        /// </summary>
        public enum e_SkaModoSalvar
        {
            /// <summary>
            ///     Permite o usuário salvar a macro em um arquivo em um local no windows.
            /// </summary>
            SalvarEmArquivo,
            SalvarEmPropriedade
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Instância o objeto que da acesso à interface e permite definir algumas configurações.
        /// </summary>
        /// <param name="Tema">Claro ou escuro</param>
        /// <param name="ModoSalvar">Modo de salvar (se vai salvar em um arquivo físico ou em uma propriedade
        /// da classe.</param>
        /// <param name="Arquivo">Caminho de um arquivo à ser carregado.</param>
        public FrmMain(e_SkaTheme Tema = e_SkaTheme.Claro, e_SkaModoSalvar ModoSalvar = e_SkaModoSalvar.SalvarEmArquivo, String Arquivo = "")
        {
            InitializeComponent();

            //Salva as configurações.
            mTheme = Tema;
            mModoSalvar = ModoSalvar;
            mArquivo = Arquivo;

            TrocaCorFundo();
            DefineConfiguracoesSintaxe();
            if (!String.IsNullOrEmpty(Arquivo) && File.Exists(Arquivo))
                CarregaArquivo();
        }

        #endregion

        #region Métodos

        /// <summary>
        ///     Troca a cor de fundo do controle Rich Text Box
        /// </summary>
        private void TrocaCorFundo()
        {
            if (mTheme == e_SkaTheme.Claro)
            {
                //Define a cor branca de fundo, igual ao do Visual Studio White Theme.
                rtbTexto.BackColor = Color.White;
            }
            else
            {
                //Define a cor preta de fundo, igual ao do Visual Studio Black Theme.
                rtbTexto.BackColor = System.Drawing.ColorTranslator.FromHtml("#1E1E1E");
            }
        }

        /// <summary>
        ///     Define as configurações de sintaxe (cores das letras e etc).
        /// </summary>
        private void DefineConfiguracoesSintaxe()
        {
            // Adiciona as palavras chaves
            rtbTexto.Settings.Keywords.Add("function");
            rtbTexto.Settings.Keywords.Add("if");
            rtbTexto.Settings.Keywords.Add("then");
            rtbTexto.Settings.Keywords.Add("else");
            rtbTexto.Settings.Keywords.Add("elseif");
            rtbTexto.Settings.Keywords.Add("end");
            rtbTexto.Settings.Keywords.Add("import");
            rtbTexto.Settings.Keywords.Add("var");
            rtbTexto.Settings.Keywords.Add("message");
            rtbTexto.Settings.Keywords.Add("alert");
            rtbTexto.Settings.Keywords.Add("return");
            rtbTexto.Settings.Keywords.Add("_TRUE");
            rtbTexto.Settings.Keywords.Add("_FALSE");
            //rtbTexto.Settings.Keywords.Add("()");//Não funciona
            rtbTexto.Settings.Keywords.Add("Number");
            rtbTexto.Settings.Keywords.Add("while");
            rtbTexto.Settings.Keywords.Add("new");
            rtbTexto.Settings.Keywords.Add("switch");
            rtbTexto.Settings.Keywords.Add("case");
            rtbTexto.Settings.Keywords.Add("break");
            rtbTexto.Settings.Keywords.Add("default");


            // Define qual texto irá representar os comentários
            rtbTexto.Settings.Comment = "//";

            // Define as cores para destaque
            rtbTexto.Settings.NormalTextColor = mTheme == e_SkaTheme.Escuro ? Color.White : Color.Black;
            rtbTexto.Settings.KeywordColor = mTheme == e_SkaTheme.Escuro ? ColorTranslator.FromHtml("#569CD6") : Color.Blue;
            rtbTexto.Settings.CommentColor = Color.Green;
            rtbTexto.Settings.StringColor = ColorTranslator.FromHtml("#C8703D");
            rtbTexto.Settings.IntegerColor = Color.Red;
            //rtbTexto.Settings.StringColor = Color.Orange;
            //rtbTexto.Settings.StringColor = System.Drawing.ColorTranslator.FromHtml("#D69D85");//Laranja pessego

            // habilita o destaque em números inteiros e textos
            rtbTexto.Settings.EnableStrings = true;
            rtbTexto.Settings.EnableIntegers = false;

            // Let's make the settings we just set valid by compiling
            // the keywords to a regular expression.
            rtbTexto.CompileKeywords();

            // Carrega um arquivo qualquer e inicia o destaque das palavras chaves.
            //rtbTexto.LoadFile("../script.lua", RichTextBoxStreamType.PlainText);
            //rtbTexto.ProcessAllLines();
        }

        /// <summary>
        ///     Carrega um arquivo e o carrega na interface para o usuário realizar modificações.
        /// </summary>
        private void CarregaArquivo()
        {
            if (File.Exists(mArquivo))
            {
                rtbTexto.Text = File.ReadAllText(mArquivo);
            }
            else
            {
                MessageBox.Show(String.Format("Não foi possível ler o arquivo de nome '{0}'.", mArquivo), "Falha na leitura do arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Abre um arquivo de texto de extensão JS e carrega na interface.
        /// </summary>
        private void AbreArquivo()
        {
            SkaUtil u = new SkaUtil();

            //Aqui contém o arquivo selecionado pelo usuário.
            String arqUsr = u.BuscaArquivo("js", "Arquivo de macro do Edgecam");

            if (File.Exists(arqUsr))
            {
                rtbTexto.LoadFile(arqUsr, RichTextBoxStreamType.PlainText);
                //Não descomentar a linha abaixo, pois irá aumentar o tempo de leitura absurdamente.
                //rtbTexto.ProcessAllLines();
            }
            else MessageBox.Show("Não foi possível carregar o arquivo", "Arquivo não localizado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        ///     Aa
        /// </summary>
        private void SalvaArquivo()
        {
            SkaUtil u = new SkaUtil();

            if (u.SalvaArquivo(rtbTexto.Text, "js", "JavaScript", true))
                MessageBox.Show("Arquivo salvo com êxito", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Não foi possível salvar o arquivo. Verifique se as permissões no diretório escolhido.", "Falha ao tentar salvar o arquivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Evento que valida se o usuário inseriu algum texto para habilitar o
        /// botão de salvamento.
        /// </summary>
        private void tsmArquivo_DropDownOpening(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(rtbTexto.Text))
                tsmSalvar.Enabled = true;
            else tsmSalvar.Enabled = false;
        }

        private void tsmAbrir_Click(object sender, EventArgs e)
        {
            AbreArquivo();
        }

        private void tsmSalvar_Click(object sender, EventArgs e)
        {
            SalvaArquivo();
        }

        private void tsmSair_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        #endregion
        
    }
}