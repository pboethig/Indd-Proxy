namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
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
            };
            
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/"+ testuuid + "/" +commandRequest.version+".indd";
            
            for(int i = 0; i < 2; i++)
            {
                Open DocumentOpenCommand = new Open(commandRequest);

                DocumentOpenCommand.uuid = testuuid;
                
                DocumentOpenCommand.setDocumentPath(filePath);

                DocumentOpenCommand.processSequence();

                List<System.Exception> exceptions = DocumentOpenCommand.processSequence();

                Assert.IsEmpty(exceptions);

                Assert.NotNull(DocumentOpenCommand.document);

                Assert.AreEqual("1.0.indd", DocumentOpenCommand.document.Name);

                DocumentOpenCommand.document.Close();
            }
        }
    }
}