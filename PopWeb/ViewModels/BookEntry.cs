//-----------------------------------------------------------------------
// <copyright file="BookEntry.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    /// <summary>
    /// Book session view model object
    /// </summary>
    public class BookEntry {
        /// <summary>
        /// Gets or sets the session date
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the book title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the book writer
        /// </summary>
        public string Writer { get; set; }

        /// <summary>
        /// Gets or sets the session note
        /// </summary>
        public string Note { get; set; }
    }
}
