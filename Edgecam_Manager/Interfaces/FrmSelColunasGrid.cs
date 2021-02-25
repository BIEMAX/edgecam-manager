using Infragistics.Win.UltraWinGrid;
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
    internal partial class FrmSelColunasGrid : Form
    {
        #region Variáveis globais

        /// <summary>
        ///     Objeto contendo o ultra grid view
        /// </summary>
        private UltraGrid mGrid;

        /// <summary>
        ///     Lista de String contendo o nome das colunas.
        /// </summary>
        private List<String> mLstColunas;

        private int mValorXAtual_Toggle;
        private int mValorYAtual_Toggle;

        private int mValorXAtual_Lbl;
        private int mValorYAtual_Lbl;

        private int mValorXAtual_Lck;
        private int mValorYAtual_Lck;

        /*
         *  There's a trap here that not enough .NET programmers are aware of. 
         *  Responsible for a lot of programs that run with bloated memory footprints. 
         *  Using the Labirint.Properties.Resources.xxxx property creates a new image object, 
         *  it will never match any other image. You need to use the property only once, 
         *  store the images in a field of your class. Roughly:
         *  Link:
         *      https://stackoverflow.com/questions/27749541/how-to-check-if-image-object-is-the-same-as-one-from-resource
         */

        private Image mToggleOn;
        private Image mToggleOff;

        private Image mLock;
        private Image mPadlock;

        #endregion

        #region Propriedades

        public UltraGrid _Grid 
        { 
            get
            {
                return mGrid;
            } 
        }

        #endregion

        #region Instância dos objetos da classe

        public FrmSelColunasGrid(UltraGrid Grid)
        {
            InitializeComponent();

            mGrid = Grid;
            mLstColunas = mGrid.DisplayLayout.Bands[0].Columns.All.AsEnumerable().Where(x => !x.ToString().Contains("Id") && !x.ToString().Contains("_db")).Select(c => c.ToString()).ToArray().ToList();

            ////Preciso remover as colunas DB e ID
            //for (int x = 0; x < mLstColunas.Count; x++)
            //    if (mLstColunas[x].ToString().Contains("Id") || mLstColunas[x].ToString().Contains("_db"))
            //        mLstColunas.Remove(mLstColunas[x]);

            this.Text = "Escolher colunas";

            mToggleOn = Edgecam_Manager.Properties.Resources.toggle_on_48;
            mToggleOff = Edgecam_Manager.Properties.Resources.toggle_off_48;

            mLock = Edgecam_Manager.Properties.Resources.lock_48;
            mPadlock = Edgecam_Manager.Properties.Resources.padlock_48;

            this.CarregaListaColunas();
        }

        #endregion

        #region métodos

        /// <summary>
        ///     Método responsável por carregar a lista de colunas na interface para o usuário.
        /// </summary>
        private void CarregaListaColunas()
        {
            for (int x = 0; x < mLstColunas.Count; x++)
            {
                //Removo as colunas do TIPO ID e DB(valores originais do banco de dados)
                if (mLstColunas[x].ToString().Contains("Id") || mLstColunas[x].ToString().Contains("_db"))
                    continue;

                if (lblNomeColuna.Text == "label1")
                {
                    if (mLstColunas[x].ToUpper().Contains("[HIDDEN]"))
                    {
                        lblNomeColuna.Text = mLstColunas[x].Replace("[hidden]", "");
                        pcbToggle.Image = mToggleOff;
                        pcbCadeado.Image = mPadlock;
                    }
                    else
                    {
                        pcbToggle.Image = mToggleOn;
                        lblNomeColuna.Text = mLstColunas[x];
                    }
                    
                    
                    //Salva os valores atuais de X e Y dos controles
                    mValorXAtual_Toggle = pcbToggle.Location.X;
                    mValorYAtual_Toggle = pcbToggle.Location.Y;

                    mValorXAtual_Lbl = lblNomeColuna.Location.X;
                    mValorYAtual_Lbl = lblNomeColuna.Location.Y;

                    mValorXAtual_Lck = pcbCadeado.Location.X;
                    mValorYAtual_Lck = pcbCadeado.Location.Y;
                }
                else
                {
                    this.AdicionaNovaColunaInterface(mLstColunas[x], x);
                }
            }
        }

        /// <summary>
        ///     Adiciona uma coluna na interface para o usuário e libera para o mesmo
        /// definir o status de exibição da coluna.
        /// </summary>
        /// <param name="NomeColuna">Nome da coluna</param>
        /// <param name="ValorContadorAtual">Valor do contador atual do loop</param>
        private void AdicionaNovaColunaInterface(String NomeColuna, int ValorContadorAtual)
        {
            #region Cadeado

            PictureBox pcbTmp1 = new PictureBox();
            pcbTmp1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            //if (!NomeColuna.ToUpper().Contains("[HIDDEN]"))
            //    pcbTmp1.Image = mLock;
            //else pcbTmp1.Image = mPadlock;

            pcbTmp1.Image = mPadlock;

            //Salva o novo valor do controle
            mValorYAtual_Lck += 35;

            pcbTmp1.Location = new System.Drawing.Point(mValorXAtual_Lck, mValorYAtual_Lck);
            pcbTmp1.Name = "pcbCadeado" + ValorContadorAtual.ToString();
            pcbTmp1.Size = new System.Drawing.Size(31, 29);
            pcbTmp1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pcbTmp1.TabIndex = 2;
            pcbTmp1.TabStop = false;

            //Adiciona o controle à interface.
            panel1.Controls.Add(pcbTmp1);

            #endregion

            #region Label

            Label lblTmp1 = new Label();
            lblTmp1.AutoSize = true;

            //Salva o novo valor do controle
            mValorYAtual_Lbl += 35;

            lblTmp1.Location = new System.Drawing.Point(mValorXAtual_Lbl, mValorYAtual_Lbl);
            lblTmp1.Name = "lblNomeColuna" + ValorContadorAtual.ToString();
            lblTmp1.Size = new System.Drawing.Size(35, 13);
            lblTmp1.TabIndex = 1;
            lblTmp1.Text = NomeColuna.Replace("[hidden]", "");

            //Adiciona o controle à interface.
            panel1.Controls.Add(lblTmp1);

            #endregion

            #region Toggle

            PictureBox pcbTmp2 = new PictureBox();

            if (NomeColuna.ToUpper().Contains("[HIDDEN]"))
                pcbTmp2.Image = mToggleOff;
            else pcbTmp2.Image = mToggleOn;

            //Salva o novo valor do controle
            mValorYAtual_Toggle += 35;

            pcbTmp2.Location = new System.Drawing.Point(mValorXAtual_Toggle, mValorYAtual_Toggle);
            pcbTmp2.Name = "pcbToggle" + ValorContadorAtual.ToString();
            pcbTmp2.Size = new System.Drawing.Size(39, 30);
            pcbTmp2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pcbTmp2.TabIndex = 0;
            pcbTmp2.TabStop = false;
            pcbTmp2.Click += new System.EventHandler(this.pcbToggle_Click);
            
            //Adiciona o controle à interface.
            panel1.Controls.Add(pcbTmp2);

            #endregion
        }

        private void SalvaOrdemExibicaoColunas()
        {
            //TODO: TEM UM PROBLEMA, APÓS ALTERAR A EXIBIÇÃO DAS COLUNAS, NÃO SEI QUAL DELAS MAIS DEVEM SER FIXAS.

            for (int x = 0; x < mLstColunas.Count; x++)
            {
                /*
                 *  Se for a primeira passada do loop (X=0), significa que eu pego
                 * os controles do form, caso contrário (X>0), pego os controles virtuis
                 * criados via programação.
                 */
                var pcbTogTmp = (PictureBox)panel1.Controls["pcbToggle" + (x == 0 ? "" : x.ToString())];
                var lblTmp = (Label)panel1.Controls["lblNomeColuna" + (x == 0 ? "" : x.ToString())];
                var pcbCad = (PictureBox)panel1.Controls["pcbCadeado" + (x == 0 ? "" : x.ToString())];

                if (pcbCad.Image == mLock)
                {
                    //Se a coluna estiver com o cadeado, signfica que ela estará "sempre visível" e o usuário não poderá alterar isso.
                    mGrid.DisplayLayout.Bands[0].Columns[mLstColunas[x].Replace("[hidden]", "").Trim()].Hidden = false;
                }
                else
                {
                    if (pcbTogTmp.Image == mToggleOn)
                        mGrid.DisplayLayout.Bands[0].Columns[mLstColunas[x].Replace("[hidden]", "").Trim()].Hidden = false;
                    else mGrid.DisplayLayout.Bands[0].Columns[mLstColunas[x].Replace("[hidden]", "").Trim()].Hidden = true;
                }
            }

            //TODO: Verificar se devo mostrar uma mensagem ou não?? Não sei, mas tenho que averiguar.
            //MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);            

            btnVoltar_Click(new object(), new EventArgs());
        }

        #endregion

        #region Eventos

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
            GC.Collect();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvaOrdemExibicaoColunas();
        }

        /// <summary>
        ///     Evento responsável por trocar a imagem da picture box e determinar se o item
        /// está ou não visível.
        /// </summary>
        /// <remarks>Utilizado em todos os controles virtuais.</remarks>
        private void pcbToggle_Click(object sender, EventArgs e)
        {
            var pcbTmp = (PictureBox)sender;

            var pcbTmp2 = (PictureBox)panel1.Controls["pcbCadeado" + pcbTmp.Name.Replace("pcbToggle", "")];

            //Se for a imagem do cadeado estiver aberto, significa que ele pode alterar a exibição da coluna.
            //if (pcbTmp2.Image == mPadlock)
            //{
            //    if(pcbTmp.Image == mToggleOn)
            //        pcbTmp.Image = mToggleOff;
            //    else pcbTmp.Image = mToggleOn;
            //}
            if (pcbTmp.Image == mToggleOn)
                pcbTmp.Image = mToggleOff;
            else pcbTmp.Image = mToggleOn;
        }

        #endregion
        
    }
}