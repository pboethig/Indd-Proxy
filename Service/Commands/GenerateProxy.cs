using System;
using System.Linq;
using CommandLine;

namespace Indd.Service.Commands {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class GenerateProxy : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Validate Request
        /// </summary>
        /// <param name="args"></param>
        /// <returns>dynamic</returns>
        public dynamic validateRequest(dynamic request)
        {

            dynamic result = "";

            return result;
        }
    }
}