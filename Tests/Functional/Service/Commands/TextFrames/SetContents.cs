namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands.TextFrames;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;
    using Indd.Service.Commands.Document;

    [TestFixture]
    public class SetContentsTest
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

            dynamic commandRequest = new
            {
                classname = "Document.SaveAndClose",
                uuid = testuuid,
                version = "1.0",
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension="indd"
            };

            SaveAndClose command = new SaveAndClose(commandRequest);

        }
        
        [Test]
        public void Commands_SetContents()
        {
            for (int i = 0; i <= 5; i++)
            {

                string mock = @" [
                  {
                    ""frameId"": ""7256"",
                    ""contents"": ""Here is a test""
                    },
                  {
                    ""frameId"": ""7233"",
                    ""contents"": ""And here is another""
                  }
                ]
                ";

                dynamic commandRequest = new
                {
                    classname = "SetContents",
                    uuid = testuuid,
                    version = "1.0",
                    ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                    extension = "indd",
                    frameIdToContentsMap=Indd.Helper.Json.Convert.deserializeObject(mock)
                };
                
                SetContents command = new SetContents(commandRequest);
            
                List<System.Exception> exceptions = command.processSequence();

                Assert.IsEmpty(exceptions);
            }
        }
     }
}