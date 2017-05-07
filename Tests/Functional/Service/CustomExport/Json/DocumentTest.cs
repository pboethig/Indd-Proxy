﻿namespace Indd.Tests.Functional.Service.CustomExport.Json
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
        public void Service_CustomExport_Json_Document()
        {
            string uuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";

            string version = "7.0";

            InDesignServer.Document document = Indd.Service.Document.getDocument(uuid, version);

            Indd.Service.CustomExport.Json.Document jsonDocument = new Indd.Service.CustomExport.Json.Document(document);

            Assert.IsEmpty(jsonDocument.exportExceptions);

            string documentPath = Indd.Service.Document.getDocumentPath(uuid, version);

            string json = jsonDocument.toJson(documentPath + ".json");

            Assert.IsTrue(System.IO.File.Exists(documentPath + ".json"));

            document.Close();
        }
    }
}