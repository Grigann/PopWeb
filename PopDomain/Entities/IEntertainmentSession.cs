//-----------------------------------------------------------------------
// <copyright file="IEntertainmentSession.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;

    /// <summary>
    /// Base interface for reading, watching and gaming session
    /// </summary>
    public interface IEntertainmentSession {
        /// <summary>
        /// Gets or sets the date of the session
        /// </summary>
        DateTime Date { get; set; }
    }
}
