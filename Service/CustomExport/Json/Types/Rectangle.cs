/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Rectangle
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Rectangle";

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
        /// ItemLayer
        /// </summary>
        public int ItemLayer;
        
        /// <summary>
        /// GeometricBounds
        /// </summary>
        public dynamic GeometricBounds;
        
        /// <summary>
        /// Notes
        /// </summary>
        private List<Note> Notes = new List<Note>();

        /// <summary>
        /// Links
        /// </summary>
        public List<Image> Images = new List<Image>();

        /// <summary>
        /// TextFrame
        /// </summary>
        /// <param name="TextFrame"></param>
        public Rectangle(InDesignServer.Rectangle rectangle)
        {
            this.Name = rectangle.Name;

            this.Index = rectangle.Index;

            this.Label = rectangle.Label;

            this.Id = rectangle.Id;
            
            this.GeometricBounds = rectangle.GeometricBounds;

            this.Visible = rectangle.Visible;
            
            this.ItemLayer = rectangle.ItemLayer.Id;
            
            this.setImages(rectangle);
        }

        /// <summary>
        /// Images
        /// </summary>
        /// <param name="rectangle"></param>
        void setImages(InDesignServer.Rectangle rectangle)
        {
            foreach(InDesignServer.Image image in rectangle.Images) {

                this.Images.Add(new Image(image));
            }
        }
    }
}
