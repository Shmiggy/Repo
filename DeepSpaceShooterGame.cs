namespace SSSG
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;
    using SSSG.Input;
    using SSSG.Models;
    using SSSG.Utils.Assets;
    using SSSG.Views;
    using System;
    using System.Collections.Generic;

    public class DeepSpaceShooterGame : Game
    {
        private GraphicsDeviceManager graphics;         // graphics device
        private SpriteBatch spriteBatch;                // the sprite batch

        private Dictionary<GameState, IView> views;     // views by state
        private GameModel gameModel;                    // the game model
        private Controller controller;                  // the game controller

        private static DeepSpaceShooterGame instance;   // the DeepSpaceShooterGame instance
        private static Object syncRoot = new Object();  // dummy object, used to lock

        /// <summary>
        /// Initializes a new instance of DeepSpaceShooterGame class.
        /// </summary>
        private DeepSpaceShooterGame()
        {
            Window.Title = "Deep Space Shooter";
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        public GameState State
        {
            get
            {
                return this.gameModel.State;
            }
        }

        /// <summary>
        /// Initializes the game data.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;

            this.gameModel = new GameModel { State = GameState.Menu };
            this.controller = new Controller(this.gameModel);
            this.controller.Keyboard = new KeyboardInput();

            this.views = new Dictionary<GameState, IView>();

            MenuView menuView = new MenuView(spriteBatch);
            menuView.addEventListenerOnPlayButton(this.controller.OnBtnPlayOnClick);
            menuView.addEventListenerOnQuitButton(this.controller.OnBtnQuitOnClick);

            GameView gameView = new GameView(spriteBatch);
            this.gameModel.AttachObserver(gameView);

            this.views.Add(GameState.Menu, menuView);
            this.views.Add(GameState.Game, gameView);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(AssetsManager.Instance.GetSong(GameAssets.ASSET_SONG_GAME_MUSIC));
        }

        /// <summary>
        /// Loads assets.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetsManager.Instance.LoadGameAssets(Content);
        }

        /// <summary>
        /// Unloads assets.
        /// </summary>
        protected override void UnloadContent()
        {
            AssetsManager.Instance.UnloadGameAssets(Content);
        }

        /// <summary>
        /// Update section of the game loop
        /// </summary>
        /// <param name="gameTime">current game time</param>
        protected override void Update(GameTime gameTime)
        {
            this.controller.Update(gameTime);

            this.views[State].Update(gameTime, this.gameModel);

            base.Update(gameTime);
        }

        /// <summary>
        ///  Draw section of the game loop
        /// </summary>
        /// <param name="gameTime">current game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            this.views[State].Draw(this.gameModel);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the instance of DeepSpaceShooterGame.
        /// </summary>
        public static DeepSpaceShooterGame Instance
        {
            get
            {
                if ( instance == null )
                {
                    lock ( syncRoot )
                    {
                        if ( instance == null )
                        {
                            instance = new DeepSpaceShooterGame();
                        }
                    }
                }

                return instance;
            }
        }

    }
}
