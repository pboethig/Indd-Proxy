/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    /// <summary>
    /// ParagraphStyle
    /// </summary>
    class ParagraphStyle
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "ParagraphStyle";

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
        /// Font
        /// </summary>
        public Font AppliedFont;

        /// <summary>
        /// Note
        /// </summary>
        /// <param name="paragraphStyle"></param>
        public ParagraphStyle(InDesignServer.ParagraphStyle paragraphStyle)
        {
            this.Name = paragraphStyle.Name;

            this.Index = paragraphStyle.Index;

            this.Label = paragraphStyle.Label;

            this.Id = paragraphStyle.Id;

            this.AppliedFont = new Font(paragraphStyle.AppliedFont);
        }
    }
}
