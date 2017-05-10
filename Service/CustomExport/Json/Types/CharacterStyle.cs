/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    /// <summary>
    /// CharacterStyle
    /// </summary>
    class CharacterStyle
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "CharacterStyle";

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
        public string AppliedFont;

        /// <summary>
        /// FontStyle
        /// </summary>        
        public dynamic  FontStyle;

        /// <summary>
        /// Position
        /// </summary>
        public dynamic Position;

    /// <summary>
    /// CharacterStyle
    /// </summary>
    /// <param name="characterStyle"></param>
    public CharacterStyle(InDesignServer.CharacterStyle characterStyle)
    {
            this.Name = characterStyle.Name;

            this.Index = characterStyle.Index;

            this.Label = characterStyle.Label;

            this.Id = characterStyle.Id;

            this.FontStyle = characterStyle.FontStyle;

            this.Position = characterStyle.Position;
            
            this.AppliedFont = characterStyle.AppliedFont;
            
            dynamic tracking = characterStyle.Tracking;
        }
    }
}
