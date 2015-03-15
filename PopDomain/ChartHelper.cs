//-----------------------------------------------------------------------
// <copyright file="ChartHelper.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Entities;

    /// <summary>
    /// Chart build helper object
    /// </summary>
    public class ChartHelper<T> where T : IEntertainmentSession {
        /// <summary>
        /// Computes the chart date from a given date
        /// </summary>
        /// <param name="currentDate">A date</param>
        /// <param name="chartInterval">A chart interval</param>
        /// <returns>An associated chart date</returns>
        public DateTime ComputeChartDate(DateTime currentDate, ChartIntervalType chartInterval) {
            var chartDate = currentDate;
            switch (chartInterval) {
                case ChartIntervalType.Weekly:
                    var delta = CultureInfo.GetCultureInfo("fr-FR").DateTimeFormat.FirstDayOfWeek - chartDate.DayOfWeek;
                    chartDate = delta > 0 ? chartDate.AddDays(delta - 7) : chartDate.AddDays(delta);
                    break;
                case ChartIntervalType.Monthly:
                    chartDate = new DateTime(chartDate.Year, chartDate.Month, 1);
                    break;
            }

            return chartDate;
        }

        /// <summary>
        /// Computes the interval type from a list of sessions
        /// </summary>
        /// <param name="sessions">A list of sessions</param>
        /// <returns>An interval type</returns>
        public ChartIntervalType ComputeIntervalType(IList<T> sessions) {
            if (sessions.Count == 0) {
                return ChartIntervalType.None;
            }

            var dates = sessions.Select(x => x.Date).ToList();
            var interval = (dates.Max() - dates.Min()).TotalDays;
            return interval < (7 * 3)
                ? ChartIntervalType.Daily
                    : interval < (7 * 4 * 3)
                            ? ChartIntervalType.Weekly
                            : ChartIntervalType.Monthly;
        }

        /// <summary>
        /// Gets the chart reading sessions
        /// </summary>
        public IDictionary<DateTime, int> ExtractChartSessions(IList<T> sessions, ChartIntervalType intervalType) {
            var result = new Dictionary<DateTime, int>();
            var dates = sessions.Select(x => x.Date).ToList();
            if (dates.Distinct().Count() < 3) {
                // To draw a chart we need at least three dates
                return result;
            }

            var first = dates.Min();

            // Initializing the dates tick
            var interval = (dates.Max() - first).TotalDays;
            for (var i = 0; i <= interval; i++) {
                var date = this.ComputeChartDate(first.AddDays(i), intervalType);
                if (!result.ContainsKey(date)) {
                    result.Add(date, 0);
                }
            }

            // Grouping the sessions by date
            foreach (var session in sessions) {
                var date = this.ComputeChartDate(session.Date, intervalType);
                result[date] += 1;
            }

            return result;
        }

        /// <summary>
        /// Gets the sessions chart as a string for jqPlot
        /// </summary>
        /// <param name="chartSessions">A dictionary of date and sessions count</param>
        /// <returns>A JSON string</returns>
        public string ComputeSessionsChart(IDictionary<DateTime, int> chartSessions) {
            var result = string.Empty;
            var index = 0;
            foreach (var tick in chartSessions.OrderBy(x => x.Key)) {
                result += "[" + index + ", " + tick.Value + "], ";
                index++;
            }

            if (string.IsNullOrEmpty(result)) {
                return "[]";
            } else {
                return "[[" + result.Substring(0, result.Length - 2) + "]]";
            }
        }

        /// <summary>
        /// Gets the sessions chart x-axis ticks for jqPlot
        /// </summary>
        /// <param name="chartSessions">A dictionary of date and sessions count</param>
        /// <returns>A JSON string</returns>
        public string ComputeXAxisTicks(IDictionary<DateTime, int> chartSessions) {
            var step = (chartSessions.Count / 7) + 1;
            if (chartSessions.Count % 7 == 0) {
                step -= 1;
            }

            var index = 0;
            var result = string.Empty;
            while (index < (chartSessions.Count - 1)) {
                result += index + ", ";
                index += step;
            }

            if (string.IsNullOrEmpty(result)) {
                return "[]";
            } else {
                result += (chartSessions.Count - 1);
                return "[" + result + "]";
            }
        }

        /// <summary>
        /// Gets the sessions chart y-axis ticks for jqPlot
        /// </summary>
        /// <param name="chartSessions">A dictionary of date and sessions count</param>
        /// <returns>A JSON string</returns>
        public string ComputeYAxisTicks(IDictionary<DateTime, int> chartSessions) {
            if (chartSessions.Count == 0) {
                return "[]";
            }

            var result = string.Empty;
            var maxSessionsCount = chartSessions.Values.Max();
            for (int i = 0; i <= maxSessionsCount; i++) {
                result += i + ", ";
            }

            var upperLimit = maxSessionsCount + ((double)maxSessionsCount / 10);
            result += upperLimit.ToString("###.0", CultureInfo.InvariantCulture) + ", ";

            return "[" + result.Substring(0, result.Length - 2) + "]";
        }   
    }
}
