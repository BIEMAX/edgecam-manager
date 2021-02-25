using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.CompilerServices;

namespace Edgecam_Manager_Arquive
{
    internal class EcMgrArquive
    {
        #region Variáveis globais

        private String mDir;
        private System.Globalization.CultureInfo mCult;
        private List<String> mLogInfo;

        /// <summary>
        ///     Lista de idiomas disponíveis.
        /// </summary>
        private List<String> mLanguagesAvailable = new List<string>() { "pt-BR", "en-US", "es" };

        #endregion

        #region Enumeradores

        /// <summary>
        ///     Enumerador determinante da mensagem que será apresentada/adicionada
        /// ao log de erros.
        /// </summary>
        private enum e_SkaMsg
        {
            /// <summary>
            ///     Idioma não disponível
            /// </summary>
            msgid0000,
            /// <summary>
            ///     Mensagem de exceção.
            /// </summary>
            msgid0001,
            /// <summary>
            ///     Diretório não informado.
            /// </summary>
            msgid0002,
            /// <summary>
            ///     Diretório não existente.
            /// </summary>
            msgid0003,
            /// <summary>
            ///     Diretório existente.
            /// </summary>
            msgid0004,
            /// <summary>
            ///     Usuário possuí permissões.
            /// </summary>
            msgid0005,
            /// <summary>
            ///     Usuário não possuí permissões.
            /// </summary>
            msgid0006,
            /// <summary>
            ///     Diretório não existente.
            /// </summary>
            msgid0007,
            /// <summary>
            ///     Usuário sem permissões dentro do vault.
            /// </summary>
            msgid0008,
            msgid0009
        }

        #endregion

        #region Propriedades

        /// <summary>
        ///     Lista de logs gerados pela biblioteca.
        /// </summary>
        public List<String> _LogInfo
        {
            get { return mLogInfo; }
        }

        #endregion

        #region Instância dos objetos da classe

        /// <summary>
        ///     Aa
        /// </summary>
        /// <param name="DirectoryArchive"></param>
        /// <param name="Culture"></param>
        public EcMgrArquive(String DirectoryArchive, String Culture)
        {
            mDir = DirectoryArchive;

            if (mLanguagesAvailable.Where(a => a.ToUpper().Trim() == Culture.ToUpper().Trim()).Count() > 0)
            {
                try { mCult = new System.Globalization.CultureInfo(Culture); }
                catch { mCult = new System.Globalization.CultureInfo("en-US"); }
            }
            else AddLog(GetMsg(e_SkaMsg.msgid0000));

            try
            {
                CreateVault();
            }
            catch (Exception ex)
            {
                throw new Exception(GetMsg(e_SkaMsg.msgid0001), ex);
            }
        }

        /// <summary>
        ///     Método destrutor da classe.
        /// </summary>
        ~EcMgrArquive() { }

        #endregion

        #region Métodos privados

        /// <summary>
        ///     Método que cria o arquivamento.
        /// </summary>
        private void CreateVault()
        {
            if (String.IsNullOrEmpty(mDir))
            {
                AddLog(GetMsg(e_SkaMsg.msgid0002));
                return;
            }
            else if (!Directory.Exists(mDir))
            {
                AddLog(String.Format(GetMsg(e_SkaMsg.msgid0003), mLogInfo));
                Directory.CreateDirectory(mDir);

                CheckVaultPermission();
            }
            else if (Directory.Exists(mDir))
            {
                AddLog(String.Format(GetMsg(e_SkaMsg.msgid0004), mDir));

                if (CheckVaultPermission()) AddLog(GetMsg(e_SkaMsg.msgid0005));
                else
                {
                    AddLog(GetMsg(e_SkaMsg.msgid0006));
                    return;
                }
            }

            CreateSubFolders();
        }

        /// <summary>
        ///     Método responsável por verificar as permissões no diretório.
        /// </summary>
        /// <returns>True caso estiver tudo ok.</returns>
        private Boolean CheckVaultPermission()
        {
            try
            {
                //Primeiro valida se o diretório existe.
                if (!Directory.Exists(mDir)) throw new Exception(GetMsg(e_SkaMsg.msgid0007));

                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(mDir);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMsg(e_SkaMsg.msgid0008), ex);
            }
        }

        /// <summary>
        ///     Método responsável por criar os diretórios.
        /// </summary>
        private void CreateSubFolders()
        {
            Directory.CreateDirectory(Path.Combine(mDir, "Cncs"));
            Directory.CreateDirectory(Path.Combine(mDir, "GenericFiles"));
            Directory.CreateDirectory(Path.Combine(mDir, "Images"));
            Directory.CreateDirectory(Path.Combine(mDir, "Orders"));
            Directory.CreateDirectory(Path.Combine(mDir, "Parts"));
            Directory.CreateDirectory(Path.Combine(mDir, "Posts processors"));
            Directory.CreateDirectory(Path.Combine(mDir, "Process sheets"));
            Directory.CreateDirectory(Path.Combine(mDir, "QuotesAdv"));
            Directory.CreateDirectory(Path.Combine(mDir, "QuotesExp"));
            Directory.CreateDirectory(Path.Combine(mDir, "Reports"));
            Directory.CreateDirectory(Path.Combine(mDir, "TasksFiles"));
            Directory.CreateDirectory(Path.Combine(mDir, "Templates"));
            Directory.CreateDirectory(Path.Combine(mDir, "UserData"));
        }

