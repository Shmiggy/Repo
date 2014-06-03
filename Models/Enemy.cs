namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;

    public class Enemy : BaseEntity
    {
        private int damage;                 // how much damage can this enemy inflict
        private static readonly Random rng; // random number generator

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Enemy()
        {
            rng = new Random((int) DateTime.UtcNow.Ticks);
        }

        /// <summary>
        /// Initializes the Enemy instance.
        /// </summary>
        public void Initialize()
        {
            Vector2 startLocation = new Vector2();
            startLocation.X = 1000;
            startLocation.Y = rng.Next(50, 550);

            Health = 100;
            damage = 20;
            Speed = 5.0f;
            Position = startLocation;
        }

        /// <summary>
        /// Gets how much damage can this enemy inflict. 
        /// </summary>
        public int Damage
        {
            get
            {
                return damage;
            }
        }

        /// <summary>
        /// Updates the enemy position.
        /// </summary>
        /// <param name="gameTime">current game time</param>
        public void Update(GameTime gameTime)
        {
            float y = Position.Y;
            float x = Position.X - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;

            Position = new Vector2 { X = x, Y = y };
        }

        /// <summary>
        /// Gets the collision box of the entity.
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int) Position.X - 32, (int) Position.Y - 32, 64, 64);
            }
        }

    }
}
