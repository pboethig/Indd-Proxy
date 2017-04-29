using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Indd.Service.Commands.Document;

namespace Indd.Service.Commands.Images {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class SetLinks : Abstract,  Contracts.ICommand
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
        public SetLinks(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            dynamic DocumentOpenCommandRequest = new { classname = "Document.Open", uuid = this.uuid, version = "1.0" };

            Open DocumentOpenCommand = new Open(DocumentOpenCommandRequest);

            DocumentOpenCommand.processSequence();

            this.document = DocumentOpenCommand.document;
            
            try
            {

            }
            catch (System.Exception ex)
            {
                throw new SystemException("Images.SetLinks: " + ex.Message);
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