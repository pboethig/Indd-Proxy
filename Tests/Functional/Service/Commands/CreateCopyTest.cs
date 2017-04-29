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
        }

        [TearDown]
        public void TearDown()
        {

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

        [Test]
        public void Commands_CopyAndOpenAndRelinkImagesInNewDocument()
        {
            for (int i = 0; i <= 1; i++)
            {
                string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

                dynamic commandRequest = new { classname = "CreateCopy", uuid = testuuid, version = "1.0", serverless = true };

                string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/" + testuuid + "/" + commandRequest.version + ".indd";

                ///create a simple copy
                CreateCopy createCopyCommand = new Indd.Service.Commands.CreateCopy(commandRequest);

                createCopyCommand.processSequence();

                bool directoryExists = System.IO.Directory.Exists(createCopyCommand.getTargetFolderPath());

                Assert.IsTrue(directoryExists);



                ///Open Document after copy
                testuuid = createCopyCommand.getTargetUuid().ToString();

                commandRequest = new { classname = "OpenDocument", uuid = testuuid, version = "1.0" };

                filePath = createCopyCommand.getTargetFolderPath() + "/" + commandRequest.version + ".indd";

                OpenDocument openDocumentCommand = new Indd.Service.Commands.OpenDocument(commandRequest);

                openDocumentCommand.uuid = testuuid;

                openDocumentCommand.setDocumentPath(filePath);

                openDocumentCommand.processSequence();

                Assert.AreEqual(openDocumentCommand.document.Name, "1.0.indd");




                ///close document and de´lete folder
                openDocumentCommand.document.Close();
            
                System.IO.Directory.Delete(createCopyCommand.getTargetFolderPath(), true);
            }
        }
     }
}