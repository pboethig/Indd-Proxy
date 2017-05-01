﻿using System;
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
        /// <summary>
        /// Contains all commands
        /// </summary>
        public List<ICommand> commandList = new List<ICommand>();

        /// <summary>
        /// Contains all exceptions on a command during execiution
        /// </summary>
        public List<List<System.Exception>> ticketExceptions = new List<List<System.Exception>>();
        
        /// <summary>
        /// Builds commandList from commandline parameter
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public List<ICommand> buildCommandObjectList(dynamic ticket)
        {
            dynamic commandRequests = ticket.commands;

            foreach (dynamic request in commandRequests)
            {
                string className = "Indd.Service.Commands." + request.classname;

                try
                {
                    request.ticketId = ticket.id;

                    Type myType = Type.GetType(className);

                    ICommand command = Activator.CreateInstance(myType, request );
                    
                    commandList.Add(command);
                }
                catch (System.Exception ex)
                {
                    string innerExceptionMessage ="";

                    if (ex.InnerException != null)
                    {
                        innerExceptionMessage = "Inner Exception: " + ex.InnerException.Message;
                    }

                    string message = "CommandError: " + className + "  Message: " + ex.Message + innerExceptionMessage; 

                    Indd.Service.Log.Syslog.log(message);
                }
            }
            
            return commandList;
        }

        /// <summary>
        /// Runs commandlist
        /// </summary>
        /// <param name="commandRequests"></param>
        /// <returns>Response</returns>
        public Response processTicket(dynamic ticket)
        {
            List<ICommand> commands = buildCommandObjectList(ticket);

            foreach (Indd.Contracts.ICommand command in commands)
            {
                List<System.Exception> commandExceptions = command.processSequence();

                if (commandExceptions.Count > 0)
                {
                    ticketExceptions.Add(commandExceptions);
                }
            }

            Response response = new Response(ticket, ticketExceptions, commands);
            
            return response;
        }
    }
}