namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    public interface IGameModel
    {
        IEnumerable<Projectile> OnScreenProjectiles { get; }
        IEnumerable<Enemy> OnScreenEnemies { get; }
        Vector2 ShipPosition { get; }
        int ShipTilt { get; }
    }
}
