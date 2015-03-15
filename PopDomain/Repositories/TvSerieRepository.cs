//-----------------------------------------------------------------------
// <copyright file="TvSerieRepository.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;
    using NHibernate.Linq;

    using Entities;

    /// <summary>
    /// TV series repository
    /// </summary>
    public class TvSerieRepository {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSerieRepository"/> class.
        /// </summary>
        /// <param name="session">A NHibernate session</param>
        public TvSerieRepository(ISession session) {
            this.Session = session;
        }

        /// <summary>
        /// Gets or sets the NHibernate session
        /// </summary>
        private ISession Session { get; set; }

        /// <summary>
        /// Saves or updates a series
        /// </summary>
        /// <param name="tvSerie">A series</param>
        public void SaveOrUpdate(TvSerie tvSerie) {
            if (tvSerie == null) {
                throw new ArgumentNullException("tvSerie");
            }

            this.Session.SaveOrUpdate(tvSerie);
        }

        /// <summary>
        /// Gets all the series
        /// </summary>
        /// <returns>A list of series</returns>
        public IList<TvSerie> All() {
            return this.Session.Query<TvSerie>().ToList();
        }

        /// <summary>
        /// Gets all the series with a title matching the given title start
        /// </summary>
        /// <param name="start">A title start</param>
        /// <returns>A list of series</returns>
        public IList<TvSerie> AllByTitleStart(string start) {
            return this.Session.Query<TvSerie>().Where(x => x.Title.StartsWith(start)).ToList();
        }

        /// <summary>
        /// Finds a series by its id
        /// </summary>
        /// <param name="id">An id</param>
        /// <returns>A TV series</returns>
        public TvSerie Find(int id) {
            return this.Session.Get<TvSerie>(id);
        }

        /// <summary>
        /// Finds a series by its title
        /// </summary>
        /// <param name="title">A title</param>
        /// <returns>A series (null if not found)</returns>
        public TvSerie FindByTitle(string title) {
            return this.Session.Query<TvSerie>().SingleOrDefault(x => x.Title == title);
        }
    }
}
