//-----------------------------------------------------------------------
// <copyright file="MovieEntry.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    /// <summary>
    /// Movie session view model object
    /// </summary>
    public class MovieEntry {
        /// <summary>
        /// Gets or sets the session date
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the movie title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the movie director
        /// </summary>
        public string Director { get; set; }
    }
}
