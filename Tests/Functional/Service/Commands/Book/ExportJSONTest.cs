namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Book
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class ExportJSONTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Book.ExportJSON");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Book_ExportJSON()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}