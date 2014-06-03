namespace SSSG
{
    using Microsoft.Xna.Framework.Input;
    using System;
    using SSSG.Models;
    using Microsoft.Xna.Framework;
    using SSSG.Input;

    public class Controller
    {
        private IKeyboardInput keyboard;    // by default, implemented by a wrapper around XNA Keyboard class
        private GameModel model;            // logical representation of the game

        /// <summary>
        /// Initializes a new instance of Controller class.
        /// </summary>
        /// <param name="model">the model of the game</param>
        public Controller(GameModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Sets the current keyboard input.
        /// </summary>
        public IKeyboardInput Keyboard
        {
            set { keyboard = value; }
        }

        /// <summary>
        /// Updates the current game model. Should be called periodically as the game progresses.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void Update(GameTime gameTime)
        {
            if ( model.IsGameOver )
            {
                DeepSpaceShooterGame.Instance.Exit();
            }

            model.Update(gameTime);

            if ( model.State == GameState.Game )
            {
                PollKeyboardInput(gameTime);
            }
        }

        /// <summary>
        /// Polls the keyboard input for state changes and updates the model accordingly.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        public void PollKeyboardInput(GameTime gameTime)
        {
            if ( keyboard.IsKeyDown(Keys.Escape) )
            {
                DeepSpaceShooterGame.Instance.Exit();
            }

            if ( keyboard.IsKeyDown(Keys.Z) )
            {
                model.FirePlayerProjectile(ProjectileType.Rocket);
            }

            if ( keyboard.IsKeyDown(Keys.Space) )
            {
                model.FirePlayerProjectile(ProjectileType.Beam);
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

        /// <summary>
        /// Handles the event of quit button being clicked.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        public void OnBtnQuitOnClick(object sender, EventArgs e)
        {
            DeepSpaceShooterGame.Instance.Exit();
        }

        /// <summary>
        /// Handles the event of play button being clicked.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        public void OnBtnPlayOnClick(object sender, EventArgs e)
        {
            model.State = GameState.Game;
        }
    }
}
