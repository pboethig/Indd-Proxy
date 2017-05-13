using System;

namespace Indd.Service.Commands.Book {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class ExportJSON : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public ExportJSON(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        public string exportFilePath { get; set; }

        /// <summary>
        /// Json representation of book
        /// </summary>
        public string json { get; set; }

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                this.openBook();

                this.buildExportFilePath();
                
                Indd.Service.CustomExport.Json.Book jsonBook = new Indd.Service.CustomExport.Json.Book(book);
                
                this.json = jsonBook.toJson(this.exportFilePath);

                book.Close();
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Book.Export to JSON failed: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Build export filepath
        /// </summary>
        /// <returns>void</returns>
        public void buildExportFilePath()
        {
            this.exportFilePath = this.commandRequest.exportFolderPath + "\\" + this.uuid + "\\book_"+ this.version+".json";

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