/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Text
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Text";

        /// <summary>
        /// Contents
        /// </summary>
        public string Contents;

        /// <summary>
        /// Name
        /// </summary>
        public string FontStyle;
        
        /// <summary>
        /// Index
        /// </summary>
        public int Index;

        /// <summary>
        /// ParagraphStyle
        /// </summary>
        public ParagraphStyle AppliedParagraphStyle;

        /// <summary>
        /// ChacaterStyles
        /// </summary>
        public CharacterStyle AppliedCharacterStyle;
        
        /// <summary>
        /// AppliedFont
        /// </summary>
        public Font AppliedFont;

        /// <summary>
        /// Links
        /// </summary>
        public List<Image> Images = new List<Image>();

        /// <summary>
        /// Note
        /// </summary>
        /// <param name="note"></param>
        public Text(InDesignServer.Text text)
        {
            this.FontStyle = text.FontStyle;

            this.Index = text.Index;

            this.AppliedParagraphStyle = new ParagraphStyle(text.AppliedParagraphStyle);

            this.Contents = text.Contents;

            this.AppliedFont = new Font(text.AppliedFont);
            
            this.AppliedCharacterStyle = new CharacterStyle(text.AppliedCharacterStyle);
            
        }

        

    }
}
