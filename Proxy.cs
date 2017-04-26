using System;
using System.Linq;
using Indd.Contracts;
using System.Collections.Generic;
using CommandLine;
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

            List<ICommand> commandList  = commandFactory.buildCommandObjectList(commandRequests);

            commandFactory.buildCommandObjectList(commandList);

            //ApplicationMananger manager = new ApplicationMananger();

            //InDesignServer.Application app = manager.createInstance();

        }
    }
}
