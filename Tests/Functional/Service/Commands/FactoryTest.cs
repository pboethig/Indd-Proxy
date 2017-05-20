namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using System.Collections.Generic;
    using Response = Indd.Service.Commands.Response;
    using Indd.Contracts;
    
        [TestFixture]
    public class FactoryTest : TestAbstract
    {
        /// <summary>
        /// Response to test
        /// </summary>
        Response response;
        
        [SetUp]
        public void Setup()
        {
            ticket = this.getTicket("Document.Open");
        }

        [TearDown]
        public void TearDown()
        {
            //delete test data
            try
            {
                dynamic propertyValue = response.getAdditionalDataPropertyValue("Document.CreateCopy.targetFolderPath");

                if (System.IO.Directory.Exists(propertyValue))
                {
                    System.IO.Directory.Delete(propertyValue, true);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Tests commandlist build
        /// </summary>
        [Test]
        public void CommandFactory_buildCommandObjectList()
        {
            List<ICommand> commands = commandFactory.buildCommandObjectList(ticket);

            Assert.IsEmpty(commandFactory.ticketExceptions);
        }

        [Test]
        public void CommandFactory_runCommands()
        {
            response = commandFactory.processTicket(ticket);

            Assert.IsNotEmpty(response.ticketId);

            Assert.IsEmpty(response.errors);

            Assert.AreEqual("ready", response.status);

            Assert.AreEqual(0, commandFactory.ticketExceptions.Count);
        }

        [Test]
        public void CommandFactory_runFailingCommands()
        {
            ticket = this.getTicket("failingTicket");
            response = commandFactory.processTicket(ticket);
            
            Assert.AreEqual("error", response.status);

            Assert.AreEqual(commandFactory.ticketExceptions.Count, response.errors.Count);
        }

        [Test]
        public void CommandFactory_buildJsonResponse()
        {
            ticket = this.getTicket("Document.Open");

            response = commandFactory.processTicket(ticket);
           
            string jsonString = response.toJson();
            
            Assert.IsNotEmpty(jsonString);
        }

        [Test]
        public void CommandFactory_BuildAdditionalData()
        {
            ticket = this.getTicket("Document.ExportIDML");

            response = commandFactory.processTicket(ticket);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            string jsonString = response.toJson();

             dynamic decodedResponse = Indd.Helper.Json.Convert.deserializeObject(jsonString);

            Assert.AreEqual(decodedResponse.additionalData.Count, response.additionalData.Count);
        }

        [Test]
        public void CommandFactory_sendResponse()
        {
            ticket = this.getTicket("Document.Open");

            response = commandFactory.processTicket(ticket, false);
            
            List<string> responses = response.send();
        }

        [Test]
        public void CommandFactory_convertJsonTicket()
        {
            string filePath = this.getJobInQueue() + "\\" + "Document.Open.json";

            Factory factory = new Factory();

            dynamic ticket = factory.convertJsonTicket(filePath);
            
            Assert.IsNotEmpty((string)ticket.id);
        }
    }
}