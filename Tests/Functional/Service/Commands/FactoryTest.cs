﻿namespace Indd.Tests.Functional.Service.IndesignServer
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
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "../../../Tests/Functional/Fixures/jobQueue/In/command.json";

            object commandList = CliRequest.getCommandList(filePath);

            Factory commandFactory = new Factory();

            List<Indd.Contracts.ICommand> commandObjectList = commandFactory.buildCommandObjectList(commandList);

            Assert.AreEqual(3, commandObjectList.Count);
        }
    }
}