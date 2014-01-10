//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Pop.Domain;

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
            TempData["referrerUrl"] = this.Request.UrlReferrer.ToString();
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
                } else {
                    FormsAuthentication.SetAuthCookie(userLogin, false);
                    if (string.IsNullOrEmpty(referrerUrl)) {
                        return RedirectToAction("Index", "Timeline");
                    } else {
                        return Redirect(referrerUrl);
                    }
                }
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
