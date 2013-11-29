//-----------------------------------------------------------------------
// <copyright file="SessionFrequencyComputer.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Pop.Domain.Entities;

    /// <summary>
    /// Session frequency computer class, used to compute the frequency of the sessions
    /// </summary>
    public static class SessionFrequencyComputer<T> where T : IEntertainmentSession {
        /// <summary>
        /// Reading threshold between Regular and Occasional
        /// </summary>
        private const double ReadingThreshold = 0.25f;

        /// <summary>
        /// Gaming threshold between Regular and Occasional
        /// </summary>
        private const double GamingThreshold = 0.3f;

        /// <summary>
        /// Computes the frequency of the given list of sessions
        /// </summary>
        /// <param name="sessions">A list of entertainment sessions</param>
        /// <returns>A frequency</returns>
        public static SessionFrequency Compute(IList<T> sessions) {
            if (sessions.Count == 0) {
                return SessionFrequency.None;
            }

            if (sessions.Count == 1) {
                return SessionFrequency.Occasional;
            }

            var dates = sessions.Select(x => x.Date);
            var days = (dates.Max() - dates.Min()).TotalDays;
            var threshold = sessions.Count / days;

            return threshold >= GetThresholdLimit<T>() ? SessionFrequency.Regular : SessionFrequency.Occasional;
        }

        /// <summary>
        /// Gets the threshold limit for the given type of session
        /// </summary>
        /// <typeparam name="T">A type of session</typeparam>
        /// <returns>A threshold as a double</returns>
        public static double GetThresholdLimit<T>() {
            if (typeof(T) == typeof(ReadingSession)) {
                return ReadingThreshold;
            } else if (typeof(T) == typeof(GamingSession)) {
                return GamingThreshold;
            } else if (typeof(T) == typeof(TvWatchingSession)) {
                return 0.0f;
            } else {
                return 0.0f;
            }
        }
    }
}
