/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Link
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Link";

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
        /// FullPath
        /// </summary>
        public string FilePath;

        /// <summary>
        /// AssetURL
        /// </summary>
        public string AssetURL;

        /// <summary>
        /// 
        /// </summary>
        public string AssetID;

        /// <summary>
        /// Note
        /// </summary>
        /// <param name="note"></param>
        public Link(InDesignServer.Link link)
        {
            this.Name = link.Name;

            this.Index = link.Index;

            this.Label = link.Label;

            this.Id = link.Id;

            this.FilePath = link.FilePath;

            this.AssetID = link.AssetURL;

            this.AssetURL = link.AssetID;
        }
    }
}
