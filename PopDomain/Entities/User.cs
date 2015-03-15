//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Entities {
    using System;
    using System.ComponentModel;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// User entity
    /// </summary>
    public class User {
        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        [DisplayName("Identifiant")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        [DisplayName("Mot de passe")]
        public virtual string Password {
            get {
                return string.Empty;
            }

            set {
                var sourceBytes = Encoding.ASCII.GetBytes(value.Trim());
                var hashedBytes = new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
                this.EncryptedPassword = BitConverter.ToString(hashedBytes).Replace("-", "");
            }
        }

        /// <summary>
        /// Gets or sets the encrypted password
        /// </summary>
        public virtual string EncryptedPassword { get; set; }
    }
}
