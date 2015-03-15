//-----------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web {
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route configuration object
    /// </summary>
    public class RouteConfig {
        /// <summary>
        /// Registers new routes in the given routes collection
        /// </summary>
        /// <param name="routes">A collection of routes</param>
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Errors404",
                url: "erreurs/404",
                defaults: new { controller = "Errors", action = "Http404" });

            routes.MapRoute(
                name: "Errors",
                url: "erreurs/{action}",
                defaults: new { controller = "Errors", action = "Index" });

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" });

            routes.MapRoute(
                name: "Logout",
                url: "logout",
                defaults: new { controller = "Account", action = "Logout" });

            routes.MapRoute(
                name: "About",
                url: "a-propos/{action}/{id}",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "GamesNewAchievement",
                url: "jeux/{gameId}/succes/creation", 
                defaults: new { controller = "Games", action = "NewAchievement", gameId = UrlParameter.Optional });

            routes.MapRoute(
                name: "GamesEditAchievement",
                url: "jeux/{gameId}/succes/edition/{achievementId}",
                defaults: new { controller = "Games", action = "EditAchievement", gameId = UrlParameter.Optional, achievementId = UrlParameter.Optional });

            routes.MapRoute(
                name: "GamesNew",
                url: "jeux/creation",
                defaults: new { controller = "Games", action = "New" });

            routes.MapRoute(
                name: "GamesEdit",
                url: "jeux/edition/{id}",
                defaults: new { controller = "Games", action = "Edit", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "GamesView",
                url: "jeux/{id}",
                defaults: new { controller = "Games", action = "View", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" });

            routes.MapRoute(
                name: "Games",
                url: "jeux/{action}/{id}",
                defaults: new { controller = "Games", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "MoviesNew",
                url: "films/creation",
                defaults: new { controller = "Movies", action = "New" });

            routes.MapRoute(
                name: "MoviesEdit",
                url: "films/edition/{id}",
                defaults: new { controller = "Movies", action = "Edit", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "MoviesView",
                url: "films/{id}",
                defaults: new { controller = "Movies", action = "View", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" });

            routes.MapRoute(
                name: "Movies",
                url: "films/{action}/{id}",
                defaults: new { controller = "Movies", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "BooksNew",
                url: "livres/creation",
                defaults: new { controller = "Books", action = "New" });

            routes.MapRoute(
                name: "BooksEdit",
                url: "livres/edition/{id}",
                defaults: new { controller = "Books", action = "Edit", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "BooksView",
                url: "livres/{id}",
                defaults: new { controller = "Books", action = "View", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" });

            routes.MapRoute(
                name: "Books",
                url: "livres/{action}/{id}",
                defaults: new { controller = "Books", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "TvSeriesNewSeason",
                url: "series/{serieId}/saisons/creation",
                defaults: new { controller = "TvSeries", action = "NewSeason", serieId = UrlParameter.Optional });

            routes.MapRoute(
                name: "TvSeriesEditSeason",
                url: "series/{serieId}/saisons/edition/{seasonId}",
                defaults: new { controller = "TvSeries", action = "EditSeason", serieId = UrlParameter.Optional, seasonId = UrlParameter.Optional });

            routes.MapRoute(
                name: "TvSeriesNew",
                url: "series/creation",
                defaults: new { controller = "TvSeries", action = "New" });

            routes.MapRoute(
                name: "TvSeriesEdit",
                url: "series/edition/{id}",
                defaults: new { controller = "TvSeries", action = "Edit", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "TvSeriesView",
                url: "series/{id}",
                defaults: new { controller = "TvSeries", action = "View", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" });

            routes.MapRoute(
                name: "TvSeries",
                url: "series/{action}/{id}",
                defaults: new { controller = "TvSeries", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Timeline",
                url: "chronologie/{action}/{id}",
                defaults: new { controller = "Timeline", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Timeline", action = "Index", id = UrlParameter.Optional });
        }
    }
}