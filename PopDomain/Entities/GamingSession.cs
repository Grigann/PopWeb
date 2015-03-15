//-----------------------------------------------------------------------
// <copyright file="GamingSession.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Gaming session entity
    /// </summary>
    public class GamingSession : IEntertainmentSession {
        /// <summary>
        /// Gets the session id
        /// </summary>
        public virtual int Id { get; protected set; }

        /// <summary>
        /// Gets or sets the session date
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a note regarding the session
        /// </summary>
        public virtual string Note { get; set; }

        /// <summary>
        /// Gets or sets the game
        /// </summary>
        [ScriptIgnore]
        public virtual Game Game { get; set; }
    }
}
