using System;
using Indd.Contracts;
using System.Collections.Generic;
/// <summary>
/// Handles Commands
/// </summary>
namespace Indd.Service.Commands
{
    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class Factory
    {
        public List<ICommand> commands { get; set; }

        public string className;

        /// <summary>
        /// Builds commandList from commandline parameter
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<ICommand> buildCommandObjectList(dynamic commandRequests)
        {
            List<ICommand> list = new List<ICommand>();
            
            foreach(dynamic request in commandRequests)
            {
                try
                {
                    this.className = "Indd.Service.Commands." + request.classname;

                    if (request.classname == null) continue;

                    Type myType = Type.GetType(this.className);

                    ICommand command = Activator.CreateInstance(myType, request );

                    list.Add(command);
                }
                catch (System.Exception ex)
                {

                    string innerExceptionMessage ="";

                    if (ex.InnerException != null)
                    {
                        innerExceptionMessage = "Inner Exception: " + ex.InnerException.Message;
                    }

                    string message = "CommandError: " + this.className + "  Message: " + ex.Message + innerExceptionMessage; 

                    Indd.Service.Log.Syslog.log(message);
                }
            }

            return list;
        }
    }
}