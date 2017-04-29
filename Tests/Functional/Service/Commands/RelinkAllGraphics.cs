namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;

    [TestFixture]
    public class RelinkAllGraphicsTest
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
        public void Commands_RelinkAllGraphics()
        {
            for (int i = 0; i <= 5; i++)
            {
                string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

                dynamic commandRequest = new { classname = "OpenDocument", uuid = testuuid, version = "1.0" };

                string testFolderPath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/" + testuuid;

                string filePath = testFolderPath + "/" + commandRequest.version + ".indd";

                commandRequest = new { classname = "OpenDocument", uuid = testuuid, version = "1.0" };
                
                OpenDocument openDocumentCommand = new Indd.Service.Commands.OpenDocument(commandRequest);

                openDocumentCommand.uuid = testuuid;

                openDocumentCommand.setDocumentPath(filePath);

                openDocumentCommand.processSequence();

                Assert.AreEqual(openDocumentCommand.document.Name, "1.0.indd");

                ///relink all assets to new basePath
                string basePath = testFolderPath;

                dynamic relinkCommandRequest = commandRequest = new { classname = "Relink", uuid = testuuid, version = "1.0", basePath = basePath };

                RelinkAllGraphics relinkCommand = new RelinkAllGraphics(relinkCommandRequest);
                
                relinkCommand.processSequence();
            }
        }
     }
}