namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class AnimatedBackground
    {
        private Texture2D texture;      // background texture
        private Vector2[] positions;    // positions for background
        private float speed;            // speed of the background

        /// <summary>
        /// Initializes the background animation
        /// </summary>
        /// <param name="bgTexture">background texture</param>
        /// <param name="screenWidth">width of the screen</param>
        /// <param name="speed">speed of the background</param>
        public void Initialize(Texture2D bgTexture, int screenWidth, int speed)
        {
            this.texture = bgTexture;
            this.speed = -speed;
            this.positions = new Vector2[screenWidth / this.texture.Width + 1];
            for (int i = 0; i < this.positions.Length; i++)
            {
                this.positions[i] = new Vector2(i * this.texture.Width, 0);
            }
        }

        /// <summary>
        /// Updates positions for background
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].X += speed;
                
                if (speed <= 0)
                {
                    if (positions[i].X <= -texture.Width)
                    {
                        positions[i].X = texture.Width * (positions.Length - 1);
                    }
                }
                else
                {
                    if (positions[i].X >= texture.Width * (positions.Length - 1))
                    {
                        positions[i].X = -texture.Width;
                    }
                }
            }
        }

        /// <summary>
        /// Draw the background
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }
        }
    }
}
