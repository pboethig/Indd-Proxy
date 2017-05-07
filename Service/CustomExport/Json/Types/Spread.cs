/// <summary>
/// DataType for exported page
/// </summary>
namespace Indd.Service.CustomExport.Json.Types
{
    using System.Collections.Generic;

    class Spread
    {
        /// <summary>
        /// Rectangle
        /// </summary>
        public string Type = "Spread";

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
        /// PageTransitionDirection
        /// </summary>
        public string PageTransitionDirection;

        /// <summary>
        /// PageTransitionDuration
        /// </summary>
        public string PageTransitionDuration;

        /// <summary>
        /// PageTransitionType
        /// </summary>
        public string PageTransitionType;
        /// <summary>
        /// Assigned Pages
        /// </summary>
        public List<Page> Pages = new List<Page>();
        
        /// <summary>
        /// Spread
        /// </summary>
        /// <param name="spread"></param>
        public Spread(InDesignServer.Spread spread)
        {
            this.Name = spread.Name;

            this.Index = spread.Index;

            this.Label = spread.Label;

            this.Id = spread.Id;
            
            this.buildPages(spread);

            this.PageTransitionDirection = spread.PageTransitionDirection.ToString();

            this.PageTransitionDuration = spread.PageTransitionDuration.ToString();

            this.PageTransitionType= spread.PageTransitionType.ToString();
        }

        /// <summary>
        /// Builds pages
        /// </summary>
        public void buildPages(InDesignServer.Spread spread)
        {
            foreach (InDesignServer.Page page in spread.Pages)
            {
                Page _page = new Page(page);

                this.Pages.Add(_page);
            }
        }
    }
}
