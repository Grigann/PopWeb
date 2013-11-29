//-----------------------------------------------------------------------
// <copyright file="ReadingSessionMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// Reading session entity mapping
    /// </summary>
    public class ReadingSessionMap : ClassMap<ReadingSession> {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingSessionMap"/> class.
        /// </summary>
        public ReadingSessionMap() {
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Note);
            References(x => x.Book);
        }
    }
}
