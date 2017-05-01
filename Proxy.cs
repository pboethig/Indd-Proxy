using Indd.Contracts;
using System.Collections.Generic;

using Indd.Service.Commands;
using CliRequest = Indd.Cli.Request.CommandList;

/// <summary>
/// Handles Incomming cli requests
/// </summary>
namespace Indd
{

    /// <summary>
    /// Implements main function
    /// </summary>
    class Proxy
    {
        static void Main(string[] args)
        {
            Factory commandFactory = new Factory();

            var result = CliRequest.validate(args);

            dynamic commandRequests = CliRequest.getCommandList(result.Value.InputFile);

            dynamic responseObject = CliRequest.getResponseObject(result.Value.InputFile);

            List<ICommand> commandList = commandFactory.buildCommandObjectList(commandRequests);

            commandFactory.buildCommandObjectList(commandList);

            foreach (Indd.Contracts.ICommand command in commandList)
            {
                List<System.Exception> exceptions = command.processSequence();
            }



        }
    }
}
