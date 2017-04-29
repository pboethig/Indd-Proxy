namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands.Document;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;

    [TestFixture]
    public class CopyDocumentTest
    {

        private string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

        [SetUp]
        public void Setup()
        {
            dynamic commandRequest = new
            {
                classname = "Document.SaveAndClose",
                uuid = testuuid,
                version = "1.0"
            };

            SaveAndClose command = new SaveAndClose(commandRequest);
        }

        [TearDown]
        public void TearDown()
        {
            dynamic commandRequest = new
            {
                classname = "Document.SaveAndClose",
                uuid = testuuid,
                version = "1.0"
            };

            SaveAndClose command = new SaveAndClose(commandRequest);
            command.processSequence();
        }

        [Test]
        public void Commands_CopyDocument()
        {
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            dynamic commandRequest = new
            {
                classname = "CreateCopy",
                uuid = testuuid,
                version = "1.0",
                serverless =true
            };
            
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/"+ testuuid + "/" +commandRequest.version+".indd";

            CreateCopy command = new CreateCopy(commandRequest);

            List<System.Exception> exceptions = command.processSequence();

            Assert.IsEmpty(exceptions);
        }
        
     }
}