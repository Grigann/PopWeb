//-----------------------------------------------------------------------
// <copyright file="WatchingSession.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Watching session entity
    /// </summary>
    public class WatchingSession : IEntertainmentSession {
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
        /// Gets or sets the movie
        /// </summary>
        [ScriptIgnore]
        public virtual Movie Movie { get; set; }
    }
}
