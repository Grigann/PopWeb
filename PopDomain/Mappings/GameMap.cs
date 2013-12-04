//-----------------------------------------------------------------------
// <copyright file="GameMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// Game entity mapping
    /// </summary>
    public class GameMap : ClassMap<Game> {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameMap"/> class.
        /// </summary>
        public GameMap() {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Genre);
            Map(x => x.GamingPlatform);
            Map(x => x.Developper);
            Map(x => x.ReleaseDate);
            Map(x => x.CoverFileName);
            Map(x => x.Summary);
            Map(x => x.Review);
            Map(x => x.WikipediaLink);

            HasMany(x => x.Achievements).Not.LazyLoad().Cascade.All().OrderBy("Id");
            HasMany(x => x.GamingSessions).Not.LazyLoad().Cascade.All().OrderBy("Date");
        }
    }
}
