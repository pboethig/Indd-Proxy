namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Document
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using System.Collections.Generic;

    [TestFixture]
    public class ExportHTMLFXTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Document.ExportHTMLFX");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportHTMLFX()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(2, response.additionalData.Count);

            bool exportFilePathFound = false;

            bool exportWebResourcesPath = true;

            foreach (KeyValuePair<string, object> additionalDataItem in response.additionalData)
            {

                if (additionalDataItem.Key == "Document.ExportHTMLFX.exportFilePath")
                {
                    Assert.IsNotEmpty(additionalDataItem.Value.ToString());

                    string htmlFilePath = additionalDataItem.Value.ToString();

                    Assert.IsTrue(System.IO.File.Exists(htmlFilePath));

                    System.IO.File.Delete(additionalDataItem.Value.ToString());

                    exportFilePathFound = true;
                }

                if (additionalDataItem.Key == "Document.ExportHTMLFX.exportWebResourcesPath")
                {
                    Assert.IsNotEmpty(additionalDataItem.Value.ToString());

                    Assert.IsTrue(System.IO.Directory.Exists(additionalDataItem.Value.ToString()));

                    System.IO.Directory.Delete(additionalDataItem.Value.ToString(), true);

                    exportWebResourcesPath = true;
                }

                Assert.IsTrue(exportFilePathFound);

                Assert.IsTrue(exportWebResourcesPath);
            }
        }
    }
}