namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;
    using Indd.Service.Commands.Document;
    using Indd.Service.Commands.Book;

    [TestFixture]
    public class DocumentBookTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";
        
        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }
        
        [Test]
        public void Commands_BookOpen()
        {
            dynamic commandRequest = new
            {
                classname = "Book.Open",
                uuid = testuuid,
                version = "1.0",
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension = "indb"
            };

            string filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/" + testuuid + "/" + commandRequest.version + "." + commandRequest.extension;

            Indd.Service.Commands.Book.Open BookOpenCommand = new Indd.Service.Commands.Book.Open(commandRequest);

            BookOpenCommand.uuid = testuuid;

            BookOpenCommand.setDocumentPath(filePath);

            BookOpenCommand.processSequence();

            List<System.Exception> exceptions = BookOpenCommand.processSequence();

            Assert.IsEmpty(exceptions);

            Assert.NotNull(BookOpenCommand.book);

            Assert.AreEqual("1.0." + commandRequest.extension, BookOpenCommand.book.Name);

            BookOpenCommand.book.Close();
        }
    }
}