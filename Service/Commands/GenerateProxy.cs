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
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public GenerateProxy(dynamic commandRequest) : base ((object)commandRequest)
        {
           
        }

        /// <summary>
        /// Validate Request
        /// </summary>
        /// <param name="args"></param>
        /// <returns>dynamic</returns>
        public override bool validateRequest() 
        {
            base.validateRequest();
            
            return true;
        }
    }
}