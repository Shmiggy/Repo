namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    public class Player : BaseEntity
    {
        private int tilt;                       // how much will the ship tilt
        private Rectangle movementRect;         // area the player can move into
        private List<Projectile> projectiles;   // all projectiles produced by the player
        private readonly int projectileEdge;    // limit at which the player's projectiles will be removed

        /// <summary>
        /// Initializes an instance of the Player class.
        /// </summary>
        public Player()
        {
            Initialize();
            projectileEdge = 1000;
        }

        /// <summary>
        /// Sets the default values for a Player instance.
        /// </summary>
        public void Initialize()
        {
            Vector2 startLocation = new Vector2 { X = 125, Y = 300 };

            Health = 100;
            Speed = 15.0f;
            tilt = 0;
            Position = startLocation;
            movementRect = new Rectangle(75, 50, 500, 500);
            projectiles = new List<Projectile>();
        }

        /// <summary>
        /// Fires a projectiles.
        /// </summary>
        /// <param name="type">the type of the projectile</param>
        /// <returns>the fired projectile</returns>
        /// <exception cref="ArgumentException">invalid projectile type has been provided.</exception>
        public Projectile Shoot(ProjectileType type)
        {
            Projectile projectile = ProjectileFactory.CreateProjectile(type, Position);

            if ( projectile == null )
            {
                throw new ArgumentException(string.Format("The projectile type {0} is not valid.", type.ToString()));
            }

            projectiles.Add(projectile);
            return projectile;
        }

        /// <summary>
        /// Updates the fired projectiles.
        /// </summary>
        public void UpdateProjectiles()
        {
            foreach ( Projectile item in Projectiles )
            {
                item.Update();
            }
            projectiles.RemoveAll((item) => (item.Position.X > projectileEdge || !item.IsAlive));
        }

        /// <summary>
        /// Resets the tilt.
        /// </summary>
        public void ResetTilt()
        {
            tilt = 0;
        }

        /// <summary>
        /// Gets the tilt.
        /// </summary>
        public int Tilt
        {
            get
            {
                return tilt;
            }
        }

        /// <summary>
        /// Gets the player projectiles.
        /// </summary>
        public List<Projectile> Projectiles
        {
            get
            {
                return projectiles;
            }   
        }

        /// <summary>
        /// Moves the player ship up on the Y-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MoveUp(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) Position.X, (int) (Position.Y - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100))) )
            {
                float x = Position.X;
                float y = Position.Y - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                Position = new Vector2 { X = x, Y = y };
                tilt = 1;
            }
        }

        /// <summary>
        /// Moves the player ship down on the Y-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MoveDown(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) Position.X, (int) (Position.Y + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100))) )
            {
                float x = Position.X;
                float y = Position.Y + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                Position = new Vector2 { X = x, Y = y };
                tilt = -1;
            }
        }

        /// <summary>
        /// Moves the player ship to the left on the X-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MoveLeft(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) (Position.X - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100), (int) Position.Y)) )
            {
                float x = Position.X - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                float y = Position.Y;
                Position = new Vector2 { X = x, Y = y };
            }
        }

        /// <summary>
        /// Moves the player ship to the right on the X-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MoveRight(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) (Position.X + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100), (int) Position.Y)) )
            {
                float x = Position.X + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                float y = Position.Y;
                Position = new Vector2 { X = x, Y = y };
            }
        }

        /// <summary>
        /// Gets the collision box of the entity.
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int) Position.X - 64, (int) Position.Y - 32, 128, 64);
            }
        }

    }
}
