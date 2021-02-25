using System;
using System.Net;
using System.Xml;

namespace Edgecam_Manager_AutoUpdate
{
    internal class AutoUpdateXml
    {

        #region Variáveis Globais

        private Version mVersion;
        private Uri mUri;
        private String mFileName;
        private String mMd5;
        private String mDescription;
        private String mLaunchArgs;

        #endregion

        #region Propriedades

        internal Version _Version
        {
            get { return this.mVersion; }
        }

        internal Uri _Uri
        {
            get { return this.mUri; }
        }

        internal String _FileName
        {
            get { return this.mFileName; }
        }

        internal String _Md5
        {
            get { return this.mMd5; }
        }

        internal String _Description
        {
            get { return this.mDescription; }
        }

        internal String _LaunchArgs
        {
            get { return this.mLaunchArgs; }
        }

        #endregion

        #region Instância do objeto da classe

        public AutoUpdateXml(Version Version, Uri Uri, String FileName, String Md5, String Description, String LaunchArgs)
        {
            this.mVersion = Version;
            this.mUri = Uri;
            this.mFileName = FileName;
            this.mMd5 = Md5;
            this.mDescription = Description;
            this.mLaunchArgs = LaunchArgs;
        }

        #endregion

        #region Métodos internos

        internal Boolean IsNewerThan(Version Version)
        {
            return this.mVersion > Version;
        }

        #endregion

        #region Métodos estátios

        internal static Boolean ExistsOnServer(Uri Location)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Location.AbsoluteUri);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                resp.Close();

                return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        internal static AutoUpdateXml Parse(Uri Location, String AppId)
        {
            Version version = null;
            String url = "", fileName = "", md5 = "", desc = "", launch = "";

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Location.AbsoluteUri);

                XmlNode nodes = xml.DocumentElement.SelectSingleNode(String.Format("//update[@appID='{0}']", AppId));

                //Pode ser que o usuário tenha digitado errado.
                if (nodes == null)
                    nodes = xml.DocumentElement.SelectSingleNode(String.Format("//update[@appId='{0}']", AppId));

                if (nodes == null)
                    return null;

                version     = Version.Parse(nodes["latestVersion"].InnerText);
                url         = nodes["latestVersionUrl"].InnerText;
                fileName    = nodes["fileName"].InnerText;
                md5         = nodes["md5"].InnerText;
                desc        = nodes["description"].InnerText;
                launch      = nodes["launchArgs"].InnerText;

                Uri tmp = !url.Contains("http") || !url.Contains("https") ? new Uri("http://" + url) : new Uri(url);

                return new AutoUpdateXml(version, tmp, fileName, md5, desc, launch);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
