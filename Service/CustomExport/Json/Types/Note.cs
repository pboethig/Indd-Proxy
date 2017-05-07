/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Note
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
        /// CreationDate
        /// </summary>
        public string CreationDate;

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName;

        /// <summary>
        /// InsertionPoints
        /// </summary>
        public dynamic InsertionPoints;
        

        /// <summary>
        /// Note
        /// </summary>
        /// <param name="note"></param>
        public Note(InDesignServer.Note note)
        {
            this.Name = note.Name;

            this.Index = note.Index;

            this.Label = note.Label;

            this.Id = note.Id;

            this.CreationDate = note.CreationDate.ToLongDateString();

            this.UserName = note.UserName;

            this.InsertionPoints = note.InsertionPoints;
            
            this.setTexts(note);
        }

        void setTexts(InDesignServer.Note note)
        {
            
        }
    }
}
