/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;
    using Indd.Service.IndesignServerWrapper;

    class BookContent
    {
        public string Type = "BookContent";
        
        /// <summary>
        /// Id
        /// </summary>
        public int Id;

        /// <summary>
        /// Index
        /// </summary>
        public int Index;

        /// <summary>
        /// Label
        /// </summary>
        public string Label;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName;

        /// <summary>
        /// FilePath
        /// </summary>
        public string FilePath;

        /// <summary>
        /// Size
        /// </summary>
        public int Size;

        /// <summary>
        /// Status
        /// </summary>
        public dynamic Status;

        /// <summary>
        /// Document
        /// </summary>
        public Document Document;

        /// </summary>
        /// <param name="document"></param>
        public BookContent(InDesignServer.BookContent bookContent)
        {
            this.Id = bookContent.Id;

            this.FullName = bookContent.FullName;

            this.setDocument(bookContent);

            this.FilePath = bookContent.FilePath;

            this.Index = bookContent.Index;

            this.Label = bookContent.Label;

            this.Size = bookContent.Size;

            this.Status = bookContent.Status;

            this.setDocument(bookContent);
            
        }
        
        /// <summary>
        /// Set CharacterStyles
        /// </summary>
        /// <param name="document"></param>
        void setDocument(InDesignServer.BookContent bookContent)
        {
            InDesignServer.Application application = (new ApplicationMananger()).createInstance();

            try
            {
                InDesignServer.Document document = application.Open(this.FullName);

                this.Document = new Document(application.Open(this.FullName));

                document.Close();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Indd.Service.CustomExport.Json.Types.setDocument failed for document: " + this.FullName);
            }
            
        }
    }
}