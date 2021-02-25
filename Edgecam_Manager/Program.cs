using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edgecam_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Environment.GetCommandLineArgs().Count() > 1)
            {
                String[] args = Environment.GetCommandLineArgs();

                Objects.LoadConfigAPI("X154812A85SD4DSDS5A1A1S8A", "S31X8A8E12385532SDI;/SP43WED");

                switch (args[1].ToString().ToUpper().Trim())
                {
                    case "ORC_ADVANCED": Application.Run(new FrmOrcamentos_NewDet()); break;
                    case "ORC_EXPRESS": Application.Run(new FrmOrcamentos_NewSim()); break;
                    default: Application.Run(new FrmLogin()); break;
                }
            }
            else Application.Run(new FrmLogin()); 
        }
    }
}