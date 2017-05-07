/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Color
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Color";

        /// <summary>
        /// Id
        /// </summary>
        public int Id;

        /// <summary>
        /// Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Label
        /// </summary>
        public string Label;
        
        /// <summary>
        /// Index
        /// </summary>
        public int Index;
        /// <summary>
        /// Model
        /// </summary>
        public string Model;

        /// <summary>
        /// ColorSpace
        /// </summary>
        public string Space;

        public Color(InDesignServer.Color color)
        {
            this.Name = color.Name;

            this.Index = color.Index;

            this.Label = color.Label;

            this.Id = color.Id;

            this.Model = color.Model.ToString();

            this.Space = color.Space.ToString();
        }
    }
}
