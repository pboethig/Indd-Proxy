using System;
using InDesignServer;
using Indd.Service.Log;

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

            try
            {
                return (Application)Activator.CreateInstance(type, true); 
            }
            catch (System.Exception ex)
            {
                Syslog.log("could not get IndesignServer instance: " + ex.Message);

                Environment.Exit(0);
            }

            return null;
        }
    }
}
