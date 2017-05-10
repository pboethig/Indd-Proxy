using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indd.Service.IndesignServerWrapper;

namespace Indd.Service
{
    class Book:Abstract
    {
        /// <summary>
        /// Opens a document
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="version"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static InDesignServer.Book getBook(string uuid, string version = "1.0", string extension = "indd", string ticketId="randomId")
        {
            dynamic OpenCommandRequest = new
            {
                classname = "Book.Open",
                uuid = uuid,
                version = version,
                ticketId = ticketId,
                extension = extension,
                documentFolderPath = Indd.Service.Config.Manager.getStoragePath("templates")
            };

            Indd.Service.Commands.Book.Open command = new Indd.Service.Commands.Book.Open(OpenCommandRequest);

            command.processSequence();

            return command.book;
        }
    }
}
