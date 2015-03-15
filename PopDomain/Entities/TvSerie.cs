//-----------------------------------------------------------------------
// <copyright file="TvSerie.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// TV serie entity
    /// </summary>
    public class TvSerie {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerie"/> class.
        /// </summary>
        public TvSerie() {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Seasons = new List<TvSerieSeason>();
        }

        /// <summary>
        /// Gets or sets the series id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the series title
        /// </summary>
        [DisplayName("Titre")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the genre
        /// </summary>
        [DisplayName("Genre")]
        public virtual string Genre { get; set; }

        /// <summary>
        /// Gets or sets the series creator
        /// </summary>
        [DisplayName("Créateur(s)")]
        public virtual string Creator { get; set; }

        /// <summary>
        /// Gets or sets the release date
        /// </summary>
        [DisplayName("Date de diffusion")]
        public virtual string ReleaseDate { get; set; }

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
        /// Gets the list of seasons
        /// </summary>
        public virtual IList<TvSerieSeason> Seasons { get; protected set; }

        /// <summary>
        /// Gets the total number of episodes in the series
        /// </summary>
        public virtual int EpisodeCount {
            get {
                if (this.Seasons.Count == 0) {
                    return 0;
                }

                return this.Seasons
                    .Select(x => x.Episodes.Count)
                    .Aggregate((total, x) => total + x);
            }
        }

        /// <summary>
        /// Gets or sets the list of watching session as a JSON string for charts
        /// </summary>
        public virtual string WatchingSessionForCharts { get; protected set; }

        /// <summary>
        /// Gets or sets the watching session ticks
        /// </summary>
        public virtual string WatchingSessionXAxisTicks { get; protected set; }

        /// <summary>
        /// Gets or sets the Y-Axis ticks array
        /// </summary>
        public virtual string WatchingSessionYAxisTicks { get; protected set; }

        /// <summary>
        /// Gets or sets the first chart date
        /// </summary>
        public virtual DateTime FirstChartDate { get; protected set; }

        /// <summary>
        /// Gets or sets the chart interval type
        /// </summary>
        public virtual ChartIntervalType ChartIntervalType { get; protected set; }

        /// <summary>
        /// Adds a season for this series
        /// </summary>
        /// <param name="season">A TV serie season</param>
        public virtual void AddSeason(TvSerieSeason season) {
            season.TvSerie = this;
            this.Seasons.Add(season);
        }

        /// <summary>
        /// Initializes the chart information
        /// </summary>
        public virtual void InitializeChartInfos() {
            var chartHelper = new ChartHelper<TvWatchingSession>();

            var watchingSessions = this.Seasons.SelectMany(x => x.Episodes).SelectMany(x => x.WatchingSessions).ToList();
            this.ChartIntervalType = chartHelper.ComputeIntervalType(watchingSessions);

            var chartSessions = chartHelper.ExtractChartSessions(watchingSessions, this.ChartIntervalType);

            this.FirstChartDate = chartSessions.Count == 0 ? DateTime.Today : chartSessions.Keys.Min();
            this.WatchingSessionForCharts = chartHelper.ComputeSessionsChart(chartSessions);
            this.WatchingSessionXAxisTicks = chartHelper.ComputeXAxisTicks(chartSessions);
            this.WatchingSessionYAxisTicks = chartHelper.ComputeYAxisTicks(chartSessions);
        }
    }
}
