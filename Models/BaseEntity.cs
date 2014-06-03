namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public abstract class BaseEntity : ICollidable
    {
        private int health;         // how much damage can the entity receive
        private float speed;        // how fast will the entity move
        private Vector2 position;   // the location of the entity on the screen

        /// <summary>
        /// Gets or sets the health of the entity.
        /// </summary>
        public int Health
        {
            get
            {
                return health;
            }
            protected set
            {
                health = value;
            }
        }

        /// <summary>
        /// Gets or sets the speed of the entity.
        /// </summary>
        public float Speed
        {
            get
            {
                return speed;
            }
            protected set
            {
                speed = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the entity.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
            protected set
            {
                position = value;
            }
        }

        /// <summary>
        /// Returns whether or not the entity collides with another collidable entity.
        /// </summary>
        /// <param name="other">another collidable entity</param>
        /// <returns></returns>
        public bool CollidesWith(ICollidable other)
        {
            return !(other.CollisionBox.Left > this.CollisionBox.Right ||
                       other.CollisionBox.Right < this.CollisionBox.Left ||
                       other.CollisionBox.Top > this.CollisionBox.Bottom ||
                       other.CollisionBox.Bottom < this.CollisionBox.Top);
        }

        /// <summary>
        /// Gets the collision box of the entity.
        /// </summary>
        public abstract Rectangle CollisionBox { get; }

        /// <summary>
        /// Returns whether or not the entity is still alive.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        /// <summary>
        /// Inflicts a certain amount of damage to the current entity.
        /// </summary>
        /// <param name="damageValue">amount of damage</param>
        public void TakeDamage(int damageValue)
        {
            Health -= damageValue;
        }
    }
}
