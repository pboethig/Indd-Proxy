﻿using System;
using Newtonsoft.Json;

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