//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Game entity
    /// </summary>
    public class Game {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game() {
            this.GamingSessions = new List<GamingSession>();
        }

        /// <summary>
        /// Gets or sets the game id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the game title
        /// </summary>
        [DisplayName("Titre")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the genre
        /// </summary>
        [DisplayName("Genre")]
        public virtual string Genre { get; set; }

        /// <summary>
        /// Gets or sets the gaming platform
        /// </summary>
        [DisplayName("Support")]
        public virtual string GamingPlatform { get; set; }

        /// <summary>
        /// Gets or sets the game developper
        /// </summary>
        [DisplayName("Développeur")]
        public virtual string Developper { get; set; }

        /// <summary>
        /// Gets or sets the release date
        /// </summary>
        [DisplayName("Date de sortie")]
        public virtual string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the cover file name
        /// </summary>
        [DisplayName("Couverture")]
        public virtual string CoverFileName { get; set; }

        /// <summary>
        /// Gets or sets the summary
        /// </summary>
        [DisplayName("Résumé")]
        public virtual string Summary { get; set; }

        /// <summary>
        /// Gets or sets the review
        /// </summary>
        [DisplayName("Mon avis")]
        public virtual string Review { get; set; }

        /// <summary>
        /// Gets or sets the Wikipedia link
        /// </summary>
        [DisplayName("Lien Wikipedia")]
        public virtual string WikipediaLink { get; set; }

        /// <summary>
        /// Gets the list of gaming sessions
        /// </summary>
        public virtual IList<GamingSession> GamingSessions { get; protected set; }

        /// <summary>
        /// Gets the list of marked sessions (with a Note)
        /// </summary>
        public virtual IList<GamingSession> MarkedSessions {
            get {
                return this.GamingSessions.Where(x => !string.IsNullOrEmpty(x.Note)).ToList();
            }
        }

        /// <summary>
        /// Gets the frequency of the sessions
        /// </summary>
        public virtual SessionFrequency SessionFrequency {
            get {
                return SessionFrequencyComputer<GamingSession>.Compute(this.GamingSessions);
            }
        }

        /// <summary>
        /// Gets or sets the list of gaming session as a JSON string for charts
        /// </summary>
        public virtual string GamingSessionForCharts { get; protected set; }

        /// <summary>
        /// Gets or sets the gaming session ticks
        /// </summary>
        public virtual string GamingSessionXAxisTicks { get; protected set; }

        /// <summary>
        /// Gets or sets the Y-Axis ticks array
        /// </summary>
        public virtual string GamingSessionYAxisTicks { get; protected set; }

        /// <summary>
        /// Gets or sets the first chart date
        /// </summary>
        public virtual DateTime FirstChartDate { get; protected set; }

        /// <summary>
        /// Gets or sets the chart interval type
        /// </summary>
        public virtual ChartIntervalType ChartIntervalType { get; protected set; }

        /// <summary>
        /// Gets the first session date
        /// </summary>
        public virtual DateTime? FirstSessionDate {
            get {
                return this.GamingSessions.Count == 0
                        ? null
                        : new DateTime?(this.GamingSessions.Select(x => x.Date).Min());
            }
        }

        /// <summary>
        /// Gets the last session date
        /// </summary>
        public virtual DateTime? LastSessionDate {
            get {
                return this.GamingSessions.Count == 0
                        ? null
                        : new DateTime?(this.GamingSessions.Select(x => x.Date).Max());
            }
        }

        /// <summary>
        /// Adds a gaming session for this game
        /// </summary>
        /// <param name="gamingSession">A gaming session</param>
        public virtual void AddGamingSession(GamingSession gamingSession) {
            gamingSession.Game = this;
            this.GamingSessions.Add(gamingSession);
        }

        /// <summary>
        /// Initializes the chart information
        /// </summary>
        public virtual void InitializeChartInfos() {
            var chartHelper = new ChartHelper<GamingSession>();

            this.ChartIntervalType = chartHelper.ComputeIntervalType(this.GamingSessions);

            var chartSessions = chartHelper.ExtractChartSessions(this.GamingSessions, this.ChartIntervalType);

            this.FirstChartDate = chartSessions.Count == 0 ? DateTime.Today : chartSessions.Keys.Min();
            this.GamingSessionForCharts = chartHelper.ComputeSessionsChart(chartSessions);
            this.GamingSessionXAxisTicks = chartHelper.ComputeXAxisTicks(chartSessions);
            this.GamingSessionYAxisTicks = chartHelper.ComputeYAxisTicks(chartSessions);
        }
    }
}
