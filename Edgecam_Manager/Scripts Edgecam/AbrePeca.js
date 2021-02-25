/*

            SKA Edgecam - EdgeCAM Manager

    Developer:          Dionei Beilke dos Santos
    Customer:           SKA
    Edgecam Version:    2016 R1 or Later
    Version:            1.0
    Revision:           Dionei Beilke dos Santos
    Updates:            <None>

*/

/// <reference path="c:\program files\vero software\edgecam 2016 r2\cam\PCI\pci-vsdoc.js" />

//Use this line for debug using Visual Studio <Any Version>
//debugger;


// Iniciando comando:- Novo Arquivo
var cmd1 = InitCommand(9, 0);
ClearMods(cmd1);
// Configurar campo 'Forçar Reinício'
SetModifier(cmd1, 339, "<Yes>");
var gdh1 = InitDigInfo();
var cmdret = ExecCommand(cmd1, gdh1);
FreeDigInfo(gdh1);


// Iniciando comando:- Arquivo Parasolid
var cmd1 = InitCommand(3, 5);
ClearMods(cmd1);
// Configurar campo 'Nome^Procurar...'
SetModifier(cmd1, 14, "@CAMINHOPECA@");
var gdh1 = InitDigInfo();
var cmdret = ExecCommand(cmd1, gdh1);
FreeDigInfo(gdh1);