using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using Indd.Service.Log;

namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class Open : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// current document
        /// </summary>
        public InDesignServer.Document document;

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

                foreach (InDesignServer.Document DocumentOpen in this.application.Documents)
                {
                    if (DocumentOpen.Name == this.version + ".indd")
                    {
                        this.document = DocumentOpen;

                        return true;
                    }
                }

                this.document = this.application.Open(this.documentPath);
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Document.Open: Cannot open document: + " + this.documentPath + " Message:" + ex.Message);
            }

            return true;
        }

        public override bool notify()
        {
            return true;
        }

        public override bool saveResponse()
        {
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