using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edgecam_Manager_License
{
    /// <summary>
    ///     Contém uma lista dos módulos.
    /// </summary>
    public class LicenseModules
    {
        public Boolean Habilitado;
        public String NomeModulo;
        public String QtdeModulo;
        public API Api;
    }

    /// <summary>
    ///     Objeto que contém informações da API.
    /// </summary>
    public class API
    {
        public String APILogin;
        public String APIPassword;
    }
}