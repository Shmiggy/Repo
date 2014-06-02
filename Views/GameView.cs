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
            List<Projectile> rocketProjectiles = (from p in model.OnScreenProjectiles where p is RocketProjectile select p).ToList();
            List<Projectile> beamProjetiles = (from p in model.OnScreenProjectiles where p is BeamProjectile select p).ToList();
            int enemyCount = 0;
            int rocketCount = 0;
            int beamCount = 0;

            backgroundAnimation.Update();

            for(int i = 0; i < animations.Count; ++i)
            {
                Animation animation = animations[i];
                switch (animation.Type)
                {
                    case AnimationType.PLAYER:
                        animation.UpdateByInput(gameTime, model.ShipTilt, model.ShipPosition);
                        break;
                    case AnimationType.MINE:
                        if(enemyCount >= enemies.Length)
                        {
                            animation.ToBeRemoved = true;
                            animations[i+1].ToBeRemoved = true;
                            break;
                        }

                        if(!enemies[enemyCount].IsAlive)
                        {
                            ++i;
                            animation = animations[i];
                            animation.ToBeRemoved = true;
                            animations[i-1].ToBeRemoved = true;
                        }
                        animation.Update(gameTime, enemies[enemyCount].Position);
                        ++enemyCount;
                        break;
                    case AnimationType.EXPLOSION:
                        //treated in MINE case
                        break;
                    case AnimationType.BEAM:
                        if ( beamCount >= beamProjetiles.Count )
                        {
                            animation.ToBeRemoved = true;
                            break;
                        }
                        animation.Update(gameTime, beamProjetiles[beamCount].Position);
                        ++beamCount;
                        break;
                    case AnimationType.ROCKET:
                        if ( rocketCount >= rocketProjectiles.Count )
                        {
                            animation.ToBeRemoved = true;
                            break;
                        }
                        animation.Update(gameTime, rocketProjectiles[rocketCount].Position);
                        ++rocketCount;
                        break;
                    default:
                        //wrong animation type WTF ? 
                        break;
                }
            }

            animations.RemoveAll((item) => item.ToBeRemoved);
        }

        public void Draw(IGameModel model)
        {
            backgroundAnimation.Draw(spriteBatch);
            Animation playerAnimation = null;

            foreach (Animation animation in animations)
            {
                if(animation.Type == AnimationType.PLAYER)
                {
                    playerAnimation = animation;
                    continue;
                }
                animation.Draw(spriteBatch);
            }

            playerAnimation.Draw(spriteBatch);
        }

        #endregion

        private void AddEnemyAnimation()
        {
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_MINE), Vector2.Zero, 96, 96, 10, 60, 0, AnimationType.MINE));
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_EXPLOSION), Vector2.Zero, 96, 96, 16, 30, 0, AnimationType.EXPLOSION));
        }

        private void AddProjectileAnimation()
        {
            animations.Add(new Animation(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0, AnimationType.BEAM));
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
