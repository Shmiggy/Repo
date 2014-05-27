namespace SSSG
{
    using Microsoft.Xna.Framework.Input;
    using System;
    using SSSG.Models;
    using Microsoft.Xna.Framework;

    public static class Controller
    {
        private static GameModel gameModel = new GameModel();
        static int projCoolDown = 0;
        static int rocketCoolDown = 0;

        public static GameModel GameModel
        {
            get { return gameModel; }
            set { gameModel = value; }
        }

        public static void Update(GameTime gameTime)
        {
            if ( GameModel.State == GameState.Game )
            {
                if ( !GameModel.CurrentPlayer.IsAlive )
                {
                    DeepSpaceShooterGame.Instance.Exit();
                }

                //Controller.UpdatePlayer();
                Controller.UpdateEnemy(gameTime);
                Controller.GameModel.CurrentPlayer.UpdateProjectiles();
                Controller.GameModel.UpdateColision();
            }
            Controller.KeyInput(gameTime);
        }

        //public static void UpdatePlayer()
        //{
        //gameModel.CurrentPlayer.PlayerStillAlive();
        // }


        public static void UpdateEnemy(GameTime gameTime)
        {
            gameModel.UpdateEnemies(gameTime);
        }

        public static void KeyInput(GameTime gameTime)
        {
            if ( projCoolDown != 0 )
            {
                projCoolDown = (projCoolDown + 1) % 10;
            }
            if ( rocketCoolDown != 0 )
            {
                rocketCoolDown = (rocketCoolDown + 1) % 100;
            }
            if ( GameModel.State == GameState.Game )
            {
                if ( Keyboard.GetState().IsKeyDown(Keys.Escape) )
                {
                    DeepSpaceShooterGame.Instance.Exit();
                }
                if ( Keyboard.GetState().IsKeyDown(Keys.Z) )
                {
                    if ( rocketCoolDown == 0 )
                    {
                        GameModel.PlayerShoot(2);
                        rocketCoolDown++;
                    }
                }
                if ( Keyboard.GetState().IsKeyDown(Keys.Space) )
                {
                    if ( projCoolDown == 0 )
                    {
                        GameModel.PlayerShoot(1);
                        projCoolDown++;
                    }
                }
                if ( Keyboard.GetState().IsKeyDown(Keys.Up) )
                {
                    GameModel.PlayerMoveUp(gameTime);
                }
                if ( Keyboard.GetState().IsKeyDown(Keys.Down) )
                {
                    GameModel.PlayerMoveDown(gameTime);
                }
                if ( Keyboard.GetState().IsKeyDown(Keys.Right) )
                {
                    GameModel.PlayerMoveRight(gameTime);
                }
                if ( Keyboard.GetState().IsKeyDown(Keys.Left) )
                {
                    GameModel.PlayerMoveLeft(gameTime);
                }
                if ( Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) )
                {
                    GameModel.CurrentPlayer.ResetPlayerTilt();
                }
            }
        }

        public static void btnQuit_OnClick(object sender, EventArgs e)
        {
            DeepSpaceShooterGame.Instance.Exit();
        }

        public static void btnPlay_OnClick(object sender, EventArgs e)
        {
            GameModel.State = GameState.Game;
        }
    }
}
