
namespace Indd.Service
{
    class Abstract
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
    }
}
