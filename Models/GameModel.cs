namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using SSSG.Utils.Patterns;
    using System.Collections.Generic;

    public class GameModel : ISubject
    {
        public GameState State { get; set; }

        public Player CurrentPlayer; // holds the player ship
        public List<Enemy> OnScreenEnemies; // for now, holds a list with space mines (enemies)
        int currentGameLevel; // game difficulty, I think... don't quote me on this one
        static int EnemyCoolDown = 0; // wtf does this stand for?

        private List<IObserver> observers;

        public GameModel()
        {
            CurrentPlayer = new Player();
            OnScreenEnemies = new List<Enemy>();
            currentGameLevel = 5;
            CurrentPlayer.Initialize();

            observers = new List<IObserver>();
        }

        /// <summary>
        /// Handles collisions such as: projectiles vs enemies, playerShip vs enemies
        /// </summary>
        public void UpdateColision()
        {
            foreach ( var enemy in OnScreenEnemies )
            {
                foreach ( var proj in CurrentPlayer.Projectiles )
                {
                    if ( proj.IsAlive && proj.CollidesWith(enemy) )
                    {
                        enemy.TakeDamage(proj.projDamage);
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

        public void PlayerShoot(int type)
        {
            CurrentPlayer.PlayerShoot(type);
            if ( type == 1 )
            {
                Notify(ModelChanges.ProjectileSpawned);
            }
            else if ( type == 2 )
            {
                Notify(ModelChanges.RocketSpawned);
            }
        }

        public void IncrementLevel()
        {
            currentGameLevel++;
        }

        public void PlayerMoveUp(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerUp(gameTime);
        }

        public void PlayerMoveDown(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerDown(gameTime);
        }

        public void PlayerMoveLeft(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerLeft(gameTime);
        }

        public void PlayerMoveRight(GameTime gameTime)
        {
            CurrentPlayer.MovePlayerRight(gameTime);
        }

        public void PlayerDamage(int damageValue)
        {
            CurrentPlayer.TakeDamage(damageValue);
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