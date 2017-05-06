namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Documents
{
    using NUnit.Framework;
    using System.Collections.Generic;

    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class DocumentCreateCopyTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Document.CreateCopy");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_CreateCopy()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
            
            dynamic propertyValue = response.getAdditionalDataPropertyValue("Document.CreateCopy.targetFolderPath");
            
             Assert.IsNotNull(propertyValue);

            System.IO.Directory.Delete(propertyValue, true);
        }
    }
}