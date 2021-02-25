using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Reflection;

namespace Edgecam_Manager_License
{
    /// <summary>
    ///     Objeto que dá acesso à licença.
    /// </summary>
    [Serializable]
    public class LicenseInfo
    {

        #region Variáveis globais internas

        /// <summary>
        ///     Contém um dicionário de dados com os valores das configurações.
        /// </summary>
        protected Dictionary<Object, String> mParameters;

        protected List<Edgecam_Manager_License.LicenseModules> mLstModules;

        #endregion

        #region Enumeradores

        /// <summary>
        ///     Enumerado contendo os parâmetros disponíveis.
        /// </summary>
        //public enum e_Parameters
        public enum e_h01x
        {
            //License,
            h01xfffS4A,
            //Customer,
            h02xfffS4A,
            //LicenseExpiry,
            h03xfffS4A,
            //VersionEdgecamManager,
            h04xfffS4A,
            //EdgecamLicense,
            h05xfffS4A,
            //LicenseType, (0 = local, 1 = rede)
            h06xfffS4A,
            //ComputerName,
            h07xfffS4A,
            //UserName,
            h08xfffS4A,
            //UserDomainName,
            h09xfffS4A,
            //MacAddress,
            h10xfffS4A,
            //IpAddress,
            h11xfffS4A,
            //OsVersion,
            h12xfffS4A,
            //OsBits,
            h13xfffS4A,
            //Modules
            h14xfffS4A,
            //APIEnabled (0 = false, 1 = true)
            h15xfffS4A,
            //APILogin
            h16xfffS4A,
            //APIPassword
            h17xfffS4A,
            //DateLicGenerate
            h18xfffS4A
        }

        internal enum LicMod
        {
            Tasks = 5158,
            Orders = 5159,
            /// <summary>
            ///     Orçamentos
            /// </summary>
            QuotesViewer = 5160,
            QuotesExpress = 5161,
            QuotesIntermediate = 5162,
            QuotesAdvanced = 5163,
            QuotesSimulator = 5164,
            Criptocurrency = 5165,
            InventoryManager = 5166,
            JobManager = 5167,
            ToolsManager = 5168,
            MachineManager = 5169,
            Reports = 5170,
            API = 5171
        }

        #endregion

        #region Instância dos objetos da classe

        public LicenseInfo()
        {
            this.LoadSettings();
        }

        #endregion

        #region Métodos privados

