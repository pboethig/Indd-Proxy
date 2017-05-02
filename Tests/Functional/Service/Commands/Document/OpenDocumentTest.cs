namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;
    using Indd.Service.Commands.Document;

    [TestFixture]
    public class DocumentOpenTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";
        
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_DocumentOpen()
        {
            dynamic commandRequest = new
            {
                classname = "Document.Open",
                uuid = testuuid,
                version = "1.0",
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension="indd"
            };
            
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/"+ testuuid + "/" +commandRequest.version+ "." + commandRequest.extension;

            Open DocumentOpenCommand = new Open(commandRequest);

            DocumentOpenCommand.uuid = testuuid;
                
            DocumentOpenCommand.setDocumentPath(filePath);

            DocumentOpenCommand.processSequence();

            List<System.Exception> exceptions = DocumentOpenCommand.processSequence();

            Assert.IsEmpty(exceptions);

            Assert.NotNull(DocumentOpenCommand.document);

            Assert.AreEqual("1.0." + commandRequest.extension, DocumentOpenCommand.document.Name);

            DocumentOpenCommand.document.Close();
        }
    }
}