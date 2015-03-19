//-----------------------------------------------------------------------
// <copyright file="TimelineEntry.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using Domain;
    using Domain.Entities;

    /// <summary>
    /// A timeline entry
    /// </summary>
    public class TimelineEntry {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineEntry"/> class.
        /// </summary>
        /// <param name="session">An entertainment session</param>
        /// <param name="controller">A controller</param>
        public TimelineEntry(IEntertainmentSession session, Controller controller) {
            this.EntryType = session.GetType();
            this.Date = session.Date;

            var today = DateTime.Today;
            var thisWeekLimit = today.StartOfWeek(DayOfWeek.Monday);
            var lastWeekLimit = thisWeekLimit.AddDays(-7);
            var lastMonthLimit = today.AddMonths(-2);
            lastMonthLimit = lastMonthLimit.AddDays(1 - lastMonthLimit.Day);

            if (this.Date >= thisWeekLimit) {
                this.Timeline = "today";
            } else if (this.Date >= lastWeekLimit) {
                this.Timeline = "last-week";
            } else if (this.Date >= lastMonthLimit) {
                this.Timeline = "last-month";
            }

            if (session is ReadingSession) {
                this.Initialize((ReadingSession)session, controller);
            } else if (session is WatchingSession) {
                this.Initialize((WatchingSession)session, controller);
            } else if (session is GamingSession) {
                this.Initialize((GamingSession)session, controller);
            } else if (session is TvWatchingSession) {
                this.Initialize((TvWatchingSession)session, controller);
            }
        }

        /// <summary>
        /// Gets or sets the timeline
        /// </summary>
        public string Timeline { get; set; }

        /// <summary>
        /// Gets or sets the view url
        /// </summary>
        public string ViewUrl { get; set; }

        /// <summary>
        /// Gets or sets the entry title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the details
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the image URL
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the image title
        /// </summary>
        public string ImageAlt { get; set; }

        /// <summary>
        /// Gets or sets the entry type
        /// </summary>
        [ScriptIgnore]
        public Type EntryType { get; private set; }

        /// <summary>
        /// Gets or sets the entry date
        /// </summary>
        [ScriptIgnore]
        public DateTime? Date { get; private set; }

        /// <summary>
        /// Adds a details to the entry
        /// </summary>
        /// <param name="details">A details to add</param>
        public TimelineEntry AddDetails(string details) {
            this.Details += "<br />" + details;
            return this;
        }

        /// <summary>
        /// Escapes the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The HTML escaped value, with also ' convert to &#39;</returns>
        private static string EscapeString(string value) {
            return HttpUtility.HtmlEncode(value);
        }

        /// <summary>
        /// Initializes the entry with a reading session
        /// </summary>
        /// <param name="readingSession">A reading session</param>
        /// <param name="controller">A controller</param>
        private void Initialize(ReadingSession readingSession, Controller controller) {
            this.ViewUrl = controller.Url.Action("View", "Books", new { id = readingSession.Book.Id });
            this.Title = EscapeString(readingSession.Book.Title);
            this.Details = string.IsNullOrEmpty(readingSession.Book.PublicationDate)
                    ? EscapeString(readingSession.Book.Writer)
                    : EscapeString(readingSession.Book.Writer + ", " + readingSession.Book.PublicationDate);
            this.ImageUrl = string.IsNullOrEmpty(readingSession.Book.CoverFileName)
                    ? "/Content/images/no_cover.jpg"
                    : "/Content/images/books/" + readingSession.Book.MediumThumbName;
            this.ImageAlt = EscapeString("Couverture de " + readingSession.Book.Title);
        }

        /// <summary>
        /// Initializes the entry with a watching session
        /// </summary>
        /// <param name="watchingSession">A watching session</param>
        /// <param name="controller">A controller</param>
        private void Initialize(WatchingSession watchingSession, Controller controller) {
            this.ViewUrl = controller.Url.Action("View", "Movies", new { id = watchingSession.Movie.Id });
            this.Title = EscapeString(watchingSession.Movie.Title);
            this.Details = string.IsNullOrEmpty(watchingSession.Movie.ReleaseDate)
                    ? EscapeString(watchingSession.Movie.Director)
                    : EscapeString(watchingSession.Movie.Director + ", " + watchingSession.Movie.ReleaseDate);
            this.ImageUrl = string.IsNullOrEmpty(watchingSession.Movie.PosterFileName)
                    ? "/Content/images/no_cover.jpg"
                    : "/Content/images/movies/" + watchingSession.Movie.MediumThumbName;
            this.ImageAlt = EscapeString("Poster de " + watchingSession.Movie.Title);
        }

        /// <summary>
        /// Initializes the entry with a gaming session
        /// </summary>
        /// <param name="gamingSession">A gaming session</param>
        /// <param name="controller">A controller</param>
        private void Initialize(GamingSession gamingSession, Controller controller) {
            this.ViewUrl = controller.Url.Action("View", "Games", new { id = gamingSession.Game.Id });
            this.Title = EscapeString(gamingSession.Game.Title);
            this.Details = string.IsNullOrEmpty(gamingSession.Game.ReleaseDate)
                    ? EscapeString(gamingSession.Game.Developper)
                    : EscapeString(gamingSession.Game.Developper + ", " + gamingSession.Game.ReleaseDate);
            this.ImageUrl = string.IsNullOrEmpty(gamingSession.Game.CoverFileName)
                    ? "/Content/images/no_cover.jpg"
                    : "/Content/images/games/" + gamingSession.Game.MediumThumbName;
            this.ImageAlt = EscapeString("Couverture de " + gamingSession.Game.Title);
        }

        /// <summary>
        /// Initializes the entry with a TV watching session
        /// </summary>
        /// <param name="tvWatchingSession">A TV watching session session</param>
        /// <param name="controller">A controller</param>
        private void Initialize(TvWatchingSession tvWatchingSession, Controller controller) {
            var episode = tvWatchingSession.Episode;
            var season = episode.Season;
            var serie = season.TvSerie;

            this.ViewUrl = controller.Url.Action("View", "TvSeries", new { id = serie.Id }) + "#tab_saison-" + season.Number;
            this.Title = EscapeString(serie.Title) + " - Saison " + season.Number;
            this.Details = "Ep. " + tvWatchingSession.Episode.Number + " - <em>\"" + EscapeString(episode.Title) + "\"</em>";
            this.ImageUrl = string.IsNullOrEmpty(season.PosterFileName)
                    ? "/Content/images/no_cover.jpg"
                    : "/Content/images/tvSeries/" + season.MediumThumbName;
            this.ImageAlt = this.Title;
        }
    }
}