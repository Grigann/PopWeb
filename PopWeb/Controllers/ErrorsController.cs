//-----------------------------------------------------------------------
// <copyright file="ErrorsController.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    /// <summary>
    /// Errors controller
    /// </summary>
    [AllowAnonymous]
    public class ErrorsController : Controller {
        /// <summary>
        /// General error action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult Index() {
            return View();
        }

        /// <summary>
        /// HTTP 404 error action
        /// </summary>
        /// <returns>An ActionResult</returns>
        /// <param name="aspxerrorpath">An error path</param>
        public ActionResult Http404(string aspxerrorpath) {
            ViewData["errorPath"] = this.Request.Url.Scheme + "://" + this.Request.Url.Authority + aspxerrorpath;
            return View();
        }
    }
}
