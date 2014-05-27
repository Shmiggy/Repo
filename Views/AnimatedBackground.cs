namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class AnimatedBackground
    {
        private Texture2D texture;
        private Vector2[] positions;
        private float speed;

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

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }
        }
    }
}
