namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SSSG.Models;
    using SSSG.Utils.Assets;
    using SSSG.Utils.Patterns;
    using System.Collections.Generic;

    public class GameView : IView, IObserver
    {
        private SpriteBatch spriteBatch;                // the sprite batch

        private AnimatedBackground backgroundAnimation; // background animation
        private Animation playerAnimation;              // ship animation

        private List<Animation> beamAnimationList;      // animations for beam projectiles
        private List<Animation> rocketAnimationList;    // animations for rocket projectiles
        private List<Animation> mineAnimationList;      // animations for mines
        private List<Animation> explosionAnimationList; // animations for explosions

        /// <summary>
        /// Initializes a new Intance of GameView class.
        /// </summary>
        public GameView()
        {
        }

        /// <summary>
        /// Initializes a new intance of GameView class.
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public GameView(SpriteBatch spriteBatch)
        {
            Initialize(spriteBatch);
        }

        #region IView Members

        /// <summary>
        /// Initializes the view
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public void Initialize(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            beamAnimationList = new List<Animation>();
            rocketAnimationList = new List<Animation>();
            mineAnimationList = new List<Animation>();
            explosionAnimationList = new List<Animation>();

            playerAnimation = new Animation();
            playerAnimation.Initialize(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_REAPER), Vector2.Zero, 128, 96, 11, 45, 5);

            backgroundAnimation = new AnimatedBackground();
            backgroundAnimation.Initialize(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_STARS), 800, 1);
        }

        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        /// <param name="model">the current game model</param>
        public void Update(GameTime gameTime, IGameModel model)
        {
            // the view receives a stripped-down, read-only version of the model

            IEnumerable<Enemy> enemies = model.OnScreenEnemies;
            IEnumerable<Projectile> projectiles = model.OnScreenProjectiles;

            backgroundAnimation.Update();
            playerAnimation.UpdateByInput(gameTime, model.ShipTilt, model.ShipPosition);

            IEnumerator<Enemy> enemyReader = enemies.GetEnumerator();

            int enemyCounter = 0;
            while ( enemyReader.MoveNext() )
            {
                if ( enemyReader.Current.IsAlive )
                {
                    mineAnimationList[enemyCounter].Update(gameTime, enemyReader.Current.Position);
                }
                else
                {
                    explosionAnimationList[enemyCounter].Update(gameTime, enemyReader.Current.Position);
                }
                enemyCounter += 1;
            }

            if ( enemyCounter < mineAnimationList.Count )
            {
                mineAnimationList.RemoveRange(enemyCounter, mineAnimationList.Count - enemyCounter);
                explosionAnimationList.RemoveRange(enemyCounter, explosionAnimationList.Count - enemyCounter);
            }

            IEnumerator<Projectile> projectileReader = projectiles.GetEnumerator();

            int beamCounter = 0;
            int rocketCounter = 0;
            while ( projectileReader.MoveNext() )
            {
                if ( projectileReader.Current is BeamProjectile )
                {
                    beamAnimationList[beamCounter].Update(gameTime, projectileReader.Current.Position);
                    beamCounter += 1;
                }
                else if ( projectileReader.Current is RocketProjectile )
                {
                    rocketAnimationList[rocketCounter].Update(gameTime, projectileReader.Current.Position);
                    rocketCounter += 1;
                }
            }

            if ( beamCounter < beamAnimationList.Count )
            {
                beamAnimationList.RemoveRange(beamCounter, beamAnimationList.Count - beamCounter);
            }

            if ( rocketCounter < rocketAnimationList.Count )
            {
                rocketAnimationList.RemoveRange(rocketCounter, rocketAnimationList.Count - rocketCounter);
            }

        }

        /// <summary>
        /// Draws the view.
        /// </summary>
        /// <param name="model">the current game model</param>
        public void Draw(IGameModel model)
        {
            IEnumerable<Enemy> enemies = model.OnScreenEnemies;
            IEnumerable<Projectile> projectiles = model.OnScreenProjectiles;

            backgroundAnimation.Draw(spriteBatch);

            IEnumerator<Enemy> enemyReader = enemies.GetEnumerator();

            int enemyCounter = 0;
            while ( enemyReader.MoveNext() )
            {
                if ( enemyReader.Current.IsAlive )
                {
                    mineAnimationList[enemyCounter].Draw(spriteBatch);
                }
                else
                {
                    explosionAnimationList[enemyCounter].Draw(spriteBatch);
                }
                enemyCounter += 1;
            }

            IEnumerator<Projectile> projectileReader = projectiles.GetEnumerator();

            int beamCounter = 0;
            int rocketCounter = 0;
            while ( projectileReader.MoveNext() )
            {
                if ( projectileReader.Current is BeamProjectile )
                {
                    beamAnimationList[beamCounter].Draw(spriteBatch);
                    beamCounter += 1;
                }
                else if ( projectileReader.Current is RocketProjectile )
                {
                    rocketAnimationList[rocketCounter].Draw(spriteBatch);
                    rocketCounter += 1;
                }
            }

            playerAnimation.Draw(spriteBatch);

            SpriteFont font = AssetsManager.Instance.GetSpriteFont();

            string value = model.ShipHealth.ToString();
            spriteBatch.DrawString(font, value, new Vector2(10f, 10f), new Color(0xFF, 0x00, 0x00));
        }

        #endregion

        /// <summary>
        /// Adds an enemy animation.
        /// </summary>
        private void AddEnemyAnimation()
        {
            Animation newMineAnim = new Animation();
            newMineAnim.Initialize(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_MINE), Vector2.Zero, 96, 96, 10, 60, 0);
            mineAnimationList.Add(newMineAnim);
            Animation newExplosionAnim = new Animation();
            newExplosionAnim.Initialize(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_EXPLOSION), Vector2.Zero, 96, 96, 16, 30, 0);
            explosionAnimationList.Add(newExplosionAnim);
        }

        /// <summary>
        /// Adds an projectile animation.
        /// </summary>
        private void AddProjectileAnimation()
        {
            Animation newProjAnim = new Animation();
            newProjAnim.Initialize(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0);
            beamAnimationList.Add(newProjAnim);
        }

        /// <summary>
        /// Adds an rocket animation.
        /// </summary>
        private void AddRocketAnimation()
        {
            Animation newRocketAnim = new Animation();
            newRocketAnim.Initialize(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_ROCKET), Vector2.Zero, 64, 16, 6, 60, 0);
            rocketAnimationList.Add(newRocketAnim);
        }

        #region IObserver Members

        /// <summary>
        /// Updates the current observer.
        /// </summary>
        /// <param name="subject">the sender</param>
        /// <param name="payload">data sent</param>
        public void Update(ISubject subject, object payload)
        {
            ModelChange command = (ModelChange) payload;
            switch ( command )
            {
                case ModelChange.EnemySpawned:
                    AddEnemyAnimation();
                    break;
                case ModelChange.RocketProjectileSpawned:
                    AddRocketAnimation();
                    break;
                case ModelChange.BeamProjectileSpawned:
                    AddProjectileAnimation();
                    break;
            }
        }

        #endregion
    }
}
