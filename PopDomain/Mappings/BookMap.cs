//-----------------------------------------------------------------------
// <copyright file="BookMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// Book entity mapping
    /// </summary>
    public class BookMap : ClassMap<Book> {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMap"/> class.
        /// </summary>
        public BookMap() {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Genre);
            Map(x => x.Writer);
            Map(x => x.BookSeries);
            Map(x => x.BookNumber);
            Map(x => x.PublicationDate);
            Map(x => x.CoverFileName);
            Map(x => x.Summary);
            Map(x => x.WikipediaLink);

            HasMany(x => x.ReadingSessions).Not.LazyLoad().Cascade.All().OrderBy("Date");
        }
    }
}
