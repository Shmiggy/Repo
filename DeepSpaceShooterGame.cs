namespace SSSG
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;
    using SSSG.Views;
    using SSSG.Utils.Assets;
    using System.Collections.Generic;
    using SSSG.Models;
    using SSSG.Utils.Patterns;
    using SSSG.Input;

    public class DeepSpaceShooterGame : Game, ISubject
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Dictionary<GameState, IView> views;
        private GameModel gameModel;
        private Controller controller;

        private static DeepSpaceShooterGame instance;
        private static Object syncRoot = new Object();

        private List<IObserver> observers;

        private DeepSpaceShooterGame()
        {
            Window.Title = "Deep Space Shooter";
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            observers = new List<IObserver>();
        }

        public GameState State
        {
            get
            {
                return this.gameModel.State;
            }
        }

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
            MediaPlayer.Play(AssetsManager.Instance.getSong(GameAssets.ASSET_SONG_GAME_MUSIC));
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetsManager.Instance.LoadGameAssets(Content);
        }

        protected override void UnloadContent()
        {
            AssetsManager.Instance.UnloadGameAssets(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Notify(gameTime);

            this.controller.Update(gameTime);

            this.views[State].Update(gameTime, this.gameModel);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            this.views[State].Draw(this.gameModel);

            spriteBatch.End();
            base.Draw(gameTime);
        }

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


        #region ISubject Members

        public void AttachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(object payload)
        {
            foreach ( IObserver observer in observers )
            {
                observer.Update(this, payload);
            }
        }

        #endregion

    }
}
