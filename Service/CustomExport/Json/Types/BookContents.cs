/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class BookContents
    {
        public string Type = "BookContents";
        
        /// <summary>
        /// Point per inch
        /// </summary>
        public  List<BookContent>BookContentsList = new List<BookContent>();
        
        
        /// </summary>
        /// <param name="document"></param>
        public BookContents(InDesignServer.BookContents bookContents)
        {
            this.setDocuments(bookContents);    
        }
        
        /// <summary>
        /// Set CharacterStyles
        /// </summary>
        /// <param name="document"></param>
        void setDocuments(InDesignServer.BookContents bookContents)
        {
            foreach (InDesignServer.BookContent bookContent in bookContents)
            {
                this.BookContentsList.Add(new BookContent(bookContent));
            }
        }
    }
}