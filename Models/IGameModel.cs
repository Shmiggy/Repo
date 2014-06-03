namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    public interface IGameModel
    {
        /// <summary>
        /// Gets a copy of the projectiles currently on screen.
        /// </summary>
        IEnumerable<Projectile> OnScreenProjectiles { get; }

        /// <summary>
        /// Gets a copy of the enemies currently on screen.
        /// </summary>
        IEnumerable<Enemy> OnScreenEnemies { get; }

        /// <summary>
        /// Gets the position of the ship.
        /// </summary>
        Vector2 ShipPosition { get; }

        /// <summary>
        /// Gets the tilt of the ship.
        /// </summary>
        int ShipTilt { get; }

        /// <summary>
        /// Gets the health of the ship.
        /// </summary>
        int ShipHealth { get; }

        /// <summary>
        /// Gets the players score.
        /// </summary>
        int PlayerScore { get; }
    }
}
