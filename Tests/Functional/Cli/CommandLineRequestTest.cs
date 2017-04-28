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
            string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "../../../Tests/Functional/Fixures/jobQueue/In/"+ testuuid + ".json";
            
            dynamic commandList = CliRequest.getCommandList(filePath);

            foreach (dynamic command in commandList)
            {
                Assert.NotNull(command.classname);

                Assert.NotNull(command.uuid);

                Assert.NotNull(command.version);
            }
        }
    }
}