        /// <summary>
        ///     Carrega as configurações em um dicionário de dados privado.
        /// </summary>
        protected void LoadSettings()
        {
            if (mParameters == null)
                mParameters = new Dictionary<object, string>();

            #region Dados default da licença local

            mParameters.Add("License", Edgecam_Manager_License.Properties.Settings.Default.License.ToString());
            mParameters.Add("Customer", Edgecam_Manager_License.Properties.Settings.Default.Customer.ToString());
            mParameters.Add("LicenseExpiry", Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry.ToString());
            mParameters.Add("VersionEdgecamManager", Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager.ToString());
            mParameters.Add("EdgecamLicense", Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense.ToString());
            //0 - local, 1 - rede
            mParameters.Add("LicenseType", Edgecam_Manager_License.Properties.Settings.Default.LicenseType.ToString());
            mParameters.Add("ComputerName", Edgecam_Manager_License.Properties.Settings.Default.ComputerName.ToString());
            mParameters.Add("UserName", Edgecam_Manager_License.Properties.Settings.Default.UserName.ToString());
            mParameters.Add("UserDomainName", Edgecam_Manager_License.Properties.Settings.Default.UserDomainName.ToString());
            mParameters.Add("MacAddress", Edgecam_Manager_License.Properties.Settings.Default.MacAddress.ToString());
            mParameters.Add("IpAddress", Edgecam_Manager_License.Properties.Settings.Default.IpAddress.ToString());
            mParameters.Add("OsVersion", Edgecam_Manager_License.Properties.Settings.Default.OsVersion.ToString());
            mParameters.Add("OsBits", Edgecam_Manager_License.Properties.Settings.Default.OsBits.ToString());
            mParameters.Add("Modules", Edgecam_Manager_License.Properties.Settings.Default.Modules.ToString());
            mParameters.Add("APIEnabled", Edgecam_Manager_License.Properties.Settings.Default.Modules.ToString());
            mParameters.Add("APILogin", Edgecam_Manager_License.Properties.Settings.Default.Modules.ToString());
            mParameters.Add("APIPassword", Edgecam_Manager_License.Properties.Settings.Default.Modules.ToString());
            mParameters.Add("DateLicGenerate", Edgecam_Manager_License.Properties.Settings.Default.Modules.ToString());

            #endregion

            #region Dados default para licença em rede



            #endregion
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        ///     Contém um dicionário de dados com os valores da licença.
        /// </summary>
        /// <returns>Dicionário de dados de objeto & string.</returns>
        public Dictionary<Object, String> GetConfigValues()
        {
            return mParameters;
        }

        /// <summary>
        ///     Atualiza os dados na DLL relacionados as configurações.
        /// </summary>
        /// <param name="Param1">Parâmetro à ser atualizado.</param>
        /// <param name="Param2">Novo valor.</param>
        public void UpdateConfigValue(e_h01x Param1, String Param2)
        {
            switch (Param1)
            {
                case e_h01x.h01xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.License = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h02xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.Customer = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h03xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h04xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h05xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h06xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseType = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h07xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.ComputerName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h08xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.UserName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h09xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.UserDomainName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h10xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.MacAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h11xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.IpAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h12xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.OsVersion = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h13xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.OsBits = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h14xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.Modules = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h15xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.APIEnabled = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h16xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.APILogin = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h17xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.APIPassword = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case e_h01x.h18xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.DateLicGenerate = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
            }
        }

        /// <summary>
        ///     Atualiza os dados na DLL relacionados as configurações.
        /// </summary>
        /// <param name="Param1">Parâmetro à ser atualizado.</param>
        /// <param name="Param2">Novo valor.</param>
        public void UpdateConfigValueByCode(String Param1, String Param2)
        {
            switch (Param1)
            {
                case "License":
                    Edgecam_Manager_License.Properties.Settings.Default.License = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "Customer":
                    Edgecam_Manager_License.Properties.Settings.Default.Customer = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "LicenseExpiry":
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "VersionEdgecamManager":
                    Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "EdgecamLicense":
                    Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "LicenseType":
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseType = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "ComputerName":
                    Edgecam_Manager_License.Properties.Settings.Default.ComputerName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "UserName":
                    Edgecam_Manager_License.Properties.Settings.Default.UserName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "UserDomainName":
                    Edgecam_Manager_License.Properties.Settings.Default.UserDomainName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "MacAddress":
                    Edgecam_Manager_License.Properties.Settings.Default.MacAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "IpAddress":
                    Edgecam_Manager_License.Properties.Settings.Default.IpAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "OsVersion":
                    Edgecam_Manager_License.Properties.Settings.Default.OsVersion = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "OsBits":
                    Edgecam_Manager_License.Properties.Settings.Default.OsBits = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "Modules":
                    Edgecam_Manager_License.Properties.Settings.Default.Modules = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "APIEnabled":
                    Edgecam_Manager_License.Properties.Settings.Default.APIEnabled = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "APILogin":
                    Edgecam_Manager_License.Properties.Settings.Default.APILogin = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "APIPassword":
                    Edgecam_Manager_License.Properties.Settings.Default.APIPassword = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "DateLicGenerate":
                    Edgecam_Manager_License.Properties.Settings.Default.DateLicGenerate = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
            }
        }

        /// <summary>
        ///     Obtém os dados na DLL relacionados as configurações,
        /// obtendo de configuração à configuração (Uma por vez).
        /// </summary>
        /// <param name="Param">Parâmetro à ser obtido.</param>
        public String GetSingleConfigValue(e_h01x Param)
        {
            switch (Param)
            {
                case e_h01x.h01xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.License;
                case e_h01x.h02xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.Customer;
                case e_h01x.h03xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry;
                case e_h01x.h04xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager;
                case e_h01x.h05xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense;
                case e_h01x.h06xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.LicenseType;
                case e_h01x.h07xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.ComputerName;
                case e_h01x.h08xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.UserName;
                case e_h01x.h09xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.UserDomainName;
                case e_h01x.h10xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.MacAddress;
                case e_h01x.h11xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.IpAddress;
                case e_h01x.h12xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.OsVersion;
                case e_h01x.h13xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.OsBits;
                case e_h01x.h14xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.Modules;
                case e_h01x.h15xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.APIEnabled.ToString();
                case e_h01x.h16xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.APILogin;
                case e_h01x.h17xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.APIPassword;
                case e_h01x.h18xfffS4A:
                    return Edgecam_Manager_License.Properties.Settings.Default.DateLicGenerate;
                default: return "";
            }
        }

        #endregion

        #region Métodos estáticos

        /// <summary>
        ///     Obtém os dados na DLL relacionados as configurações,
        /// obtendo de configuração à configuração (Uma por vez).
        /// </summary>
        internal static Dictionary<Object, String> GetAllConfigValues()
        {
            Dictionary<object, string> ret = new Dictionary<object, string>();

            ret.Add("License", Edgecam_Manager_License.Properties.Settings.Default.License.ToString());
            ret.Add("Customer", Edgecam_Manager_License.Properties.Settings.Default.Customer.ToString());
            ret.Add("LicenseExpiry", Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry.ToString());
            ret.Add("VersionEdgecamManager", Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager.ToString());
            ret.Add("EdgecamLicense", Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense.ToString());
            //0 - local, 1 - rede
            ret.Add("LicenseType", Edgecam_Manager_License.Properties.Settings.Default.LicenseType.ToString());
            ret.Add("ComputerName", Edgecam_Manager_License.Properties.Settings.Default.ComputerName.ToString());
            ret.Add("UserName", Edgecam_Manager_License.Properties.Settings.Default.UserName.ToString());
            ret.Add("UserDomainName", Edgecam_Manager_License.Properties.Settings.Default.UserDomainName.ToString());
            ret.Add("MacAddress", Edgecam_Manager_License.Properties.Settings.Default.MacAddress.ToString());
            ret.Add("IpAddress", Edgecam_Manager_License.Properties.Settings.Default.IpAddress.ToString());
            ret.Add("OsVersion", Edgecam_Manager_License.Properties.Settings.Default.OsVersion.ToString());
            ret.Add("OsBits", Edgecam_Manager_License.Properties.Settings.Default.OsBits.ToString());
            ret.Add("Modules", Edgecam_Manager_License.Properties.Settings.Default.Modules.ToString());
            ret.Add("APIEnabled", Edgecam_Manager_License.Properties.Settings.Default.APIEnabled.ToString());
            ret.Add("APILogin", Edgecam_Manager_License.Properties.Settings.Default.APILogin.ToString());
            ret.Add("APIPassword", Edgecam_Manager_License.Properties.Settings.Default.APIPassword.ToString());
            ret.Add("DateLicGenerate", Edgecam_Manager_License.Properties.Settings.Default.DateLicGenerate.ToString());

            return ret;
        }

        /// <summary>
        ///     Atualiza os dados na DLL relacionados as configurações.
        /// </summary>
        /// <param name="Param1">Parâmetro à ser atualizado.</param>
        /// <param name="Param2">Novo valor.</param>
        internal static void UpdateConfigValue2(e_h01x Param1, String Param2)
        {
            switch (Param1)
            {
                case e_h01x.h01xfffS4A:
                    //SkaLicense.Properties.Settings.Default.SkaLicense = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h02xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.Customer = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h03xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h04xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h05xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h06xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseType = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h07xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.ComputerName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h08xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.UserName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h09xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.UserDomainName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h10xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.MacAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h11xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.IpAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h12xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.OsVersion = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h13xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.OsBits = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h14xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.Modules = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h15xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.APIEnabled = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h16xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.APILogin = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h17xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.APIPassword = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
                case e_h01x.h18xfffS4A:
                    Edgecam_Manager_License.Properties.Settings.Default.DateLicGenerate = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    Edgecam_Manager_License.Properties.Settings.Default.Upgrade();
                    break;
            }
        }

        /// <summary>
        ///     Atualiza os dados na DLL relacionados as configurações.
        /// </summary>
        /// <param name="Param1">Parâmetro à ser atualizado.</param>
        /// <param name="Param2">Novo valor.</param>
        internal static void UpdateConfigValueByCode2(String Param1, String Param2)
        {
            switch (Param1)
            {
                case "License":
                    //SkaLicense.Properties.Settings.Default.SkaLicense = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "Customer":
                    Edgecam_Manager_License.Properties.Settings.Default.Customer = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "LicenseExpiry":
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseExpiry = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "VersionEdgecamManager":
                    Edgecam_Manager_License.Properties.Settings.Default.VersionEdgecamManager = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "EdgecamLicense":
                    Edgecam_Manager_License.Properties.Settings.Default.EdgecamLicense = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "LicenseType":
                    Edgecam_Manager_License.Properties.Settings.Default.LicenseType = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "ComputerName":
                    Edgecam_Manager_License.Properties.Settings.Default.ComputerName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "UserName":
                    Edgecam_Manager_License.Properties.Settings.Default.UserName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "UserDomainName":
                    Edgecam_Manager_License.Properties.Settings.Default.UserDomainName = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "MacAddress":
                    Edgecam_Manager_License.Properties.Settings.Default.MacAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "IpAddress":
                    Edgecam_Manager_License.Properties.Settings.Default.IpAddress = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "OsVersion":
                    Edgecam_Manager_License.Properties.Settings.Default.OsVersion = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "OsBits":
                    Edgecam_Manager_License.Properties.Settings.Default.OsBits = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "Modules":
                    Edgecam_Manager_License.Properties.Settings.Default.Modules = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "APIEnabled":
                    Edgecam_Manager_License.Properties.Settings.Default.APIEnabled = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "APILogin":
                    Edgecam_Manager_License.Properties.Settings.Default.APILogin = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "APIPassword":
                    Edgecam_Manager_License.Properties.Settings.Default.APIPassword = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
                case "DateLicGenerate":
                    Edgecam_Manager_License.Properties.Settings.Default.DateLicGenerate = Param2;
                    Edgecam_Manager_License.Properties.Settings.Default.Save();
                    break;
            }
        }

        /// <summary>
        ///     Método responsável por validar, se o cliente possuí o módulo desejado,
        /// de acordo com o código do módulo.
        /// </summary>
        /// <param name="Module">Código do módulo</param>
        /// <returns>True caso o cliente possua o módulo</returns>
        internal static Boolean HasModule(int Module)
        {
            try
            {
                var modules = JsonConvert.DeserializeObject<List<Edgecam_Manager_License.LicenseModules>>(Edgecam_Manager_License.Properties.Settings.Default.Modules);
                LicMod m = (LicMod)Module;

                switch (Module)
                {
                    case 5158: return modules.Where(x => x.NomeModulo == "Tarefas" && x.Habilitado).Count() > 0;
                    case 5159: return modules.Where(x => x.NomeModulo == "Ordens de produção" && x.Habilitado).Count() > 0;
                    case 5160: return modules.Where(x => x.NomeModulo == "Orçamentos" && x.Habilitado).Count() > 0;
                    case 5161: return modules.Where(x => x.NomeModulo == "Orçamentos expressos" && x.Habilitado).Count() > 0;
                    case 5162: return modules.Where(x => x.NomeModulo == "Orçamentos intermediários" && x.Habilitado).Count() > 0;
                    case 5163: return modules.Where(x => x.NomeModulo == "Orçamentos avançados" && x.Habilitado).Count() > 0;
                    case 5164: return modules.Where(x => x.NomeModulo == "Simulador para Orçamentos" && x.Habilitado).Count() > 0;
                    case 5165: return modules.Where(x => x.NomeModulo == "Criptomoedas" && x.Habilitado).Count() > 0;
                    case 5166: return modules.Where(x => x.NomeModulo == "Gerenciador de inventários" && x.Habilitado).Count() > 0;
                    case 5167: return modules.Where(x => x.NomeModulo == "Gerenciador de trabalhos" && x.Habilitado).Count() > 0;
                    case 5168: return modules.Where(x => x.NomeModulo == "Gerenciador de ferramentas" && x.Habilitado).Count() > 0;
                    case 5169: return modules.Where(x => x.NomeModulo == "Gerenciador de máquinas" && x.Habilitado).Count() > 0;
                    case 5170: return modules.Where(x => x.NomeModulo == "Relatórios" && x.Habilitado).Count() > 0;
                    case 5171: return modules.Where(x => x.NomeModulo == "API" && x.Habilitado).Count() > 0;
                    default: return false;
                }
            }
            catch { return false; }
        }

        #endregion
    }

