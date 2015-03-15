//-----------------------------------------------------------------------
// <copyright file="GameEntry.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    /// <summary>
    /// Gaming session view model object
    /// </summary>
    public class GameEntry {
        /// <summary>
        /// Gets or sets the session date
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the game title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the game developper
        /// </summary>
        public string Developper { get; set; }

        /// <summary>
        /// Gets or sets the note
        /// </summary>
        public string Note { get; set; }
    }
}
