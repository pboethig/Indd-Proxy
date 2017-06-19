namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Book
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using System.Collections.Generic;

    [TestFixture]
    public class ExportJPGTest : TestAbstract
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
        public void Commands_Book_ExportJPG()
        {
            ticket = base.getTicket("Book.ExportJPG");

            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            foreach (KeyValuePair<string, object> additionalDataItem in response.additionalData)
            {
                Assert.AreEqual(additionalDataItem.Key, "Book.ExportJPG.pageThumbnailPaths");
            }
        }
    }
}