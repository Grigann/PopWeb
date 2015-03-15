//-----------------------------------------------------------------------
// <copyright file="GamesController.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Domain;
    using Domain.Entities;
    using ViewModels;

    /// <summary>
    /// Games controller
    /// </summary>
    public class GamesController : Controller {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Index() {
            using (var uow = new UnitOfWork(false)) {
                var games = uow.Games.All().OrderBy(x => x.Title).ToList();
                return View(games);
            }
        }

        /// <summary>
        /// View a game action
        /// </summary>
        /// <param name="id">A game id</param>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult View(int id) {
            using (var uow = new UnitOfWork(false)) {
                var game = uow.Games.Find(id);
                game.InitializeChartInfos();
                return View(game);
            }
        }

        /// <summary>
        /// New game action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult New() {
            return View(new Game());
        }

        /// <summary>
        /// Edit a game action
        /// </summary>
        /// <param name="id">A game id</param>
        /// <returns>An ActionResult</returns>
        public ActionResult Edit(int id) {
            using (var uow = new UnitOfWork(false)) {
                var game = uow.Games.Find(id);
                return View(game);
            }
        }

        /// <summary>
        /// Creates or updates a game and redirect to the View action
        /// </summary>
        /// <param name="game">A new game to create</param>
        /// <param name="coverFile">An uploaded file (can be null)</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public ActionResult SaveOrUpdate(Game game, HttpPostedFileBase coverFile) {
            if (!string.IsNullOrEmpty(game.CoverFileName)) {
                game.CoverFileName = Path.GetFileName(game.CoverFileName);
            }

            if (coverFile != null && coverFile.ContentLength > 0) {
                var fileName = Path.GetFileName(coverFile.FileName);
                var directory = Server.MapPath("~/Content/images/games");
                if (!Directory.Exists(directory)) {
                    Directory.CreateDirectory(directory);
                }

                // Saves the original image
                if (fileName != null) {
                    var originalFilePath = Path.Combine(directory, fileName);
                    coverFile.SaveAs(originalFilePath);

                    ThumbnailHandler.CreateAllThumbs(originalFilePath);
                }
            }

            using (var uow = new UnitOfWork(true)) {
                if (game.Id != 0) {
                    var oldGame = uow.Games.Find(game.Id);
                    oldGame.Title = game.Title;
                    oldGame.Genre = game.Genre;
                    oldGame.GamingPlatform = game.GamingPlatform;
                    oldGame.Developper = game.Developper;
                    oldGame.ReleaseDate = game.ReleaseDate;
                    oldGame.CoverFileName = game.CoverFileName;
                    oldGame.Summary = game.Summary;
                    oldGame.WikipediaLink = game.WikipediaLink;
                    uow.Games.SaveOrUpdate(oldGame);
                } else {
                    uow.Games.SaveOrUpdate(game);
                }

                uow.Commit();
            }

            return RedirectToAction("View", new { id = game.Id });
        }

        /// <summary>
        /// Saves a new gaming session and creates a new game if necessary
        /// </summary>
        /// <param name="gameEntry">A game entry</param>
        /// <returns>A JSON representation of a timeline entry</returns>
        public JsonResult QuickSave(GameEntry gameEntry) {
            // Used to emulate a failure
            if (gameEntry.Developper == "FAIL") {
                throw new Exception("Une erreur");
            }

            GamingSession gamingSession;
            using (var uow = new UnitOfWork(true)) {
                var game = uow.Games.FindByTitle(gameEntry.Title) ??
                           new Game() { Title = gameEntry.Title, Developper = gameEntry.Developper };

                gamingSession = new GamingSession() { 
                    Date = DateTime.ParseExact(gameEntry.EntryDate, "dd/MM/yyyy", null), 
                    Note = gameEntry.Note };
                game.AddGamingSession(gamingSession);

                uow.Games.SaveOrUpdate(game);
                uow.Commit();
            }

            var timelineEntry = new TimelineEntry(gamingSession, this);
            return Json(timelineEntry);
        }
    }
}
