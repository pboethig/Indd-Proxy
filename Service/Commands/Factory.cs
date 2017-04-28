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
                string className = "Indd.Service.Commands." + request.classname;
                
                try
                {
                    Type myType = Type.GetType(className);

                    ICommand command = Activator.CreateInstance(myType, request );

                    list.Add(command);
                }
                catch (System.Exception ex)
                {
                    string message = "CommandError: " + className + " Inner Message: " + ex.Message; 

                    Indd.Service.Log.Syslog.log(message);
                }
            }

            return list;
        }
    }
}