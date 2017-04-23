using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InDesignServer;


namespace Indd.Service.IndesignServerWrapper
{
    /// <summary>
    /// Contains application methods
    /// </summary>
    class ApplicationMananger
    {
        /// <summary>
        ///  Creates Instance of IndesignServer application
        /// </summary>
        /// <returns></returns>
        public InDesignServer.Application createInstance()
        {
            Type type = Type.GetTypeFromProgID("InDesignServer.Application");

            Application app = (Application)Activator.CreateInstance(type, true);

            return app;
        }
    }
}
