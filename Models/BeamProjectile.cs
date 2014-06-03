namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public class BeamProjectile : Projectile
    {
        /// <summary>
        /// Initializes a new instance of BeamProjectile class.
        /// </summary>
        /// <param name="spawnPosition">the location on the screen where the projectile should be placed</param>
        private BeamProjectile(Vector2 spawnPosition)
            : base(spawnPosition)
        {
            Damage = 10;
            Speed = 10;
        }

        /// <summary>
        /// Updates the projectile position.
        /// </summary>
        public override void Update()
        {
            float x = Position.X + Speed;
            float y = Position.Y;
            Position = new Vector2 { X = x, Y = y };
        }

        /// <summary>
        /// Factory class for the BeamProjectile class.
        /// </summary>
        private class Factory : ProjectileFactory
        {
            /// <summary>
            /// Creates a new Projectile object.
            /// </summary>
            /// <param name="position">the location on the screen where the projectile should be placed</param>
            /// <returns>the newly created projectile</returns>
            protected override Projectile create(Vector2 position)
            {
                return new BeamProjectile(position);
            }
        }

        /// <summary>
        /// Static constructor. Responsible for registering BeamProjectile's factory.
        /// </summary>
        static BeamProjectile()
        {
            ProjectileFactory.AddFactory(ProjectileType.Beam, new Factory());
        }
    }
}
