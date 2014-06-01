namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public class BeamProjectile : Projectile
    {
        private BeamProjectile(Vector2 spawnPosition)
            : base(spawnPosition)
        {
            Damage = 10;
            Speed = 10;
        }

        public override void Update()
        {
            float x = Position.X + Speed;
            float y = Position.Y;
            Position = new Vector2 { X = x, Y = y };
        }

        private class Factory : ProjectileFactory
        {
            protected override Projectile create(Vector2 position)
            {
                return new BeamProjectile(position);
            }
        }

        static BeamProjectile()
        {
            ProjectileFactory.addFactory(ProjectileType.Beam, new Factory());
        }
    }
}
