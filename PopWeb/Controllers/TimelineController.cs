//-----------------------------------------------------------------------
// <copyright file="TimelineController.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Domain;
    using Domain.Entities;

    using ViewModels;

    /// <summary>
    /// Timeline controller
    /// </summary>
    public class TimelineController : Controller {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        // ReSharper disable once FunctionComplexityOverflow
        public ActionResult Index() {
            var today = DateTime.Today;
            var thisWeekLimit = today.StartOfWeek(DayOfWeek.Monday);
            var lastWeekLimit = thisWeekLimit.AddDays(-7);
            var lastMonthLimit = today.AddMonths(-2);
            lastMonthLimit = lastMonthLimit.AddDays(1 - lastMonthLimit.Day);

            var thisWeek = new List<IEntertainmentSession>();
            var lastWeek = new List<IEntertainmentSession>();
            var lastMonth = new List<IEntertainmentSession>();

            string[] seriesTitles;
            string[] booksTitles;
            string[] moviesTitles;
            string[] gamesTitles;

            using (var uow = new UnitOfWork(false)) {
                seriesTitles = uow.TvSeries.All().OrderBy(x => x.Title).Select(x => x.Title).ToArray();
                booksTitles = uow.Books.All().OrderBy(x => x.Title).Select(x => x.Title).ToArray();
                moviesTitles = uow.Movies.All().OrderBy(x => x.Title).Select(x => x.Title).ToArray();
                gamesTitles = uow.Games.All().OrderBy(x => x.Title).Select(x => x.Title).ToArray();

                var readingSessions = uow.Books.All().SelectMany(x => x.ReadingSessions).Where(x => x.Date >= lastMonthLimit).ToList();
                var watchingSessions = uow.Movies.All().SelectMany(x => x.WatchingSessions).Where(x => x.Date >= lastMonthLimit).ToList();
                var gamingSessions = uow.Games.All().SelectMany(x => x.GamingSessions).Where(x => x.Date >= lastMonthLimit).ToList();
                var tvSessions = uow.TvSeries.All()
                        .SelectMany(x => x.Seasons)
                        .SelectMany(x => x.Episodes)
                        .SelectMany(x => x.WatchingSessions)
                        .Where(x => x.Date >= lastMonthLimit)
                        .ToList();

                thisWeek = thisWeek
                    .Concat(readingSessions
                        .Where(x => x.Date >= thisWeekLimit)
                        .GroupBy(x => x.Book.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(watchingSessions
                        .Where(x => x.Date >= thisWeekLimit)
                        .GroupBy(x => x.Movie.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(gamingSessions
                        .Where(x => x.Date >= thisWeekLimit)
                        .GroupBy(x => x.Game.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(tvSessions
                        .Where(x => x.Date >= thisWeekLimit)
                        .GroupBy(x => x.Episode.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .ToList();

                lastWeek = lastWeek
                    .Concat(readingSessions
                        .Where(x => x.Date >= lastWeekLimit && x.Date < thisWeekLimit)
                        .GroupBy(x => x.Book.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(watchingSessions
                        .Where(x => x.Date >= lastWeekLimit && x.Date < thisWeekLimit)
                        .GroupBy(x => x.Movie.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(gamingSessions
                        .Where(x => x.Date >= lastWeekLimit && x.Date < thisWeekLimit)
                        .GroupBy(x => x.Game.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(tvSessions
                        .Where(x => x.Date >= lastWeekLimit && x.Date < thisWeekLimit)
                        .GroupBy(x => x.Episode.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .ToList();

                lastMonth = lastMonth
                    .Concat(readingSessions
                        .Where(x => x.Date >= lastMonthLimit && x.Date < lastWeekLimit)
                        .GroupBy(x => x.Book.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(watchingSessions
                        .Where(x => x.Date >= lastMonthLimit && x.Date < lastWeekLimit)
                        .GroupBy(x => x.Movie.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(gamingSessions
                        .Where(x => x.Date >= lastMonthLimit && x.Date < lastWeekLimit)
                        .GroupBy(x => x.Game.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .Concat(tvSessions
                        .Where(x => x.Date >= lastMonthLimit && x.Date < lastWeekLimit)
                        .GroupBy(x => x.Episode.Id)
                        .Select(x => x.OrderBy(y => y.Date).Last()))
                    .ToList();
            }

            var thisWeekEntries = thisWeek
                    .Where(x => x.GetType() != typeof(TvWatchingSession))
                    .Select(x => new TimelineEntry(x, this))
                    .Concat(thisWeek
                            .Where(x => x.GetType() == typeof(TvWatchingSession))
                            .Select(x => new TimelineEntry(x, this))
                            .GroupBy(x => x.Title)
                            .Select(x => {
                                var session = x.OrderBy(y => y.Date).Last();
                                session.Details = x.Aggregate((final, current) => final.AddDetails(current.Details)).Details;
                                return session;
                            }))
                    .OrderByDescending(x => x.Title)
                    .ThenByDescending(x => x.Date);

            var lastWeekEntries = lastWeek
                    .Where(x => x.GetType() != typeof(TvWatchingSession))
                    .Select(x => new TimelineEntry(x, this))
                    .Concat(lastWeek
                            .Where(x => x.GetType() == typeof(TvWatchingSession))
                            .Select(x => new TimelineEntry(x, this))
                            .GroupBy(x => x.Title)
                            .Select(x => {
                                var session = x.OrderBy(y => y.Date).Last();
                                session.Details = x.Aggregate((final, current) => final.AddDetails(current.Details)).Details;
                                return session;
                            }))
                    .OrderByDescending(x => x.Title)
                    .ThenByDescending(x => x.Date);

            var lastMonthEntries = lastMonth
                    .Where(x => x.GetType() != typeof(TvWatchingSession))
                    .Select(x => new TimelineEntry(x, this))
                    .Concat(lastMonth
                            .Where(x => x.GetType() == typeof(TvWatchingSession))
                            .Select(x => new TimelineEntry(x, this))
                            .GroupBy(x => x.Title)
                            .Select(x => {
                                var session = x.OrderBy(y => y.Date).Last();
                                session.Details = x.Aggregate((final, current) => final.AddDetails(current.Details)).Details;
                                return session;
                            }))
                    .OrderByDescending(x => x.Title)
                    .ThenByDescending(x => x.Date);

            var timelineDetails = new TimelineDetails() {
                ThisWeekLimit = thisWeekLimit,
                LastWeekLimit = lastWeekLimit,
                LastMonthLimit = lastMonthLimit,
                ThisWeek = thisWeekEntries.ToList(),
                LastWeek = lastWeekEntries.ToList(),
                LastMonth = lastMonthEntries.ToList()
            };

            ViewBag.SeriesTitles = "'" + string.Join("', '", seriesTitles) + "'";
            ViewBag.BooksTitles = "'" + string.Join("', '", booksTitles) + "'";
            ViewBag.MoviesTitles = "'" + string.Join("', '", moviesTitles) + "'";
            ViewBag.GamesTitles = "'" + string.Join("', '", gamesTitles) + "'";

            return View(timelineDetails);
        }
    }
}
