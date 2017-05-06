namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Document
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class ExportHTMLFXTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Document.ExportHTMLFX");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportHTMLFX()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}