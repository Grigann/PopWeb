﻿//-----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.Controllers {
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

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
            return View();
        }

        /// <summary>
        /// Login action
        /// </summary>
        /// <returns>An ActionResult</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userLogin, string userPassword) {
            if (userLogin == "Laurent" && userPassword == "elrond") {
                FormsAuthentication.SetAuthCookie(userLogin, false);
                return RedirectToAction("Index", "Timeline");
            } else {
                return RedirectToAction("Login");
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