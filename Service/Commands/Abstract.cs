using System;
using Newtonsoft.Json;
using Indd.Service.IndesignServerWrapper;
using ConfigManager=Indd.Service.Config.Manager;
using Indd.Helper.Dynamic;
using Indd.Service.Log;
using System.Collections.Generic;

namespace Indd.Service.Commands
{

    /// <summary>
    /// Preimplementation for registered commands
    /// </summary>
    public abstract class Abstract : Indd.Contracts.ICommand
    {

        // Area is a read-only property - only a get accessor is needed:
        public dynamic commandRequest;

        /// <summary>
        /// UUID of whatever
        /// </summary>
        public string uuid;
        
        /// <summary>
        /// command class
        /// </summary>
        public string classname;

        /// <summary>
        /// version of what ever
        /// </summary>
        public string version;

        /// <summary>
        /// Path to document
        /// </summary>
        public string documentPath;

        /// <summary>
        /// if true a InDesignServerInstance will be started
        /// </summary>
        public bool serverless = false;

        /// <summary>
        /// Indesign Server application
        /// </summary>
        public InDesignServer.Application application;

        /// <summary>
        /// current document
        /// </summary>
        public InDesignServer.Document document;

        /// <summary>
        /// Stores request and starts validation
        /// </summary>
        /// <param name="commandRequests"></param>
        protected Abstract(dynamic _commandRequests)
        {
            this.commandRequest = _commandRequests;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual List<System.Exception> processSequence()
        {
            List<System.Exception> exceptions = new List<System.Exception>();

            try
            {
                try
                {
                    this.init();

                    ///call following methods on child commands
                    this.execute();
                }
                catch (System.Exception ex)
                {
                    string innerExceptionMessage = "";

                    if (ex.InnerException != null)
                    {
                        innerExceptionMessage = "Inner Exception: " + ex.InnerException.Message;
                    }

                    string message =
                        "JobticketException: " + this.classname + " throws an Error. Inner Exception:"+ ex.Message + innerExceptionMessage
                        + " \nPayload:\n " + this.commandRequest;

                    Syslog.log(message);

                    throw new SystemException(message);
                }
            }
            catch (System.Exception ex)
            {
                exceptions.Add(ex);
            }

            string stat ="Jobticket executed: " + this.classname + " \nPayload:\n " + this.commandRequest;

            Syslog.log(stat, System.Diagnostics.EventLogEntryType.SuccessAudit);
            
            return exceptions;
        }

        /// <summary>
        /// INitializes this
        /// </summary>
        public void init()
        {
            if (this.validateRequest() == true)
            {
                this.uuid = this.commandRequest.uuid;

                this.version = this.commandRequest.version;

                this.classname = this.commandRequest.classname;

                if (Property.isset("serverless", this.commandRequest))
                {
                    this.serverless = commandRequest.serverless;
                }

                this.buildServerInstance();

                this.setDocumentPath(this.buildDocumentPath());
            }
        }

        /// <summary>
        /// Build serverinstance if command needs one
        /// </summary>
        private void buildServerInstance()
        {
            if (this.serverless) {
                return;
            }

            this.application = (new ApplicationMananger()).createInstance();
        }

        /// <summary>
        /// Builds documentPath
        /// </summary>
        /// <returns></returns>
        public string buildDocumentPath()
        {
            string path = ConfigManager.getStoragePath("templates") + "/" + this.uuid + "/" + this.version + ".indd";

            if (!System.IO.File.Exists(path))
            {
                throw new System.Exception("document: " + path + " not found");
            }
            
            return path;
        }

        /// <summary>
        /// builds the document path
        /// </summary>
        /// <param name="path"></param>
        public virtual void setDocumentPath(string path)
        {
            this.documentPath = path;
        }

        public virtual bool execute()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Validates request
        /// </summary>
        /// <param name="commandRequest"></param>
        /// <returns>bool</returns>
        public virtual  bool validateRequest()
        {
            try
            {
                if (this.commandRequest.uuid == null)
                {
                    throw new FormatException("uuid is missing in command: ");
                }
               
                if (this.commandRequest.version == null)
                {
                    throw new FormatException("version is missing in command: ");
                }
            }
            catch(System.Exception ex)
            {
                Syslog.log("Malformat jobticket found. Message:  "  +  ex.Message + "original jobticket: " + JsonConvert.SerializeObject(this.commandRequest));
            }
            
            return true;
        }
    }
}