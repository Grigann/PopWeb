//-----------------------------------------------------------------------
// <copyright file="ErrorsController.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System.Web.Mvc;

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
            var url = this.Request.Url;
            if (url != null) {
                this.ViewData["errorPath"] = url.Scheme + "://" + url.Authority + aspxerrorpath;
            }

            return View();
        }
    }
}
