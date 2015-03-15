//-----------------------------------------------------------------------
// <copyright file="TvSerieEpisodeMap.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Entities;

    /// <summary>
    /// TV serie episode entity mapping
    /// </summary>
    public class TvSerieEpisodeMap : ClassMap<TvSerieEpisode> {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerieEpisodeMap"/> class.
        /// </summary>
        public TvSerieEpisodeMap() {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Number);
            Map(x => x.Director);
            References(x => x.Season);

            HasMany(x => x.WatchingSessions).Not.LazyLoad().Cascade.All().OrderBy("Date");
        }
    }
}
