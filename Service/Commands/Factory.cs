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
    public class Factory
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

                        request.documentFolderPath = ticket.documentFolderPath;

                        Type commandType = Type.GetType(className);

                        if (commandType == null) throw new System.Exception("Command " + className + " not implemented");

                        ICommand command = Activator.CreateInstance(commandType, request);

                        commandList.Add(command);
                    }
                    catch (System.Exception ex)
                    {
                        string innerExceptionMessage = "";

                        if (ex.InnerException != null)
                        {
                            innerExceptionMessage = "Inner Exception: " + ex.InnerException.Message;
                        }

                        string message = "CommandError: " + className + "  Message: " + ex.Message + innerExceptionMessage;

                        Indd.Service.Log.Syslog.log(message);

                        throw new System.Exception("CommandObjectList failed:" + message);
                    }
            }
            
            return commandList;
        }

        /// <summary>
        /// Runs commandlist
        /// </summary>
        /// <param name="commandRequests"></param>
        /// <returns>Response</returns>
        public Response processTicket(dynamic ticket, bool sendResponse = true)
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

            if (sendResponse)
            {
                response.send();
            }

            return response;
        }

        /// <summary>$
        /// Return Jobticket
        /// </summary>
        /// <param name="classname"></param>
        /// <returns>dynamic</returns>
        public dynamic getJsonTicket(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception("Ticket not found under: " + filePath);
            }

            string json = System.IO.File.ReadAllText(filePath);

            string directory = Indd.Service.Config.Manager.getStoragePath("root");
            
            json = json.Replace("$root", directory.Replace("\\", "\\\\"));

            dynamic ticket = Indd.Helper.Json.Convert.deserializeObject(json);

            return ticket;
        }


        /// <summary>
        /// Reads CommandListRequest from filesystem
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>dynamic</returns>
        public  dynamic convertJsonTicket(string filePath)
        {
            return this.getJsonTicket(filePath) ;
        }

    }
}