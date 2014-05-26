using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SSSG
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public View GameView;
        public GameTime CurrentGameTime { get; set; }

        private Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            GameView = new View();
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
            Assets.Instance();
            GameView.InitializeView(spriteBatch);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Assets.Instance().getSong(GameAssets.ASSET_SONG_GAME_MUSIC));
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Instance().LoadGameAssets(Content);
        }

        protected override void UnloadContent()
        {
            Assets.Instance().UnloadGameAssets(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentGameTime = gameTime;
            if (Controller.gameState == "game")
            {
                Controller.UpdatePlayer();
                Controller.UpdateEnemy();
                Controller.GameModel.UpdateColision();
            }
            Controller.KeyInput();
            GameView.UpdateView(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            GameView.DrawView();

            spriteBatch.End(); 
            base.Draw(gameTime);
        }

        private static Game instance;
        private static Object syncRoot = new Object();

        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Game();
                        }
                    }
                }

                return instance;
            }
        }
         
    }
}
