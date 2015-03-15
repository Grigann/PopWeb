//-----------------------------------------------------------------------
// <copyright file="Book.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Book entity
    /// </summary>
    public class Book {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        public Book() {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.ReadingSessions = new List<ReadingSession>();
        }

        /// <summary>
        /// Gets or sets the book id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the book title
        /// </summary>
        [DisplayName("Titre")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the book series
        /// </summary>
        [DisplayName("Série")]
        public virtual string BookSeries { get; set; }

        /// <summary>
        /// Gets or sets the book number in the series
        /// </summary>
        [DisplayName("Tome n°")]
        public virtual int? BookNumber { get; set; }

        /// <summary>
        /// Gets or sets the genre
        /// </summary>
        [DisplayName("Genre")]
        public virtual string Genre { get; set; }

        /// <summary>
        /// Gets or sets the book author
        /// </summary>
        [DisplayName("Auteur")]
        public virtual string Writer { get; set; }

        /// <summary>
        /// Gets or sets the publication date
        /// </summary>
        [DisplayName("Date de publication")]
        public virtual string PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the cover file name
        /// </summary>
        [DisplayName("Couverture")]
        public virtual string CoverFileName { get; set; }

        /// <summary>
        /// Gets the medium thumbnail name.
        /// </summary>
        public virtual string MediumThumbName {
            get {
                return ThumbnailHandler.GetMedThumbName(this.CoverFileName);
            }
        }

        /// <summary>
        /// Gets the name of the small thumbnail.
        /// </summary>
        public virtual string SmallThumbName {
            get {
                return ThumbnailHandler.GetSmallThumbName(this.CoverFileName);
            }
        }

        /// <summary>
        /// Gets or sets the summary
        /// </summary>
        [DisplayName("Résumé")]
        public virtual string Summary { get; set; }

        /// <summary>
        /// Gets or sets the wikipedia link
        /// </summary>
        [DisplayName("Lien Wikipedia")]
        public virtual string WikipediaLink { get; set; }

        /// <summary>
        /// Gets the list of reading sessions
        /// </summary>
        public virtual IList<ReadingSession> ReadingSessions { get; protected set; }

        /// <summary>
        /// Gets the list of marked sessions (with a Note)
        /// </summary>
        public virtual IList<ReadingSession> MarkedSessions {
            get {
                return this.ReadingSessions.Where(x => !string.IsNullOrEmpty(x.Note)).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the list of reading session as a JSON string for charts
        /// </summary>
        public virtual string ReadingSessionForCharts { get; protected set; }

        /// <summary>
        /// Gets or sets the reading session ticks
        /// </summary>
        public virtual string ReadingSessionXAxisTicks { get; protected set; }

        /// <summary>
        /// Gets or sets the Y-Axis ticks array
        /// </summary>
        public virtual string ReadingSessionYAxisTicks { get; protected set; }

        /// <summary>
        /// Gets or sets the first chart date
        /// </summary>
        public virtual DateTime FirstChartDate { get; protected set; }

        /// <summary>
        /// Gets or sets the chart interval type
        /// </summary>
        public virtual ChartIntervalType ChartIntervalType { get; protected set; }

        /// <summary>
        /// Gets the session frequency
        /// </summary>
        public virtual SessionFrequency SessionFrequency {
            get {
                return SessionFrequencyComputer<ReadingSession>.Compute(this.ReadingSessions);
            }
        }

        /// <summary>
        /// Gets the first session date
        /// </summary>
        public virtual DateTime? FirstSessionDate {
            get {
                return this.ReadingSessions.Count == 0 
                        ? null
                        : new DateTime?(this.ReadingSessions.Select(x => x.Date).Min());
            }
        }

        /// <summary>
        /// Gets the last session date
        /// </summary>
        public virtual DateTime? LastSessionDate {
            get {
                return this.ReadingSessions.Count == 0
                        ? null
                        : new DateTime?(this.ReadingSessions.Select(x => x.Date).Max());
            }
        }

        /// <summary>
        /// Adds a reading session for this book
        /// </summary>
        /// <param name="readingSession">A reading session</param>
        public virtual void AddReadingSession(ReadingSession readingSession) {
            readingSession.Book = this;
            this.ReadingSessions.Add(readingSession);
        }

        /// <summary>
        /// Initializes the chart information
        /// </summary>
        public virtual void InitializeChartInfos() {
            var chartHelper = new ChartHelper<ReadingSession>();

            this.ChartIntervalType = chartHelper.ComputeIntervalType(this.ReadingSessions);

            var chartSessions = chartHelper.ExtractChartSessions(this.ReadingSessions, this.ChartIntervalType);

            this.FirstChartDate = chartSessions.Count == 0 ? DateTime.Today : chartSessions.Keys.Min();
            this.ReadingSessionForCharts = chartHelper.ComputeSessionsChart(chartSessions);
            this.ReadingSessionXAxisTicks = chartHelper.ComputeXAxisTicks(chartSessions);
            this.ReadingSessionYAxisTicks = chartHelper.ComputeYAxisTicks(chartSessions);
        }
    }
}
