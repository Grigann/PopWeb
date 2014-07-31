//-----------------------------------------------------------------------
// <copyright file="TimelineDetails.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Web.ViewModels {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the lists of timeline entries to display in the index
    /// </summary>
    public class TimelineDetails {
        /// <summary>
        /// Gets or sets the list of entries for the week before the day before yesterday
        /// </summary>
        public IList<TimelineEntry> LastWeek { get; set; }

        /// <summary>
        /// Gets or setst the list of entries for the three months before the last week
        /// </summary>
        public IList<TimelineEntry> LastMonth { get; set; }
    }
}