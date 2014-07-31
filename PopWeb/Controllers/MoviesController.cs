//-----------------------------------------------------------------------
// <copyright file="MoviesController.cs" company="Laurent Perruche-Joubert">
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
    /// Movies controller
    /// </summary>
    public class MoviesController : Controller {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Index() {
            using (var uow = new UnitOfWork(false)) {
                var movies = uow.Movies.All().OrderBy(x => x.Title).ToList();
                return View(movies);
            }
        }

        /// <summary>
        /// View a movie action
        /// </summary>
        /// <param name="id">A movie id</param>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult View(int id) {
            using (var uow = new UnitOfWork(false)) {
                var movie = uow.Movies.Find(id);
                return View(movie);
            }
        }

        /// <summary>
        /// New movie action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult New() {
            return View(new Movie());
        }

        /// <summary>
        /// Edit a movie action
        /// </summary>
        /// <param name="id">A movie id</param>
        /// <returns>An ActionResult</returns>
        public ActionResult Edit(int id) {
            using (var uow = new UnitOfWork(false)) {
                var movie = uow.Movies.Find(id);
                return View(movie);
            }
        }

        /// <summary>
        /// Creates or updates a movie and redirect to the View action
        /// </summary>
        /// <param name="movie">A new movie to create</param>
        /// <param name="posterFile">An uploaded file (can be null)</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public ActionResult SaveOrUpdate(Movie movie, HttpPostedFileBase posterFile) {
            if (!string.IsNullOrEmpty(movie.PosterFileName)) {
                movie.PosterFileName = Path.GetFileName(movie.PosterFileName);
            }

            if (posterFile != null && posterFile.ContentLength > 0) {
                var fileName = Path.GetFileName(posterFile.FileName);
                var directory = Server.MapPath("~/Content/images/movies");
                if (!Directory.Exists(directory)) {
                    Directory.CreateDirectory(directory);
                }

                // Saves the original image
                var originalFilePath = Path.Combine(directory, fileName);
                posterFile.SaveAs(originalFilePath);

                ThumbnailHandler.CreateAllThumbs(originalFilePath);
            }

            using (var uow = new UnitOfWork(true)) {
                if (movie.Id != 0) {
                    var oldMovie = uow.Movies.Find(movie.Id);
                    oldMovie.Title = movie.Title;
                    oldMovie.Genre = movie.Genre;
                    oldMovie.Director = movie.Director;
                    oldMovie.ReleaseDate = movie.ReleaseDate;
                    oldMovie.PosterFileName = movie.PosterFileName;
                    oldMovie.Summary = movie.Summary;
                    oldMovie.WikipediaLink = movie.WikipediaLink;
                    uow.Movies.SaveOrUpdate(oldMovie);
                } else {
                    uow.Movies.SaveOrUpdate(movie);
                }

                uow.Commit();
            }

            return RedirectToAction("View", new { id = movie.Id });
        }

        /// <summary>
        /// Saves a new watching session and creates a new movie if necessary
        /// </summary>
        /// <param name="movieEntry">A movie entry</param>
        /// <returns>A JSON representation of a timeline entry</returns>
        public JsonResult QuickSave(MovieEntry movieEntry) {
            // Used to emulate a failure
            if (movieEntry.Director == "FAIL") {
                throw new Exception("Une erreur");
            }

            Movie movie;
            WatchingSession watchingSession;
            using (var uow = new UnitOfWork(true)) {
                movie = uow.Movies.FindByTitle(movieEntry.Title);
                if (movie == null) {
                    movie = new Movie() { Title = movieEntry.Title, Director = movieEntry.Director };
                }

                watchingSession = new WatchingSession() { Date = DateTime.ParseExact(movieEntry.EntryDate, "dd/MM/yyyy", null) };
                movie.AddWatchingSession(watchingSession);

                uow.Movies.SaveOrUpdate(movie);
                uow.Commit();
            }

            var timelineEntry = new TimelineEntry(watchingSession, this);
            return Json(timelineEntry);
        }
    }
}
