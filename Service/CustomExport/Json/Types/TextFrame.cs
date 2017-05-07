﻿/// <summary>
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
        /// Contents
        /// </summary>
        public string Contents;
        
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
        public TextFrame(InDesignServer.TextFrame textFrame)
        {
            this.Name = textFrame.Name;

            this.Index = textFrame.Index;

            this.Label = textFrame.Label;

            this.Id = textFrame.Id;

            this.Contents = textFrame.Contents;

            this.GeometricBounds = textFrame.GeometricBounds;

            this.Visible = textFrame.Visible;
            
            this.ItemLayer = textFrame.ItemLayer.Id;
            
            this.setGraphics(textFrame);

            this.setNotes(textFrame);
        }

        void setGraphics(InDesignServer.TextFrame textFrame)
        {
            foreach (InDesignServer.Image image in textFrame.AllGraphics)
            {
                this.Images.Add(new Image(image));
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
