//-----------------------------------------------------------------------
// <copyright file="TvSerieSeasonMap.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Entities;

    /// <summary>
    /// TV serie season entity mapping
    /// </summary>
    public class TvSerieSeasonMap : ClassMap<TvSerieSeason> {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerieSeasonMap"/> class.
        /// </summary>
        public TvSerieSeasonMap() {
            Id(x => x.Id);
            Map(x => x.Number);
            Map(x => x.ReleaseDate);
            Map(x => x.PosterFileName);
            Map(x => x.IsDone);
            References(x => x.TvSerie);

            HasMany(x => x.Episodes).Not.LazyLoad().Cascade.All();
        }
    }
}
