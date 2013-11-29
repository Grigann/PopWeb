//-----------------------------------------------------------------------
// <copyright file="BookRepository.cs" company="Laurent Perruche-Joubert">
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
    /// Books repository
    /// </summary>
    public class BookRepository {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="session">A NHibernate session</param>
        public BookRepository(ISession session) {
            this.Session = session;
        }

        /// <summary>
        /// Gets or sets the NHibernate session
        /// </summary>
        private ISession Session { get; set; }

        /// <summary>
        /// Saves or updates a book
        /// </summary>
        /// <param name="book">A book</param>
        public void SaveOrUpdate(Book book) {
            if (book == null) {
                throw new ArgumentNullException("book");
            }

            this.Session.SaveOrUpdate(book);
        }

        /// <summary>
        /// Gets all the books
        /// </summary>
        /// <returns>A list of books</returns>
        public IList<Book> All() {
            return this.Session.Query<Book>().ToList();
        }

        /// <summary>
        /// Gets all the books with a title matching the given title start
        /// </summary>
        /// <param name="start">A title start</param>
        /// <returns>A list of books</returns>
        public IList<Book> AllByTitleStart(string start) {
            return this.Session.Query<Book>().Where(x => x.Title.StartsWith(start)).ToList();
        }

        /// <summary>
        /// Finds a book by its id
        /// </summary>
        /// <param name="id">An id</param>
        /// <returns>A book</returns>
        public Book Find(int id) {
            return this.Session.Get<Book>(id);
        }

        /// <summary>
        /// Finds a book by its title
        /// </summary>
        /// <param name="title">A title</param>
        /// <returns>A book (null if not found)</returns>
        public Book FindByTitle(string title) {
            return this.Session.Query<Book>().Where(x => x.Title == title).SingleOrDefault();
        }
    }
}
