/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Image
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Image";

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
        /// ItemLayer
        /// </summary>
        public int ItemLayer;

        /// <summary>
        /// Visible
        /// </summary>
        public bool Visible;

        /// <summary>
        /// Link
        /// </summary>
        public Link Link;
        
        /// <summary>
        /// Note
        /// </summary>
        /// <param name="note"></param>
        public Image(InDesignServer.Image image)
        {
            this.Name = image.Name;

            this.Index = image.Index;

            this.Label = image.Label;

            this.Id = image.Id;

            this.ItemLayer = image.ItemLayer.Id;

            this.Link = new Link(image.ItemLink);
            
            this.Visible = image.Visible;
        }
    }
}
