using System;
using System.Collections.Generic;
namespace Indd.Service.Commands.Book {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class ExportJPG : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public ExportJPG(dynamic commandRequest) : base ((object)commandRequest){}
        
        /// <summary>
        /// contains books documentpages as path to thumbnails
        /// </summary>
        public List<string> pageThumbnailPaths { get; set; }

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            this.pageThumbnailPaths = new List<string>();

            try
            {
                this.openBook();

                this.setPageThumbnailPaths(this.book.BookContents);

                this.book.Close();
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Book.Export to JPG failed: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Set CharacterStyles
        /// </summary>
        /// <param name="document"></param>
        void setPageThumbnailPaths(InDesignServer.BookContents bookContents)
        {
            foreach (InDesignServer.BookContent bookContent in bookContents)
            {
                dynamic ExportCommandRequest = new
                {
                    classname = "Document.ExportJPG",
                    uuid = this.uuid,
                    version = bookContent.Name.Replace(".indd",""),
                    ticketId = this.ticketId,
                    extension = "indd",
                    documentFolderPath = this.documentFolderPath,
                    exportFolderPath= (string)this.commandRequest.exportFolderPath
                };

                Indd.Service.Commands.Document.ExportJPG exportCommand = new Indd.Service.Commands.Document.ExportJPG(ExportCommandRequest);

                exportCommand.processSequence();

                foreach(string pageThumbnailPath in exportCommand.pageThumbnailPaths)
                {
                    this.pageThumbnailPaths.Add((string)pageThumbnailPath);
                }

                var test = "";
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
                throw new SystemException("Property exportFolderPath missing in payload"); 
            }

            if (!System.IO.Directory.Exists((string)this.commandRequest.exportFolderPath))
            {
                System.IO.Directory.CreateDirectory(this.commandRequest.exportFolderPath);
            }

            return true;
        }
    }
}