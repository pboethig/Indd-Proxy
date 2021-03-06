﻿/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Paragraph
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Paragraph";

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
        public Paragraph(InDesignServer.Paragraph paragraph)
        {
            this.FontStyle = paragraph.FontStyle;

            this.Index = paragraph.Index;

            this.AppliedParagraphStyle = new ParagraphStyle(paragraph.AppliedParagraphStyle);

            this.Contents = paragraph.Contents;

            this.AppliedFont = new Font(paragraph.AppliedFont);

            
        
            this.AppliedCharacterStyle = new CharacterStyle(paragraph.AppliedCharacterStyle);
            
            this.setGraphics(paragraph);

        }

        void setGraphics(InDesignServer.Paragraph textFrame)
        {
            foreach (InDesignServer.Image image in textFrame.AllGraphics)
            {
                this.Images.Add(new Image(image));
            }
        }


    }
}
