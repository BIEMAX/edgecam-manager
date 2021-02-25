using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Edgecam_Manager_AutoUpdate
{
    internal partial class FrmDownloading : Form
    {
        #region Variáveis Globais

        private WebClient mWebCliente;
        private BackgroundWorker mBgWorker;
        private String mTmpFile;
        private String mMd5;

        internal String _TmpFilePath
        {
            get
            {
                return this.mTmpFile;
            }
        }

        #endregion

        #region Instância de objetos da classe

        internal FrmDownloading(Uri DownloadUri, String Md5)
        {
            InitializeComponent();

            mTmpFile = Path.GetTempFileName();
            mMd5 = Md5;

            mWebCliente = new WebClient();
            mWebCliente.DownloadProgressChanged += new DownloadProgressChangedEventHandler(mWebCliente_DownloadProgressChanged);
            mWebCliente.DownloadFileCompleted += new AsyncCompletedEventHandler(mWebCliente_DownloadFileCompleted);

            mBgWorker = new BackgroundWorker();
            mBgWorker.DoWork += new DoWorkEventHandler(mBgWorker_DoWork);
            mBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mBgWorker_RunWorkerCompleted);

            try
            {
                mWebCliente.DownloadFileAsync(DownloadUri, mTmpFile);
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                GC.Collect();
            }
        }

        #endregion

        #region Métodos

        private String FormataBytes(long Bytes, int DecimalPlaces, Boolean ShowByteType)
        {
            double newBytes = Bytes;
            String ret = "{0";
            String byteType = "B";

            if (newBytes > 1024 && newBytes < 1048576)
            {
                newBytes /= 1024;
                byteType = "KB";
            }
            else if (newBytes > 1048576 && newBytes < 1073741824)
            {
                newBytes /= 1048576;
                byteType = "MB";
            }
            else
            {
                newBytes /= 1073741824;
                byteType = "GB";
            }

            if (DecimalPlaces > 0)
                ret += "0";

            for (int i = 0; i < DecimalPlaces; i++)
                ret += "0";

            ret += "}";

            if (ShowByteType)
                ret += byteType;

            return String.Format(ret, newBytes);
        }

        #endregion

        #region Eventos

        private void mWebCliente_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.lblProgresso.Text = String.Format("Efetuando download '{0}' de '{1}'", FormataBytes(e.BytesReceived, 1, true), FormataBytes(e.TotalBytesToReceive, 1, true));
        }

        private void mWebCliente_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                this.Close();
            }
            else if (e.Cancelled)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Close();
            }
            else
            {
                lblProgresso.Text = "Verificando download...";
                progressBar.Style = ProgressBarStyle.Marquee;

                mBgWorker.RunWorkerAsync(new string[] { this.mTmpFile, this.mMd5 });
            }
        }

        private void mBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string file = ((string[])e.Argument)[0];
            string updateMd5 = ((string[])e.Argument)[1];

            //if (AutoUpdateHasher.HashFile(file, HashType.MD5) != updateMd5)
            //    e.Result = DialogResult.No;
            //else e.Result = DialogResult.OK;
            e.Result = DialogResult.OK;
        }

        private void mBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = (DialogResult)e.Result;
            this.Close();
        }

        private void FrmDownloading_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mWebCliente.IsBusy)
            {
                mWebCliente.CancelAsync();
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }

            if (mBgWorker.IsBusy)
            {
                mBgWorker.CancelAsync();
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
        }

        #endregion
    }
}
