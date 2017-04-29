namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;


    [TestFixture]
    public class OpenDocumentTest
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
        public void Commands_OpenDocument()
        {
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            dynamic commandRequest = new { classname = "OpenDocument", uuid = testuuid, version = "1.0" };
            
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/"+ testuuid + "/" +commandRequest.version+".indd";
            
            for(int i = 0; i < 2; i++)
            {
                OpenDocument openDocumentCommand = new Indd.Service.Commands.OpenDocument(commandRequest);

                openDocumentCommand.uuid = testuuid;

                openDocumentCommand.setDocumentPath(filePath);

                openDocumentCommand.processSequence();

                Assert.NotNull(openDocumentCommand.document);

                Assert.AreEqual("1.0.indd", openDocumentCommand.document.Name);

                openDocumentCommand.document.Close();
            }
        }
    }
}