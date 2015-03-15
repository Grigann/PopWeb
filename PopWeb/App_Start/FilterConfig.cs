//-----------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web {
    using System.Web.Mvc;

    /// <summary>
    /// Filter configuration object
    /// </summary>
    public class FilterConfig {
        /// <summary>
        /// Registers new filters in a collection of global filters
        /// </summary>
        /// <param name="filters">A collection of global filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new AuthorizeAttribute());
        }
    }
}