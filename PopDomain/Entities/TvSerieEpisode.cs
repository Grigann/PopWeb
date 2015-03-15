//-----------------------------------------------------------------------
// <copyright file="TvSerieEpisode.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Script.Serialization;

    /// <summary>
    /// TV serie episode entity
    /// </summary>
    public class TvSerieEpisode {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerieEpisode"/> class.
        /// </summary>
        public TvSerieEpisode() {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.WatchingSessions = new List<TvWatchingSession>();
        }

        /// <summary>
        /// The TV series season
        /// </summary>
        private TvSerieSeason season;

        /// <summary>
        /// Gets or sets the episode id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the episode title
        /// </summary>
        [DisplayName("Titre")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the episode number
        /// </summary>
        [DisplayName("Episode n°")]
        public virtual int Number { get; set; }

        /// <summary>
        /// Gets or sets the episode director
        /// </summary>
        [DisplayName("Réalisateur")]
        public virtual string Director { get; set; }

        /// <summary>
        /// Gets or sets the parent season
        /// </summary>
        [ScriptIgnore]
        public virtual TvSerieSeason Season {
            get {
                return this.season;
            }

            set {
                this.season = value;
                this.SeasonId = value == null ? 0 : value.Id;
            }
        }

        /// <summary>
        /// Gets or sets the parent season id
        /// </summary>
        public virtual int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the parent series id
        /// </summary>
        public virtual int TvSerieId { get; set; }

        /// <summary>
        /// Gets the list of watching sessions
        /// </summary>
        public virtual IList<TvWatchingSession> WatchingSessions { get; protected set; }

        /// <summary>
        /// Adds a watching session for this episode
        /// </summary>
        /// <param name="watchingSession">A reading session</param>
        public virtual void AddWatchingSession(TvWatchingSession watchingSession) {
            watchingSession.Episode = this;
            this.WatchingSessions.Add(watchingSession);
        }
    }
}
