using System;
using Indd.Service.IndesignServerWrapper;
namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class SaveAndClose : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public SaveAndClose(dynamic commandRequest) : base ((object)commandRequest){}

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

                foreach (InDesignServer.Document _document in this.application.Documents)
                {
                    _document.Save(this.documentPath, false, "saved", true);
                    
                    if (_document.FullName == this.documentPath)
                    {
                        this.document = _document;
                        
                        _document.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                string innerExceptionMessage ="";

                if (ex.InnerException != null)
                {
                    innerExceptionMessage ="Inner Exception: " + ex.InnerException.Message;
                }
                
                throw new SystemException("Document.SaveAndClose failed: " + ex.Message + innerExceptionMessage);
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
            
            return true;
        }
    }
}