/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Layer
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Note";

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
        /// Visible
        /// </summary>
        public bool Visible;
        
        /// <summary>
        /// Note
        /// </summary>
        /// <param name="note"></param>
        public Layer(InDesignServer.Layer layer)
        {
            this.Name = layer.Name;

            this.Index = layer.Index;

            this.Label = layer.Label;

            this.Id = layer.Id;

            this.Visible = layer.Visible;
        }
    }
}
