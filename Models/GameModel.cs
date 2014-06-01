namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using SSSG.Utils.Patterns;
    using System.Collections.Generic;

    public class GameModel : ISubject
    {
        public GameState State { get; set; }

        public Player CurrentPlayer;        // holds the player ship
        public List<Enemy> OnScreenEnemies; // for now, holds a list with space mines (enemies)
        int currentGameLevel;               // game difficulty, I think... don't quote me on this one
        static int EnemyCoolDown = 0;       // wtf does this stand for?

        private static int beamCoolDown = 0;
        private static int rocketCoolDown = 0;

        private List<IObserver> observers;

        public GameModel()
        {
            CurrentPlayer = new Player();
            OnScreenEnemies = new List<Enemy>();
            currentGameLevel = 5;
            CurrentPlayer.Initialize();

            observers = new List<IObserver>();
        }

        public bool IsGameOver
        {
            get
            {
                return !CurrentPlayer.IsAlive;
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
            foreach ( var enemy in OnScreenEnemies )
            {
                foreach ( var proj in CurrentPlayer.Projectiles )
                {
                    if ( proj.IsAlive && proj.CollidesWith(enemy) )
                    {
                        enemy.TakeDamage(proj.Damage);
                        proj.TakeDamage(proj.Health);
                    }
                }
            }

            foreach ( var enemy in OnScreenEnemies )
            {
                if ( enemy.CollidesWith(CurrentPlayer) )
                {
                    CurrentPlayer.TakeDamage(enemy.Damage);
                    enemy.TakeDamage(enemy.Health);
                }
            }
        }

        public void UpdateProjectiles()
        {
            CurrentPlayer.UpdateProjectiles();
        }

        public void UpdateEnemies(GameTime gameTime)
        {
            EnemyCoolDown += currentGameLevel;
            if ( EnemyCoolDown >= 500 ) // wtf is 500? rate at which enemies are spawned?
            {
                Enemy newEnemy = new Enemy();
                newEnemy.Initialize();
                OnScreenEnemies.Add(newEnemy);
                Notify(ModelChanges.EnemySpawned);
            }
            EnemyCoolDown %= 500;
            foreach ( Enemy item in OnScreenEnemies )
            {
                item.UpdatePosition(gameTime);
            }
            OnScreenEnemies.RemoveAll((item) => (item.Position.X < -100 || !item.IsAlive));
        }

        public bool PlayerShoot(ProjectileType type)
        {
            bool returnValue = false;

            if (type == ProjectileType.Beam && beamCoolDown == 0)
            {
                fireBeam();
                beamCoolDown++;
                Notify(ModelChanges.BeamProjectileSpawned);
                returnValue = true;
            } 
            else if (type == ProjectileType.Rocket && rocketCoolDown == 0)
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
            CurrentPlayer.Shoot(ProjectileType.Beam);
        }

        private void fireRocket()
        {
            CurrentPlayer.Shoot(ProjectileType.Rocket);
        }

        public void IncrementLevel()
        {
            currentGameLevel++;
        }

        public void MovePlayerUp(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerUp(gameTime);
        }

        public void MovePlayerDown(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerDown(gameTime);
        }

        public void MovePlayerLeft(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerLeft(gameTime);
        }

        public void MovePlayerRight(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerRight(gameTime);
        }

        public void DamagePlayer(int damageValue)
        {
            CurrentPlayer.TakeDamage(damageValue);
        }

        public void ResetPlayerTilt()
        {
            CurrentPlayer.ResetPlayerTilt();
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
    }
}