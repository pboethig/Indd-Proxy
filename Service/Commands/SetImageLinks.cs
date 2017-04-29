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
    class SetImageLinks : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// current document
        /// </summary>
        public InDesignServer.Document document;

        /// <summary>
        /// BasePath of linked assets
        /// </summary>
        public Dictionary<string, string> frameToImageLinkMap;

        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public SetImageLinks(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            dynamic openDocumentCommandRequest = new { classname = "OpenDocument", uuid = this.uuid, version = "1.0" };

            Indd.Service.Commands.OpenDocument openDocumentCommand = new Indd.Service.Commands.OpenDocument(openDocumentCommandRequest);

            openDocumentCommand.processSequence();

            this.document = openDocumentCommand.document;
            
            try
            {
            }
            catch (System.Exception ex)
            {
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
            
            if (this.commandRequest.frameToImageLinkMap==null)
            {
                throw new System.Exception("property frameToImageLinkMap missing Jobticket: " + this.commandRequest.uuid);
            }
            
            return true;
        }
    }
}