namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using CliRequest = Indd.Cli.Request.CommandList;

    [TestFixture]
    public class CommandLineRequestTest
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
        public void CliRequestCommandline_getCommandList()
        {
            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "../../../Tests/Functional/Fixures/jobQueue/In/command.json";
            
            dynamic commandList = CliRequest.getCommandList(filePath);

            foreach (dynamic command in commandList)
            {
                Assert.NotNull(command.name);

                Assert.NotNull(command.sourcePath);

                Assert.NotNull(command.notifyUrl);
            }
        }
    }
}