namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;
    using Indd.Service.Commands.Document;
    using Indd.Service.Commands.Images;

    [TestFixture]
    public class RelinkAllTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

        string rootDir = Indd.Service.Config.Manager.getRootDirectory();

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }
        
        [Test]
        public void Commands_RelinkAll()
        {
            for (int i = 0; i <= 5; i++)
            {
                dynamic commandRequest = new
                {
                    classname = "Document.Open",
                    uuid = testuuid,
                    version = "1.0",
                    ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                    extension="indd"
                };
                
                ///relink all assets to new basePath
                string testFolderPath = rootDir + "/Tests/Functional/Fixures/assets";

                dynamic relinkCommandRequest = new
                {
                    classname = "Relink",
                    uuid = testuuid,
                    version = "1.0",
                    basePath = testFolderPath,
                    ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                    extension = "indd"
                };
                
                RelinkAll relinkCommand = new RelinkAll(relinkCommandRequest);
                
                relinkCommand.processSequence();

                List<System.Exception> exceptions = relinkCommand.processSequence();

                Assert.IsEmpty(exceptions);

                relinkCommand.document.Close();
            }
        }
     }
}