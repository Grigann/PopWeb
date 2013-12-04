namespace Pop.Domain.Entities {
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Game achievement object
    /// </summary>
    public class GameAchievement {
        /// <summary>
        /// The game
        /// </summary>
        private Game game;

        /// <summary>
        /// Gets the achievement ID
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [DisplayName("Nom")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [DisplayName("Description")]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the value in gamerpoints
        /// </summary>
        [DisplayName("Gamerpoints")]
        public virtual int Gamerpoints { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this achievement is multiplayer
        /// </summary>
        [DisplayName("Multijoueur")]
        public virtual bool IsMultiplayer { get; set; }

        /// <summary>
        /// Gets or sets the associated DLC (can be null)
        /// </summary>
        [DisplayName("DLC associé")]
        public virtual string AssociatedDlc { get; set; }

        /// <summary>
        /// Gets or sets the image file name
        /// </summary>
        [DisplayName("Image")]
        public virtual string ImageFileName { get; set; }

        /// <summary>
        /// Gets or sets the TrueAchievements link
        /// </summary>
        [DisplayName("Lien TA")]
        public virtual string TALink { get; set; }

        /// <summary>
        /// Gets or sets the X360Achievements link
        /// </summary>
        [DisplayName("Lien X360A")]
        public virtual string X360AchievementsLink { get; set; }

        /// <summary>
        /// Gets or sets the win date
        /// </summary>
        [DisplayName("Date")]
        public virtual DateTime? WinDate { get; set; }

        /// <summary>
        /// Gets or sets the game
        /// </summary>
        public virtual Game Game {
            get {
                return this.game;
            }

            set {
                this.game = value;
                this.GameId = value == null ? 0 : value.Id;
            }
        }

        /// <summary>
        /// Gets or sets the associated game ID
        /// </summary>
        public virtual int GameId { get; set; }
    }
}
