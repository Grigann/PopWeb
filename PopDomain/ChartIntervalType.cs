//-----------------------------------------------------------------------
// <copyright file="ChartIntervalType.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    /// <summary>
    /// The charts interval type
    /// </summary>
    public enum ChartIntervalType {
        /// <summary>
        /// No interval type defined
        /// </summary>
        None,

        /// <summary>
        /// Interval in days, straight from the sessions
        /// </summary>
        Daily,

        /// <summary>
        /// Interval in weeks, from first day of weeks
        /// </summary>
        Weekly,

        /// <summary>
        /// Interval in months, from first day of months
        /// </summary>
        Monthly
    }
}
