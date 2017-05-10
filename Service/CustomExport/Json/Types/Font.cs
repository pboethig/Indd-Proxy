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
        /// Index
        /// </summary>
        public int Index;

        /// <summary>
        /// Location
        /// </summary>
        public string Location;


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

            try
            {
                this.FontStyleName = font.FontStyleName;
            }
            catch(System.Exception ex)
            {
                this.FontStyleName = ex.Message;
            }

            try
            {
                this.FontStyleName = font.FontStyleName;
            }
            catch (System.Exception ex)
            {
                this.Location = ex.Message;
            }
            
            this.PostScriptName = font.PostScriptName;

            try
            {
                this.FontStyleName = font.FontStyleName;
            }
            catch (System.Exception ex)
            {
                this.FullName = ex.Message;
            }

            try
            {
                this.FontStyleName = font.FontStyleName;
            }
            catch (System.Exception ex)
            {
                this.FullNameNative = ex.Message;
            }

            try
            {
                this.FontStyleName = font.FontStyleName;
            }
            catch (System.Exception ex)
            {
                this.FontType = ex.Message;
            }
        }
    }
}
