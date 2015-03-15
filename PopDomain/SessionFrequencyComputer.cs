//-----------------------------------------------------------------------
// <copyright file="SessionFrequencyComputer.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System.Collections.Generic;
    using System.Linq;

    using Entities;

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

            var dates = sessions.Select(x => x.Date).ToList();
            var days = (dates.Max() - dates.Min()).TotalDays;
            var threshold = sessions.Count / days;

            return threshold >= GetThresholdLimit<T>() ? SessionFrequency.Regular : SessionFrequency.Occasional;
        }

        /// <summary>
        /// Gets the threshold limit for the given type of session
        /// </summary>
        /// <typeparam name="TS">A type of session</typeparam>
        /// <returns>A threshold as a double</returns>
        public static double GetThresholdLimit<TS>() {
            if (typeof(TS) == typeof(ReadingSession)) {
                return ReadingThreshold;            
            }

            if (typeof(TS) == typeof(GamingSession)) {
                return GamingThreshold;
            }

            if (typeof(TS) == typeof(TvWatchingSession)) {
                return 0.0f;
            }

            return 0.0f;
        }
    }
}
