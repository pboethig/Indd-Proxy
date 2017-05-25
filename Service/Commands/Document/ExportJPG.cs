using System;
using System.Collections.Generic;

namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class ExportJPG : Abstract, Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public ExportJPG(dynamic commandRequest) : base((object)commandRequest) { }

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        private string exportFilePath;

        /// <summary>
        /// List of paths to thumbnails
        /// </summary>
        public List<string> pageThumbnailPaths {get;set;}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                this.openDocument();

                this.pageThumbnailPaths = new List<string>();

                foreach (InDesignServer.Page page in this.document.Pages)
                {
                    if (page.AppliedSection.Name != "")
                    {
                        page.AppliedSection.Name = "";
                    }
                    
                    this.application.JPEGExportPreferences.JPEGExportRange = InDesignServer.idExportRangeOrAllPages.idExportRange;
                    
                    string pageName = page.Name;

                    this.application.JPEGExportPreferences.PageString = pageName;

                    this.buildExportFilePath(pageName.Replace(":","_"));

                    this.pageThumbnailPaths.Add(this.exportFilePath);

                    this.document.Export(InDesignServer.idExportFormat.idJPG, this.exportFilePath);
                }
                 
                this.document.Close();
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Document.Export to JPG failed: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Build export filepath
        /// </summary>
        /// <returns></returns>
        public void buildExportFilePath(string fileName)
        {
            if (!System.IO.Directory.Exists((string)this.commandRequest.exportFolderPath))
            {
                System.IO.Directory.CreateDirectory((string)this.commandRequest.exportFolderPath); 
            }

            string uuidFolder =@""+ (string)this.commandRequest.exportFolderPath + "\\" + this.uuid;

            if (!System.IO.Directory.Exists(uuidFolder))
            {
                System.IO.Directory.CreateDirectory(uuidFolder);
            }
            
            this.exportFilePath = uuidFolder + "\\"+ this.version+"_"+fileName+".jpg";

            if (System.IO.File.Exists(this.exportFilePath))
            {
                System.IO.File.Delete(this.exportFilePath);  
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

            if (this.commandRequest.exportFolderPath == null)
            {
                throw new SystemException("Property exportFolder missing in payload"); 
            }

            if (!System.IO.Directory.Exists((string)this.commandRequest.exportFolderPath))
            {
                System.IO.Directory.CreateDirectory((string)this.commandRequest.exportFolderPath);
            }

            return true;
        }
    }
}