namespace Indd.Tests.Functional.Service.Command.Book
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Indd.Service.Commands.Document;

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
                classname = "Document.ExportPDF",
                uuid = testuuid,
                version = "2.0",
                exportFolderPath = exportFolderPath,
                ticketId = "dsedsd-sdsdsd-sdsdsd-sdsdsd",
                extension="indd"
            };

            filePath = Indd.Service.Config.Manager.getRootDirectory() + "/Tests/Functional/Fixures/templates/" + testuuid + "/" + commandRequest.version + "." + commandRequest.extension;
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Commands_Document_ExportPDF()
        {
                ExportPDF command = new ExportPDF(commandRequest);

                //set fixurepath
                command.setDocumentPath(filePath);

                command.processSequence();

                List<System.Exception> exceptions = command.processSequence();

                Assert.IsEmpty(exceptions);

                Assert.IsNotEmpty(command.exportFilePath);

                Assert.IsTrue(System.IO.File.Exists(command.exportFilePath));

                command.document.Close();

                System.IO.File.Delete(command.exportFilePath);
        }
    }
}