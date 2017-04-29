using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Indd.Service.Commands {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class SaveAndCloseDocument : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public SaveAndCloseDocument(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            dynamic openDocumentCommandRequest = new
            {
                classname = "OpenDocument",
                uuid = this.uuid,
                version = "1.0"
            };
            
            try
            {
                OpenDocument openDocumentCommand = new OpenDocument(openDocumentCommandRequest);

                openDocumentCommand.processSequence();

                openDocumentCommand.document.Save(this.documentPath, false,"saved", true);

                openDocumentCommand.document.Close();
            }
            catch (System.Exception ex)
            {
                Indd.Service.Log.Syslog.log("SaveAndCloseDocument failed: " + ex.Message);
            }

            return true;
        }
        
        public override bool notify()
        {
            return true;
        }

        public override bool saveResponse()
        {
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