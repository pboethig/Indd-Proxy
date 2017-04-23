﻿namespace Indd.Tests.Functional.Helper.Json
{
    using System;
    using NUnit.Framework;
    using ConfigManager = Indd.Service.Config.Manager;
   

    [TestFixture]
    public class ManagerTests
    {

        private string jsonString = @"{""id"":""FAC2SOUTHX"",""name"":""South District MW        "",""description"":""South District MW                                           "",""selected"":true,""required"":false,""sortOrder"":10}";

        [SetUp]
        public void Setup()
        {
            
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void JsonHelper_deserializeObject()
        {
            dynamic config = Indd.Helper.Json.Convert.deserializeObject(this.jsonString);

            Assert.NotNull(config.id);
        }
    }
}