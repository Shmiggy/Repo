namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SSSG.Models;

    public interface IView
    {
        void Initialize(SpriteBatch spriteBatch);
        void Update(GameTime gameTime, GameModel model);
        void Draw(GameModel model);
    }
}
