//-----------------------------------------------------------------------
// <copyright file="TvSerieSeason.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;

    /// <summary>
    /// TV serie season entity
    /// </summary>
    public class TvSerieSeason {
        /// <summary>
        /// The TV series
        /// </summary>
        private TvSerie tvSerie;

        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerieSeason"/> class.
        /// </summary>
        public TvSerieSeason() {
            this.Episodes = new List<TvSerieEpisode>();
        }

        /// <summary>
        /// Gets or sets the season id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the season number
        /// </summary>
        [DisplayName("Saison n°")]
        public virtual int Number { get; set; }

        /// <summary>
        /// Gets or sets the release date
        /// </summary>
        [DisplayName("Date de diffusion")]
        public virtual string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the poster file name
        /// </summary>
        [DisplayName("Poster")]
        public virtual string PosterFileName { get; set; }

        /// <summary>
        /// Gets the medium thumbnail name.
        /// </summary>
        public virtual string MediumThumbName {
            get {
                return ThumbnailHandler.GetMedThumbName(this.PosterFileName);
            }
        }

        /// <summary>
        /// Gets the name of the small thumbnail.
        /// </summary>
        public virtual string SmallThumbName {
            get {
                return ThumbnailHandler.GetSmallThumbName(this.PosterFileName);
            }
        }

        /// <summary>
        /// Gets or sets the parent series
        /// </summary>
        public virtual TvSerie TvSerie {
            get {
                return this.tvSerie;
            }

            set {
                this.tvSerie = value;
                this.TvSerieId = value == null ? 0 : value.Id;
            }
        }

        /// <summary>
        /// Gets or sets the parent series id
        /// </summary>
        public virtual int TvSerieId { get; set; }

        /// <summary>
        /// Gets the list of episodes
        /// </summary>
        public virtual IList<TvSerieEpisode> Episodes { get; protected set; }

        /// <summary>
        /// Adds a episode for this season
        /// </summary>
        /// <param name="episode">A TV serie episode</param>
        public virtual void AddEpisode(TvSerieEpisode episode) {
            episode.Season = this;
            this.Episodes.Add(episode);
        }
    }
}
