﻿namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Document
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using System.Collections.Generic;
    [TestFixture]
    public class ExportPDFTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Document.ExportPDF");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportPDF()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            foreach (KeyValuePair<string, object> additionalDataItem in response.additionalData)
            {
                Assert.AreEqual(additionalDataItem.Key, "Document.ExportPDF.exportFilePath");

                Assert.IsNotEmpty(additionalDataItem.Value.ToString());
                
                Assert.IsTrue(System.IO.File.Exists(additionalDataItem.Value.ToString()));

                System.IO.File.Delete(additionalDataItem.Value.ToString());
            }
        }
    }
}