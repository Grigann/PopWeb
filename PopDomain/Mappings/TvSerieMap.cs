//-----------------------------------------------------------------------
// <copyright file="TvSerieMap.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Entities;

    /// <summary>
    /// TV serie entity mapping
    /// </summary>
    public class TvSerieMap : ClassMap<TvSerie> {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerieMap"/> class.
        /// </summary>
        public TvSerieMap() {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Genre);
            Map(x => x.Creator);
            Map(x => x.ReleaseDate);
            Map(x => x.Summary);
            Map(x => x.WikipediaLink);
            Map(x => x.IsDone);

            HasMany(x => x.Seasons).Not.LazyLoad().Cascade.All().OrderBy("Number");
        }
    }
}
