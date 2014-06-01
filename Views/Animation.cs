namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        private Texture2D spriteStrip;
        private int elapsedTime;
        private int frameTime;
        private int frameCount;
        private int currentFrame;
        private Rectangle sourceRect = new Rectangle();
        private Rectangle destinationRect = new Rectangle();
        private int frameWidth;
        private int frameHeight;
        private Vector2 position;
        public AnimationType Type { get; set; }

        public Animation(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, int frameStart, AnimationType type)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;

            this.position = position;
            this.spriteStrip = texture;

            this.elapsedTime = 0;
            this.currentFrame = frameStart;
            this.Type = type;
        }

        public void UpdateByInput(GameTime gameTime, int input, Vector2 position)
        {
            this.position = position;
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
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            destinationRect = new Rectangle((int)position.X - frameWidth / 2, (int)position.Y - frameHeight / 2, frameWidth, frameHeight);
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.position = position;
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
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            destinationRect = new Rectangle((int)position.X - frameWidth / 2, (int)position.Y - frameHeight / 2, frameWidth, frameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, Color.White);
        }
    }
}
