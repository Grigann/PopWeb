//-----------------------------------------------------------------------
// <copyright file="GameRepository.cs" company="Laurent Perruche-Joubert">
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
    public class GameRepository {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="session">A NHibernate session</param>
        public GameRepository(ISession session) {
            this.Session = session;
        }

        /// <summary>
        /// Gets or sets the NHibernate session
        /// </summary>
        private ISession Session { get; set; }

        /// <summary>
        /// Saves or updates a game
        /// </summary>
        /// <param name="game">A game</param>
        public void SaveOrUpdate(Game game) {
            if (game == null) {
                throw new ArgumentNullException("game");
            }

            this.Session.SaveOrUpdate(game);
        }

        /// <summary>
        /// Gets all the games
        /// </summary>
        /// <returns>A list of games</returns>
        public IList<Game> All() {
            return this.Session.Query<Game>().ToList();
        }

        /// <summary>
        /// Gets all the games with a title matching the given title start
        /// </summary>
        /// <param name="start">A title start</param>
        /// <returns>A list of games</returns>
        public IList<Game> AllByTitleStart(string start) {
            return this.Session.Query<Game>().Where(x => x.Title.StartsWith(start)).ToList();
        }

        /// <summary>
        /// Finds a game by its id
        /// </summary>
        /// <param name="id">An id</param>
        /// <returns>A game</returns>
        public Game Find(int id) {
            return this.Session.Get<Game>(id);
        }

        /// <summary>
        /// Finds a game by its title
        /// </summary>
        /// <param name="title">A title</param>
        /// <returns>A game (null if not found)</returns>
        public Game FindByTitle(string title) {
            return this.Session.Query<Game>().Where(x => x.Title == title).SingleOrDefault();
        }
    }
}
