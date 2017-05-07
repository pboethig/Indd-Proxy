/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Page
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Page";

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
        /// pageColor
        /// </summary>
        public dynamic PageColor;

        /// <summary>
        /// bounds
        /// </summary>
        public dynamic Bounds;

        /// <summary>
        /// documentOffset
        /// </summary>
        public int DocumentOffset;

        /// <summary>
        /// optional page for HTML5 pagination
        /// </summary>
        public bool OptionalPage;
        
        /// <summary>
        /// The side of the binding spine on which to place the page within the spread.
        // PageSideOptions.RIGHT_HAND
        // PageSideOptions.LEFT_HAND
        // PageSideOptions.SINGLE_SIDED
        /// </summary>
        public dynamic Side;

        /// <summary>
        /// List of textFrames
        /// </summary>
        public List<Rectangle> Rectangles = new List<Rectangle>();

        /// <summary>
        /// List of textFrames
        /// </summary>
        public List<TextFrame> TextFrames = new List<TextFrame>();

        /// <summary>
        /// Saves Page
        /// </summary>
        /// <param name="page"></param>
        public Page(InDesignServer.Page page)
        {
            this.Name = page.Name;

            this.Index = page.Index;

            this.Label = page.Label;

            this.Id = page.Id;

            this.PageColor = page.PageColor;

            this.Bounds = page.Bounds;

            this.DocumentOffset = page.DocumentOffset;

            this.OptionalPage = page.OptionalPage;

            this.Side = page.Side;
            
            this.setTextFrames(page);

            this.setRectangles(page);
        }

        /// <summary>
        /// Add TextFrames
        /// </summary>
        /// <param name="page"></param>
        private void setTextFrames(InDesignServer.Page page)
        {
            foreach(InDesignServer.TextFrame textFrame in page.TextFrames)
            {
                TextFrame _textFrame = new TextFrame(textFrame);

                this.TextFrames.Add(_textFrame);                  
            }
        }

        /// <summary>
        /// Add TextFrames
        /// </summary>
        /// <param name="page"></param>
        private void setRectangles(InDesignServer.Page page)
        {
            foreach (InDesignServer.Rectangle rectangle in page.Rectangles)
            {
                Rectangle _rectangle = new Rectangle(rectangle);

                this.Rectangles.Add(_rectangle);
            }
        }
    }
}