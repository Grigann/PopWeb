//-----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web {
    using System;
    using System.Web.Http;

    /// <summary>
    /// Web API configuration object
    /// </summary>
    public static class WebApiConfig {
        /// <summary>
        /// Registers new configuration information in the given configuration
        /// </summary>
        /// <param name="config">An HTTP configuration</param>
        public static void Register(HttpConfiguration config) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
