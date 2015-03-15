//-----------------------------------------------------------------------
// <copyright file="AboutController.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System.Web.Mvc;

    /// <summary>
    /// About controller
    /// </summary>
    public class AboutController : Controller {
        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [AllowAnonymous]
        public ActionResult Index() {
            return View();
        }
    }
}
