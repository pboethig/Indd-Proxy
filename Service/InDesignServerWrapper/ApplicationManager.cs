﻿using System;
using InDesignServer;
using Indd.Service.Log;

namespace Indd.Service.IndesignServerWrapper
{
    /// <summary>
    /// Contains application methods
    /// </summary>
    public class ApplicationMananger
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
                return (Application)Activator.CreateInstance(type); 
            }
            catch (System.Exception ex)
            {
                Syslog.log("Indd-Applicationmanager:  could not get IndesignServer instance: " + ex.Message);

                Environment.Exit(0);
            }

            return null;
        }
    }
}
