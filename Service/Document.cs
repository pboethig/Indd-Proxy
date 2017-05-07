using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indd.Service.IndesignServerWrapper;

namespace Indd.Service
{
    class Document
    {
        /// <summary>
        /// Returns documents path
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="version"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string getDocumentPath(string uuid, string version = "1.0", string extension = "indd")
        {
            string documentPath = Indd.Service.Config.Manager.getStoragePath("templates") + "\\" + uuid + "\\" + version + "." + extension;

            if (!System.IO.File.Exists(documentPath))
            {
                throw new System.Exception("Document: " + documentPath + " not found");
            }

            return documentPath;
        }

        /// <summary>
        /// Opens a document
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="version"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static InDesignServer.Document getDocument(string uuid, string version = "1.0", string extension = "indd", string ticketId="randomId")
        {
            dynamic DocumentOpenCommandRequest = new
            {
                classname = "Document.Open",
                uuid = uuid,
                version = version,
                ticketId = ticketId,
                extension = extension,
                documentFolderPath = Indd.Service.Config.Manager.getStoragePath("templates")
            };

            Indd.Service.Commands.Document.Open documentCommand = new Indd.Service.Commands.Document.Open(DocumentOpenCommandRequest);

            documentCommand.processSequence();

            return documentCommand.document;
        }
    }
}
