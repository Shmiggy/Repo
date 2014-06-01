namespace SSSG
{
    using Microsoft.Xna.Framework.Input;
    using System;
    using SSSG.Models;
    using Microsoft.Xna.Framework;
    using SSSG.Input;

    public class Controller
    {
        private IKeyboardInput keyboard;
        private GameModel model;

        public Controller(GameModel model)
        {
            this.model = model;
        }

        public IKeyboardInput Keyboard
        {
            set { keyboard = value; }
        }

        public void Update(GameTime gameTime)
        {
            if ( model.IsGameOver )
            {
                DeepSpaceShooterGame.Instance.Exit();
            }

            model.Update(gameTime);

            PollInput(gameTime);
        }

        public void PollInput(GameTime gameTime)
        {

            if ( model.State == GameState.Game )
            {
                if ( keyboard.IsKeyDown(Keys.Escape) )
                {
                    DeepSpaceShooterGame.Instance.Exit();
                }

                if ( keyboard.IsKeyDown(Keys.Z) )
                {
                    model.PlayerShoot(ProjectileType.Rocket);
                }

                if ( keyboard.IsKeyDown(Keys.Space) )
                {
                    model.PlayerShoot(ProjectileType.Beam);
                }

                if ( keyboard.IsKeyDown(Keys.Up) )
                {
                    model.MovePlayerUp(gameTime);
                }

                if ( keyboard.IsKeyDown(Keys.Down) )
                {
                    model.MovePlayerDown(gameTime);
                }

                if ( keyboard.IsKeyDown(Keys.Right) )
                {
                    model.MovePlayerRight(gameTime);
                }

                if ( keyboard.IsKeyDown(Keys.Left) )
                {
                    model.MovePlayerLeft(gameTime);
                }

                if ( keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down) )
                {
                    model.ResetPlayerTilt();
                }
            }
        }

        public void OnBtnQuitOnClick(object sender, EventArgs e)
        {
            DeepSpaceShooterGame.Instance.Exit();
        }

        public void OnBtnPlayOnClick(object sender, EventArgs e)
        {
            model.State = GameState.Game;
        }
    }
}
