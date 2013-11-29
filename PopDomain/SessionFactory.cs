//-----------------------------------------------------------------------
// <copyright file="SessionFactory.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain {
    using System;
    using System.IO;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    using Pop.Domain.Entities;

    /// <summary>
    /// Session factory static class, used to generate database and open session
    /// </summary>
    public static class SessionFactory {
        /// <summary>
        /// Session factory
        /// </summary>
        private static ISessionFactory sessionFactory = null;

        /// <summary>
        /// Initializes the connection and creates the database if necessary
        /// </summary>
        public static void Initialize() {
            sessionFactory = Fluently
                    .Configure()
                    .Database(MsSqlConfiguration.MsSql2008
                            .ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Book>())
                    // .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
        }

        /// <summary>
        /// Opens a session
        /// </summary>
        /// <returns>An NHibernate session</returns>
        public static ISession CreateSession() {
            return sessionFactory.OpenSession();
        }

        /// <summary>
        /// Creates the database
        /// </summary>
        /// <param name="config">An NHibernate configuration</param>
        public static void BuildSchema(Configuration config) {
            new SchemaExport(config).Create(false, true);
        }
    }
}
