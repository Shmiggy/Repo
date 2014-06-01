namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SSSG.Models;
    using SSSG.Utils.Assets;
    using SSSG.Utils.Patterns;
    using System.Collections.Generic;
    using System.Linq;

    public class GameView : IView, IObserver
    {
        private SpriteBatch spriteBatch;

        private AnimatedBackground backgroundAnimation;
        private Animation playerAnimation;

        private List<Animation> beamAnimationList;
        private List<Animation> rocketAnimationList;
        private List<Animation> mineAnimationList;
        private List<Animation> explosionAnimationList;

        public GameView()
        {
        }

        public GameView(SpriteBatch spriteBatch)
        {
            Initialize(spriteBatch);
        }

        #region IView Members

        public void Initialize(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            beamAnimationList = new List<Animation>();
            rocketAnimationList = new List<Animation>();
            mineAnimationList = new List<Animation>();
            explosionAnimationList = new List<Animation>();

            playerAnimation = new Animation();
            playerAnimation.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_REAPER), Vector2.Zero, 128, 96, 11, 45, 5);

            backgroundAnimation = new AnimatedBackground();
            backgroundAnimation.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_STARS), 800, 1);
        }

        // TODO: take a deep breath and refactor this
        // *Quietly walking away :-" *  -- Alex
        public void Update(GameTime gameTime, IGameModel model)
        {
            // the view receives a stripped-down, read-only version of the model

            IEnumerable<Enemy> enemies = model.OnScreenEnemies;
            IEnumerable<Projectile> projectiles = model.OnScreenProjectiles;

            backgroundAnimation.Update();
            playerAnimation.UpdateByInput(gameTime, model.ShipTilt, model.ShipPosition);

            IEnumerator<Enemy> enemyReader = enemies.GetEnumerator();

            int eCounter = 0;
            while ( enemyReader.MoveNext() )
            {
                if ( enemyReader.Current.IsAlive )
                {
                    mineAnimationList[eCounter].Update(gameTime, enemyReader.Current.Position);
                }
                else
                {
                    explosionAnimationList[eCounter].Update(gameTime, enemyReader.Current.Position);
                }
                eCounter += 1;
            }

            if ( eCounter < mineAnimationList.Count )
            {
                mineAnimationList.RemoveRange(eCounter, mineAnimationList.Count - eCounter);
                explosionAnimationList.RemoveRange(eCounter, explosionAnimationList.Count - eCounter);
            }

            IEnumerator<Projectile> projectileReader = projectiles.GetEnumerator();

            int bCounter = 0;
            int rCounter = 0;
            while ( projectileReader.MoveNext() )
            {
                if ( projectileReader.Current is BeamProjectile )
                {
                    beamAnimationList[bCounter].Update(gameTime, projectileReader.Current.Position);
                    bCounter += 1;
                }
                else if ( projectileReader.Current is RocketProjectile )
                {
                    rocketAnimationList[rCounter].Update(gameTime, projectileReader.Current.Position);
                    rCounter += 1;
                }
            }

            if ( bCounter < beamAnimationList.Count )
            {
                beamAnimationList.RemoveRange(bCounter, beamAnimationList.Count - bCounter);
            }

            if ( rCounter < rocketAnimationList.Count )
            {
                rocketAnimationList.RemoveRange(rCounter, rocketAnimationList.Count - rCounter);
            }

        }

        public void Draw(IGameModel model)
        {
            IEnumerable<Enemy> enemies = model.OnScreenEnemies;
            IEnumerable<Projectile> projectiles = model.OnScreenProjectiles;

            backgroundAnimation.Draw(spriteBatch);

            IEnumerator<Enemy> enemyReader = enemies.GetEnumerator();

            int eCounter = 0;
            while ( enemyReader.MoveNext() )
            {
                if ( enemyReader.Current.IsAlive )
                {
                    mineAnimationList[eCounter].Draw(spriteBatch);
                }
                else
                {
                    explosionAnimationList[eCounter].Draw(spriteBatch);
                }
                eCounter += 1;
            }

            IEnumerator<Projectile> projectileReader = projectiles.GetEnumerator();

            int bCounter = 0;
            int rCounter = 0;
            while ( projectileReader.MoveNext() )
            {
                if ( projectileReader.Current is BeamProjectile )
                {
                    beamAnimationList[bCounter].Draw(spriteBatch);
                    bCounter += 1;
                }
                else if ( projectileReader.Current is RocketProjectile )
                {
                    rocketAnimationList[rCounter].Draw(spriteBatch);
                    rCounter += 1;
                }
            }

            playerAnimation.Draw(spriteBatch);
        }

        #endregion

        private void AddEnemyAnimation()
        {
            Animation newMineAnim = new Animation();
            newMineAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_MINE), Vector2.Zero, 96, 96, 10, 60, 0);
            mineAnimationList.Add(newMineAnim);
            Animation newExplosionAnim = new Animation();
            newExplosionAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_EXPLOSION), Vector2.Zero, 96, 96, 16, 30, 0);
            explosionAnimationList.Add(newExplosionAnim);
        }

        private void AddProjectileAnimation()
        {
            Animation newProjAnim = new Animation();
            newProjAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0);
            beamAnimationList.Add(newProjAnim);
        }

        private void AddRocketAnimation()
        {
            Animation newRocketAnim = new Animation();
            newRocketAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_ROCKET), Vector2.Zero, 64, 16, 6, 60, 0);
            rocketAnimationList.Add(newRocketAnim);
        }

        #region IObserver Members

        public void Update(ISubject subject, object payload)
        {
            ModelChanges command = (ModelChanges) payload;
            switch ( command )
            {
                case ModelChanges.EnemySpawned:
                    AddEnemyAnimation();
                    break;
                case ModelChanges.RocketProjectileSpawned:
                    AddRocketAnimation();
                    break;
                case ModelChanges.BeamProjectileSpawned:
                    AddProjectileAnimation();
                    break;
            }
        }

        #endregion
    }
}
