namespace Indd.Tests.Functional.Service.Document
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using IndesignServer;
    
    [TestFixture]
    public class DocumentTest : TestAbstract
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
        public void Service_Document_getDocument()
        {
            string uuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            InDesignServer.Document document = Indd.Service.Document.getDocument(uuid);
            
            Assert.AreEqual(document.Name,  "1.0.indd");
        }
    }
}