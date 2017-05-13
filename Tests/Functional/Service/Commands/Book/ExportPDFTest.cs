namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Book
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
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Book_ExportPDF()
        {
            ticket = base.getTicket("Book.ExportPDF");

            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            foreach (KeyValuePair<string, object> additionalDataItem in response.additionalData)
            {
            
                Assert.AreEqual(additionalDataItem.Key, "Book.ExportPDF.exportFilePath");

                Assert.IsNotEmpty(additionalDataItem.Value.ToString());

                Assert.IsTrue(System.IO.File.Exists(additionalDataItem.Value.ToString()));

                System.IO.File.Delete(additionalDataItem.Value.ToString());
            }
        }
    }
}