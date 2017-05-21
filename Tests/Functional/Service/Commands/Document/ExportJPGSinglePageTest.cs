namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Document
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using System.Collections.Generic;
    [TestFixture]
    public class ExportJPGSinglePageTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Document.ExportJPGSinglePage");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportJPGSinglePageTest()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            foreach (KeyValuePair<string, dynamic> additionalDataItem in response.additionalData)
            {
                Assert.AreEqual(additionalDataItem.Key, "Document.ExportJPGSinglePage.pageThumbnailPaths");

                foreach (string filePath in additionalDataItem.Value)
                {
                    Assert.IsTrue(System.IO.File.Exists(filePath));

                    System.IO.File.Delete(filePath);
                }
            }
        }
    }
}