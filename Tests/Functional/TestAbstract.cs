
using Indd.Service.Commands;
using System.Collections.Generic;
using ConfigManager = Indd.Service.Config.Manager;

namespace Indd.Tests.Functional
{
    public abstract class TestAbstract
    {

        /// <summary>
        /// ticket
        /// </summary>
        public dynamic ticket;


        public string root = Indd.Service.Config.Manager.getRootDirectory();

        public Factory commandFactory = new Factory();

        public TestAbstract()
        {

        }
        
        /// <summary>
        /// get job in queue
        /// </summary>
        /// <returns></returns>
        public string getJobInQueue()
        {
            //return root + "\\Tests\\Functional\\Fixures\\jobQueue\\In";
            string jobQueuePath = ConfigManager.getJobQueuePath("in");

            return jobQueuePath;
        }

        /// <summary>$
        /// Return Jobticket
        /// </summary>
        /// <param name="classname"></param>
        /// <returns>dynamic</returns>
        public dynamic getTicket(string classname)
        {
            string filePath = this.getJobInQueue() + "\\" + classname + ".json";

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.Exception("Ticket not found under: " + filePath);
            }

            string json = System.IO.File.ReadAllText(filePath);

            json = json.Replace("$root", this.root.Replace("\\","\\\\"));

            dynamic ticket = Indd.Helper.Json.Convert.deserializeObject(json);
            
            return ticket;
        }
    }
}
