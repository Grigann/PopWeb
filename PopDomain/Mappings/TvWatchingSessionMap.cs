//-----------------------------------------------------------------------
// <copyright file="TvWatchingSessionMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// TV watching session entity mapping
    /// </summary>
    public class TvWatchingSessionMap : ClassMap<TvWatchingSession> {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvWatchingSessionMap"/> class.
        /// </summary>
        public TvWatchingSessionMap() {
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Note);
            References(x => x.Episode);
        }
    }
}
