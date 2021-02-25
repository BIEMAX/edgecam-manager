using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using System.Reflection;

namespace Edgecam_Manager_AutoUpdate
{
    public class AutoUpdateSharp
    {

        #region Variáveis globais

        private IAutoUpdatable mAppInfo;
        private BackgroundWorker mBgWorker;

        #endregion

        #region Instância dos objetos da classe

        public AutoUpdateSharp(IAutoUpdatable AppInfo)
        {
            mAppInfo = AppInfo;

            mBgWorker = new BackgroundWorker();
            mBgWorker.DoWork += new DoWorkEventHandler(mBgWorker_DoWork);
            mBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mBgWorker_RunWorkerCompleted);
        }

        #endregion

        #region Métodos

        public void DoUpdate()
        {
            if (!mBgWorker.IsBusy)
                mBgWorker.RunWorkerAsync(mAppInfo);
        }

        private void DownloadUpdate(AutoUpdateXml Update)
        {
            FrmDownloading frm = new FrmDownloading(Update._Uri, Update._Md5);
            DialogResult result = frm.ShowDialog(this.mAppInfo.Context);

            if (result == DialogResult.OK)
            {
                string currentPath = this.mAppInfo.ApplicationAssembly.Location;
                string newPath = Path.GetDirectoryName(currentPath) + "\\" + Update._FileName;

                UpdateCurrentApplication(frm._TmpFilePath, currentPath, newPath, Update._LaunchArgs);

                Application.Exit();
            }
            else if (result == DialogResult.Abort)
            {
                MessageBox.Show("Atualização cancelada. Essa versão do sistema não será modificada", "Atualização cancelada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Houve um problema durante o download da atualização", "Erro na atualização de versão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Método responsável por deletar a aplicação atual.
        /// </summary>
        private void UpdateCurrentApplication(String TmpFilePath, String CurrentPath, String NewPath, String LaunchArgs)
        {
            String argument = "/C Choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & Choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\" & Start \"\" /D \"{3}\" \"{4}\" {5}";

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = string.Format(argument, CurrentPath, TmpFilePath, NewPath, Path.GetDirectoryName(NewPath), Path.GetFileName(NewPath), LaunchArgs);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.FileName = "cmd.exe";
            Process.Start(psi);
        }

        #endregion

        #region Eventos

        void mBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IAutoUpdatable app = (IAutoUpdatable)e.Argument;

            if (!AutoUpdateXml.ExistsOnServer(app.UpdateXmlLocation))
                e.Cancel = true;
            else e.Result = AutoUpdateXml.Parse(app.UpdateXmlLocation, app.ApplicationID);
        }

        void mBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                AutoUpdateXml update = (AutoUpdateXml)e.Result;

                if (update != null && update.IsNewerThan(mAppInfo.ApplicationAssembly.GetName().Version))
                {
                    this.DownloadUpdate(update);
                }
            }
        }

        #endregion

    }
}
