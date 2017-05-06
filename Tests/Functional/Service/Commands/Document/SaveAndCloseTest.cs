namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Book
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class SaveAndCloseTest : TestAbstract
    {

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_SaveAndClose()
        {
            ticket = base.getTicket("Document.SaveAndClose");

            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}