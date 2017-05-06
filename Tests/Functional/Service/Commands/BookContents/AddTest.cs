namespace Indd.Tests.Functional.Service.Command.BookContents
{
    using NUnit.Framework;
    
    using CommandResponse=Indd.Service.Commands.Response;

    [TestFixture]
    public class AddTest : TestAbstract
    {
        
        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Book.Contents.Add");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_BookContents_Add()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}