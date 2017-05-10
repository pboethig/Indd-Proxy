/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Document
    {
        public string Type = "Document";

        /// <summary>
        /// Id
        /// </summary>
        public int Id;

        /// <summary>
        /// Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Fullname
        /// </summary>
        public string FullName;

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
        /// Horizontal MessurementUnits
        /// </summary>
        public string HorizontalMeasurementUnits;

        /// <summary>
        /// Vertical MessurementUnits
        /// </summary>
        public string VerticalMeasurementUnits;

        /// <summary>
        /// Point per inch
        /// </summary>
        public double PointsPerInch;

        /// <summary>
        /// TextSizeMeasurementUnits
        /// </summary>
        public string TextSizeMeasurementUnits;

        /// <summary>
        /// TypographicMeasurementUnits
        /// </summary>
        public string TypographicMeasurementUnits;

        /// <summary>
        /// Layers
        /// </summary>
        public List<Layer> Layers = new List<Layer>();

        /// <summary>
        /// Spreadlist
        /// </summary>
        public List<Spread> Spreads = new List<Spread>();

        /// <summary>
        /// Color list
        /// </summary>
        public List<Color> Colors = new List<Color>();
        
        /// <summary>
        /// CharacterStyles
        /// </summary>
        public List<CharacterStyle> CharacterStyles = new List<CharacterStyle>();

        
        /// </summary>
        /// <param name="document"></param>
        public Document(InDesignServer.Document document)
        {
            this.Name = document.Name;

            this.Id = document.Id;

            this.Label = document.Label;

            this.Index = document.Index;

            this.FullName = document.FullName;
            
            this.HorizontalMeasurementUnits = document.ViewPreferences.HorizontalMeasurementUnits.ToString();

            this.VerticalMeasurementUnits = document.ViewPreferences.VerticalMeasurementUnits.ToString();

            this.PointsPerInch = document.ViewPreferences.PointsPerInch;

            this.TextSizeMeasurementUnits=document.ViewPreferences.TextSizeMeasurementUnits.ToString();

            this.TypographicMeasurementUnits = document.ViewPreferences.TypographicMeasurementUnits.ToString();

            this.Visible=document.Visible;

            this.setLayers(document);

            this.setSpreads(document);

            this.setColors(document);
            
            this.setCharacterStyles(document);
        }
        
        /// <summary>
        /// Set CharacterStyles
        /// </summary>
        /// <param name="document"></param>
        void setCharacterStyles(InDesignServer.Document document)
        {
            foreach (InDesignServer.CharacterStyle characterStyle in document.AllCharacterStyles)
            {
                this.CharacterStyles.Add(new CharacterStyle(characterStyle));
            }
        }

     

        /// <summary>
        /// Add Layers
        /// </summary>
        /// <param name="document"></param>
        void setLayers(InDesignServer.Document document)
        {
            foreach (InDesignServer.Layer layer in document.Layers)
            {
                Layer _layer = new Layer(layer);

                this.Layers.Add(_layer);
            } 
        }

        /// <summary>
        /// Set spreads
        /// </summary>
        /// <param name="document"></param>
        void setSpreads(InDesignServer.Document document)
        {
            foreach (InDesignServer.Spread spread in document.Spreads)
            {
                Spread jsonSpread = new Spread(spread);

                this.Spreads.Add(jsonSpread);
            } 
        }

        /// <summary>
        /// Set Colors
        /// </summary>
        /// <param name="document"></param>
        void setColors(InDesignServer.Document document)
        {
            foreach (InDesignServer.Color color in document.Colors)
            {
                Color jsonColor = new Color(color);

                this.Colors.Add(jsonColor);
            }
        }

    }
}