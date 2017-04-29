using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Indd.Helper.IO;
using Indd.Service.Commands.Document;
namespace Indd.Service.Commands.Images {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class RelinkAll : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// current document
        /// </summary>
        public InDesignServer.Document document;

        /// <summary>
        /// BasePath of linked assets
        /// </summary>
        public string basePath = "";

        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public RelinkAll(dynamic commandRequest) : base ((object)commandRequest){}

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
                    version = "1.0"
                };

                Open DocumentOpenCommand = new Open(DocumentOpenCommandRequest);

                DocumentOpenCommand.processSequence();

                this.document = DocumentOpenCommand.document;

                this.basePath =this.commandRequest.basePath;

                this.relink(this.document.AllGraphics, (string)this.commandRequest.basePath);
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Images.RelinkAll: " + ex.Message);
            }

            return true;
        }
        
        /// <summary>
        /// relink all graphics 
        /// </summary>
        /// <param name="allGrapics"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool relink(InDesignServer.Objects allGraphics, string basePath)
        {
            foreach (dynamic graphic in allGraphics)
            {
                InDesignServer.Link link = graphic.ItemLink;

                string path = link.FilePath;

                string fileName = this.basePath + "/" + link.Name;

                object scriptingFileSystemObject = ScripingFileSystemObject.getObject(fileName);

                link.Relink(scriptingFileSystemObject);

                link.Update();
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
            
            if (this.commandRequest.basePath==null)
            {
                throw new System.Exception("property basepath missing Jobticket: " + this.commandRequest.uuid);
            }

            string basePath = this.commandRequest.basePath;

            if (!System.IO.Directory.Exists(basePath))
            {
                throw new System.Exception("given basePath in Jobticket RelinkAll does not exist: " + this.commandRequest.basePath);
            }

            return true;
        }
    }
}