using System;
using System.Collections.Generic;

namespace Indd.Service.Commands.BookContents {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class Add : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public Add(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Contains the exported filepath
        /// </summary>
        public List<dynamic> documentUuids { get; set; }

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {

                this.openBook();
                
                string documentToAddFilePath = "";
                
                foreach (dynamic documentUuid in this.commandRequest.documentUuids)
                {
                    documentToAddFilePath = this.buildDocumentPath((string)documentUuid.uuid, (string)documentUuid.version, (string)documentUuid.extension);
                    
                    foreach (InDesignServer.BookContent bookContent in this.book.BookContents)
                    {
                        if (bookContent.FullName == documentToAddFilePath)
                        {
                            bookContent.Delete();
                        }
                    }
                    
                    if (!System.IO.File.Exists(documentToAddFilePath))
                    {
                        throw new System.Exception("filePath: " + documentToAddFilePath + " not found.");
                    }

                    this.book.BookContents.Add(documentToAddFilePath);
                }

                this.book.Save(this.documentPath);

                this.book.Close();
                
            }
            catch (System.Exception ex)
            {
                throw new SystemException("BookContents.Add failed: " + ex.Message);
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

            if (this.commandRequest.documentUuids == null)
            {
                throw new SystemException("Property documentUuids missing in payload"); 
            }
            
            return true;
        }
    }
}