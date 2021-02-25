using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Reflection;

namespace Edgecam_Manager_AutoUpdate
{
    public interface IAutoUpdatable
    {
        /// <summary>
        ///     Contém o nome da aplicação.
        /// </summary>
        String ApplicationName      { get; }
        /// <summary>
        ///     Contém o ID da aplicação (nesse caso, aconselha-se a utilizar o mesmo nome da aplicação).
        /// </summary>
        String ApplicationID        { get; }
        /// <summary>
        ///     Contém a versão da aplicação.
        /// </summary>
        String ApplicationVersion   { get; }
        /// <summary>
        ///     Contém o objeto do assembly (assembly em execução).
        /// </summary>
        Assembly ApplicationAssembly { get; }
        /// <summary>
        ///     Contém o ícone da aplicação.
        /// </summary>
        Icon ApplicationIcon        { get; }
        /// <summary>
        ///     Localização do arquivo 'update.xml' no servidor.
        /// </summary>
        Uri UpdateXmlLocation       { get; }
        /// <summary>
        ///     O contexto do programa.
        ///     Para formulários do windows, utilizar o 'this'.
        ///     Console apps, referência a library 'System.Windows.Forms' e retorne nulo.
        /// </summary>
        Form Context { get; }
    }
}