//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System.Web.Mvc;
    using System.Web.Security;

    using Domain;

    /// <summary>
    /// Account controller
    /// </summary>
    public class AccountController : Controller {
        /// <summary>
        /// Login action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login() {
            var urlReferrer = this.Request.UrlReferrer;
            if (urlReferrer != null) {
                this.TempData["referrerUrl"] = urlReferrer.ToString();
            }

            return View();
        }

        /// <summary>
        /// Login action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userLogin, string userPassword, string referrerUrl) {
            using (var uow = new UnitOfWork(false)) {
                var user = uow.Users.FindByNameAndPassword(userLogin, userPassword);
                if (user == null) {
                    return RedirectToAction("Login");
                }

                FormsAuthentication.SetAuthCookie(userLogin, true);
                if (string.IsNullOrEmpty(referrerUrl) || referrerUrl.EndsWith("login")) {
                    return this.RedirectToAction("Index", "Timeline");
                }

                return this.Redirect(referrerUrl);
            }
        }

        /// <summary>
        /// Logout action
        /// </summary>
        /// <returns>An ActionResult</returns>
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
