using System;
using Newtonsoft.Json;
using Indd.Service.IndesignServerWrapper;
using ConfigManager=Indd.Service.Config.Manager;

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
        /// Indesign Server application
        /// </summary>
        public InDesignServer.Application application;

        /// <summary>
        /// Stores request and starts validation
        /// </summary>
        /// <param name="commandRequests"></param>
        protected Abstract(dynamic _commandRequests)
        {
            this.commandRequest = _commandRequests;

            if (this.validateRequest() == true)
            {
                this.uuid = this.commandRequest.uuid;

                this.version = this.commandRequest.version;

                this.classname = this.commandRequest.classname;
                
                this.application = (new ApplicationMananger()).createInstance();

                this.setDocumentPath(this.buildDocumentPath());
            }

            try
            {
                ///call following methods on child commands
                this.execute();

                this.saveResponse();

                this.notify();
            }
            catch (System.Exception ex)
            {
                Indd.Service.Log.Syslog.log("JobticketException: " + this.classname + " throws an Error. Inner Exception:" + ex.Message);
            }
        }

        /// <summary>
        /// Builds documentPath
        /// </summary>
        /// <returns></returns>
        public string buildDocumentPath()
        {
            string path = ConfigManager.getStoragePath("templates") + "/" + this.uuid + "/" + this.version + ".indd";

            try
            {
                if (!System.IO.File.Exists(path))
                {
                    throw new System.Exception("document: " + this.documentPath + " not found");
                }
            }
            catch (System.Exception ex)
            {
                Indd.Service.Log.Syslog.log(ex.Message);
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

        public virtual bool notify()
        {
            throw new NotImplementedException();
        }

        public virtual bool saveResponse()
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
                Indd.Service.Log.Syslog.log("Malformat jobticket found. Message:  "  +  ex.Message + "original jobticket: " + JsonConvert.SerializeObject(this.commandRequest));
            }
            
            return true;
        }
    }
}