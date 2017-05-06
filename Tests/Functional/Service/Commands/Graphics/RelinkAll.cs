namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Graphics
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class RelinkAllTest : TestAbstract
    {
        
        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Graphics_RelinkAll()
        {
            ticket = base.getTicket("Graphics.RelinkAll");

            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}