namespace Indd.Tests.Functional.Service.CustomExport.Json
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using IndesignServer;
    
    [TestFixture]
    public class BookTest : TestAbstract
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
        public void Service_CustomExport_Json_Book()
        {
            string uuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            string version = "8.0";

            string extension = "indb";

            InDesignServer.Book book = Indd.Service.Book.getBook(uuid, version, extension);

            Indd.Service.CustomExport.Json.Book jsonBook= new Indd.Service.CustomExport.Json.Book(book);

            Assert.IsEmpty(jsonBook.exportExceptions);

            string bookPath = Indd.Service.Book.getDocumentPath(uuid, version, extension);

            string json = jsonBook.toJson(bookPath + ".json");

            Assert.IsTrue(System.IO.File.Exists(bookPath + ".json"));

            book.Close();
        }
    }
}