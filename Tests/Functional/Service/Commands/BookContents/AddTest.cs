namespace Indd.Tests.Functional.Service.Command.BookContents
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Indd.Service.Commands.BookContents;

    [TestFixture]
    public class AddTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";
        
        string root = Indd.Service.Config.Manager.getRootDirectory();
        
        dynamic commandRequest;

        string filePath;

        [SetUp]
        public void Setup()
        {
            string documentUuids = @" [
                  {
                    ""uuid"": ""c2335ce8-7000-4287-8972-f355ed23bd7f"",
                    ""extension"": ""indd"",
                    ""version"": ""2.0""
                  },
                  {
                    ""uuid"": ""c2335ce8-7000-4287-8972-f355ed23bd7f"",
                    ""extension"": ""indd"",
                    ""version"": ""3.0""
                  }
                ]
                ";
            
            commandRequest = new
            {
                classname = "BookContent.Add",
                uuid = testuuid,
                version = "1.0",
                documentUuids = Indd.Helper.Json.Convert.deserializeObject(documentUuids),
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension="indb"
            };

            filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/" + testuuid + "/" + commandRequest.version + "." + commandRequest.extension;
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_BookContents_Add()
        {
                Add command = new Add(commandRequest);

                //set fixurepath
                command.setDocumentPath(filePath);
            
                List<System.Exception> exceptions = command.processSequence();

                Assert.IsEmpty(exceptions);

                Assert.IsNotNull(command.commandRequest.documentUuids);

                Assert.IsNotEmpty(command.commandRequest.ticketId);

                command.book.Close();
        }
    }
}