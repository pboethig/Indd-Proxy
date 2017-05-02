namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Indd.Service.Commands.Images;
    using Indd.Service.Commands.Document;

    [TestFixture]
    public class SetLinksTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

        string root = Indd.Service.Config.Manager.getRootDirectory();

        [SetUp]
        public void Setup()
        {
           
        }

        [TearDown]
        public void TearDown()
        {
            ///close document
            dynamic commandRequest = new
            {
                classname = "Document.SaveAndClose",
                uuid = testuuid,
                version = "2.0",
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension="indd"
            };

            SaveAndClose command = new SaveAndClose(commandRequest);

            command.processSequence();

            List<System.Exception>exceptions = command.processSequence();

            Assert.IsEmpty(exceptions);
        }
        
        [Test]
        public void Commands_SetLinksV2()
        {
                string mock = @" [
                  {
                    ""objectId"": ""3022"",
                    ""imageId"": ""5e513f64-2dee-4e21-9871-53af41d6bf7b"",
                    ""type"": ""jpg"",
                    ""basePath"": ""Z:/indd/assets""
                  },
                  {
                    ""objectId"": ""3021"",
                    ""imageId"": ""8778687-78676876-54354-786786ghfhgf"",
                    ""type"": ""jpg"",
                    ""basePath"": ""Z:/indd/assets""
                  }
                ]
                ";

                dynamic objectToImageLinkMap = Indd.Helper.Json.Convert.deserializeObject(mock);

            dynamic setLinkCommandRequest = new
            {
                classname = "Relink",
                uuid = testuuid,
                version = "2.0",
                objectToImageLinkMap,
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension = "indd"
                };
            
            SetLinks setLinkCommand = new SetLinks(setLinkCommandRequest);

            setLinkCommand.processSequence();

            Assert.AreEqual("2.0." + setLinkCommandRequest.extension, setLinkCommand.document.Name);
            
            List<System.Exception> exceptions = setLinkCommand.processSequence();

            Assert.IsEmpty(exceptions);
        }

    [Test]
    public void Commands_SetLinksV1()
    {

        string testFolderPath = root + "/Tests/Functional/Fixures/templates/" + testuuid;

        string test = @" [
                  {
                    ""objectId"": ""3223"",
                    ""imageId"": ""5e513f64-2dee-4e21-9871-53af41d6bf7b"",
                    ""type"": ""jpg"",
                    ""basePath"": ""Z:/indd/assets""
                  },
                  {
                    ""objectId"": ""3222"",
                    ""imageId"": ""8778687-78676876-54354-786786ghfhgf"",
                    ""type"": ""jpg"",
                    ""basePath"": ""Z:/indd/assets""
                  }
                ]
                ";

        dynamic objectToImageLinkMap = Indd.Helper.Json.Convert.deserializeObject(test);

        dynamic setLinkCommandRequest = new
        {
            classname = "Relink",
            uuid = testuuid,
            version = "1.0",
            objectToImageLinkMap,
            ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
            extension="indd"
        };

        SetLinks setLinkCommand = new SetLinks(setLinkCommandRequest);

        setLinkCommand.processSequence();

        Assert.AreEqual("1.0." + setLinkCommandRequest.extension, setLinkCommand.document.Name);

        List<System.Exception> exceptions = setLinkCommand.processSequence();

        Assert.IsEmpty(exceptions);
    }
    }
}