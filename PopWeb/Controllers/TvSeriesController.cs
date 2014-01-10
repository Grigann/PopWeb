//-----------------------------------------------------------------------
// <copyright file="TvSeriesController.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Pop.Domain;
    using Pop.Domain.Entities;
    using Pop.Web.ViewModels;

    /// <summary>
    /// TV series controller
    /// </summary>
    public class TvSeriesController : Controller {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Index() {
            using (var uow = new UnitOfWork(false)) {
                var tvSeries = uow.TvSeries.All().OrderBy(x => x.Title).ToList();
                return View(tvSeries);
            }
        }

        /// <summary>
        /// View a TV series action
        /// </summary>
        /// <param name="id">A series id</param>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult View(int id) {
            using (var uow = new UnitOfWork(false)) {
                var tvSerie = uow.TvSeries.Find(id);
                tvSerie.InitializeChartInfos();
                return View(tvSerie);
            }
        }

        /// <summary>
        /// New TV series action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult New() {
            return View(new TvSerie());
        }

        /// <summary>
        /// Edit a series action
        /// </summary>
        /// <param name="id">A series id</param>
        /// <returns>An ActionResult</returns>
        public ActionResult Edit(int id) {
            using (var uow = new UnitOfWork(false)) {
                var tvSerie = uow.TvSeries.Find(id);
                return View(tvSerie);
            }
        }

        /// <summary>
        /// Creates or updates a TV series and redirect to the View action
        /// </summary>
        /// <param name="tvSerie">A new series to create</param>
        /// <param name="posterFile">An uploaded file (can be null)</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public ActionResult SaveOrUpdate(TvSerie tvSerie) {
            using (var uow = new UnitOfWork(true)) {
                if (tvSerie.Id != 0) {
                    var oldSerie = uow.TvSeries.Find(tvSerie.Id);
                    oldSerie.Title = tvSerie.Title;
                    oldSerie.Genre = tvSerie.Genre;
                    oldSerie.Creator = tvSerie.Creator;
                    oldSerie.ReleaseDate = tvSerie.ReleaseDate;
                    oldSerie.Summary = tvSerie.Summary;
                    oldSerie.WikipediaLink = tvSerie.WikipediaLink;
                    uow.TvSeries.SaveOrUpdate(oldSerie);
                } else {
                    uow.TvSeries.SaveOrUpdate(tvSerie);
                }

                uow.Commit();
            }

            return RedirectToAction("View", new { id = tvSerie.Id });
        }

        /// <summary>
        /// New TV series season action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult NewSeason(int serieId) {
            using (var uow = new UnitOfWork(false)) {
                var tvSerie = uow.TvSeries.Find(serieId);
                return View(new TvSerieSeason() { TvSerie = tvSerie });
            }
        }

        /// <summary>
        /// Edit TV series season action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult EditSeason(int serieId, int seasonId) {
            using (var uow = new UnitOfWork(false)) {
                var tvSerie = uow.TvSeries.Find(serieId);
                var season = tvSerie.Seasons.Where(x => x.Id == seasonId).SingleOrDefault();
                return View(season);
            }
        }

        /// <summary>
        /// Creates or updates a TV series season and redirect to the View series action
        /// </summary>
        /// <param name="tvSerie">A new season to create</param>
        /// <param name="posterFile">An uploaded file (can be null)</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public ActionResult SaveOrUpdateSeason(TvSerieSeason season, HttpPostedFileBase posterFile) {
            if (!string.IsNullOrEmpty(season.PosterFileName)) {
                season.PosterFileName = Path.GetFileName(season.PosterFileName);
            }

            if (posterFile != null && posterFile.ContentLength > 0) {
                var fileName = Path.GetFileName(posterFile.FileName);
                var directory = Server.MapPath("~/Content/images/tvSeries");
                if (!Directory.Exists(directory)) {
                    Directory.CreateDirectory(directory);
                }

                posterFile.SaveAs(Path.Combine(directory, fileName));
            }

            using (var uow = new UnitOfWork(true)) {
                var tvSerie = uow.TvSeries.Find(season.TvSerieId);
                if (season.Id == 0) {
                    tvSerie.AddSeason(season);
                } else {
                    var oldSeason = tvSerie.Seasons.Where(x => x.Id == season.Id).SingleOrDefault();
                    oldSeason.Number = season.Number;
                    oldSeason.ReleaseDate = season.ReleaseDate;
                    oldSeason.PosterFileName = season.PosterFileName;
                }

                uow.TvSeries.SaveOrUpdate(tvSerie);
                uow.Commit();

                return RedirectToAction("View", new { id = tvSerie.Id });
            }
        }

        /// <summary>
        /// Saves or updates an episode and returns
        /// </summary>
        /// <param name="episode">A TV series episode</param>
        /// <returns>A JSON TV series episode</returns>
        public JsonResult SaveOrUpdateEpisode(TvSerieEpisode episode) {
            using (var uow = new UnitOfWork(true)) {
                var tvSerie = uow.TvSeries.Find(episode.TvSerieId);
                var season = tvSerie.Seasons.Where(x => x.Id == episode.SeasonId).SingleOrDefault();
                var newEpisode = episode;

                if (newEpisode.Id == 0) {
                    season.AddEpisode(newEpisode);
                } else {
                    newEpisode = season.Episodes.Where(x => x.Id == episode.Id).SingleOrDefault();
                    newEpisode.Number = episode.Number;
                    newEpisode.Title = episode.Title;
                    newEpisode.Director = episode.Director;
                }

                uow.TvSeries.SaveOrUpdate(tvSerie);
                uow.Commit();

                return Json(newEpisode);
            }
        }

        /// <summary>
        /// Saves a new watching session session and creates a new series/season/episode if necessary
        /// </summary>
        /// <param name="episodeEntry">Aen episode entry</param>
        /// <returns>A JSON representation of a timeline entry</returns>
        public JsonResult QuickSave(TvSerieEpisodeEntry episodeEntry) {
            // Used to emulate a failure
            if (episodeEntry.EpisodeTitle == "FAIL") {
                throw new Exception("Une erreur");
            }

            TvSerie serie;
            TvSerieSeason season;
            TvSerieEpisode episode;
            TvWatchingSession watchingSession;
            using (var uow = new UnitOfWork(true)) {
                serie = uow.TvSeries.FindByTitle(episodeEntry.Title);
                if (serie == null) {
                    serie = new TvSerie() { Title = episodeEntry.Title };
                }

                season = serie.Seasons.Where(x => x.Number == episodeEntry.SeasonNb).SingleOrDefault();
                if (season == null) {
                    season = new TvSerieSeason() { Number = episodeEntry.SeasonNb };
                    serie.AddSeason(season);
                }

                episode = season.Episodes.Where(x => x.Number == episodeEntry.EpisodeNb).SingleOrDefault();
                if (episode == null) {
                    episode = new TvSerieEpisode() { Number = episodeEntry.EpisodeNb, Title = episodeEntry.EpisodeTitle };
                    season.AddEpisode(episode);
                }

                watchingSession = new TvWatchingSession() { Date = DateTime.ParseExact(episodeEntry.EntryDate, "dd/MM/yyyy", null) };
                episode.AddWatchingSession(watchingSession);

                uow.TvSeries.SaveOrUpdate(serie);
                uow.Commit();
            }

            var timelineEntry = new TimelineEntry(watchingSession, this);
            return Json(timelineEntry);
        }
    }
}
