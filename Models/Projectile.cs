namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;

    public abstract class Projectile : BaseEntity
    {
        private int damage;

        public Projectile(Vector2 spawnPosition)
        {
            Health = 1;
            Position = spawnPosition;
        }

        public abstract void Update();

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int) Position.X - 16, (int) Position.Y - 4, 32, 8);
            }
        }

        public int Damage
        {
            get { return damage; }
            protected set { damage = value; }
        }

    }
}
