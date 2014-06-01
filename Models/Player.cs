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
        /// Initializes an instance of Player.
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


        public void Shoot(ProjectileType type)
        {
            Projectile projectile = ProjectileFactory.CreateProjectile(type, Position);
            if ( projectile != null )
            {
                Projectiles.Add(projectile);
            }
        }


        public void UpdateProjectiles()
        {
            foreach ( Projectile item in Projectiles )
            {
                item.Update();
            }
            Projectiles.RemoveAll((item) => (item.Position.X > projectileEdge || !item.IsAlive));
        }

        public void ResetPlayerTilt()
        {
            tilt = 0;
        }

        public int Tilt
        {
            get
            {
                return tilt;
            }
        }

        public List<Projectile> Projectiles
        {
            get
            {
                return projectiles;
            }
        }

        public void MovePlayerUp(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) Position.X, (int) (Position.Y - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100))) )
            {
                float x = Position.X;
                float y = Position.Y - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                Position = new Vector2 { X = x, Y = y };
                tilt = 1;
            }
        }

        public void MovePlayerDown(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) Position.X, (int) (Position.Y + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100))) )
            {
                float x = Position.X;
                float y = Position.Y + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                Position = new Vector2 { X = x, Y = y };
                tilt = -1;
            }
        }

        public void MovePlayerRight(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) (Position.X + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100), (int) Position.Y)) )
            {
                float x = Position.X + Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                float y = Position.Y;
                Position = new Vector2 { X = x, Y = y };
            }
        }

        public void MovePlayerLeft(GameTime gameTime)
        {
            if ( movementRect.Contains(new Point((int) (Position.X - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100), (int) Position.Y)) )
            {
                float x = Position.X - Speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 100;
                float y = Position.Y;
                Position = new Vector2 { X = x, Y = y };
            }
        }

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int) Position.X - 64, (int) Position.Y - 32, 128, 64);
            }
        }

    }
}
