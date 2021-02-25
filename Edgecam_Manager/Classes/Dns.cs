/***********************************************************************************************************
 * 
 * 
 *             Dns - Classe com métodos para obter informações sobre o computador corrente
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Obter o MacAddress, IP Address, Machine name, Domain Name, Current user, etc...
 *      Version:    2.2
 *      Date:       20/11/2017, at 02:31 AM
 *      Note:       <None>
 *      History:    Update      - 20/11/2017 - 02:31 AM - Primeira versão - V1.0 Lançada
 *                  Update      - 10/05/2019 - 03:15 PM - Alterado o método 'GetLocalIpAddress' - V1.1 Lançada.
 *                  Update      - 13/05/2019 - 11:01 AM - Adicionado os comentários nas propriedades - V1.2 Lançada.
 *                  Update      - 13/05/2019 - 11:05 AM - Adicionado a propriedade '_LocalIpAddress' - V1.3 Lançada.
 *                  Update      - 13/05/2019 - 11:26 AM - Adicionado o método 'IsPortBusy' - V1.4 Lançada.
 *                  Update      - 13/05/2019 - 01:25 PM - Alterado a visibilidade do método 'GetLocalIpAddress' de 'protected' para 'public' - V1.5 Lançada.
 *                  Update      - 14/05/2019 - 08:13 AM - Alterado a lógica do método 'GetLocalIpAddress' - V1.6 Lançada.
 *                  Update      - 24/07/2019 - 08:45 AM - Adicionado o método 'GetMachineNameFromIPAddress' - V1.7 Lançada.
 *                  Update      - 24/07/2019 - 09:19 AM - Adicionado o método 'GetIPAddressFromMachineName' - V1.8 Lançada.
 *                  Update      - 24/07/2019 - 10:35 AM - Adicionado o método 'GetAllDevicesOnLAN' - V1.9 Lançada.
 *                  Update      - 27/08/2019 - 03:23 PM - Alterado a propriedade de '_MacAddress' para '_ActiveMacAddress' - V2.0 Lançada.
 *                  Update      - 27/08/2019 - 03:35 PM - Adicionado a propriedade '_AllMacAddressInstalled' - V2.1 Lançada.
 *                  Update      - 27/08/2019 - 03:35 PM - Adicionado o método 'ExistMacAddressInHost' - V2.2 Lançada.
 *                  
 * 
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

/// <summary>
///     Classe para obter os dados do computador atual, cujo está executando uma determinada aplicação.
/// </summary>
internal class Dns
{

    #region External references

    /// <summary>
    /// MIB_IPNETROW structure returned by GetIpNetTable
    /// DO NOT MODIFY THIS STRUCTURE.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct MIB_IPNETROW
    {
        [MarshalAs(UnmanagedType.U4)]
        public int dwIndex;
        [MarshalAs(UnmanagedType.U4)]
        public int dwPhysAddrLen;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac0;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac1;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac2;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac3;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac4;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac5;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac6;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac7;
        [MarshalAs(UnmanagedType.U4)]
        public int dwAddr;
        [MarshalAs(UnmanagedType.U4)]
        public int dwType;
    }

    /// <summary>
    /// GetIpNetTable external method
    /// </summary>
    /// <param name="pIpNetTable"></param>
    /// <param name="pdwSize"></param>
    /// <param name="bOrder"></param>
    /// <returns></returns>
    [DllImport("IpHlpApi.dll")]
    [return: MarshalAs(UnmanagedType.U4)]
    static extern int GetIpNetTable(IntPtr pIpNetTable, [MarshalAs(UnmanagedType.U4)] ref int pdwSize, bool bOrder);

    /// <summary>
    /// Error codes GetIpNetTable returns that we recognise
    /// </summary>
    const int ERROR_INSUFFICIENT_BUFFER = 122;

    #endregion

    #region Variáveis globais

    private String mMachineName;
    private String mMachineDomainName;
    private String mUserName;

    /// <summary>
    ///     Contém o mac address ativo no computador.
    /// </summary>
    private String mActiveMacAddress;
    private String mIntranetIpAddress;
    private String mLocalIpAddress = "127.0.0.1";
    private String mOsVersion;
    private Boolean mIs64Bits;

    /// <summary>
    ///     Contém uma lista de todos os mac address no computador corrente.
    /// </summary>
    private List<String> mLstMacAddress;

    #endregion

    #region Propriedades

    /// <summary>
    ///     Contém o nome da máquina/computador atual
    /// </summary>
    public String _MachineName
    {
        get
        {
            return mMachineName.ToUpper();
        }
        private set
        {
            mMachineName = value;
        }
    }

    /// <summary>
    ///     Contém o domínimo da máquina/computador local.
    /// </summary>
    public String _MachineDomainName
    {
        get
        {
            return mMachineDomainName.ToUpper();
        }
        private set
        {
            mMachineDomainName = value;
        }
    }

    /// <summary>
    ///     Obtém o nome do usuário corrente.
    /// </summary>
    public String _UserName
    {
        get
        {
            return mUserName;
        }
        private set
        {
            mUserName = value;
        }
    }

    /// <summary>
    ///     Contém o MacAddress ativo do computador atual
    /// </summary>
    public String _ActiveMacAddress
    {
        get
        {
            return mActiveMacAddress;
        }
        private set
        {
            mActiveMacAddress = value;
        }
    }

    /// <summary>
    ///     Contém o IP da rede (intranet)
    /// </summary>
    public String _IntranetIpAddress
    {
        get
        {
            return mIntranetIpAddress;
        }
        private set
        {
            mIntranetIpAddress = value;
        }
    }

    /// <summary>
    ///     Contém o IP local (127.0.0.1).
    /// </summary>
    public String _LocalIpAddress
    {
        get
        {
            return mLocalIpAddress;
        }
    }

    /// <summary>
    ///     Contém a versão do sistema operacional atual.
    /// </summary>
    public String _OsVersion
    {
        get
        {
            return mOsVersion;
        }
        private set
        {
            mOsVersion = value;
        }
    }

    /// <summary>
    ///     True caso o sistema operacional seja 64 bits.
    /// </summary>
    public Boolean _Is64Bits
    {
        get
        {
            return mIs64Bits;
        }
        private set
        {
            mIs64Bits = value;
        }
    }

    /// <summary>
    ///     Lista contendo todos os mac address ativos e instalados no computador correte.
    /// </summary>
    public List<String> _AllMacAddressInstalled
    {
        get
        {
            return mLstMacAddress;
        }
        private set
        {
            mLstMacAddress = value;
        }
    }

    #endregion

    #region Instância dos objetos da classe

    /// <summary>
    ///     Instância o objeto e obtem os valores do computador corrente e disponibiliza
    /// nas propriedades.
    /// </summary>
    public Dns()
    {
        GetLocalInformation();
    }

    #endregion

    #region Métodos privados

    /// <summary>
    ///     Carrega as informações do computador corrente nas variáveis globais.
    /// </summary>
    private void GetLocalInformation()
    {
        _MachineName = Environment.MachineName;
        _MachineDomainName = Environment.UserDomainName;
        _UserName = Environment.UserName;
        _ActiveMacAddress = GetLocalMacAddress();
        _IntranetIpAddress = GetLocalIpAddress();
        _OsVersion = Environment.OSVersion.VersionString;
        _Is64Bits = Environment.Is64BitOperatingSystem;

        _AllMacAddressInstalled = GetAllLocalMacAddress();
    }

    /// <summary>
    ///     Obtém o Mac Address do computador atual
    /// </summary>
    /// <returns></returns>
    private String GetLocalMacAddress()
    {
        try
        {
            return (from nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
        }
        catch { return ""; };
    }

    /// <summary>
    ///     Método responsável por obter todos os MacAddress da máquina corrente
    /// e adicionar em uma lista.
    /// </summary>
    /// <returns>Lista de string.</returns>
    private List<String> GetAllLocalMacAddress()
    {
        List<String> lstRet = new List<string>();

        try
        {
            foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                //Pego todas as interfaces ativas, pode ser que por algum motivo, uma ficou offline.
                //if (n.OperationalStatus == OperationalStatus.Up)
                //    lstRet.Add(n.GetPhysicalAddress().ToString());

                lstRet.Add(n.GetPhysicalAddress().ToString());
            }
        }
        catch { lstRet = null; }

        return lstRet;
    }

    #region Obsoleto

    //protected void GetAllIpsLocalComputer()
    //{
    //    foreach (System.Net.NetworkInformation.NetworkInterface ni in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
    //    {
    //        Console.WriteLine(ni.Name);
    //        Console.WriteLine("Operational? {0}", ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);
    //        Console.WriteLine("MAC: {0}", ni.GetPhysicalAddress());
    //        Console.WriteLine("Gateways:");

    //        foreach (System.Net.NetworkInformation.GatewayIPAddressInformation gipi in ni.GetIPProperties().GatewayAddresses)
    //        {
    //            Console.WriteLine("\t{0}", gipi.Address);
    //        }

    //        Console.WriteLine("IP Addresses:");

    //        foreach (System.Net.NetworkInformation.UnicastIPAddressInformation uipi in ni.GetIPProperties().UnicastAddresses)
    //        {
    //            Console.WriteLine("\t{0} / {1}", uipi.Address, uipi.IPv4Mask);
    //        }
    //        Console.WriteLine();
    //    }
    //}

    #endregion

    #endregion

    #region Métodos públicos

    /// <summary>
    ///     Verifica se uma determinada porta está sendo utilizada.
    /// </summary>
    /// <param name="Port">Número da porta.</param>
    /// <returns>True caso esteja sendo utilizada, false para o contrário.</returns>
    public Boolean IsPortBusy(int Port)
    {
        IPGlobalProperties ipGP = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] endpoints = ipGP.GetActiveTcpListeners();
        if (endpoints == null || endpoints.Length == 0) return false;
        for (int i = 0; i < endpoints.Length; i++)
            if (endpoints[i].Port == Port)
                return true;
        return false;
    }

    /// <summary>
    ///     Obtém o IP Address local da máquina
    /// </summary>
    /// <returns>String contendo o IP atual.</returns>
    public String GetLocalIpAddress()
    {
        try
        {
            //Esse cara retorna o IP do gateway padrão.
            //return (from nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
            //        where nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up
            //        from nini in nic.GetIPProperties().GatewayAddresses
            //        select nini.Address.ToString()).FirstOrDefault();

            IPHostEntry host;
            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "127.0.0.1";

        }
        catch { return "127.0.0.1"; };
    }

    /// <summary>
    ///     Método responsável por verificar se o MacAddress existe no computador correte.
    /// </summary>
    /// <param name="MacAddress">MacAddress a ser verificado</param>
    /// <returns>True caso exista, false para o contrário (ou em caso de não conseguir 
    /// obter/identificar macaddress ativos no host atual).</returns>
    public Boolean ExistMacAddressInHost(String MacAddress)
    {
        try
        {
            if (mLstMacAddress != null && mLstMacAddress.Count > 0)
            {
                return mLstMacAddress.Where(m => m.ToUpper().Trim() == MacAddress.ToUpper().Trim()).Count() > 0;
            }
            else return false;
        }
        catch { return false; }
    }

    #endregion

    #region Métodos estáticos (privados)

    /// <summary>
    /// Gets the IP address of the current PC
    /// </summary>
    /// <returns></returns>
    private static IPAddress GetIPAddress()
    {
        String strHostName = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        foreach (IPAddress ip in addr)
        {
            if (!ip.IsIPv6LinkLocal)
            {
                return (ip);
            }
        }
        return addr.Length > 0 ? addr[0] : null;
    }

    /// <summary>
    /// Gets the MAC address of the current PC.
    /// </summary>
    /// <returns></returns>
    private static PhysicalAddress GetMacAddress()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            // Only consider Ethernet network interfaces
            if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                nic.OperationalStatus == OperationalStatus.Up)
            {
                return nic.GetPhysicalAddress();
            }
        }
        return null;
    }

    /// <summary>
    /// Returns true if the specified IP address is a multicast address
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    private static bool IsMulticast(IPAddress ip)
    {
        bool result = true;
        if (!ip.IsIPv6Multicast)
        {
            byte highIP = ip.GetAddressBytes()[0];
            if (highIP < 224 || highIP > 239)
            {
                result = false;
            }
        }
        return result;
    }

    #endregion

    #region Métodos estáticos (públicos)

    /// <summary>
    ///     Método que obtém o nome da máquina através do IP do mesmo, informado como
    /// parâmetro.
    /// </summary>
    /// <param name="IpAddress">IP do computador</param>
    /// <returns>String contendo o nome do computador juntamente com o domínio (se houver)</returns>
    public static String GetMachineNameFromIPAddress(String IpAddress)
    {
        string machineName = string.Empty;
        try
        {
            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(IpAddress);
            machineName = hostEntry.HostName;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter o nome do computador pelo IP. " + ex.Message, ex);
        }
        return machineName;
    }

    /// <summary>
    ///     Método que obtém o IP do computador através do seu nome, informado como
    /// parâmetro.
    /// </summary>
    /// <param name="MachineName">Nome da máquina</param>
    /// <returns>IP do computador/máquina</returns>
    public static String GetIPAddressFromMachineName(String MachineName)
    {
        string ipAddress = string.Empty;
        try
        {
            System.Net.IPAddress ip = System.Net.Dns.GetHostEntry(MachineName).AddressList.Where(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First();
            ipAddress = ip.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter o IP do computador pelo nome da máquina. " + ex.Message, ex);
        }
        return ipAddress;
    }

    /// <summary>
    ///     Método responsável por obter todos os endereços IP's e MAC's
    /// da rede LAN que está conectado.
    /// </summary>
    /// <remarks>
    /// 1) This table is not updated often - it can take some human-scale time 
    ///    to notice that a device has dropped off the network, or a new device
    ///    has connected.
    /// 2) This discards non-local devices if they are found - these are multicast
    ///    and can be discarded by IP address range.
    /// </remarks>
    /// <returns></returns>
    public static Dictionary<IPAddress, PhysicalAddress> GetAllDevicesOnLAN()
    {
        Dictionary<IPAddress, PhysicalAddress> all = new Dictionary<IPAddress, PhysicalAddress>();
        // Add this PC to the list...
        all.Add(GetIPAddress(), GetMacAddress());
        int spaceForNetTable = 0;
        // Get the space needed
        // We do that by requesting the table, but not giving any space at all.
        // The return value will tell us how much we actually need.
        GetIpNetTable(IntPtr.Zero, ref spaceForNetTable, false);
        // Allocate the space
        // We use a try-finally block to ensure release.
        IntPtr rawTable = IntPtr.Zero;
        try
        {
            rawTable = Marshal.AllocCoTaskMem(spaceForNetTable);
            // Get the actual data
            int errorCode = GetIpNetTable(rawTable, ref spaceForNetTable, false);
            if (errorCode != 0)
            {
                // Failed for some reason - can do no more here.
                throw new Exception(string.Format("Unable to retrieve network table. Error code {0}", errorCode));
            }
            // Get the rows count
            int rowsCount = Marshal.ReadInt32(rawTable);
            IntPtr currentBuffer = new IntPtr(rawTable.ToInt64() + Marshal.SizeOf(typeof(Int32)));
            // Convert the raw table to individual entries
            MIB_IPNETROW[] rows = new MIB_IPNETROW[rowsCount];
            for (int index = 0; index < rowsCount; index++)
            {
                rows[index] = (MIB_IPNETROW)Marshal.PtrToStructure(new IntPtr(currentBuffer.ToInt64() +
                                            (index * Marshal.SizeOf(typeof(MIB_IPNETROW)))
                                           ),
                                            typeof(MIB_IPNETROW));
            }
            // Define the dummy entries list (we can discard these)
            PhysicalAddress virtualMAC = new PhysicalAddress(new byte[] { 0, 0, 0, 0, 0, 0 });
            PhysicalAddress broadcastMAC = new PhysicalAddress(new byte[] { 255, 255, 255, 255, 255, 255 });
            foreach (MIB_IPNETROW row in rows)
            {
                IPAddress ip = new IPAddress(BitConverter.GetBytes(row.dwAddr));
                byte[] rawMAC = new byte[] { row.mac0, row.mac1, row.mac2, row.mac3, row.mac4, row.mac5 };
                PhysicalAddress pa = new PhysicalAddress(rawMAC);
                if (!pa.Equals(virtualMAC) && !pa.Equals(broadcastMAC) && !IsMulticast(ip))
                {
                    //Console.WriteLine("IP: {0}\t\tMAC: {1}", ip.ToString(), pa.ToString());
                    if (!all.ContainsKey(ip))
                    {
                        all.Add(ip, pa);
                    }
                }
            }
        }
        finally
        {
            // Release the memory.
            Marshal.FreeCoTaskMem(rawTable);
        }
        return all;
    }

    #endregion
}