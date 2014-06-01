namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public class RocketProjectile : Projectile
    {
        private static int spawnDirection = 1; // why is this static ??
        private int selfDirection;

        private RocketProjectile(Vector2 spawnPosition)
            : base(spawnPosition)
        {
            Speed = -5;
            Damage = 100;
            selfDirection = spawnDirection;
            spawnDirection *= -1;
        }

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

        private class Factory : ProjectileFactory
        {
            protected override Projectile create(Vector2 position)
            {
                return new RocketProjectile(position);
            }
        }

        static RocketProjectile()
        {
            ProjectileFactory.addFactory(ProjectileType.Rocket, new Factory());
        }

    }
}
