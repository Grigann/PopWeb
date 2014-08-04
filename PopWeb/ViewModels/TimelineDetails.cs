//-----------------------------------------------------------------------
// <copyright file="TimelineDetails.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
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
        private CultureInfo uiCulture;

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
                var day = string.Empty;
                if (this.ThisWeekLimit.Day == 1) {
                    day = string.Format(this.uiCulture, "{0:dddd} 1er {0:MMMM}", this.ThisWeekLimit);
                } else {
                    day = string.Format(this.uiCulture, "{0:dddd d MMMM}", this.ThisWeekLimit);
                }

                return "Depuis le " + day;
            }
        }

        /// <summary>
        /// Gets the label for "last week" group.
        /// </summary>
        public string LastWeekLabel {
            get {
                var endOfPeriod = this.ThisWeekLimit.AddDays(-1);

                var startDay = string.Empty;
                if (this.LastWeekLimit.Day == 1) {
                    if (this.LastWeekLimit.Month == endOfPeriod.Month) {
                        startDay = string.Format(this.uiCulture, "{0:dddd} 1er", this.LastWeekLimit);
                    } else {
                        startDay = string.Format(this.uiCulture, "{0:dddd} 1er {0:MMMM}", this.LastWeekLimit);
                    }
                } else {
                    if (this.LastWeekLimit.Month == endOfPeriod.Month) {
                        startDay = string.Format(this.uiCulture, "{0:dddd d}", this.LastWeekLimit);
                    } else {
                        startDay = string.Format(this.uiCulture, "{0:dddd d MMMM}", this.LastWeekLimit);
                    }
                }

                var endDay = string.Empty;
                if (endOfPeriod.Day == 1) {
                    endDay = string.Format(this.uiCulture, "{0:dddd} 1er {0:MMMM}", endOfPeriod);
                } else {
                    endDay = string.Format(this.uiCulture, "{0:dddd d MMMM}", endOfPeriod);
                }

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