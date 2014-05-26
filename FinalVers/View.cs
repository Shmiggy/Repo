using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ButtonControl;

namespace SSSG
{
    public class View
    {
        SpriteBatch spriteBatch;

        //====================//
        //        menu        //
        //====================//
        Texture2D menuBackGround;
        Texture2D menuPlayButtonHover;
        Texture2D menuPlayButtonNormal;
        Texture2D menuQuitButtonHover;
        Texture2D menuQuitButtonNormal;
        Button btnPlay;
        Button btnQuit;

        //====================//
        //        game        //
        //====================//
        AnimatedBackGround backGround;
        Animation animPlayer;
        List<Animation> ProjectileAnimationList;
        List<Animation> RocketAnimationList;
        List<Animation> MineAnimationList;
        List<Animation> ExplosionAnimationList;

        public void InitializeView(SpriteBatch sBatch)
        {
            Controller.gameState = Controller.gameStates[0];

            spriteBatch = sBatch;

            //====================//
            //        menu        //
            //====================//
            menuBackGround = Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_BG);

            menuPlayButtonHover = Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_PLAYBTNH);
            menuPlayButtonNormal = Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_PLAYBTNN);
            btnPlay = new Button(menuPlayButtonHover, menuPlayButtonNormal, spriteBatch);
            btnPlay.Location(600, 420);
            btnPlay.OnClick += Controller.btnPlay_OnClick;

            menuQuitButtonHover = Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_QUITBTNH);
            menuQuitButtonNormal = Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_QUITBTNN);
            btnQuit = new Button(menuQuitButtonHover, menuQuitButtonNormal, spriteBatch);
            btnQuit.Location(600, 470);
            btnQuit.OnClick += Controller.btnQuit_OnClick;

            //====================//
            //        game        //
            //====================//
            ProjectileAnimationList = new List<Animation>();
            RocketAnimationList = new List<Animation>();
            MineAnimationList = new List<Animation>();
            ExplosionAnimationList = new List<Animation>();

            animPlayer = new Animation();
            animPlayer.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_REAPER), Vector2.Zero, 128, 96, 11, 45, 5);

            backGround = new AnimatedBackGround();
            backGround.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_STARS), 800, 1);
        }

        public void AddEnemyAnimation()
        {
            Animation newMineAnim = new Animation();
            newMineAnim.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_MINE), Vector2.Zero, 96, 96, 10, 60, 0);
            MineAnimationList.Add(newMineAnim);
            Animation newExplosionAnim = new Animation();
            newExplosionAnim.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_EXPLOSION), Vector2.Zero, 96, 96, 16, 30, 0);
            ExplosionAnimationList.Add(newExplosionAnim);
        }

        public void AddProjectileAnimation()
        {
            Animation newProjAnim = new Animation();
            newProjAnim.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_PROJECTILE), Vector2.Zero, 32, 8, 2, 60, 0);
            ProjectileAnimationList.Add(newProjAnim);
        }

        public void AddRocketAnimation()
        {
            Animation newRocketAnim = new Animation();
            newRocketAnim.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_ROCKET), Vector2.Zero, 64, 16, 6, 60, 0);
            RocketAnimationList.Add(newRocketAnim);
        }

        public void UpdateView(GameTime gameTime)
        {
            if (Controller.gameState == "menu")
            {
                btnPlay.Update();
                btnQuit.Update();
            }
            else if (Controller.gameState == "game")
            {
                animPlayer.UpdatePlayer(gameTime, Controller.GameModel.CurrentPlayer.getMovemntState(), Controller.GameModel.CurrentPlayer.getPlayerPoz());
                backGround.Update();
                Controller.GameModel.CurrentPlayer.UpdateProjectiles();
                int indexProj = 0;
                int indexRocket = 0;
                for (int i = 0; i < Controller.GameModel.OnScreenEnemies.Count; i++)
                {
                    if(Controller.GameModel.OnScreenEnemies[i].enemyHealth > 0)
                    {
                        MineAnimationList[i].Update(gameTime, Controller.GameModel.OnScreenEnemies[i].enemyPosition);
                    }
                    else
                    {
                        ExplosionAnimationList[i].Update(gameTime, Controller.GameModel.OnScreenEnemies[i].enemyPosition);
                    }
                }
                if (Controller.GameModel.OnScreenEnemies.Count < MineAnimationList.Count)
                {
                    MineAnimationList.RemoveRange(Controller.GameModel.OnScreenEnemies.Count , MineAnimationList.Count - Controller.GameModel.OnScreenEnemies.Count );
                    ExplosionAnimationList.RemoveRange(Controller.GameModel.OnScreenEnemies.Count , MineAnimationList.Count - Controller.GameModel.OnScreenEnemies.Count );
                }
                for (int i = 0; i < Controller.GameModel.CurrentPlayer.playerProjectiles.Count; i++)
                {
                    if (Controller.GameModel.CurrentPlayer.playerProjectiles[i].projType == 1)
                    {
                        ProjectileAnimationList[indexProj].Update(gameTime, Controller.GameModel.CurrentPlayer.playerProjectiles[i].projPoz);
                        indexProj++;
                    }
                    else
                    {
                        RocketAnimationList[indexRocket].Update(gameTime, Controller.GameModel.CurrentPlayer.playerProjectiles[i].projPoz);
                        indexRocket++;
                    }

                }
                if (Controller.GameModel.CurrentPlayer.playerProjectiles.Count < ProjectileAnimationList.Count)
                {
                    ProjectileAnimationList.RemoveRange(Controller.GameModel.CurrentPlayer.playerProjectiles.Count, ProjectileAnimationList.Count - Controller.GameModel.CurrentPlayer.playerProjectiles.Count);
                }
            }
        }

        public void DrawView()
        {
            if (Controller.gameState == "menu")
            {
                spriteBatch.Draw(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_BG), Vector2.Zero, Color.White);
                btnPlay.Draw();
                btnQuit.Draw();
            }
            else if (Controller.gameState == "game")
            {
                backGround.Draw(spriteBatch);
                int indexProj = 0;
                int indexRocket = 0;
                for (int i = 0; i < Controller.GameModel.OnScreenEnemies.Count; i++)
                {
                    if (Controller.GameModel.OnScreenEnemies[i].enemyHealth > 0)
                    {
                        MineAnimationList[i].Draw(spriteBatch);
                    }
                    else
                    {
                        ExplosionAnimationList[i].Draw(spriteBatch);
                    }
                }
                for (int i = 0; i < Controller.GameModel.CurrentPlayer.playerProjectiles.Count; i++)
                {
                    if (Controller.GameModel.CurrentPlayer.playerProjectiles[i].projType == 1)
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
        }
    }
}
