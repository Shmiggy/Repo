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
        public GameTime CurrentGameTime { get; set; }

        private Dictionary<GameState, IView> views;

        private static DeepSpaceShooterGame instance;
        private static Object syncRoot = new Object();

        private IMouseInput mouseInput;
        private IKeyboardInput keyboardInput;
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
                return Controller.GameModel.State;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;

            Controller.GameModel = new GameModel { State = GameState.Menu };
            this.views = new Dictionary<GameState, IView>();

            MenuView menuView = new MenuView(spriteBatch);
            menuView.addEventListenerOnPlayButton(Controller.btnPlay_OnClick);
            menuView.addEventListenerOnQuitButton(Controller.btnQuit_OnClick);
            this.views.Add(GameState.Menu, menuView);

            GameView gameView = new GameView(spriteBatch);
            Controller.GameModel.AttachObserver(gameView);
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
            Notify(gameTime); // notifies all registered observers, including keyboard & mouse listeners

            CurrentGameTime = gameTime;

            Controller.Update(gameTime);

            this.views[State].Update(gameTime, Controller.GameModel);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            this.views[State].Draw(Controller.GameModel);

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

        public IMouseInput MouseInput
        {
            get { return mouseInput; }
            set { mouseInput = value; AttachObserver(value); }
        }

        public IKeyboardInput KeyboardInput
        {
            get { return keyboardInput; }
            set { keyboardInput = value; AttachObserver(value); }
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
