//-----------------------------------------------------------------------
// <copyright file="TimelineDetails.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents the lists of timeline entries to display in the index
    /// </summary>
    public class TimelineDetails {
        /// <summary>
        /// The UI culture
        /// </summary>
        private readonly CultureInfo uiCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineDetails"/> class.
        /// </summary>
        public TimelineDetails() {
            this.uiCulture = CultureInfo.GetCultureInfo("fr-FR");
        }

        /// <summary>
        /// Gets or sets this week limit.
        /// </summary>
        public DateTime ThisWeekLimit { get; set; }

        /// <summary>
        /// Gets or sets last week limit.
        /// </summary>
        public DateTime LastWeekLimit { get; set; }

        /// <summary>
        /// Gets or sets last month limit.
        /// </summary>
        public DateTime LastMonthLimit { get; set; }

        /// <summary>
        /// Gets the label for "this week" group.
        /// </summary>
        public string ThisWeekLabel {
            get {
                var day = string.Format(
                    this.uiCulture,
                    this.ThisWeekLimit.Day == 1 ? "{0:dddd} 1er {0:MMMM}" : "{0:dddd d MMMM}",
                    this.ThisWeekLimit);

                return "Depuis le " + day;
            }
        }

        /// <summary>
        /// Gets the label for "last week" group.
        /// </summary>
        public string LastWeekLabel {
            get {
                var endOfPeriod = this.ThisWeekLimit.AddDays(-1);

                string startDay;
                if (this.LastWeekLimit.Day == 1) {
                    startDay = string.Format(
                        this.uiCulture,
                        this.LastWeekLimit.Month == endOfPeriod.Month ? "{0:dddd} 1er" : "{0:dddd} 1er {0:MMMM}",
                        this.LastWeekLimit);
                } else {
                    startDay = string.Format(
                        this.uiCulture,
                        this.LastWeekLimit.Month == endOfPeriod.Month ? "{0:dddd d}" : "{0:dddd d MMMM}",
                        this.LastWeekLimit);
                }

                var endDay = string.Format(
                    this.uiCulture,
                    endOfPeriod.Day == 1 ? "{0:dddd} 1er {0:MMMM}" : "{0:dddd d MMMM}",
                    endOfPeriod);

                return "Entre le " + startDay + " et le " + endDay;
            }
        }

        /// <summary>
        /// Gets the label for "last month" group.
        /// </summary>
        public string LastMonthLabel {
            get {
                var month = string.Format(this.uiCulture, "{0:MMMM}", this.LastMonthLimit);
                return "Depuis le 1er " + month.ToLower(this.uiCulture);
            }
        }

        /// <summary>
        /// Gets or sets the list of entries for this week
        /// </summary>
        public IList<TimelineEntry> ThisWeek { get; set; }

        /// <summary>
        /// Gets or sets the list of entries for last week
        /// </summary>
        public IList<TimelineEntry> LastWeek { get; set; }

        /// <summary>
        /// Gets or setst the list of entries for the last two months
        /// </summary>
        public IList<TimelineEntry> LastMonth { get; set; }
    }
}