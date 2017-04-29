namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;

    [TestFixture]
    public class CopyDocumentTest
    {
        [SetUp]
        public void Setup()
        {
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            dynamic commandRequest = new { classname = "SaveAndCloseDocument", uuid = testuuid, version = "1.0" };

            SaveAndCloseDocument command = new Indd.Service.Commands.SaveAndCloseDocument(commandRequest);
        }

        [TearDown]
        public void TearDown()
        {
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            dynamic commandRequest = new { classname = "SaveAndCloseDocument", uuid = testuuid, version = "1.0" };

            SaveAndCloseDocument command = new Indd.Service.Commands.SaveAndCloseDocument(commandRequest);
        }

        [Test]
        public void Commands_CopyDocument()
        {
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            dynamic commandRequest = new { classname = "CreateCopy", uuid = testuuid, version = "1.0", serverless=true };
            
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/"+ testuuid + "/" +commandRequest.version+".indd";

            CreateCopy createCopyCommand = new Indd.Service.Commands.CreateCopy(commandRequest);

            createCopyCommand.processSequence();
        }
        
     }
}