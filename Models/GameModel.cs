namespace SSSG.Models
{
    using Microsoft.Xna.Framework;
    using SSSG.Utils.Patterns;
    using System.Collections.Generic;

    public class GameModel : ISubject, IGameModel
    {
        private List<Enemy> screenEnemies;      // for now, holds a list with space mines (enemies)
        private int currentGameLevel;           // game difficulty
        private static int enemyCoolDown = 0;   // used to limit the number of enemies being created.
        private static int beamCoolDown = 0;    // used to limit the number of beam projectiles being fired.
        private static int rocketCoolDown = 0;  // used to limit the number of rocket projectiles being fired.
        private Player currentPlayer;           // holds the player ship

        private List<IObserver> observers;      // holds the list of observers
        private GameState state;                // current game state

        /// <summary>
        /// Inititalizes a new instance of GameModel class.
        /// </summary>
        public GameModel()
        {
            currentPlayer = new Player();
            screenEnemies = new List<Enemy>();
            currentGameLevel = 5;
            currentPlayer.Initialize();

            observers = new List<IObserver>();
        }

        /// <summary>
        /// Returns whether or not the game is over.
        /// </summary>
        public bool IsGameOver
        {
            get
            {
                return !currentPlayer.IsAlive;
            }
        }

        /// <summary>
        /// Gets or sets the game state.
        /// </summary>
        public GameState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        /// <summary>
        /// Updates the game model. Should be called periodically as the game progresses.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if ( State == GameState.Game )
            {
                updateEnemies(gameTime);
                updateProjectiles();
                updateColisions();
            }

            if ( beamCoolDown != 0 )
            {
                beamCoolDown = (beamCoolDown + 1) % 10;
            }

            if ( rocketCoolDown != 0 )
            {
                rocketCoolDown = (rocketCoolDown + 1) % 50;
            }
        }

        /// <summary>
        /// Handles collisions such as: projectiles vs enemies, playerShip vs enemies
        /// </summary>
        private void updateColisions()
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

        /// <summary>
        /// Updates the projectiles fired by player.
        /// </summary>
        private void updateProjectiles()
        {
            currentPlayer.UpdateProjectiles();
        }

        /// <summary>
        /// Updates the enemies spawned until now.
        /// </summary>
        /// <param name="gameTime">current game time</param>
        private void updateEnemies(GameTime gameTime)
        {
            enemyCoolDown += currentGameLevel;
            if ( enemyCoolDown >= 500 ) // wtf is 500? rate at which enemies are spawned?
            {
                Enemy newEnemy = new Enemy();
                newEnemy.Initialize();
                screenEnemies.Add(newEnemy);
                Notify(ModelChange.EnemySpawned);
            }
            enemyCoolDown %= 500;
            foreach ( Enemy item in screenEnemies )
            {
                item.Update(gameTime);
            }
            screenEnemies.RemoveAll((item) => (item.Position.X < -100 || !item.IsAlive));
        }

        /// <summary>
        /// Attempts to fire a new player projectile.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>whether or not the projectile was fired successfully.</returns>
        public bool FirePlayerProjectile(ProjectileType type)
        {
            if ( type == ProjectileType.Beam && beamCoolDown == 0 )
            {
                fireBeam();
                beamCoolDown += 1;
                Notify(ModelChange.BeamProjectileSpawned);
                return true;
            }
            else if ( type == ProjectileType.Rocket && rocketCoolDown == 0 )
            {
                fireRocket();
                rocketCoolDown += 1;
                Notify(ModelChange.RocketProjectileSpawned);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Fires a beam projectile.
        /// </summary>
        private void fireBeam()
        {
            currentPlayer.Shoot(ProjectileType.Beam);
        }

        /// <summary>
        ///  Fires a rocket projectile.
        /// </summary>
        private void fireRocket()
        {
            currentPlayer.Shoot(ProjectileType.Rocket);
        }

        /// <summary>
        /// Increments game level.
        /// </summary>
        public void IncrementLevel()
        {
            currentGameLevel += 1;
        }

        /// <summary>
        /// Moves the player ship up on the Y-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MovePlayerUp(GameTime gameTime)
        {
            currentPlayer.MoveUp(gameTime);
        }

        /// <summary>
        /// Moves the player ship down on the Y-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MovePlayerDown(GameTime gameTime)
        {
            currentPlayer.MoveDown(gameTime);
        }

        /// <summary>
        /// Moves the player ship to the left on the X-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MovePlayerLeft(GameTime gameTime)
        {
            currentPlayer.MoveLeft(gameTime);
        }

        /// <summary>
        /// Moves the player ship to the right on the X-axis.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void MovePlayerRight(GameTime gameTime)
        {
            currentPlayer.MoveRight(gameTime);
        }

        /// <summary>
        /// Inflicts a certain amount of damage to the current entity.
        /// </summary>
        /// <param name="damageValue"></param>
        public void DamagePlayer(int damageValue)
        {
            currentPlayer.TakeDamage(damageValue);
        }

        /// <summary>
        /// Resets the player ship tilt.
        /// </summary>
        public void ResetPlayerTilt()
        {
            currentPlayer.ResetTilt();
        }

        #region ISubject Members

        /// <summary>
        /// Attaches an observer.
        /// </summary>
        /// <param name="observer">observer to be attached</param>
        public void AttachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// Detaches an observer.
        /// </summary>
        /// <param name="observer">observer to be detached</param>
        public void DetachObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// Notify all observers.
        /// </summary>
        /// <param name="payload">data to be sent</param>
        public void Notify(object payload)
        {
            foreach ( IObserver observer in observers )
            {
                observer.Update(this, payload);
            }
        }

        #endregion

        #region IGameModel Members

        /// <summary>
        /// Gets a copy of the projectiles currently on screen.
        /// </summary>
        public IEnumerable<Projectile> OnScreenProjectiles
        {
            get
            {
                return currentPlayer.Projectiles.ToArray();
            }
        }

        /// <summary>
        /// Gets a copy of the enemies currently on screen.
        /// </summary>
        public IEnumerable<Enemy> OnScreenEnemies
        {
            get
            {
                return screenEnemies.ToArray();
            }
        }

        /// <summary>
        /// Gets the position of the ship.
        /// </summary>
        public Vector2 ShipPosition
        {
            get
            {
                return currentPlayer.Position;
            }
        }

        /// <summary>
        /// Gets the tilt of the ship.
        /// </summary>
        public int ShipTilt
        {
            get
            {
                return currentPlayer.Tilt;
            }
        }

        /// <summary>
        /// Gets the health of the ship.
        /// </summary>
        public int ShipHealth
        {
            get
            {
                return currentPlayer.Health;
            }
        }

        #endregion
    }
}