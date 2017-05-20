﻿using System;

namespace Indd.Service.Commands.Book {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class ExportPDF : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public ExportPDF(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        public string exportFilePath { get; set; }
        
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

                this.book.Export(InDesignServer.idExportFormat.idPDFType, this.exportFilePath);

            }
            catch (System.Exception ex)
            {
                throw new SystemException("Book.Export failed: " + ex.Message);
            }

            this.book.Close();

            return true;
        }

        /// <summary>
        /// Build export filepath
        /// </summary>
        /// <returns></returns>
        public void buildExportFilePath()
        {
            string folderPath = (string)this.commandRequest.exportFolderPath + "\\" + this.uuid;

            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            this.exportFilePath = folderPath + "\\book_" + this.version + ".pdf";

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