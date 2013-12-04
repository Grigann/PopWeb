//-----------------------------------------------------------------------
// <copyright file="GamesController.cs" company="Laurent Perruche-Joubert">
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
        /// New achievement action
        /// </summary>
        /// <param name="gameId">A game id</param>
        /// <returns>An ActionResult</returns>
        public ActionResult NewAchievement(int gameId) {
            using (var uow = new UnitOfWork(false)) {
                var game = uow.Games.Find(gameId);
                return View(new GameAchievement() { Game = game });
            }
        }

        /// <summary>
        /// Edit achievement action
        /// </summary>
        /// <param name="gameId">A game id</param>
        /// <param name="achievementId">An achievement id</param>
        /// <returns>An ActionResult</returns>
        public ActionResult EditAchievement(int gameId, int achievementId) {
            using (var uow = new UnitOfWork(false)) {
                var game = uow.Games.Find(gameId);
                var achievement = game.Achievements.Where(x => x.Id == achievementId).SingleOrDefault();
                return View(achievement);
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
                var path = Path.Combine(Server.MapPath("~/Content/images/games"), fileName);
                coverFile.SaveAs(path);
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
                    oldGame.Review = game.Review;
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
        /// Creates or updates a game achievement and redirect to the View action
        /// </summary>
        /// <param name="achievement">A new achievement to create</param>
        /// <param name="imageFile">An uploaded file (can be null)</param>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        public ActionResult SaveOrUpdateAchievement(GameAchievement achievement, HttpPostedFileBase imageFile) {
            if (!string.IsNullOrEmpty(achievement.ImageFileName)) {
                achievement.ImageFileName = Path.GetFileName(achievement.ImageFileName);
            }

            if (imageFile != null && imageFile.ContentLength > 0) {
                var fileName = Path.GetFileName(imageFile.FileName);
                var directory = Server.MapPath("~/Content/images/games/achievements");
                if (!Directory.Exists(directory)) {
                    Directory.CreateDirectory(directory);
                }

                imageFile.SaveAs(Path.Combine(directory, fileName));
            }

            using (var uow = new UnitOfWork(true)) {
                var game = uow.Games.Find(achievement.GameId);
                if (achievement.Id != 0) {
                    var oldAchievement = game.Achievements.Where(x => x.Id == achievement.Id).Single();
                    oldAchievement.Name = achievement.Name;
                    oldAchievement.Description = achievement.Description;
                    oldAchievement.Gamerpoints = achievement.Gamerpoints;
                    oldAchievement.IsMultiplayer = achievement.IsMultiplayer;
                    oldAchievement.AssociatedDlc = achievement.AssociatedDlc;
                    oldAchievement.ImageFileName = achievement.ImageFileName;
                    oldAchievement.TALink = achievement.TALink;
                    oldAchievement.X360AchievementsLink = achievement.X360AchievementsLink;
                    oldAchievement.WinDate = achievement.WinDate;
                } else {
                    game.AddAchievement(achievement);
                }

                uow.Games.SaveOrUpdate(game);
                uow.Commit();
            }

            return RedirectToAction("View", new { id = achievement.GameId });
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

            Game game;
            GamingSession gamingSession;
            using (var uow = new UnitOfWork(true)) {
                game = uow.Games.FindByTitle(gameEntry.Title);
                if (game == null) {
                    game = new Game() { Title = gameEntry.Title, Developper = gameEntry.Developper };
                }

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
