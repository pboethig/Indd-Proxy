using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indd.Helper.IO
{
    /// <summary>
    /// Creates ScriptingfilesystemObjects
    /// </summary>
    class ScripingFileSystemObject
    {

        /// <summary>
        /// Create a VB Scriptingfilesystem object and get a c# filesystemobject
        /// from it to use it in methods wich takes scripting paths from indd
        /// </summary>
        /// <param name="path"></param>
        /// <returns>object</returns>
        public static object getObject(string path)
        {
            object fileSystemObject = Microsoft.VisualBasic.Interaction.CreateObject("Scripting.FileSystemObject", "");

            Type fileSystemObjectType = Type.GetTypeFromProgID("Scripting.FileSystemObject");

            object file = fileSystemObjectType.InvokeMember("GetFile", System.Reflection.BindingFlags.InvokeMethod, null, fileSystemObject, new object[] { path });

            return file;
        }
    }
}
