using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

/// <summary>
/// Contains userconfiguration handler
/// </summary>
namespace Indd.Service.Config
{
    /// <summary>
    /// Handles User config files 
    /// </summary>
    class Manager
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
        /// Loads a given configFile
        /// </summary>
        /// <param name="path"></param>
        /// <returns>XDocument</returns>
        public static dynamic load(string path)
        {
            string filePath = getRootDirectory() + "\\" + path + ".json";

            return JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(filePath));
        }

        /// <summary>
        /// Returns app root directory
        /// </summary>
        /// <returns>string</returns>
        public static string getRootDirectory()
        {
            return System.IO.Path.GetDirectoryName(Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }
    }
}
