using System;
using System.Linq;
using Indd.Service.IndesignServerWrapper;
using Indd.Contracts;
using System.Collections.Generic;
/// <summary>
/// Handles Commands
/// </summary>
namespace Indd.Service.Commands {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    class Factory :  Abstract
    {
        public List<ICommand> commands { get; set; }

        /// <summary>
        /// Builds commandList from commandline parameter
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<ICommand> buildCommandList(dynamic commandRequests)
        {
            List<ICommand> list = new List<ICommand>();
            
            foreach(dynamic request in commandRequests)
            {
                ICommand command = new GenerateProxy();

                command.validateRequest(request);

                list.Add(command);
            }

            return list;
        }

    }
}