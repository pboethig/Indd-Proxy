using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Indd.Helper.IO;
using Indd.Service.Commands.Document;

namespace Indd.Service.Commands.Graphics {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class RelinkAll : Indd.Service.Commands.Images.Base,   Contracts.ICommand
    {
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
                this.openDocument();

                this.basePath =this.commandRequest.basePath;
                
                this.relink(this.document, (string)this.commandRequest.basePath);
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Images.RelinkAll: " + ex.Message);
            }

            return true;
        }
    
        /// <summary>
        /// Relinks all graphics
        /// </summary>
        /// <param name="allGraphics"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public bool relink(InDesignServer.Document document, string basePath)
        {
            foreach (dynamic graphic in document.AllGraphics)
            {
                InDesignServer.Link link = graphic.ItemLink;

                string path = link.FilePath;

                string fileName = this.basePath + "/" + link.Name;
                
                if (!System.IO.File.Exists(fileName))
                {
                    throw new System.IO.FileNotFoundException("LinkPath not found: " + fileName);
                }
                
                object scriptingFileSystemObject = ScripingFileSystemObject.getObject(fileName);
                
                link.Relink(scriptingFileSystemObject);
                
                link.Update();
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