using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSG
{
    public class Animation
    {
        Texture2D spriteStrip;
        int elapsedTime;
        int frameTime;
        int frameCount;
        int currentFrame;
        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRect = new Rectangle();
        public int FrameWidth;
        public int FrameHeight;
        public Vector2 Position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime)
        {
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;

            this.Position = position;
            this.spriteStrip = texture;

            this.elapsedTime = 0;
            this.currentFrame = 0;
        }

        public void Update(GameTime gameTime, int input, Vector2 position)
        {
            this.Position = position;
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > frameTime)
            {
                if (input == 0)
                {
                    if (currentFrame > 5)
                    {
                        currentFrame--;
                    }
                    else if (currentFrame < 5)
                    {
                        currentFrame++;
                    }
                }
                else if (input <= -1)
                {
                    if (currentFrame > 0)
                    {
                        currentFrame--;
                    }
                }
                else
                {
                    if (currentFrame < 10)
                    {
                        currentFrame++;
                    }
                }
                elapsedTime = 0;
            }
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            destinationRect = new Rectangle((int)Position.X - FrameWidth / 2, (int)Position.Y - FrameHeight / 2, FrameWidth, FrameHeight);
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.Position = position;
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime)
            {
                currentFrame++;

                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                }
                elapsedTime = 0;
            }
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            destinationRect = new Rectangle((int)Position.X - FrameWidth / 2, (int)Position.Y - FrameHeight / 2, FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, Color.White);
        }
    }
}
