/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Book
    {
        public string Type = "Book";
        
        /// <summary>
        /// Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Fullname
        /// </summary>
        public string FullName;

        /// <summary>
        /// Label
        /// </summary>
        public string Label;
        
        /// <summary>
        /// Index
        /// </summary>
        public int Index;

        /// <summary>
        /// Horizontal MessurementUnits
        /// </summary>
        public bool Modified;

        /// <summary>
        /// Saved
        /// </summary>
        public bool Saved;

        /// <summary>
        /// BookContents
        /// </summary>
        public  BookContents BookContents;
        
        /// </summary>
        /// <param name="document"></param>
        public Book(InDesignServer.Book book)
        {
            this.Name = book.Name;
            
            this.Label = book.Label;

            this.Index = book.Index;

            this.FullName = book.FullName;
            
            this.Modified = book.Modified;

            this.Saved = book.Saved;
        
            this.BookContents = new BookContents(book.BookContents); 
        }
      

    }
}