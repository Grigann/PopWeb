//-----------------------------------------------------------------------
// <copyright file="ReadingSession.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Reading session entity
    /// </summary>
    public class ReadingSession : IEntertainmentSession {
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
        /// Gets or sets the book
        /// </summary>
        [ScriptIgnore]
        public virtual Book Book { get; set; }
    }
}
