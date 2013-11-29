//-----------------------------------------------------------------------
// <copyright file="WatchingSessionMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// Watching session entity mapping
    /// </summary>
    public class WatchingSessionMap : ClassMap<WatchingSession> {
        /// <summary>
        /// Initializes a new instance of the <see cref="WatchingSessionMap"/> class.
        /// </summary>
        public WatchingSessionMap() {
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Note);
            References(x => x.Movie);
        }
    }
}
