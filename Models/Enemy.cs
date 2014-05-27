namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;

    public class Enemy : BaseEntity
    {
        private int damage; // how much damage can this enemy inflict
        private static readonly Random rng = new Random((int) DateTime.UtcNow.Ticks);

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

        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public void UpdatePosition(GameTime gameTime)
        {
            float y = Position.Y;
            float x = Position.X - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;

            Position = new Vector2 { X = x, Y = y };
        }

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int) Position.X - 32, (int) Position.Y - 32, 64, 64);
            }
        }

    }
}
