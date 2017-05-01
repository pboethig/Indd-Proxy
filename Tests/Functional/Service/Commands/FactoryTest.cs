namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;
    using Response = Indd.Service.Commands.Response;

    [TestFixture]
    public class FactoryTest
    {
        /// <summary>
        /// raw json commandlist
        /// </summary>
        string jsonTicket;

        /// <summary>
        /// UUID to test
        /// </summary>
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";
       
        /// <summary>
        /// commandfactory
        /// </summary>
        private Factory commandFactory;

        /// <summary>
        /// json converted commands
        /// </summary>
        private dynamic commandRequests;

        private string root = Indd.Service.Config.Manager.getRootDirectory();

        [SetUp]
        public void Setup()
        {
            jsonTicket = root + "../../../Tests/Functional/Fixures/jobQueue/In/" + testuuid + ".json";
            
            commandRequests = CliRequest.convertJsonTicket(jsonTicket);
            
            commandFactory = new Factory();
        }

        [TearDown]
        public void TearDown()
        {

        }
        
        [Test]
        public void CommandFactory_runCommands()
        {
            Response response = commandFactory.processTicket(commandRequests);

            Assert.AreEqual(0, response.errors.Count);

            Assert.AreEqual("ready", response.status);

            Assert.AreEqual(0, commandFactory.ticketExceptions.Count);
        }

        [Test]
        public void CommandFactory_runFailingCommands()
        {
            jsonTicket = root + "../../../Tests/Functional/Fixures/jobQueue/In/failingTicket.json";

            commandRequests = CliRequest.convertJsonTicket(jsonTicket);

            Response response = commandFactory.processTicket(commandRequests);

            Assert.AreEqual("error", response.status);

            Assert.AreEqual(1, response.errors.Count);

            Assert.AreEqual(commandFactory.ticketExceptions.Count, response.errors.Count);
        }
    }
}