//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Repositories {
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Entities;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// Users repository
    /// </summary>
    public class UserRepository {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="session">A NHibernate session</param>
        public UserRepository(ISession session) {
            this.Session = session;
        }

        /// <summary>
        /// Gets or sets the NHibernate session
        /// </summary>
        private ISession Session { get; set; }

        /// <summary>
        /// Saves or updates a user
        /// </summary>
        /// <param name="user">A user</param>
        public void SaveOrUpdate(User user) {
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            this.Session.SaveOrUpdate(user);
        }

        /// <summary>
        /// Finds a user by its id
        /// </summary>
        /// <param name="id">An id</param>
        /// <returns>A user</returns>
        public User Find(int id) {
            return this.Session.Get<User>(id);
        }

        /// <summary>
        /// Finds a user by its name and password
        /// </summary>
        /// <param name="name">A name</param>
        /// <param name="password">An non-encrypted password</param>
        /// <returns>A user (null if not found)</returns>
        public User FindByNameAndPassword(string name, string password) {
            var sourceBytes = Encoding.ASCII.GetBytes(password.Trim());
            var hashedBytes = new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
            var encryptedPassword = BitConverter.ToString(hashedBytes).Replace("-", "");
            return this.Session
                .Query<User>()
                .SingleOrDefault(x => x.Name == name && x.EncryptedPassword == encryptedPassword);
        }
    }
}
