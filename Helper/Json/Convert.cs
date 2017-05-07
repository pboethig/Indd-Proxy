using Newtonsoft.Json;

/// <summary>
/// Contains Jsion Helper
/// </summary>
namespace Indd.Helper.Json
{
    /// <summary>
    /// Provides converter methods
    /// </summary>
    class Convert
    {
        /// <summary>
        /// Converts a jsonString to dynamic object
        /// </summary>
        /// <param name="jsonstring"></param>
        /// <returns>dynamic</returns>
        public static dynamic deserializeObject(string jsonstring)
        {
            dynamic deserializedObject = JsonConvert.DeserializeObject<dynamic>(jsonstring);

            return deserializedObject;
        }

        /// <summary>
        /// Gneerate Jsonstring from object
        /// </summary>
        /// <param name="toJsonfyObject"></param>
        /// <returns></returns>
        public static string toJson(object toJsonfyObject)
        {
            string jsonString = JsonConvert.SerializeObject(toJsonfyObject, Formatting.Indented);

            return jsonString;
        }
    }
}
