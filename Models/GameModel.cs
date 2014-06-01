namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using SSSG.Utils.Patterns;
    using System.Collections.Generic;

    public class GameModel : ISubject, IGameModel
    {
        public GameState State { get; set; }
        public List<Enemy> screenEnemies;         // for now, holds a list with space mines (enemies)

        private int currentGameLevel;               // game difficulty, I think... don't quote me on this one
        private static int enemyCoolDown = 0;
        private static int beamCoolDown = 0;
        private static int rocketCoolDown = 0;
        private Player currentPlayer;               // holds the player ship

        private List<IObserver> observers;

        public GameModel()
        {
            currentPlayer = new Player();
            screenEnemies = new List<Enemy>();
            currentGameLevel = 5;
            currentPlayer.Initialize();

            observers = new List<IObserver>();
        }

        public bool IsGameOver
        {
            get
            {
                return !currentPlayer.IsAlive;
            }
        }

        public void Update(GameTime gameTime)
        {
            if ( State == GameState.Game )
            {
                UpdateEnemies(gameTime);
                UpdateProjectiles();
                UpdateColisions();
            }

            if ( beamCoolDown != 0 )
            {
                beamCoolDown = (beamCoolDown + 1) % 10;
            }

            if ( rocketCoolDown != 0 )
            {
                rocketCoolDown = (rocketCoolDown + 1) % 100;
            }
        }

        /// <summary>
        /// Handles collisions such as: projectiles vs enemies, playerShip vs enemies
        /// </summary>
        public void UpdateColisions()
        {
            foreach ( var enemy in screenEnemies )
            {
                foreach ( var proj in currentPlayer.Projectiles )
                {
                    if ( proj.IsAlive && proj.CollidesWith(enemy) )
                    {
                        enemy.TakeDamage(proj.Damage);
                        proj.TakeDamage(proj.Health);
                    }
                }
            }

            foreach ( var enemy in screenEnemies )
            {
                if ( enemy.CollidesWith(currentPlayer) )
                {
                    currentPlayer.TakeDamage(enemy.Damage);
                    enemy.TakeDamage(enemy.Health);
                }
            }
        }

        public void UpdateProjectiles()
        {
            currentPlayer.UpdateProjectiles();
        }

        public void UpdateEnemies(GameTime gameTime)
        {
            enemyCoolDown += currentGameLevel;
            if ( enemyCoolDown >= 500 ) // wtf is 500? rate at which enemies are spawned?
            {
                Enemy newEnemy = new Enemy();
                newEnemy.Initialize();
                screenEnemies.Add(newEnemy);
                Notify(ModelChanges.EnemySpawned);
            }
            enemyCoolDown %= 500;
            foreach ( Enemy item in screenEnemies )
            {
                item.UpdatePosition(gameTime);
            }
            screenEnemies.RemoveAll((item) => (item.Position.X < -100 || !item.IsAlive));
        }

        public bool PlayerShoot(ProjectileType type)
        {
            bool returnValue = false;

            if ( type == ProjectileType.Beam && beamCoolDown == 0 )
            {
                fireBeam();
                beamCoolDown++;
                Notify(ModelChanges.BeamProjectileSpawned);
                returnValue = true;
            }
            else if ( type == ProjectileType.Rocket && rocketCoolDown == 0 )
            {
                fireRocket();
                rocketCoolDown++;
                Notify(ModelChanges.RocketProjectileSpawned);
                returnValue = true;
            }

            return returnValue;
        }

        private void fireBeam()
        {
            currentPlayer.Shoot(ProjectileType.Beam);
        }

        private void fireRocket()
        {
            currentPlayer.Shoot(ProjectileType.Rocket);
        }

        public void IncrementLevel()
        {
            currentGameLevel++;
        }

        public void MovePlayerUp(GameTime gameTime)
        {
            currentPlayer.MovePlayerUp(gameTime);
        }

        public void MovePlayerDown(GameTime gameTime)
        {
            currentPlayer.MovePlayerDown(gameTime);
        }

        public void MovePlayerLeft(GameTime gameTime)
        {
            currentPlayer.MovePlayerLeft(gameTime);
        }

        public void MovePlayerRight(GameTime gameTime)
        {
            currentPlayer.MovePlayerRight(gameTime);
        }

        public void DamagePlayer(int damageValue)
        {
            currentPlayer.TakeDamage(damageValue);
        }

        public void ResetPlayerTilt()
        {
            currentPlayer.ResetPlayerTilt();
        }

        #region ISubject Members

        public void AttachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(object payload)
        {
            foreach ( IObserver observer in observers )
            {
                observer.Update(this, payload);
            }
        }

        #endregion

        #region IGameModel Members

        public IEnumerable<Projectile> OnScreenProjectiles
        {
            get
            {
                return currentPlayer.Projectiles.ToArray();
            }
        }

        public IEnumerable<Enemy> OnScreenEnemies
        {
            get
            {
                return screenEnemies.ToArray();
            }
        }

        public Vector2 ShipPosition
        {
            get
            {
                return currentPlayer.Position;
            }
        }

        public int ShipTilt
        {
            get
            {
                return currentPlayer.Tilt;
            }
        }

        #endregion
    }
}