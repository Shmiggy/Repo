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

        //test
        AnimatedBackGround backGround = new AnimatedBackGround();
        //test-inter
        public Animation animTest,animTest2;
        Vector2 o,l;
        static int i = 0;
        //test

        public DesignPattern()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Assets.Instance();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Instance().LoadGameAssets(Content);

            //test
            o.Y = 300;
            o.X = 100;
            l.Y = 300;
            l.X = 300;
            animTest = new Animation();
            animTest.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_REAPER), o, 128, 96, 11, 60);
            animTest2 = new Animation();
            animTest2.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_ROCKET), l, 64, 16, 6, 60);
            //test-inter
            backGround.Initialize(Assets.Instance().getTexture(GameAssets.ASSET_TEXTURE_STARS), 800, 1);
            //test
        }

        protected override void UnloadContent()
        {
            Assets.Instance().UnloadGameAssets(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            base.Update(gameTime);

            //test
            animTest.Update(gameTime,i,o);
            if (gameTime.TotalGameTime.Seconds % 6 == 0)
            {
                i = -1;
                o.Y++;
            }
            else if (gameTime.TotalGameTime.Seconds % 6 == 2)
            {
                i = 1;
                o.Y--;
            }
            else
            {
                i = 0;
            }
            animTest2.Update(gameTime,l);
            //test-inter
            backGround.Update();
            //test
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            //test
            backGround.Draw(spriteBatch);
            //test-inter
            animTest.Draw(spriteBatch);
            animTest2.Draw(spriteBatch);
            //test

            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
