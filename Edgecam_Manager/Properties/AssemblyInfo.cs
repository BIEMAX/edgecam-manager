using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Edgecam Manager")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Edgecam Manager")]
[assembly: AssemblyCopyright("Kreitech Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

/*
 *  Dionei Beilke dos Santos
 *  Developer at SKA
 *  01/08/2019, at 06:19 AM GMT - 3
 *  Implemented this internal control to other assemblies.
 */
[assembly: InternalsVisibleTo("Edgecam_Manager")]
[assembly: InternalsVisibleTo("Edgecam_Manager_Arquive")]
[assembly: InternalsVisibleTo("Edgecam_Manager_DataSheetDev")]
[assembly: InternalsVisibleTo("Edgecam_Manager_MacroDev")]
[assembly: InternalsVisibleTo("Edgecam_Manager_Notifications")]
[assembly: InternalsVisibleTo("Edgecam_Manager_PackAndGo")]
[assembly: InternalsVisibleTo("Edgecam_Manager_WebService")]
[assembly: InternalsVisibleTo("Edgecam_Manager_XmlIntegrator")]
[assembly: InternalsVisibleTo("EdgecamManagerPluginInterface")]

[assembly: InternalsVisibleTo("Edgecam_Manager_DevTasks")]
[assembly: InternalsVisibleTo("TestClass")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("2784ef93-32d7-404d-a1c3-008f921396e6")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2020.10.0.0")]
[assembly: AssemblyFileVersion("2020.10.0.0")]
