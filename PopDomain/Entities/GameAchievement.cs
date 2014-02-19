namespace Pop.Domain.Entities {
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

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
        public virtual DateTime? WinDate { get; set; }

        /// <summary>
        /// Gets or sets the date as a string to properly display and edit it
        /// </summary>
        [DisplayName("Date")]
        public virtual string WinDateAsString {
            get {
                return this.WinDate.HasValue ? this.WinDate.Value.ToString("dd/MM/yyyy", null) : string.Empty;
            }

            set {
                DateTime tempDate;
                if (DateTime.TryParseExact(value, "dd/MM/yyyy", null, DateTimeStyles.None, out tempDate)) {
                    this.WinDate = tempDate;
                } else {
                    this.WinDate = null;
                }
            }
        }

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
