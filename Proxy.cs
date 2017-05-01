using Indd.Contracts;
using System.Collections.Generic;

using Indd.Service.Commands;
using CliRequest = Indd.Cli.Request.CommandList;
using ResponseType=Indd.Service.Commands.Response;
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

            dynamic commandRequests = CliRequest.convertJsonTicket(result.Value.InputFile);

            commandFactory.processTicket(commandRequests);

        }
    }
}
