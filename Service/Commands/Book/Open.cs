using System;
using Indd.Service.IndesignServerWrapper;


namespace Indd.Service.Commands.Book {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class Open : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public Open(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                if (this.application == null)
                {
                    this.application = (new ApplicationMananger()).createInstance();
                }

                foreach (InDesignServer.Book _book in this.application.Books)
                {
                    if (_book.Name ==  this.version + "." + this.extension)
                    {
                        this.book =_book;

                       return true;
                    }
                }
                
                this.book = this.application.Open(this.documentPath, InDesignServer.idOpenOptions.idOpenCopy);

                return true;
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Book.Open: Cannot open book: + " + this.documentPath + " Message:" + ex.Message);
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
            
            return true;
        }
    }
}