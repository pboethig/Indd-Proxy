using System;
using System.Linq;
using CommandLine;

namespace Indd.Service.Commands {

    /// <summary>
    /// Options to generate proxy
    /// </summary>

    class OpenDocument : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public OpenDocument(dynamic commandRequest) : base ((object)commandRequest)
        {
           
        }

        public override bool execute()
        {
            throw new NotImplementedException();
        }

        public override bool notify()
        {
            throw new NotImplementedException();
        }

        public override bool saveResponse()
        {
            throw new NotImplementedException();
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