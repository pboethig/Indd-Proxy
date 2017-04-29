namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;


    [TestFixture]
    public class FactoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void CommandFactory_buildCommandObjectList()
        {
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "../../../Tests/Functional/Fixures/jobQueue/In/"+testuuid+".json";

            object commandList = CliRequest.getCommandList(filePath);

            Factory commandFactory = new Factory();

            List<Indd.Contracts.ICommand> commandObjectList = commandFactory.buildCommandObjectList(commandList);

            Assert.AreEqual(5, commandObjectList.Count);
        }
    }
}