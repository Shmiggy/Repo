using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    static class Controller
    {
        public static String[] gameStates = { "menu", "game" };
        public static String gameState;
        private static Model gameModel = new Model();
        static int projCoolDown = 0;
        static int rocketCoolDown = 0;

        public static Model GameModel
        {
            get { return gameModel; }
            set { gameModel = value; }
        }

        public static void UpdatePlayer()
        {
            gameModel.CurrentPlayer.PlayerStillAlive();
        }

        public static void UpdateEnemy()
        {
            gameModel.UpdateEnemies();
        }

        public static void KeyInput()
        {
            if (projCoolDown != 0)
            {
                projCoolDown = (projCoolDown + 1) % 10;
            }
            if (rocketCoolDown != 0)
            {
                rocketCoolDown = (rocketCoolDown + 1) % 100;
            }
            if (gameState == "game")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Game.Instance.Exit();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    if (rocketCoolDown == 0)
                    {
                        GameModel.CurrentPlayer.PlayerShoot(2);
                        Game.Instance.GameView.AddRocketAnimation();
                        rocketCoolDown++;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (projCoolDown == 0)
                    {
                        GameModel.CurrentPlayer.PlayerShoot(1);
                        Game.Instance.GameView.AddProjectileAnimation();
                        projCoolDown++;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    GameModel.PlayerMoveUp();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    GameModel.PlayerMoveDown();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    GameModel.PlayerMoveRight();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    GameModel.PlayerMoveLeft();
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
                {
                    GameModel.CurrentPlayer.ResetMovemntState();
                }
            }
        }

        public static void btnQuit_OnClick(object sender, EventArgs e)
        {
            Game.Instance.Exit();
        }

        public static void btnPlay_OnClick(object sender, EventArgs e)
        {
            gameState = gameStates[1];
        }
    }
}
