using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Indd.Helper.IO;
using Indd.Service.Commands.Document;

namespace Indd.Service.Commands.TextFrames
{
    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class SetContents : Indd.Service.Commands.Images.Base, Contracts.ICommand
    {
        /// <summary>
        /// Ids of used frame
        /// </summary>
        public dynamic frameIdToContentsMap;
        
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public SetContents(dynamic commandRequest) : base((object)commandRequest) { }

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                dynamic DocumentOpenCommandRequest = new
                {
                    classname = "Document.Open",
                    uuid = this.uuid,
                    version = this.version,
                    ticketId = this.commandRequest.ticketId
                };

                Open DocumentOpenCommand = new Open(DocumentOpenCommandRequest);

                DocumentOpenCommand.processSequence();

                this.document = DocumentOpenCommand.document;

                this.frameIdToContentsMap = this.commandRequest.frameIdToContentsMap;

                foreach (InDesignServer.TextFrame frame in this.document.TextFrames)
                {
                    foreach (dynamic item in this.frameIdToContentsMap)
                    {
                        if (frame.Id == (int)item.frameId)
                        {
                            frame.Contents =(string) item.contents;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new SystemException("TextFrames.SetContents: " + ex.Message);
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

            if (this.commandRequest.frameIdToContentsMap == null)
            {
                throw new System.Exception("property frameIdToContentsMap missing Jobticket: " + this.commandRequest.ticketId);
            }
            
            return true;
        }
    }
}