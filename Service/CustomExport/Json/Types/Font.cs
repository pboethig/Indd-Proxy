/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Font
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Font";

        /// <summary>
        /// Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Label
        /// </summary>
        public string Family;

        /// <summary>
        /// FontStyleName
        /// </summary>
        public string FontStyleName;

        /// <summary>
        /// Index
        /// </summary>
        public int Index;

        /// <summary>
        /// Location
        /// </summary>
        public string Location;

        /// <summary>
        /// PostScriptName
        /// </summary>
        public string PostScriptName;

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName;

        /// <summary>
        /// FullNameNative
        /// </summary>
        public string FullNameNative;

        /// <summary>
        /// FontType
        /// </summary>
        public string FontType;


        /// <summary>
        /// Note
        /// </summary>
        /// <param name="note"></param>
        public Font(InDesignServer.Font font)
        {
            this.Name = font.Name;

            this.Index = font.Index;

            this.Family = font.FontFamily;

            this.FontStyleName = font.FontStyleName;

            this.Location = font.Location;

            this.PostScriptName = font.PostScriptName;

            this.FullName = font.FullName;

            this.FullNameNative = font.FullNameNative;

            this.FontType = font.FontType.ToString(); 

        }
    }
}
