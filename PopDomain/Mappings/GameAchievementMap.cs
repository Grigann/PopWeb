//-----------------------------------------------------------------------
// <copyright file="GameAchievementMap.cs" company="Laurent Perruche-Joubert">
//     © 2013 Laurent Perruche-Joubert
// </copyright>
//-----------------------------------------------------------------------
namespace Pop.Domain.Mappings {
    using FluentNHibernate.Mapping;

    using Pop.Domain.Entities;

    /// <summary>
    /// Game achievement entity mapping
    /// </summary>
    public class GameAchievementMap : ClassMap<GameAchievement> {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameAchievementMap"/> class.
        /// </summary>
        public GameAchievementMap() {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Gamerpoints);
            Map(x => x.IsMultiplayer);
            Map(x => x.AssociatedDlc);
            Map(x => x.ImageFileName);
            Map(x => x.TALink);
            Map(x => x.X360AchievementsLink);
            Map(x => x.WinDate);

            References(x => x.Game);
        }
    }
}
