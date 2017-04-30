using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indd.Service.Commands.Images
{
    class Base : Indd.Service.Commands.Abstract
    {

        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public Base(dynamic commandRequest) : base ((object)commandRequest){ }


        /// <summary>
        /// Relinks Document graphics
        /// </summary>
        /// <param name="document">document</param>
        /// <param name="basePath">target filepath</param>
        /// <param name="fileName">target filename</param>
        /// <param name="objectId">If an objectId is set, restrict relinking on that id</param>
        /// <returns></returns>
        public virtual bool relink(InDesignServer.Document document, string basePath, string fileName = "", int objectId = 0)
        {

            string documentName = document.Name;

            int targetObjectId;

            foreach (dynamic graphic in document.AllGraphics)
            {
                InDesignServer.Link link = graphic.ItemLink;

                targetObjectId = link.Parent.Parent.id;

                string filePath = this.buildFilePath(link, basePath, fileName); 

                object scriptingFileSystemObject = Indd.Helper.IO.ScripingFileSystemObject.getObject(filePath);

                if (objectId == 0 || objectId == targetObjectId)
                {
                    link.Relink(scriptingFileSystemObject);

                    link.Update();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Builds filePath to relink
        /// </summary>
        /// <param name="link"></param>
        /// <param name="basePath"></param>
        /// <param name="fileName"></param>
        /// <returns>string</returns>
        private string buildFilePath(InDesignServer.Link link,  string basePath, string fileName)
        {
            string filePath = "";

            if (String.IsNullOrEmpty(fileName) )
            {
                filePath = basePath + "/" + link.Name;
            }
            else
            {
                filePath = basePath + "/" + fileName;
            }

            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException("LinkPath not found: " + filePath);
            }

            return filePath;
        }
    }
}
