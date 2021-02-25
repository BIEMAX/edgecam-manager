/***********************************************************************************************************
 * 
 * 
 *                  WindowsServiceMgr - Classe com funções genéricas voltadas para os serviços do windows
 * 
 * 
 *      Developer:  Dionei Beilke dos Santos
 *      Function:   Classe com funções genéricas voltadas para os serviços do windows
 *      Version:    1.1
 *      Date:       28/09/2018, at 09:25 AM
 *      Note:       <None>
 *      History:    Update      - 28/09/2018 - 09:25 AM - Primeira versão concluída - V1.0 Lançada
 *                  Update      - 01/10/2018 - 12:47 AM - Adicionado o método 'IsServiceRunning' - V1.1 Lançada.
 * 
 * 
 * 
 *      Links de ajuda:
 *              https://stackoverflow.com/questions/34068907/how-can-install-a-windows-services-with-c-sharp-code-i-have-using-the-administra
 *              https://stackoverflow.com/questions/49789957/creating-a-new-windows-service-on-windows-form-button-click-event
 *              http://www.csharp-examples.net/restart-windows-service/
 *              http://www.csharp-examples.net/install-net-service/
 *              http://www.csharp-examples.net/windows-service-list/
 *              
 * 
 **********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public enum ServiceState
{
    Unknown = -1, // The state cannot be (has not been) retrieved.
    NotFound = 0, // The service is not known on the host server.
    Stopped = 1,
    StartPending = 2,
    StopPending = 3,
    Running = 4,
    ContinuePending = 5,
    PausePending = 6,
    Paused = 7
}

[Flags]
public enum ScmAccessRights
{
    Connect = 0x0001,
    CreateService = 0x0002,
    EnumerateService = 0x0004,
    Lock = 0x0008,
    QueryLockStatus = 0x0010,
    ModifyBootConfig = 0x0020,
    StandardRightsRequired = 0xF0000,
    AllAccess = (StandardRightsRequired | Connect | CreateService |
                 EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
}

[Flags]
public enum ServiceAccessRights
{
    QueryConfig = 0x1,
    ChangeConfig = 0x2,
    QueryStatus = 0x4,
    EnumerateDependants = 0x8,
    Start = 0x10,
    Stop = 0x20,
    PauseContinue = 0x40,
    Interrogate = 0x80,
    UserDefinedControl = 0x100,
    Delete = 0x00010000,
    StandardRightsRequired = 0xF0000,
    AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
                 QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
                 Interrogate | UserDefinedControl)
}

public enum ServiceBootFlag
{
    Start = 0x00000000,
    SystemStart = 0x00000001,
    AutoStart = 0x00000002,
    DemandStart = 0x00000003,
    Disabled = 0x00000004
}

public enum ServiceControl
{
    Stop = 0x00000001,
    Pause = 0x00000002,
    Continue = 0x00000003,
    Interrogate = 0x00000004,
    Shutdown = 0x00000005,
    ParamChange = 0x00000006,
    NetBindAdd = 0x00000007,
    NetBindRemove = 0x00000008,
    NetBindEnable = 0x00000009,
    NetBindDisable = 0x0000000A
}

public enum ServiceError
{
    Ignore = 0x00000000,
    Normal = 0x00000001,
    Severe = 0x00000002,
    Critical = 0x00000003
}

/// <summary>
///     Classe que dá permissões para iniciar um serviço, parar e até mesmo instalar um
/// determinado serviço.
/// </summary>
public static class WindowsServiceMgr
{
    private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
    private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;

    [StructLayout(LayoutKind.Sequential)]
    private class SERVICE_STATUS
    {
        public int dwServiceType = 0;
        public ServiceState dwCurrentState = 0;
        public int dwControlsAccepted = 0;
        public int dwWin32ExitCode = 0;
        public int dwServiceSpecificExitCode = 0;
        public int dwCheckPoint = 0;
        public int dwWaitHint = 0;
    }

    #region OpenSCManager
    [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
    static extern IntPtr OpenSCManager(string machineName, string databaseName, ScmAccessRights dwDesiredAccess);
    #endregion

    #region OpenService
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceAccessRights dwDesiredAccess);
    #endregion

    #region CreateService
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceAccessRights dwDesiredAccess, int dwServiceType, ServiceBootFlag dwStartType, ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);
    #endregion

    #region CloseServiceHandle
    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseServiceHandle(IntPtr hSCObject);
    #endregion

    #region QueryServiceStatus
    [DllImport("advapi32.dll")]
    private static extern int QueryServiceStatus(IntPtr hService, SERVICE_STATUS lpServiceStatus);
    #endregion

    #region DeleteService
    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool DeleteService(IntPtr hService);
    #endregion

    #region ControlService
    [DllImport("advapi32.dll")]
    private static extern int ControlService(IntPtr hService, ServiceControl dwControl, SERVICE_STATUS lpServiceStatus);
    #endregion

    #region StartService
    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);
    #endregion

    /// <summary>
    ///     Desinstala um determinado serviço do computador.
    /// </summary>
    /// <param name="serviceName">Nome do serviço (do windows)</param>
    public static void Uninstall(string serviceName)
    {
        IntPtr scm = OpenSCManager(ScmAccessRights.AllAccess);

        try
        {
            IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.AllAccess);
            if (service == IntPtr.Zero)
                throw new ApplicationException("Service not installed.");

            try
            {
                StopService(service);
                if (!DeleteService(service))
                    throw new ApplicationException("Could not delete service " + Marshal.GetLastWin32Error());
            }
            finally
            {
                CloseServiceHandle(service);
            }
        }
        finally
        {
            CloseServiceHandle(scm);
        }
    }

    /// <summary>
    ///     Verifica se o serviço está instalado.
    /// </summary>
    /// <param name="serviceName">Nome do serviço (do windows)</param>
    /// <returns>True para sim.</returns>
    public static bool ServiceIsInstalled(string serviceName)
    {
        IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

        try
        {
            IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus);

            if (service == IntPtr.Zero)
                return false;

            CloseServiceHandle(service);
            return true;
        }
        finally
        {
            CloseServiceHandle(scm);
        }
    }

    /// <summary>
    ///     Instala e inicia um serviço do windows
    /// </summary>
    /// <param name="serviceName">Nome do serviço</param>
    /// <param name="displayName">Nome da exibição do serviço</param>
    /// <param name="fileName">Caminho completo do serviço (seja ele DLL ou executável).</param>
    public static void InstallAndStart(string serviceName, string displayName, string fileName)
    {
        if (serviceName == string.Empty)
            throw new ArgumentNullException("Nome do serviço não pode ser vazio ou nulo");

        if (displayName == string.Empty)
            throw new ArgumentNullException("Nome de exibição do serviço não pode ser vazio ou nulo");

        if (!System.IO.File.Exists(fileName))
            throw new System.IO.FileNotFoundException("Não foi possível localizar o caminho do serviço.");

        IntPtr scm = OpenSCManager(ScmAccessRights.AllAccess);

        try
        {
            IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.AllAccess);

            if (service == IntPtr.Zero)
                service = CreateService(scm, serviceName, displayName, ServiceAccessRights.AllAccess, SERVICE_WIN32_OWN_PROCESS, ServiceBootFlag.AutoStart, ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);

            if (service == IntPtr.Zero)
                throw new ApplicationException("Failed to install service.");

            try
            {
                StartService(service);
            }
            finally
            {
                CloseServiceHandle(service);
            }
        }
        finally
        {
            CloseServiceHandle(scm);
        }
    }

    /// <summary>
    ///     Inicia um determinado serviço.
    /// </summary>
    /// <param name="serviceName">Nome do serviço (do windows)</param>
    public static void StartService(string serviceName)
    {
        IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

        try
        {
            IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus | ServiceAccessRights.Start);
            if (service == IntPtr.Zero)
                throw new ApplicationException("Could not open service.");

            try
            {
                StartService(service);
            }
            finally
            {
                CloseServiceHandle(service);
            }
        }
        finally
        {
            CloseServiceHandle(scm);
        }
    }

    /// <summary>
    ///     Para a execução de um determinado serviço.
    /// </summary>
    /// <param name="serviceName">Nome do serviço (do windows)</param>
    public static void StopService(string serviceName)
    {
        IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

        try
        {
            IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus | ServiceAccessRights.Stop);
            if (service == IntPtr.Zero)
                throw new ApplicationException("Could not open service.");

            try
            {
                StopService(service);
            }
            finally
            {
                CloseServiceHandle(service);
            }
        }
        finally
        {
            CloseServiceHandle(scm);
        }
    }

    /// <summary>
    ///     Inicia um determinado serviço.
    /// </summary>
    /// <param name="service">Nome do serviço (do windows)</param>
    private static void StartService(IntPtr service)
    {
        SERVICE_STATUS status = new SERVICE_STATUS();
        StartService(service, 0, 0);
        var changedStatus = WaitForServiceStatus(service, ServiceState.StartPending, ServiceState.Running);
        if (!changedStatus)
            throw new ApplicationException("Unable to start service");
    }

    /// <summary>
    ///     Para a execução de um determinado serviço.
    /// </summary>
    /// <param name="service">Número interno do serviço (do windows)</param>
    private static void StopService(IntPtr service)
    {
        SERVICE_STATUS status = new SERVICE_STATUS();
        ControlService(service, ServiceControl.Stop, status);
        var changedStatus = WaitForServiceStatus(service, ServiceState.StopPending, ServiceState.Stopped);
        if (!changedStatus)
            throw new ApplicationException("Unable to stop service");
    }

    /// <summary>
    ///     Método que obtém o estado atual de um determinado serviço.
    /// </summary>
    /// <param name="serviceName">Nome do serviço (do windows)</param>
    /// <returns>Estado atual do serviço (parado, em execução, aguardando, reiniciando, etc).</returns>
    public static ServiceState GetServiceStatus(string serviceName)
    {
        IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

        try
        {
            IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus);
            if (service == IntPtr.Zero)
                return ServiceState.NotFound;

            try
            {
                return GetServiceStatus(service);
            }
            finally
            {
                CloseServiceHandle(service);
            }
        }
        finally
        {
            CloseServiceHandle(scm);
        }
    }

    /// <summary>
    ///     Método que obtém o estado atual de um determinado serviço.
    /// </summary>
    /// <param name="service">Número interno do serviço (do windows)</param>
    /// <returns>Estado atual do serviço (parado, em execução, aguardando, reiniciando, etc).</returns>
    private static ServiceState GetServiceStatus(IntPtr service)
    {
        SERVICE_STATUS status = new SERVICE_STATUS();

        if (QueryServiceStatus(service, status) == 0)
            throw new ApplicationException("Failed to query service status.");

        return status.dwCurrentState;
    }

    /// <summary>
    ///     Método respoonsável por "aguardar" a resposta do estado atual do serviço.
    /// </summary>
    /// <param name="service">Número interno do serviço (do windows)</param>
    /// <param name="waitStatus"></param>
    /// <param name="desiredStatus"></param>
    /// <returns></returns>
    private static bool WaitForServiceStatus(IntPtr service, ServiceState waitStatus, ServiceState desiredStatus)
    {
        SERVICE_STATUS status = new SERVICE_STATUS();

        QueryServiceStatus(service, status);
        if (status.dwCurrentState == desiredStatus) return true;

        int dwStartTickCount = Environment.TickCount;
        int dwOldCheckPoint = status.dwCheckPoint;

        while (status.dwCurrentState == waitStatus)
        {
            // Do not wait longer than the wait hint. A good interval is
            // one tenth the wait hint, but no less than 1 second and no
            // more than 10 seconds.

            int dwWaitTime = status.dwWaitHint / 10;

            if (dwWaitTime < 1000) dwWaitTime = 1000;
            else if (dwWaitTime > 10000) dwWaitTime = 10000;

            Thread.Sleep(dwWaitTime);

            // Check the status again.

            if (QueryServiceStatus(service, status) == 0) break;

            if (status.dwCheckPoint > dwOldCheckPoint)
            {
                // The service is making progress.
                dwStartTickCount = Environment.TickCount;
                dwOldCheckPoint = status.dwCheckPoint;
            }
            else
            {
                if (Environment.TickCount - dwStartTickCount > status.dwWaitHint)
                {
                    // No progress made within the wait hint
                    break;
                }
            }
        }
        return (status.dwCurrentState == desiredStatus);
    }

    /// <summary>
    ///     Se conecta ao gerenciador de controle de serviços para realizar todas as ações.
    /// </summary>
    /// <param name="rights"></param>
    /// <returns></returns>
    private static IntPtr OpenSCManager(ScmAccessRights rights)
    {
        IntPtr scm = OpenSCManager(null, null, rights);
        if (scm == IntPtr.Zero)
        {
            if (rights == ScmAccessRights.AllAccess)
                throw new ApplicationException("Não foi possível se conectar ao gerenciador de controle de serviços. Verifique se está executando o depurador ou o sistema como modo administrador");
            else throw new ApplicationException("Não foi possível se conectar ao gerenciador de controle de serviços.");
        }
        return scm;
    }

    /// <summary>
    ///     Valida se o serviço está em execução.
    /// </summary>
    /// <param name="NomeServico">Nome do serviço do windows</param>
    /// <returns></returns>
    public static Boolean IsServiceRunning(String NomeServico)
    {
        ServiceController sc = new ServiceController(NomeServico);

        switch (sc.Status)
        {
            //case ServiceControllerStatus.Running:
            //    return "Running";
            //case ServiceControllerStatus.Stopped:
            //    return "Stopped";
            //case ServiceControllerStatus.Paused:
            //    return "Paused";
            //case ServiceControllerStatus.StopPending:
            //    return "Stopping";
            //case ServiceControllerStatus.StartPending:
            //    return "Starting";
            //default:
            //    return "Status Changing";
            case ServiceControllerStatus.Running: return true;
            default: return false;
        }
    }
}