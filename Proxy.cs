using Indd.Contracts;
using System.Collections.Generic;

using Indd.Service.Commands;
using CommandListCliRequest = Indd.Cli.Request.CommandList;


using ResponseType=Indd.Service.Commands.Response;
using Indd.Service.Filesystem;
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

            dynamic whatchFolderResult = CommandListCliRequest.validateWatchFolder(args);

            if (whatchFolderResult != null)
            {
                Watcher watcher = new Watcher(whatchFolderResult.Value.WatchFolder);

                return;
            }

            var commandResult = CommandListCliRequest.validate(args);

            dynamic ticket = commandFactory.convertJsonTicket(commandResult.Value.InputFile);

            commandFactory.processTicket(ticket);
        }
    }
}
