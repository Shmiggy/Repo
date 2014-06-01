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
        private List<Animation> animations;

        //private List<Animation> beamAnimationList;
        //private List<Animation> rocketAnimationList;
        //private List<Animation> mineAnimationList;
        //private List<Animation> explosionAnimationList;

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

            animations = new List<Animation>();
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_REAPER), Vector2.Zero, 128, 96, 11, 45, 5, AnimationType.PLAYER));

            backgroundAnimation = new AnimatedBackground();
            backgroundAnimation.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_STARS), 800, 1);
        }

        // TODO: take a deep breath and refactor this
        // *Quietly walking away :-" *  -- Alex
        public void Update(GameTime gameTime, IGameModel model)
        {
            // the view receives a stripped-down, read-only version of the model

            Enemy[] enemies = model.OnScreenEnemies;
            Projectile[] projectiles = model.OnScreenProjectiles;
            int enemyCount = 0;
            int projectileCount = 0;

            backgroundAnimation.Update();

            IEnumerator<Animation> animationsIterator = animation.GetEnumerator();
            while (animationsIterator.MoveNext())
            {
                Animation animation = animationsIterator.Current;
                switch (animation.Type)
                {
                    case AnimationType.PLAYER:
                        animation.UpdateByInput(gameTime, model.ShipTilt, model.ShipPosition);
                        break;
                    case AnimationType.MINE:
                        if(!enemies[enemyCount].IsAlive)
                        {
                            animationsIterator.MoveNext();
                            animation = animationsIterator.Current;
                        }
                        animation.Update(gameTime, enemies[enemyCount].Position);
                        ++enemyCount;
                        break;
                    case AnimationType.EXPLOSION:
                        //treated in MINE case
                        break;
                    case AnimationType.BEAM:
                        goto case AnimationType.ROCKET;
                    case AnimationType.ROCKET:
                        animation.Update(gameTime, projectiles[projectileCount].Position);
                        ++projectileCount;
                        break;
                    default:
                        //wrong animation type WTF ? 
                        break;
                }
            }

            //IEnumerator<Enemy> enemyReader = enemies.GetEnumerator();

            //int eCounter = 0;
            //while ( enemyReader.MoveNext() )
            //{
            //    if ( enemyReader.Current.IsAlive )
            //    {
            //        mineAnimationList[eCounter].Update(gameTime, enemyReader.Current.Position);
            //    }
            //    else
            //    {
            //        explosionAnimationList[eCounter].Update(gameTime, enemyReader.Current.Position);
            //    }
            //    eCounter += 1;
            //}

            //if ( eCounter < mineAnimationList.Count )
            //{
            //    mineAnimationList.RemoveRange(eCounter, mineAnimationList.Count - eCounter);
            //    explosionAnimationList.RemoveRange(eCounter, explosionAnimationList.Count - eCounter);
            //}

            //IEnumerator<Projectile> projectileReader = projectiles.GetEnumerator();

            //int bCounter = 0;
            //int rCounter = 0;
            //while ( projectileReader.MoveNext() )
            //{
            //    if ( projectileReader.Current is BeamProjectile )
            //    {
            //        beamAnimationList[bCounter].Update(gameTime, projectileReader.Current.Position);
            //        bCounter += 1;
            //    }
            //    else if ( projectileReader.Current is RocketProjectile )
            //    {
            //        rocketAnimationList[rCounter].Update(gameTime, projectileReader.Current.Position);
            //        rCounter += 1;
            //    }
            //}

            //if ( bCounter < beamAnimationList.Count )
            //{
            //    beamAnimationList.RemoveRange(bCounter, beamAnimationList.Count - bCounter);
            //}

            //if ( rCounter < rocketAnimationList.Count )
            //{
            //    rocketAnimationList.RemoveRange(rCounter, rocketAnimationList.Count - rCounter);
            //}

        }

        public void Draw(IGameModel model)
        {
            backgroundAnimation.Draw(spriteBatch);

            foreach (Animation animation in animations)
            {
                animation.Draw(spriteBatch);
            }

            //IEnumerable<Enemy> enemies = model.OnScreenEnemies;
            //IEnumerable<Projectile> projectiles = model.OnScreenProjectiles;

            //backgroundAnimation.Draw(spriteBatch);

            //IEnumerator<Enemy> enemyReader = enemies.GetEnumerator();

            //int eCounter = 0;
            //while ( enemyReader.MoveNext() )
            //{
            //    if ( enemyReader.Current.IsAlive )
            //    {
            //        mineAnimationList[eCounter].Draw(spriteBatch);
            //    }
            //    else
            //    {
            //        explosionAnimationList[eCounter].Draw(spriteBatch);
            //    }
            //    eCounter += 1;
            //}

            //IEnumerator<Projectile> projectileReader = projectiles.GetEnumerator();

            //int bCounter = 0;
            //int rCounter = 0;
            //while ( projectileReader.MoveNext() )
            //{
            //    if ( projectileReader.Current is BeamProjectile )
            //    {
            //        beamAnimationList[bCounter].Draw(spriteBatch);
            //        bCounter += 1;
            //    }
            //    else if ( projectileReader.Current is RocketProjectile )
            //    {
            //        rocketAnimationList[rCounter].Draw(spriteBatch);
            //        rCounter += 1;
            //    }
            //}

            //playerAnimation.Draw(spriteBatch);
        }

        #endregion

        private void AddEnemyAnimation()
        {
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_MINE), Vector2.Zero, 96, 96, 10, 60, 0, AnimationType.MINE));
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_EXPLOSION), Vector2.Zero, 96, 96, 16, 30, 0, AniamtionType.EXPLOSION));
        }

        private void AddProjectileAnimation()
        {
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0, AnimationType.BEAM));
            newProjAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0);
            beamAnimationList.Add(newProjAnim);
        }

        private void AddRocketAnimation()
        {
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_ROCKET), Vector2.Zero, 64, 16, 6, 60, 0, AnimationType.ROCKET));
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
