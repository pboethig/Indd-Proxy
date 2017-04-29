using System;
using Indd.Helper.IO;
using Indd.Service.Config;
using System.IO;
namespace Indd.Service.Commands {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class CreateCopy : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Path of sourceFolder
        /// </summary>
        private string sourceFolderPath;

        /// <summary>
        /// Guid of the target copy
        /// </summary>
        private Guid targetUuid;

        /// <summary>
        /// path of target
        /// </summary>
        private string targetFolderPath;

        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public CreateCopy(dynamic commandRequest) : base ((object)commandRequest)
        {
            
        }

        /// <summary>
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                this.sourceFolderPath = Path.GetDirectoryName(this.documentPath);

                this.targetUuid = Guid.NewGuid();

                this.targetFolderPath = Manager.getStoragePath("templates") + "/" + this.targetUuid.ToString();

                Indd.Helper.IO.Directory.Copy(this.sourceFolderPath, this.targetFolderPath);
            }
            catch(System.Exception ex)
            {
                Indd.Service.Log.Syslog.log("Command CreateCopy failed: path:" + sourceFolderPath + "Inner Exception: " + ex.Message);
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
        /// <returns>bool</returns>
        public override bool validateRequest() 
        {
            base.validateRequest();
            
            return true;
        }

        /// <summary>
        /// Return generated targetPath
        /// </summary>
        /// <returns>string</returns>
        public string getTargetFolderPath()
        {
            return this.targetFolderPath;  
        }

        /// <summary>
        /// Returns target Uuid
        /// </summary>
        /// <returns></returns>
        public Guid getTargetUuid()
        {
            return this.targetUuid;    
        }
    }
}