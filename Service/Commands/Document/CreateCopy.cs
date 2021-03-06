﻿using System;
using Indd.Service.Config;
using System.IO;
namespace Indd.Service.Commands.Document {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class CreateCopy : Abstract,  Contracts.ICommand
    {
        /// <summary>
        /// Path of sourceFolder
        /// </summary>
        private string sourceFolderPath;

        /// <summary>
        /// Guid of the target copy
        /// </summary>
        public Guid targetUuid { get; set; }

        /// <summary>
        /// path of target
        /// </summary>
        public string targetFolderPath { get; set; }

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

                this.targetFolderPath = this.buildTargetFolderPath();
                
                Indd.Helper.IO.Directory.Copy(this.sourceFolderPath, this.targetFolderPath);
            }
            catch(System.Exception ex)
            {
                throw new SystemException("Document.CreateCopy failed: path:" + sourceFolderPath + "Inner Exception: " + ex.Message);
            }

            return true;
        }

        /// <summary>
        /// Builds targetfolder path
        /// </summary>
        /// <returns>string</returns>
        public string buildTargetFolderPath()
        {
            targetFolderPath =(string)this.commandRequest.targetFolderPath;

            targetFolderPath = targetFolderPath.Replace("$root", Indd.Service.Config.Manager.getRootDirectory() );

            return this.targetFolderPath + "\\" + this.targetUuid.ToString();
        }

        /// <summary>
        /// Validate Request
        /// </summary>
        /// <param name="args"></param>
        /// <returns>bool</returns>
        public override bool validateRequest() 
        {
            base.validateRequest();

            if (this.commandRequest.targetFolderPath == null)
            {
                throw new System.Exception("property targetFolderPath is missing in commandRequest");
            }

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