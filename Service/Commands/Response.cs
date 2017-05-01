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
        public List<KeyValuePair<string,dynamic>> additionalData = new List<KeyValuePair<string,dynamic>>();

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
        /// <param name="commands"></param>
        private void handleAdditionalData(dynamic ticket, List<ICommand> commands)
        {
            foreach (dynamic command in commands)
            {
                foreach (dynamic additionalDataItem in ticket.response.additionalData)
                {
                    if (additionalDataItem.classname == command.classname)
                    {
                        this.addAdditionalDataItem(additionalDataItem, command);
                    }
                }
            }
        }

        /// <summary>
        /// Adds additionalDataItem to response
        /// </summary>
        /// <param name="additionalDataItem"></param>
        /// <param name="command"></param>
        private void addAdditionalDataItem(dynamic additionalDataItem, dynamic command)
        {
            string objectPath = "";

            try
            {
                string propertyName = (string)additionalDataItem.property;
                
                dynamic value = Indd.Helper.Dynamic.Property.getValue(command, propertyName);

                objectPath = additionalDataItem.classname + "." + propertyName;

                KeyValuePair<string, dynamic> propertyValue = new KeyValuePair<string, dynamic>(objectPath, value);

                this.additionalData.Add(propertyValue);
            }
            catch (System.Exception ex)
            {
                KeyValuePair<string, dynamic> propertyValue = new KeyValuePair<string, dynamic>(objectPath, ex.Message);

                this.additionalData.Add(propertyValue);
            }
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
