namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Book
{
    using NUnit.Framework;
    using CommandResponse = Indd.Service.Commands.Response;
    using System.Collections.Generic;

    [TestFixture]
    public class ExportJSONTest : TestAbstract
    {

        [SetUp]
        public void Setup()
        {
            ticket = base.getTicket("Book.ExportJSON");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Book_ExportJSONWithAditionalDataResponse()
        {
            CommandResponse response = commandFactory.processTicket(ticket, false);
            
            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            foreach (KeyValuePair<string, object> additionalDataItem in response.additionalData)
            {
                

                Assert.AreEqual(additionalDataItem.Key, "Book.ExportJSON.json");

                Assert.AreEqual(additionalDataItem.Key, "Book.ExportJSON.json");

                Assert.IsNotEmpty(additionalDataItem.Value.ToString());

                dynamic book = Indd.Helper.Json.Convert.deserializeObject(additionalDataItem.Value.ToString());

                Assert.IsNotNull(book);
            }
        }
    }
}