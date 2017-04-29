﻿namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using Indd.Service.Commands;
    using CliRequest = Indd.Cli.Request.CommandList;
    using System.Collections.Generic;


    [TestFixture]
    public class SaveAndCloseDocumentTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";
        
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
            dynamic commandRequest = new
            {
                classname = "SaveAndCloseDocument",
                uuid = testuuid,
                version = "1.0"
            };

            SaveAndCloseDocument command = new SaveAndCloseDocument(commandRequest);
        }

        [Test]
        public void Commands_SaveAndClose()
        {
            dynamic commandRequest = new
            {
                classname = "SaveAndCloseDocument",
                uuid = testuuid,
                version = "1.0"
            };
            
             SaveAndCloseDocument command = new SaveAndCloseDocument(commandRequest);
         }
    }
}