//-----------------------------------------------------------------------
// <copyright file="TvSerieEpisodeEntry.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    /// <summary>
    /// TV series episode session view model object
    /// </summary>
    public class TvSerieEpisodeEntry {
        /// <summary>
        /// Gets or sets the session date
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the series title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the season number
        /// </summary>
        public int SeasonNb { get; set; }

        /// <summary>
        /// Gets or sets the episode number
        /// </summary>
        public int EpisodeNb { get; set; }
        
        /// <summary>
        /// Gets or sets the episode title
        /// </summary>
        public string EpisodeTitle { get; set; }
    }
}
