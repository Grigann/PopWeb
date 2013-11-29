//-----------------------------------------------------------------------
// <copyright file="ReadingSessionMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// Gaming session entity mapping
    /// </summary>
    public class GamingSessionMap : ClassMap<GamingSession> {
        /// <summary>
        /// Initializes a new instance of the <see cref="GamingSessionMap"/> class.
        /// </summary>
        public GamingSessionMap() {
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Note);
            References(x => x.Game);
        }
    }
}
