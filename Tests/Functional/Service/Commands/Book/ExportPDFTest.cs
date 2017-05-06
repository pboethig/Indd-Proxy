namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Book
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class ExportPDFTest : TestAbstract
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
        public void Commands_Book_ExportPDF()
        {
            ticket = base.getTicket("Book.ExportPDF");

            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            //System.IO.File.Delete(command.exportFilePath);
        }
    }
}