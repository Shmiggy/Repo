namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;

    public abstract class Projectile : BaseEntity
    {
        private int damage; // the amount of damage this pojectile can cause.

        /// <summary>
        /// Initializes a new instance of Projectile class.
        /// </summary>
        /// <param name="spawnPosition">the location on the screen where the projectile should be placed</param>
        public Projectile(Vector2 spawnPosition)
        {
            Health = 1;
            Position = spawnPosition;
        }
        
        /// <summary>
        /// Updates the projectile position.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Gets the collision box of the entity.
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int) Position.X - 16, (int) Position.Y - 4, 32, 8);
            }
        }

        /// <summary>
        /// Gets or (protected) sets the amount of damage this projectile can cause.
        /// </summary>
        public int Damage
        {
            get { return damage; }
            protected set { damage = value; }
        }

    }
}
