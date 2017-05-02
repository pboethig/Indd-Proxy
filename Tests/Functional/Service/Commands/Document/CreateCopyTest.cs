namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands.Document;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;

    [TestFixture]
    public class CopyDocumentTest
    {
        /// <summary>
        /// ID to test
        /// </summary>
        private string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

        /// <summary>
        /// Root dir of the storage
        /// </summary>
        string root = Indd.Service.Config.Manager.getRootDirectory();
        
        /// <summary>
        /// Folder to test
        /// </summary>
        string folderPath;

        [SetUp]
        public void Setup()
        {
            folderPath = root + "/" + "/Tests/Functional/Fixures/templates/" + testuuid;

            dynamic commandRequest = new
            {
                classname = "Document.SaveAndClose",
                uuid = testuuid,
                version = "1.0",
                extenstion = "indd"
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
                version = "1.0",
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extenstion = "indd"
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
                ticketId="dsedsd-sdsdsd-sdsdsd-sdsdsd",
                uuid = testuuid,
                version = "1.0",
                serverless =true,
                extension = "indd"
            };
            
            string filePath = folderPath + "/" +commandRequest.version+"." + commandRequest.extension;

            CreateCopy command = new CreateCopy(commandRequest);

            List<System.Exception> exceptions = command.processSequence();

            Assert.IsEmpty(exceptions);

            System.IO.Directory.Delete(command.getTargetFolderPath(), true);
        }
     }
}