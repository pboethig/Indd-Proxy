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
    }
}
