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
        private SpriteBatch spriteBatch;

        private AnimatedBackground backGround;
        private Animation animPlayer;
        private List<Animation> ProjectileAnimationList;
        private List<Animation> RocketAnimationList;
        private List<Animation> MineAnimationList;
        private List<Animation> ExplosionAnimationList;

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

            ProjectileAnimationList = new List<Animation>();
            RocketAnimationList = new List<Animation>();
            MineAnimationList = new List<Animation>();
            ExplosionAnimationList = new List<Animation>();

            animPlayer = new Animation();
            animPlayer.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_REAPER), Vector2.Zero, 128, 96, 11, 45, 5);

            backGround = new AnimatedBackground();
            backGround.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_STARS), 800, 1);
        }

        // TODO: take a deep breath and refactor this
		// *Quietly walking away*  -- Alex
        public void Update(GameTime gameTime, GameModel model)
        {
            animPlayer.UpdateByInput(gameTime, model.CurrentPlayer.Tilt, model.CurrentPlayer.Position);
            backGround.Update();
            int indexProj = 0;
            int indexRocket = 0;
            for ( int i = 0; i < model.OnScreenEnemies.Count; i++ )
            {
                if ( model.OnScreenEnemies[i].Health > 0 )
                {
                    MineAnimationList[i].Update(gameTime, model.OnScreenEnemies[i].Position);
                }
                else
                {
                    ExplosionAnimationList[i].Update(gameTime, model.OnScreenEnemies[i].Position);
                }
            }
            if ( model.OnScreenEnemies.Count < MineAnimationList.Count )
            {
                MineAnimationList.RemoveRange(model.OnScreenEnemies.Count, MineAnimationList.Count - model.OnScreenEnemies.Count);
                ExplosionAnimationList.RemoveRange(model.OnScreenEnemies.Count, MineAnimationList.Count - model.OnScreenEnemies.Count);
            }
            for ( int i = 0; i < model.CurrentPlayer.Projectiles.Count; i++ )
            {
                if ( model.CurrentPlayer.Projectiles[i].projType == 1 )
                {
                    ProjectileAnimationList[indexProj].Update(gameTime, model.CurrentPlayer.Projectiles[i].projPoz);
                    indexProj++;
                }
                else
                {
                    RocketAnimationList[indexRocket].Update(gameTime, model.CurrentPlayer.Projectiles[i].projPoz);
                    indexRocket++;
                }

            }
            if ( model.CurrentPlayer.Projectiles.Count < ProjectileAnimationList.Count )
            {
                ProjectileAnimationList.RemoveRange(model.CurrentPlayer.Projectiles.Count, ProjectileAnimationList.Count - model.CurrentPlayer.Projectiles.Count);
            }
        }

        public void Draw(GameModel model)
        {
            backGround.Draw(spriteBatch);
            int indexProj = 0;
            int indexRocket = 0;
            for ( int i = 0; i < model.OnScreenEnemies.Count; i++ )
            {
                if ( model.OnScreenEnemies[i].Health > 0 )
                {
                    MineAnimationList[i].Draw(spriteBatch);
                }
                else
                {
                    ExplosionAnimationList[i].Draw(spriteBatch);
                }
            }
            for ( int i = 0; i < model.CurrentPlayer.Projectiles.Count; i++ )
            {
                if ( model.CurrentPlayer.Projectiles[i].projType == 1 )
                {
                    ProjectileAnimationList[indexProj].Draw(spriteBatch);
                    indexProj++;
                }
                else
                {
                    RocketAnimationList[indexRocket].Draw(spriteBatch);
                    indexRocket++;
                }
            }
            animPlayer.Draw(spriteBatch);
        }

        #endregion

        private void AddEnemyAnimation()
        {
            Animation newMineAnim = new Animation();
            newMineAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_MINE), Vector2.Zero, 96, 96, 10, 60, 0);
            MineAnimationList.Add(newMineAnim);
            Animation newExplosionAnim = new Animation();
            newExplosionAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_EXPLOSION), Vector2.Zero, 96, 96, 16, 30, 0);
            ExplosionAnimationList.Add(newExplosionAnim);
        }

        private void AddProjectileAnimation()
        {
            Animation newProjAnim = new Animation();
            newProjAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0);
            ProjectileAnimationList.Add(newProjAnim);
        }

        private void AddRocketAnimation()
        {
            Animation newRocketAnim = new Animation();
            newRocketAnim.Initialize(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_ROCKET), Vector2.Zero, 64, 16, 6, 60, 0);
            RocketAnimationList.Add(newRocketAnim);
        }


        #region IObserver Members

        public void Update(ISubject subject, object payload)
        {
            ModelChanges command = (ModelChanges) payload;
            switch (command)
            {
                case ModelChanges.EnemySpawned:
                    AddEnemyAnimation();
                break;
                case ModelChanges.RocketSpawned:
                    AddRocketAnimation();
                break;
                case ModelChanges.ProjectileSpawned:
                    AddProjectileAnimation();
                break;
            }
        }

        #endregion
    }
}
