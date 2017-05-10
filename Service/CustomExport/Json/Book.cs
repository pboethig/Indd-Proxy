using System.Collections.Generic;
using JsonPage = Indd.Service.CustomExport.Json.Types.Page;
using JsonFrame = Indd.Service.CustomExport.Json.Types.TextFrame;

namespace Indd.Service.CustomExport.Json
{
    class Book:Abstract
    {
        /// <summary>
        /// Document to export
        /// </summary>
        public InDesignServer.Book book;

        /// <summary>
        /// jsonBook
        /// </summary>
        public Indd.Service.CustomExport.Json.Types.Book jsonBook;
        
        /// <summary>
        /// Stores exceptions during proxy generation
        /// </summary>
        public List<System.Exception> exportExceptions = new List<System.Exception>();

        /// <summary>
        /// Stores document
        /// </summary>
        /// <param name="document"></param>
        public Book(InDesignServer.Book _book)
        {
            try
            {
                this.book = _book;
                
                this.buildDocumentType();
            }
            catch (System.Exception ex)
            {
                exportExceptions.Add(ex);
            }
        }
        
        /// <summary>
        /// Sets jsonBook
        /// </summary>
        public void buildDocumentType()
        {
            this.jsonBook = new Indd.Service.CustomExport.Json.Types.Book(book);
        }
        
        /// <summary>
        /// converts to json
        /// </summary>
        /// <returns></returns>
        public string toJson(string filePath="")
        {
            string json = Indd.Helper.Json.Convert.toJson(this.jsonBook);

            if (filePath.Length > 0)
            {

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                System.IO.File.WriteAllText(filePath, json);
            }

            return json;
        }
    }
}
