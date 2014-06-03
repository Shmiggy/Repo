namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public class RocketProjectile : Projectile
    {
        private static int spawnDirection = 1; // used to alternate the side of the ship on which the rocket projectile is spawned.
        private int selfDirection; // on which side of the ship was this projectile spawned

        /// <summary>
        /// Initializes an instance of RocketProjectile class.
        /// </summary>
        /// <param name="spawnPosition">the location on the screen where the projectile should be placed</param>
        private RocketProjectile(Vector2 spawnPosition)
            : base(spawnPosition)
        {
            Speed = -5;
            Damage = 100;
            selfDirection = spawnDirection;
            spawnDirection = -spawnDirection;
        }

        /// <summary>
        /// Updates the projectile position.
        /// </summary>
        public override void Update()
        {
            float x = Position.X;
            float y = Position.Y;

            x += Speed;
            Speed += 0.25f;

            if ( Speed < 0 )
            {
                if ( selfDirection == 1 )
                {
                    y += 3;
                }
                else
                {
                    y -= 3;
                }
            }
            else if ( Speed >= 5 && Speed <= 11.75 )
            {
                if ( selfDirection == -1 )
                {
                    y += 2;
                }
                else
                {
                    y -= 2;
                }
            }

            Position = new Vector2 { X = x, Y = y };
        }

        /// <summary>
        /// Factory class for the RocketProjectile class.
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
                return new RocketProjectile(position);
            }
        }

        /// <summary>
        /// Static constructor. Responsible for registering RocketProjectile's factory.
        /// </summary>
        static RocketProjectile()
        {
            ProjectileFactory.AddFactory(ProjectileType.Rocket, new Factory());
        }

    }
}
