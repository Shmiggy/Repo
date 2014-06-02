namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    public interface IGameModel
    {
        Projectile[] OnScreenProjectiles { get; }
        Enemy[] OnScreenEnemies { get; }
        Vector2 ShipPosition { get; }
        int ShipTilt { get; }
    }
}
