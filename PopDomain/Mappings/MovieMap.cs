//-----------------------------------------------------------------------
// <copyright file="MovieMap.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Entities;

    /// <summary>
    /// Movie entity mapping
    /// </summary>
    public class MovieMap : ClassMap<Movie> {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieMap"/> class.
        /// </summary>
        public MovieMap() {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Genre);
            Map(x => x.Director);
            Map(x => x.ReleaseDate);
            Map(x => x.PosterFileName);
            Map(x => x.Summary);
            Map(x => x.WikipediaLink);

            HasMany(x => x.WatchingSessions).Not.LazyLoad().Cascade.All().OrderBy("Date");
        }
    }
}
