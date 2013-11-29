//-----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System;

    using NHibernate;

    using Pop.Domain.Repositories;

    /// <summary>
    /// Unit of Work object
    /// </summary>
    public class UnitOfWork : IDisposable {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="mustCommit">Whether or not the transaction is expected to be committed (false if only SELECT operations are expected)</param>
        public UnitOfWork(bool mustCommit) {
            this.Session = SessionFactory.CreateSession();
            this.Transaction = this.Session.BeginTransaction();
            this.MustCommit = mustCommit;

            this.Books = new BookRepository(this.Session);
            this.Games = new GameRepository(this.Session);
            this.Movies = new MovieRepository(this.Session);
            this.TvSeries = new TvSerieRepository(this.Session);
        }

        /// <summary>
        /// Gets the books repository
        /// </summary>
        public BookRepository Books { get; private set; }

        /// <summary>
        /// Gets the games repository
        /// </summary>
        public GameRepository Games { get; private set; }

        /// <summary>
        /// Gets the movies repository
        /// </summary>
        public MovieRepository Movies { get; private set; }

        /// <summary>
        /// Gets the movies repository
        /// </summary>
        public TvSerieRepository TvSeries { get; private set; }

        /// <summary>
        /// Gets or sets the NHibernate session
        /// </summary>
        private ISession Session { get; set; }

        /// <summary>
        /// Gets or sets the NHibernate transaction
        /// </summary>
        private ITransaction Transaction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the transaction is expected to be committed
        /// </summary>
        private bool MustCommit { get; set; }

        /// <summary>
        /// Commits the transaction
        /// </summary>
        public void Commit() {
            if (this.Transaction != null && this.Transaction.IsActive) {
                this.Transaction.Commit();
            }
        }

        /// <summary>
        /// Rollbacks the transaction
        /// </summary>
        public void Rollback() {
            if (this.Transaction != null && this.Transaction.IsActive) {
                this.Transaction.Rollback();
            }
        }

        /// <summary>
        /// Disposes the unit of work (rollback the current transaction if no commit has been done)
        /// </summary>
        public void Dispose() {
            if (this.MustCommit && this.Transaction != null && this.Transaction.IsActive) {
                this.Transaction.Rollback();
            }

            if (this.Transaction != null) {
                this.Transaction.Dispose();
            }

            if (this.Session != null) {
                this.Session.Dispose();
            }
        }
    }
}
