using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using Indd.Service.Log;

namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class Open : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public Open(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                
                
                this.document = this.application.Open(this.documentPath);

                return true;
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Document.Open: Cannot open document: + " + this.documentPath + " Message:" + ex.Message);
            }
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