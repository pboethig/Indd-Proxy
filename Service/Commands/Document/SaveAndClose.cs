using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class SaveAndClose : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public SaveAndClose(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            dynamic DocumentOpenCommandRequest = new
            {
                classname = "Document.Open",
                uuid = this.uuid,
                version = this.version,
                ticketId = this.commandRequest.ticketId,
                extension = this.extension
            };
            
            try
            {
                Open DocumentOpenCommand = new Open(DocumentOpenCommandRequest);

                DocumentOpenCommand.processSequence();

                DocumentOpenCommand.document.Save(this.documentPath, false,"saved", true);

                DocumentOpenCommand.document.Close();
            }
            catch (System.Exception ex)
            {
                string innerExceptionMessage ="";

                if (ex.InnerException != null)
                {
                    innerExceptionMessage ="Inner Exception: " + ex.InnerException.Message;
                }
                
                throw new SystemException("Document.SaveAndClose failed: " + ex.Message + innerExceptionMessage);
            }

            return true;
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