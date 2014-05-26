using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    class Player : BaseEntity
    {
        int playerHealth, playerMovementState;
        float playerSpeed;
        Vector2 playerPosition;
        Rectangle playerMovementRect;
        public List<Projectile> playerProjectiles = new List<Projectile>();

        public void Initialize()
        {
            Vector2 startLocation = new Vector2();
            startLocation.X = 125;
            startLocation.Y = 300;

            playerHealth = 100;
            playerMovementState = 0;
            playerSpeed = 15.0f;
            playerPosition = startLocation;
            playerMovementRect = new Rectangle(75, 50, 500, 500);
        }

        public void PlayerStillAlive()
        {
            if (playerHealth <= 0)
            {
                Game.Instance.Exit();
            }
        }

        public void PlayerShoot(int type)
        {
            Projectile newProj = new Projectile(type,playerPosition);
            playerProjectiles.Add(newProj);
        }

        public void UpdateProjectiles()
        {
            foreach (Projectile item in playerProjectiles)
            {
                item.Update();
            }
            playerProjectiles.RemoveAll((item) => (item.projPoz.X > 1000 || !item.IsActive));
        }

        public void ResetMovemntState()
        {
            playerMovementState = 0;
        }

        public int getMovemntState()
        {
            return playerMovementState;
        }

        public Vector2 getPlayerPoz()
        {
            return playerPosition;
        }

        public int getPlayerHealth()
        {
            return playerHealth;
        }

        public void DamagePlayer(int damageValue)
        {
            playerHealth -= damageValue;
        }

        public void MovePlayerUp()
        {
            if (playerMovementRect.Contains(new Point((int)playerPosition.X, (int)(playerPosition.Y - playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100))))
            {
                playerPosition.Y -= playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100;
                playerMovementState = 1;
            }
        }

        public void MovePlayerDown()
        {
            if (playerMovementRect.Contains(new Point((int)playerPosition.X, (int)(playerPosition.Y + playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100))))
            {
                playerPosition.Y += playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100;
                playerMovementState = -1;
            }
        }

        public void MovePlayerRight()
        {
            if (playerMovementRect.Contains(new Point((int)(playerPosition.X + playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100), (int)playerPosition.Y)))
            {
                playerPosition.X += playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100;
            }
        }

        public void MovePlayerLeft()
        {
            if (playerMovementRect.Contains(new Point((int)(playerPosition.X - playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100), (int)playerPosition.Y)))
            {
                playerPosition.X -= playerSpeed * (int)Game.Instance.CurrentGameTime.ElapsedGameTime.TotalMilliseconds / 100;
            }
        }

        public override Rectangle ColisionBox
        {
            get
            {
                return new Rectangle((int)playerPosition.X - 64, (int)playerPosition.Y - 32, 128, 64);
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
