using System.Collections.Generic;
using System.Net;
using Indd.Contracts;
namespace Indd.Service.Commands
{
    /// <summary>
    /// Builds response on commandRequest
    /// </summary>
    class Response
    {
        /// <summary>
        /// UUI of the ticket
        /// </summary>
        public string ticketId;

        /// <summary>
        /// Message
        /// </summary>
        public string status;

        /// <summary>
        /// Array of errormessages
        /// </summary>
        public List <string> errors = new List<string>();

        /// <summary>
        /// Can be used to pass custom data to client
        /// </summary>
        public List<dynamic> additionalData = new List<dynamic>();

        /// <summary>
        /// Urls to notifiy
        /// </summary>
        public dynamic urls;

        /// <summary>
        /// Saves RequestData
        /// </summary>
        /// <param name="ticket"></param>
        public Response(dynamic ticket, List<List<System.Exception>> ticketExceptions, List<ICommand> commands)
        {
            this.ticketId = ticket.response.ticketId;
            
            this.status = "ready";

           this.handleAdditionalData(ticket, commands);

            this.urls = ticket.response.urls;

            this.handleExceptions(ticketExceptions);
        }

        /// <summary>
        /// Adds requested objectproperties, if exists on object
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="command"></param>
        private void handleAdditionalData(dynamic ticket, List<ICommand> command)
        {
            this.additionalData.Add(ticket.response.additionalData);
        }

        /// <summary>
        /// Handles ticketExceptions
        /// </summary>
        /// <param name="ticketExceptions"></param>
        private void handleExceptions(List<List<System.Exception>> ticketExceptions)
        {
            if (ticketExceptions.Count > 0)
            {
                this.status = "error";

                this.buildErrors(ticketExceptions);
            }
        }
        
        /// <summary>
        /// Builds errors
        /// </summary>
        /// <param name="ticketExceptions"></param>
        private void buildErrors(List<List<System.Exception>> ticketExceptions)
        {
            foreach (List<System.Exception> commandExceptions in ticketExceptions)
            {
                foreach (System.Exception commandException in commandExceptions)
                {
                    this.errors.Add(commandException.Message.ToString());
                }
            }
        }

        /// <summary>
        /// Converts object to json
        /// </summary>
        /// <param name="toJsonfyObject"></param>
        /// <returns></returns>
        public string toJson()
        {
            string json = Indd.Helper.Json.Convert.toJson(this);

            return json;
        }

        /// <summary>
        /// Sends response
        /// </summary>
        /// <returns></returns>
        public List<string> send()
        {

            WebClient client = new WebClient();

            List<string> responses = new List<string>();

            foreach (string url in urls)
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                string response = client.UploadString(url, "POST", this.toJson());

                responses.Add(response);
            }
            
            return responses;
        }
    }
}
