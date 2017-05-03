﻿namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Indd.Service.Commands.Book;

    [TestFixture]
    public class ExportPDFTest
    {
        string testuuid = "c2335ce8-7000-4287-8972-f355ed23bd7f";
        
        string root = Indd.Service.Config.Manager.getRootDirectory();

        string exportFolderPath;

        dynamic commandRequest;

        string filePath;

        [SetUp]
        public void Setup()
        {
            exportFolderPath = root + "/Tests/Functional/Fixures/exports";

            commandRequest = new
            {
                classname = "Book.ExportPDF",
                uuid = testuuid,
                version = "1.0",
                exportFolderPath = exportFolderPath,
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension="indb"
            };

            filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/" + testuuid + "/" + commandRequest.version + "." + commandRequest.extension;
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Book_ExportPDF()
        {
                ExportPDF command = new ExportPDF(commandRequest);

                //set fixurepath
                command.setDocumentPath(filePath);

                command.processSequence();

                List<System.Exception> exceptions = command.processSequence();

                Assert.IsEmpty(exceptions);

                Assert.IsNotEmpty(command.exportFilePath);

                Assert.IsTrue(System.IO.File.Exists(command.exportFilePath));

                command.book.Close();

                System.IO.File.Delete(command.exportFilePath);
        }
    }
}