//-----------------------------------------------------------------------
// <copyright file="UserMap.cs" company="Laurent Perruche-Joubert">
//     © 2013-2015 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Entities;

    /// <summary>
    /// User entity mapping
    /// </summary>
    public class UserMap : ClassMap<User> {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.EncryptedPassword);
        }
    }
}
