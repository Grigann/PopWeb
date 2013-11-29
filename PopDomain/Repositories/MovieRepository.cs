//-----------------------------------------------------------------------
// <copyright file="MovieRepository.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;
    using NHibernate.Linq;

    using Pop.Domain.Entities;

    /// <summary>
    /// Movies repository
    /// </summary>
    public class MovieRepository {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieRepository"/> class.
        /// </summary>
        /// <param name="session">A NHibernate session</param>
        public MovieRepository(ISession session) {
            this.Session = session;
        }

        /// <summary>
        /// Gets or sets the NHibernate session
        /// </summary>
        private ISession Session { get; set; }

        /// <summary>
        /// Saves or updates a movie
        /// </summary>
        /// <param name="game">A movie</param>
        public void SaveOrUpdate(Movie movie) {
            if (movie == null) {
                throw new ArgumentNullException("movie");
            }

            this.Session.SaveOrUpdate(movie);
        }

        /// <summary>
        /// Gets all the movies
        /// </summary>
        /// <returns>A list of movies</returns>
        public IList<Movie> All() {
            return this.Session.Query<Movie>().ToList();
        }

        /// <summary>
        /// Gets all the movies with a title matching the given title start
        /// </summary>
        /// <param name="start">A title start</param>
        /// <returns>A list of movies</returns>
        public IList<Movie> AllByTitleStart(string start) {
            return this.Session.Query<Movie>().Where(x => x.Title.StartsWith(start)).ToList();
        }

        /// <summary>
        /// Finds a movie by its id
        /// </summary>
        /// <param name="id">An id</param>
        /// <returns>A movie</returns>
        public Movie Find(int id) {
            return this.Session.Get<Movie>(id);
        }

        /// <summary>
        /// Finds a movie by its title
        /// </summary>
        /// <param name="title">A title</param>
        /// <returns>A movie (null if not found)</returns>
        public Movie FindByTitle(string title) {
            return this.Session.Query<Movie>().Where(x => x.Title == title).SingleOrDefault();
        }
    }
}
