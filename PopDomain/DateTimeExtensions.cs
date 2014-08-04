//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="DataCard Corporation">
//     © 2014 DataCard Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System;

    /// <summary>
    /// Static class providing DateTime extension methods.
    /// </summary>
    public static class DateTimeExtensions {
        /// <summary>
        /// Returns the starts the of week containing the provided date.
        /// </summary>
        /// <param name="dt">The date for which we want the start of the week.</param>
        /// <param name="startOfWeek">The day representing the start of the week.</param>
        /// <returns>A <see cref="DateTime"/> object.</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek) {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0) {
                diff += 7;
            }

            return dt.Date.AddDays(-1 * diff);
        }
    }
}
