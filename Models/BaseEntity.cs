namespace SSSG.Models
{
    using Microsoft.Xna.Framework;

    public abstract class BaseEntity : ICollidable
    {
        private int health;
        private float speed;
        private Vector2 position;

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

        public bool CollidesWith(ICollidable other)
        {
            return !(other.ColisionBox.Left > this.ColisionBox.Right ||
                       other.ColisionBox.Right < this.ColisionBox.Left ||
                       other.ColisionBox.Top > this.ColisionBox.Bottom ||
                       other.ColisionBox.Bottom < this.ColisionBox.Top);
        }

        // Template Pattern
        public abstract Rectangle ColisionBox { get; }

        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        public void TakeDamage(int damageValue)
        {
            Health -= damageValue;
        }
    }
}
