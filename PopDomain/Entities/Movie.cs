//-----------------------------------------------------------------------
// <copyright file="Movie.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Movie entity
    /// </summary>
    public class Movie {
        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
        /// </summary>
        public Movie() {
            this.WatchingSessions = new List<WatchingSession>();
        }

        /// <summary>
        /// Gets or sets the movie id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the movie title
        /// </summary>
        [DisplayName("Titre")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the genre
        /// </summary>
        [DisplayName("Genre")]
        public virtual string Genre { get; set; }

        /// <summary>
        /// Gets or sets the movie director
        /// </summary>
        [DisplayName("Réalisateur")]
        public virtual string Director { get; set; }

        /// <summary>
        /// Gets or sets the release date
        /// </summary>
        [DisplayName("Date de sortie")]
        public virtual string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the poster file name
        /// </summary>
        [DisplayName("Poster")]
        public virtual string PosterFileName { get; set; }

        /// <summary>
        /// Gets or sets the summary
        /// </summary>
        [DisplayName("Résumé")]
        public virtual string Summary { get; set; }

        /// <summary>
        /// Gets or sets the Wikipedia link
        /// </summary>
        [DisplayName("Lien Wikipedia")]
        public virtual string WikipediaLink { get; set; }

        /// <summary>
        /// Gets the list of watching sessions
        /// </summary>
        public virtual IList<WatchingSession> WatchingSessions { get; protected set; }

        /// <summary>
        /// Adds a watching session for this movie
        /// </summary>
        /// <param name="watchingSession">A watching session</param>
        public virtual void AddWatchingSession(WatchingSession watchingSession) {
            watchingSession.Movie = this;
            this.WatchingSessions.Add(watchingSession);
        }
    }
}
