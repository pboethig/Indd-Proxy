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

            ticket = this.getTicket("Document.ExportPDF");
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