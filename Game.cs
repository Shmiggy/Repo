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
    public class DesignPattern : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public DesignPattern()
        {
            graphics = new GraphicsDeviceManager(this);
            //Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Assets.Instance();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Assets.Instance().LoadGameAssets(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Assets.Instance().UnloadGameAssets(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(); 
            // TODO: Add your drawing code here


            //test
            Vector2 o;
            o.X = 100;
            o.Y = 100;
            if (gameTime.TotalGameTime.Seconds >= 5)
            {
                Texture2D a;
                a = Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_EXAMPLE);
                spriteBatch.Draw(a, o, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            if (gameTime.TotalGameTime.Seconds >= 1)
            {
                SpriteFont a;
                a = Assets.Instance().getSpriteFont();
                spriteBatch.DrawString(a, "Asset Loading !", new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);
            }
            if (gameTime.TotalGameTime.Seconds == 5 && gameTime.TotalGameTime.Milliseconds == 0 )
            {
                SoundEffect a;
                a = Assets.Instance().getSoundFX(GameAssets.ASSET_SOUNDFX_EXAMPLE);
                a.Play();
            }
            if (gameTime.TotalGameTime.Seconds == 1 && gameTime.TotalGameTime.Milliseconds == 0)
            {
                Song a;
                a = Assets.Instance().getSong(GameAssets.ASSET_SONG_EXAMPLE);
                MediaPlayer.Play(a);
            }
            //test
            

            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