    #region Classes de exceção personalizadas

    [Serializable]
    public class EcMGRInvalidLicenseKeyException : Exception
    {
        public EcMGRInvalidLicenseKeyException()
        { }

        public EcMGRInvalidLicenseKeyException(String Key)
            : base(String.Format("The license key '{0}' is not valid.", Key))
        {
        }
    }

    [Serializable]
    public class EcMGRLicenseTypeNotSupportedException : Exception
    {
        public EcMGRLicenseTypeNotSupportedException()
        { }

        public EcMGRLicenseTypeNotSupportedException(String Type)
            : base(String.Format("License type '{0}' is not supported.", Type))
        {
        }
    }

    [Serializable]
    public class EcMGRLicenseChangedException : Exception
    {
        public EcMGRLicenseChangedException()
        { }

        public EcMGRLicenseChangedException(String License, String AdditionalInfo)
            : base(String.Format("This license '{0}' has changed from the latest login and the license is not more supported. Please, contact your reseller. The '{1}' has changed", License, AdditionalInfo))
        {
        }
    }

    [Serializable]
    public class EcMGRComputerChangedExpection : Exception
    {
        public EcMGRComputerChangedExpection() { }

        public EcMGRComputerChangedExpection(String Computer, String AdditionalInfo)
            : base(String.Format("This computer '{0}' has changed from the latest login and the license is not more supported. Please, contact your reseller. The '{1}' has changed", Computer, AdditionalInfo))
        {
        }
    }

    [Serializable]
    public class EcMGRLicenseExpiredException : Exception
    {
        public EcMGRLicenseExpiredException()
        { }

        public EcMGRLicenseExpiredException(String Date)
            : base(String.Format("The license '{0}' has expired and is not more supported. Please, contact your reseller.", Date))
        {
        }
    }

    [Serializable]
    public class EcMGRLicenseNotLoadedException : Exception
    {
        public EcMGRLicenseNotLoadedException() { }

        public EcMGRLicenseNotLoadedException(String AdditionalInfo)
            : base(String.Format("Not was possible to load license for some unknown reason. Additional info: '{0}'", AdditionalInfo))
        {

        }
    }


    #endregion

}