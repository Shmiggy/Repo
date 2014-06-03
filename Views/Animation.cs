namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        private Texture2D spriteStrip;                          // sprite strip
        private int elapsedTime;                                // elapsed time
        private int frameTime;                                  // frame time
        private int frameCount;                                 // frame count
        private int currentFrame;                               // current frame
        private Rectangle sourceRect = new Rectangle();         // source rectangle
        private Rectangle destinationRect = new Rectangle();    // destination rectangle
        private int frameWidth;                                 // width of the frame
        private int frameHeight;                                // height of the frame
        private Vector2 position;                               // animation position

        /// <summary>
        /// Initializes an instance of Animation class.
        /// </summary>
        /// <param name="texture">animation texture</param>
        /// <param name="position">animation position</param>
        /// <param name="frameWidth">width of the frame</param>
        /// <param name="frameHeight">height of the frame</param>
        /// <param name="frameCount">frame count</param>
        /// <param name="frameTime">frame time</param>
        /// <param name="frameStart">initial frame</param>
        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, int frameStart)
        {
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;

            this.position = position;
            this.spriteStrip = texture;

            this.elapsedTime = 0;
            this.currentFrame = frameStart;
        }

        /// <summary>
        /// Updates the animation by input.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        /// <param name="input">the input used for altering the animation normal flow</param>
        /// <param name="position">animation position</param>
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

        /// <summary>
        /// Updates the animation
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        /// <param name="position">animation position</param>
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

        /// <summary>
        /// Draws the animation
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, Color.White);
        }
    }
}
