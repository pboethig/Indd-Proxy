using System;

namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class ExportHTMLFX : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public ExportHTMLFX(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        public string exportFilePath { get; set; }

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        public string exportWebResourcesPath { get; set; }

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                this.openDocument();

                this.buildExportFilePath();

                this.document.Export(InDesignServer.idExportFormat.idHTMLFXL, this.exportFilePath);
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Document.Export to HTML FX failed: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Build export filepath
        /// </summary>
        /// <returns>void</returns>
        public void buildExportFilePath()
        {
            this.exportFilePath = this.commandRequest.exportFolderPath + "\\" + this.uuid + "\\document_"+ this.version+".html";

            this.exportWebResourcesPath = this.commandRequest.exportFolderPath + "\\" + this.uuid + "\\document_" + this.version + "-web-resources";

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
                System.IO.Directory.CreateDirectory(this.commandRequest.exportFolderPath);
            }

            return true;
        }
    }
}