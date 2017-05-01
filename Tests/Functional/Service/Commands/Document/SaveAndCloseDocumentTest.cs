namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;
    using Indd.Service.Commands.Document;

    [TestFixture]
    public class SaveAndCloseDocumentTest
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
        public void Commands_SaveAndClose()
        {
            dynamic commandRequest = new
            {
                classname = "Document.SaveAndClose",
                uuid = testuuid,
                version = "2.0"
            };
            
             SaveAndClose command = new SaveAndClose(commandRequest);

            List<System.Exception> exceptions = command.processSequence();

            Assert.IsEmpty(exceptions);
        }
    }
}