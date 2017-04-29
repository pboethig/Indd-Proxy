namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;


    [TestFixture]
    public class FactoryTest
    {

        object commandList;

        string filePath;

        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

        List<Indd.Contracts.ICommand> commandObjectList;

        [SetUp]
        public void Setup()
        {
            filePath = Indd.Service.Config.Manager.getRootDirectory() + "../../../Tests/Functional/Fixures/jobQueue/In/" + testuuid + ".json";

            commandList = CliRequest.getCommandList(filePath);

            Factory commandFactory = new Factory();

            commandObjectList = commandFactory.buildCommandObjectList(commandList);
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void CommandFactory_buildCommandObjectList()
        {
            Assert.AreEqual(6, commandObjectList.Count);
        }

        [Test]
        public void CommandFactory_runCommands()
        {
            foreach (Indd.Contracts.ICommand command in commandObjectList)
            {
                command.processSequence();
            }
        }
    }
}