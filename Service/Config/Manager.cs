using System.IO;

/// <summary>
/// Contains userconfiguration handler
/// </summary>
namespace Indd.Service.Config
{
    /// <summary>
    /// Handles User config files 
    /// </summary>
    public class Manager
    {

        /// <summary>
        /// Returns storageConfiguration
        /// </summary>
        /// <param name="path"></param>
        /// <returns>XDocument</returns>
        public static dynamic getStoragePath(string path)
        {
            dynamic config = load("storage");

            dynamic paths = config.paths;
            
            foreach (Newtonsoft.Json.Linq.JProperty item in paths)
            {
                if (item.Name == path)
                    return item.Value.ToString();
            }

            throw new Indd.Exception.StoragePathNotFoundException("no storagePath configured for: " + path);
        }


        /// <summary>
        /// Returns storageConfiguration
        /// </summary>
        /// <param name="path"></param>
        /// <returns>XDocument</returns>
        public static dynamic getJobQueuePath(string path)
        {
            dynamic config = load("jobqueues");

            dynamic paths = config.paths;

            foreach (Newtonsoft.Json.Linq.JProperty item in paths)
            {
                if (item.Name == path)
                    return item.Value.ToString();
            }

            string message = "no jobqueuePath configured for: " + path;

            Indd.Service.Log.Syslog.log(message);

            throw new System.Exception(message);
        }

        /// <summary>
        /// Loads a given configFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns>XDocument</returns>
        public static dynamic load(string path)
        {
            string filePath = getRootDirectory() + "\\" + path + ".json";
            
            string fileContent = File.ReadAllText(filePath);

            fileContent = fileContent.Replace("$root", getRootDirectory().Replace("\\", "\\\\"));

            return Indd.Helper.Json.Convert.deserializeObject(fileContent);
        }

        /// <summary>
        /// Returns app root directory
        /// </summary>
        /// <returns>string</returns>
        public static string getRootDirectory()
        {
            string path = System.IO.Path.GetDirectoryName(Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location));

            if (!System.IO.Directory.Exists(path))
            {
                throw new System.Exception("Rootdirectory: " + path + " not found");
            }

            return path.Replace("\\bin\\Debug", "");
        }
    }
}
