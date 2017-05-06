namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Document
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class ExportPDFTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Document.ExportPDF");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportPDF()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}