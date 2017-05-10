/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class TextFrame
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "TextFrame";

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
        /// List of Paragraphs
        /// </summary>
        public List<Paragraph> Paragraphs = new List<Paragraph>();


        /// <summary>
        /// TextFrame
        /// </summary>
        /// <param name="TextFrame"></param>
        public TextFrame(InDesignServer.TextFrame textFrame)
        {
            this.Name = textFrame.Name;

            this.Index = textFrame.Index;

            this.Label = textFrame.Label;

            this.Id = textFrame.Id;
            
            this.GeometricBounds = textFrame.GeometricBounds;

            this.Visible = textFrame.Visible;
            
            this.ItemLayer = textFrame.ItemLayer.Id;
            
            this.setNotes(textFrame);
            
            this.setParagraphs(textFrame);
        }

        /// <summary>
        /// Set Paragraphs
        /// </summary>
        /// <param name="textFrame"></param>
        void setParagraphs(InDesignServer.TextFrame textFrame)
        {
            foreach (InDesignServer.Paragraph paragraph in textFrame.Paragraphs)
            {
                this.Paragraphs.Add(new Paragraph(paragraph));
            }
        }
        
        /// <summary>
        /// Add Notes
        /// </summary>
        /// <param name="textFrame"></param>
        private void setNotes(InDesignServer.TextFrame textFrame)
        {
            foreach (InDesignServer.Note note in textFrame.Notes)
            {
                Note _note = new Note(note);

                this.Notes.Add(_note);
            }                     
        } 
    }
}
