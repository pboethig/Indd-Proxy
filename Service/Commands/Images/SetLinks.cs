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
    class SetLinks : Indd.Service.Commands.Images.Base,  Contracts.ICommand
    {
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
            try
            {

                dynamic DocumentOpenCommandRequest = new
                {
                    classname = "Document.Open",
                    uuid = this.uuid,
                    version = this.version
                };

                Open DocumentOpenCommand = new Open(DocumentOpenCommandRequest);

                DocumentOpenCommand.processSequence();

                this.document = DocumentOpenCommand.document;

                string basePath, fileName;

                int objectId;

                bool result;

                foreach (dynamic item in commandRequest.objectToImageLinkMap)
                {
                    basePath = (string)item.basePath;

                    fileName = (string)item.imageId + "." + (string)item.type;

                    objectId=(int)item.objectId;

                    result = base.relink(this.document, basePath, fileName, objectId);

                    if (!result)
                    {
                        throw new SystemException("Object with id: " + objectId + " not relinkable for this document"); 
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Images.SetLinks: " + ex.Message);
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
            
            if (this.commandRequest.objectToImageLinkMap==null)
            {
                throw new System.Exception("property objectToImageLinkMap missing Jobticket: " + this.commandRequest.uuid);
            }
            
            return true;
        }
    }
}