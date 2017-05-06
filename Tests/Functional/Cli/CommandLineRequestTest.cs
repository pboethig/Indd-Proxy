namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using CliRequest = Indd.Cli.Request.CommandList;

    [TestFixture]
    public class CommandLineRequestTest : TestAbstract
    {
        [SetUp]
        public void Setup()
        {

            ticket = this.getTicket("c2335ce8-7000-4287-8972-f355ed23bd7f");
        }

        [TearDown]
        public void TearDown()
        {
        }
        
        [Test]
        public void CliRequestCommandline_getCommandList()
        {
            foreach (dynamic command in ticket.commands)
            {
                Assert.NotNull(command.classname);

                Assert.NotNull(command.uuid);

                Assert.NotNull(command.version);
            }
        }
    }
}