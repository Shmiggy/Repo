namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;

    public abstract class Projectile : BaseEntity
    {
		// TODO: de transformat in proprietati
        public Vector2 projPoz;
        public int projDamage;

        public Projectile(Vector2 spawnPosition)
        {
            Health = 1;
            projPoz = spawnPosition;
        }

        public abstract void Update();

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int)projPoz.X-16,(int)projPoz.Y-4,32,8);
            }
        }

    }
}