        /// <summary>
        ///     Adiciona o log a lista, que pode ser apenas lida utilizando as
        /// propriedades dessa classe.
        /// </summary>
        /// <param name="Log">String a ser adicionada.</param>
        private void AddLog(String Log)
        {
            this.mLogInfo.Add(Log);
        }

        /// <summary>
        ///     A partir de um id, método devolve uma mensagem.
        /// </summary>
        /// <param name="MsgId">Id da mensagem.</param>
        /// <returns>String contendo a mensagem.</returns>
        private String GetMsg(e_SkaMsg MsgId)
        {
            if (mCult.ToString() == "pt-BR")
            {
                switch (MsgId)
                {
                    case e_SkaMsg.msgid0000: return pt_BR.msgid_0000;
                    case e_SkaMsg.msgid0001: return pt_BR.msgid_0001;
                    case e_SkaMsg.msgid0002: return pt_BR.msgid_0002;
                    case e_SkaMsg.msgid0003: return pt_BR.msgid_0003;
                    case e_SkaMsg.msgid0004: return pt_BR.msgid_0004;
                    case e_SkaMsg.msgid0005: return pt_BR.msgid_0005;
                    case e_SkaMsg.msgid0006: return pt_BR.msgid_0006;
                    case e_SkaMsg.msgid0007: return pt_BR.msgid_0007;
                    case e_SkaMsg.msgid0008: return pt_BR.msgid_0008;
                    case e_SkaMsg.msgid0009: return pt_BR.msgid_0009;
                    default: return "";
                }
            }
            else if (mCult.ToString() == "en-US")
            {
                switch (MsgId)
                {
                    case e_SkaMsg.msgid0000: return en_US.msgid_0000;
                    case e_SkaMsg.msgid0001: return en_US.msgid_0001;
                    case e_SkaMsg.msgid0002: return en_US.msgid_0002;
                    case e_SkaMsg.msgid0003: return en_US.msgid_0003;
                    case e_SkaMsg.msgid0004: return en_US.msgid_0004;
                    case e_SkaMsg.msgid0005: return en_US.msgid_0005;
                    case e_SkaMsg.msgid0006: return en_US.msgid_0006;
                    case e_SkaMsg.msgid0007: return en_US.msgid_0007;
                    case e_SkaMsg.msgid0008: return en_US.msgid_0008;
                    case e_SkaMsg.msgid0009: return en_US.msgid_0009;
                    default: return "";
                }
            }
            else
            {
                switch (MsgId)
                {
                    case e_SkaMsg.msgid0000: return pt_BR.msgid_0000;
                    case e_SkaMsg.msgid0001: return pt_BR.msgid_0001;
                    case e_SkaMsg.msgid0002: return pt_BR.msgid_0002;
                    case e_SkaMsg.msgid0003: return pt_BR.msgid_0003;
                    case e_SkaMsg.msgid0004: return pt_BR.msgid_0004;
                    case e_SkaMsg.msgid0005: return pt_BR.msgid_0005;
                    case e_SkaMsg.msgid0006: return pt_BR.msgid_0006;
                    case e_SkaMsg.msgid0007: return pt_BR.msgid_0007;
                    case e_SkaMsg.msgid0008: return pt_BR.msgid_0008;
                    case e_SkaMsg.msgid0009: return pt_BR.msgid_0009;
                    default: return "";
                }
            }

        }

        #endregion

        #region Métodos estáticos (públicos)

        /// <summary>
        /// 
        /// </summary>
        private static void DeleteVault(String VaultDir)
        {
            if (CheckVault(VaultDir))
            {
                Directory.Delete(VaultDir, true);
            }
        }

        /// <summary>
        ///     Aa
        /// </summary>
        /// <returns></returns>
        private static Boolean CheckVault(String VaultDir)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Método que copia um arquivo para o arquivamento.
        /// </summary>
        /// <param name="VaultDir">Diretório do arquivamento</param>
        /// <param name="SubFolder">Diretório que será criado para armazenar os arquivos</param>
        /// <param name="FilesToCopy">Arquivos a serem copiados</param>
        public static void AddFileToVault(String VaultDir, String SubFolder, params String[] FilesToCopy)
        {
            try
            {
                if (Directory.Exists(VaultDir))
                {
                    String dirEnd = Path.Combine(VaultDir, SubFolder);

                    if (!Directory.Exists(dirEnd))
                        Directory.CreateDirectory(dirEnd);

                    foreach (String f in FilesToCopy)
                    {
                        if (!String.IsNullOrEmpty(f) && File.Exists(f))
                        {
                            String fileName = f.Substring(f.LastIndexOf("\\") + 1);
                            String tmp = Path.Combine(dirEnd, fileName);
                            File.Copy(f, tmp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        #endregion

    }
}