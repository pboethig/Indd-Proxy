using System;
using System.Linq;
using CommandLine;
using Indd.Service.IndesignServerWrapper;
using Indd.Service.Log;

namespace Indd.Service.Commands {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class OpenDocument : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// current document
        /// </summary>
        public InDesignServer.Document document;

        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public OpenDocument(dynamic commandRequest) : base ((object)commandRequest){}

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

                foreach (InDesignServer.Document openDocument in this.application.Documents)
                {
                    if (openDocument.Name == this.version + ".indd")
                    {
                        this.document = openDocument;

                        return true;
                    }
                }

                this.document = this.application.Open(this.documentPath);
            }
            catch (System.Exception ex)
            {
                Syslog.log("OpenDocument: Cannot open document: + " + this.documentPath + " Message:" + ex.Message);
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