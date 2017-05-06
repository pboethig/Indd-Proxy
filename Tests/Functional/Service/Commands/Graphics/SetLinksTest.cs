namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Graphics
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    [TestFixture]
    public class SetLinksV1Test : TestAbstract
    {
        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Graphics_SetLinksV1()
        {
            ticket = base.getTicket("Graphics.SetLinks.V1");

            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);
        }
    }
}