namespace Indd.Tests.Functional.Service.IndesignServer.Commands.Document
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
            ticket = base.getTicket("Document.ExportJSON");
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportJSON()
        {
            CommandResponse response = commandFactory.processTicket(ticket);

            Assert.IsEmpty(response.errors);

            Assert.IsNotEmpty(response.additionalData);

            Assert.AreEqual(1, response.additionalData.Count);

            foreach (KeyValuePair<string, object> additionalDataItem in response.additionalData)
            {
                Assert.AreEqual(additionalDataItem.Key, "Document.ExportJSON.json");

                Assert.IsNotEmpty(additionalDataItem.Value.ToString());

                dynamic document = Indd.Helper.Json.Convert.deserializeObject(additionalDataItem.Value.ToString());

                Assert.IsNotNull(document);
            }
        }
    }
}