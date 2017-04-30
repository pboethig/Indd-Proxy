using System;

namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class ExportPDF : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public ExportPDF(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        public string exportFilePath;

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
                version = this.version
            };
            
            try
            {
                Open DocumentOpenCommand = new Open(DocumentOpenCommandRequest);

                DocumentOpenCommand.processSequence();

                this.document = DocumentOpenCommand.document;

                this.buildExportFilePath();

                DocumentOpenCommand.document.Export(InDesignServer.idExportFormat.idPDFType, this.exportFilePath);
                
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Document.Export failed: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Build export filepath
        /// </summary>
        /// <returns></returns>
        public void buildExportFilePath()
        {
            this.exportFilePath = this.commandRequest.exportFolderPath + "/" + this.uuid + ".pdf";

            if (System.IO.File.Exists(this.exportFilePath))
            {
                System.IO.File.Delete(this.exportFilePath);  
            }
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