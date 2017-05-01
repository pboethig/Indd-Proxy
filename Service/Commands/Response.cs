using System.Collections.Generic;
using System.Net;

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
        public dynamic additionalData;

        /// <summary>
        /// Urls to notifiy
        /// </summary>
        public dynamic urls;

        /// <summary>
        /// Saves RequestData
        /// </summary>
        /// <param name="ticket"></param>
        public Response(dynamic ticket, List<List<System.Exception>> ticketExceptions)
        {
            this.ticketId = ticket.response.ticketId;
            
            this.status = "ready";

            this.additionalData = ticket.response.additionalData;

            this.urls = ticket.response.urls;

            this.handleExceptions(ticketExceptions);
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
